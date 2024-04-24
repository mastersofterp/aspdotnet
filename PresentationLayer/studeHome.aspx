<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudeHome.aspx.cs" Inherits="StudeHome" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="<%=Page.ResolveClientUrl("~/plugins/jQuery/jQuery-2.2.0.min.js")%>"   type="text/javascript"></script>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
       <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/Dashboard.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("https://www.google.com/jsapi")%>" type="text/javascript"></script>--%>

    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/Dashboard.css")%>" rel="stylesheet" />

    <style>
        .pay-online {
            position: fixed;
            bottom: 0px;
            left: 2px;
            z-index: 9999;
        }

            .pay-online .pay-img {
                width: 36px;
                height: auto;
            }

        #header-fixed {
            position: fixed;
            top: 0px;
            display: none;
            background-color: white;
        }

        .box-tools.pull-right {
            display: none;
        }

        .chiller-theme .sidebar-wrapper {
            display: none;
        }

        .page-wrapper.toggled .page-content {
            padding: 0;
            margin-top: 15px;
            padding-top: 40px !important;
        }

        .page-wrapper .page-content {
            height: auto;
        }

        .content {
            padding-bottom: 10px;
        }

        .scrollbar ul.fav-list li:hover {
            color: #212529;
            background-color: rgba(0,0,0,.075);
        }

        @media (min-width: 576px) and (max-width: 991px) {
            .content {
                padding-top: 0px;
                padding-bottom: 0px;
            }
        }

        .table-bordered > tbody > tr > td {
            text-align: left !important;
        }

        #tblAcdActivity.table-bordered > thead > tr > th {
            border-top: 0px solid #e6e9ed;
        }

        .new-parent {
            max-height: 180px;
            overflow-y: auto;
            background: #fff;
        }

            .new-parent .x_panel {
                box-shadow: none;
            }

        article.media {
            margin-top: 5px!important;
        }
    </style>
    <style>
        /* Customize website's scrollbar like Mac OS
        Not supports in Firefox and IE */
        /* total width */
        .scrollbar::-webkit-scrollbar {
            background-color: #fff;
            width: 5px;
            scrollbar-width: thin;
        }

        /* background of the scrollbar except button or resizer */
        .scrollbar::-webkit-scrollbar-track {
            background-color: #fff;
        }

            .scrollbar::-webkit-scrollbar-track:hover {
                background-color: #f4f4f4;
            }

        /* scrollbar itself */
        .scrollbar::-webkit-scrollbar-thumb {
            background-color: #babac0;
            border-radius: 2px;
            border: 2px solid #fff;
        }

            .scrollbar::-webkit-scrollbar-thumb:hover {
                background-color: #a0a0a5;
                border: 2px solid #f4f4f4;
            }

        /* set button(top and bottom of the scrollbar) */
        .scrollbar::-webkit-scrollbar-button {
            display: none;
        }
        /*::-webkit-scrollbar {
            width: 4px;
            height: 5px;
            background: #e5e5e5;
        }*/
    </style>
    <style>
        @keyframes blink {
            0%, 100% {
                border: solid 2px #3F06FF;
            }

            50% {
                border: solid 2px #f10a0b;
            }
        }

        .blink {
            border: #f10a0b;
            color: white;
            animation: blink 1s linear infinite;
        }
    </style>

    <script>            // Added By Shrikant W. on 12-12-2023
        $(document).on('click', '#ulTasks a', function(event) {
            event.preventDefault(); 

            var clickedLink = event.target.href; 
            var modifiedLink = removeUnwantedSegment(clickedLink);
            window.open(modifiedLink, '_blank');
        });


        function removeUnwantedSegment(link) {
            var linkParts = link.split('/'); 
            var filteredLink = linkParts.filter(part => part !== 'iitmsBc7v'); 
            var modifiedLink = filteredLink.join('/');
            return modifiedLink;
        }
    </script>

    <script>
        $(document).ready(function () {

            $('.news-jq').find('.media-body p, .media-body font').css({ 'font-size': '12px', 'line-height': '1.1', 'font-family': 'opensans' });
            $('.news-jq').find('.media-body font').attr('size', '0');

            /******************Student Attendance Percentage*********START**************/

            $.ajax({
                type: "POST",
                //url: "StudeHome.aspx/ShowAttPer",
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowAttPer")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessAttPer,
                failure: function (response) {
                }
            });           
            function OnSuccessAttPer(response) {
                //console.log("Label :",response['d']);
                var AttnPer = response['d'] + ' %';
                if (response['d'] != null) {
                    $('#lblAttPer').html(AttnPer);
                }
            };
            /******************Student Attendance Percentage*********END**************/

        });
    </script>

   <%-- QuickAccess -- PRASHANTG-220324----%>
    <script type ="text/javascript">
        function QuickAccess()
        {
            /************************ Quick Access ************************/
            /************************ PRASHANTG-TN56760-180424 ************************/
               $.ajax({
                type: "POST",
                //url: "StudeHome.aspx/ShowQuickAccessData",
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowQuickAccessData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessQL,
                failure: function (response) {
                }
            });
            function OnSuccessQL(response) {           
                loadQLData(response['d']);
            };
            function loadQLData(data) {
                var html = ''; $('#ulQuickAccess').html("");
                var LinkCount = 1;
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            /* *************************************************************************************************************
                                Do not give the space in href
                                Eg. href="'+item.Link+'?pageno='+item.PageNo+'"                --  Working in Both Local & Live
                                Eg. href="' + item.Link + '?pageno=' + item.PageNo + '"        --  Working in Local but Issue in Live // Gives Error Unexpacted Token %
                               ************************************************************************************************************* */                          
                            html += '<li class="list-group-item"><a href="' + item.Link == null ? '#' : item.Link + '?pageno=' + item.PageNo + '"  runat="server"  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
                           // item.Link is already binded with the iif logic and resolveurl
                            //html += '<li class="list-group-item"><a href="' + item.Link + '"  runat="server"  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
                          
                            LinkCount += 1;
                        });
                    } else {
                        html += '<li class="list-group-item text-center info" style="text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;">No records to display.. </li>';
                    }
                } else {
                    html += '<li class="list-group-item text-center info" style="text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;">No records to display.. </li>';
                }
                $('#ulQuickAccess').append(html);
            }
        
            /************************ Quick Access ************************/
        }
    </script>
   <%-- ShowAttendance -- PRASHANTG-180424--%>
    <script type="text/javascript">
        function ShowAttendance(){
            /******************* Student Attendance *********************/
            /******** PRASHANTG-TN56760-180424-TN56760******************/
           
            $.ajax({
                type: "POST",               
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowAttendance")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessStudAtt,
                failure: function (response) {
                    var html = ''; $('#tbodyAtten').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No records to display..</td></tr>';
                    $('#tbodyAtten').html(html);
                }
              });
            function OnSuccessStudAtt(response) {
              
                loadStudAtt(response['d']);
            };
            function loadStudAtt(data) {
                var html = '';
                $('#tbodyAtten').html("");
                if (data != null) {
                    //if (data.AttendancePercent != null) {
                    //    $('#lblAttPer').text(data.AttendancePercent + '%');
                    //}
                    if (data.AttendList != null) {
                        $.each(data.AttendList, function (i, d) {
                            html += '<tr>';
                            html += '<td class="text-center" data-container="body"  data-original-title="' + d.CourseCode + ' - ' + d.CourseName + ' (SEC - ' + d.SectionName + ')" data-toggle="tooltip">' + d.CourseCode + '</td>';
                            html += '<td class="text-center">' + d.Attendance + '</td>';
                            html += '<td class="text-center">' + d.AttendancePerc + '</td>';
                            html += '</tr>';
                     
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No records to display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No records to display..</td></tr>';
                }
                $('#tbodyAtten').html(html);
                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });

            };
            /******************* Student Attendance *********************/

        }
    </script>
  
    <%--LoadTask-- PRASHANTG-180424----%> 
    <script type="text/javascript">
        function LoadTask()
        {
            /************************ Tasks ************************/
            /************************ BELOW BLOCK COMMENTED BY PRASHANTG-TN56760-220324 ************************/

           $.ajax({
                type: "POST",
                //url: "StudeHome.aspx/ShowStudTasks",
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowStudTasks")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTask,
                failure: function (response) {
                    debugger;
                    console.log("no data : ", response);
                }
            });
            function OnSuccessTask(response) {
                debugger;
                loadTaskData(response['d']);
                console.log("On Succ Task : ", response['d']);
            };
            function loadTaskData(data) {
                debugger;
                console.log("On Load Task : ", data);
                var html = '';  $('#ulTasks').html("");
                var LinkCount = 1;
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            /* *************************************************************************************************************
                                Do not give the space in href
                                Eg. href="'+item.Link+'?pageno='+item.PageNo+'"                --  Working in Both Local & Live
                                Eg. href="' + item.Link + '?pageno=' + item.PageNo + '"        --  Working in Local but Issue in Live // Gives Error Unexpacted Token %
                               ************************************************************************************************************* */
                            html += '<li class="list-group-item"><a href="' + (item.Link == null ? '#' : item.Link + '?pageno=' + item.PageNo) + '"  runat="server"  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
                            LinkCount += 1;
                        });
                    } else {
                        html += '<li class="list-group-item text-center info" style="text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;">No records to display.. </li>';
                    }
                } else {
                    html += '<li class="list-group-item text-center info" style="text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;">No records to display.. </li>';
                }
                $('#ulTasks').append(html);
            }
            /************************ Tasks ************************/

        }
    </script>

   <%-- ClassTT-- PRASHANTG-180424------%>
    <script type="text/javascript">
        function ClassTT()
        {
            /************************  PRASHANTG-TN56760-180424 ************************/

           $.ajax({
                type: "POST",          
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowStudTimeTableData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTT,
                failure: function (response) {
                    var html = '';   $('#tbodyStudTT').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="7">No records to display..</td></tr>';
                    $('#tbodyStudTT').html(html);
                }
            });
            function OnSuccessTT(response) {
                loadTimeTableData(response['d']);
            };
            function loadTimeTableData(data) {
                var html = '';   $('#tbodyStudTT').html("");
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            html += '<tr>';
                            html += '<td class="text-center" style="width:14%">' + item.Slot + '</td>';
                            if (item.Monday != '-') {
                                var arrLec1 = item.Monday;
                                //.split('-');
                                html += '<td class="text-center" data-container="body"  data-original-title="' + arrLec1[1] + '-' + arrLec1[0] + ' (SEC-' + arrLec1[2] + ')" data-toggle="tooltip">' + arrLec1 + '</td>';
                            } else {
                                html += '<td class="text-center">' + item.Monday + '</td>';
                            }
                            if (item.Tuesday != '-') {
                                var arrLec2 = item.Tuesday;
                                //.split('-');
                                html += '<td class="text-center" data-container="body"  data-original-title="' + arrLec2[1] + ' - ' + arrLec2[0] + ' (SEC-' + arrLec2[2] + ')" data-toggle="tooltip">' + arrLec2 + '</td>';
                            } else {
                                html += '<td class="text-center">' + item.Tuesday + '</td>';
                            }
                            if (item.Wednesday != '-') {
                                var arrLec3 = item.Wednesday;
                                //.split('-');
                                html += '<td class="text-center" data-container="body"  data-original-title="' + arrLec3[1] + ' - ' + arrLec3[0] + ' (SEC-' + arrLec3[2] + ')" data-toggle="tooltip">' + arrLec3 + '</td>';
                            } else {
                                html += '<td class="text-center">' + item.Wednesday + '</td>';
                            }
                            if (item.Thursday != '-') {
                                var arrLec4 = item.Thursday;
                                //.split('-');
                                html += '<td class="text-center" data-container="body"  data-original-title="' + arrLec4[1] + ' - ' + arrLec4[0] + ' (SEC-' + arrLec4[2] + ')" data-toggle="tooltip">' + arrLec4 + '</td>';
                            } else {
                                html += '<td class="text-center">' + item.Thursday + '</td>';
                            }
                            if (item.Friday != '-') {
                                var arrLec5 = item.Friday;
                                //.split('-');
                                html += '<td class="text-center" data-container="body"  data-original-title="' + arrLec5[1] + ' - ' + arrLec5[0] + ' (SEC-' + arrLec5[2] + ')" data-toggle="tooltip">' + arrLec5 + '</td>';
                            } else {
                                html += '<td class="text-center">' + item.Friday + '</td>';
                            }
                            if (item.Saturday != '-') {
                                var arrLec6 = item.Saturday.split('-');
                                html += (arrLec6[0] == 'unefined'); { arrLec6[0] = '' } '<td class="text-center" data-container="body"  data-original-title="' + arrLec6[1] + '-' + arrLec6[0] + ' (SEC-' + arrLec6[2] + ')" data-toggle="tooltip">' + arrLec6 + '</td>';
                            } else {
                                html += '<td class="text-center">' + item.Saturday + '</td>';
                            }
                            //if (item.Sunday != '-') {
                            //    var arrLec7 = item.Sunday.split('-');
                            //    html += '<td class="text-center" data-container="body"  data-original-title="' + arrLec7[1] + ' - ' + arrLec7[0] + ' (SEC-' + arrLec7[2] + ')" data-toggle="tooltip">' + arrLec7[1] + '</td>';
                            //} else
                            //{
                            //    html += '<td class="text-center">' + item.Sunday + '</td>';
                            //}
                            //html += '</tr>';
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="7">No records to display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="7">No records to display..</td></tr>';
                }
                $('#tbodyStudTT').html(html);
                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });
            };
            /*********** Comment on 03-04-2020 Bind Only Body ******************/

            /************************ Bind Time Table ************************/
        }
    </script>

    <%-- TodayTT-- PRASHANTG-180424------%>
    <script type="text/javascript">
        function TodayTT()
        {
            /************************  PRASHANTG-TN56760-180424 ************************/

            $.ajax({
                type: "POST",          
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowTodayTimeTableData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTodayTT,
                failure: function (response) {
                    var html = ''; $('#tbodyTodaysTimeTable').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No records to display..</td></tr>';
                    $('#tbodyTodaysTimeTable').html(html);
                }
           });
            function OnSuccessTodayTT(response) {
                loadTodayTimeTableData(response['d']);
            };
            function loadTodayTimeTableData(data) {
                var html = '';
                $('#tbodyTodaysTimeTable').html("");
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            html += '<tr>';
                            html += '<td class="text-left" >' + item.Slot + '</td>';    
                            html += '<td class="text-center" >' + item.SlotNo + '</td>';    
                            html += '<td class="text-center" >' + item.CCode + '</td>';   
                            html += '</tr>';
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No records to display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No records to display..</td></tr>';
                }
                $('#tbodyTodaysTimeTable').html(html);
                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });
            };
            /*********** Comment on 03-04-2020 Bind Only Body ******************/

            /************************ Bind Time Table ************************/
        }
    </script>

     <%-- ExamTT-- PRASHANTG-200424------%>
    <script type="text/javascript">
        function ExamTT()
        {
            /************************  PRASHANTG-TN56760-20424 ************************/

            $.ajax({
                type: "POST",          
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowExamTimeTableData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessExamTT,
                failure: function (response) {
                    var html = ''; $('#tbodyExamTimeTable').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="6">No records to display..</td></tr>';
                    $('#tbodyExamTimeTable').html(html);
                }
            });
            function OnSuccessExamTT(response) {
                loadExamTimeTableData(response['d']);
            };
            function loadExamTimeTableData(data) {
                var html = '';
                $('#tbodyExamTimeTable').html("");
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            html += '<tr>';
                            html += '<td class="text-left" >' + item.ExamDate + '</td>';    
                            html += '<td class="text-center" >' + item.Slot + '</td>';    
                            html += '<td class="text-center" >' + item.CCode + '</td>';  
                            html += '<td class="text-left" >' + item.Course + '</td>';    
                            html += '<td class="text-center" >' + item.Semester + '</td>';    
                            html += '<td class="text-center" >' + item.Backlog + '</td>';
                            html += '</tr>';
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="6">No records to display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="6">No records to display..</td></tr>';
                }
                $('#tbodyExamTimeTable').html(html);
                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });
            };
           
            /************************ Bind examTime Table ************************/
        }
    </script>

      <%-- Placement-- PRASHANTG-200424------%>
    <script type="text/javascript">
        function Placement()
        {
            /************************  PRASHANTG-TN56760-20424 ************************/

            $.ajax({
                type: "POST",          
                url: '<%=Page.ResolveUrl("~/StudeHome.aspx/ShowPlacementData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessPlacement,
                failure: function (response) {
                    var html = ''; $('#tbodyPlacement').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="5">No records to display..</td></tr>';
                    $('#tbodyPlacement').html(html);
                }
            });
            function OnSuccessPlacement(response) {
                loadPlacementData(response['d']);
            };
            function loadPlacementData(data) {
                var html = '';
                $('#tbodyPlacement').html("");
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            html += '<tr>';
                            html += '<td class="text-left" >' + item.Company + '</td>';    
                            html += '<td class="text-center" >' + item.SchDate + '</td>';    
                            html += '<td class="text-center" >' + item.Salary + '</td>';  
                            html += '<td class="text-left" >' + item.Criteria + '</td>';    
                            html += '<td class="text-center" >' + item.Course + '</td>';
                            html += '</tr>';
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="5">No records to display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="5">No records to display..</td></tr>';
                }
                $('#tbodyPlacement').html(html);
                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });
            };
           
            /************************ Bind examTime Table ************************/
        }
    </script>

    <script>
        function AddClassTobtnoutfees() {
            $('#ctl00_ContentPlaceHolder1_btnoutfees').addClass('blink');
        }
    </script>

    <asp:Panel ID="pnlMarquee" runat="server" Visible="false">
        <div class="container-fluid">
            <h3>
                <marquee width="100%" direction="left" style="color: #ff0000; font-size: 18px">
                     Due to student related ongoing activity, some dashboard fetures has been OFF for certain period of time, It will be available soon. 
                </marquee>
            </h3>
        </div>
    </asp:Panel>
    <%--PRASHANTG-TN56760-220324--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlDash"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
        
    <div class="container-fluid">
        <section class="statistics">
            <asp:HiddenField ID="PageID" runat="server" Visible="false" />
            <div class="gutters-sm">
                <div class="mybox-main d-flex justify-content-between align-items-center">
                    <div class="pad-box flex-fill">
                        <div class="tile-box in-down a1">
                            <i class="fa fa-percent fa-fw icon-sm" style="background-color: #2f4554"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblAttPer">0.00 %</label>
                                </h3>
                                <span>Attendance</span>
                            </div>
                        </div>
                    </div>
                    <div class="pad-box flex-fill">
                        <div class="tile-box in-down a2">
                            <i class="fa fa-tasks fa-fw danger icon-sm" aria-hidden="true"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblTaskCount"></label>
                                    <%--<asp:Label ID="lblTaskCount" runat="server">00</asp:Label>--%></h3>
                                <%--<span>Tasks</span>--%>
                                <span>Assignment</span>
                            </div>
                        </div>
                    </div>
                    <div class="pad-box flex-fill">
                        <div class="tile-box in-down a3">
                            <i class="fa fa-bullhorn fa-fw success icon-sm" aria-hidden="true"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblAnnouncement"></label>
                                </h3>
                                <span>Announcement</span>
                            </div>
                        </div>
                    </div>
                    <div class="pad-box flex-fills" id="divoutstanding" runat="server">
                        <div class="tile-box in-down a4">
                            <%--  <i class="fa-light fa-indian-rupee-sign"></i>--%>
                            <i class="fas fa-rupee-sign icon-sm text-center" style="background-color: #d48265"></i>
                            <%--<i class="fa fa-indian-rupee-sign icon-sm" style="background-color: #d48265"></i>--%>
                            <div class="info">
                                <h3>
                                    <span>Total Outstanding Amount:  </span>
                                    <asp:Label ID="lblLastLoginTime" runat="server"></asp:Label>
                                    <small>
                                        <b>
                                            <asp:Label ID="lblLastLoginForm" runat="server"></asp:Label></b></small></h3>
                                &nbsp;
                             
                                <%--<span>Fees Related</span>--%>
                                <asp:LinkButton ID="btnoutfees" runat="server" CssClass="btn btn-sm btn-outline-primary blink" Text="Pay" OnClick="btnoutfees_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <%--PRASHANTG-TN56760-220324--%>
        <asp:UpdatePanel ID="pnlDash" runat="server">
            <ContentTemplate>
                <div class="col-12">
                    <div class="row equalHMRWrap flex gutters-sm">
                        <%--Attendance--%>
                        <div class="col-lg-3 col-md-6 col-12">
                            <div class="x_panel in-left a1">
                                <div class="x_title">
                                    <h2>Attendance</h2>
                                    <button id="btnLoadAttend" runat="server" onclick="ShowAttendance();"
                                        tabindex="1" type="button" class="btn float-right">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>
                                    <%--PRASHANTG-TN56760-220324 --%>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar">
                                    <table class="table table-hover small table-striped table-bordered nowrap">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th class="text-center">Subject</th>
                                                <th class="text-center">Lectures</th>
                                                <th class="text-center">%</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyAtten" >
                                            <td class='text-center' data-container='body'
                                                data-original-title='anc'
                                                data-toggle='tooltip'></td>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%-- Quick Access--%>
                        <div class="col-lg-2 col-md-6 col-12">
                            <div class="x_panel in-left a2">
                                <div class="x_title">
                                    <h2>Quick Access</h2>
                                    <button id="btnLoadQA" runat="server" onclick="QuickAccess();"
                                        tabindex="2" type="button" class="btn float-right">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>
                                    <%--PRASHANTG-TN56760-220324 --%>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar small">
                                    <ul class="list-group with-border-bottom fav-list" id="ulQuickAccess">
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <%--Tasks--%>
                        <div class="col-lg-2 col-md-6 col-12">
                            <div class="x_panel in-right a1">
                                <div class="x_title">
                                    <h2>Tasks</h2>
                                    <button id="btnLoadTask" runat="server" onclick="LoadTask();"
                                        tabindex="3" type="button" class="btn float-right">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>
                                    <%--PRASHANTG-TN56760-220324 --%>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar small">
                                    <asp:HiddenField ID="hftot" runat="server" />
                                    <ul class="list-group with-border-bottom fav-list" id="ulTasks">
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <%--Active Notice/News--%>
                        <div class="col-lg-5 col-md-12 col-12">
                            <div class="x_panel in-right a1">
                                <div class="x_title">
                                    <h2>Active Notice/News</h2>
                                  <%--  <button id="btnActNotice" runat="server" onserverclick="btnActNotice_Click"
                                        tabindex="4" type="button" class="btn float-right">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>--%>
                                    <%--PRASHANTG-TN56760-220324 --%>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="new-parent">
                                    <div class="x_content scrollbar small news-jq">
                                        <asp:ListView ID="lvActiveNotice" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-hover small table-striped table-bordered nowrap" id="tblNotice">
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>

                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <article class="media event">
                                                        <a class="pull-left date">
                                                            <p class="month"><%#Eval("MM")%></p>
                                                            <p class="day"><%#Eval("DD")%></p>
                                                        </a>
                                                        <div class="media-body">
                                                            <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                                    <p><%#Eval("NEWSDESC") %></p>--%>

                                                            <asp:LinkButton ID="lnkDownloadActive" runat="server" Text='<%#Eval("TITLE")%>' CommandArgument='<%#Eval("FILENAME")%>' OnCommand="GetFileNamePathEventForActiveNotice"></asp:LinkButton>
                                                            <p><%#Eval("NEWSDESC") %></p>
                                                        </div>
                                                    </article>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:ListView>

                                        <div class="x_title">
                                            <h2>Expired Notice/News</h2>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="x_content scrollbar small news-jq">
                                            <asp:ListView ID="lvExpNotice" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-hover small table-striped table-bordered nowrap" id="tblExpNotice">
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>

                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <article class="media event">
                                                            <a class="pull-left date">
                                                                <p class="month"><%#Eval("MM")%></p>
                                                                <p class="day"><%#Eval("DD")%></p>
                                                            </a>
                                                            <div class="media-body">
                                                                <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                                        <p><%#Eval("NEWSDESC") %></p>--%>

                                                                <asp:LinkButton ID="lnkDownloadExpired" runat="server" Text='<%#Eval("TITLE")%>' CommandArgument='<%#Eval("FILENAME")%>' OnCommand="GetFileNamePathEventForExpiredNotice"></asp:LinkButton>
                                                                <p><%#Eval("NEWSDESC") %></p>
                                                            </div>
                                                        </article>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:ListView>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- Today's Time Table--%>
                        <div class="col-lg-2 col-md-6 col-12">
                            <div class="x_panel in-left a1">
                                <div class="x_title">
                                    <h2>Today's Time Table</h2>
                                    <button id="btnTT" runat="server" onclick="TodayTT();"
                                        tabindex="5" type="button" class="btn float-right">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>
                                    <%--PRASHANTG-TN56760-220324 --%>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar">
                                   
                                            <table class="table table-hover small table-striped table-bordered nowrap" id="tblTodaysTimeTable">
                                                <thead class="bg-primary">
                                                    <tr>
                                                        <%--<th class="text-center">Time</th>
                                                        <th class="text-center">Subject</th>--%>
                                                        <th class="text-center">Slot</th>
                                                        <th class="text-center">SlotNo</th>
                                                        <th class="text-center">CCode</th>
                                                </thead>
                                                <tbody  id="tbodyTodaysTimeTable" >
                                                   
                                                </tbody>
                                            </table>                                       
                                </div>
                            </div>
                        </div>
                        <%--Class Time Table--%>
                        <div class="col-lg-5 col-md-6 col-12">
                            <div class="x_panel in-left a2">
                                <div class="x_title">
                                    <h2>Class Time Table</h2>
                                    <button id="btnClassTT" runat="server" onclick="ClassTT();"
                                        tabindex="6" type="button" class="btn float-right">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>
                                    <%--PRASHANTG-TN56760-220324 --%>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar">
                                    <table class="table table-hover small table-striped table-bordered nowrap">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th style="width: 14%" class="text-center">Time/ Day</th>
                                                <th class="text-center">Monday</th>
                                                <th class="text-center">Tuesday</th>
                                                <th class="text-center">Wednesday</th>
                                                <th class="text-center">Thursday</th>
                                                <th class="text-center">Friday</th>
                                                <th class="text-center">Saturday</th>
                                                <%-- <th class="text-center">Sunday</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyStudTT">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%--Exam Time Table--%>
                        <div class="col-lg-5 col-md-6 col-12">
                            <div class="x_panel in-right a2">
                                <div class="x_title">
                                    <h2>Exam Time Table</h2>
                                    <button id="btnExamTT" runat="server" onclick="ExamTT();"
                                        tabindex="7" type="button" class="btn float-right">
                                        <i class="fas fa-sync-alt"></i>
                                    </button>
                                    <%--PRASHANTG-TN56760-240324 --%>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content height-250 scrollbar">                             
                                            <table class="table table-hover small table-striped table-bordered nowrap" id="tblExamTimeTable">
                                                <thead class="bg-primary">
                                                    <tr>
                                                        <th style="width: 14%" class="text-center">EXAMDATE</th>
                                                        <th class="text-center">SLOTNAME</th>
                                                        <th class="text-center">CCODE</th>
                                                        <th class="text-center">COURSENAME</th>
                                                        <th class="text-center">SEMESTERNAME</th>
                                                        <th class="text-center">REGULAR_BACKLOG</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbodyExamTimeTable">                                                 
                                                </tbody>
                                            </table>
                                </div>
                            </div>
                        </div>
                    
                        <%--Placement Scheduled--%>
                        <div class="col-lg-12 col-md-8 col-12">
                            <div id="divplacement" runat="server">
                                <div class="x_panel in-right a2">
                                    <div class="x_title">
                                        <h2>Placement Scheduled</h2>
                                        <button id="btnPlacement" runat="server" onclick="Placement();"
                                            tabindex="8" type="button" class="btn float-right">
                                            <i class="fas fa-sync-alt"></i>
                                        </button>
                                        <%--PRASHANTG-TN56760-240324 --%>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content height-250 scrollbar">                                    
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblPlacement">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Company Name</th>
                                                            <th>Scheduled Date</th>
                                                            <th>Salary/Stipend</th>
                                                            <th>Eligibility</th>
                                                            <th>Degree & Branch</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbodyPlacement">                                                      
                                                    </tbody>
                                                </table>                                         
                                    </div>
                                </div>
                            </div>
                        </div>                      
                    </div>
                </div>
                <%--Tasks--%>
                <div class="col-lg-2 col-md-6 col-12">
                    <div class="x_panel in-right a1">
                        <div class="x_title">
                            <h2>Tasks</h2>
                            <button id="btnLoadTask" runat="server"  onserverclick="btnLoadTask_Click"
                                tabindex="3" type="button" class="btn float-right"><i class="fas fa-sync-alt"></i></button><%--PRASHANTG-TN56760-220324 --%>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar small">
                            <asp:HiddenField ID="hftot" runat="server" />
                            <ul class="list-group with-border-bottom fav-list" id="ulTasks" runat="server">
                            </ul>
                        </div>
                    </div>
                </div>
                <%--Active Notice/News--%>
                <div class="col-lg-5 col-md-12 col-12">
                    <div class="x_panel in-right a1">
                        <div class="x_title">
                            <h2>Active Notice/News</h2>
                             <button id="btnActNotice" runat="server"  onserverclick="btnActNotice_Click"
                                tabindex="4" type="button" class="btn float-right"><i class="fas fa-sync-alt"></i></button><%--PRASHANTG-TN56760-220324 --%>
                            <div class="clearfix"></div>
                        </div>
                        <div class="new-parent">
                            <div class="x_content scrollbar small news-jq">
                                <asp:ListView ID="lvActiveNotice" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-hover small table-striped table-bordered nowrap" id="tblNotice">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>

                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <article class="media event">
                                                <a class="pull-left date">
                                                    <p class="month"><%#Eval("MM")%></p>
                                                    <p class="day"><%#Eval("DD")%></p>
                                                </a>
                                                <div class="media-body">
                                                    <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                                    <p><%#Eval("NEWSDESC") %></p>--%>

                                                    <asp:LinkButton ID="lnkDownloadActive" runat="server" Text='<%#Eval("TITLE")%>' CommandArgument='<%#Eval("FILENAME")%>' OnCommand="GetFileNamePathEventForActiveNotice"></asp:LinkButton>
                                                    <p><%#Eval("NEWSDESC") %></p>
                                                </div>
                                            </article>
                                        </tr>

                                    </ItemTemplate>
                                </asp:ListView>

                                

                                <div class="x_title">
                                    <h2>Expired Notice/News</h2>                                   
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content scrollbar small news-jq">
                                    <asp:ListView ID="lvExpNotice" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-hover small table-striped table-bordered nowrap" id="tblExpNotice">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>

                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <article class="media event">
                                                    <a class="pull-left date">
                                                        <p class="month"><%#Eval("MM")%></p>
                                                        <p class="day"><%#Eval("DD")%></p>
                                                    </a>
                                                    <div class="media-body">
                                                        <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                                        <p><%#Eval("NEWSDESC") %></p>--%>

                                                        <asp:LinkButton ID="lnkDownloadExpired" runat="server" Text='<%#Eval("TITLE")%>' CommandArgument='<%#Eval("FILENAME")%>' OnCommand="GetFileNamePathEventForExpiredNotice"></asp:LinkButton>
                                                        <p><%#Eval("NEWSDESC") %></p>
                                                    </div>
                                                </article>
                                            </tr>

                                        </ItemTemplate>
                                    </asp:ListView>

                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
               <%-- Today's Time Table--%>
                <div class="col-lg-2 col-md-6 col-12">
                    <div class="x_panel in-left a1">
                        <div class="x_title">
                            <h2>Today's Time Table</h2>
                             <button id="btnTT" runat="server"  onserverclick="btnTT_Click"
                                tabindex="5" type="button" class="btn float-right"><i class="fas fa-sync-alt"></i></button><%--PRASHANTG-TN56760-220324 --%>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">
                            <asp:ListView ID="lvTodaysTT" runat="server">
                                <LayoutTemplate>
                                    <table class="table table-hover small table-striped table-bordered nowrap" id="tblTodaysTimeTable">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th class="text-center">Time</th>
                                                <th class="text-center">Subject</th>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center">
                                            <%#Eval("SLOTNAME") %>
                                        </td>
                                        <td class="text-center">
                                            <%#Eval("CCODE") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
                <%--Class Time Table--%>
                <div class="col-lg-5 col-md-6 col-12">
                    <div class="x_panel in-left a2">
                        <div class="x_title">
                            <h2>Class Time Table</h2>
                            <button id="btnClassTT" runat="server"  onserverclick="btnClassTT_Click"
                                tabindex="6" type="button" class="btn float-right"><i class="fas fa-sync-alt"></i></button><%--PRASHANTG-TN56760-220324 --%>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">
                            <table class="table table-hover small table-striped table-bordered nowrap">
                                <thead class="bg-primary">
                                    <tr>
                                        <th style="width: 14%" class="text-center">Time/ Day</th>
                                        <th class="text-center">Monday</th>
                                        <th class="text-center">Tuesday</th>
                                        <th class="text-center">Wednesday</th>
                                        <th class="text-center">Thursday</th>
                                        <th class="text-center">Friday</th>
                                        <th class="text-center">Saturday</th>
                                        <%-- <th class="text-center">Sunday</th>--%>
                                    </tr>
                                </thead>
                                <tbody id="tbodyStudTT">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <%--Exam Time Table--%>
                <div class="col-lg-5 col-md-6 col-12">
                    <div class="x_panel in-right a2">
                        <div class="x_title">
                            <h2>Exam Time Table</h2>
                             <button id="btnExamTT" runat="server"  onserverclick="btnExamTT_Click"
                                tabindex="7" type="button" class="btn float-right"><i class="fas fa-sync-alt"></i></button><%--PRASHANTG-TN56760-240324 --%>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">
                            <asp:ListView ID="lvExamTT" runat="server">
                                <LayoutTemplate>
                                    <table class="table table-hover small table-striped table-bordered nowrap" id="tblExamTimeTable">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th style="width: 14%" class="text-center">EXAMDATE</th>
                                                <th class="text-center">SLOTNAME</th>
                                                <th class="text-center">CCODE</th>
                                                <th class="text-center">COURSENAME</th>
                                                <th class="text-center">SEMESTERNAME</th>
                                                <th class="text-center">REGULAR_BACKLOG</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>

                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center">
                                            <%#Eval("EXAMDATE") %>
                                        </td>
                                        <td class="text-center">
                                            <%#Eval("SLOTNAME") %>
                                        </td>
                                        <td class="text-center">
                                            <%#Eval("CCODE") %>
                                        </td>
                                        <td class="text-center">
                                            <%#Eval("COURSENAME") %>
                                        </td>
                                        <td class="text-center">
                                            <%#Eval("SEMESTERNAME") %>
                                        </td>
                                        <td class="text-center">
                                            <%#Eval("REGULAR_BACKLOG") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
                <%-- //-----------------start----14-12-2023--%>
                <%--Placement Scheduled--%>
                <div class="col-lg-12 col-md-8 col-12">
                    <div id="divplacement" runat="server">
                        <div class="x_panel in-right a2">
                            <div class="x_title">
                                <h2>Placement Scheduled</h2>
                                 <button id="btnPlacement" runat="server"  onserverclick="btnPlacement_Click"
                                tabindex="8" type="button" class="btn float-right"><i class="fas fa-sync-alt"></i></button><%--PRASHANTG-TN56760-240324 --%>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content height-250 scrollbar">
                                <asp:ListView ID="LvPlacement" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Company Name</th>
                                                    <th>Scheduled Date</th>
                                                    <th>Salary/Stipend</th>
                                                    <th>Eligibility</th>
                                                    <th>Degree & Branch</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("COMPNAME")%></td>
                                             <td><%# Eval("SCHEDULEDATE", "{0: dd-MM-yyyy}")%></td>
                                            <td><%# Eval("Salary")%></td>
                                            <td><%# Eval("CRITERIA")%></td>
                                            <td><%# Eval("CourseStream")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <%--  //end--------13-12-2023--%>
                        </div>
                    </div>
                </div>
                <%--  //---------------------end------14-12-2023--%>
           
            </div>
        </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    </div>    
       
    <div class="pay-online">
        <asp:LinkButton ID="btnpayonline" runat="server" OnClick="btnpayonline_Click">
            <%--<a href="ACADEMIC/OnlinePayment.aspx">--%>

            <asp:Image ID="imgPay" runat="server" ImageUrl="~/Images/pay.png" class="pay-img" />
            <%-- </a>--%>
        </asp:LinkButton>
    </div>

    <script>
        function openWindowReload(link) {
            var href = link.href;
            window.open(href, '_blank');
            document.location.reload(true)
        }
    </script>

    <script type="text/javascript">
        function RefreshParent() {
            if (window.opener != null && !window.opener.closed) {
                window.opener.location.reload();
            }
        }
    </script>

</asp:Content>
