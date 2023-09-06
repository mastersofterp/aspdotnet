<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Manage_Tickets.aspx.cs" Inherits="Manage_Tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        //data-toggle="modal"  data-target="#veiw"
        function openModal() {
            $('#veiw').modal('show');
        }


        function shwwindow(myurl) {
            window.open(myurl, '_blank');
        }


        function OpenDetailView() {
            debugger
            $('#View2').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/myfilterOpt.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/semantic.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/Sematic/JS/semantic.min.js")%>"></script>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .ticket {
            text-align: center;
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 15px 15px;
            font-size: 18px;
        }

            .ticket .label-dynamic {
                font-size: 15px;
            }

        .total-ticket {
            border-top: 3px solid #0d70fd;
        }

            .total-ticket span {
                color: #0d70fd;
                font-weight: 600;
            }

        .open-ticket {
            border-top: 3px solid #ef4c4c;
        }

            .open-ticket span {
                color: #ef4c4c;
                font-weight: 600;
            }

        .process-ticket {
            border-top: 3px solid #ffc107;
        }

            .process-ticket span {
                color: #ffc107;
                font-weight: 600;
            }

        .close-ticket {
            border-top: 3px solid #28a745;
        }

            .close-ticket span {
                color: #28a745;
                font-weight: 600;
            }

        .note-msg {
            border: 2px solid #28a745;
            border-radius: 30px;
        }

            .note-msg .fa-bell {
                font-size: 18px;
                padding-top: 5px;
                padding-left: 10px;
                padding: 8px 20px 8px 20px;
                background: #28a745;
                border-top-left-radius: 15px;
                border-bottom-left-radius: 15px;
                color: #fff;
            }

            .note-msg h3 {
                padding: 8px 12px;
                font-size: 16px;
            }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }

        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Manage Ticket</h3>
                </div>

                <div class="box-body">

                    <div class="pageright-wrapper chiller-theme d-none">
                        <div id="sidebar" class="right-wrapper">
                            <%--<a id="showright-sidebar" class="sidebtn" style="cursor: pointer">
                                <asp:Image ID="ImLogo" runat="server" ImageUrl="~/IMAGES/right-arrow.png" class="btnsidebar" />
                            </a>--%>
                            <div class="right-content" style="background-color: #fff;">
                                <div class="right-brand">
                                    <div class="filter-heading">
                                        <a href="#">FILTERS</a>
                                        <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle"></i>

                                        <div class="filter-text input-group  mt-3">
                                            <div class="input-group-prepend input-filter">
                                                <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="sidebar-header scrollbar-right">
                                            <div class="form-group">
                                                <label>Status</label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--<asp:ListItem Value="P">OPEN</asp:ListItem>--%>
                                                    <asp:ListItem Value="C">CLOSE</asp:ListItem>
                                                    <asp:ListItem Value="F">IN PROCESS</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <label>Team</label>
                                                <%--     <asp:DropDownList ID="ddlTeam" runat="server" CssClass="form-control" data-select2-enable="true">
                                           <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>

                                                <asp:DropDownList ID="ddlTeam" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="TRUE">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Date Range</label>
                                                <div id="picker" class="form-control">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label>Category</label>
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="TRUE" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>Sub-Category</label>
                                                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="sidebar-footer">
                                                <asp:LinkButton ID="btnApplyFilter" runat="server" CssClass="btn btn-outline-info">Apply Filter</asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnClearFilter" runat="server" CssClass="btn btn-outline-danger">Clear Filter</asp:LinkButton>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <!-- sidebar-content  -->
                        </div>
                    </div>


                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="ticket total-ticket">
                                    <div class="label-dynamic">
                                        <label>Total Tickets</label>
                                    </div>
                                    <span>
                                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="ticket open-ticket">
                                    <div class="label-dynamic">
                                        <label>Open Tickets</label>
                                    </div>
                                    <span>
                                        <asp:Label ID="lblOpen" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="ticket process-ticket">
                                    <div class="label-dynamic">
                                        <label>In-Process Tickets</label>
                                    </div>
                                    <span>
                                        <asp:Label ID="lblInprocess" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="ticket close-ticket">
                                    <div class="label-dynamic">
                                        <label>Close Tickets</label>
                                    </div>
                                    <span>
                                        <asp:Label ID="lblClose" runat="server" Text=""></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Ticket Details</h5>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-bordered nowrap tblmanageticket" style="width: 100%" id="tab4_mytable">
                            <asp:Repeater ID="lvticket" runat="server">
                                <HeaderTemplate>
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Reference No.</th>
                                            <th>Student ID</th>
                                            <th>Applicant Name</th>
                                            <th>Mobile No.</th>
                                            <th>Email ID</th>
                                            <th>Ticket Date</th>
                                            <th>Assign To</th>
                                            <th>Status</th>
                                            <th>View Details</th>
                                            <th>History</th>
                                            <th>Resolved Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ReferenceNo")%>
                                            <td class="text-center">
                                                <%-- <asp:LinkButton ID="linkButton3" runat="server" Text='<%#Eval("studentId")%>' CommandArgument='<%#Eval("studentId")%>' OnClick="linkButton3_Click" ToolTip="Click for Student Details"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="linkButton3" runat="server" Text='<%#Eval("REGNO")%>' CommandArgument='<%#Eval("studentId")%>' OnClick="linkButton3_Click" ToolTip="Click for Student Details"></asp:LinkButton>
                                            </td>
                                            <td><%# Eval("Applicant")%></td>
                                            <td><%# Eval("Mobile")%></td>
                                            <td><%# Eval("Email")%></td>
                                            <%--<td><%# Eval("CreationDate", "{0:dd/M/yyyy}")%></td>--%>    <%--First time --%>
                                            <td><%# Eval("CreationDate")%></td>
                                            <%--edit this time--%>
                                            <td><%# Eval("AssignedTo")%></td>
                                            <%--<td class="text-center"><span class="badge badge-success">Close</span></td>--%>
                                            <td class="text-center">
                                                <asp:Label ID="lablstatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="lnkbutton" runat="server" CommandArgument='<%#Eval("QMRaiseTicketID")%>' OnClick="LinkButton1_Click1"><i class="fa fa-eye" aria-hidden="true" ></i></asp:LinkButton>
                                            </td>
                                            <%--<td><%# Eval("studentId")%></td>--%>

                                            <td>
                                                <asp:LinkButton ID="linkButton4" runat="server" CommandArgument='<%#Eval("studentId")%>' OnClick="linkButton4_Click"><i class="fa fa-eye" aria-hidden="true" ></i></asp:LinkButton>
                                            </td>

                                            <td><%# Eval("TicketResolveDate")%></td>
                                        </td>
                                    </tr>

                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </FooterTemplate>
                            </asp:Repeater>

                        </table>

                        <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Reference No.</th>
                                    <th>Applicant Name</th>
                                    <th>Mobile No.</th>
                                    <th>Email ID</th>
                                    <th>Ticket Date</th>
                                    <th>Assign To</th>
                                    <th>Status</th>
                                    <th>View</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Ref1234</td>
                                    <td>Ajanta Mendis</td>
                                    <td>0123456789</td>
                                    <td>email_id@email.com</td>
                                    <td>01-01-2022</td>
                                    <td>Rahul Chauvan</td>
                                    <td class="text-center"><span class="badge badge-success">Close</span></td>
                                    <td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i></td>
                                </tr>
                                
                            </tbody>
                        </table>--%>
                    </div>


                    <!-- View Modal -->
                    <div class="modal" id="veiw">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">View Details</h4>
                                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>

                                    <asp:LinkButton ID="btnclear" runat="server" OnClick="btnclear_Click">&times;</asp:LinkButton>
                                </div>

                                <!-- Modal body -->
                                <div class="modal-body pl-0 pr-0">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student ID :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LblRegNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                    <%--<div class="col-12">
                                                <div class="row">--%>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LblStudName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Program :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbDegName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Specialization :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbBranch" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Current year :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbCurrYear" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Semester :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbSemester" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Request Type :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblrequesttype" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Category :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcatgory" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Sub-Category :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblsubcatgory" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Created Date :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="Labcredate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-7 col-8 mt-3 mb-3">
                                                        <div class="note-msg d-flex" id="Emergency" runat="server" visible="false">
                                                            <i class="fa fa-bell"></i>
                                                            <h3 class="text-center mt-0 mb-0" style="color: gray;">Student has Opted for Emergency Services</h3>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-5 col-md-4 col-12 mt-3">
                                                        <ul class="list-group list-group-unbordered" id="amount" runat="server" visible="false">
                                                            <li class="list-group-item"><b>Amount :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"> 1000</asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                    <div class="col-lg-12 col-md-12 col-12 mt-1">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Ticket Description :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true"> </asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-12 mt-1">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>View Attachment:</b>
                                                                <a class="sub-label">
                                                                    <asp:LinkButton ID="lnkdescription" runat="server" CommandArgument='<%#Eval("QMRaiseTicketID")%>' OnClick="lnkdescription_Click1" ToolTip="Download"><i class="fa fa-download fa-3" aria-hidden="true"  style="color: #28a745;"></i></asp:LinkButton>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Re-Assign</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlReAssign" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlReAssign_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divstatus">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStatusView" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusView_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%--<asp:ListItem Value="P">OPEN</asp:ListItem>--%>
                                                            <asp:ListItem Value="C">RESOLVED</asp:ListItem>
                                                            <asp:ListItem Value="F">PROCESSING</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Your Response</label>
                                                        </div>
                                                        <asp:TextBox ID="txtResponse" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-12 col-12">
                                                        <div class="row">

                                                            <div class="form-group col-lg-12 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Upload</label>
                                                                </div>
                                                                <asp:FileUpload ID="fuManageTicket" runat="server" onchange="return CheckFileSize(this)" />
                                                                <%--<input type="file" id="myFile" name="filename2">--%>
<%--                                                               <asp:Label ID="lblfile" runat="server" />--%>
                                                                <asp:Label ID="lblfile" runat="server" />



                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--add new function--%>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<label>Student Remark</label>--%>
                                                            <label>Required Information</label>
                                                        </div>
                                                        <asp:CheckBox ID="chkStudRemark" runat="server" OnCheckedChanged="chkPass_CheckedChanged" AutoPostBack="true" Checked="false" />
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <asp:Panel ID="pnlPassport" runat="server" Visible="false">
                                                            <div class="label-dynamic">
                                                                <%--<label>Required Information</label>--%>
                                                                <label>Student Remark</label>
                                                            </div>
                                                            <asp:TextBox ID="txtReqInfo" runat="server" CssClass="form-control" TextMode="MultiLine"  />
                                                        </asp:Panel>
                                                    </div>


                                                </div>
                                            </div>

                                            <%-- new added --%>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-2 col-md-5 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Change Department</label>
                                                        </div>
                                                        <asp:CheckBox ID="ChkDepartment" runat="server" AutoPostBack="true" OnCheckedChanged="ChkDepartment_CheckedChanged" Checked="false" />
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Service Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDepartmentM" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentM_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <label>Request Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlRequestTypeM" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlRequestTypeM_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRequestType"
                                                            Display="None" ErrorMessage="Please Enter Request Type" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <label>Category</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCategoryM" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlCategoryM_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                                                            Display="None" ErrorMessage="Please Select Category" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <label>Sub-Category</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubCategoryM" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlSubCategoryM_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubCategory"
                                                            Display="None" ErrorMessage="Please Select Sub Category" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>

                            </div>
                        </div>
                    </div>





                    <%-- ------------------Detail View-----------------%>
                    <%--23-08-2022--%>
                    <div class="modal" id="View2">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">


                                <!-- Modal body -->
                                <div class="modal-body pl-0 pr-0">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>

                                            <div class="modal-header">
                                                <h4 class="modal-title">View History</h4>
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            </div>


                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LblStudName1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Program :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbDegName1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Specialization :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbBranch1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Current year :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbCurrYear1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Semester :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="LbSemester1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>



                                                </div>
                                            </div>

                                            <!-- Modal Header -->
                                            <%--notes History date by sp  code --%>

                                            <!-- Modal body -->
                                            <div class="modal-body ">
                                                <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>--%>
                                                <table class="table table-striped table-bordered nowrap tblmanageticket" style="width: 100%" id="Table1">
                                                    <asp:Repeater ID="lvTickerHistory" runat="server">
                                                        <HeaderTemplate>
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Reference No</th>
                                                                    <th>Request Type</th>
                                                                    <th>Request Category</th>
                                                                    <th>Request SubCategory</th>
                                                                    <th>CreationDate</th>
                                                                    <th>Ticket Status</th>
                                                                    <th>Ticket Description</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("ReferenceNo")%>
                                                                <td><%# Eval("QMRequestTypeName")%></td>
                                                                <td><%# Eval("QMRequestCategoryName")%></td>
                                                                <td><%# Eval("QMRequestSubCategoryName")%></td>
                                                                <td><%# Eval("CreationDate", "{0:dd/M/yyyy}")%></td>
                                                                <%--First time --%>
                                                                <%--<td><%# Eval("CreationDate")%></td> --%>       <%--edit this time--%>
                                                                <td><%# Eval("TicketStatus")%> </td>
                                                                <td><%# Eval("TicketDescription") %></td>

                                                                <%--<td><%# Eval("AssignedTo")%></td>--%>
                                                                <%--<td class="text-center"><span class="badge badge-success">Close</span></td>--%>
                                                            </tr>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                        </FooterTemplate>
                                                    </asp:Repeater>

                                                </table>

                                                <%--                                        </ContentTemplate>                                       
                                    </asp:UpdatePanel>--%>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- ENDED BY Aashna 28-10-2021 --%>
    <script>
        (function ($) {
            "use strict";
            $('.label.ui.dropdown')
         .dropdown();
            $('.no.label.ui.dropdown')
              .dropdown({
                  useLabels: false
              });
            $('.ui.button').on('click', function () {
                $('.ui.dropdown')
                  .dropdown('restore defaults')
            })
        })(jQuery);

        var prm1 = Sys.WebForms.PageRequestManager.getInstance();
        prm1.add_endRequest(function () {
            "use strict";
            $('.label.ui.dropdown')
         .dropdown();
            $('.no.label.ui.dropdown')
              .dropdown({
                  useLabels: false
              });
            $('.ui.button').on('click', function () {
                $('.ui.dropdown')
                  .dropdown('restore defaults')
            })
        })(jQuery);
    </script>
    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar").click(function () {
            // alert('hi');
            $(".pageright-wrapper").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var prm2 = Sys.WebForms.PageRequestManager.getInstance();
        prm2.add_endRequest(function () {
            $("#showright-sidebar").click(function () {
                // alert('hi');
                $(".pageright-wrapper").toggleClass("toggleed");
                //  $(".btnsidebar").toggleClass('rotated')
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var prm3 = Sys.WebForms.PageRequestManager.getInstance();
        prm3.add_endRequest(function () {
            $("#filter-toggle").click(function () {
                $(".filter-text").toggle();
                $(".input-filter").addClass('inputfilter')

            });
        });

    </script>


    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>

    <!-- Start End Date Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>

    <script>
        $(document).ready(function () {
            var table = $('.tblmanageticket').DataTable({
                responsive: true,
                lengthChange: true,
                "bSort": false,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('.display').DataTable().column(idx).visible();
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
                                                return $('.display').DataTable().column(idx).visible();
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
                                                return $('.display').DataTable().column(idx).visible();
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
                                                return $('.display').DataTable().column(idx).visible();
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
                var table = $('.display').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('.display').DataTable().column(idx).visible();
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
                                                    return $('.display').DataTable().column(idx).visible();
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
                                                    return $('.display').DataTable().column(idx).visible();
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
                                                    return $('.display').DataTable().column(idx).visible();
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
    </script>


      <script type="text/javascript">
          function CheckFileSize(chk) {

              var maxFileSize = 500000;
              var fi = document.getElementById(chk.id);
              for (var i = 0; i <= fi.files.length - 1; i++) {

                  var fsize = fi.files.item(i).size;

                  if (fsize >= maxFileSize) {
                      alert("File size should not be greater than 500kb");
                      $(chk).val("");
                      return false;

                  }
              }
              var fileExtension = ['pdf', 'jpg', 'jpeg'];
              if ($.inArray($(chk).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                  alert("Only formats are allowed : " + fileExtension.join(', '));
                  $(chk).val("");
              }
          }

    </script>


</asp:Content>

