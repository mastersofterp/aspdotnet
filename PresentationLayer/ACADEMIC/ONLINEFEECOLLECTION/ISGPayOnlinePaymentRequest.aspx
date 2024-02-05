<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ISGPayOnlinePaymentRequest.aspx.cs" Inherits="ISGPayOnlinePaymentRequest" %>


<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>ISGPay Payment Request</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="../../plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />

    <script src="../../plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="../../plugins/newbootstrap/js/popper.min.js"></script>
    <script src="../../plugins/newbootstrap/js/bootstrap.min.js"></script>

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

<body>

    <form name="RedirectForm" id="form1" runat="server" action="https://sandbox.isgpay.com/ISGPay/request.action" method="post">

       <%-- <asp:TextBox ID="service_provider" runat="server" Text="payu_paisa" Style="display: none" />--%>

        <div class="container">
            <div class="row mt-5">
                <div class="col-lg-8 offset-lg-2 col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>

                        <div class="box-header with-border">
                              <div class="row">
                                <%--  <div class="col-sm-3 text-center mt-md-4">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive" runat="server" Width="200px" ImageUrl="~/images/nophoto.jpg" />
                                </div>--%>
                                <%--<div class="col-sm-9">
                                    <div class="text-center college-name mt-md-3">
                                        <h2>
                                            <asp:Label ID="lblCollege" runat="server" Text=""></asp:Label></h2>
                                        <h6>
                                            <asp:Label ID="lblAddress" Font-Size="14px" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                </div>--%>

                                <div class="col-sm-12">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive center" runat="server" ImageUrl="~/images/nophoto.jpg" />
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%-- ISGPay--%>

                                    <div class="col-md-12">
                                        <div>
                                            <input type="hidden" name="MerchantId" id="MerchantId" runat="server" value="<%=isgPayReqParams.MerchantId%>">
                                            <input type="hidden" name="TerminalId" id="TerminalId" runat="server" value="<%=isgPayReqParams.TerminalId%>">
                                            <input type="hidden" name="Version" id="Version" runat="server" value="<%=isgPayReqParams.Version%>">
                                            <input type="hidden" name="BankId" id="BankId" runat="server" value="<%=isgPayReqParams.%>">
                                            <input id="hidden1" type="hidden" name="payOpt" value="dc" runat="server">
                                            <input type="hidden" name="EncData" id="EncData" runat="server" value="<%=isgPayReqParams.EncData%>">
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel_Debug" runat="server">
                                            <div>
                                                <asp:Label ID="Label_Debug" runat="server" />
                                                <br />
                                                <asp:Label ID="Label_DigitalOrder" runat="server" />
                                            </div>

                                        </asp:Panel>
                                    </div>

                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel_StackTrace" runat="server">
                                            <h5><strong>&nbsp;Exception Stack Trace</strong></h5>
                                            <div>
                                                <asp:Label ID="Label_StackTrace" runat="server" />
                                            </div>
                                            <div>
                                                <asp:Label ID="Label_Message" runat="server" />
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <%--End ISGPay--%>

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
                                            <li class="list-group-item"><b>PRN Number :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblstudentname" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Year :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblYear" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>

                                            <li class="list-group-item"><b>Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblamount" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-12 btn-footer mt-3">
                                        <asp:Button ID="btnPay" runat="server" Text="Pay" CausesValidation="false" CssClass="btn btn-primary" />
                                        <%--  <asp:Button ID="Button1" runat="server" Text="Pay" OnClick="btnPay_Click" CausesValidation="false" CssClass="btn btn-primary" />--%>
                                        <%-- <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" CssClass="btn btn-warning" />--%>
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

    <div id="divMsg" runat="server"></div>

</body>
</html>
