<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default_VYWS_IPER.aspx.cs" Inherits="default_VYWS_IPER" %>

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
    <title>VYWS - IPER </title>
    <link rel="shortcut icon" href="Images/Login/logo_vyws.png" type="image/x-icon">

    <!-- Bootstrap -->
    <link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/css/login_VYWS_IPER.css" rel="stylesheet" />


    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700;800&display=swap" rel="stylesheet" />

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
        <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" EnablePageMethods="true" runat="server" ID="Script_water" />
        <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="90000">
            <%--OnTick="Timer1_Tick"--%>
        </asp:Timer>
        <div class="login">

            <section class="login__left">
                <div id="loginCarousel" class="carousel slide" data-ride="carousel" data-interval="4000" data-pause="hover">
                    <div class="carousel-inner" role="listbox">
                        <div class="carousel-item active ">
                            <div class="background-overlay"></div>
                            <div class="slide" style="background-image: url('Images/Login/bg_slider5.png'); background-size: cover;">
                                <%--<video autoplay="" muted="" loop="" id="myVideo">
                                    <source src="" type="video/mp4">
                                </video>--%>
                            </div>
                            <div class="carousel-caption">
                                <div class="txt-clg animationss a1">

                                    <h1 class="slide__title college-name animations a2">VYWS</h1>

                                    <p class="slide__text mb-0 animation a2">
                                        Institute Of Pharmaceutical <br />
                                        Education & Research
                                        
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section class="login__right">

                <div class="college-details text-center mt-4 mt-lg-0 mb-0">
                    <%--  <img src="Images/Login/logo_vyws.png" alt="logo" />--%>
                    <asp:Image ID="Imagelogo" runat="server" ImageUrl="~/Images/Login/logo_vyws.png" />
                </div>

                <div class="signin-details mt-2">
                    <h5 class="text-center">Vidarbha Youth Welfare Society's </h5>
                      <h3 class="text-center mb-4">Institute Of Pharmaceutical Education & Research - Wardha
                    </h3>
                    <h4 class="heading mb-3">Sign In</h4>
                    <!-- User Name Start -->
                    <div class="floating-form">
                        <div class="floating-label">
                            <asp:TextBox ID="txt_username" runat="server" TabIndex="1" onCopy="return false" onPaste="return false" onCut="return false" CssClass="floating-input" type="text" placeholder=" "></asp:TextBox>
                            <asp:RequiredFieldValidator ID="req_username" runat="server" ControlToValidate="txt_username" ErrorMessage="Username Required !" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                            <span class="highlight"></span>
                            <label>User Name</label>
                            <i class="far fa-user"></i>
                        </div>
                    </div>
                    <!-- User Name END -->
                    <!-- Password Start -->
                    <div class="floating-form">
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
                        <div class="floating-form">
                            <div class="floating-label">
                                <asp:TextBox ID="txtcaptcha" runat="server" TabIndex="3" Style='text-transform: uppercase; padding-left: 10px; width: 135px;' ValidationGroup="login" CssClass="floating-input" type="text" placeholder=" "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server" ErrorMessage="Captcha Required !" ControlToValidate="txtcaptcha" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                <span class="highlight"></span>
                                <label>Captcha</label>
                            </div>
                        </div>
                        <div class="form-group mr-1">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="input-group">
                                        <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="4"
                                            CaptchaHeight="42" CaptchaWidth="145" CaptchaLineNoise="None" CaptchaMinTimeout="5" Font-Size="12px" BackColor="#edf8f2"
                                            NoiseColor="#D9261C" CaptchaInputCSS="BlackTextInput" EnableTheming="true" CaptchaChars="123456789" CaptchaMaxTimeout="240" FontColor="#fd7e14" class="img-responsive" />
                                        <div class="input-group-append">
                                            <span class="input-group-text">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Login/refresh-btn.png" />
                                            </span><%--OnClick="imgrefresh_Click"--%>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                    </div>
                    <!-- Captcha END -->

                    <!-- Login Button Start -->
                    <asp:LinkButton class="form-control btn btn-primary text-white pl-2" ID="btnLogin" TabIndex="4" runat="server" OnClick="btnLogin_Click" Text="Sign In" ValidationGroup="login"></asp:LinkButton>
                    <!-- Login Button END -->
                    <%--   OnClick="btnLogin_Click"--%>
                    <div style="text-align: -webkit-center; text-align: -moz-center;">
                        <asp:TextBox runat="server" ID="txtdemo" Style="display: none" />
                    </div>
                    <asp:HiddenField ID="hdfuserno" runat="server" />
                    <asp:HiddenField ID="hdffirstlog" runat="server" />
                    <asp:HiddenField ID="hdflastlogout" runat="server" />
                    <asp:HiddenField ID="hdfAllowpopup" runat="server" />

                    <div class="forget-pass text-center mt-3">
                        <div class="forget-pass">
                            <a data-toggle="modal" data-target="#ModelPopUp" id="lbtForgePass" runat="server" style="cursor: pointer; color: #00539f;"><span>Forgot Password 
                            </span></a>
                        </div>
                    </div>

                </div>

            </section>

            <div class="compatiblity">
                <div class="container-fluid">
                    <ul class="list-inline">
                        <li>Site Compatible - </li>
                        <li>
                            <asp:Image ID="Imagechrome" runat="server" ImageUrl="~/IMAGES/chrome.png" />

                            Google Chrome 70+ ,</li>
                        <li>
                            <asp:Image ID="Imagefirefox" runat="server" ImageUrl="~/IMAGES/firefox.png" />
                            <%--<img src="" alt="firefox">--%>
                            Firefox 65+ ,</li>
                        <li>

                            <asp:Image ID="Imageedge" runat="server" ImageUrl="~/IMAGES/edge.png" />
                            Microsoft Edge 89+</li>
                    </ul>
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
                                                    <asp:TextBox ID="txtusername" runat="server" placeholder="Enter User Name" CssClass="form-control mt-1" MaxLength="25" Enabled="false"></asp:TextBox>
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
                                    <p>
                                        &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="login1" DisplayMode="List" />
                                        <p>
                                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                        </p>
                                        <p>
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
            <div class="form-group row">
                <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mdlPopupDel"
                    runat="server" TargetControlID="LinkButton1" PopupControlID="Panel5"
                    BackgroundCssClass="modalBackground" />

                <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Height="150px">

                    <div style="text-align: center">
                        <div class="col-md-12">
                            <div>

                                <div>
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <b>
                                            <label>Password changed successfully.</label></b>
                                    </div>

                                </div>
                            </div>
                            <br />

                            <div>
                                <div>
                                    <asp:Button ID="btnclosepop" runat="server" Text="Ok" CssClass="btn btn-primary" OnClick="btnclosepop_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </form>
    <!-- Script -->

    <%-- scripts added by gaurav--%>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>

    <script>

        window.onload = function () {
            document.getElementById("txt_password").focus();
            document.getElementById("txt_username").focus();
        };

    </script>
    <script>

        $("#txt_username").focus(function () {
            $(".fa-user").css("color", "#00539F");
        });
        $("#txt_username").focusout(function () {
            $(".fa-user").css("color", "#ced4da");
        });

        $("#show_hide_password .floating-input").focus(function () {
            $(".fa-eye-slash").css("color", "#00539F ");
        });
        $("#show_hide_password .floating-input").focusout(function () {
            $(".fa-eye-slash").css("color", "#ced4da");
        });

    </script>

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
