<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default_MITU.aspx.cs" Inherits="default_MITU" %>


<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UC/feed_back.ascx" TagName="feedback" TagPrefix="uc1" %>

<!DOCTYPE html>
<html lang="en">
<!-- Global site tag (gtag.js) - Google Analytics -->
<%--<script async src="https://www.googletagmanager.com/gtag/js?id=UA-131521698-1"></script>--%>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    gtag('config', 'UA-131521698-1');
</script>
<script>

    function validate() {

        document.getElementById('txtName').value = '';
        document.getElementById('txt_emailid').value = '';
        document.getElementById('txt_captcha').value = '';
        return false;
    }
</script>


<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>MIT World Peace University Pune</title>
    <link rel="shortcut icon" href="Images/Login/mit_fevicon.png" type="image/x-icon">

    <!-- Bootstrap -->
    <link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="plugins/newbootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/css/login_mit.css" rel="stylesheet" />
    <link href="plugins/virtual-keyboard/keyboard.css" rel="stylesheet" />

    <%-- Added Google Sign In on Date 21/09/2020 by Deepali--%>
    <%--    <meta name="google-signin-client_id" content="YOUR_CLIENT_ID.apps.googleusercontent.com">--%>
    <meta name="google-signin-client_id" content="756005764126-6b0tt497vp345vfn0nso5reuonq5o11l.apps.googleusercontent.com">
    <%-- End Google Sign In on Date 21/09/2020 by Deepali--%>
    <style>
        .logo img {
            width: auto;
            height: 80px;
        }

        #btnLogin {
            font-size: 16px;
            padding: 0.55rem 0.75rem;
        }

        @media (min-width:576px) and (max-width:991px) {
            .logo img {
                width: auto;
                height: 125px;
            }
        }

        @media (max-width:576px) {
            .logo img {
                width: 40%;
                height: auto;
                margin-top: 5px;
            }
        }
    </style>



</head>
<body>
    <form id="frmDefault" runat="server" defaultbutton="btnLogin">
        <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" EnablePageMethods="true" runat="server" ID="Script_water" />
        <%--<asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="90000">
                        </asp:Timer>--%>
        <div class="login">


            <section class="login__right animationss a5">


                <div class="college-details text-center mt-5 mt-md-0 pt-4 mt-lg-5 pt-xl-3 animations a1">
                    <img src="Images/Login/mit_logo.png" alt="logo" class="w-100" />
                </div>

                <div class="signin-details mt-5 mt-md-3 mt-lg-4">
                    <h4 class="heading animations a4 mb-3">Sign In</h4>
                    <!-- User Name Start -->
                    <div class="floating-form animationss a1">
                        <div class="floating-label">
                            <asp:TextBox ID="txt_username" runat="server" TabIndex="1" onCopy="return false" onPaste="return false" onCut="return false" CssClass="floating-input" type="text" placeholder=" " AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req_username" runat="server" ControlToValidate="txt_username" ErrorMessage="Username Required !" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                            <span class="highlight"></span>
                            <label>User Name</label>
                            <i class="far fa-user"></i>
                        </div>
                    </div>
                    <!-- User Name END -->
                    <!-- Password Start -->
                    <div class="floating-form animationss a2">
                        <div id="show_hide_password">
                            <div class="floating-label">

                                <asp:TextBox ID="txt_password" runat="server" TabIndex="2" TextMode="Password" onCopy="return false" onPaste="return false" onCut="return false" CssClass="floating-input" type="text" placeholder=" "></asp:TextBox>
                                <a href="#"><i class="fa fa-eye-slash" aria-hidden="true"></i></a>
                                <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txt_password" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>

                                <span class="highlight"></span>
                                <label>Password</label>
                            </div>
                        </div>
                    </div>
                    <!-- Password END -->
                    <!-- Captcha Start -->
                    <div class="d-flex justify-content-between align-items-center">

                        <div class="floating-form animationss a3">
                            <div class="floating-label">
                                <asp:TextBox ID="txtcaptcha" runat="server" TabIndex="3" Style='text-transform: uppercase; padding-left: 10px; width: 130px;' ValidationGroup="login" CssClass="floating-input" type="text" placeholder=" "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="Captcha Required !" ControlToValidate="txtcaptcha" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                <span class="highlight"></span>
                                <label>Captcha</label>
                            </div>
                        </div>
                        <div class="form-group mr-1 animation a3" style="margin-bottom: 1.5rem">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="input-group">
                                        <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="4"
                                            CaptchaHeight="42" CaptchaWidth="145" CaptchaLineNoise="None" CaptchaMinTimeout="5" Font-Size="12px" BackColor="#c8cfe9"
                                            NoiseColor="#666699" CaptchaInputCSS="BlackTextInput" EnableTheming="true" CaptchaChars="123456789" CaptchaMaxTimeout="240" FontColor="#CD354D" class="img-responsive" />
                                        <div class="input-group-append">
                                            <span class="input-group-text">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Login/refresh-btn.png" OnClick="imgrefresh_Click" />
                                            </span>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <!-- Captcha END -->

                    <!-- Login Button Start -->
                    <asp:LinkButton class="form-control btn btn-primary text-white pl-2 animation a1" ID="btnLogin" TabIndex="4" runat="server" OnClick="btnLogin_Click" Text="Sign In" ValidationGroup="login"></asp:LinkButton>
                    <!-- Login Button END -->

                    <div style="text-align: -webkit-center; text-align: -moz-center;">
                        <asp:TextBox runat="server" ID="txtdemo" Style="display: none" />
                    </div>
                    <asp:HiddenField ID="hdfuserno" runat="server" />
                    <asp:HiddenField ID="hdffirstlog" runat="server" />
                    <asp:HiddenField ID="hdflastlogout" runat="server" />
                    <asp:HiddenField ID="hdfAllowpopup" runat="server" />

                    <div class="forget-pass text-center mt-3 animation a2">
                        <a data-toggle="modal" data-target="#ModelPopUp" id="lbtForgePass" runat="server" style="cursor: pointer; color: #CD354D;"><span>Forgot Password 
                        </span></a>
                    </div>


                    <a href="TP_Company_Registration.aspx" id="regCompLink" runat="server" visible="false">TP Company Reg.</a>
                    <asp:ValidationSummary ID="vsLogin" runat="server" ShowMessageBox="true"
                        ShowSummary="false" DisplayMode="List" ValidationGroup="login" />

                    <!-- Other links click Start -->

                    <div class="d-flex justify-content-between align-items-center mt-1">
                        <!-- T&P Sign Up Start -->
                        <div class="forget-pass text-white">
                            <a href="TP_Reg_Form.aspx" id="tpSignupLink" runat="server" style="cursor: pointer" visible="false"><span>T&P Sign Up </span></a>
                        </div>
                        <!-- T&P Sign Up END -->
                        <!-- Library Sign Up Start -->
                        <div class="virtual-key">
                            <a href="https://libcloud.mastersofterp.in/" id="LibSignUP" runat="server" target="_blank" style="cursor: pointer; display: none;"><span>Library Sign Up</span></a>
                        </div>
                        <!-- Library Sign Up END -->
                    </div>

                    <div class="d-flex justify-content-between align-items-center mt-1">
                        <!-- G Sign in Start -->
                        <div class="d-none">
                            <span class="g-signin2" data-onsuccess="onSignIn" data-theme="dark"></span>
                        </div>
                        <!-- G Sign in END -->
                        <!-- Google Play Start -->
                        <div class="d-none">
                            <a href="" target="_blank" style="cursor: pointer;">
                                <span>
                                    <img alt="App Downloads" src="Images/GPlayImg.png" /></span>
                            </a>
                        </div>
                        <!-- Google Play END -->
                    </div>

                </div>


            </section>

            <div class="img-mit animations a2">
                <img src="Images/Login/mit-img.png" alt="image" />
            </div>
            <!-- Modal Popup Forget Password -->

            <div class="compatiblity">
                <div class="container-fluid">
                    <ul class="list-inline">
                        <li>Site Compatible - </li>
                        <li>
                            <img src="IMAGES/chrome.png" alt="chrome">
                            Google Chrome 70+ ,</li>
                        <li>
                            <img src="IMAGES/firefox.png" alt="firefox">
                            Firefox 65+ ,</li>
                        <li>
                            <img src="IMAGES/edge.png" alt="firefox">
                            Microsoft Edge 89+</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ModelPopUp" role="dialog">
            <div class="modal-dialog modal-md">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Forgot Password</h4>
                                <button type="button" class="close" data-dismiss="modal" title="Close">&times;</button>
                            </div>
                            <div class="modal-body clearfix pb-0">
                                <div class="col-12">
                                    <div class="form-group">
                                        <asp:Panel ID="pnlUserPass" runat="server" Visible="true">

                                            <asp:RadioButton runat="server" ID="rdoUsername" Text="UserName" GroupName="UserPass" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoUsername_CheckedChanged" />&nbsp;&nbsp;

                                                        <%-- <asp:RadioButton runat="server" ID="rdoUsername" Text="UserName" GroupName="UserPass" Font-Bold="true" />--%>
                                            <asp:RadioButton runat="server" ID="rdoPassword" Text="Password" GroupName="UserPass" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoPassword_CheckedChanged" />

                                        </asp:Panel>
                                        <br />

                                        <asp:Panel ID="pnlChoice" runat="server" Visible="false">

                                            <div id="dvChoice">

                                                <asp:RadioButton runat="server" ID="rdoMobile" Text=" Mobile Number" GroupName="MobileEmail" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoMobile_CheckedChanged" />
                                                <asp:RadioButton runat="server" ID="rdoEmail" Text=" Email Address" GroupName="MobileEmail" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoEmail_CheckedChanged" />

                                            </div>
                                        </asp:Panel>
                                        <br />

                                        <asp:Panel ID="pnlMobile" runat="server" Visible="false">
                                            <div id="dvmobile">
                                                <div class="form-group">
                                                    <asp:Label ID="lblmobile" runat="server">Enter Registered Mobile Number:</asp:Label>
                                                    <asp:TextBox ID="txtMobile" runat="server" TabIndex="1" placeholder="Enter Registered Mobile Number" CssClass="form-control"></asp:TextBox><br />

                                                    <asp:Button ID="btnSendotp" runat="server" Text="Send OTP" CssClass="btn btn-info btn-sm" OnClick="btnMobileotp_Click" />
                                                    <asp:Label runat="server" ID="lblVerify"></asp:Label>



                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile"
                                                        ErrorMessage="Mobile Number Required !" Display="None" ValidationGroup="login1"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                                        TargetControlID="txtMobile" WatermarkText="Enter Mobile Number" />
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblOtp" runat="server" Visible="false">Enter OTP:</asp:Label>
                                                        <asp:TextBox ID="txtOtp" runat="server" Visible="false" TabIndex="2" placeholder="Enter OTP" CssClass="form-control"></asp:TextBox>
                                                        <%-- <asp:Button ID="btnMobOtpVerify" runat="server" Text="Verify OTP" Visible="false" CssClass="btn-light" OnClick="btnMobOtpVerify_Click" /> --%>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOtp"
                                                            ErrorMessage="Otp Required !" Display="None" ValidationGroup="Loginotp"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server"
                                                            TargetControlID="txtOtp" WatermarkText="Enter OTP" />

                                                        <asp:Button ID="btnForgotPasswordMobile" runat="server" Text="Forgot Password" ValidationGroup="Loginotp" CssClass="btn btn-primary" OnClick="btnForgotPasswordMobile_Click" />

                                                        <asp:Button ID="btnCancelp" runat="server" CssClass="btn btn-warning" Text="Cancel" />

                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlEmail" runat="server" Visible="false">
                                            <div id="dvEmail">

                                                <div class="form-group">
                                                    <asp:Label ID="lbEmail" runat="server">Enter Registered Email Address:</asp:Label>
                                                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="1" placeholder="Enter Registered Email Address" CssClass="form-control"></asp:TextBox><br />

                                                    <asp:Button ID="benotop2" runat="server" Text="Send OTP" CssClass="btn btn-info btn-sm" OnClick="btnEmailotp_Click" />
                                                    <asp:Label runat="server" ID="lblVerify2"></asp:Label>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail"
                                                        ErrorMessage="Email Address Required !" Display="None" ValidationGroup="login1"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server"
                                                        TargetControlID="txtEmail" WatermarkText="Enter Email Address" />
                                                </div>
                                                <div class="form-group">

                                                    <asp:Label ID="lblotp2" runat="server" Visible="false">Enter OTP:</asp:Label>
                                                    <asp:TextBox ID="txtOtp2" runat="server" TabIndex="2" Visible="false" placeholder="Enter OTP" CssClass="form-control"></asp:TextBox>
                                                    <%--                                                                     <asp:Button ID="btnVerifyEmailOtp" runat="server" Text="Verify OTP" Visible="false" CssClass="btn-light" OnClick="btnVerifyEmailOtp_Click" /><br /> --%>



                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtOtp2"
                                                        ErrorMessage="Otp Required !" Display="None" ValidationGroup="Loginotp"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server"
                                                        TargetControlID="txtOtp2" WatermarkText="Enter OTP" />

                                                </div>
                                                <br />
                                                <div>

                                                    <asp:Button ID="btnSendUsernamePassword" runat="server" Text="Forgot Password" ValidationGroup="Loginotp" CssClass="btn btn-primary" OnClick="btnSendUsernamePassword_Click" />
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClientClick="return validate()" />

                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                </div>
                                <p>
                                    &nbsp;<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Loginotp" DisplayMode="List" />

                                </p>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_Reset" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-md">
                <asp:UpdatePanel ID="updReset" runat="server">
                    <ContentTemplate>
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Reset Password</h4>
                                <button type="button" class="close" data-dismiss="modal" title="Close">&times;</button>
                            </div>
                            <div class="modal-body clearfix pb-0">
                                <div class="col-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtName" runat="server" TabIndex="1" placeholder="Enter User Name" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:TextBox ID="txtName" runat="server" TabIndex="1" placeholder="Enter User Name" CssClass="form-control" OnTextChanged="txtName_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Username Required !" Display="None" ValidationGroup="login1"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                            TargetControlID="txtName" WatermarkText="Enter User Name" />
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txt_emailid" runat="server" placeholder="Enter Email Id" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email Id Required !"
                                            ControlToValidate="txt_emailid" Display="None" ValidationGroup="login1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_emailid"
                                            Display="None" ErrorMessage="Enter Valid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="login1"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                            TargetControlID="txt_emailid" WatermarkText="Enter Registered Email Id" />
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txt_captcha" runat="server" placeholder="Enter Captcha" Style='text-transform: uppercase' TabIndex="3" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="txt_captcha_TextBoxWatermarkExtender" runat="server"
                                            TargetControlID="txt_captcha" WatermarkText="Enter Following Captcha" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Captcha Character not Matched!"
                                            ControlToValidate="txt_captcha" Display="None" ValidationGroup="login1"></asp:RequiredFieldValidator>

                                        <cc1:CaptchaControl ID="CaptchaControl1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                            CaptchaHeight="40" CaptchaWidth="110" CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                            CaptchaMaxTimeout="240" FontColor="#529E00" Font-Size="Smaller" />
                                    </div>
                                </div>
                                <p>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="login1" DisplayMode="List" />
                                    <p>
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                    </p>
                                </p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btn_Reset" runat="server" Text="Submit" ValidationGroup="login1" CssClass="btn btn-primary" OnClick="btn_Reset_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClientClick="return validate()" />
                                <asp:Label ID="lbl1" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl2" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl3" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl4" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl5" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl6" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl7" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl8" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl9" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbl10" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_Reset" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- Modal Popup Forget Password END-->

    </form>

    <!-- Script -->
    <script src="plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="plugins/newbootstrap/js/popper.min.js"></script>
    <script src="plugins/newbootstrap/js/bootstrap.min.js"></script>
    <script src="plugins/virtual-keyboard/keyboard.js"></script>

    <script>
        window.onload = function () {
            document.getElementById("txt_password").focus();
            document.getElementById("txt_username").focus();
        };
    </script>

    <script>
        $("#txt_username").focus(function () {
            $(".fa-user").css("color", "#f48f9e");
        });
        $("#txt_username").focusout(function () {
            $(".fa-user").css("color", "#90b0d7");
        });

        $("#show_hide_password .floating-input").focus(function () {
            $(".fa-eye-slash").css("color", "#f48f9e");
        });
        $("#show_hide_password .floating-input").focusout(function () {
            $(".fa-eye-slash").css("color", "#90b0d7");
        });
    </script>
    <%-- Disable Right Click --%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("body, html").on("contextmenu", function (e) {
                return false;
            });
        });
    </script>--%>
    <%-- X --%>

    <%-- added by Prafull on 16-11-2021--%>
    <script>
        function validate() {

            document.getElementById('txtName').value = '';
            document.getElementById('txt_emailid').value = '';
            document.getElementById('txt_captcha').value = '';
            return false;
        }
    </script>

    <!-- Script for Browser validation -->
    <script type="text/javascript">
        (function () {
            'use strict';

            var module = {
                options: [],
                header: [navigator.platform, navigator.userAgent, navigator.appVersion, navigator.vendor, window.opera],
                dataos: [
                    { name: 'Windows Phone', value: 'Windows Phone', version: 'OS' },
                    { name: 'Windows', value: 'Win', version: 'NT' },
                    { name: 'iPhone', value: 'iPhone', version: 'OS' },
                    { name: 'iPad', value: 'iPad', version: 'OS' },
                    { name: 'Kindle', value: 'Silk', version: 'Silk' },
                    { name: 'Android', value: 'Android', version: 'Android' },
                    { name: 'PlayBook', value: 'PlayBook', version: 'OS' },
                    { name: 'BlackBerry', value: 'BlackBerry', version: '/' },
                    { name: 'Macintosh', value: 'Mac', version: 'OS X' },
                    { name: 'Linux', value: 'Linux', version: 'rv' },
                    { name: 'Palm', value: 'Palm', version: 'PalmOS' }
                ],
                databrowser: [
                    { name: 'Chrome', value: 'Chrome', version: 'Chrome' },
                    { name: 'Firefox', value: 'Firefox', version: 'Firefox' },
                    { name: 'Safari', value: 'Safari', version: 'Version' },
                    { name: 'Internet Explorer', value: 'MSIE', version: 'MSIE' },
                    { name: 'Opera', value: 'Opera', version: 'Opera' },
                    { name: 'BlackBerry', value: 'CLDC', version: 'CLDC' },
                    { name: 'Mozilla', value: 'Mozilla', version: 'Mozilla' }
                ],
                init: function () {
                    var agent = this.header.join(' '),
                        os = this.matchItem(agent, this.dataos),
                        browser = this.matchItem(agent, this.databrowser);

                    return { os: os, browser: browser };
                },
                matchItem: function (string, data) {
                    var i = 0,
                        j = 0,
                        html = '',
                        regex,
                        regexv,
                        match,
                        matches,
                        version;

                    for (i = 0; i < data.length; i += 1) {
                        regex = new RegExp(data[i].value, 'i');
                        match = regex.test(string);
                        if (match) {
                            regexv = new RegExp(data[i].version + '[- /:;]([\\d._]+)', 'i');
                            matches = string.match(regexv);
                            version = '';
                            if (matches) { if (matches[1]) { matches = matches[1]; } }
                            if (matches) {
                                matches = matches.split(/[._]+/);
                                for (j = 0; j < matches.length; j += 1) {
                                    if (j === 0) {
                                        version += matches[j] + '.';
                                    } else {
                                        version += matches[j];
                                    }
                                }
                            } else {
                                version = '0';
                            }
                            return {
                                name: data[i].name,
                                version: parseFloat(version)
                            };
                        }
                    }
                    return { name: 'unknown', version: 0 };
                }
            };

            var e = module.init(),
                debug = '';

            // debug += 'os.name = ' + e.os.name + '<br/>';
            //  debug += 'os.version = ' + e.os.version + '<br/>';
            debug += 'browser.name = ' + e.browser.name + '<br/>';
            frmDefault
            if ((e.browser.name) == 'Chrome') {
                if ((e.browser.version) >= 70) {
                    //    alert('You are using Updated Browser')
                }
                else {
                    alert("The current version of your browser is not compatible, please update your browser.");
                    setTimeout(function () {
                        window.location.href = "https://www.google.com/chrome/";
                    }, 300);

                }
            }

            if ((e.browser.name) == 'Firefox') {
                if ((e.browser.version) >= 65) {
                    //    alert('You are using Updated Browser')
                }
                else {
                    alert("The current version of your browser is not compatible, please update your browser.");
                    setTimeout(function () {
                        window.location.href = "https://www.mozilla.org/en-US/firefox/download/thanks/?scene=2#download-fx";
                    }, 300);
                }
            }
            debugger;
            if ((e.browser.name) == 'Internet Explorer') {
                if ((e.browser.version) >= 10) {
                    // alert('You are using Updated Browser')
                }
                else {
                    alert("The current version of your browser is not compatible, please update your browser.");
                    setTimeout(function () {
                        window.location.href = "https://www.microsoft.com/en-in/download/internet-explorer.aspx";
                    }, 300);
                }
            }
            debug += 'browser.version = ' + e.browser.version + '<br/>';
            debug += '<br/>';
            //    debug += 'navigator.userAgent = ' + navigator.userAgent + '<br/>';
            //   debug += 'navigator.appVersion = ' + navigator.appVersion + '<br/>';
            //  debug += 'navigator.platform = ' + navigator.platform + '<br/>';
            //  debug += 'navigator.vendor = ' + navigator.vendor + '<br/>';
            //  document.getElementById('log').innerHTML = debug;
        }());
    </script>
    <!-- end -->

    <%-- Added Google Sign In on Date 22/06/2020 by Mr.Aman--%>
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <script type="text/javascript">
        function onSignIn(googleUser) {
            var profile = googleUser.getBasicProfile();

            console.log("Email: " + profile.getEmail());
            // The ID token you need to pass to your backend:
            var id_token = googleUser.getAuthResponse().id_token;

            var sdata = {
                "id": profile.getId(),
                "idToken": id_token
            };
            var gmail_id = profile.getEmail();
            var get_id = profile.getId();
            var userno = "";
            var firstlog = "";
            var lastlogout = "";
            var Allowpopup = "";

            $.ajax({
                url: '<%=Page.ResolveUrl("~/WEB_API/Stud_Info.asmx/Get_GoogleUserDetails")%>',
                data: { SLOGINDATA: get_id },//, SLOGINDATA: sloginid },IDTOKEN: gmail_id, 
                type: 'POST',
                dataType: 'text',
                success: function (r) {
                    if (gmail_id != "" && get_id != "") {
                        CheckGmailAuthoOnDefaul(get_id, gmail_id);
                    }
                    else {
                        alert("Please Registered the Google Sign In...!")
                    }


                },
                error: function (e) {
                    alert('Please Registered the Google Sign In...!');
                }

            })

            var auth2 = gapi.auth2.getAuthInstance();
            auth2.signOut().then(function () {
                console.log('User signed out.');
            });

        };

        function CheckGmailAuthoOnDefaul(SLOGINDATA, IDTOKEN) {
            PageMethods.GetData(SLOGINDATA, IDTOKEN, onSucess, onError);
            function onSucess(response) {
                for (var i in response) {
                    userno = response[i].userno;
                    firstlog = response[i].firstlog;
                    lastlogout = response[i].lastlogout;
                    Allowpopup = response[i].Allowpopup;
                }

                if (firstlog == "False") {
                    //alert(firstlog);
                    window.location.href = "https://makaut.mastersofterp.in/changePassword.aspx?IsReset=1";
                }
                else {
                    if (lastlogout == "" && Allowpopup == "1") {
                        window.location.href = "https://makaut.mastersofterp.in/SignoutHold.aspx";
                    }
                    else if (userno == "1") {
                        // alert(userno);
                        window.location.href = "https://makaut.mastersofterp.in/home.aspx";
                    }
                    else {
                        window.location.href = "https://makaut.mastersofterp.in/home.aspx";
                    }
                }
            }

            function onError(response) {
                alert('Please Registered the Google Sign In...!');
            }
        }
    </script>
    <%-- End Google Sign In on Date 22/06/2020 by Mr.Aman --%>
    <script type="text/javascript">
        function onSignIn_test() {
            gapi.load({
                'callback': function () {
                    gapi.auth2.init();
                }
            });

            gapi.auth.signIn({
                'callback': function (authResult) {
                }
            });
        }
    </script>

    <script type="text/javascript">
        var screenHeight = screen.availHeight;
    </script>

    <!-- Hide/Show Password Script -->
    <script>
        $(document).ready(function () {
            $("#show_hide_password a").on('click', function (event) {
                event.preventDefault();
                if ($('#show_hide_password input').attr("type") == "text") {
                    $('#show_hide_password input').attr('type', 'password');
                    $('#show_hide_password i').addClass("fa-eye-slash");
                    $('#show_hide_password i').removeClass("fa-eye");
                } else if ($('#show_hide_password input').attr("type") == "password") {
                    $('#show_hide_password input').attr('type', 'text');
                    $('#show_hide_password i').removeClass("fa-eye-slash");
                    $('#show_hide_password i').addClass("fa-eye");
                }
            });
        });
    </script>
    <!-- Hide/Show Password Script END -->
</body>
</html>
