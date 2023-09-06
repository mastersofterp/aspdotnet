<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="homeNonFaculty.aspx.cs" Inherits="homeNonFaculty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/Dashboard.css")%>" rel="stylesheet" />

    <style>
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

    <script>
        $(document).ready(function () {

            $('.news-jq').find('.media-body p, .media-body font').css({ 'font-size': '12px', 'line-height': '1.1', 'font-family': 'opensans' });
            $('.news-jq').find('.media-body font').attr('size', '0');

            // Added on 30-03-2020

            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowAttPer",
                url: '<%=Page.ResolveUrl("~/homeNonFaculty.aspx/ShowAttPer")%>',
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

            $.ajax({
                type: "POST",
                //url: "~/homeNonFaculty.aspx/ShowInOutTime",
                url: '<%=Page.ResolveUrl("~/homeNonFaculty.aspx/ShowInOutTime")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessInOut,
                failure: function (response) {
                    var html = '';
                    html += '<tr style="text-align:center; font-size:15px; font-weigth:bold" class="info"><td colspan="3">No Records To Display..</td></tr>';
                    $('#inOutBody').html(html);
                }
            });
            function OnSuccessInOut(response) {
                loadInOutTbl(response['d']);
            };
            function loadInOutTbl(data) {
                var html = '';
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (i, d) {
                            html += '<tr>';
                            html += '<td style="text-align:center;">' + d.Day + '</td>';
                            html += '<td style="text-align:center;">' + d.InTime + '</td>';
                            html += '<td style="text-align:center;">' + d.OutTime + '</td>';
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

            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowEmpTasks",
                url: '<%=Page.ResolveUrl("~/homeNonFaculty.aspx/ShowEmpTasks")%>',
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
                var LinkCount = 1;
                if (data != null) {
                    if (data.length > 0) {
                        $.each(data, function (row, item) {
                            /**************************************************************************************************************
                               Do not give the space in href
                               Eg. href="'+item.Link+'?pageno='+item.PageNo+'"                --  Working in Both Local & Live
                               Eg. href="' + item.Link + '?pageno=' + item.PageNo + '"        --  Working in Local but Issue in Live // Gives Error Unexpacted Token %
                              ************************************************************************************************************* */
                            html += '<li class="list-group-item"><a href="' + item.Link == null ? '#' : item.Link + '?pageno=' + item.PageNo + '"  runat="server"  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
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


            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowCasualBalLeaves",
                url: '<%=Page.ResolveUrl("~/homeNonFaculty.aspx/ShowCasualBalLeaves")%>',
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
                url: '<%=Page.ResolveUrl("~/homeNonFaculty.aspx/ShowUpcommingHolidays")%>',
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


            /************************ Quick Access ************************/
            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowQuickAccessData",
                url: '<%=Page.ResolveUrl("~/homeNonFaculty.aspx/ShowQuickAccessData")%>',
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
        });


    </script>

    <div class="container-fluid mt-5 mb-4">
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

                                    <asp:Label ID="lblLastLoginForm" runat="server"></asp:Label></h3>

                                <span>Last Login</span>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <div class="row equalHMRWrap flex gutters-sm">
            <div class="col-lg-6 col-md-6 col-12">
                <div class="x_panel in-right a1">
                    <div class="x_title">
                        <h2>In / Out Time</h2>

                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">

                        <asp:Panel ID="pnlAttachmentList" runat="server">
                            <table class="table table-hover small table-striped table-bordered nowrap" id="tblTodaysTimeTable">
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

            <div class="col-lg-6 col-md-6 col-12">
                <div class="x_panel in-right a1">
                    <div class="x_title">
                        <h2>Tasks</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar small">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <ul class="list-group with-border-bottom fav-list" id="ulEmpTask">
                        </ul>
                    </div>
                </div>
            </div>

            <div class="col-lg-6 col-md-6 col-12">
                <div class="x_panel in-right a2">
                    <div class="x_title">
                        <h2>Active Notice/News</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
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
                                            <p><%#Eval("NEWSDESC") %></p>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                        </div>
                                    </article>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>

            <div class="col-lg-6 col-md-6 col-12">
                <div class="x_panel in-right a2">
                    <div class="x_title">
                        <h2>Expired Notice/News</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar small news-jq">
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
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
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

