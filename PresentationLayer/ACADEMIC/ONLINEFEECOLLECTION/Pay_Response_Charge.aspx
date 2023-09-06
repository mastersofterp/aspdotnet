<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pay_Response_Charge.aspx.cs" Inherits="Pay_Response_Charge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <title>Online Payment</title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="bootstrap/fonts/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript">
        document.onmousedown = disableclick;
        function disableclick(event) {
            if (event.button == 2) {
                return false;
            }
            else if (event.keyCode == 10) {
                return false;
            }
            else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
                return false;
            }
        }
    </script>


    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>

    <script type="text/javascript" language="javascript">
        function detectPopupBlocker() {
            var myTest = window.open("about:blank", "", "directories=no,height=100,width=100,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,top=0,location=no");
            if (!myTest) {
                alert("A popup blocker was detected. Please Remove popup blocker");
            } else {
                myTest.close();
            }
        }

    </script>
    <style>
        .main-padding {
            padding: 30px;
        }

        .inner-padd {
            padding: 10px;
        }

        .main-margin {
            margin-top: 200px;
        }
    </style>

</head>
<body oncontextmenu="return false">
    <form id="form1" runat="server">
        <asp:Label ID="lblResponseDecrypted" runat="server" Visible="false"></asp:Label>
        <!--Header-->
        <header>
            <div class="container">

                <div class="col-sm-8 col-sm-offset-2 ">
                    <div class="main-margin">
                        <div class="main-padding bg-success" style="border: 3px solid green">
                            <div class="row">
                                <div class="col-sm-4 bg-success">
                                    <a href="../Default.aspx" class="logo">
                                        <img src="IMAGES/LNMIIT-logo.png" class="img-responsive" />
                                        <%--<img src="/Images/logo.png" alt="FEEPAYR" />--%>
                                    </a>
                                </div>
                                <div class="col-sm-8">
                                    <%-- <a href="../Default.aspx" class="pull-right home" style="margin: 23px;"><i class="fa fa-home fa-2x"></i></a>--%>


                                    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                                    </ajaxToolkit:ToolkitScriptManager>
                                    <asp:UpdatePanel ID="updResponse" runat="server">
                                        <ContentTemplate>

                                            <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                                                <div class="inner-padd">
                                                    <h1 class="text-success"><i class="fa fa-check-circle-o text-success" aria-hidden="true"></i>
                                                        <asp:Label ID="lblPaySeccess" runat="server" Text="Payment Successful" Visible="false"></asp:Label>
                                                    </h1>
                                                </div>

                                                <div class="payment-status">
                                                    <div class="payment-status-content">
                                                        <asp:Label ID="lblmessage" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblerror" runat="server" Text="" Visible="false"></asp:Label>
                                                        <p class="text-center">
                                                            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="Intermediate.aspx" CssClass="btn btn-primary">Go To Dashboard</asp:LinkButton>
                                                            <%--  <asp:LinkButton ID="lbReport" runat="server" CssClass="btn btn-primary"
                                                        CausesValidation="false" OnClick="lbReport_Click" Visible="false">Payment Slip</asp:LinkButton>--%>
                                                            <asp:Button ID="btnReport" runat="server" Text="Payment Slip" CssClass="btn btn-primary" OnClick="btnReport_Click" Visible="false"/>
                                                        </p>
                                                        <%-- <p class="text-center">
                                                <asp:LinkButton ID="lbSignOut" runat="server" PostBackUrl="~/default.aspx" CssClass="btn btn-primary"
                                                    OnClientClick="return confirm('Do you want to Sign out?');" CausesValidation="false" OnClick="lbSignOut_Click">Sign Out</asp:LinkButton>
                                            </p>--%>
                                                    </div>
                                                </div>
                                            </asp:Panel>


                                            <asp:Panel ID="pnlfailed" runat="server" Visible="false">

                                                <div class="inner-padd">
                                                    <h1 class="text-danger"><i class="fa fa-remove text-danger" aria-hidden="true"></i>
                                                        <asp:Label ID="lblPayFailed" runat="server" Text="Payment Failed" Visible="false"></asp:Label>
                                                    </h1>
                                                </div>


                                                <div class="payment-status">
                                                    <div class="payment-status-content">
                                                        <asp:Label ID="lblmessageError" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblerrorE" runat="server" Text="" Visible="false"></asp:Label>
                                                        <p class="text-center">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="Intermediate.aspx" CssClass="btn btn-primary">Go To Dashboard</asp:LinkButton>
                                                            <%--  <asp:LinkButton ID="lbReport" runat="server" CssClass="btn btn-primary"
                                                        CausesValidation="false" OnClick="lbReport_Click" Visible="false">Payment Slip</asp:LinkButton>--%>
                                                        </p>
                                                        <%-- <p class="text-center">
                                                <asp:LinkButton ID="lbSignOut" runat="server" PostBackUrl="~/default.aspx" CssClass="btn btn-primary"
                                                    OnClientClick="return confirm('Do you want to Sign out?');" CausesValidation="false" OnClick="lbSignOut_Click">Sign Out</asp:LinkButton>
                                            </p>--%>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnReport" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <!--middel content-->
        <div class="middle-container">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                    </div>
                </div>
            </div>
        </div>

        <!--Footer-->
        <%--<div class="footer-section">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-xs-6">
                        <div class="social">
                            <a href="http://www.facebook.com/feepayr/" class="fb tool-tip" title="Facebook"><i class="fa fa-facebook"></i></a>
                            <a href="https://www.linkedin.com/company/master%27s-software" class="linkedin" title="Linkedin"><i class="fa fa-linkedin"></i></a>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <ul class="menu-icons-section clearfix text-right">
                            <li><a href="/blogs/" title="Blog">Blog</a></li>
                            <li><a href="/Contact.aspx" title="Contact">Contact</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>--%>
        <div id="divMsg" runat="server"></div>
    </form>
</body>
</html>
