<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnderConstruction.aspx.cs" Inherits="UnderConstruction" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>WebPortal Under Maintenance</title>

    <!-- Bootstrap -->
    <link href="<%=Page.ResolveClientUrl("plugins/newbootstrap/css/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css")%>" rel="stylesheet" />

    <style>
        /*body {
            background: #dde1ff;
        }

        .bg-maintain {
            padding: 15px;
            background: #fff;
            border-radius: 10px;<img src="Images/Login/maintenance.png" />
        }*/
        .navbar-brand img {
            height: 50px;
            width: auto;
        }
        .lbl-clg {
            color: #fff;
        }
        body {
            background: #000 url("<%=Page.ResolveClientUrl("Images/Login/maintenance.png")%>") no-repeat center center fixed;
            background-size: cover;
            background-attachment: fixed;
            background-blend-mode: luminosity;
        }

        /*Make a square*/
        .container {
            position: absolute;
            top: 80px;
            left: 100px;
            right: 100px;
            bottom: 80px;
            background: url("<%=Page.ResolveClientUrl("Images/Login/maintenance.png")%>");
            background-attachment: fixed;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            box-shadow: 0 50px 50px rgba(0, 0, 0, 0.9), 0 0 0 100px rgba(0, 0, 0, 0.1);
        }


        .countdown {
            display: flex;
            margin-top: 50px;
        }

            .countdown div {
                position: relative;
                width: 85px;
                height: 85px;
                line-height: 85px;
                text-align: center;
                background: #000000;
                color: #fff;
                margin: 0 15px;
                font-size: 2.5em;
                font-weight: 500;
            }

                .countdown div:before {
                    content: "";
                    position: absolute;
                    bottom: -30px;
                    left: 0;
                    width: 100%;
                    height: 35px;
                    background: #ffce00;
                    color: #333;
                    font-size: 0.35em;
                    line-height: 35px;
                    font-weight: 300;
                }

                .countdown div#day:before {
                    content: "Days";
                }

                .countdown div#hour:before {
                    content: "Hours";
                }

                .countdown div#minute:before {
                    content: "Minutes";
                }

                .countdown div#second:before {
                    content: "Seconds";
                }

        @media (min-width:1440px) {
            .bg-maintain {
                margin-top: 50px !important;
            }

            .img-gif {
                height: 350px;
            }
        }

        @media (min-width:576px) and (max-width:991px) {
            .container {
                position: absolute;
                top: 80px;
                left: 25px;
                right: 25px;
                bottom: 80px;
            }
        }

        @media (max-width:575px) {
            .container {
                position: absolute;
                top: 25px;
                left: 0px;
                right: 0px;
                bottom: 25px;
            }

            h1 {
                font-size: 2rem;
            }

            .countdown {
                display: flex;
                margin-top: 30px;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid pl-0 pr-0">
            <header class="main-header">

                <nav class="navbar navbar-expand-lg navbar-light fixed-top new-nav-container p-0">
                    <!-- Brand -->
                    <a class="navbar-brand ml-1 ml-md-3" href="#">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/nophoto.jpg" />
                        <asp:Label id="lblclgName" CssClass="lbl-clg" runat="server"></asp:Label>
                    </a>
                </nav>
            </header>
        </div>
        <div class="container">

            <div class="col-12 mt-5">
                <div class="row bg-maintain">
                    <div class="col-lg-5 col-md-12 col-12 pl-lg-4">
                        <h1 class="mt-4 text-white">WebPortal Under Maintenance</h1>
                        <p class="mt-3 text-white">
                            We are Improving our WebPortal
                                <br />
                            We will be back with new changes in 
                        </p>
                        <div class="text-center text-lg-left">
                            <div class="countdown">
                                <div id="day" class="d-none">na</div>
                                <div id="hour">na</div>
                                <div id="minute">na</div>
                                <div id="second">na</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-7 col-md-12 col-12 mt-5 mt-lg-3">
                        <img src="<%=Page.ResolveClientUrl("Images/Login/siteunderconstruction.gif")%>" class="w-100 img-gif" alt="maintenance" />
                        <asp:HiddenField ID="hdfdate" runat="server" />
                        <asp:HiddenField ID="hdfEndTime" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Script -->
    <script src="<%=Page.ResolveClientUrl("plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("plugins/newbootstrap/js/bootstrap.min.js")%>"></script>

    <!-- Countdown Script -->
    <script>

        //2023-02-06 17:22:55.087
        // var countDate = new Date("Feb 06, 2023 17:15:00").getTime();

        var getDate = $('#hdfdate').val();// start time

        var sdate = new Date(getDate).getTime();
        var startDate = new Date(sdate);
  
        var timeSpan = parseInt($('#hdfEndTime').val());//;
        console.log(timeSpan);
        // var endTime = $('#hdfEndTime').val()
        var endTime = startDate.setMinutes(startDate.getMinutes() + timeSpan);
     //  var endTime = startDate.setHours(startDate.getHours() + timeSpan);
        //hdfEndTime
        console.log(endTime);
        
        
        //var countDate = new Date(getDate).getTime();
        var countDate = new Date(endTime).getTime();
        
        console.log(new Date(countDate));
        function newYear() {
            var now = new Date().getTime();
            gap = countDate - now;

            var second = 1000;
            var minute = second * 60;
            var hour = minute * 60;
            var day = hour * 24;

            var d = Math.floor(gap / day);
            var h = Math.floor((gap % day) / hour);
            var m = Math.floor((gap % hour) / minute);
            var s = Math.floor((gap % minute) / second);

            document.getElementById("day").innerText = d;
            document.getElementById("hour").innerText = h;
            document.getElementById("minute").innerText = m;
            document.getElementById("second").innerText = s;

            if (h <= "0" && m <= "0" && s <= "5") {
                //<a href="default.aspx">default.aspx</a>
                //var url = "mitaoeuat.mastersofterp.in";
                // window.location.href = "https://"+url+"";
                debugger;
                // var flag = 1;
                var obj = {};
                obj.flag = 1;
                $.ajax({
                    type: "POST",
                    // url: "< %Page.ResolveClientUrl("UnderConstruction.aspx/UpdateMaintenance");%>",
                    url: "<%=Page.ResolveClientUrl("~/UnderConstruction.aspx/UpdateMaintenance")%>",
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var data = response.d;
                        // console.log(data);
                        OnSuccess(data)
                    },
                });
                function OnSuccess(data) {
                    var value = parseInt(data)
                    // alert(value);
                    console.log(value);
                    window.location.href = "<%=Page.ResolveClientUrl("default.aspx")%>";
                }

            }
        }

        setInterval(function () {
            newYear();
        }, 1000);

    </script>
</body>
</html>
