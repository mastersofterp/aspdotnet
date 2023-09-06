<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineAdmissionStatusChart.aspx.cs" 
    MasterPageFile="~/SiteMasterPage.master" Inherits="OnlineAdmissionStatusChart" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

     <script language="javascript" type="text/javascript">
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

        .highcharts-container
        {
        height:500px;
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


    <script src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script>
        $(function () {
            $(".beauty").draggable();
        });
    </script>
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
 

    <span onclick="topFunction()" id="myBtn" title="Go to top">Top</span>
    
    <div class="row">

        <div class="marquee" id="divmarquee" runat="server" visible="false">
            <p id="pmarq" runat="server"></p>
        </div>
        
         <asp:Label ID="lblpopup" runat="server" style="display:none"></asp:Label>
        <div class="col-md-6 ">

            <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="totalregisteredug" runat="server" visible="false">
                <div class="info-box bg-aqua wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-users"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size :11px">
                            Total Applicants<br />Registered</span>
                          <asp:Label ID="lbladmbatch1" runat="server"></asp:Label>
                        <span class="info-box-number sss">
                          
                            <asp:Label ID="lblUGTotalStudent" runat="server"></asp:Label></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="totalSubmitted" runat="server" visible="false">
                <div class="info-box bg-green wrong-item item bot" id="test">
                    <span class="info-box-icon"><i class="fa fa-male"></i></span>
                    <div class="info-box-content" >
                        <span class="info-box-text" style="font-size :11px">Total Applicants<br/>Final Confirmed</span>
                         <asp:Label ID="lbladmbatch2" runat="server"></asp:Label>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblotalSubmitted" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>

            <div class="col-md-12 beauty" id="ugstatewise" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one1">
                    <div id="a" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">State Wise Applicants (Final Confirmed) - (<asp:Label ID="lbladmbatch7" runat="server" Text="Label"></asp:Label>)</h3>
                       <span> <asp:ImageButton ID="btnimg" runat="server" ToolTip="Export to Excel"   OnClick="btnStateWise_Click" style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_carousel');" />
                        
                    </div>

                    <div id="carousel" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="ltrChart1" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    <div id="Div_chartug"></div>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#carousel" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#carousel" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
            </div>
           
            <div class="col-md-12 beauty" id="divbranchcount" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div38">
                    <div id="Div39" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Branch Wise Registration Count</h3>
                         <span> <asp:ImageButton ID="btnbranchwise" runat="server" ToolTip="Export to Excel"  style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div40');" />
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <div id="Div40" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div41" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal24" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                     <asp:Literal ID="Literal25" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div36" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div36" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
            </div>

            <div class="col-md-12 beauty" id="Div14" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one11">
                    <div id="k" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title"> Fee Collection</h3>
                       <span> <asp:ImageButton ID="btnFeeCollection" runat="server" ToolTip="Export to Excel" style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div15');" />
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div15" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div16" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal4" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div15" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div15" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>
                       
            <div class="col-md-12 beauty" id="divgendercontadmbatchwise" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div51">
                    <div id="Div52" class="l"></div>
                    <div class="box-header with-border">
                        <div class="col-md-7">
                        <i class="fa fa-bar-chart"></i>

                        <h6 class="box-title" style="font-size:13px"><b>Total Admission Count Year Wise</b></h6>
                        
                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>--%>
                            </div>
                         <div class="col-md-1">
                              <asp:ImageButton ID="btnyearwise" runat="server" ToolTip="Export to Excel"   style="width:25px;height:20px;margin-left:-30px" ImageUrl="IMAGES/ICON_EX.png" />
                              <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-55px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div53');" /> 
                            
<%--                              <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Export to Excel" OnClick="btnyearwise_Click"  style="width:25px;height:20px;margin-left:-30px" ImageUrl="IMAGES/ICON_EX.png" />
                              <img src="IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-55px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div53');" /> --%>
                             
                       
                             </div>
                         <div class="col-md-4">
                              <asp:DropDownList ID="ddlbranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" >
                                  <asp:ListItem Value="0">Please Select</asp:ListItem>
                              </asp:DropDownList>
                        </div>
                    </div>

                    <div id="Div53" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div54" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal30" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal31" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>

                        <a class="carousel-control left" href="#Div53" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div53" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>

            <div class="col-md-12 beauty" id="Div17" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one3">
                    <div id="c" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">ReceiptTypeWise Fee Collection </h3>

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div18" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div19" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal8" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">

                                    <asp:Literal ID="Literal9" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div18" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div18" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>



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
            <div class="col-md-12 beauty" id="applfeecolug" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one4">
                    <div id="d" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Fee Collection Through Fee Collection(U.G)-(<asp:Label ID="lblafcsess" runat="server" Text="Label"></asp:Label>)</h3>

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12 beauty" id="Div2" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one8">
                    <div id="h" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Attendance Details</h3>

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>

                        </div>
                    </div>

                    <div id="Div3" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div4" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal12" runat="server"></asp:Literal>

                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal13" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div3" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div3" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>

            <div class="col-md-12 beauty" id="Div11" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div12">
                    <div id="Div13" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>
                        <h3 class="box-title">Degree Wise Applicants (Final Confirmed) - (<asp:Label ID="lbladmbatch9" runat="server" Text="Label"></asp:Label>)</h3>
                         <span> <asp:ImageButton ID="btndegreewise" runat="server" ToolTip="Export to Excel" OnClick="btndegreewise_Click" style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div22');" />
       
                    </div>

                    <div id="Div22" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div23" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal10" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal11" runat="server"></asp:Literal>

                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div22" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div22" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>


            <div class="col-md-12 beauty" id="Div25" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one102">
                    <div id="pb" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title" style="font-size:13px"><b>Employee Joining Details (FY :
                            <asp:Label ID="lblEmpFY" runat="server"></asp:Label>)</b></h3>
                         <span> <asp:ImageButton ID="btnEmpjoining" runat="server" ToolTip="Export to Excel"  style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div31');" />
                         <%-- <span> <asp:ImageButton ID="btnEmpjoining" runat="server" ToolTip="Export to Excel" OnClick="btnEmpjoining_Click" style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="IMAGES/ICON_IMG.png" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_carousel');" />--%>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    

                    <div id="Div31" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div32" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal18" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal19" runat="server"></asp:Literal>

                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div31" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div31" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>

            <div class="col-md-12 beauty"  style="display:none" >
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

        <div class="col-md-6">

            <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="totalregisteredpg" runat="server" visible="false">
                <div class="info-box bg-aqua wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-users"></i>
                    </span>
                    <div class="info-box-content">
                        <span class="info-box-text"> Registered<br />
                            Students(P.G)</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblPGTotalStudent" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="pgmale" runat="server" visible="false">
                <div class="info-box bg-green wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-male"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text"> Male(P.G)</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblPGTotalMale" runat="server"></asp:Label></b></span>


                        <span class="progress-description"></span>
                    </div>

                </div>

            </div>

          <%--  <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="totfeepaid" runat="server" visible="false">
                <div class="info-box bg-red wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size :11px">Payment Paid Applicants<br /> Final Confirmed</span>
                         <asp:Label ID="lbladmbatch5" runat="server"></asp:Label>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lbltotfeepaid" runat="server"></asp:Label></b></span>

                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>--%>

             <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="totfemale" runat="server" visible="false">
                <div class="info-box bg-aqua wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-users"></i></span>
                    <div class="info-box-content" style="font-size:11px">
                        <span class="info-box-text" style="font-size :11px">Total Female Applicants<br />Final Confirmed </span>
                        <asp:Label ID="lbladmbatch3" runat="server"></asp:Label>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lbltotfemale" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="totmale" runat="server" visible="false">
                <div class="info-box bg-red wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size :11px">Total Male Applicants<br />Final Confirmed</span>
                         <asp:Label ID="lbladmbatch4" runat="server"></asp:Label>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblotalmale" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>


       
            <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="totfeepending" runat="server" visible="false">
                <div class="info-box bg-yellow wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-inr"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size :11px">Total Payment<br /> Pending Students</span>
                         <asp:Label ID="lbladmbatch6" runat="server"></asp:Label>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lbltotfeepending" runat="server"></asp:Label></b></span>
                         <asp:Label ID="lblreport" Visible="false" runat="server"></asp:Label>
                        <span class="progress-description"></span>
                    </div>

                </div>

            </div>

            <div class="col-md-6 col-sm-6 col-xs-12 san animated rubberBand beauty" id="paytotalempFemale" runat="server" visible="false">
                <div class="info-box bg-red wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Female Employees </span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblTotalEmpFemale" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>


              <div class="col-md-12 beauty" id="pgstatewise" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one10">
                    <div id="j" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>
                        <h3 class="box-title">PG Admissions Statewise Count-(<asp:Label ID="lblpgascsess" runat="server" Text="Label"></asp:Label>)</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>

                        </div>
                    </div>

                    <div id="Div1" class="carousel slide carousel-fade" data-ride="carousel">

                        <div class="carousel-inner">
                            <div class="active item">
                                <div class="box-body">

                                    <asp:Literal ID="lt" runat="server"></asp:Literal>

                                    <div id="chart_div"></div>

                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body">
                                    <asp:Chart ID="PGChart" runat="server"
                                        BorderlineWidth="0" Height="350px" Palette="EarthTones"
                                        Width="500px" IsSoftShadows="true" AntiAliasing="All">
                                        <Titles>
                                            <asp:Title ShadowOffset="0" Name="Items" />
                                        </Titles>
                                        <Legends>
                                            <asp:Legend Alignment="Center" Docking="Right" ItemColumnSeparator="GradientLine" IsTextAutoFit="true" Name="Default1"
                                                LegendStyle="Table" IsEquallySpacedItems="true" Font="Verdana" />
                                        </Legends>
                                        <Series>
                                            <asp:Series Name="Default1" CustomProperties="DrawingStyle=Pie,
                    PieDrawingStyle=concave, MaxPixelPointWidth=100"
                                                ShadowOffset="0"
                                                ChartType="Pie" IsValueShownAsLabel="true" Font="Microsoft Sans Serif, 14pt" Palette="SemiTransparent">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" AlignmentStyle="Position" Area3DStyle-IsClustered="true" />
                                        </ChartAreas>
                                    </asp:Chart>

                                </div>

                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div1" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div1" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
            </div>

              <div class="col-md-12 beauty" id="Div29" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div30">
                    <div id="Div35" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Admission Registration Count</h3>
                      <%--  <span> <asp:ImageButton ID="btnregistrationcount" runat="server" ToolTip="Export to Excel" OnClick="btnregistrationcount_Click" style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="IMAGES/ICON_IMG.png" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_carousel');" />--%>

                        <span>     </span>
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
            </div>

            <div class="col-md-12 beauty" id="applicantregistpg" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one14">
                    <div id="o" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Date Wise Applicants (Final Confirmed) - (<asp:Label ID="lbladmbatch8" runat="server" Text="Label"></asp:Label>)</h3>
                         <span> <asp:ImageButton ID="btnregistrationcount" runat="server" OnClick="btnDateWise_Click"  ToolTip="Export to Excel"  style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div20');" />

                        <%--<div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>--%>
                    </div>

                    <div id="Div20" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div21" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                </div>
                            </div>

                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div20" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div20" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>


               <div class="col-md-12 beauty" id="Div50" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div57">
                    <div id="Div59" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Application Stages - (<asp:Label ID="lbladmbatch11" runat="server" Text="Label"></asp:Label>)</h3><br />
                         <span> <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Export to Excel" OnClick="btnApplicationStagesWise_Click" style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_EX.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div60');" />

                        <%--<div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>--%>
                    </div>

                    <div id="Div60" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div61" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal84" runat="server"></asp:Literal>
                                </div>
                            </div>

                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal89" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div60" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div60" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>


            <div class="col-md-12 beauty" id="divcategory" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div42">
                    <div id="Div43" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Category Wise Registration Count</h3>
                        <span><asp:ImageButton ID="btncategorywise" runat="server" ToolTip="Export to Excel"  Style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_Ex.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-18px;" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div44');" />
                        <%--<div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>--%>
                    </div>
                    <div id="Div44" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div45" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal26" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                     <asp:Literal ID="Literal27" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div40" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div40" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
            </div>
            

            <div class="col-md-12 beauty" id="Div8" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one">
                    <div id="t" class="l"></div>


                    <div class="box-header with-border ">

                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Admission BatchWise Count</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div9" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div10" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal6" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">

                                    <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div9" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div9" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>


            <div class="col-md-12 beauty" id="Div5" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one18">
                    <div id="s" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Result Details</h3>

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>

                        </div>
                    </div>

                    <div id="Div6" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div7" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal15" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal14" runat="server"></asp:Literal>

                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div6" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div6" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>

            <div class="col-md-12 beauty" id="divBankwisedd" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div46">
                    <div id="Div47" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-list"></i>

                        <h3 class="box-title">Bank Wise DD Collection</h3>
                        <span> <asp:ImageButton ID="btnbankwise" runat="server" ToolTip="Export to Excel"  Style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_Ex.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-18px;" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div48');" /> 
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <div id="Div48" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div49" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal28" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal29" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div20" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div20" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>
                </div>
            </div>
            

                <%-- <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="btn btn-warning"
                                 OnClientClick="javascript:PrintDivContent('ctl00_ContentPlaceHolder1_ugstatewise');">PRINT RECEIPT</asp:LinkButton>--%>
             <div class="col-md-12 beauty" id="divfeecollmonthwise" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="Div55">
                    <div id="Div56" class="l"></div>
                    <div class="box-header with-border">
                        <div class="col-md-7">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Month Wise Fee Collection</h3>
                       <%--  <span> <asp:ImageButton ID="feecollection" runat="server" ToolTip="Export to Excel" OnClick="feecollection_Click" Style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_Ex.png" /></span>
                        <img src="IMAGES/ICON_IMG.png" style="width:30px;height:25px;margin-top:-18px;" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_arousel');" />--%>

                            <%-- <span><asp:ImageButton ID="feecollection" runat="server" ToolTip="Export to Excel" OnClick="feecollection_Click" Style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGEICON_Ex.png" /></span>
                            <img src="IMAGES/ICON_IMG.png" style="width:30px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_arousel');" /> --%>
                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>--%>
                            </div>
                        <div class="col-md-1">
                             <span> <asp:ImageButton ID="feecollection" runat="server" ToolTip="Export to Excel"  style="width:25px;height:20px;margin-left:-30px" ImageUrl="IMAGES/ICON_Ex.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-55px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divfeecollection');" />

<%--                            <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Export to Excel" OnClick="btnyearwise_Click"  style="width:25px;height:20px;margin-left:-30px" ImageUrl="IMAGES/ICON_EX.png" />
                              <img src="IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-55px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div53');" /> --%>
                            </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlbranchfees" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  CssClass="form-control">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         

                    </div>

                    <div id="divfeecollection" runat="server" class="carousel slide carousel-fade" data-ride="carousel" >
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div58" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal32" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal33" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#divfeecollection" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#divfeecollection" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>

            <div class="col-md-12 beauty" id="Div24" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one101">
                    <div id="pa" class="l"></div>
                    <div class="box-header with-border">
                        <i class="fa fa-pie-chart"></i>

                        <h3 class="box-title">Staff Details (Total :
                            <asp:Label ID="lblTotalStaff" runat="server"></asp:Label>)</h3>
                        <span><asp:ImageButton ID="btnstaffdetails" runat="server" ToolTip="Export to Excel" Style="width:30px;height:25px;margin-left:20px" ImageUrl="IMAGES/ICON_Ex.png" /></span>
                        <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-18px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div27');"/>
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
                                    <asp:Literal ID="Literal16" runat="server"></asp:Literal>


                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal17" runat="server"></asp:Literal>


                                </div>
                            </div>
                        </div>
                        <a class="carousel-control left" href="#Div27" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div27" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>

                </div>
            </div>

            <div class="col-md-12 beauty" id="Div26" runat="server" visible="false">
                <div class="box box-primary wrong-item item top" id="one103">
                    <div id="pc" class="l"></div>
                    <div class="box-header with-border">
                        <div class="col-md-6">
                            <i class="fa fa-pie-chart"></i>
                            <h3 class="box-title">Shift Employees :
                                    <asp:Label ID="lblShiftEmp" runat="server"></asp:Label>
                            </h3>
                           
                        </div>
                        <%--<asp:UpdatePanel ID="updpayrule" runat="server">
                                <ContentTemplate>--%>
                        <div class="col-md-2">
                             <span> <asp:ImageButton ID="btnshiftemployees" runat="server" ToolTip="Export to Excel"  style="width:25px;height:20px;margin-left:-30px" ImageUrl="IMAGES/ICON_Ex.png" /></span>
                            <img src="../IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-15px;margin-left:-5px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_Div33');">
                        </div>
<%-- <span> <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Export to Excel" OnClick="feecollection_Click" style="width:25px;height:20px;margin-left:-30px" ImageUrl="IMAGES/ICON_Ex.png" /></span>
                        <img src="IMAGES/ICON_IMG.png" title="Download Image" style="width:30px;height:25px;margin-top:-55px" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divfeecollection');" />--%>
                        <div class="col-md-4">
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <asp:Image ID="imgCalJoinDate" runat="server" Style="cursor: pointer;" ImageUrl="~/images/calendar.png" />
                                </div>
                                <asp:TextBox ID="txtEmpAttendanceDate" CssClass="form-control" runat="server" Enabled="true" Width="100px"
                                    OnTextChange="txtEmpAttendanceDate_TextChanged" AutoPostBack="true" >
                                </asp:TextBox>
                                <asp:CalendarExtender ID="ceJoinDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtEmpAttendanceDate" PopupButtonID="imgCalJoinDate" Enabled="true" EnableViewState="true">
                                </asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="meeJoinDate" runat="server" TargetControlID="txtEmpAttendanceDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                <asp:MaskedEditValidator ID="mevJoinDate" runat="server" ControlExtender="meeJoinDate"
                                    ControlToValidate="txtEmpAttendanceDate" EmptyValueMessage="Please Enter Attendance Date"
                                    InvalidValueMessage="Attendance Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                    TooltipMessage="Please Enter Attendance Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                    ValidationGroup="emp" SetFocusOnError="True" />

                            </div>
                        </div>
                        <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtEmpAttendanceDate" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>


                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                <div id="Div33" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="active item">
                                <div id="Div34" style="text-align: center; margin-bottom: -30px" class="box-body">
                                    <asp:Literal ID="Literal20" runat="server" EnableViewState="False" Visible="true"></asp:Literal>
                                </div>
                            </div>
                            <div class="item">
                                <div class="box-body" style="margin-bottom: -30px">
                                    <asp:Literal ID="Literal21" runat="server" EnableViewState="False" Visible="true"></asp:Literal>
                                </div>
                            </div>
                        </div>

                        <a class="carousel-control left" href="#Div33" style="color: black" data-slide="prev">&lsaquo;</a>
                        <a class="carousel-control right" href="#Div33" style="color: black" data-slide="next">&rsaquo;</a>
                    </div>
                    <%--</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtEmpAttendanceDate" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </div>
            </div>

            <div class="col-md-12 beauty" style="display:none">
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
    </div>









    <div class="row" id="trTimeTable" runat="server" visible="false">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <i class="fa fa-newspaper-o"></i>

                    <h3 class="box-title">TIME TABLE</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="col-md-12">
                    <asp:ListView ID="lvTimeTable" runat="server">
                        <LayoutTemplate>
                            <div>
                                <h4>Time Table </h4>
                                <table class="table table-hover table-bordered">
                                    <thead>
                                        <tr class="bg-light-blue">
                                            <th>Slot
                                            </th>
                                            <th>Monday
                                            </th>
                                            <th>Tuesday
                                            </th>
                                            <th>Wednesday
                                            </th>
                                            <th>Thursday
                                            </th>
                                            <th>Friday
                                            </th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSlot" runat="server" Text='<%# Eval("TIME") %>' ToolTip='<%# Eval("SLOTNO") %>'></asp:Label>
                                </td>
                                <td id="tdMon" runat="server">
                                    <%# Eval("MONDAY").ToString().ToUpper().Replace("BREAK","--") %>
                                </td>
                                <td id="tdTue" runat="server">
                                    <%# Eval("TUESDAY").ToString().ToUpper().Replace("BREAK", "--")%>
                                </td>
                                <td id="tdWed" runat="server">
                                    <%# Eval("WEDNESDAY").ToString().ToUpper().Replace("BREAK", "--")%>
                                </td>
                                <td id="tdThu" runat="server">
                                    <%# Eval("THURSDAY").ToString().ToUpper().Replace("BREAK", "--")%>
                                </td>
                                <td id="tdFri" runat="server">
                                    <%# Eval("FRIDAY").ToString().ToUpper().Replace("BREAK", "--")%>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>

      <div id="divScript" runat="server" />
    <style style="text/css">
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
    </style>


</asp:Content>

