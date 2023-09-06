<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ccavExamResponse.aspx.cs" Inherits="ccavExamResponse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Success Page</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- CSS -->
    <link href="../../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../bootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../dist/css/AdminLTE.css" rel="stylesheet" />
    <script src="../../bootstrap/js/bootstrap.min.js"></script>

    <script src='steps.js' type='text/javascript'></script>
    <style>
        body {
            background-color: #F2F2F2;
        }
    </style>
</head>
<%--<body onload="goBackOrClose();">--%>
<body>
    <form id="form1" runat="server" method="post">
        <div class="container">
            <div class="row" style="margin-top: 10px;">
                <div class="col-md-12">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <div class="box box-warning">
                            <div class="box-header">
                                <div class="col-md-2 col-xs-6">
                                    <img src="../../imgnew1/MAKAUAT_LOGO.png" class="img-responsive"/>
                                </div>

                                <div class="col-md-9 col-xs-12" style="text-align: center;">
                                    <h2 style="margin-top: 3px;">Maulana Abul Kalam Azad University of Technology</h2>
                                    <p class="text-muted"><small>SIMHAT, HARINGHATA, NADIA, WEST  BENGAL, INDIA - 741249</small></p>
                                </div>

                                <%-- <div class="col-md-12">
                                    <h4 class="text-center"><span class="label label-info">Payment Success Details</span></h4>
                                </div>--%>
                                <div id="divSuccess" runat="server" visible="false">
                                    <h4 class="text-center"><i class="fa fa-check text-green"></i>Payment Success Details</h4>
                                </div>
                                <div id="divFailure" runat="server" visible="false">
                                    <h4 class="text-center"><i class="fa fa-exclamation-triangle"></i>Payment Failure Details</h4>
                                </div>
                            </div>
                            <hr style="margin: 1px;" />
                            <div class="box-body">
                                <div class="well col-xs-12 col-sm-12 col-md-12 ">
                                    <div class="">
                                        <h4 style="text-align:center">Application Fee Payment</h4>
                                        <div class="table table-responsive">
                                            <table class="table table-border">
                                                <tr>
                                                    <th>Order ID</th>
                                                    <td>
                                                        <asp:Label ID="ldlresponceHandling" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>

                                                    <th>Transaction ID</th>
                                                    <td>
                                                        <asp:Label ID="lbltransactionId" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdfOrderId" runat="server" />
                                                    </td>

                                                </tr>
                                                <tr>

                                                    <th>Application Id</th>
                                                    <td>
                                                        <asp:Label ID="lblidno" runat="server"></asp:Label>
                                                    </td>

                                                </tr>

                                                <tr>

                                                    <th>Student Name </th>
                                                    <td>
                                                        <asp:Label ID="lblstudentname" runat="server"></asp:Label>
                                                    </td>

                                                </tr>
                                               <%-- <tr>

                                                    <th>DCR NO </th>
                                                    <td>
                                                        <asp:Label ID="txtDcr" runat="server"></asp:Label>
                                                    </td>

                                                </tr>--%>
                                                <tr>

                                                    <th>Amount </th>
                                                    <td>
                                                        <asp:Label ID="lblamount" runat="server"></asp:Label><br />
                                                    </td>

                                                </tr>

                                                <%--salystring: <asp:Label ID="productinfo" runat="server"></asp:Label><br />--%>
                                            </table>
                                            <div class="input-group">

                                                <div class="input-group-btn">
                                                   
                                                    <%--<asp:Button ID="button" runat="server" Text="Print Receipt" OnClick="btnprint_Click" CausesValidation="false" class="btn btn-success btn-flat" />--%>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Visible="true" CssClass="btn btn-success btn-flat" CausesValidation="false" OnClick="LinkButton1_Click1"><i class="fa fa-print" ></i> PRINT RECEIPT </asp:LinkButton>
                                                        
                                               <%--   <button type="button" name="submit" onclick="" class="btn btn-success btn-flat"><i class="fa fa-print"></i>>
                                                    Print Receipt 
                            </button>
                                                </div>--%>
                                                
                                               </div>
                                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                                    <div class="input-group-btn">
                                                        <asp:LinkButton ID="LinkButton2" runat="server"
                                                            CssClass="btn btn-success btn-flat" CausesValidation="false"
                                                            href="https://makauttest.mastersofterp.in"><i class="fa fa-home" ></i> HOME PAGE</a></asp:LinkButton>
                                                    </div>
                                            
                                                </div>
                                            <!-- /.input-group -->

                                        </div>
                                        <!-- /.error-content -->
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="box-footer">
                            &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; <strong>Designed and Developed By:<a href="#"> Mastersoft Group Nagpur</a> Copyright &copy; 2016.</strong> All rights
      reserved.
                        </div>
                    </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
        </div>
        
    </form>
    <div id="divMsg" runat="server">
            </div>
</body>
</html>
