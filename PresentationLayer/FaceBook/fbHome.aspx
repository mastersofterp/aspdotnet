<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fbHome.aspx.cs" Inherits="FaceBook_fbHome" MasterPageFile="~/SiteMasterPage.master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<link href="bootstrap/css/sweetalert.min.css" rel="stylesheet" />--%>
    <%--<link href="https://fonts.googleapis.com/css?family=PT+Sans" rel="stylesheet" />--%>

    <style type="text/css">
        body
        {
            padding: 0 !important;
            margin: 0 !important;
        }

        .breadcrumb-menu
        {
            display: none;
        }

        .mytable.table
        {
            margin-bottom: 0;
            font-family: 'PT Sans', sans-serif;
        }

        .mytable td
        {
            position: relative;
            padding: 0px;
        }

        .mytable.table td
        {
            background-color: transparent !important;
            text-decoration: none !important;
            border-color: #ccc;
        }

            .mytable.table td span.fa.fa-calendar-check-o
            {
                position: absolute;
                left: 12.5%;
                top: 11.5%;
                transform: translateX(-30%);
                font-size: 120%;
                display: table;
                margin: 0;
            }

            .mytable.table td a span:nth-child(2)
            {
            }

            .mytable.table td span:nth-child(1)
            {
                position: static;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                font-weight: 500;
                font-size: small;
                /*text-decoration: line-through;*/
            }

            .mytable.table td .fa
            {
                position: absolute;
                top: 5.5%;
                right: 14%;
            }

            .mytable.table td span + span.fa.fa-unlock
            {
                right: 8%;
            }

            .mytable.table td a
            {
                display: block;
                position: absolute;
                top: 3px;
                bottom: 0;
                left: 0;
                right: 0;
                z-index: 0;
            }

        .mytable.table tr:nth-child(1) td a::before
        {
            display: none !important;
        }

        .mytable.table td a::before
        {
            content: '';
            display: block;
            position: absolute;
            height: 27px;
            width: 27px;
            top: -3%;
            border-radius: 50%;
            left: 39%;
            transform: scale(0);
            background-color: rgba(0,0,0,0.08);
            padding: 5px;
            z-index: -1;
        }

        .mytable.table td:hover a::before
        {
            background-color: rgba(0,0,0,0.15);
            transform: scale(1);
            transition: .2s;
        }

        .mytable.table td a + span
        {
            position: static;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            font-size: small;
            font-weight: 600;
        }

        .mytable.table td span:nth-child(1), .mytable.table td .fa, .mytable.table td a + span
        {
            color: #555;
        }

        .mytable.table > tbody > tr:nth-child(1) > td
        {
            background-color: #3c8dbc !important;
            font-size: 125%;
            font-weight: 600;
        }

            .mytable.table > tbody > tr:nth-child(1) > td, .mytable.table > tbody > tr:nth-child(1) > td a
            {
                color: #FFF !important;
                font-weight: 300;
            }

        .table-bordered
        {
            /*border: 1px solid #ddd !important;*/
            border-color: #ccc;
        }

        .box
        {
            box-shadow: none;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td
        {
            /*border: 1px solid #ccc;*/
        }

        table.mytable
        {
            border-collapse: collapse;
            /*border-radius: 1em;*/
            overflow: hidden;
            box-shadow: 0px 2px 3px rgba(0,0,0,0.3);
        }

        .mytable.table td span:nth-child(1).fa.fa-lock
        {
            display: none;
        }

        .mytable.table td span:nth-child(1).fa.fa-unlock
        {
            display: none;
        }

        .lbltop
        {
            margin-top: 0px;
            margin-bottom: 10px;
        }

        .txtTopClass
        {
            font-size: inherit;
            font-family: inherit;
            padding: 5px 12px;
            letter-spacing: normal;
            background: #fff !important;
            color: #3c4551;
            border-radius: 5px;
            font-weight: 400;
            border-left: 6px solid #25CD7F !important;
        }
    </style>

    <style type="text/css">
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th
        {
            padding: 3px;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td
        {
        }

        .dataTables_filter
        {
            display: none;
        }

        .mytable.table td.shifted-lecture
        {
            border-color: #ccc !important;
        }

            .mytable.table td.shifted-lecture::before
            {
                content: '';
                display: block;
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: rgba(255, 204, 102,0.4);
                height: 100%;
                z-index: 0;
            }

        .mytable.table td.today-date
        {
            /*background-color: rgba(46, 170, 8,.2) !important;*/
            background-color: #047da5 !important;
            border-color: #ccc !important;
        }
    </style>

    <style>
        .notify-table .fa.fa-unlock
        {
            color: #36c60a;
            /*opacity: .5;*/
        }

        .notify-table .fa.fa-lock
        {
            color: #d11e1e;
            /*opacity: .2;*/
        }

        .notify-table .fa.fa-calendar-check-o
        {
            color: #FF0000;
            /*opacity: .5;*/
        }

        .notify-table .fa.fa-square
        {
            color: rgba(255, 204, 102,0.4);
        }

            .notify-table .fa.fa-square.green
            {
                color: rgba(46, 170, 8,.4);
            }

        .notify-table .blue.fa.fa-calendar-check-o
        {
            color: #92F303;
        }

        .notify-table .fa-2x
        {
            font-size: 18px;
        }

        .notify-table .table-bordered > tbody > tr > td
        {
            font-size: 12px !important;
            font-weight: 500;
        }

        .notify-table .table-bordered > tbody > tr
        {
            cursor: pointer;
        }

        .notify-table .r-lecture.fa.fa-font
        {
            color: magenta;
        }
    </style>
    <style type="text/css">
        .box, .info-box
        {
            -webkit-animation: fadein 4s;
            -moz-animation: fadein 4s;
            -ms-animation: fadein 4s;
            -o-animation: fadein 4s;
            animation: fadein 4s;
        }

        @keyframes fadein
        {
            from
            {
                opacity: 0;
            }

            to
            {
                opacity: 3;
            }
        }

        @-moz-keyframes fadein
        {
            from
            {
                opacity: 0;
            }

            to
            {
                opacity: 3;
            }
        }

        @-webkit-keyframes fadein
        {
            from
            {
                opacity: 0;
            }

            to
            {
                opacity: 3;
            }
        }

        @-ms-keyframes fadein
        {
            from
            {
                opacity: 0;
            }

            to
            {
                opacity: 3;
            }
        }

        .san
        {
            background-image: -webkit-linear-gradient(top,whiteffd,#eef2f5);
            background-image: -moz-linear-gradient(top,whiteffd,#eef2f5);
            background-image: -o-linear-gradient(top,whiteffd,#eef2f5);
            background-image: linear-gradient(to bottom,whiteffd,#eef2f5);
        }

        .animated
        {
            animation-duration: 2.5s;
            animation-fill-mode: both;
            animation-iteration-count: 1;
        }

        .rubberBand
        {
            animation-name: rubberBand;
        }

        @keyframes rubberBand
        {
            0%
            {
                transform: scale(1);
            }

            30%
            {
                transform: scaleX(1.25) scaleY(.75);
            }

            40%
            {
                transform: scaleX(.75) scaleY(1.25);
            }

            60%
            {
                transform: scaleX(1.15) scaleY(.85);
            }

            100%
            {
                transform: scale(1);
            }
        }

        .carousel-fade .carousel-inner .item
        {
            -webkit-transition-property: opacity;
            transition-property: opacity;
        }

        .carousel-fade .carousel-inner .active.left, .carousel-fade .carousel-inner .active.right, .carousel-fade .carousel-inner .item
        {
            opacity: 0;
        }

        .carousel-fade .carousel-inner .active, .carousel-fade .carousel-inner .next.left,
        .carousel-fade .carousel-inner .prev.right
        {
            opacity: 1;
        }

            .carousel-fade .carousel-inner .active.left, .carousel-fade .carousel-inner .active.right, .carousel-fade .carousel-inner .next, .carousel-fade .carousel-inner .prev
            {
                left: 0;
                -webkit-transform: translate3d(0,0,0);
                transform: translate3d(0,0,0);
            }

        .carousel-fade .carousel-control
        {
            z-index: 2;
        }

        .chart
        {
            visibility: hidden;
        }

        .item
        {
            display: inline-block;
            background-color: #fff;
            border-radius: 5px;
        }

        .wrong-item
        {
            box-shadow: 0 1px 2px rgba(0,0,0,.15);
            -webkit-transition: all .6s cubic-bezier(0,.84,.44,1);
            transition: all .2s cubic-bezier(0,0,0,1);
        }

            .wrong-item:active, .wrong-item:hover
            {
                box-shadow: 0 3px 10px rgba(0,0,0,.1);
                transform: scale(1.2,1.05);
            }

        .bot
        {
            box-shadow: 0 -5px 5px -5px #333;
        }

        .l
        {
            width: 525px;
            height: 2px;
            background: #000;
            transition: width 1s;
            -webkit-transition: width 1s;
        }

        .top
        {
            box-shadow: 0 0 15px #333;
        }

        #myBtn
        {
            display: none;
            position: fixed;
            bottom: 60px;
            right: 30px;
            z-index: 99;
            border: none;
            outline: 0;
            cursor: pointer;
            color: #00569d;
        }

        .marquee
        {
            height: 40px;
            overflow: hidden;
            position: relative;
            color: red;
            font: bold;
            font-size: 16px;
        }

            .marquee p
            {
                position: absolute;
                width: 100%;
                height: 100%;
                margin: 0;
                line-height: 30px;
                text-align: center;
                -moz-transform: translateX(50%);
                -webkit-transform: translateX(50%);
                transform: translateX(50%);
                -moz-animation: scroll-left 15s linear infinite;
                -webkit-animation: scroll-left 15s linear infinite;
                animation: scroll-left 15s linear infinite;
                -moz-animation: bouncing-text 15s linear infinite alternate;
                -webkit-animation: bouncing-text 15s linear infinite alternate;
                animation: bouncing-text 15s linear infinite alternate;
            }

        @-moz-keyframes bouncing-text
        {
            0%
            {
                -moz-transform: translateX(50%);
            }

            100%
            {
                -moz-transform: translateX(-50%);
            }
        }

        @-webkit-keyframes bouncing-text
        {
            0%
            {
                -webkit-transform: translateX(50%);
            }

            100%
            {
                -webkit-transform: translateX(-50%);
            }
        }

        @keyframes bouncing-text
        {
            0%
            {
                -moz-transform: translateX(50%);
                -webkit-transform: translateX(50%);
                transform: translateX(50%);
            }

            100%
            {
                -moz-transform: translateX(-50%);
                -webkit-transform: translateX(-50%);
                transform: translateX(-50%);
            }
        }

        @-moz-keyframes scroll-left
        {
            0%
            {
                -moz-transform: translateX(100%);
            }

            100%
            {
                -moz-transform: translateX(-100%);
            }
        }

        @-webkit-keyframes scroll-left
        {
            0%
            {
                -webkit-transform: translateX(100%);
            }

            100%
            {
                -webkit-transform: translateX(-100%);
            }
        }

        @keyframes scroll-left
        {
            0%
            {
                -moz-transform: translateX(100%);
                -webkit-transform: translateX(100%);
                transform: translateX(100%);
            }

            100%
            {
                -moz-transform: translateX(-100%);
                -webkit-transform: translateX(-100%);
                transform: translateX(-100%);
            }
        }
    </style>

    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            width: 79%;
            border: 3px solid #3c8dbc;
        }
            /*#0DA9D0*/

            .modalPopup .header
            {
                background-color: #3c8dbc;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }
            /*#2FBDF1*/

            .modalPopup .body
            {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
            }

            .modalPopup .footer
            {
                padding: 3px;
            }

            .modalPopup .button
            {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

            .modalPopup td
            {
                text-align: left;
            }
    </style>
  <%--   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
</asp:ScriptManager>--%>
    <%--<script src="bootstrap/js/sweetalert.min.js"></script>--%>
    <script type="text/javascript">
        function functionConfirm() {
            var labelText = document.getElementById("ctl00_ContentPlaceHolder1_lblpopup").innerHTML;
            swal({
                // title: "Hi Every one!!",
                text: labelText,
                confirmButtonColor: 'green',
                type: "info",
                //  timer: 2000
                // imageUrl: 'thumbs-up.jpg'
            });
        }
    </script>
    <script type="text/javascript">
        //function openModal() {
        $(document).ready(function () {
            $('#B.Tech').click(function () {
                alert("Test");
                //}
            });
        });
    </script>
    <script type="text/javascript">
        // When the user scrolls down 20px from the top of the document, show the button
        window.onscroll = function () { scrollFunction() };

        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                document.getElementById("myBtn").style.display = "block";
            }
            else {
                document.getElementById("myBtn").style.display = "none";
            }
        }

        // When the user clicks on the button, scroll to the top of the document
        function topFunction() {
            //document.body.scrollTop = 0;
            //document.documentElement.scrollTop = 0;
            return $("html, body").animate({
                scrollTop: 0
            }, 1000), !1

        }
    </script>
    <script type="text/javascript">
        function SubmitSearch() {
            try {
                __doPostBack(UpdateToken);
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }
    </script>
    
    
    
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <span onclick="topFunction()" id="myBtn" title="Go to top"><i class="fa fa-chevron-circle-up fa-3x" aria-hidden="true"></i></span>
    <asp:UpdatePanel runat="server" ID="updatePanel1">
        <ContentTemplate>
            <div class="row">

                <div class="marquee" id="divmarquee" runat="server" visible="false">
                    <p id="pmarq" runat="server"></p>
                </div>
                <asp:Label ID="lblpopup" runat="server" Style="display: none"></asp:Label>

                <div id="divNews" runat="server" class="col-md-6">
                    <div class="col-md-12 beauty">
                        <div class="box box-primary wrong-item item top" id="one9">
                            <div id="i" class="l"></div>
                            <div class="box-header with-border">
                                <i class="fa fa-newspaper-o"></i>

                                <h3 class="box-title">Latest News</h3>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <p><%=sMarquee%></p>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divNotices" class="col-md-6" runat="server">
                    <div class="col-md-12 beauty">
                        <div class="box box-primary wrong-item item top">
                            <div class="box-header with-border">
                                <i class="fa fa-list"></i>
                                <h3 class="box-title">Latest Notices</h3>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <div id="chartHere">
                                </div>
                                <div id="chartHere1">
                                </div>
                                <p><%=Notice%></p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4" id="divQLinks" runat="server" style="display: none;">
                    <div class="col-md-12 beauty">
                        <div class="box box-primary wrong-item item top" style="height: 376px">
                            <div class="box-header with-border">
                                <i class="fa fa-list"></i>

                                <h3 class="box-title">Quick Links</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                                </div>
                            </div>
                            <div class="box-body">

                                <div id="Div1">
                                </div>
                                <div id="Div2">
                                </div>

                                <asp:ListView ID="lvQLinks" runat="server">
                                    <LayoutTemplate>
                                        <div class="vista-grid" id="demo-grid">
                                            <table class="table table-responsive table-hover table-bordered">
                                                <tr></tr>
                                                <tr>
                                                    <td id="itemPlaceholder" runat="server"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnLink" runat="server" Enabled="false" CommandArgument='<%# Eval("al_url1") %>'><%# Eval("al_link")%></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="box box-primary ">
                        <div class="box-header with-border">
                            <strong><i class="fa fa-calendar text-blue"></i>Calendar</strong>
                        </div>
                        <div class="box-body">
                            <div class="col-md-10 col-md-offset-1" runat="server" id="divCalendar">
                                <asp:UpdatePanel runat="server" ID="updatePanelClass">
                                    <ContentTemplate>
                                        <div class="box" style="border-top: none">
                                            <%--   <span class="input-group-addon"><a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">--%>
                                            <div id="calDiv" class="table-responsive">

                                                <div id="Cal" runat="server">

                                                    <asp:Calendar ID="Calendar1" runat="server" BackColor="#129ed9" NextPrevFormat="FullMonth"
                                                        Width="100%" CellSpacing="1" OnDayRender="Calendar1_DayRender" DayStyle-Height="70px"
                                                        CssClass="mytable table table-responsive table-bordered">
                                                        <SelectedDayStyle BackColor="#CCFFCC" ForeColor="#003300" />
                                                        <TodayDayStyle BackColor="#ffffff" ForeColor="White" Font-Bold="true" BorderColor="green"
                                                            BorderStyle="solid" BorderWidth="1px" />
                                                        <OtherMonthDayStyle ForeColor="#999999" />
                                                        <NextPrevStyle Font-Bold="True" Font-Size="10pt" Font-Underline="false" />
                                                        <DayHeaderStyle Font-Bold="True" Font-Names="Lucida Grande,Lucida Sans,Arial,sans-serif" Font-Size="12pt" ForeColor="White" Height="10pt" CssClass="text-center" />
                                                        <TitleStyle Height="12pt" CssClass="nav" Font-Underline="false" />
                                                        <DayStyle BackColor="White" />
                                                    </asp:Calendar>
                                                    <asp:LinkButton ID="LinkButton1" Style="display: none;" runat="server"
                                                        OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>

                                                </div>
                                            </div>
                                            <%--</span>--%>
                                        </div>

                                        <div class="clearfix"></div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="notify-table">
                                                    <table class="table table-bordered table-hover text-center">
                                                        <thead class="bg-primary">
                                                            <tr>
                                                                <th>Notation</th>
                                                                <th>Description</th>
                                                                <%-- <th>Notation</th>
                                                    <th>Description</th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <%--<tr>
                                                    <td class="text-center">
                                                        <i class="fa fa-unlock fa-2x" aria-hidden="true"></i>
                                                    </td>
                                                    <td>Unlocked Lecture Day</td>
                                                    <td class="text-center">
                                                        <i class="fa fa-lock fa-2x" aria-hidden="true"></i>
                                                    </td>
                                                    <td>Locked Lecture Day</td>
                                                </tr>--%>
                                                            <tr>
                                                                <td class="text-center">
                                                                    <i class="fa fa-calendar-check-o fa-2x" aria-hidden="true"></i>
                                                                </td>
                                                                <td><b>Holiday</b></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-center">
                                                                    <i class="blue fa fa-calendar-check-o fa-2x" aria-hidden="true"></i>
                                                                </td>
                                                                <td><b>Event</b></td>
                                                            </tr>
                                                            <%-- <tr>
                                                    <td class="text-center">
                                                        <i class="fa fa-square fa-2x" aria-hidden="true"></i>
                                                    </td>
                                                    <td>Shifted Lectures</td>
                                                    <td class="text-center">
                                                        <i class="green fa fa-square fa-2x" aria-hidden="true"></i>
                                                    </td>
                                                    <td>Current Date</td>
                                                </tr>--%>
                                                            <tr>
                                                                <td class="text-center">
                                                                    <%--<i class="fa fa-font fa-2x" aria-hidden="true"></i>--%>
                                                                    <b>Subject Codes</b>
                                                                </td>
                                                                <td><b>Regular Lectures</b></td>
                                                                <%-- <td class="text-center">
                                                       &nbsp;
                                                    </td>
                                                    <td>&nbsp;</td>--%>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div id="auth-status" style="margin-top: 15px; text-align: center;">
                                            <div id="auth-loggedout">
                                                <div class="fb-login-button" autologoutlink="true" scope="email">Login</div>
                                            </div>

                                            <div id="auth-loggedin" style="display: none">
                                                Hi, <span id="auth-displayname"></span>(<a href="LoginFb.html" id="auth-logoutlink">logout</a>)
                                            </div>
                                            <asp:TextBox runat="server" ID="authName" style="display: none"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="accessT" style="display: none"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblMsg" ForeColor="Green"></asp:Label>
                                        </div>--%>
                                    </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:PostBackTrigger ControlID="Calendar1" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>

    <%-- Model Pop up Panel --%>

    <asp:ImageButton ID="lnkFake" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Course List
        </div>
        <div class="body">
            <asp:UpdatePanel ID="updEdit" runat="server">
                <ContentTemplate>
                    <div class="col-md-12">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="None">
                            <asp:ListView ID="lstCourseList" runat="server">
                                <LayoutTemplate>
                                    <div>
                                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Course Name
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="Tr1" runat="server" />
                                                </tbody>
                                        </asp:Panel>
                                    </div>

                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkbtnCourse" runat="server" CommandArgument='<%# Eval("COURSENO")%>'
                                                OnClick="lnkbtnCourse_Click" Text='<%# GetCourseName(Eval("COURSENAME"),Eval("SCHEMENAME"),Eval("SECTION"),Eval("SUBJECTTYPE"),Eval("BATCHNAME")) %>'
                                                ToolTip='<%# Eval("SECTIONNO")%>' CommandName='<%# Eval("BATCH")%>' />
                                        </td>
                                        <asp:HiddenField ID="hdnSchemename" runat="server" Value='<%# Eval("SCHEMENAME")%>' />
                                        <asp:HiddenField ID="hdnCoursename" runat="server" Value='<%# Eval("COURSENAME")%>' />
                                        <asp:HiddenField ID="hdnBatch" runat="server" Value='<%# Eval("BATCHNAME")%>' />
                                        <asp:HiddenField ID="hdnSectionname" runat="server" Value='<%# Eval("SECTION")%>' />
                                        <asp:HiddenField ID="hdnSubjecttype" runat="server" Value='<%# Eval("SUBJECTTYPE")%>' />
                                        <asp:HiddenField ID="hdnCourseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                        <asp:HiddenField ID="hdnSectionno" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                        <asp:HiddenField ID="hdnBatchno" runat="server" Value='<%# Eval("BATCH")%>' />
                                        <asp:HiddenField ID="hdnSubId" runat="server" Value='<%# Eval("SUBID")%>' />
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" />
        </div>
    </asp:Panel>

    <%-- Model Pop up Panel --%>

    <div id="divScript" runat="server" />
    <script type="text/javascript">
        function ShowPopup(title, body) {
            alert("aaaaa");
            $("#myModal2 .modal-title").html(title);
            $("#myModal2 .modal-body").html(body);
            $("#myModal2").modal("show");
        }
    </script>

</asp:Content>
