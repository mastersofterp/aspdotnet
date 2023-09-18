﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default_DAIICT.aspx.cs" Inherits="default_DAIICT" %>

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

    function validateto() {

        document.getElementById('txtMobile').value = '';
        document.getElementById('txtEmail').value = '';
        document.getElementById('txt_captcha').value = '';
        return false;
    }

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
</script>


<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>DA-IICT</title>
    <link rel="shortcut icon" href="Images/Login/DAIICT_logo.png" type="image/x-icon">

    <!-- Bootstrap -->
    <link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="plugins/newbootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/css/login_daiict.css" rel="stylesheet" />

    <style>
        .logo img {
            width: auto;
            height: 280px;
        }

        .modal-backdrop {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1;
            width: 100vw;
            height: 100vh;
            background-color: #000;
        }

        .input-group-text {
            background-color: transparent;
            border: 0px solid transparent;
        }

        #txtcaptcha {
            width: auto;
        }

        @media (min-width:576px) and (max-width:991px) {
            .logo img {
                width: auto;
                height: 250px;
            }
        }

        @media (max-width:576px) {
            .logo img {
                width: 50%;
                height: auto;
                margin-top: 5px;
                margin-bottom: 15px;
            }

            #txtcaptcha {
                width: 115px;
            }
        }

        @media (max-width:360px) {
            #txtcaptcha {
                width: 104px;
            }
        }
    </style>

      <style type="text/css">
        .modalBackground {
            background-color: #ccc; 
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .modalPopup {
            background-color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 300px;
            height: 200px;
            overflow-y: auto;
        }

    </style>
</head>
<body>
    <form id="frmDefault" runat="server" defaultbutton="btnLogin">
        <div class="bg-video-wrap">

            <div class="sign-in-video scrollbar" id="style-1">

                <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" EnablePageMethods="true" runat="server" ID="Script_water" />
                <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="90000">
                </asp:Timer>
                <div class="container-fluid force-overflow ">
                    <div class="row">
                        <div class="col-lg-6 col-xl-4 col-xl-1 col-md-8 offset-md-2 offset-lg-0 mb-0 mb-md-0 ml-lg-5 pl-lg-5">
                            <div class="side-background pt-2 pb-0 pt-sm-5 pb-sm-5">
                                <div class="text-center pt-lg-4 pb-lg-5 pt-md-2 pb-md-4">
                                    <div class="logo in-left a2">
                                        <asp:Image ID="imglogo" runat="server" ImageUrl="~/Images/Login/DAIICT_logo.png" />
                                        <%--<img src="Images/Login/DAIICT_logo.png"  alt="logo"/>--%>
                                    </div>
                                    <%--<div class="college-name in-left a3">
                                        <h2 class="mt-2 mt-md-3 mb-2 mb-md-2">Dhirubhai Ambani Institute of Information and Communication Technology</h2>
                                    </div>
                                    <div class="clg-address pb-md-3 in-left a4">
                                        <p>
                                            DA-IICT, Near Indroda Circle, Gandhinagar 382 007, Gujarat (India)
                                        </p>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8 offset-md-2 col-lg-5 offset-lg-2 col-xl-4 offset-xl-2 mt-lg-4 mt-xl-0 mb-4 mb-md-0">

                            <div class="login-form in-left a4">
                                <div class="sign-heading">
                                    <h4>Sign In</h4>
                                </div>
                                <div class="form-group mt-3">
                                    <label>User Name</label>
                                    <asp:TextBox ID="txt_username" runat="server" TabIndex="1" onCopy="return false" onPaste="return false" onCut="return false" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="req_username" runat="server" ControlToValidate="txt_username" ErrorMessage="Username Required !" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                    <i class="fas fa-user"></i>
                                </div>
                                <div class="form-group">
                                    <label>Password</label>


                                    <div id="show_hide_password">
                                        <asp:TextBox ID="txt_password" runat="server" TabIndex="2" TextMode="Password" onCopy="return false" onPaste="return false" onCut="return false" CssClass="form-control"></asp:TextBox>
                                        <i class="fas fa-lock"></i>
                                        <a href="#"><i class="fa fa-eye-slash" aria-hidden="true"></i></a>
                                        <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txt_password" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <!-- Captcha Start -->
                                <div class="d-flex justify-content-between align-items-center">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="input-group">
                                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="4"
                                                    CaptchaHeight="33" CaptchaWidth="125" CaptchaLineNoise="None" CaptchaMinTimeout="5" Font-Size="12px" BackColor="#edf8f2"
                                                    NoiseColor="#D9261C" CaptchaInputCSS="BlackTextInput" EnableTheming="true" CaptchaChars="123456789" CaptchaMaxTimeout="240" FontColor="#fd7e14" class="img-responsive" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text">
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/refresh-btn.png" OnClick="imgrefresh_Click" />
                                                    </span>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <%--<div style='background-color: #6a6a6a; font-size: Smaller;'>
                                            <img src="CaptchaImage.axd?guid=39332eee-4c12-489c-9512-b806d4e02802" border='0' width="130" height="34">
                                        </div>
                                        <input type="image" name="ImageButton1" id="Image1" src="IMAGES/fi-rr-refresh.png" onclick="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;ImageButton1&quot;, &quot;&quot;, true, &quot;&quot;, &quot;&quot;, false, false))" style="margin-top: -130px; margin-left: 135px; margin-bottom: 25px; background: transparent;" />--%>


                                    <div class="form-group pl-1 mb-0">
                                        <asp:TextBox ID="txtcaptcha" runat="server" TabIndex="3" Style='text-transform: uppercase; padding-left: 10px;' ValidationGroup="login" CssClass="form-control" placeholder="Enter Captcha"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="Captcha Required !" ControlToValidate="txtcaptcha" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>

                                    </div>

                                </div>
                                <!-- Captcha END -->
                                <!-- OTP Start -->

                                <!-- OTP END -->
                                <div class="mt-4">
                                    <asp:LinkButton class="btn btn-outline-info mt-md-1 d-block" ID="btnLogin" TabIndex="4" runat="server" OnClick="btnLogin_Click" ValidationGroup="login"> Sign In </asp:LinkButton>

                                </div>

                                <div class="text-center">

                                    <div class="forget-pass">
                                        <a data-toggle="modal" data-target="#ModelPopUp" id="lbtForgePass" runat="server" style="cursor: pointer"><span>Forgot Password </span></a>
                                        <%--<a id="A1" data-toggle="modal" data-target="#myModal" tabindex="7" style="cursor: pointer" onclick="lbtForgePass_Click"><span>Forgot Password </span></a>--%>
                                    </div>
                                </div>

                            </div>



                            <div style="text-align: -webkit-center; text-align: -moz-center;">
                                <%--<a href="#" onclick="signOut(); ">Sign out</a>--%>
                                <asp:TextBox runat="server" ID="txtdemo" Style="display: none" />
                            </div>
                            <asp:HiddenField ID="hdfuserno" runat="server" />
                            <asp:HiddenField ID="hdffirstlog" runat="server" />
                            <asp:HiddenField ID="hdflastlogout" runat="server" />
                            <asp:HiddenField ID="hdfAllowpopup" runat="server" />

                            <a href="TP_Company_Registration.aspx" id="regCompLink" runat="server" visible="false">TP Company Reg.</a>
                            <asp:ValidationSummary ID="vsLogin" runat="server" ShowMessageBox="true"
                                ShowSummary="false" DisplayMode="List" ValidationGroup="login" />

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

                            <div class="compatiblity">
                                <div class="container-fluid">
                                    <ul class="list-inline">
                                        <li>Site Compatible - </li>
                                        <li>
                                            <asp:Image ID="Imagechrome" runat="server" ImageUrl="~/IMAGES/chrome.png" />

                                            Google Chrome 70+ ,</li>
                                        <li>
                                            <asp:Image ID="Imagefirefox" runat="server" ImageUrl="~/IMAGES/firefox.png" />
                                            Firefox 65+ ,</li>
                                        <li>

                                            <asp:Image ID="Imageedge" runat="server" ImageUrl="~/IMAGES/edge.png" />
                                            Microsoft Edge 89+</li>
                                    </ul>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Popup Forget Password -->
        <div class="modal fade" id="ModelPopUp" role="dialog">
            <div class="modal-dialog modal-md">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" style="font-weight: 600;">Forgot Password / User Name</h5>
                                <%-- <button type="button" class="close" data-dismiss="modal" title="Close" onclick="CloseToClearpopup()">&times;</button>   --%>
                                <asp:ImageButton ID="clsepop" runat="server" OnClick="clsepop_Click" ImageUrl="~/Images/cancel.gif" />
                            </div>
                            <div class="modal-body clearfix pb-0">
                                <div class="col-12">
                                    <asp:Panel ID="pnlUserPass" runat="server" Visible="true">

                                        <asp:RadioButton runat="server" ID="rdoUsername" Text="UserName" GroupName="UserPass" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoUsername_CheckedChanged" />&nbsp;&nbsp;

                                            <asp:RadioButton runat="server" ID="rdoPassword" Text="Password" GroupName="UserPass" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoPassword_CheckedChanged" />
                                        <!--=========== by default password radio button checked ============-->
                                    </asp:Panel>
                                </div>

                                <div class="col-12 mt-2">
                                    <asp:Panel ID="pnlChoice" runat="server" Visible="false">
                                        <div id="dvChoice">
                                            <!--=========== by default Email radio button checked ============-->
                                            <asp:RadioButton runat="server" ID="rdoEmail" Text=" Email Address" GroupName="MobileEmail" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoEmail_CheckedChanged" />&nbsp;&nbsp;
                                                <asp:RadioButton runat="server" ID="rdoMobile" Text=" Mobile Number" GroupName="MobileEmail" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoMobile_CheckedChanged" />
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 mt-2">
                                    <asp:Panel ID="pnlMobile" runat="server" Visible="false">
                                        <div id="dvmobile">
                                            <!--=========== by default Text input open ============-->
                                            <div class="form-group">
                                                <asp:Label ID="lblmobile" runat="server" Style="font-weight: 600;">Enter Registered Mobile Number</asp:Label>
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtMobile" runat="server" TabIndex="1" placeholder="Enter Registered Mobile Number" MaxLength="10" CssClass="form-control mt-1" onkeypress="return isNumber(event)"></asp:TextBox><br />

                                                    <asp:Button ID="btnSendotp" runat="server" Text="Send OTP" CssClass="btn btn-outline-info ml-3 mt-1" OnClick="btnMobileotp_Click" ValidationGroup="logmob" />
                                                </div>
                                                <asp:Label runat="server" ID="lblVerify"></asp:Label>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile"
                                                    ErrorMessage="Please Enter Mobile No." Display="None" ValidationGroup="logmob"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                                    TargetControlID="txtMobile" WatermarkText="Enter Mobile Number" />

                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblOtp" runat="server" Visible="false" Style="font-weight: 600;">Enter OTP</asp:Label>
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtOtp" runat="server" Visible="false" TabIndex="2" placeholder="Enter OTP" CssClass="form-control mt-1"></asp:TextBox>
                                                    <asp:Button ID="btnMobOtpVerify" runat="server" Text="Verify OTP" Visible="false" CssClass="btn btn-outline-info ml-3 mt-1" OnClick="btnMobOtpVerify_Click" />

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOtp"
                                                        ErrorMessage="Otp Required !" Display="None" ValidationGroup="Loginotp"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server"
                                                        TargetControlID="txtOtp" WatermarkText="Enter OTP" />
                                                </div>
                                            </div>

                                            <div class="form-group" runat="server" visible="false" id="divLoginId">
                                                <asp:Label ID="Label5" runat="server" Style="font-weight: 600;">User Name</asp:Label>
                                                <asp:TextBox ID="txtusername" runat="server" placeholder="Enter User Name" CssClass="form-control mt-1" Enabled="false" MaxLength="25"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtusername"
                                                    Display="None" ErrorMessage="Enter User Name"
                                                    ValidationGroup="changePassword"></asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group" runat="server" visible="false" id="divnewpass">
                                                <asp:Label ID="Label1" runat="server" Style="font-weight: 600;">New Password</asp:Label>
                                                <asp:TextBox ID="txtnewpass" runat="server" placeholder="Enter New Password" CssClass="form-control mt-1" TextMode="Password" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" ControlToValidate="txtnewpass"
                                                    Display="None" ErrorMessage="New Password Required"
                                                    ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:PasswordStrength ID="PasswordStrength1" TargetControlID="txtnewpass" StrengthIndicatorType="Text"
                                                    PrefixText="Strength:" HelpStatusLabelID="lblhelp1" PreferredPasswordLength="8"
                                                    MinimumNumericCharacters="1" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" MinimumLowerCaseCharacters="1" RequiresUpperAndLowerCaseCharacters="true"
                                                    TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                                                    TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;AverageStrength;GoodStrength;ExcellentStrength"
                                                    runat="server" />

                                            </div>

                                            <div class="form-group" runat="server" visible="false" id="divconfirmpass">
                                                <asp:Label ID="Label2" runat="server" Style="font-weight: 600;">Confirm Password</asp:Label>
                                                <asp:TextBox ID="txtconfirmpass" runat="server" placeholder="Confirm Password" CssClass="form-control mt-1" TextMode="Password" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvConfirmPass" runat="server" ControlToValidate="txtconfirmpass"
                                                    Display="None" ErrorMessage="Confirm Password Required"
                                                    ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New &amp; Confirm Password Not Matching"
                                                    ControlToCompare="txtnewpass" ControlToValidate="txtconfirmpass" Display="None"
                                                    ValidationGroup="changePassword"></asp:CompareValidator>
                                            </div>

                                            <div class="text-center">
                                                <asp:Button ID="btnForgotPasswordMobile" runat="server" Text="Change Password" ValidationGroup="changePassword" CssClass="btn btn-outline-info" OnClick="btnForgotPasswordMobile_Click" />
                                                <asp:Button ID="btnCancelp" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancelp_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="changePassword"
                                                    ShowMessageBox="True" ShowSummary="False" DisplayMode="List" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlEmail" runat="server" Visible="false">
                                        <div id="dvEmail">
                                            <label id="lblhelp1" runat="server"></label>
                                            <div class="form-group">
                                                <asp:Label ID="lbEmail" runat="server" Style="font-weight: 600;">Enter Registered Email Address</asp:Label>
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="1" placeholder="Enter Registered Email Address" CssClass="form-control mt-1" MaxLength="64"></asp:TextBox>
                                                    <asp:Button ID="benotop2" runat="server" Text="Send OTP" CssClass="btn btn-outline-info mt-1 ml-3" OnClick="btnEmailotp_Click" ValidationGroup="logmail" />
                                                </div>
                                                <asp:Label runat="server" ID="lblVerify2"></asp:Label>

                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                                    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                    ErrorMessage="Email Address Require in valid format" Display="None" ValidationGroup="logmail" />--%>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail"
                                                    ErrorMessage="Please Enter Email Address." Display="None" ValidationGroup="logmail"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server"
                                                    TargetControlID="txtEmail" WatermarkText="Enter Email Address" />
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblotp2" runat="server" Visible="false" Style="font-weight: 600;">Enter OTP</asp:Label>
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtOtp2" runat="server" TabIndex="2" Visible="false" placeholder="Enter OTP" CssClass="form-control mt-1"></asp:TextBox>
                                                    <asp:Button ID="btnVerifyEmailOtp" runat="server" Text="Verify OTP" Visible="false" CssClass="btn btn-outline-info ml-3 mt-1" OnClick="btnVerifyEmailOtp_Click" />
                                                </div>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtOtp2"
                                                    ErrorMessage="Otp Required !" Display="None" ValidationGroup="Loginotp"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server"
                                                    TargetControlID="txtOtp2" WatermarkText="Enter OTP" />
                                            </div>

                                            <div class="form-group" runat="server" visible="false" id="divEloginId">
                                                <asp:Label ID="Label6" runat="server" Style="font-weight: 600;">User Name</asp:Label>
                                                <asp:TextBox ID="txtEusername" runat="server" placeholder="Enter User Name" CssClass="form-control mt-1" MaxLength="25" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtEusername"
                                                    Display="None" ErrorMessage="Enter User Name"
                                                    ValidationGroup="EchangePassword"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group" runat="server" visible="false" id="divEnewPass">
                                                <asp:Label ID="Label3" runat="server" Style="font-weight: 600;">New Password</asp:Label>
                                                <asp:TextBox ID="txtEnewpass" runat="server" placeholder="Enter New Password" CssClass="form-control mt-1" TextMode="Password" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEnewpass"
                                                    Display="None" ErrorMessage="New Password Required"
                                                    ValidationGroup="EchangePassword"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:PasswordStrength ID="pwdStrength" TargetControlID="txtEnewpass" StrengthIndicatorType="Text"
                                                    PrefixText="Strength:" HelpStatusLabelID="lblhelp1" PreferredPasswordLength="8"
                                                    MinimumNumericCharacters="1" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" MinimumLowerCaseCharacters="1" RequiresUpperAndLowerCaseCharacters="true"
                                                    TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                                                    TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;AverageStrength;GoodStrength;ExcellentStrength"
                                                    runat="server" />
                                            </div>

                                            <div class="form-group" runat="server" visible="false" id="divEconfirmpass">
                                                <asp:Label ID="Label4" runat="server" Style="font-weight: 600;">Confirm Password</asp:Label>
                                                <asp:TextBox ID="txtEconfirmPass" runat="server" placeholder="Confirm Password" CssClass="form-control mt-1" TextMode="Password" MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEconfirmPass"
                                                    Display="None" ErrorMessage="Confirm Password Required"
                                                    ValidationGroup="EchangePassword"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="New &amp; Confirm Password Not Matching"
                                                    ControlToCompare="txtEnewpass" ControlToValidate="txtEconfirmPass" Display="None"
                                                    ValidationGroup="EchangePassword"></asp:CompareValidator>
                                            </div>

                                            <div class="text-center">
                                                <asp:Button ID="btnSendUsernamePassword" runat="server" Text="Change Password" ValidationGroup="EchangePassword" CausesValidation="false" CssClass="btn btn-outline-info" OnClick="btnSendUsernamePassword_Click" />
                                                <asp:Button ID="btnEmailCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnEmailCancel_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="EchangePassword"
                                                    ShowMessageBox="True" ShowSummary="False" DisplayMode="List" />
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                                <p>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Loginotp" DisplayMode="List" />
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="logmob" />
                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="logmail" />
                                </p>
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
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="login1" DisplayMode="List" />
                                <p>
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
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
        <div>
            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="LinkButton1" PopupControlID="Panel5"
                BackgroundCssClass="modalBackground" />

            <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Height="150px">

                <div style="text-align: center">
                    <div class="col-md-12">
                        <div class="label-dynamic">
                            <label>Password changed successfully.</label>
                        </div>

                    </div>
                    <div>
                        <asp:Button ID="btnclosepop" runat="server" Text="Ok" CssClass="btn btn-primary" OnClick="btnclosepop_Click" />

                    </div>
                </div>
            </asp:Panel>
        </div>
        <!-- Modal Popup Forget Password END-->
    </form>
    <!-- Sign In Details END -->

    <%-- scripts added by gaurav--%>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>

    <%-- Disable Right Click --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body, html").on("contextmenu", function (e) {
                return false;
            });
        });
    </script>
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
