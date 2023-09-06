<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword_forcefully.aspx.cs" Inherits="changepassword_forcefully" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Terms & condition</title>

    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <!-- Font Awesome -->

    <link href="<%=Page.ResolveClientUrl("~/bootstrap/font-awesome-4.6.3/css/font-awesome.min.css")%>" rel="stylesheet" />

    <link href="plugins/newbootstrap/css/newcustom.css" rel="stylesheet" />
    <style>
        .DocumentList {
            /*overflow-x: scroll;*/
            overflow-y: scroll;
            height: 300px;
            width: 100%;
            margin-bottom: 25px;
        }

        #page-wrapper {
            margin-top: 100px;
        }

        #Panel1 {
            top: 0!important position: absolute !important;
        }
    </style>


    <!-- jQuery 3.3.1 -->
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <!-- Bootstrap 3.4.1 -->
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>


</head>
<body>


    <form id="frm1" runat="server">


        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--  <div class="container">
            <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            </nav>

        </div>--%>
        <div id="page-wrapper">
            <header class="main-header">

                <nav class="navbar navbar-expand-lg navbar-dark fixed-top">
                    <!-- Brand -->
                    <a class="navbar-brand ml-1" href="#">
                        <img id="Img1" alt="logo" runat="server" src="Images/nophoto.jpg" />
                    </a>

                </nav>
            </header>
            <section>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-12">
                            <div>
                                <div id="div1" runat="server"></div>
                                <%-- <div class="box-header with-border">
                                    <h3 class="box-title">SESSION CREATION</h3>
                                </div>--%>

                                <div class="box-body">
                                    <div class="container">
                                        <div id="divReset" runat="server" style="background-color: #fff; padding: 20px; box-shadow: rgb(0 0 0 / 20%) 0px 5px 10px; border-radius: 5px;">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class=" col-lg-12  col-md-12 col-12">
                                                        <div class="sub-heading">
                                                            <h5>Change Password</h5>
                                                        </div>
                                                    </div>
                                                    <div class=" col-lg-12  col-md-12 col-12">
                                                        <label id="lblhelp1" runat="server"></label>
                                                    </div>
                                               
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Old Password </label>
                                                        </div>
                                                        <asp:TextBox class="form-control" ID="oldpass" MaxLength="32" EnableViewState="false" placeholder="Enter Old Password" type="password" runat="server" ValidationGroup="Submit" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ErrorMessage="Enter Old Password" ControlToValidate="oldpass" ValidationGroup="Submit" Display="None" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                            TargetControlID="oldpass"
                                                            FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                            FilterMode="ValidChars"
                                                            ValidChars="@#$&*!()_+[]\/-">
                                                        </ajaxtoolkit:FilteredTextBoxExtender>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>New Password </label>
                                                        </div>
                                                        <asp:TextBox class="form-control" ID="password" MaxLength="32" placeholder="New Password" type="password" ValidationGroup="Submit" runat="server"></asp:TextBox>
                                                        <span id="result" runat="server"></span>
                                                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Enter New Password" ControlToValidate="password" ValidationGroup="Submit" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <ajaxtoolkit:PasswordStrength ID="pwdStrength" TargetControlID="password"
                                                            StrengthIndicatorType="Text"
                                                            PrefixText="Strength:"
                                                            HelpStatusLabelID="lblhelp1"
                                                            PreferredPasswordLength="8"
                                                            MinimumNumericCharacters="1"
                                                            MinimumSymbolCharacters="1"
                                                            TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                                                            TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;AverageStrength;GoodStrength;ExcellentStrength"
                                                            runat="server" />
                                                    
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Confirm Password</label>
                                                        </div>
                                                        <asp:TextBox class="form-control" ID="cpwd" MaxLength="32" placeholder="Confirm Password" type="password" ValidationGroup="Submit" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvcpwd" runat="server" ErrorMessage="Confirm New Password" ControlToValidate="cpwd" ValidationGroup="Submit" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvConfirmPassword" runat="server" ErrorMessage="New Password And Confirm password not matched." ControlToValidate="cpwd" ControlToCompare="password" ValidationGroup="Submit" Operator="Equal" Display="None" SetFocusOnError="true">
                                                        </asp:CompareValidator>
                                                        <ajaxtoolkit:PasswordStrength ID="PasswordStrength1" TargetControlID="cpwd"
                                                            StrengthIndicatorType="Text"
                                                            PrefixText="Strength:"
                                                            HelpStatusLabelID="lblhelp1"
                                                            PreferredPasswordLength="8"
                                                            MinimumNumericCharacters="1"
                                                            MinimumSymbolCharacters="1"
                                                            TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                                                            TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;AverageStrength;GoodStrength;ExcellentStrength"
                                                            runat="server" />
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divMobOtp" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <div class="d-flex">
                                                            <div>
                                                                <asp:TextBox ID="txtMobOtp" runat="server" CssClass="form-control" PlaceHolder="ENTER OTP" MaxLength="5" TabIndex="21"></asp:TextBox>
                                                            </div>

                                                            <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                TargetControlID="txtMobOtp"
                                                                FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                FilterMode="ValidChars"
                                                                ValidChars="@&*$!()_+-\/">
                                                            </ajaxtoolkit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter OTP" ControlToValidate="txtMobOtp" ValidationGroup="OTPMobVerify" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <asp:Button ID="btnMobVerify" runat="server" class="btn btn-sm btn-info" ValidationGroup="OTPMobVerify" Text="Submit" OnClick="btnMobVerify_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="OTPMobVerify" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />

                                                        </div>

                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email Id </label>
                                                        </div>



                                                        <asp:TextBox ID="txtemailid" CssClass="form-control" onkeydown="return (event.keyCode!=13);" TabIndex="10" MaxLength="64" runat="server" placeholder="Enter Email Id" class="form-control"></asp:TextBox>
                                                        <span class="input-group-addon" id="basic-addon2"><span class="glyphicon glyphicon-envelope"></span></span>
                                                        <asp:RegularExpressionValidator ID="refEmail" runat="server"
                                                            ControlToValidate="txtemailid" ValidationGroup="Submit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" ErrorMessage="Please Enter Valid Email Address" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Email Id." ControlToValidate="txtemailid" ValidationGroup="Submit" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                            ControlToValidate="txtemailid" ValidationGroup="OTPEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" ErrorMessage="Please Enter Valid Email Address" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Email Id." ControlToValidate="txtemailid" ValidationGroup="OTPEmail" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>


                                                    </div>
                                                    <div class="form-group col-lg-1 col-md-6 col-12 mt-1">

                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnEmailOTP" runat="server" class="btn btn-sm btn-primary" ValidationGroup="OTPEmail" Text="Verify" OnClick="btnEmailOTP_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="OTPEmail" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />


                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblMobile" runat="server" CssClass="control-label" Text="Mobile"></asp:Label>
                                                            <span style="color: #FF0000; font-weight: bold">*</span>
                                                        </div>
                                                        <div class="d-flex">
                                                            <div>
                                                                <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control" PlaceHolder="Enter 10 Digit Mobile Number" MaxLength="10" TabIndex="21"></asp:TextBox>
                                                                <span class="input-group-addon"><span class="glyphicon glyphicon-phone"></span></span>
                                                            </div>

                                                            <ajaxtoolkit:FilteredTextBoxExtender ID="ftbeMobile" runat="server"
                                                                TargetControlID="txtmobile"
                                                                FilterType="Custom,Numbers"
                                                                FilterMode="ValidChars"
                                                                ValidChars="">
                                                            </ajaxtoolkit:FilteredTextBoxExtender>
                                                            <asp:Button ID="btnMobOTP" runat="server" class="btn btn-sm btn-primary" ValidationGroup="OTPMob" Text="Verify" OnClick="btnMobOTP_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="OTPMob" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />

                                                        </div>
                                                    </div>


                                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divOtpEmail" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <div class="d-flex">
                                                            <div>
                                                                <asp:TextBox ID="txtOtpEmail" runat="server" CssClass="form-control" PlaceHolder="ENTER OTP" MaxLength="5" TabIndex="21"></asp:TextBox>
                                                            </div>

                                                            <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                TargetControlID="txtOtpEmail"
                                                                FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                                                FilterMode="ValidChars"
                                                                ValidChars="@&*$!()_+-\/">
                                                            </ajaxtoolkit:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter OTP." ControlToValidate="txtOtpEmail" ValidationGroup="OTPEmailVerify" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                            <asp:Button ID="btnVerifyEmail" runat="server" class="btn btn-sm btn-info" ValidationGroup="OTPEmailVerify" Text="Submit" OnClick="btnVerifyEmail_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="OTPEmailVerify" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />

                                                        </div>
                                                    </div>

                                                 
                                                <div class="btn-footer col-12">

                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnClose_Click" />
                                                    <asp:Button ID="btnHome" runat="server" Text="Back to Login" CssClass="btn btn-warning" OnClick="btnHome_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />

                                                </div>
                                            </div>
                                        </div>

                                

                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </section>

        </div>



    </form>

</body>
</html>
