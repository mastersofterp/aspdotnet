<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Raise_Ticket.aspx.cs" Inherits="Raise_Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="Scripts/impromptu.css" rel="stylesheet" type="text/javascript" />

    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    </script>--%>
    <%--       <script>
            $(document).ready(function () {
                $(".btnsubmit").on('click', function (event) {
                    event.preventDefault();
                    var el = $(this);
                    el.prop('disabled', true);
                    setTimeout(function () { el.prop('disabled', false); }, 3000);
                });
            });
    </script>--%>



    <script src="https://test-bankofceylon.mtf.gateway.mastercard.com/checkout/version/60/checkout.js" data-error="errorCallback" data-cancel="cancelCallback"></script>
    <script type="text/javascript">
        function errorCallback(error) {
            console.log(JSON.stringify(error));
        }
        function cancelCallback() {
            //console.log('Payment cancelled');
        }
        //cancelCallback = "https://sims.sliit.lk/OnlineResponse.aspx"
        //function completeCallback(resultIndicator, sessionVersion) {
        //    //handle payment completion
        //    completeCallback = "http://localhost:59566/PresentationLayer/OnlineResponse.aspx"
        //}
        completeCallback = "http://localhost:59566/PresentationLayer/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["ERPPaymentSession"] %>'
            },
            interaction: {
                merchant: {
                    name: 'SLIIT',
                    address: {
                        line1: 'TEST',
                        line2: 'DONE'
                    }
                }
            }
        });
    </script>

    <script>
        $(document).ready(function () {


            var table = $('#tab4_mytable').DataTable({
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
                                return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab4_mytable').DataTable().column(idx).visible();
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

                var table = $('#tab4_mytable').DataTable({
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
                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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
        function startvalue() {
            //var rates = document.getElementById('rating').value;
            var rate_value;
            if (document.getElementById('rating10-0').checked) {
                rate_value = document.getElementById('rating10-0').value;

            } else if (document.getElementById('rating10-05').checked) {
                rate_value = document.getElementById('rating10-05').value;

            } else if (document.getElementById('rating10-10').checked) {
                rate_value = document.getElementById('rating10-10').value;
            } else if (document.getElementById('rating10-15').checked) {
                rate_value = document.getElementById('rating10-15').value;

            } else if (document.getElementById('rating10-20').checked) {
                rate_value = document.getElementById('rating10-20').value;
            } else if (document.getElementById('rating10-25').checked) {
                rate_value = document.getElementById('rating10-25').value;

            } else if (document.getElementById('rating10-30').checked) {
                rate_value = document.getElementById('rating10-30').value;
            } else if (document.getElementById('rating10-35').checked) {
                rate_value = document.getElementById('rating10-35').value;

            } else if (document.getElementById('rating10-40').checked) {
                rate_value = document.getElementById('rating10-40').value;
            } else if (document.getElementById('rating10-45').checked) {
                rate_value = document.getElementById('rating10-45').value;

            } else if (document.getElementById('rating10-50').checked) {
                rate_value = document.getElementById('rating10-50').value;
            }

            //alert(rate_value)
            document.getElementById('<%#hdfFeedback.ClientID%>').value = rate_value;

            //$("#hdfFeedback").val() = rate_value;
            //alert(document.getElementById('<%#hdfFeedback.ClientID%>').value)
            //document.getElementById('results').innerHTML = rate_value;
        }

    </script>

    <%--    <script type="text/javascript">
        function startvalue() {
            //var rates = document.getElementById('rating').value;
            var rate_value;
            if (document.getElementById('rating10-0').checked) {
                rate_value = document.getElementById('rating10-0').value;

            } else if (document.getElementById('rating10-05').checked) {
                rate_value = document.getElementById('rating10-05').value;

            } else if (document.getElementById('rating10-10').checked) {
                rate_value = document.getElementById('rating10-10').value;
            } else if (document.getElementById('rating10-15').checked) {
                rate_value = document.getElementById('rating10-15').value;

            } else if (document.getElementById('rating10-20').checked) {
                rate_value = document.getElementById('rating10-20').value;
            } else if (document.getElementById('rating10-25').checked) {
                rate_value = document.getElementById('rating10-25').value;

            } else if (document.getElementById('rating10-30').checked) {
                rate_value = document.getElementById('rating10-30').value;
            } else if (document.getElementById('rating10-35').checked) {
                rate_value = document.getElementById('rating10-35').value;

            } else if (document.getElementById('rating10-40').checked) {
                rate_value = document.getElementById('rating10-40').value;
            } else if (document.getElementById('rating10-45').checked) {
                rate_value = document.getElementById('rating10-45').value;

            } else if (document.getElementById('rating10-50').checked) {
                rate_value = document.getElementById('rating10-50').value;
            }

            //alert(rate_value)
            document.getElementById('<%#hdfFeedback.ClientID%>').value = rate_value;

            //$("#hdfFeedback").val() = rate_value;
            //alert(document.getElementById('<%#hdfFeedback.ClientID%>').value)
            //document.getElementById('results').innerHTML = rate_value;
        }

    </script>--%>

    <script type="text/javascript">
        //data-toggle="modal"  data-target="#veiw"
        function openModal() {
            $('#raise-ticket-modal').modal('show');
        }


        function shwwindow(myurl) {
            window.open(myurl, '_blank');
        }
    </script>
    <script type="text/javascript">
        //data-toggle="modal"  data-target="#veiw"
        function openModal1() {
            $('#feedback').modal('show');
        }
    </script>






    <script type="text/javascript">
        function startvalue1(val) {
            //var rates = document.getElementById('rating').value;
            var rate_value;
            if (val == 0) {
                document.getElementById('rating10-0').checked = true;

            } else if (val == 0.5) {
                document.getElementById('rating10-05').checked = true;

            } else if (val == 1) {
                document.getElementById('rating10-10').checked = true;
            } else if (val == 1.5) {
                document.getElementById('rating10-15').checked = true;

            } else if (val == 2) {
                document.getElementById('rating10-20').checked = true;
            } else if (val == 2.5) {
                document.getElementById('rating10-25').checked = true;

            } else if (val == 3) {
                document.getElementById('rating10-30').checked = true;
            } else if (val == 3.5) {
                document.getElementById('rating10-35').checked = true;

            } else if (val == 4.0) {
                document.getElementById('rating10-40').checked = true;
            } else if (val == 4.5) {
                document.getElementById('rating10-45').checked = true;

            } else if (val == 5) {
                document.getElementById('rating10-50').checked = true;
            }

            //alert(rate_value)
            document.getElementById('<%#hdfFeedback.ClientID%>').value = rate_value;

            //$("#hdfFeedback").val() = rate_value;
            //alert(document.getElementById('<%#hdfFeedback.ClientID%>').value)
            //document.getElementById('results').innerHTML = rate_value;
        }

    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

        .half-stars-example .rating__label {
            cursor: pointer;
            padding: 0 0.1em;
            font-size: 2.5rem;
        }

        .half-stars-example .rating__label--half {
            margin-right: -1.3em;
            z-index: 2;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }

        td .fa-star {
            font-size: 20px;
            color: #ffc107;
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

        .half-stars-example .rating__label--half {
            padding-right: 0;
            margin-right: -1.3em;
            z-index: 2;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Raise Ticket</h3>
                </div>

                <div class="box-body">
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

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnRaiseTicket" runat="server" CssClass="btn btn-outline-info" OnClick="btnRaiseTicket_Click">Raise Ticket</asp:LinkButton>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Ticket Details</h5>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tab4_mytable">
                            <asp:Repeater ID="lvticket" runat="server">
                                <HeaderTemplate>
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Reference No.</th>
                                            <th>Request Type</th>
                                            <th>Request Category</th>
                                            <th>Sub Category</th>
                                            <th>Request Date</th>
                                            <th>Request Status</th>
                                            <th>Response</th>
                                            <th>Feedback</th>
                                            <%--<th>Student ID</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ReferenceNo")%><asp:HiddenField ID="hdfQMRaiseTicketID" runat="server" Value='<%#Eval("QMRaiseTicketID")%>' />
                                        </td>
                                        <td><%# Eval("QMRequestTypeName")%></td>
                                        <td><%# Eval("QMRequestCategoryName")%></td>
                                        <td><%# Eval("QMRequestSubCategoryName")%></td>
                                        <td><%# Eval("CreationDate", "{0:dd/M/yyyy}")%></td>
                                        <%--<td class="text-center"><span class="badge badge-success">Close</span></td>--%>
                                        <td class="text-center">
                                            <asp:Label ID="lablstatus"  Width="90" runat="server" Text='<%# Eval("TicketStatus")%>'></asp:Label></td>
                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkResponse" runat="server" CommandArgument='<%#Eval("QMRaiseTicketID")%>' OnClick="lnkResponse_Click"><i class="fa fa-eye" aria-hidden="true"  data-toggle="modal" data-target="#raise-ticket-modal" ></i></asp:LinkButton>
                                        </td>

                                        <td class="text-center">
                                            <asp:LinkButton ID="lnkFeedBack" runat="server" CommandArgument='<%#Eval("QMRaiseTicketID")%>' OnClick="lnkFeedBack_Click"><i class="fa fa-star" aria-hidden="true"></i></asp:LinkButton>
                                        </td>

                                        <%--<td><%# Eval("studentId")%></td>--%>
                                        <%-- <td class="text-center">
                                            <asp:LinkButton ID="linkButton3" runat="server" CommandArgument='<%#Eval("studentId")%>' OnClick="linkButton3_Click"><i class="fa fa-eye" aria-hidden="true" ></i></asp:LinkButton>
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </FooterTemplate>
                            </asp:Repeater>

                        </table>
                    </div>

                    <!-- Raise Ticket Modal -->
                    <%--\\ 26-04-2022 add  data-backdrop="static"--%>
                    <div class="modal" id="raise-ticket-modal" data-backdrop="static">
                        <div class="modal-dialog">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">Raise Ticket</h4>
                                    <%-- <button type="button" class="close" data-dismiss="modal" runat="server" onclick="btnclear()">&times;</button>--%>

                                    <asp:LinkButton ID="btnclear1" runat="server" OnClick="btnclear1_Click">&times;</asp:LinkButton>

                                </div>

                                <!-- Modal body -->

                                <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12" style="display: none">
                                                        <%--add by 16-12-2022 diable none--%>
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Service Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Request Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlRequestType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRequestType"
                                                            Display="None" ErrorMessage="Please Enter Request Type" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Category</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                                                            Display="None" ErrorMessage="Please Select Category" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Sub-Category</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubCategory"
                                                            Display="None" ErrorMessage="Please Select Sub Category" InitialValue="0"
                                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>General Instruction :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblrequesttype" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="form-group col-lg-12 col-md-12 col-12" id="divstudreq" runat="server" visible="false">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Requirement :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblstudreq" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                        <div class="form-check pl-0">
                                                            <label class="form-check-label">
                                                                <asp:CheckBox ID="chkYess" runat="server" Text="OPT for Emergency Services" Visible="false" AutoPostBack="true" OnCheckedChanged="chkYess_CheckedChanged" />
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label id="lblamount" runat="server" visible="false">Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAmount" runat="server" Visible="false" CssClass="form-control" Enabled="false" />
                                                        <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfTotalAmount" runat="server" Value="0" />
                                                        <asp:Label ID="lblOrderID" runat="server" Visible="false"></asp:Label>
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-12 col-12" id="ticketDec" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Ticket Description</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTicketDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                                        <%--Amol sawarkar--%>
                                                        <asp:RequiredFieldValidator ID="rfvtxtRequiredQty" runat="server" ControlToValidate="txtTicketDescription"
                                                            ErrorMessage="Please Enter Ticket Description" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-8 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Upload Supporting Documents</label>
                                                            <br />
                                                            <sup>*<span style="color: red">File Size should be less than 500 KB</span></sup>

                                                        </div>
                                                        <asp:FileUpload ID="fuRaiseTicket" runat="server" onchange="return CheckFileSize(this)"  />
                                                        <%--<input type="file" id="myFile" name="filename2">--%>
                                                        <asp:Label ID="lblfile" runat="server" />
                                                    </div>


                                                    <%--add new function--%>  <%--style="display: none" not for this page --%>

                                                    <div class="form-group col-lg-5 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <label>Student Remark</label>
                                                        </div>
                                                        <asp:CheckBox ID="chkStudRemark" runat="server" AutoPostBack="true" OnCheckedChanged="chkStudRemark_CheckedChanged" Checked="false" />
                                                    </div>

                                                    <div class="form-group col-lg-7 col-md-7 col-12" style="display: none">
                                                        <asp:Panel ID="pnlPassport" runat="server" Visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Required Information</label>
                                                            </div>
                                                            <asp:TextBox ID="txtReqInfo" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">

                                                <%-- Add amol-- add button link remove 07-04-2022--%>
                                                <%--<asp:LinkButton ID="btnPaySubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnPaySubmit_Click" Visible="false" ValidationGroup="submit">Pay & Submit</asp:LinkButton>--%>
                                                <asp:Button ID="btnPaySubmit" runat="server" CssClass="btn btn-outline-info" Text="Pay & Submit" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Submit';" OnClick="btnPaySubmit_Click" Visible="false" ValidationGroup="submit" UseSubmitBehavior="false" />

                                                <%-- <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" OnClick="btnSubmit_Click" Visible="false" ValidationGroup="submit" UseSubmitBehavior="false" >Submit</asp:LinkButton>--%>
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" Text="Submit" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Submit';" OnClick="btnSubmit_Click" ValidationGroup="submit" UseSubmitBehavior="false" />
                                                <asp:ValidationSummary ID="ValidsummaryItem" runat="server" ValidationGroup="submit"
                                                    ShowMessageBox="true" ShowSummary="false" />
                                                <%-- Amol sawarkar 10-03-2022--%>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <asp:PostBackTrigger ControlID="btnPaySubmit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
                    </div>

                    <!-- View Modal -->
                    <div class="modal" id="veiw">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">View Response Details</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>

                                <!-- Modal body -->
                                <%-- 25-01-2023--%>
                                <div class="modal-body pl-0 pr-0">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-12 mt-1">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Ticket Description :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    <asp:LinkButton ID="lnkdescription" runat="server" CommandArgument='<%#Eval("QMRaiseTicketID")%>' OnClick="lnkdescription_Click"><i class="fa fa-download" aria-hidden="true" style="color: #28a745;"></i></asp:LinkButton>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Response :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    <asp:LinkButton ID="lnkresponse" runat="server" CommandArgument='<%#Eval("QMRaiseTicketID")%>' OnClick="lnkresponse_Click"><i class="fa fa-download" aria-hidden="true" style="color: #28a745;"></i></asp:LinkButton>
                                                                </a>
                                                            </li>

                                                            <li class="list-group-item"><b>Student Remark :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    <asp:LinkButton ID="lnkstudentremark" runat="server" CommandArgument='<%#Eval("QMRaiseTicketID")%>'><i class="fa fa-download"  aria-hidden="true" style="color: #28a745;"></i></asp:LinkButton>
                                                                </a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Service Department  :</b>
                                                                <a class="sub-label">
                                                                    <span id="ctl00_ContentPlaceHolder1_lblrequesttype" style="font-weight: bold;"></span>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Request Type  :</b>
                                                                <a class="sub-label">
                                                                    <%-- <span id="lblrequesttype1" style="font-weight: bold;"></span>--%>
                                                                    <asp:Label ID="lblrequesttype1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Category  :</b>
                                                                <a class="sub-label">
                                                                    <%--<span id="Span2" style="font-weight: bold;"></span>--%>
                                                                    <asp:Label ID="lblcategory1" runat="server" Text="" Font-Bold="true"></asp:Label>

                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Sub-Category  :</b>
                                                                <a class="sub-label">

                                                                    <asp:Label ID="Lblsubcategory1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Ticket Description  :</b>
                                                        <a class="sub-label">

                                                            <asp:Label ID="lblticketDes" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Ticket </label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Upload Document </label>
                                                            <br />
                                                            <sup>*<span style="color: red">File Size should be less than 1 MB</span></sup>

                                                        </div>
                                                        <asp:FileUpload ID="fupayment" runat="server" onchange="return CheckFileSize(this)"  />
                                                        <%--<input type="file" id="myFile" name="filename2">--%>
                                                        <%--                                                        <asp:Label ID="lblfile" runat="server" />--%>
                                                    </div>

                                                </div>
                                                <div class="row mt-3">
                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Ticket </label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer mt-3">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-info" Text="Submit" />

                                                <%-- <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" OnClick="btnSubmit_Click" Visible="false" ValidationGroup="submit" UseSubmitBehavior="false" >Submit</asp:LinkButton>--%>
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-warning" Text="Cancel" />

                                            </div>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>

                                    <%-- <div class="col-12 btn-footer">
                                        <button type="button" class="btn btn-outline-info">Submit</button>
                                    </div>--%>
                                </div>

                            </div>
                        </div>
                    </div>

                    <!-- Feedback Modal Pop up -->
                    <div class="modal" id="feedback">
                        <div class="modal-dialog">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">Feedback</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>

                                <!-- Modal body -->
                                <div class="modal-body">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Star Rating</label>
                                        </div>
                                        <div class="half-stars-example">
                                            <div class="rating-group" id="rating">
                                                <input class="rating__input rating__input--none" checked name="rating1" id="rating10-0" value="0" type="radio">
                                                <label aria-label="0 stars" class="rating__label" for="rating10-0">&nbsp;</label>
                                                <label aria-label="0.5 stars" class="rating__label rating__label--half" for="rating10-05"><i class="rating__icon rating__icon--star fa fa-star-half"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-05" value="0.5" type="radio">
                                                <label aria-label="1 star" class="rating__label" for="rating10-10"><i class="rating__icon rating__icon--star fa fa-star"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-10" value="1" type="radio">
                                                <label aria-label="1.5 stars" class="rating__label rating__label--half" for="rating10-15"><i class="rating__icon rating__icon--star fa fa-star-half"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-15" value="1.5" type="radio">
                                                <label aria-label="2 stars" class="rating__label" for="rating10-20"><i class="rating__icon rating__icon--star fa fa-star"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-20" value="2" type="radio">
                                                <label aria-label="2.5 stars" class="rating__label rating__label--half" for="rating10-25"><i class="rating__icon rating__icon--star fa fa-star-half"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-25" value="2.5" type="radio">
                                                <label aria-label="3 stars" class="rating__label" for="rating10-30"><i class="rating__icon rating__icon--star fa fa-star"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-30" value="3" type="radio">
                                                <label aria-label="3.5 stars" class="rating__label rating__label--half" for="rating10-35"><i class="rating__icon rating__icon--star fa fa-star-half"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-35" value="3.5" type="radio">
                                                <label aria-label="4 stars" class="rating__label" for="rating10-40"><i class="rating__icon rating__icon--star fa fa-star"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-40" value="4" type="radio">
                                                <label aria-label="4.5 stars" class="rating__label rating__label--half" for="rating10-45"><i class="rating__icon rating__icon--star fa fa-star-half"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-45" value="4.5" type="radio">
                                                <label aria-label="5 stars" class="rating__label" for="rating10-50"><i class="rating__icon rating__icon--star fa fa-star"></i></label>
                                                <input class="rating__input" name="rating1" id="rating10-50" value="5" type="radio">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Your Valuable Suggestions</label>
                                        </div>
                                        <asp:TextBox ID="txtSuggestions" runat="server" CssClass="form-control" TextMode="MultiLine" col="3" Rows="10" Width="100%" onkeypress="if (this.value.length > 500) { return false; }" />
                                        <%--<asp:TextBox ID="txtSuggestions" runat="server" CssClass="form-control" TextMode="MultiLine"  MaxLength="100" />--%>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:HiddenField ID="hdfFeedback" runat="server" />
                                        <asp:LinkButton ID="btnSubmit1" runat="server" CssClass="btn btn-outline-info" OnClientClick="startvalue()" OnClick="btnSubmit1_Click">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <!-- Online payment Modal Pop up -->
                    <div id="myModal33" class="modal fade" role="dialog" data-backdrop="static">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4>Online Payment</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-xl-6 col-md-6">
                                                    <div class="md-form">
                                                        <label for="UserName"><sup></sup>Order ID </label>
                                                        <asp:TextBox ID="txtOrderid" TabIndex="1" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xl-6 col-md-6">
                                                    <div class="md-form">
                                                        <label for="UserName"><sup></sup>Application Fees </label>
                                                        <asp:TextBox ID="txtAmountPaid" TabIndex="2" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xl-6 col-md-6">
                                                    <div class="md-form">
                                                        <label for="UserName"><sup></sup>Service Charge</label>
                                                        <asp:TextBox ID="txtServiceCharge" TabIndex="3" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xl-6 col-md-6">
                                                    <div class="md-form">
                                                        <label for="UserName"><sup></sup>Total to be Paid</label>
                                                        <asp:TextBox ID="txtTotalPayAmount" TabIndex="4" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-12 text-center">
                                                    <br />
                                                    <input type="button" value="Pay Now" onclick="Checkout.showLightbox();" class="btn btn-outline-info" />
                                                    <input type="button" value="Pay Now" onclick="Checkout.ShowPaymentPage();" class="btn btn-outline-info d-none" />
                                                </div>
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

