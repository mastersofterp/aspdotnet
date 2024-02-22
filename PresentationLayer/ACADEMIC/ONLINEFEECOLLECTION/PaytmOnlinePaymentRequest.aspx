<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaytmOnlinePaymentRequest.aspx.cs" Inherits="PaytmOnlinePaymentRequest" %>


<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Paytm Payment Request</title>
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

    <form name="f1" id="form1" runat="server" method="post" action="https://securegw-stage.paytm.in/order/process?">

        <div class="container">
            <div class="row mt-5">
                <div class="col-lg-8 offset-lg-2 col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>

                        <div class="box-header with-border">
                            <div class="row">
                                <%--<div class="col-sm-2 text-center">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive" runat="server" ImageUrl="~/images/nophoto.jpg" />
                                </div>

                                <div class="col-sm-8">
                                    <div class="text-center college-name mt-md-3">
                                        <h2>
                                            <asp:Label ID="lblCompAddress" runat="server" Text="Peoples University Bhopal">Peoples University Bhopal</asp:Label>
                                        </h2>
                                        <h2>
                                            <asp:Label ID="lblCollege" runat="server" Text=""></asp:Label></h2>
                                        <h6>
                                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                </div>

                                <div class="col-sm-2 text-center"></div>--%>

                                  <div class="col-sm-12">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive center" runat="server" ImageUrl="~/images/nophoto.jpg" />
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="hidParams" runat="server">
                                            <%--<input type='hidden' name='CURRENCY' value='PAYMENT_CURRENCY'>
                                            <input type='hidden' name='GATEWAYNAME' value='GATEWAY_USED_BY_PAYTM'>
                                            <input type='hidden' name='RESPMSG' value='PAYTM_RESPONSE_MESSAGE_DESCRIPTION'>
                                            <input type='hidden' name='BANKNAME' value='BANK_NAME_OF_ISSUING_PAYMENT_MODE'>
                                            <input type='hidden' name='PAYMENTMODE' value='PAYMENT_MODE_USED_BY_CUSTOMER'>
                                            <input type='hidden' id="hidMID" name='MID' value='YOUR_MID_HERE'>
                                            <input type='hidden' name='RESPCODE' value='PAYTM_RESPONSE_CODE'>
                                            <input type='hidden' name='TXNID' value='PAYTM_TRANSACTION_ID'>
                                            <input type='hidden' name='TXNAMOUNT' value='ORDER_TRANSACTION_AMOUNT'>
                                            <input type='hidden' name='ORDERID' value='YOUR_ORDER_ID'>
                                            <input type='hidden' name='STATUS' value='PAYTM_TRANSACTION_STATUS'>
                                            <input type='hidden' name='BANKTXNID' value='BANK_TRANSACTION_ID'>
                                            <input type='hidden' name='TXNDATE' value='TRANSACTION_DATE_TIME'>
                                             <input type='hidden' name='CUST_ID' value='CUST_ID_VALUE'>
                                            <input type='hidden' name='EMAIL' value='EMAIL_VALUE'> 
                                            <input type='hidden' name='MOBILE_NO' value='MOBILE_NO_VALUE'>
                                            <input type='hidden' id="hidCHECKSUMHASH" name='CHECKSUMHASH' value='PAYTM_GENERATED_CHECKSUM_VALUE'>--%>
                                            <%-- <input type='hidden' name='CHECKSUMHASH'  id="checksumData" runat="server" value="<%=checksum%>">--%>
                                        </div>
                                        <%--   <div id="divPatymData" runat="server">
                                        </div>--%>
                                    </div>
                                </div>

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
                                        <%--  <asp:Button ID="btnPay" runat="server" Text="Pay" CausesValidation="false" OnClick="btnPay_Click" CssClass="btn btn-primary"/>--%>
                                        <button type="submit" id="btnPay" class="btn btn-primary">Pay</button>
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

    <%--<script type="text/javascript">
        function Payment() {
            alert("check");
            document.forms[0].submit();
        }
    </script>--%>
</body>
</html>
