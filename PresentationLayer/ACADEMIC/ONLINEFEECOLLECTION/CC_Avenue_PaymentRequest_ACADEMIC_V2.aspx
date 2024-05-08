<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CC_Avenue_PaymentRequest_ACADEMIC_V2.aspx.cs" Inherits="CC_Avenue_PaymentRequest" %>


<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>CC_Avenue</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="../../plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="../../plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />


    <%--<script src="<%#Page.
        ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%> "></script>--%>
    <script src="../../plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="../../plugins/newbootstrap/js/popper.min.js"></script>
    <script src="../../plugins/newbootstrap/js/bootstrap.min.js"></script>

    <%--  <link href="../../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../bootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../dist/css/AdminLTE.css" rel="stylesheet" />
    <script src="../../bootstrap/js/bootstrap.min.js"></script>--%>

    <%--<script src='steps.js' type='text/javascript'></script>--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        function showConfirm() {
            debugger
            var message = '<%=Session["ReturnpageUrl"].ToString() %>';
            //alert(message)
            //form1.action ="https://localhost:61905/PresentationLayer/ACADEMIC/Backlog_ExamRegistration_CC.aspx?pageno=2949";
            form1.action = message;         
            return true;
        }

    </script>  
    <style>
        .box-header .img-logo {
            height: 100px;
            width: 120px;
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

 <%--<form id="nonseamless" runat="server" method="post" name="redirect" action="https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction">--%>
      <form id="form1" runat="server"  >
     <input type="hidden" runat="server" id="key" name="key" />
        <input type="hidden" runat="server" id="hash" name="hash" />
        <input type="hidden" runat="server" id="txnid" name="txnid" />
         <asp:TextBox ID="surl" runat="server" Style="display: none" />
        <asp:TextBox ID="furl" runat="server" Style="display: none" />
        <asp:TextBox ID="productinfo" runat="server" Style="display: none" />
         <asp:TextBox ID="firstname" runat="server" Style="display: none" />
         <asp:TextBox ID="email" runat="server" Style="display: none" />
        <asp:TextBox ID="phone" runat="server" Style="display: none" />
        <asp:TextBox ID="udf1" runat="server" Style="display: none" />
        <asp:TextBox ID="udf2" runat="server" Style="display: none" />
        <asp:TextBox ID="udf3" runat="server" Style="display: none" />
        <asp:TextBox ID="udf4" runat="server" Style="display: none" />
   
         
        <asp:TextBox ID="service_provider" runat="server" Text="CC_Avenue" Style="display: none" />
        <div class="container">
            <div class="row">
                <div class="col-lg-8 offset-lg-2 col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-sm-6 offset-sm-3 text-center">
                                    <asp:Image ID="imgCollegeLogo" class="img-logo img-responsive" runat="server" ImageUrl="~/images/nophoto.jpg" />
                                </div>
                               <div class="col-sm-9 d-none">
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
                                        <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>"/>
                                      <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>"/>
                                       
                                        <asp:Button ID="btnPay" runat="server" Text="Pay" OnClick="btnPay_Click" CssClass="btn btn-primary"/>
                                        <asp:Button ID="btnBack" runat="server" Text="Back"  CausesValidation="false" OnClick="btnBack_Click1" OnClientClick="return showConfirm();" CssClass="btn btn-warning" /> <%-- CausesValidation="false"--%>
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
