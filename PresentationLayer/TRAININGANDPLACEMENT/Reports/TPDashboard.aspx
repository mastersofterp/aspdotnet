<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TPDashboard.aspx.cs" Inherits="TPDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script src="https://code.highcharts.com/highcharts.src.js"></script>
    <script src="http://code.highcharts.com/highcharts-3d.js"></script>
<%--    <script src="https://code.highcharts.com/modules/exporting.js"></script>--%>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/json2/20130526/json2.min.js"></script>
    <%-- <script src="../../Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>--%>
    <%--  <script src="../../Scripts/highcharts.js" type="text/javascript"></script>--%>
     <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
        media="screen" />
    <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.css'
        media="screen" />
         <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
    <%-- <script>
        setInterval(function () { blink() }, 1500);
        function blink() {
            $(".sss").fadeTo(100, 0.lblDesignation1).fadeTo(200, 1.0);
        }
    </script>--%>

     <script type="text/javascript">
         function PrintDivContent(strid) {
             var prtContent = document.getElementById(strid);
             var WinPrint =
             window.open('', '', 'letf=0,top=0,toolbar=0,width=1100,height=700,sta­tus=0');
             WinPrint.document.write(prtContent.innerHTML);
             WinPrint.document.close();
             WinPrint.focus();
             WinPrint.print();
         }
    </script>
      <script>
          $(document).ready(function () {


              var element = $("#html-content-holder"); // global variable
              var getCanvas; // global variable

              $("#btn-Preview-Image").on('click', function () {
                  html2canvas(element, {
                      onrendered: function (canvas) {
                          $("#previewImage").append(canvas);
                          getCanvas = canvas;
                      }
                  });
              });

              $("#btn-Convert-Html2Image").on('click', function () {
                  var imgageData = getCanvas.toDataURL("image/png");
                  // Now browser starts downloading it instead of just showing it
                  var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
                  $("#btn-Convert-Html2Image").attr("download", "your_pic_name.png").attr("href", newData);
              });

          });

    </script>
    <script>
        $('#button').click(function () {
            chart.exportChart();
        });
        </script>
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

    <style>
        .info-box, .box
        {
            -webkit-animation: fadein 4s; /* Safari, Chrome and Opera > 12.1 */
            -moz-animation: fadein 4s; /* Firefox < 16 */
            -ms-animation: fadein 4s; /* Internet Explorer */
            -o-animation: fadein 4s; /* Opera < 12.1 */
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
            background-image: -webkit-linear-gradient(top, whiteffd, #eef2f5);
            background-image: -moz-linear-gradient(top, whiteffd, #eef2f5);
            background-image: -o-linear-gradient(top, whiteffd, #eef2f5);
            background-image: linear-gradient(to bottom, whiteffd, #eef2f5);
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
                transform: scaleX(1.25) scaleY(0.75);
            }

            40%
            {
                transform: scaleX(0.75) scaleY(1.25);
            }

            60%
            {
                transform: scaleX(1.15) scaleY(0.85);
            }

            100%
            {
                transform: scale(1);
            }
        }

    </style>
    <style>
        .carousel-fade .carousel-inner .item
        {
            -webkit-transition-property: opacity;
            transition-property: opacity;
        }

        .carousel-fade .carousel-inner .item,
        .carousel-fade .carousel-inner .active.left,
        .carousel-fade .carousel-inner .active.right
        {
            opacity: 0;
        }

        .carousel-fade .carousel-inner .active,
        .carousel-fade .carousel-inner .next.left,
        .carousel-fade .carousel-inner .prev.right
        {
            opacity: 1;
        }

            .carousel-fade .carousel-inner .next,
            .carousel-fade .carousel-inner .prev,
            .carousel-fade .carousel-inner .active.left,
            .carousel-fade .carousel-inner .active.right
            {
                left: 0;
                -webkit-transform: translate3d(0, 0, 0);
                transform: translate3d(0, 0, 0);
            }

        .carousel-fade .carousel-control
        {
            z-index: 2;
        }
    </style>
    <style type="text/css">
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
            box-shadow: 0 1px 2px rgba(0, 0, 0, 0.15);
            -webkit-transition: all 0.6s cubic-bezier(0.00, 0.84, 0.44, 1);
            transition: all 0.2s cubic-bezier(0.00, 0.00, 0.00, 1);
        }

            .wrong-item:hover,
            .wrong-item:active
            {
                box-shadow: 0 3px 10px rgba(0, 0, 0, 0.1);
                /*-webkit-transform: scale(1.00, 1.25);*/
                transform: scale(1.20, 1.05);
            }




        .bot
        {
            box-shadow: 0 -5px 5px -5px #333;
        }

        .l
        {
            width: 525px;
            height: 2px;
            background: black;
            transition: width 1s;
            -webkit-transition: width 1s; /* Safari 3.1 to 6.0 */
        }
        .breadcrumb-menu {
            display: none;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {


            $("#one").hover(function () {
                $("#t").css("width", "0px");
            }, function () {

                $("#t").css("width", "525px");
            });

            //-----------
            $("#one1").hover(function () {
                $("#a").css("width", "0px");
            }, function () {
                $("#a").css("width", "525px");
            });

            $("#one2").hover(function () {
                $("#b").css("width", "0px");
            }, function () {
                $("#b").css("width", "525px");
            });
            $("#one3").hover(function () {
                $("#c").css("width", "0px");
            }, function () {
                $("#c").css("width", "525px");
            });
            $("#one4").hover(function () {
                $("#d").css("width", "0px");
            }, function () {
                $("#d").css("width", "525px");
            });
            $("#one5").hover(function () {
                $("#e").css("width", "0px");
            }, function () {
                $("#e").css("width", "525px");
            });
            $("#one6").hover(function () {
                $("#f").css("width", "0px");
            }, function () {
                $("#f").css("width", "525px");
            });
            $("#one7").hover(function () {
                $("#g").css("width", "0px");
            }, function () {
                $("#g").css("width", "525px");
            });
            $("#one8").hover(function () {
                $("#h").css("width", "0px");
            }, function () {
                $("#h").css("width", "525px");
            });
            $("#one9").hover(function () {
                $("#i").css("width", "0px");
            }, function () {
                $("#i").css("width", "525px");
            });
            $("#one10").hover(function () {
                $("#j").css("width", "0px");
            }, function () {
                $("#j").css("width", "525px");
            });
            $("#one11").hover(function () {
                $("#k").css("width", "0px");
            }, function () {
                $("#k").css("width", "525px");
            });
            $("#one12").hover(function () {
                $("#m").css("width", "0px");
            }, function () {
                $("#m").css("width", "525px");
            });
            $("#one13").hover(function () {
                $("#n").css("width", "0px");
            }, function () {
                $("#n").css("width", "525px");
            });
            $("#one14").hover(function () {
                $("#o").css("width", "0px");
            }, function () {
                $("#o").css("width", "525px");
            });
            $("#one15").hover(function () {
                $("#p").css("width", "0px");
            }, function () {
                $("#p").css("width", "525px");
            });
            $("#one16").hover(function () {
                $("#q").css("width", "0px");
            }, function () {
                $("#q").css("width", "525px");
            });
            $("#one17").hover(function () {
                $("#r").css("width", "0px");
            }, function () {
                $("#r").css("width", "525px");
            });
            $("#one18").hover(function () {
                $("#s").css("width", "0px");
            }, function () {
                $("#s").css("width", "525px");
            });
            $("#one101").hover(function () {
                $("#pa").css("width", "0px");
            }, function () {
                $("#pa").css("width", "525px");
            });
            $("#one102").hover(function () {
                $("#pb").css("width", "0px");
            }, function () {
                $("#pb").css("width", "525px");
            });

            $("#one103").hover(function () {
                $("#pc").css("width", "0px");
            }, function () {
                $("#pc").css("width", "525px");
            });



        });



    </script>

    

<link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.min.css"/>
        <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.min.js" ></script>
          <script src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
   <%-- <script>
        $(function () {
           
            $(".beauty").draggable();
        });
    </script>--%>
    <style>
        .beauty
        {
            z-index: 90;
        }

            .beauty:hover
            {
                cursor: pointer;
            }
    </style>
    <style>
        .top
        {
            /*box-shadow: 0 -5px 15px -5px #333;*/
            box-shadow: 0 0 15px #333;
        }
    </style>

    <style>
        #myBtn
        {
            display: none;
            position: fixed;
            bottom: 60px;
            right: 30px;
            z-index: 99;
            border: none;
            outline: none;
            background-color: blue;
            color: white;
            cursor: pointer;
            padding: 15px;
            border-radius: 10px;
        }

            #myBtn:hover
            {
                background-color: #555;
            }
    </style>
    <script>
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
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;

        }
    </script>
    <%--<script>
        var canvas = document.querySelector('canvas'),
    c = canvas.getContext('2d'),
    mouseX = 0,
    mouseY = 0,
    colour = 'hotpink',
    mousedown = false;

        // resize the canvas
        canvas.width = width;
        canvas.height = height;

        function draw() {
            if (mousedown) {
                // set the colour
                c.fillStyle = colour;
                // start a path and paint a circle of 20 pixels at the mouse position
                c.beginPath();
                c.arc(mouseX, mouseY, 10, 0, Math.PI * 2, true);
                c.closePath();
                c.fill();
            }
        }

        // get the mouse position on the canvas (some browser trickery involved)
        canvas.addEventListener('mousemove', function (event) {
            if (event.offsetX) {
                mouseX = event.offsetX;
                mouseY = event.offsetY;
            } else {
                mouseX = event.pageX - event.target.offsetLeft;
                mouseY = event.pageY - event.target.offsetTop;
            }
            // call the draw function
            draw();
        }, false);

        canvas.addEventListener('mousedown', function (event) {
            mousedown = true;
        }, false);
        canvas.addEventListener('mouseup', function (event) {
            mousedown = false;
        }, false);

        var link = document.createElement('a');
        link.innerHTML = 'download image';
        link.addEventListener('click', function (ev) {
            link.href = canvas.toDataURL();
            link.download = "mypai nting.png";
        }, false);
        document.body.appendChild(link);

    </script>--%>

    <span onclick="topFunction()" id="myBtn" title="Go to top">Top</span>
    
    <div class="row">

        <div class="marquee" id="divmarquee" runat="server" visible="false">
            <p id="pmarq" runat="server"></p>
        </div>
        <br />
        <div class="row">
         <div class="col-md-4 col-sm-4 col-xs-12 san animated rubberBand beauty" id="RegisteredStudent" runat="server" visible="false">
                <div class="info-box bg-aqua wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-users"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">
                            TP Registered Students</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblRegiteredStudent" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-12 san animated rubberBand beauty" id="MaleStudent" runat="server" visible="false">
                <div class="info-box bg-green wrong-item item bot" id="test">
                    <span class="info-box-icon"><i class="fa fa-male"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text"> Male <br/>Student's</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblMaleStudent" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>

           <div class="col-md-4 col-sm-4 col-xs-12 san animated rubberBand beauty" id="FemaleStudent" runat="server" visible="false">
                <div class="info-box bg-red wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text"> Female <br/>Student's</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblFemaleStudent" runat="server"></asp:Label></b></span>

                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>

        </div>
         <asp:Label ID="lblpopup" runat="server" style="display:none"></asp:Label>
        <div class="col-md-6 ">

      
              <div class="col-md-12 beauty" id="ApproveandReject" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one14">
                    <div id="o" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Approved/Rejected Student Count</h3>
                         <span> <asp:ImageButton ID="btnregistrationcount" runat="server" ToolTip="Export to Excel"  OnClick="btnregistrationcount_Click" style="width:30px;height:25px;margin-left:20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                        <img src="/IMAGES/check1.jpg" title="Download Image" style="width: 30px; height: 25px; margin-top: -18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div20');" />

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <div id="Div20" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div21" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div20" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div20" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
            </div>
          </div>
       
         <%--   <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="ugmale" runat="server" visible="false">
                <div class="info-box bg-green wrong-item item bot" id="test">
                    <span class="info-box-icon"><i class="fa fa-male"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text"> Male <br/>Student's</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblUGTotalMale" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>--%>
            
 <%--<div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="pgfemale" runat="server" visible="false">
                <div class="info-box bg-red wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text">Total Female <br/>Student's</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblPGTotalFemale" runat="server"></asp:Label></b></span>

                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>--%>
     
<%--<div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="ugfemale" runat="server" visible="false">
                <div class="info-box bg-red wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text"> Female <br/>Student's</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblUGTotalFemale" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>--%>

     

            <%--<script>


                $("#Div_chartug").geomap("zoom", 1)
                $("#Div_chartug").geomap("zoom", -2)
            </script>--%>
 

            <%-- <div class="col-md-12 beauty" id="Div11" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div12">
                    <div id="Div13" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Admitted Students Percentage: Degree Wise</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div22" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div23" style="text-align: center;margin-bottom:-30px" class="box-body">
                                     <asp:Literal ID="Literal10" runat="server" ></asp:Literal>
                               

                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal11" runat="server" ></asp:Literal>

                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div22" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div22" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>--%>
     
       
        

           



     <%--   <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="pgfemale" runat="server" visible="false">
                <div class="info-box bg-red wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text"> Female <br/>Student's</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblPGTotalFemale" runat="server"></asp:Label></b></span>

                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>--%>

   

  <%--  <div class="col-md-12 beauty" id="Div29" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div30">
                    <div id="Div35" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Admission Registration Count</h3>--%>
                      <%--  <span> <asp:ImageButton ID="btnregistrationcount" runat="server" ToolTip="Export to Excel" OnClick="btnregistrationcount_Click" style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="IMAGES/ICON_IMG.png" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_carousel');" />--%>

<%--                        <span>     </span>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <div id="Div36" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div37" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal22" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal23" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div20" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div20" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
            </div>--%>
        
            

                   

                <%-- <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="btn btn-warning"
                                 OnClientClick="javascript:PrintDivContent('ctl00_ContentPlaceHolder1_ugstatewise');">PRINT RECEIPT</asp:LinkButton>--%>
           

                 
        <div class="col-md-6">
            <div class="col-md-12 beauty" id="DriveappliedandPlaced" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one101">
                    <div id="pa" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Drive Applied/Placed student</h3>
                        <span><asp:ImageButton ID="btnTpStudentdetails" runat="server" ToolTip="Export to Excel"  OnClick="btnTpStudentdetails_Click" Style="width:30px;height:25px;margin-left:20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                        <img src="/IMAGES/check1.jpg" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div27');"/>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div27" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div28" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal4" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div21" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div21" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>

       
        </div>
    </div>

     <div class="row">

        <div class="marquee" id="StudentPlacedCompanyWise" runat="server" visible="false">
            <p id="p1" runat="server"></p>
        </div>
        
         <asp:Label ID="Label1" runat="server" style="display:none"></asp:Label>   
           <div class="col-md-6">
            <div class="col-md-12 beauty" id="Div2" runat="server" >
                <div class="box box-primary wrong-item item top" id="Div3">
                    <div id="Div4" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Student Placed Company Wise</h3>
                        <span><asp:ImageButton ID="btnStudentPlacedCompanyWise" runat="server" ToolTip="Export to Excel" OnClick="btnStudentPlacedCompanyWise_Click" Style="width:30px;height:25px;margin-left:20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                        <img src="/IMAGES/check1.jpg" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div5');"/>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div5" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div6" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal6" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div22" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div22" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>    
        </div>

         <div class="col-md-6">
            <div class="col-md-12 beauty" id="Scheduleasperbranchwise" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div8">
                    <div id="Div9" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Branch Wise Company Count</h3>
                        <span><asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Export to Excel" OnClick="ImageButton2_Click" Style="width:30px;height:25px;margin-left:20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                        <img src="/IMAGES/check1.jpg" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div10');"/>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div10" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div11" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal7" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal8" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div23" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div23" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>    
        </div>

          <%--<div class="col-md-6">
            <div class="col-md-12 beauty" id="SchedulePlacedunplacedbothstatusforUG" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div7">
                    <div id="Div12" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Schedule Count As Per Placed/Unplaced/Both Status &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; For UG Student</h3>
                        <span><asp:ImageButton ID="btnPlaceUnplaceStatusForUG" runat="server"  ToolTip="Export to Excel"   Style="width:30px;height:25px;margin-left:20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                        <img src="../../IMAGES/check1.jpg" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div13');"/>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div13" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div14" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal9" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal10" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div24" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div24" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>    
        </div>--%>

        <%--   <div class="col-md-6">
            <div class="col-md-12 beauty" id="SchedulePlacedunplacedbothstatusforPG" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div15">
                    <div id="Div16" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Schedule Count As Per Placed/Unplaced/Both Status &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; For PG Student</h3>
                        <span><asp:ImageButton ID="btnPlaceUnplaceStatusForPG"  runat="server" ToolTip="Export to Excel"   Style="width:30px;height:25px;margin-left:20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                        <img src="../../IMAGES/check1.jpg" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div17');"/>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div17" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div18" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal11" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal12" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div25" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div25" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>    
        </div>--%>

          <div class="col-md-6">
            <div class="col-md-12 beauty" id="Companywiseactiveinactivestatus" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div19">
                    <div id="Div22" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Company Wise Approved/Pending Status Count</h3><br />
                        <span><asp:ImageButton ID="btnCompanystatus"  runat="server" ToolTip="Export to Excel"  OnClick="btnCompanystatus_Click" Style="width:30px;height:25px;margin-left:20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                        <img src="/IMAGES/check1.jpg" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div23');"/>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div23" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div24" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal13" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal14" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div26" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div26" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>    
        </div>

      </div>



      <div id="divScript" runat="server" />
  <%--  <style style="text/css">
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
                /* Starting position */
                -moz-transform: translateX(50%);
                -webkit-transform: translateX(50%);
                transform: translateX(50%);
                /* Apply animation to this element */
                -moz-animation: scroll-left 15s linear infinite;
                -webkit-animation: scroll-left 15s linear infinite;
                animation: scroll-left 15s linear infinite;
                -moz-animation: bouncing-text 15s linear infinite alternate;
                -webkit-animation: bouncing-text 15s linear infinite alternate;
                animation: bouncing-text 15s linear infinite alternate;
            }
        /* Move it (define the animation) */
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
                -moz-transform: translateX(50%); /* Browser bug fix */
                -webkit-transform: translateX(50%); /* Browser bug fix */
                transform: translateX(50%);
            }

            100%
            {
                -moz-transform: translateX(-50%); /* Browser bug fix */
                -webkit-transform: translateX(-50%); /* Browser bug fix */
                transform: translateX(-50%);
            }
        }
        /* Move it (define the animation) */
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
                -moz-transform: translateX(100%); /* Browser bug fix */
                -webkit-transform: translateX(100%); /* Browser bug fix */
                transform: translateX(100%);
            }

            100%
            {
                -moz-transform: translateX(-100%); /* Browser bug fix */
                -webkit-transform: translateX(-100%); /* Browser bug fix */
                transform: translateX(-100%);
            }
            /* Apply animation to this element */
        }
    </style>--%>


</asp:Content>

