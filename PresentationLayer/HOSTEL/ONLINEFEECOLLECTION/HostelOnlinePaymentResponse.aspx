<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HostelOnlinePaymentResponse.aspx.cs" Inherits="HOSTEL_ONLINEFEECOLLECTION_HostelOnlinePaymentResponse" %>

<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>PayU Payment Response</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- CSS -->

    <link href="../../plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />

    <!-- JS -->
    <script src="../../plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="../../plugins/newbootstrap/js/popper.min.js"></script>
    <script src="../../plugins/newbootstrap/js/bootstrap.min.js"></script>



    <style>
        .box-header .img-logo {
            height: 120px;
            width: auto;
        }

        .college-name h2 {
            font-weight: bold;
        }

        @media (max-width: 576px) {
            .college-name h2 {
                font-size: 20px;
                font-weight: bold;
            }

            .college-name h6 {
                font-size: 12px;
            }
        }
    </style>

</head>
<%--<body onload="goBackOrClose();">--%>
<body>

    <form id="form1" runat="server" method="post">
        <input type="hidden" runat="server" id="key" name="key" />
        <input type="hidden" runat="server" id="hash" name="hash" />
        <input type="hidden" runat="server" id="txnid" name="txnid" />
         <asp:TextBox ID="surl" runat="server" Style="display: none" />
        <asp:TextBox ID="furl" runat="server" Style="display: none" />
        <asp:TextBox ID="productinfo" runat="server" Style="display: none" />
         <asp:TextBox ID="firstname" runat="server" Style="display: none" />
         <asp:TextBox ID="email" runat="server" Style="display: none" />
        <asp:TextBox ID="phone" runat="server" Style="display: none" />
         
        <asp:TextBox ID="service_provider" runat="server" Text="payu_paisa" Style="display: none" />
        <div class="container">
            <div class="row">
                <div class="col-lg-8 offset-lg-2 col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-sm-12 text-center">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive" runat="server" ImageUrl="~/images/nophoto.jpg" />
                                </div>
                                <%--<div class="col-sm-9">
                                    <div class="text-center college-name mt-md-3">
                                        <h2>
                                            <asp:Label ID="lblCollege" runat="server" Text=""></asp:Label></h2>
                                        <h6>
                                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                </div>--%>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 btn-footer">
                                        <div id="divSuccess" runat="server" visible="false">
                                            <h4 class="text-center"><i class="fa fa-check text-green"></i>Payment Success Details</h4>
                                        </div>
                                        <div id="divFailure" runat="server" visible="false">
                                            <h4 class="text-center"><i class="fa fa-exclamation-triangle"></i>Payment Failure Details</h4>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <h4>
                                            <asp:Label ID="lblActivityName" runat="server"></asp:Label>
                                        </h4>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Registration No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblstudentname" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblamount" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                              <li class="list-group-item"><b>Order Id :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblOrderId" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                               <li class="list-group-item"><b>Transaction Id :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTrasactionId" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                              <li class="list-group-item"><b>Transaction Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTransactionDate" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Room Status :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblHostelStatus" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                              <li class="list-group-item d-none"><b>Response code :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblResponsecode" runat="server" Font-Bold="true"></asp:Label></a>

                                                   <%--<asp:Label ID="lblHostelStatus" runat="server" Font-Bold="true"></asp:Label></a>--%>
                                            </li>
                                            
                                        </ul>
                                    </div>

                                    <div class="col-12 btn-footer mt-3">
                                        <asp:Button ID="btnReciept" runat="server" Visible="false" Text="Reciept" TabIndex="5" OnClick="btnReciept_Click" CssClass="btn btn-info" />

                                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" CssClass="btn btn-warning" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <strong>Designed and Developed By:<a href="#"> Mastersoft Group Nagpur</a> Copyright &copy; 2016.</strong> All rights
                                        reserved.
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div id="divMsg" runat="server">
    </div>
</body>
</html>
