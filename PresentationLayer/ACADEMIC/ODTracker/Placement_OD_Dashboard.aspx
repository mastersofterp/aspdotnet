<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Placement_OD_Dashboard.aspx.cs" Inherits="ODTracker_Placement_OD_Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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


    <%--<asp:UpdatePanel ID="updPlacementCoordinator" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Placement OD Dashboard</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="card">
                                    <div class="card-body">
                                        <h6>Student Placement OD</h6>
                                        <i class="fas fa-user-graduate" style="color: #cf8ee2;"></i>
                                        <div class="info">
                                            <span>Pending Request</span>
                                        </div>
                                        <span class="count">
                                            <asp:Label ID="lblStudPlaceReq" runat="server"></asp:Label></span>
                                        <%--<asp:Button ID="btnStdOD" runat="server" CssClass="btn btn-outline-primary viewbtn btn-sm" Text="View All" />--%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="card">
                                    <div class="card-body">
                                        <h6>My Placement Event</h6>
                                        <i class="fas fa-calendar-alt" style="color: #d677a3;"></i>
                                        <div class="info">
                                            <span>Placement Event Count</span>
                                        </div>
                                        <span class="count">
                                            <asp:Label ID="lblMyPlacementPending" runat="server"></asp:Label></span>
                                        <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-primary viewbtn btn-sm" Text="View All" />--%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <button type="button" class="btn btn-outline-primary" data-toggle="modal" data-target="#myModal_Create">Create Placement Event</button>
                                <%--<asp:Button ID="Button2" runat="server" CssClass="btn btn-outline-primary" Text="Create Event" data-toggle="modal" data-target="#myModal_Create" />--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <%--<a class="nav-link active" data-toggle="tab" href="#tab_1">My Event</a>--%>
                                    <asp:Button Text="My Placement OD" ID="btnTabMyPlacementOD" CssClass="nav-link active" runat="server" OnClick="btnTabMyPlacementOD_Click" />
                                </li>
                                <li class="nav-item">
                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2">OD Approval</a>--%>
                                    <asp:Button Text="Student Placement OD Approval" ID="btnTabStudentPlacementODApproval" CssClass="nav-link" runat="server" OnClick="btnTabStudentPlacementODApproval_Click" />
                                </li>
                            </ul>

                            <%--<div class="tab-content" id="my-tab-content">--%>
                            <asp:MultiView ID="MainView" runat="server">
                                <%--<div class="tab-pane active" id="tab_1">--%>
                                <asp:View ID="View1" runat="server">
                                    <div class="col-12 mt-3">
                                        <div class="sub-heading">
                                            <h5>My Placement Event</h5>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Status </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPlacementRequestStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlPlacementRequestStatus_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Value="0">Please Select Status</asp:ListItem>--%>
                                                <asp:ListItem Value="1">Pending for Approval</asp:ListItem>
                                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                <%--<asp:ListItem Value="4">All </asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-12 table-responsive mt-4">
                                        <asp:ListView ID="lvPlacementEventOD" runat="server">
                                            <LayoutTemplate>
                                                <div id="divlvFeeItems" class="vista-grid">
                                                    <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <%--<th>Delete</th>--%>
                                                                    <th>Placement ID</th>
                                                                    <th>Placement Name</th>
                                                                    <th>From Date</th>
                                                                    <th>To Date</th>
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
                                                    <td>
                                                        <asp:ImageButton ID="btnMyPlacementEdit" runat="server" CausesValidation="true" ImageUrl="~/images/edit.png" Enabled='<%# Convert.ToBoolean(Eval("IS_EDIT_DELETE_ENABLE").ToString())%>'
                                                            CommandArgument='<%# Eval("PLACEMENT_SRNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnMyPlacementEdit_Click" />
                                                    </td>
                                                    <%--<td>
                                                                    <asp:ImageButton ID="btnMyPlacementDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("PLACEMENT_SRNO") %>' OnClick="btnMyPlacementDelete_Click" Enabled='<%# Convert.ToBoolean(Eval("IS_EDIT_DELETE_ENABLE").ToString())%>'
                                                                        ToolTip="Delete Record" />
                                                                </td>--%>
                                                    
                                                    <td><%# Eval("PLACEMENT_ID")%></td>
                                                    <td>
                                                        <%# Eval("PLACEMENT_NAME")%>
                                                        <asp:Label ID="lblPlacementEvent_SRNO" runat="server" Visible="false" Text='<%#Eval("PLACEMENT_SRNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("START_DATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("END_DATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%--<%# Eval("REQUEST_STATUS")%>--%>
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
                                <asp:View ID="View2" runat="server">
                                    <%--<div class="tab-pane fade" id="tab_2">--%>
                                    <div class="col-12 mt-3">
                                        <div class="sub-heading">
                                            <h5>Student OD Request</h5>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Status </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudentPlacementOD" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlStudentPlacementOD_SelectedIndexChanged">
                                                <%--<asp:ListItem Value="0">Please Select Status</asp:ListItem>--%>
                                                <asp:ListItem Value="1">Pending for Approval</asp:ListItem>
                                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                <%--<asp:ListItem Value="4">All </asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnApproveStudentPlacementEvent" runat="server" Text="Approve" CssClass="btn btn-primary" TabIndex="7" OnClick="btnApproveStudentPlacementEvent_Click" />
                                        <asp:Button ID="btnRejectStudentPlacementEvent" runat="server" Text="Reject" CssClass="btn btn-warning" TabIndex="8" OnClick="btnRejectStudentPlacementEvent_Click" />
                                    </div>
                                    <div class="col-12 mt-4">
                                        <div class="table-responsive">
                                            <asp:ListView ID="lstStudentPlacement" runat="server">
                                                <LayoutTemplate>
                                                    <div id="divlvStudentPlacement" class="vista-grid">
                                                        <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Select</th>
                                                                        <th>Placement ID</th>
                                                                        <th>Reg No</th>
                                                                        <th>Student Name</th>
                                                                        <th>Placement Name</th>
                                                                        <th>Status</th>
                                                                        <th>Comment</th>
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
                                                            <asp:CheckBox ID="chkStudPlacementSelect" runat="server" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("PLACEMENT_ID")%>                                                             
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO")%>
                                                            <asp:Label ID="lblStudPlacementNo" runat="server" Visible="false" Text='<%#Eval("STUD_PLACEMENT_NO")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STUDNAME")%>                                                             
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPlacementNo" runat="server" Visible="false" Text='<%#Eval("PLACEMENT_NO")%>'></asp:Label>
                                                            <%# Eval("PLACEMENT_NAME")%>                                                             
                                                        </td>
                                                        <td>
                                                            <%--<%# Eval("STATUS")%>--%>
                                                            <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("STATUS")%>' Font-Bold="true"
                                                            ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("STATUS_COLOR").ToString())%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STUDENT_COMMENT")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- The Modal -->
    <div class="modal fade" id="myModal_Create">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Create Placement Event</h4>
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Placement Name </label>
                                </div>
                                <asp:TextBox ID="txtPlacementName" runat="server" CssClass="form-control" TabIndex="1" />
                                <asp:RequiredFieldValidator ID="rfvSessionLName" runat="server" SetFocusOnError="True"
                                    ErrorMessage="Please Enter Placement Name" ControlToValidate="txtPlacementName"
                                    Display="None" ValidationGroup="PlacementSubmit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Start Date</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtStartDate" runat="server" TabIndex="2" CssClass="form-control" AutoComplete="off" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Start Date" ControlToValidate="txtStartDate"
                                        Display="None" ValidationGroup="PlacementSubmit" />
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
                                    <asp:TextBox ID="txtEndDate" runat="server" TabIndex="3" CssClass="form-control" AutoComplete="off" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter End Date" ControlToValidate="txtEndDate"
                                        Display="None" ValidationGroup="PlacementSubmit" />
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
                                    <sup>* </sup>
                                    <label>Placement Head </label>
                                </div>
                                <asp:DropDownList ID="ddlPlacementHead" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select </asp:ListItem>
                                    <%--<asp:ListItem Value="0">Placement Head names Here </asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="True"
                                    ErrorMessage="Please Select Placement Head Name" ControlToValidate="ddlPlacementHead" InitialValue="0"
                                    Display="None" ValidationGroup="PlacementSubmit" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Comment</label>
                                </div>
                                <asp:TextBox ID="txtPlacementComment" runat="server" CssClass="form-control" TabIndex="5" />
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Placement Head Name" ControlToValidate="txtPlacementComment" 
                                            Display="None" ValidationGroup="PlacementSubmit" />--%>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Publish </label>
                                </div>
                                <asp:CheckBox ID="chkIsPublish" runat="server" TabIndex="6" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmitPlacement" runat="server" Text="Create" CssClass="btn btn-primary" TabIndex="7" OnClick="btnSubmitPlacement_Click" CausesValidation="true" ValidationGroup="PlacementSubmit" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="8" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="PlacementSubmit" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
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

</asp:Content>
