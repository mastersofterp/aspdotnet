<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="homeFaculty.aspx.cs" Inherits="homeFaculty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="<%=Page.ResolveClientUrl("~/bootstrap/css/Dashboard.css")%>" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/Dashboard.css")%>" rel="stylesheet" />
    <%--   <script src="<%=Page.ResolveClientUrl("https://www.google.com/jsapi")%>" type="text/javascript"></script>--%>

    <%-- <script src="plugins/Chart.js/dist/Chart.min.js"></script>
    <script src="plugins/chartjs-plugin-datalabels/dist/chartjs-plugin-datalabels.min.js"></script>--%>

    <script src="<%=Page.ResolveClientUrl("~/plugins/Chart.js/dist/Chart.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/chartjs-plugin-datalabels/dist/chartjs-plugin-datalabels.min.js")%>" type="text/javascript"></script>
    
    <style>
        .new-parent {
            height: 240px;
            overflow-y: auto;
            background: #fff;
        }

            .new-parent .x_panel {
                box-shadow: none;
            }

        .txt-head{
            position: sticky;
            top: -1px;
            background: #fff;
            z-index: 1080;
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
    
    <!-- SCRIPT FOR GRAPH -->

    <script>
        $(document).ready(function () {

            $('.news-jq').find('.media-body p, .media-body font').css({ 'font-size': '12px', 'line-height': '1.1', 'font-family': 'opensans' });
            $('.news-jq').find('.media-body font').attr('size', '0');

            //   Added on 30-03-2020

            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowAttPer",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowAttPer")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessAttPer,
                failure: function (response) {
                }
            });
            function OnSuccessAttPer(response) {
                //console.log("Label :",response['d']);
                var AttnPer = response['d'] + '%';
                $('#lblAttPer').html(AttnPer);
            };
       
          
            // -------------------comment notice---------------------
            //  $.ajax({
            //      type: "POST",
            //      url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowNewsData")%>',
            //      data: '{}',
            //      contentType: "application/json; charset=utf-8",
            //      dataType: "json",
            //      success: OnSuccessNews,
            //      failure: function (response) {
            //          var html = '';
            //          var html = '<div style="text-align:center; font-size:15px; font-weigth:bold" class="info">No Records To Display..</div>';
            //          $('#divNews').html(html);
            //      }
            //  });
            //  function OnSuccessNews(response) {
            //      loadNewsData(response['d']);
            //  };
            //  function loadNewsData(data) {
            //    var html = '';
            //    if (data != null) {
            //        if (data.length > 0) {
            //            $.each(data, function (i, d) {
            //                if (d.NewsLink != '') {
            //                    html += '<article class="media event"><a class="pull-left date"><p class="month">' + d.Month + '</p><p class="day">' + d.Day + '</p></a>';
            //                    html += '<div class="media-body"><a class="title" target="_blank" href=' + d.NewsLink + '>' + d.Title + '</a><p>' + d.NewsDescription + '</p></div></article>';
            //                }
            //                else {
            //                    html += '<article class="media event"><a class="pull-left date"><p class="month">' + d.Month + '</p><p class="day">' + d.Day + '</p></a>';
            //                    html += '<div class="media-body"><a class="title" href="#">' + d.Title + '</a><p>' + d.NewsDescription + '</p></div></article>';
            //                }
            //            });
            //        } else {
            //            html = '<div style="text-align:center; font-size:15px; font-weigth:bold" class="info">No Records To Display..</div>';
            //        }
            //
            //    } else {
            //        html = '<div style="text-align:center; font-size:15px; font-weigth:bold" class="info">No Records To Display..</div>';
            //    }
            //    $('#divNews').html(html);
            //};
            //--------------------------end notice ---------------------------------------------


            //-----------------------------------------------------------------------

            //function loadETaskData(data) {
            //    var html = '';
            //    if (data != null) {
            //        if (data.length > 0) {
            //            $.each(data, function (row, item) {
            //                html += '<li class="list-group-item"><a href=' + item.AL_URL + ' target="_blank">' + item.ACTIVITY_NAME + ' <span id="span1" runat="server" class="ncount">' + item.STAT + '</span></a></li>';
            //            });
            //        } else {
            //            html += '<li class="list-group-item text-center info" style="text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;" >No Records To Display..</li>';
            //        }
            //    } else {
            //        html += '<li class="list-group-item text-center info" style="text-align:center; font-size:15px; font-weigth:bold; background-color: #d9edf7;">No Records To Display..</li>';
            //    }
            //    $('#ulEmpTask').html(html);
            //}

            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowCasualBalLeaves",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowCasualBalLeaves")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessCBLeave,
                failure: function (response) {
                }
            });
            function OnSuccessCBLeave(response) {
                var CBLeave = response['d'];
                $('#lblLeaveBal').html(CBLeave);
            };

            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowUpcommingHolidays",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowUpcommingHolidays")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessUpcommingHolidays,
                failure: function (response) {
                }
            });
            function OnSuccessUpcommingHolidays(response) {
                var holidays = response['d'];
                $('#lblHolidays').html(holidays.Holiday);
                $('#lblMonth').html(holidays.Month);
            };

            //GetAttendancePerCourseWise();

            //-------------------------

           
        });

    </script>

     <%--PRASHANTG-TN56760--260324--%>
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
            <div class="gutters-sm">
                <div class="mybox-main row">
                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a1">
                            <i class="fa fa-percent fa-fw icon-sm" style="background-color: #c23531"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblAttPer">0.00</label>
                                </h3>
                                <span>Biometric Attendance</span>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a2">
                            <i class="fa fa-tags fa-fw danger icon-sm" style="background-color: #2f4554"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblLeaveBal"></label>
                                </h3>
                                <span>Casual Leave Balance</span>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a3">
                            <i class="fa fa-calendar fa-fw icon-sm" aria-hidden="true" style="background-color: #61a0a8"></i>
                            <div class="info">
                                <h3>
                                    <label id="lblHolidays"></label>
                                </h3>
                                <span>Upcoming Holidays &nbsp;
                                <label id="lblMonth"></label>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-6 col-6 pad-box">
                        <div class="tile-box in-down a4">
                            <i class="fa fa-user fa-fw icon-sm" style="background-color: #d48265"></i>
                            <div class="info">
                                <h3>
                                    <asp:Label ID="lblLastLoginTime" runat="server"></asp:Label>
                                    <small>
                                        <asp:Label ID="lblLastLoginForm" runat="server"></asp:Label></small></h3>
                                <span>Last Login</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        
         <%--PRASHANTG-TN56760--260324--%>
        <asp:UpdatePanel ID="pnlDash" runat="server">
            <ContentTemplate>

                <div class="row equalHMRWrap flex gutters-sm">
                    <%-- Today's Time Table--%>
                    <div class="col-lg-4 col-md-6 col-12">
                        <div class="x_panel in-left a1">
                            <div class="x_title">
                                <h2>Today's Time Table</h2>
                                <button id="btnTT" runat="server" onclick="TodaysTT();"
                                    tabindex="1" type="button" class="btn float-right">
                                    <i class="fas fa-sync-alt"></i>
                                </button>
                                <%--PRASHANTG-TN56760--260324 --%>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content height-250 scrollbar">
                               <%-- <asp:ListView ID="lvTodaysTT" runat="server">
                                    <LayoutTemplate>--%>
                                        <table class="table table-hover small table-striped table-bordered nowrap" id="tblTodaysTT">
                                            <thead class="bg-primary">
                                                <tr>
                                                    <th class="text-center">Slot</th>
                                                    <th class="text-center">Subject</th>
                                                    <%--<th class="text-center">Branch</th>
                                                        <th class="text-center">Semester</th>
                                                        <th class="text-center">Section</th>--%>
                                                </tr>
                                                <%-- <tr>
                                            <td>
                                                <asp:LinkButton ID="lnk" runat="server" Text="2"></asp:LinkButton>
                                            </td>
                                        </tr>--%>
                                            </thead>
                                            <tbody id="ttbody">
                                               <%-- <tr id="itemPlaceholder" runat="server" />--%>
                                            </tbody>

                                        </table>
                                    <%--</LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: left">
                                                <%#Eval("SLOTNAME") %>
                                            </td>
                                            <td class="text-center">
                                                <%#Eval("CCODE") %>
                                                <asp:LinkButton ID="lnkCourse" runat="server" Text='<%#Eval("CCODE") %>' CommandArgument='<%# Eval("COURSENO")%>' OnClick="lnkCourse_Click" Visible="false"></asp:LinkButton>
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
                                </asp:ListView>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Attendance (%)--%>
            <div class="col-lg-4 col-md-6 col-12">
                <div class="x_panel in-left a2">
                    <div class="x_title">
                        <h2>Attendance (%)</h2>
                     <%--PRASHANTG-TN56760--260324 --%>    
                        <button id="btnLoadAttend" type="button"  onclick="GetAttendancePerCourseWise();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar small">
                        <canvas id="AttendanceChart"></canvas>
                    </div>
                </div>
            </div>
            <%--Active Notice/News--%>
            <div class="col-lg-4 col-md-6 col-12 new-parent">
                <div class="x_panel in-right a2  scrollbar">
                    <div class="x_title txt-head">
                        <h2>Active Notice/News</h2>
                            <button id="btnActNotice" runat="server" onserverclick="btnActNotice_Click"
                                tabindex="3" type="button" class="btn float-right"><i class="fas fa-sync-alt"></i></button><%--PRASHANTG-TN56760--260324 --%>
                        <div class="clearfix"></div>
                    </div>
                    <%--Active Notice/News--%>
                    <div class="col-lg-4 col-md-6 col-12 new-parent">
                        <div class="x_panel in-right a2  scrollbar">
                             <div class="x_title">
                        <h2>Active Notice/News</h2>
                        <%--PRASHANTG-TN56760-280324 --%>
                        <%--<button id="btnActiveN" type="button" onclick="ActiveNotice();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>--%>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-150 scrollbar"> 
                         <div class="table-responsive" style="height: 170px; overflow: scroll; border-top: 1px solid #e5e5e5;">
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
                                                            <asp:LinkButton ID="lnkDownloadActive" runat="server" Text='<%#Eval("TITLE")%>' CommandArgument='<%#Eval("FILENAME")%>' OnCommand="GetFileNamePathEventForActiveNotice"></asp:LinkButton>
                                                            <p><%#Eval("NEWSDESC") %></p>
                                                        </div>
                                                    </article>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                         </div>
                    </div>
                    <div class="x_title ">
                        <h2>Expired Notice/News</h2>
                        <%--  <button id="btnExpNotice" type="button" onclick="ExpNotice();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>--%>
                        <div class="clearfix"></div>
                    <div class="x_content height-150 scrollbar">
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
           
        </div>

        <div class="row equalHMRWrap flex gutters-sm">
            <%--Class Time Table--%>
            <div class="col-lg-5 col-md-6 col-12">
                <div class="x_panel in-left a2">
                    <div class="x_title">
                        <h2>Class Time Table</h2>
                        <%--PRASHANTG-TN56760--260324 --%>
                        <button id="btnClassTT" type="button"  onclick="LoadClassTT();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <div class="table-responsive" style="height: 178px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                            <table class="table table-hover small table-striped table-bordered nowrap" style="width: 100%;" id="tblTimeTable">
                            </table>
                        </div>
                    </div>
                    <%--Invigilation ScheduleUpdated by PrashantG-200424)--%>
                    <%--Added by shubham on TID : 56240--%>
                    <div class="col-lg-5 col-md-6 col-12">
                        <div class="x_panel in-right a2">
                            <div class="x_title">
                                <h2>Invigilation Schedule</h2>
                                <button id="btnInvigi" runat="server" onclick="Invigilation();"
                                    type="button" class="btn float-right">
                                    <i class="fas fa-sync-alt"></i>
                                </button>
                                <%--PRASHANTG-TN56760--260324 --%>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content height-250 scrollbar">                            
                                        <table class="table table-hover small table-striped table-bordered nowrap" id="tblInvigilation">
                                            <thead class="bg-primary">
                                                <tr>
                                                    <th class="text-center">Exam Date</th>
                                                    <th class="text-center">Exam Time Slots</th>
                                                    <th class="text-center">Exam Name</th>
                                                    <th class="text-center">Room Name</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyIng">                                              
                                            </tbody>
                                        </table>                                 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--In / Out Time--%>
            <div class="col-lg-2 col-md-6 col-12">
                <div class="x_panel in-right a2">
                    <div class="x_title">
                        <h2>In / Out Time</h2>
                       <%--PRASHANTG-TN56760--260324 --%>
                        <button id="btnInOut" type="button"  onclick="ShowInOut();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">

                                <asp:Panel ID="pnlAttachmentList" runat="server">
                                    <table class="table table-hover small table-striped table-bordered nowrap" id="tblInOut">
                                        <thead class="bg-primary">
                                            <tr>
                                                <th>Day</th>
                                                <th class="text-center">In-Time</th>
                                                <th class="text-center">Out-Time</th>
                                            </tr>
                                        </thead>
                                        <tbody id="inOutBody">
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <%--Exam Time Table--%>
                    <div class="col-lg-8 col-md-6 col-12 ">
                        <div class="x_panel in-right a2">
                            <div class="x_title">
                                <h2>Exam Time Table</h2>
                                <button id="btnExamTT" runat="server" onclick="ExamTT();"
                                    tabindex="6" type="button" class="btn float-right">
                                    <i class="fas fa-sync-alt"></i>
                                </button>
                                <%--PRASHANTG-TN56760--260324 --%>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content height-250 scrollbar">                               
                                        <table class="table table-hover small table-striped table-bordered nowrap" id="tblExamTT">
                                            <thead class="bg-primary">
                                                <tr>
                                                    <th class="text-center">EXAMDATE</th>
                                                    <th class="text-center">SLOTNAME</th>
                                                    <th class="text-center">CCODE</th>
                                                    <th class="text-center">COURSENAME</th>
                                                    <th class="text-center">SEMESTERNAME</th>
                                                    <th class="text-center">REGULAR_BACKLOG</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyExamtt">                                            
                                            </tbody>
                                        </table>                                  
                            </div>
                        </div>
                    </div>
                    <%--Quick Access--%>
                    <div class="col-lg-2 col-md-6 col-12">
                        <div class="x_panel in-right a2">
                            <div class="x_title">
                                <h2>Quick Access</h2>
                                <button id="btnQA" type="button" onclick="QuickAccess();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>
                                <%--PRASHANTG-TN56760--260324 --%>
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
                                <button id="btnTask" type="button" onclick="LoadTask();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>
                                <%--PRASHANTG-TN56760--260324 --%>
                                <div class="clearfix"></div>
                            </div>

                            <div class="x_content height-250 scrollbar small">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <ul class="list-group with-border-bottom fav-list" id="ulEmpTask">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Quick Access--%>
            <div class="col-lg-2 col-md-6 col-12">
                <div class="x_panel in-right a2">
                    <div class="x_title">
                        <h2>Quick Access</h2>
                        <button id="btnQA" type="button"  onclick="QuickAccess();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>
                        <%--PRASHANTG-TN56760--260324 --%>
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
                        <button id="btnTask" type="button"  onclick="LoadTask();" class="btn float-right"><i class="fas fa-sync-alt"></i></button>
                        <%--PRASHANTG-TN56760--260324 --%>
                        <div class="clearfix"></div>
                    </div>
                    
                    <div class="x_content height-250 scrollbar small">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <ul class="list-group with-border-bottom fav-list" id="ulEmpTask">
                        </ul>
                    </div>
                </div>
            </div>
        </div>

         </ContentTemplate>
    </asp:UpdatePanel>
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
   <%-- TodaysTT - PRASHANTG-TN56760--170424--%>
    <script type="text/javascript">
        function TodaysTT()
        {
            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/BindTodaysTimeTbl",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/BindTodaysTimeTbl")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTT,
                failure: function (response) {
                    var html = ''; $('#ttbody').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="2">No Records To Display.. </td></tr>';
                    $('#ttbody').html(html);
                }
              });
            function OnSuccessTT(response) {
                loadTable(response['d']);
            };
            function loadTable(data) {
                var html = '';
                
                $('#ttbody').html("");
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (i, d) {
                            if (d.SlotIme != '-') {
                                html += '<tr>';
                                html += '<td class="text-center">' + d.SlotIme + '</td>';
                                html += '<td class="text-center"> <a id="llnkbutton" href="Academic/AttendenceByFaculty.aspx?pageno=112"' + d.CourseCode + ' - ' + d.Subject + ' (BRANCH-' + d.BranchShortName + ', SEM-' + d.Semester + ', SEC-' + d.Section + ') " data-toggle="tooltip" >' + d.CourseCode + '</td>';
                                //'</td>  data-container="body"  data-original-title="' + d.CourseCode + ' - ' + d.Subject + ' (BRANCH-' + d.BranchShortName + ', SEM-' + d.Semester + ', SEC-' + d.Section + ') " data-toggle="tooltip" >' + d.CourseCode + '</td>'
                                html += '</tr>';
                            } else {
                                html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="2">No Records To Display..</td></tr>';
                            }
                            // html += '<td class="text-center"  data-container="body" data-original-title="' + d.CourseCode + ' - ' + d.Subject + '" data-toggle="tooltip" >' + d.CourseCode + '</td>';
                            //html += '<td class="text-center"  data-container="body"  data-original-title="' + d.BranchShortName + ' - ' + d.Branch + '" data-toggle="tooltip">' + d.BranchShortName + '</td>';
                            //html += '<td class="text-center">' + d.Semester + '</td>';
                            //html += '<td class="text-center">' + d.Section + '</td>';

                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="2">No Records To Display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="2">No Records To Display..</td></tr>';
                }
                $('#ttbody').html(html);

                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });
            };
        }
    </script>

    <%--BindChart--%> <%--PRASHANTG-TN56760--260324--%>
    <script>
        function BindChart(ccode,percentage)
        {
            var ctx1 = document.getElementById('AttendanceChart');
            var myChart1 = new Chart(ctx1, {
                plugins: [ChartDataLabels],//, legendMargin],
                type: 'bar',
                data: {
                    //labels: ['Code 1', 'Code 2', 'Code 3'],
                    labels: ccode,
                    datasets: [{
                        //label: "Continuous Assessment-1",
                        //data: [85, 45, 60],
                        data: percentage,
                        backgroundColor: [
                            '#fff5dc', '#dcf2f2', '#d6ecfa', '#ebe0ff', '#f5f5f5',

                        ],
                        borderWidth: 1,
                        borderColor: '#ffff',
                    }]
                },
                options: {
                    layout: {
                        padding: 14
                    },
                    plugins: {
                        legend: {
                            display: false
                        },
                        //title:{
                        //    display:true,
                        //    text:"Research Publications",
                        //},
                        datalabels: {
                            anchor: 'end', // remove this line to get label in middle of the bar
                            align: 'end',
                            display: 'auto',
                            formatter: (val) => (`${val}`),
                            labels: {},
                            value: {},
                            font:{ family: 'open_sansregular',
                                size: '11px'}
                        }                    
                    },         
                    scales: {
                        x:{
                            ticks:{
                                font:{
                                    family: 'open_sansregular',
                                    size:'11px'
                                }
                            }
                        },
                        y:{                ticks:{
                            font:{
                                family: 'open_sansregular',
                                size:11
                            }
                        }}
                    }
                }
            });           
        }
        
    </script>

    <%--GetAttendancePerCourseWise--%> <%--PRASHANTG-TN56760--260324--%>
    <script type="text/javascript">
        function GetAttendancePerCourseWise() {

            var ccode = [];var percenatge = [];
            //alert('inside')
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("homeFaculty.aspx/ShowAttendancePerCourseWise")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                
                dataType: "json",

                success: function (response) {
                    //alert(data)
                    var data = JSON.parse(response.d);
                    var rw1 = "";
                    console.log(data)
                    for(var i = 0;i < data.length;i++)
                    {
                        ccode.push(data[i]["CCODE"]);
                        percenatge.push(data[i]["PERCENTAGECOUNT"]);
                    }
                    
                    BindChart(ccode,percenatge);
                    
                },
                failure: function (response) {
                    alert("failure");
                },
                error: function (response) {
                    console.log(response);
                    //debugger
                    alert("error");
                    alert(response.responseText);
                }

            });


        }

    </script>

    <%--LoadClassTT--%> <%--PRASHANTG-TN56760--260324--%>
    <script type="text/javascript">
        function LoadClassTT(){
            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowTimeTable",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowTimeTable")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTimeTable,
                failure: function (response) {
                    var html = ''; $('#tblTimeTable').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><th class="text-center" >No Records To Display..</th></tr>';
                    $('#tblTimeTable').html(html);
                }
            });
            function OnSuccessTimeTable(response) {
                loadTimeTable(response['d']);
            };
            function loadTimeTable(data) {
                debugger;
                if (data != null) {
                    var html = ''; $('#tblTimeTable').html("");
                    html += '<thead class="bg-light-blue" style="position: sticky; z-index:1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">';
                    // html += '<tr class="bg-success">';
                    html += '<tr>';
                    html += '<th class="text-center">Slot</th>';
                    html += '<th class="text-center">Monday</th>';
                    html += '<th class="text-center">Tuesday</th>';
                    html += '<th class="text-center">Wednesday</th>';
                    html += '<th class="text-center">Thursday</th>';
                    html += '<th class="text-center">Friday</th>';
                    html += '<th class="text-center">Saturday</th>';
                    html += '<th class="text-center">Sunday</th>';
                    html += '</tr></thead>';
                    html += '<tbody>';
                    if (data.objTTList != null) {
                        if (data.objTTList.length > 0) {
                            $.each(data.objTTList, function (row, item) {
                                html += '<tr>';
                                html += '<td class="text-center">' + item.Slot + '</td>';
                                if (item.Monday != '-') {
                                    var arrLec1 = item.Monday.split('-');
                                    html += '<td class="text-center" data-container="body">' + arrLec1 + '' + '</td>';
                                } else {
                                    html += '<td class="text-center" >' + item.Monday + '</td>';
                                }
                                if (item.Tuesday != '-') {
                                    var arrLec2 = item.Tuesday.split('-');
                                    html += '<td class="text-center" data-container="body">' + arrLec2 + '' + '</td>';
                                } else {
                                    html += '<td class="text-center" >' + item.Tuesday + '</td>';
                                }
                                if (item.Wednesday != '-') {
                                    var arrLec3 = item.Wednesday.split('-');
                                    html += '<td class="text-center" data-container="body">' + arrLec3 + '' + '</td>';
                                } else {
                                    html += '<td class="text-center" >' + item.Wednesday + '</td>';
                                }
                                if (item.Thursday != '-') {
                                    var arrLec4 = item.Thursday.split('-');
                                    html += '<td class="text-center" data-container="body">' + arrLec4 + '' + '</td>';
                                } else {
                                    html += '<td class="text-center" >' + item.Thursday + '</td>';
                                }
                                if (item.Friday != '-') {
                                    var arrLec5 = item.Friday.split('-');
                                    html += '<td class="text-center" data-container="body">' + arrLec5 + '' + '</td>';
                                } else {
                                    html += '<td class="text-center" >' + item.Friday + '</td>';
                                }
                                if (item.Saturday != '-') {
                                    var arrLec6 = item.Saturday.split('-');
                                    html += '<td class="text-center" data-container="body">' + arrLec6 + '' + '</td>';
                                } else {
                                    html += '<td class="text-center" >' + item.Saturday + '</td>';
                                }
                                //if (item.Sunday != '-') {
                                //    var arrLec7 = item.Sunday.split('-');
                                //    html += '<td class="text-center" data-container="body"  data-original-title="' + arrLec7[1] + ' - ' + arrLec7[0] + ' (' + arrLec7[2] + ') ' + '" data-toggle="tooltip">' + arrLec7[1] + '</td>';
                                //} else {
                                //    html += '<td class="text-center" >' + item.Sunday + '</td>';
                                //}
                                html += '</tr>';
                            });
                        } else {
                            html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="8">No Records To Display..</td></tr>';
                        }
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="8">No Records To Display..</td></tr>';
                    }
                    html += '</tbody>';
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><th class="text-center" >No Records To Display..</th></tr>';
                }
                $('#tblTimeTable').html(html);
                //alert(html)
                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });
            };

        }
    </script>

    <%--ShowInOut--%> <%--PRASHANTG-TN56760--260324--%>
    <script type="text/javascript">
        function ShowInOut(){
            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowInOutTime",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowInOutTime")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessInOut,
                failure: function (response) {
                    var html = ''; $('#inOutBody').html("");
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No Records To Display..</td></tr>';
                    $('#inOutBody').html(html);
                }
            });
            function OnSuccessInOut(response) {
                loadInOutTbl(response['d']);
            };
            function loadInOutTbl(data) {
                var html = '';
                $('#inOutBody').html("");
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (i, d) {
                            html += '<tr>';
                            html += '<td >' + d.Day + '</td>';
                            html += '<td >' + d.InTime + '</td>';
                            html += '<td >' + d.OutTime + '</td>';
                            html += '</tr>';
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No Records To Display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No Records To Display..</td></tr>';
                }
                $('#inOutBody').html(html);
            };
        }
    </script>

    <%--LoadTask--%> <%--PRASHANTG-TN56760--260324--%>
    <script type="text/javascript">
        function LoadTask()
        {            
            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowEmpTasks",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowEmpTasks")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessEmpTask,
                failure: function (response) {
                }
            });
            function OnSuccessEmpTask(response) {
                loadETaskData(response['d']);
            };
            function loadETaskData(data) {
                var html = '';
                $('#ulEmpTask').html("");
                var LinkCount = 1;
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            /**************************************************************************************************************
                               Do not give the space in href
                               Eg. href="'+item.Link+'?pageno='+item.PageNo+'"                --  Working in Both Local & Live
                               Eg. href="' + item.Link + '?pageno=' + item.PageNo + '"        --  Working in Local but Issue in Live // Gives Error Unexpacted Token %
                              ************************************************************************************************************* */
                            html += '<li class="list-group-item"><a href="' + item.Link + '?pageno=' + item.PageNo + '"  runat="server"  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
                            LinkCount += 1;
                        });
                    } else {
                        html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No Records To Display..</li>';
                    }
                } else {
                    html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No Records To Display..</li>';
                }
                $('#ulEmpTask').append(html);
            }
        }
    </script>

    <%--QuickAccess--%> <%--PRASHANTG-TN56760--260324--%>
    <script type="text/javascript">
        function QuickAccess(){
            /************************ Quick Access ************************/
            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowQuickAccessData",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowQuickAccessData")%>',
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
                var html = '';
                $('#ulQuickAccess').html("");
                var LinkCount = 1;
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            /* *************************************************************************************************************
                                Do not give the space in href
                                Eg. href="'+item.Link+'?pageno='+item.PageNo+'"                --  Working in Both Local & Live
                                Eg. href="' + item.Link + '?pageno=' + item.PageNo + '"        --  Working in Local but Issue in Live // Gives Error Unexpacted Token %
                               ************************************************************************************************************* */
                            html += '<li class="list-group-item"><a href="' + item.Link + '?pageno=' + item.PageNo + '"  runat="server"  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
                            LinkCount += 1;
                        });
                    } else {
                        html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No Records To Display..</li>';
                    }
                } else {
                    html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No Records To Display..</li>';
                }
                $('#ulQuickAccess').append(html);
            }
            /************************ Quick Access ************************/
        }
    </script>

    <%--ExamTT--%> <%--PRASHANTG-TN56760--1804324--%>
    <script type="text/javascript">
        function ExamTT(){
            /************************ Quick Access ************************/
            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowQuickAccessData",
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowExamTT")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessExamDetails,
                failure: function (response) {
                }
            });
            function OnSuccessExamDetails(response) {
                loadExData(response['d']);
            };
            function loadExData(data) {
                var html = '';
                $('#tbodyExamtt').html("");              
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            html += '<tr>';
                            html += '<td class="text-center">' + item.ExamDate + '</td>';
                            html += '<td class="text-center">' + item.SlotName + '</td>';
                            html += '<td class="text-center">' + item.CCode + '</td>';
                            html += '<td class="text-center">' + item.Course + '</td>';
                            html += '<td class="text-center">' + item.Semester + '</td>';
                            html += '<td class="text-center">' + item.Backlog + '</td>';
                            html += '</tr>';
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="6">No Records To Display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="6">No Records To Display..</td></tr>';
                }
                $('#tbodyExamtt').append(html);
            }
            /************************ ExamTT ************************/
        }
    </script>

    <%--Invigilation--%> <%--PRASHANTG-TN56760--2004324--%>
    <script type="text/javascript">
        function Invigilation(){
            /************************ Invigilation ************************/
            $.ajax({
                type: "POST",            
                url: '<%=Page.ResolveUrl("~/homeFaculty.aspx/ShowInvigilation")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessInvigilationDetails,
                failure: function (response) {
                }
            });
            function OnSuccessInvigilationDetails(response) {
                loadIngData(response['d']);
            };
            function loadIngData(data) {
                var html = '';
                $('#tbodyIng').html("");              
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            html += '<tr>';
                            html += '<td class="text-center">' + item.Examdt + '</td>';
                            html += '<td class="text-center">' + item.ExamName + '</td>';
                            html += '<td class="text-center">' + item.ExamTm + '</td>';
                            html += '<td class="text-center">' + item.RoomName + '</td>';                          
                            html += '</tr>';
                        });
                    } else {
                        html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="6">No Records To Display..</td></tr>';
                    }
                } else {
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="6">No Records To Display..</td></tr>';
                }
                $('#tbodyIng').append(html);
            }
            /************************ ExamTT ************************/
        }
    </script>

    <%--ActiveNotice--%> <%--PRASHANTG-TN56760--1804324--%>
    <script type="text/javascript">
        function ActiveNotice(){
            $.ajax({
                type: "POST",
                //url: "principalHome.aspx/BindActivityDetails",
                url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowNewsData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessActiveNoticeDetails,
                failure: function (response) {
                }
            });
            function OnSuccessActiveNoticeDetails(response) {
                var ActivNData = response['d'];         
                loadActivNDetails(ActivNData);
            };
            function loadActivNDetails(ActivNData) {
                var html = '';             
                $('#tblActiveNotice').html("");
                // $('#tbodyActiveNotice td').parent().remove();
                if (ActivNData != null) {
                    if (ActivNData.length > 0) {
                        $.each(ActivNData, function (row, item) {                           
                            //html += "<tr>";
                            //html += "<td class='media event'>";
                            //html += "<a class='pull-left date' >";
                            //html += "<p class='month'>" + item.Month + "</p>";
                            //html += "<p class='day'>" + item.Day + "</p>";
                            //html += "</a>";   
                            //html += "</td >";
                            //html += "<td class='media-body'><a class='title' target='_blank' href='" + item.NewsLink + "'>" + item.Title +"</a><p>" + item.NewsDescription+ "</p></td>";
                        
                            //html += "<td class='text-left'>" + item.Status + "</td>";                         
                            //// html += "<td >";
                            //// html += "<asp:LinkButton ID='lnkDownloadActive' runat='server' Text='" + item.TITLE + "' CommandArgument='" + item.FILENAME + "' OnCommand='GetFileNamePathEventForActiveNotice'></asp:LinkButton>";
                            //// html += "<a id='btndownload' href = 'javascript:;' onclick ='CallButtonEvent("+ item.FILENAME+")' target='_blank'>"+ item.TITLE +"</a>";
                            ////html += "</td >";
                            //html += "</tr> ";
                            html+="<tr>";
                            html+=" <article class='media event'>";
                            html+="  <a class='pull-left date'>";
                            html+=" <p class='month'>" + item.Month + "</p>";
                            html+=" <p class='day'>" + item.Day + "</p>";
                            html+=" </a>";
                            html+=" <div class='media-body'>";
                            html+="  <asp:LinkButton ID='lnkDownloadNotice' runat='server' Text='" + item.TITLE + "' CommandArgument='" + item.FILENAME + "' OnCommand='GetFileNamePathEventForActiveNotice'></asp:LinkButton>";
                            html+=" <p>" + item.NewsDescription+ "</p>";
                            html+=" </div>";
                            html+="</article>";
                            html+=  " </tr>";
                            alert(html);
                        });
                    } else {
                        html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                    }
                } else {
                    html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                }
                $('#tblActiveNotice').append(html);              
            };
        }
    </script> 

    <%--ExpNotice--%> <%--PRASHANTG-TN56760--1804324--%>
    <script type="text/javascript">

        //PRASHANTG-170424-BUGFIX-NOTICE WAS NOT LOADED
        //Show Expired Notice
        function ExpNotice(){
            $.ajax({
                type: "POST",
                //url: "principalHome.aspx/BindActivityDetails",
                url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowExpiredNewsData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessExpNoticeDetails,
                failure: function (response) {
                }
            });
            function OnSuccessExpNoticeDetails(response) {
                var ExpNData = response['d'];         
                loadExpDetails(ExpNData);
            };
            function loadExpDetails(ExpNData) {
                var html = '';             
                $('#tbodyExpNotice').html("");
                // $('#tbodyActiveNotice td').parent().remove();
                if (ExpNData != null) {
                    if (ExpNData.length > 0) {
                        $.each(ExpNData, function (row, item) { 
                            html += "<tr>";
                            html += "<td class='media event'>";
                            html += "<a class='pull-left date' >";
                            html += "<p class='month'>" + item.Month + "</p>";
                            html += "<p class='day'>" + item.Day + "</p>";
                            html += "</a>";
                            html += "</td>";
                            html += "<td class='media-body'>";
                            html += "<p>" + item.NewsDescription + "</p>"; 
                            html += "</td >";
                            //  html += "<td >";
                            // html += "<a id='btndownloadExp' href = 'javascript:;' onclick ='CallButtonEvent("+ item.FILENAME+")' target='_blank'>"+ item.TITLE +"</a>";
                            // html += "</td >";
                            html += "</tr> ";
                        });
                    } else {
                        html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                    }
                } else {
                    html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                }
                $('#tbodyExpNotice').append(html);              
            };
        }
            //--------------------------
    </script>


        </asp:Content>
