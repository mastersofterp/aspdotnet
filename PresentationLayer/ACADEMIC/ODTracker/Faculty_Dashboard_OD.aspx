<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Faculty_Dashboard_OD.aspx.cs" Inherits="ODTracker_Faculty_Dashboard_OD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 5px;
            padding-left: 10px;
            width: 1024px;
            height: 400px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>


    <style>
        .card {
            position: relative;
            background-color: #fff;
            box-shadow: 3px 3px 10px rgb(0 0 0 / 10%);
        }

        .card-body {
            padding: 1rem 1.25rem;
        }


            .card-body h6 {
                font-weight: 600;
            }

            .card-body i {
                font-size: 28px;
                margin-top: 10px;
                margin-bottom: 6px;
            }

        .count {
            position: absolute;
            right: 20px;
            top: 35%;
            font-size: 36px;
        }

        .viewbtn {
            position: absolute;
            right: 20px;
            bottom: 10%;
        }

        #hidden {
            display: none;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #MyEvent.table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }

        .nav-tabs-custom .nav-link {
            background: #fff;
        }
    </style>

    <%--<asp:UpdatePanel ID="updFaculty" runat="server" >
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Faculty Dashboard</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="card">
                                    <div class="card-body">
                                        <h6>Student OD</h6>
                                        <i class="fas fa-user-graduate" style="color: #cf8ee2;"></i>
                                        <div class="info">
                                            <span>Pending Request</span>
                                        </div>
                                        <span class="count">
                                            <asp:Label ID="lblDashboardStudentODCount" runat="server" Text="0"></asp:Label></span>
                                        <%--<asp:Button ID="btnStdOD" runat="server" CssClass="btn btn-outline-primary viewbtn btn-sm" Text="View All" />--%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="card">
                                    <div class="card-body">
                                        <h6>My Events</h6>
                                        <i class="fas fa-calendar-alt" style="color: #d677a3;"></i>
                                        <div class="info">
                                            <span>Event Count</span>
                                        </div>
                                        <span class="count">
                                            <asp:Label ID="lblDashboardmyEventCount" runat="server" Text="0"></asp:Label></span>
                                        <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-primary viewbtn btn-sm" Text="View All" />--%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <button type="button" class="btn btn-outline-primary" data-toggle="modal" data-target="#myModal_Create">Create Event</button>
                                <%--<asp:Button ID="btnCreateCollegeEvent" runat="server" Text="Create Event" class="btn btn-outline-primary" OnClick="btnCreateCollegeEvent_Click" />--%>
                                <%--<asp:Button ID="btnTest" runat="server" Text="Create Event" class="btn btn-outline-primary" OnClick="btnTest_Click" />--%>
                                <%--<asp:Button ID="Button2" runat="server" CssClass="btn btn-outline-primary" Text="Create Event" data-toggle="modal" data-target="#myModal_Create" />--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <%--<a class="nav-link active" data-toggle="tab" href="#tab_1">My Event</a>--%>
                                    <%--<asp:Button ID="btnMyEvent" runat="server" class="nav-link" data-toggle="tab" href="#tab_1" Text="My Event" OnClick="btnMyEvent_Click" />--%>
                                    <asp:Button Text="My Events" ID="btnTabMyEvents" CssClass="nav-link active" runat="server" OnClick="btnTabMyEvents_Click" />
                                </li>
                                <li class="nav-item">
                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2">OD Approval</a>--%>
                                    <%--<asp:Button ID="btnODApproval" runat="server" class="nav-link" data-toggle="tab" href="#tab_2" Text="OD Approval" OnClick="btnODApproval_Click" />--%>
                                    <asp:Button Text="OD Approval" ID="btnTabODApproval" CssClass="nav-link" runat="server" OnClick="btnTabODApproval_Click" />
                                </li>
                            </ul>

                            <%--<div class="tab-content" id="my-tab-content">--%>
                            <asp:MultiView ID="MainView" runat="server">
                                <%--<div class="tab-pane active" id="tab_1">--%>
                                <asp:View ID="View1" runat="server">
                                    <div class="col-12 mt-3">
                                        <div class="sub-heading">
                                            <h5>My Event</h5>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Status </label>
                                            </div>
                                            <asp:DropDownList ID="ddlMyEventStatus" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMyEventStatus_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Pending for Approval</asp:ListItem>
                                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                <%--<asp:ListItem Value="4">All </asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-12 table-responsive">
                                        <asp:ListView ID="lvMyEvent" runat="server">
                                            <LayoutTemplate>
                                                <div id="divlvFeeItems" class="vista-grid">
                                                    <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <%--<th>Delete</th>--%>
                                                                    <th>Event ID</th>
                                                                    <th>Event Name</th>
                                                                    <th>From Date</th>
                                                                    <th>To Date</th>
                                                                    <th>Category</th>
                                                                    <th>Status</th>
                                                                    <th>Publish</th>
                                                                    <th>Comments</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
                                                    <%--<td>
                                                <asp:HyperLink ID="hlkstudentdetail" runat="server" Target="_blank" Text='<%# Eval("STUDNAME") %>'
                                                    NavigateUrl='<%# Eval("LINK_NAME") + "&id=" + Eval("IDNO") %>' />
                                            </td>--%>
                                                    <td>
                                                        <asp:ImageButton ID="btnMyEventEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                            CommandArgument='<%# Eval("EVENT_SRNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnMyEventEdit_Click" />
                                                    </td>
                                                    <%--<td>
                                                                    <asp:ImageButton ID="btnMyEventDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("EVENT_SRNO") %>' OnClick="btnMyEventDelete_Click"
                                                                        ToolTip="Delete Record" OnClientClick="return ConfirmSubmit();" />
                                                                </td>--%>
                                                    <td>
                                                        <%# Eval("EVENT_ID")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EVENT_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("START_DATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("END_DATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EVENTNAME")%>                                                             
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("REQUEST_STATUS")%>' Font-Bold="true"
                                                            ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("REQUEST_STATUS_COLOR").ToString())%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("IS_PUBLISH")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COMMENT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </div>

                                </asp:View>
                                <%--</div>--%>

                                <%--<div class="tab-pane fade" id="tab_2" >--%>
                                <asp:View ID="View2" runat="server">
                                    <div class="col-12 mt-3">
                                        <div class="sub-heading">
                                            <h5>Student OD Request</h5>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Status </label>
                                            </div>
                                            <asp:DropDownList ID="ddlODStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlODStatus_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Value="0">Please Select </asp:ListItem>--%>
                                                <asp:ListItem Value="1">Pending for Approval</asp:ListItem>
                                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                <%--<asp:ListItem Value="4">All </asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary" TabIndex="7" OnClick="btnApprove_Click" />
                                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-warning" TabIndex="8" OnClick="btnReject_Click" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lstODEvent" runat="server">
                                            <LayoutTemplate>
                                                <div id="divlvFeeItems1" class="vista-grid">
                                                    <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblStudent">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Select</th>
                                                                    <th>Event ID</th>
                                                                    <th>Name</th>
                                                                    <th>Date</th>
                                                                    <th>Timing</th>
                                                                    <th>Course Code</th>
                                                                    <th>Event</th>
                                                                    <th>Taken</th>
                                                                    <th>Course</th>
                                                                    <th>Semester</th>
                                                                    <th>Faculty</th>
                                                                    <th>Status</th>
                                                                    <th>Comments</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
                                                    <td>
                                                        <asp:CheckBox ID="chkODEvent" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStudOdCourseNo" runat="server" Text='<%#Eval("STUD_OD_COURSE_NO")%>' Visible="false" />
                                                        <asp:Label ID="lblStudOdNo" runat="server" Text='<%#Eval("STUD_OD_NO")%>' Visible="false" />
                                                        <asp:Label ID="lblCourseNO" runat="server" Text='<%#Eval("COURSENO")%>' Visible="false" />
                                                        <%# Eval("EVENT_ID")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUD_OD_DATE", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TIMEFROM")%> - <%# Eval("TIMETO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CCODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EVENT_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TAKEN")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COURSE_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("STATUS")%>' Font-Bold="true"
                                                        ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("STATUS_COLOR").ToString())%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COMMENT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </div>
                                </asp:View>
                                <%--</div>--%>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <%--<asp:UpdatePanel ID="updFacultyModel" runat="server">
        <ContentTemplate>--%>
    <!-- The Modal -->
    <div class="modal fade" id="myModal_Create">
        <div class="modal-dialog modal-xl">

            <div class="modal-content">


                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title"><b>Create College Event</b></h4>
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Category </label>
                                </div>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select Category</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Please Select Event Category" ValidationGroup="vldGrpCreate" Display="None"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Sub Category </label>
                                </div>
                                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select Sub Category</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubCategory" InitialValue="0" ErrorMessage="Please Select Sub Category" ValidationGroup="vldGrpCreate" Display="None"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Event Name </label>
                                </div>
                                <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="rfvEventName" runat="server" SetFocusOnError="True"
                                    ErrorMessage="Please Enter Event Name" ControlToValidate="txtEventName"
                                    Display="None" ValidationGroup="vldGrpCreate" />
                            </div>

                            <%-------start date------------%>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Start Date</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtStartDate" runat="server" TabIndex="3" CssClass="form-control" AutoComplete="off" />
                                    <%-- <asp:Image ID="imgCalStartDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                        EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtStartDate"
                                        Display="None" ErrorMessage="Please enter start date." SetFocusOnError="true"
                                        ValidationGroup="vldGrpCreate" />
                                    <%--<asp:CompareValidator ID="valStartDateType"
                                        runat="server"
                                        ControlToValidate="txtStartDate"
                                        ControlToCompare="txtEndDate"
                                        Display="Dynamic"
                                        ErrorMessage="Please enter a valid date."
                                        Operator="LessThan"
                                        SetFocusOnError="true"
                                        Type="Date"                                         
                                        CultureInvariantValues="true"
                                        ValidationGroup="submit" />--%>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>End Date</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4" CssClass="form-control" AutoComplete="off" />
                                    <%--<asp:Image ID="imgCalEndDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtEndDate" PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="valEndDate" runat="server" ControlToValidate="txtEndDate"
                                        Display="None" ErrorMessage="Please enter end date." SetFocusOnError="true" ValidationGroup="vldGrpCreate" />
                                    <%--<asp:CompareValidator ID="valEndDateType"
                                        runat="server"
                                        ControlToValidate="txtEndDate"
                                        ControlToCompare="txtStartDate"
                                        Display="Dynamic"
                                        ErrorMessage="Please enter a valid date."
                                        Operator="GreaterThan"
                                        SetFocusOnError="true"
                                        Type="Date"
                                        ValidationGroup="submit"
                                        CultureInvariantValues="true" />--%>
                                </div>
                            </div>




                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup> </sup>
                                    <label>Request as a Special Event </label>
                                </div>
                                <asp:CheckBox ID="chkSpecialEvent" runat="server" TabIndex="5" />
                            </div>
                            <div class="form-group col-lg-1 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Publish </label>
                                </div>
                                <asp:CheckBox ID="chkIsPublish" runat="server" TabIndex="6" />
                            </div>

                            <div class="form-group col-lg-5 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Comment </label>
                                </div>
                                <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TabIndex="7" TextMode="MultiLine" Rows="1" MaxLength="250"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">                        
                        <asp:ValidationSummary ID="createSummry" runat="server" ValidationGroup="vldGrpCreate" ShowMessageBox="true" ShowSummary="false" />
                        <asp:Button ID="btnCreateEvent" runat="server" Text="Create" CssClass="btn btn-primary" TabIndex="8" OnClick="btnCreateEvent_Click" CausesValidation="true" ValidationGroup="vldGrpCreate" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="9" OnClick="btnCancel_Click" OnClientClick="RemoveModelActiveClass();" />
                    </div>

                </div>

            </div>
        </div>
    </div>
    <%--</asp:Panel>--%>
    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlCategory" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnCreateEvent" />             
        </Triggers>
    </asp:UpdatePanel>--%>

    <script>
        function RemoveModelActiveClass() {

        }
    </script>

    <script>
        function collapse(cell) {
            var row = cell.parentElement;
            var target_row = row.parentElement.children[row.rowIndex + 1];
            if (target_row.style.display == 'table-row') {
                cell.innerHTML = '<i class="fas fa-plus" style="color: #fff; background: green; padding:5px; border-radius:50%; "></i>';
                target_row.style.display = 'none';
            } else {
                cell.innerHTML = '<i class="fas fa-minus" style="color: #fff; background: red; padding:5px; border-radius:50%; "></i>';
                target_row.style.display = 'table-row';
            }
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });
    </script>
    <script>
        function TabShow() {
            debugger;
            var tabName = "tab_2";
            alert(tabName);
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
        function ConfirmSubmit() {
            var ret = confirm('Are you sure to delete this entry?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
