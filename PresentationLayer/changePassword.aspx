<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="changePassword.aspx.cs" Inherits="changePassword" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Bootstrap -->
    <link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="plugins/newbootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/css/login.css" rel="stylesheet" />
    <link href="plugins/virtual-keyboard/keyboard.css" rel="stylesheet" />

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPass"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <style>
        .ver-ify {
            padding-top: 40px;
        }

        #show_hide_password {
            position: relative;
        }

            #show_hide_password a, #show_hide_password a {
                color: #495057;
                position: absolute;
                top: 22%;
                right: 4%;
            }

        #show_hide_passwordnew {
            position: relative;
        }

            #show_hide_passwordnew a, #show_hide_passwordnew a {
                color: #495057;
                position: absolute;
                top: 22%;
                right: 4%;
            }

        @media (min-width:576px) and (max-width: 991px) {
            .ver-ify {
                padding-top: 0px;
                padding-left: 50px;
            }
        }

        @media (max-width:576px) {
            .ver-ify {
                padding-top: 0px;
            }
        }
    </style>

    <asp:UpdatePanel ID="updPass" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CHANGE PASSWORD</h3>
                           <%-- <div class="pull-right">
                                <div style="color: black; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                                <div style="color: black; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     : Password should be at least 10 characters,
                                        Must contain at least one  lower case letter, one upper case letter, one digit and one special character
                                </div>
                            </div>--%>
                        </div>
                        <div>
                            <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                        </div>

                        <div class="box-body">

                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note</h5>
                                    <p><i style="color: red" class="fa fa-star" aria-hidden="true"></i><span  style="font-weight:bold">Marked Fields Are Mandatory.</span> </p>
                                       <p><i style="color: red" class="fa fa-star" aria-hidden="true"></i><span style="font-weight:bold">Password Should Be At Least 10 Characters,
                                        Must Contain At Least One  Lower Case Letter, One Upper Case Letter, One Digit And One Special Character.</span> </p>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label ID="lblHelp" runat="server" />
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group col-md-4" style="display: none">
                                            <label>Password</label>
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="20" placeholder="Enter Password" TextMode="Password"
                                                Wrap="False" />

                                        </div>
                                        <div class="form-group col-md-4">
                                            <label><span style="color: red">*</span>Old Password</label>
                                            <div id="show_hide_password">

                                                <a href="#"><i class="fa fa-eye-slash" aria-hidden="true"></i></a>
                                                <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="20" placeholder="Old Password" TextMode="Password" AutoPostBack="false"
                                                    Wrap="False" />
                                                <asp:RequiredFieldValidator ID="rfvOldPass" runat="server" ControlToValidate="txtOldPassword"
                                                    Display="none" ErrorMessage="Old Password Required" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label><span style="color: red">*</span>New Password</label>
                                            <div id="show_hide_passwordnew">
                                                <a href="#"><i class="fa fa-eye-slash" aria-hidden="true"></i></a>
                                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="20" placeholder="New Password"
                                                    Wrap="False" />

                                                <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" ControlToValidate="txtNewPassword"
                                                    Display="None" ErrorMessage="New Password Required"
                                                    ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                            </div>
                                            <ajaxToolKit:PasswordStrength ID="pwdStrength" TargetControlID="txtNewPassword" StrengthIndicatorType="Text"
                                                PrefixText="Strength:" HelpStatusLabelID="lblhelp1" PreferredPasswordLength="8"
                                                MinimumNumericCharacters="1" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" MinimumLowerCaseCharacters="1" RequiresUpperAndLowerCaseCharacters="true"
                                                TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                                                TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;AverageStrength;GoodStrength;ExcellentStrength"
                                                runat="server" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label><span style="color: red">*</span>Confirm Password</label>
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20" placeholder="Confirm Password"
                                                Wrap="False" />
                                            <asp:RequiredFieldValidator ID="rfvConfirmPass" runat="server" ControlToValidate="txtConfirmPassword"
                                                Display="None" ErrorMessage="Confirm Password Required"
                                                ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New &amp; Confirm Password Not Matching"
                                                ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" Display="None"
                                                ValidationGroup="changePassword"></asp:CompareValidator>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group col-md-4">
                                            <label><span style="color: red">*</span>Mobile Number</label>
                                            <asp:TextBox ID="txtMobile" runat="server" MinLength="10" MaxLength="10"
                                                Wrap="False" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobile"
                                                Display="none" ErrorMessage="Mobile Number Required" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label><span style="color: red">*</span>Email Id</label>
                                            <asp:TextBox ID="txtEmailId" runat="server" MaxLength="50" ReadOnly="true"
                                                Wrap="False" />
                                            <asp:RegularExpressionValidator ID="rfvLocalEmail" runat="server" ControlToValidate="txtEmailId"
                                                Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorMessage="Please Enter Valid EmailId"
                                                ValidationGroup="changePassword"></asp:RegularExpressionValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmailId"
                                                Display="none" ErrorMessage="Email ID Required" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                        </div>
                                        <%-- <div class="col-sm-3">
                                    <label class="ver-ify"></label>
                                    <asp:Button ID="btnEmailOTP" runat="server" class="btn btn-sm btn-primary" ValidationGroup="OTPEmail" Text="Verify" OnClick="btnEmailotp_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="OTPEmail" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />
                                </div>--%>
                                    </div>

                                    <%-- <div class="col-md-12" id="divOtpEmail" runat="server" visible="false">
                                <div class="form-group col-md-2">
                                </div>
                               <div class="form-group col-md-2">
                                </div>
                               <div class="form-group col-md-4">

                                    <div class="input-group" style="width:70%">

                                        <asp:TextBox ID="txtOtpEmail" runat="server" CssClass="form-control" PlaceHolder="ENTER OTP" MaxLength="5" TabIndex="21"></asp:TextBox>

                                    </div>

                                    <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                        TargetControlID="txtOtpEmail"
                                        FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"
                                        FilterMode="ValidChars"
                                        ValidChars="@&*$!()_+-\/">
                                    </ajaxtoolkit:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter OTP." ControlToValidate="txtOtpEmail" ValidationGroup="OTPEmailVerify" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                
                                </div>--%>
                                    <%-- <div class="form-group col-md-3">
                                    <asp:Button ID="btnVerifyEmail" runat="server" class="btn btn-sm btn-info" ValidationGroup="OTPEmailVerify" Text="Submit" OnClick="btnEmailOtpVerify_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="OTPEmailVerify" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />
                                </div>--%>
                                </div>
                            </div>
                <div class="box-footer">
                    <p>
                        <label id="lblhelp1" runat="server"></label>
                    </p>
                    <p class="text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            ValidationGroup="changePassword" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            CausesValidation="False" CssClass="btn btn-danger" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="changePassword"
                            ShowMessageBox="True" ShowSummary="False" DisplayMode="List" />
                    </p>
                    <p class="text-center">
                        <asp:Label ID="lblMessage" runat="server" SkinID="Errorlbl" Style="color: Red;"
                            Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" />
                    </p>
                    <p class="text-center">
                        <asp:LinkButton runat="server" ID="lnkback" Visible="false" Text="Click Here To Login" CssClass="btn btn-primary"
                            OnClick="lnkback_Click"></asp:LinkButton>
                    </p>
                </div>
            </div>
            </div>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function IsValid() {

            var x = document.getElementById('<%=lblhelp1.ClientID %>').innerHTML;
            if (x != "") {
                alert("Password strength must be excellent.")
                return false;
            }
            else {
                return true;
            }
        }
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
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
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

    <script>
        $(document).ready(function () {
            $("#show_hide_passwordnew a").on('click', function (event) {
                event.preventDefault();
                if ($('#show_hide_passwordnew input').attr("type") == "text") {
                    $('#show_hide_passwordnew input').attr('type', 'password');
                    $('#show_hide_passwordnew i').addClass("fa-eye-slash");
                    $('#show_hide_passwordnew i').removeClass("fa-eye");
                } else if ($('#show_hide_passwordnew input').attr("type") == "password") {
                    $('#show_hide_passwordnew input').attr('type', 'text');
                    $('#show_hide_passwordnew i').removeClass("fa-eye-slash");
                    $('#show_hide_passwordnew i').addClass("fa-eye");
                }
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("#show_hide_passwordnew a").on('click', function (event) {
                event.preventDefault();
                if ($('#show_hide_passwordnew input').attr("type") == "text") {
                    $('#show_hide_passwordnew input').attr('type', 'password');
                    $('#show_hide_passwordnew i').addClass("fa-eye-slash");
                    $('#show_hide_passwordnew i').removeClass("fa-eye");
                } else if ($('#show_hide_passwordnew input').attr("type") == "password") {
                    $('#show_hide_passwordnew input').attr('type', 'text');
                    $('#show_hide_passwordnew i').removeClass("fa-eye-slash");
                    $('#show_hide_passwordnew i').addClass("fa-eye");
                }
            });
        });
    </script>
</asp:Content>
