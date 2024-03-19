<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaytmOnlinePaymentRequest.aspx.cs" Inherits="PaytmOnlinePaymentRequest" %>

<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <%--<meta name="viewport" content="width=device-width, initial-scale=1">--%>
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0"/>
    <title>Paytm Payment Request</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <link href="../../plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />

    <script type="application/javascript" src="https://business.paytm.com/demo//static/js/jquery.min.js"></script>
   <%-- <script src="../../plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="../../plugins/newbootstrap/js/popper.min.js"></script>
    <script src="../../plugins/newbootstrap/js/bootstrap.min.js"></script>--%>

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

    <form name="f1" id="form1" runat="server">
          </form>
        <div class="container">
            <div class="row mt-5">
                <div class="col-lg-8 offset-lg-2 col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>

                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive center" runat="server" ImageUrl="~/images/nophoto.jpg" />
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
                                        <button type="submit" id="btnPay" class="btn btn-primary" onclick="onPayScriptLoad()"; >Pay Now</button>
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


    <div id="divMsg" runat="server"></div>
    <script type="text/javascript">
        var merchentID = '<%=Session["MerchentID"]%>';
        //alert(merchentID);

        var jsElem = window.document.createElement('script');
        jsElem.src = 'https://securegw.paytm.in/merchantpgpui/checkoutjs/merchants/' + merchentID;
        jsElem.type = 'application/javascript';
        $('head').append(jsElem);
    </script>

    <%--<script type="application/javascript" src="https://securegw.paytm.in/merchantpgpui/checkoutjs/merchants/PEOPLE92734521187172.js" ></script>--%>

    <script type="text/javascript">

        //document.getElementById("paytmWithPaytm").addEventListener("click", function () {
        //    onScriptLoad("de5f178fdbe04169aa68e3edd51e3d0c1709555781310", "1709555781296", "1.00");
        //}); 

        var TOKEN = '<%=Session["TOKEN"]%>';
        var ORDERID = '<%=Session["ORDERID"]%>';
        var AMOUNT = '<%=Session["AMOUNT"]%>';
        //alert(TOKEN);

        function onPayScriptLoad() {
            var config = {
                "root": "",
                "flow": "DEFAULT",
                "merchant": {
                    "name": "Peoples University",
                    "logo": "https://business.paytm.com/demo//static/images/merchant-logo.png?v=1.4"
                },
                "style": {
                    "headerBackgroundColor": "#8dd8ff",
                    "headerColor": "#3f3f40"
                },
                "data": {
                    "orderId": ORDERID,
                    "token": TOKEN,
                    "tokenType": "TXN_TOKEN",
                    "amount": AMOUNT
                },
                "handler": {
                    "notifyMerchant": function (eventName, data) {
                        if (eventName == 'SESSION_EXPIRED') {
                            alert("Your session has expired!!");
                            location.reload();
                        }
                    }
                }
            };

            if (window.Paytm && window.Paytm.CheckoutJS) {
                // initialze configuration using init method
                window.Paytm.CheckoutJS.init(config).then(function onSuccess() {
                    console.log('Before JS Checkout invoke');
                    // after successfully update configuration invoke checkoutjs
                    window.Paytm.CheckoutJS.invoke();
                }).catch(function onError(error) {
                    console.log("Error => ", error);
                });
            }
        }
    </script>


</body>
</html>
