﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashFreeOnlinePaymentResponse.aspx.cs" Inherits="CashFreeOnlinePaymentResponse" %>

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
    <%--<link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css")%>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/newcustom.css")%>" rel="stylesheet" />--%>
    <%--<link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../../plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />


    <%--<script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>--%>
    <script src="../../plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="../../plugins/newbootstrap/js/popper.min.js"></script>
    <script src="../../plugins/newbootstrap/js/bootstrap.min.js"></script>

    <%--  <link href="../../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../bootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../dist/css/AdminLTE.css" rel="stylesheet" />
    <script src="../../bootstrap/js/bootstrap.min.js"></script>--%>

    <%--<script src='steps.js' type='text/javascript'></script>--%>

    <style>
        .box-header .img-logo {
            height: 100px;
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

    <form id="form1" runat="server">
       
    
        <div class="container">
            <div class="row">
                <div class="col-lg-8 offset-lg-2 col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-sm-3 text-center">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive" runat="server" ImageUrl="~/images/nophoto.jpg" />
                                </div>
                                <div class="col-sm-9">
                                    <div class="text-center college-name mt-md-3">
                                        <h2>
                                            <asp:Label ID="lblCollege" runat="server" Text=""></asp:Label></h2>
                                        <h6>
                                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                </div>
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
                                              <li class="list-group-item d-none"><b>Response code :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblResponsecode" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            
                                        </ul>
                                         
                                    </div>

                                    <div class="col-12 btn-footer mt-3">
                                        <asp:Button ID="btnReciept" runat="server" Visible="false" Text="Reciept" TabIndex="5" OnClick="btnReciept_Click" CssClass="btn btn-info" />
                                       <%-- <asp:Button ID="btnPay" runat="server" Text="Pay" OnClick="btnPay_Click" CausesValidation="false" CssClass="btn btn-primary" />--%>
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
