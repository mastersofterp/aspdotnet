<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="home.aspx.cs" Inherits="home" Title="" %>

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
        } /*#0DA9D0*/

            .modalPopup .header
            {
                background-color: #3c8dbc;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }/*#2FBDF1*/

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

             /*-------------- START--google sign in---on Date 21/09/2020 by Deepali-------------------------*/	
        .fixed-google {	
            position:fixed;	
            left:0px;	
            top:50%;	
        }
        .abcRioButtonBlue {
            width: 100px !important;
        }
        /*----------END----google sign in----on Date 21/09/2020 by Deepali------------------------*/	
    </style>
 

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
            <%--START--google sign in---on Date 21/09/2020 by Deepali--%>
          
            <%--END--google sign in---on Date 21/09/2020 by Deepali--%>
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
                              
                            </div>
                        </div>
                    </div>
                </div>

             

            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
