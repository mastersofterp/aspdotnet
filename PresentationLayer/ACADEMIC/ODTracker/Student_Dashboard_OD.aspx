<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Dashboard_OD.aspx.cs" Inherits="ODTracker_Student_Dashboard_OD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <style>
        .card.green {
            background-color: #b3ffa8;
            box-shadow: 3px 3px 10px rgb(0 0 0 / 10%);
            border: 0px solid transparent;
            color: #005000;
        }

        .card.red {
            background-color: #f5c3c3;
            box-shadow: 3px 3px 10px rgb(0 0 0 / 10%);
            border: 0px solid transparent;
            color: #b40505;
        }

        .card-body {
            padding: 1rem 1.25rem;
        }

        .count {
            font-size: 36px;
            font-weight: 900;
        }

        .new-bx {
            box-shadow: 3px 3px 10px rgb(0 0 0 / 10%);
            padding: 15px 15px 0px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
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
    </div>
    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">OD Request</h3>
                        </div>

                        <div class="box-body">

                            <asp:Repeater ID="rptCourse" runat="server" Visible="true">
                                <ItemTemplate>
                                    <div class="col-12">
                                        <div class="new-bx">
                                            <div class="row">
                                                <div class="form-group col-lg-2 col-md-2 col-4 text-center">
                                                    <div class="card red">
                                                        <div class="card-body">
                                                            <div class="info">
                                                                <span>
                                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSENAME")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCourseNo" runat="server" Text='<%# Eval("COURSENO")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCCODE" runat="server" Text='<%# Eval("CCODE")%>'></asp:Label>
                                                                </span>
                                                            </div>
                                                            <span class="count">
                                                                <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS")%>'></asp:Label></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>OD Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select OD Type</asp:ListItem>
                                            <asp:ListItem Value="1">Event OD </asp:ListItem>
                                            <asp:ListItem Value="2">Placement OD</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvODCategory" runat="server" ControlToValidate="ddlCategory" ErrorMessage="please select OD Category" InitialValue="0" ValidationGroup="ApplyOD"> 
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date </label>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="2" type="date" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" ErrorMessage="please select Event Date" ValidationGroup="ApplyOD"> 
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event </label>
                                        </div>
                                        <asp:DropDownList ID="ddlEvent" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select Event</asp:ListItem>
                                            <%--<asp:ListItem Value="0">OD for Summer Internship (MBA & MHRM)</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEvent" ErrorMessage="please select Event" InitialValue="0" ValidationGroup="ApplyOD"> 
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Timing </label>
                                        </div>
                                        <asp:ListBox ID="lstTiming" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="4">
                                            <%--<asp:ListItem Value="1">09:00AM - 10:00AM</asp:ListItem>
                                    <asp:ListItem Value="2">09:50AM - 10:40AM</asp:ListItem>
                                    <asp:ListItem Value="3">11:10AM - 12:00PM</asp:ListItem>
                                    <asp:ListItem Value="4">12:00PM - 12:50PM</asp:ListItem>
                                    <asp:ListItem Value="5">01:50PM - 02:40AM</asp:ListItem>
                                    <asp:ListItem Value="6">02:40PM - 03:30PM</asp:ListItem>
                                    <asp:ListItem Value="7">04:00PM - 05:00PM</asp:ListItem>--%>
                                        </asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Comments (If Any)</label>
                                        </div>
                                        <asp:TextBox ID="txtStdCommment" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-4 col-md-6 col-12 mt-3">
                                        <ul class="list-group list-group-unbordered">
                                            <%--<li class="list-group-item"><b>Event Co-Ordinator :</b>--%>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblCoordinator" runat="server" Text="Event Co-Ordinator"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEventCoOrdinator" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblEventCoOrdinatorId" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblIsSpecialReq" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12 mt-3">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Current Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrentSem" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnApplyOD" runat="server" Text="Apply For OD" CssClass="btn btn-primary" TabIndex="6" OnClick="btnApplyOD_Click" ValidationGroup="ApplyOD" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="7" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="createSummry" runat="server" ValidationGroup="ApplyOD" ShowMessageBox="true" ShowSummary="false" />
                            </div>

                            <div class="col-12" id="divMyEvent" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>My Event Requests</h5>
                                </div>
                                <asp:ListView ID="lvStudentODRequest" runat="server">
                                    <LayoutTemplate>
                                        <div id="divlvStudentODRequest" class="vista-grid">
                                            <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>OD Type</th>
                                                            <th>Event ID</th>
                                                            <th>OD Date</th>
                                                            <th>Course Code</th>
                                                            <th>Comments</th>
                                                            <th>Faculty Approve Status</th>
                                                            <th>Principal Approve Status</th>
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
                                                <%# Eval("OD_TYPE")%>
                                                <asp:Label ID="lblIdno" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblStudODCourseNo" runat="server" Text='<%# Eval("STUD_OD_COURSE_NO")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("EVENT_ID")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUD_OD_DATE","{0:dd-MM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("CCODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDENT_COMMENT")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFacultyRequestStatus" runat="server" Text='<%# Eval("Faculty_Request_Status")%>' Font-Bold="true"
                                                    ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("FACULTY_REQUEST_STATUS_COLOR").ToString())%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPrincipalRequestStatus" runat="server" Text='<%# Eval("Principal_Request_Status")%>' Font-Bold="true"
                                                    ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("PRINCIPAL_REQUEST_STATUS_COLOR").ToString())%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12" id="divStudPlacementEvent" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Placement OD Requests</h5>
                                </div>
                                <asp:ListView ID="lvStudPlacementEvent" runat="server">
                                    <LayoutTemplate>
                                        <div id="divlvFeeItems" class="vista-grid">
                                            <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>OD Type</th>
                                                            <th>Event ID</th>
                                                            <th>Placement Date</th>
                                                            <th>Comments</th>
                                                            <th>Coordinator Approve Status</th>
                                                            <th>Coordinator Head Approve Status</th>
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
                                                <%# Eval("OD_TYPE")%>
                                                <asp:Label ID="lblIdno" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblEventId" runat="server" Text='<%# Eval("STUD_PLACEMENT_NO")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("PLACEMENT_ID")%>
                                            </td>
                                            <td>
                                                <%# Eval("PLACEMENT_DATE","{0: dd-MM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("COMMENT")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("FACULTY_REQUEST_STATUS")%>' Font-Bold="true"
                                                   ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("FACULTY_REQUEST_STATUS_COLOR").ToString())%>' ></asp:Label>  
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRequestStatusPrincipal" runat="server" Text='<%# Eval("PRINCIPAL_REQUEST_STATUS")%>' Font-Bold="true"
                                                   ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("PRINCIPAL_REQUEST_STATUS_COLOR").ToString())%>' ></asp:Label>  
                                                 
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
</asp:Content>
