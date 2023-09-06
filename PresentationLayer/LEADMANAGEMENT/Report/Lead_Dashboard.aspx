<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Lead_Dashboard.aspx.cs" Inherits="Lead_Dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css' media="screen" />
    <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.css' media="screen" />

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script src="https://code.highcharts.com/highcharts.src.js"></script>
    <script src="http://code.highcharts.com/highcharts-3d.js"></script>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/json2/20130526/json2.min.js"></script>
    <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
    <script src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>


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

    <script>
        $(function () {
            $(".beauty").draggable();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">

        <div class="marquee" id="divmarquee" runat="server" visible="false">
            <p id="pmarq" runat="server"></p>
        </div>
        <asp:Label ID="lblpopup" runat="server" Style="display: none"></asp:Label>
        <div class="col-md-12">
            <div class="box box-primary">
                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="row">
                                <div style="align-content: center" class="col-md-12">
                                    <div class="form-group">
                                        <div class="form-group col-md-2">
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Admission Batch</label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:DropDownList ID="ddlAdmissionBatch" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Admission Batch" runat="server" OnSelectedIndexChanged="ddlAdmissionBatch_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>

        <div class="col-md-12">
            <div class="col-md-3 col-sm-3 col-xs-12 san animated rubberBand beauty" id="divTotalEnquiry" runat="server" visible="false">
                <div class="info-box bg-aqua wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-users"></i></span>

                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size: 11px">Total Enquiry</span>
                        <span class="info-box-number sss">
                            <asp:Label ID="lblTotalEnquiry" runat="server"></asp:Label></span>
                        <span class="progress-description"></span>
                    </div>
                </div>
            </div>

            <div class="col-md-3 col-sm-3 col-xs-12 san animated rubberBand beauty" id="divMale" runat="server" visible="false">
                <div class="info-box bg-green wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-male"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size: 11px">Male</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblTotalMale" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>

             <div class="col-md-3 col-sm-3 col-xs-12 san animated rubberBand beauty" id="divFemale" runat="server" visible="false">
                <div class="info-box bg-green wrong-item item bot">
                    <span class="info-box-icon"><i class="fa fa-female"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size: 11px">Female</span>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="lblTotalFemale" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>

            <div class="col-md-3 col-sm-3 col-xs-12 san animated rubberBand beauty" id="divOther" runat="server" visible="false">
                <div class="info-box bg-green wrong-item item bot" id="Div4">
                    <span class="info-box-icon"><i class="fa fa-transgender"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text" style="font-size: 11px">Other</span>
                        <asp:Label ID="lblTotalOther" runat="server"></asp:Label>
                        <span class="info-box-number sss"><b>
                            <asp:Label ID="Label4" runat="server"></asp:Label></b></span>
                        <span class="progress-description"></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>

        </div>

        <div class="col-md-12 ">
            <div class="col-md-6 ">
                <div class="col-md-12 beauty" id="divSourseWiseEnquiryCount" runat="server" visible="false">
                    <div class="box box-primary wrong-item item top" id="one1">
                        <div id="a" class="l"></div>
                        <div class="box-header with-border">
                            <i class="fa fa-pie-chart"></i>

                            <h3 class="box-title">Source Wise Enquiry Count</h3>
                            <span>
                                <asp:ImageButton ID="btnSourseWiseEnquiryCount" runat="server" ToolTip="Export to Excel" OnClick="btnSourseWiseEnquiryCount_Click" Style="width: 30px; height: 25px; margin-left: 20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                            <img src="../../IMAGES/ICON_IMG.png" title="Download image" style="width: 30px; height: 25px; margin-top: -18px; cursor: pointer" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divSourseWiseEnquiryCount');" />

                        </div>
                        

                        <div id="carousel" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                            <div class="carousel-inner">
                                <div class="active item">
                                    <div style="text-align: center; margin-bottom: -30px" class="box-body">
                                        <asp:Literal ID="ltrChart1" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <a class="carousel-control left" href="#carousel" style="color: black" data-slide="prev">&lsaquo;</a>
                            <a class="carousel-control right" href="#carousel" style="color: black" data-slide="next">&rsaquo;</a>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 beauty" id="divDegreeWiseDoneStatusCount" runat="server" visible="false">
                    <div class="box box-primary wrong-item item top" id="Div2">
                        <div id="Div3" class="l"></div>
                        <div class="box-header with-border">
                            <i class="fa fa-pie-chart"></i>

                            <h3 class="box-title">Degree Wise Enquiry Done Status</h3>
                            <span>
                                <asp:ImageButton ID="btnDegreeWiseDoneStatusCount" runat="server" ToolTip="Export to Excel" OnClick="btnDegreeWiseDoneStatusCount_Click" Style="width: 30px; height: 25px; margin-left: 20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                            <img src="../../IMAGES/ICON_IMG.png" title="Download image" style="width: 30px; height: 25px; margin-top: -18px; cursor: pointer" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divDegreeWiseDoneStatusCount');" />
                        </div>

                        <div id="divDegreeWiseDoneStatusCarousel" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                            <div class="carousel-inner">
                                <div class="active item">
                                    <div style="text-align: center; margin-bottom: -30px" class="box-body">
                                        <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                                    </div>
                                </div>

                            </div>
                            <a class="carousel-control left" href="#carousel" style="color: black" data-slide="prev">&lsaquo;</a>
                            <a class="carousel-control right" href="#carousel" style="color: black" data-slide="next">&rsaquo;</a>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 beauty" id="divDaillyFollowUpCount" runat="server" visible="false">
                    <div class="box box-primary wrong-item item top" id="Div7">
                        <div id="Div8" class="l"></div>
                        <div class="box-header with-border">
                            <i class="fa fa-pie-chart"></i>

                            <h3 class="box-title">Userwise Dailly Follow UP Count</h3>
                            <span>
                                <asp:ImageButton ID="btnDaillyFollowUpCount" runat="server" ToolTip="Export to Excel" OnClick="btnDaillyFollowUpCount_Click" Style="width: 30px; height: 25px; margin-left: 20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                            <img src="../../IMAGES/ICON_IMG.png" title="Download image" style="width: 30px; height: 25px; margin-top: -18px; cursor: pointer" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divDaillyFollowUpCount');" />
                        </div>

                        <div id="divDaillyFollowUpcarousel" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                            <div class="carousel-inner">
                                <div class="active item">
                                    <div style="text-align: center; margin-bottom: -30px" class="box-body">
                                        <asp:Literal ID="Literal10" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal11" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal12" runat="server"></asp:Literal>
                                    </div>
                                </div>

                            </div>
                            <a class="carousel-control left" href="#divDaillyFollowUpcarousel" style="color: black" data-slide="prev">&lsaquo;</a>
                            <a class="carousel-control right" href="#divDaillyFollowUpcarousel" style="color: black" data-slide="next">&rsaquo;</a>
                        </div>
                    </div>
                </div>

            </div>

            <div class="col-md-6 ">
                <div class="col-md-12 beauty" id="divEnquiryStatusDoneCount" runat="server" visible="false">
                    <div class="box box-primary wrong-item item top" id="Div12">
                        <div id="Div13" class="l"></div>
                        <div class="box-header with-border">
                            <i class="fa fa-pie-chart"></i>
                            <h3 class="box-title">Enquiry Status Done Count</h3>
                            <span>
                                <asp:ImageButton ID="btnEnquiryStatusDoneCount" runat="server" ToolTip="Export to Excel" OnClick="btnEnquiryStatusDoneCount_Click" Style="width: 30px; height: 25px; margin-left: 20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                                <img src="../../IMAGES/ICON_IMG.png" title="Download image" style="width: 30px; height: 25px; margin-top: -18px;cursor:pointer" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divEnquiryStatusDoneCount');" />
                        </div>

                        <div id="divEnquiryStatusDoneGraph" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                            <div class="carousel-inner">
                                <div class="active item">
                                    <div style="text-align: center; margin-bottom: -30px" class="box-body">
                                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="box-body" style="margin-bottom: -30px">
                                        <asp:Literal ID="Literal3" runat="server"></asp:Literal>

                                    </div>
                                </div>
                            </div>
                            <a class="carousel-control left" href="#divEnquiryStatusDoneGraph" style="color: black" data-slide="prev">&lsaquo;</a>
                            <a class="carousel-control right" href="#divEnquiryStatusDoneGraph" style="color: black" data-slide="next">&rsaquo;</a>
                        </div>

                    </div>
                </div>

                <div class="col-md-12 beauty" id="divBranchWiseDoneStatusCount" runat="server" visible="false">
                    <div class="box box-primary wrong-item item top" id="Div5">
                        <div id="Div6" class="l"></div>
                        <div class="box-header with-border">
                            <i class="fa fa-pie-chart"></i>

                            <h3 class="box-title">Branch Wise Enquiry Done Status</h3>
                            <span>
                                <asp:ImageButton ID="btnBranchWiseDoneStatusCount" runat="server" ToolTip="Export to Excel" OnClick="btnBranchWiseDoneStatusCount_Click" Style="width: 30px; height: 25px; margin-left: 20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                                <img src="../../IMAGES/ICON_IMG.png" title="Download image" style="width: 30px; height: 25px; margin-top: -18px;cursor:pointer" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divBranchWiseDoneStatusCount');" />
                        </div>

                        <div id="divBranchWiseDoneStatuscarousel" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                            <div class="carousel-inner">
                                <div class="active item">
                                    <div style="text-align: center; margin-bottom: -30px" class="box-body" >
                                        <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div  style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal8" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                 <div  style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal9" runat="server"></asp:Literal>
                                    </div>
                                </div>
                               
                            </div>
                            <a class="carousel-control left" href="#divBranchWiseDoneStatuscarousel" style="color: black" data-slide="prev">&lsaquo;</a>
                            <a class="carousel-control right" href="#divBranchWiseDoneStatuscarousel" style="color: black" data-slide="next">&rsaquo;</a>
                        </div>
                    </div>
                </div>

                 <div class="col-md-12 beauty" id="divUserStatuswiseDaillyFollowCount" runat="server" visible="false">
                    <div class="box box-primary wrong-item item top" id="Div9">
                        <div id="Div10" class="l"></div>
                        <div class="box-header with-border">
                            <i class="fa fa-pie-chart"></i>

                            <h3 class="box-title">User Statuswise Dailly Follow Count</h3>
                            <span>
                                <asp:ImageButton ID="btnUserStatuswiseDaillyFollowCount" runat="server" ToolTip="Export to Excel" OnClick="btnUserStatuswiseDaillyFollowCount_Click" Style="width: 30px; height: 25px; margin-left: 20px" ImageUrl="../../IMAGES/ICON_EX.png" /></span>
                            <img src="../../IMAGES/ICON_IMG.png" title="Download image" style="width: 30px; height: 25px; margin-top: -18px; cursor: pointer" onclick="javascript: PrintDivContent('ctl00_ContentPlaceHolder1_divUserStatuswiseDaillyFollowCount');" />
                        </div>

                        <div id="divdivUserStatuswiseDaillyFollowcarousel" runat="server" class="carousel slide carousel-fade" data-ride="carousel">
                            <div class="carousel-inner">
                                <div class="active item">
                                    <div style="text-align: center; margin-bottom: -30px" class="box-body">
                                        <asp:Literal ID="Literal13" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal14" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="margin-bottom: -30px" class="item">
                                    <div class="box-body">
                                        <asp:Literal ID="Literal15" runat="server"></asp:Literal>
                                    </div>
                                </div>

                            </div>
                            <a class="carousel-control left" href="#divdivUserStatuswiseDaillyFollowcarousel" style="color: black" data-slide="prev">&lsaquo;</a>
                            <a class="carousel-control right" href="#divdivUserStatuswiseDaillyFollowcarousel" style="color: black" data-slide="next">&rsaquo;</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
   
</asp:Content>

