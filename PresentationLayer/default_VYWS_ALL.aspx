<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default_VYWS_ALL.aspx.cs" Inherits="default_VYWS_ALL" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UC/feed_back.ascx" TagName="feedback" TagPrefix="uc1" %>

<!DOCTYPE html>
<html lang="en">
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
        //document.getElementById('txt_captcha').value = '';
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
    <title>VYVS || Vidarbha Youth Welfare Society's</title>
    <link rel="shortcut icon" href="Images/Login/logo_vyws.png" type="image/x-icon">

    <!-- Bootstrap -->
    <link href="plugins/newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="plugins/newbootstrap/css/login_VYWS_ALL.css" rel="stylesheet" />

    <style>
        .logo img {
            width: auto;
            height: 200px;
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
                height: 125px;
            }
        }

        @media (max-width:576px) {
            .logo img {
                width: 25%;
                height: auto;
                margin-top: 5px;
            }

            #txtcaptcha {
                width: 150px;
            }
        }
    </style>
</head>
<body>
    <div class="overlay"></div>
    <form id="frmDefault" runat="server">
        <div class="bg-video-wrap in-left a0">

            <div class="sign-in-video scrollbar" id="style-1">

                <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" EnablePageMethods="true" runat="server" ID="Script_water" />
                <div class="container-fluid force-overflow ">
                    <div class="row">
                        <div class="col-lg-6 col-xl-4 col-xl-1 col-md-8 offset-md-2 offset-lg-0 mb-0 mb-md-0 ml-lg-5 pl-lg-0 log-card">
                            <div class="side-background pt-2 pb-0 pt-sm-5 pb-sm-5 pl-lg-0">
                                <div class="text-center pt-lg-4 pb-lg-5 pt-md-2 pb-md-4">
                                    <div class="logo in-left a2">
                                        <asp:Image ID="imglogo" runat="server" ImageUrl="~/Images/Login/logo_vyws.png" />
                                        <%--<img src="Images/Login/DAIICT_logo.png"  alt="logo"/>--%>
                                    </div>
                                    <div class="college-name in-left a3">
                                        <h2 class="mt-2 mt-md-3 mb-2 mb-md-2">Vidarbha Youth Welfare Society's</h2>
                                    </div>
                                    <div class="clg-address pb-md-3 in-left a4">
                                        <p class="ml-lg-4 mr-lg-4">
                                            VYWS, Chaitanya Building, In front of Telephone Office, Camp Road Amravati – 444602
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 offset-md-0 col-lg-5 offset-lg-1 col-xl-6 offset-xl-1 mt-lg-4 pt-xl-4 mb-4 mb-md-0">

                            <div class="row tp-card">
                                <div class="col-sm-4 col-6">
                                    <a href="default_VYWS_RMCEM.aspx">
                                        <div class="login-form in-left a4" style="background: rgb(16 100 112 / 85%);">
                                            <div class="form-group mt-3">
                                                <img src="Images/Login/vyws_icon/engineering.png" />
                                                <label>Prof. Ram Meghe College of Engineering & Management Badnera - Amravati</label>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-sm-4 col-6">
                                    <a href="default_VYWS_RMCEM.aspx">
                                        <div class="login-form in-left a4" style="background: rgb(5 55 107 / 75%);">
                                            <%--#05376b--%>
                                            <div class="form-group mt-3">
                                                <img src="Images/Login/vyws_icon/university.png" />
                                                <label>Prof. Ram Meghe Institute of Technology and Research Badnera - Amravati</label>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-sm-4 col-6">
                                    <a href="default_VYWS_RMCEM.aspx">
                                        <div class="login-form in-left a4" style="background: rgb(0 83 159 / 70%);">
                                            <%--#05376b--%>
                                            <div class="form-group mt-3">
                                                <img src="Images/Login/vyws_icon/dental.png" />
                                                <label>VYWS Dental College & Hospital - Amravati</label>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-sm-4 col-6">
                                    <a href="default_VYWS_RMCEM.aspx">
                                        <div class="login-form in-left a4" style="background: rgb(40 103 167 / 80%);">
                                            <%--#05376b--%>
                                            <div class="form-group mt-3">
                                                <img src="Images/Login/vyws_icon/pharmacy.png" />
                                                <label>Institute of Pharmacy & Research Badnera - Amravati</label>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                                <div class="col-sm-4 col-6">
                                    <a href="default_VYWS_RMCEM.aspx">
                                        <div class="login-form in-left a4" style="background: rgb(11 96 173 / 75%);">
                                            <%--#05376b--%>
                                            <div class="form-group mt-3">
                                                <img src="Images/Login/vyws_icon/pharmaceutical.png" />
                                                <label>Institute Of Pharmaceutical Education & Research - Wardha</label>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
    <!-- Sign In Details END -->

    <!-- Script -->
    <script src="plugins/newbootstrap/js/jquery-3.5.1.min.js"></script>
    <script src="plugins/newbootstrap/js/popper.min.js"></script>
    <script src="plugins/newbootstrap/js/bootstrap.min.js"></script>

    <%-- Disable Right Click --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body, html").on("contextmenu", function (e) {
                return false;
            });
        });
    </script>
    <%-- X --%>

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
</body>
</html>
