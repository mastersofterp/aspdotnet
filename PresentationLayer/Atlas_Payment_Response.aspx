<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Atlas_Payment_Response.aspx.cs" Inherits="Atlas_Payment_Response" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Cache-Control" content="post-check=0, pre-check=0', false" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="Sat, 26 Jul 1997 05:00:00 GMT" />
    <meta http-equiv="Last-Modified" content='" + now1( D, d M Y H:i:s ) + "GMT' />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>ATLAS</title>
    <link rel="shortcut icon" href="imgnew1/favicon.ico" type="image/x-icon">

    <script type="text/javascript" src="resources/js/jquery.js"></script>

    <%--<link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="<%#Page.ResolveClientUrl("~/bootstrap/font-awesome-4.6.3/css/font-awesome.min.css")%>" rel="stylesheet" />

    <!-- DataTables -->
    <link href="<%#Page.ResolveClientUrl("~/plugins/datatables/dataTables.bootstrap.css")%>" rel="stylesheet" />--%>

    <!-- Theme style -->
    <%--<link href="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/css/newcustom.css")%>" rel="stylesheet" />--%>

    <link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />
    <style type="text/css">
        .navbar-brand {
            padding-top: 4px;
        }

            .navbar-brand > img {
                width: 32px;
                height: 32px;
            }
    </style>
    <script src="plugins/studInfo/js/jquery-3.3.1.min.js"></script>
    <script src="plugins/newbootstrap/js/bootstrap.min.js"></script>
    <!-- jQuery 3.3.1 -->
    <%-- <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.3.1.min.js")%>"></script>
    <!-- DataTables -->
    <script defer src="<%#Page.ResolveClientUrl("~/plugins/datatables/jquery.dataTables.min.js")%>"></script>
    <script defer src="<%#Page.ResolveClientUrl("~/plugins/datatables/dataTables.bootstrap.min.js")%>"></script>
    <!-- Bootstrap 3.4.1 -->
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>--%>
</head>
<body>
    <style>
        .brand > img {
            width: 60px;
            height: 60px;
        }
    </style>

    <div class="col-md-6 col-lg-4 col-12 offset-md-3 offset-lg-4 mt-3">
        <div class="box box-primary">
            <div class="text-center">
                <a class="brand">
                    <img src="../IMAGES/SLIIT_logo.png" />
                </a>
            </div>

            <form id="form1" runat="server">

                <div>
                    <h1 style="text-align: center">PAYMENT STATUS</h1>

                    <ul class="list-group list-group-unbordered mt-3">
                        <li class="list-group-item"><b>Amount :</b>
                            <a class="sub-label">
                                <asp:Label runat="server" ID="lblAmount" Font-Bold="true"></asp:Label>
                            </a>
                        </li>
                        <li class="list-group-item"><b>Transaction ID :</b>
                            <a class="sub-label">
                                <asp:Label runat="server" ID="lblTransactionID" Font-Bold="true"></asp:Label></a>
                        </li>
                        <li class="list-group-item"><b>Payment Mode :</b>
                            <a class="sub-label">
                                <asp:Label runat="server" ID="lblPaymentMode" Font-Bold="true"></asp:Label></a>
                        </li>
                        <li class="list-group-item"><b>Status :</b>
                            <a class="sub-label">
                                <asp:Label runat="server" ID="lblSataus" Font-Bold="true"></asp:Label></a>
                        </li>
                        <li class="list-group-item"><b>Reference Id :</b>
                            <a class="sub-label">
                                <asp:Label runat="server" ID="lblReferenceId" Font-Bold="true"></asp:Label></a>                           
                        </li>
                        <li class="list-group-item"><b>Message :</b>
                            <a class="sub-label">
                                <asp:Label runat="server" ID="lblMessage" Font-Bold="true"></asp:Label></a>
                        </li>
                    </ul>
                </div>
                <div>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </div>

                <div id="divMsg" runat="server">
                </div>


                  <div class="col-12 btn-footer mt-3">
                                        <asp:Button ID="btnReciept" runat="server" Visible="false" Text="Reciept" TabIndex="1"  CssClass="btn btn-info" OnClick="btnReciept_Click"/> <%--OnClick="btnReciept_Click"--%>
                                       <%-- <asp:Button ID="btnPay" runat="server" Text="Pay" OnClick="btnPay_Click" CausesValidation="false" CssClass="btn btn-primary" />--%>
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" CssClass="btn btn-warning" /> <%--OnClick="btnBack_Click"--%>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <strong>Designed and Developed By:<a href="#"> Mastersoft Group Nagpur</a> Copyright &copy; 2022.</strong> All rights
                                        reserved.
                                    </div>


            </form>
        </div>
    </div>
</body>
</html>
