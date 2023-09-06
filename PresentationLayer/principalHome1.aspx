<%@ Page Language="C#" AutoEventWireup="true" CodeFile="principalHome.aspx.cs" Inherits="principalHome" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
     <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/Dashboard.css")%>" rel="stylesheet" />
    <%--<link href="plugins/newbootstrap/css/Dashboard.css" rel="stylesheet" />--%>

    <style>
        #header-fixed{
            position: fixed;
            top: 0px;
            display: none;
            background-color: white;
        }
        .box-tools.pull-right {
            display:none;
        }
        .chiller-theme .sidebar-wrapper {
            display:none;
        }
        .page-wrapper.toggled .page-content {
            padding:0;
            margin-top:20px;
            padding-top:0px !important;
        }
        .page-wrapper .page-content {
            height: auto;
        }
        .content {
             padding-top:0px;
         }
        @media  (min-width: 576px) and (max-width: 991px){
            .content {
                padding-top: 30px;
            }
        }
    </style>

    <style>
        /* Customize website's scrollbar like Mac OS
        Not supports in Firefox and IE */
        /* total width */
        .scrollbar::-webkit-scrollbar{
            background-color: #fff;
            width: 5px;
        }

        /* background of the scrollbar except button or resizer */
        .scrollbar::-webkit-scrollbar-track{
            background-color: #fff;
        }

        .scrollbar::-webkit-scrollbar-track:hover{
            background-color: #f4f4f4;
        }

        /* scrollbar itself */
        .scrollbar::-webkit-scrollbar-thumb{
            background-color: #babac0;
            border-radius: 2px;
            border: 2px solid #fff;
        }

        .scrollbar::-webkit-scrollbar-thumb:hover{
            background-color: #a0a0a5;
            border: 2px solid #f4f4f4;
        }

        /* set button(top and bottom of the scrollbar) */
        .scrollbar::-webkit-scrollbar-button{
            display: none;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            // if no Webkit browser
            (function () {
                var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
                var isSafari = /Safari/.test(navigator.userAgent) && /Apple Computer/.test(navigator.vendor);
                var scrollbarDiv = document.querySelector('.scrollbar');
                if (!isChrome && !isSafari) {
                    scrollbarDiv.innerHTML = 'You need Webkit browser to run this code';
                }
            })();

            //alert('a');
            //added for male count
            $.ajax({
                type: "POST",
                //url: "principalHome.aspx/ShowMaleCount",
                url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowMaleCount")%>',
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessMaleCount,
                    failure: function (response) {
                    }
                });

                //$.ajax({
                //    type: "POST",
                //    url: "/principalHome.aspx/ShowMaleCount",
                //    cache: false,
                //    async: true,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (response, type, xhr) {
                //        var retVal = JSON.stringify(response);
                //        alert("hiiiiiiiii");
                //        //                            alert(response.d);
                //        window.alert(JSON.parse(retVal).GetJSONDataResult);
                //    },
                //    error: function (xhr) {
                //        window.alert('error:Hi ' + xhr.statusText);
                //    }
                //});

                //const text = "this is test";   
                //$.ajax({      
                //    url: "Test.aspx/Test?str="+text,
                //    contentType: "application/json; charset=utf-8",
                //    method: 'post',
                //    data: "{'str':'"+text+"'}",
                //    success: function (data) {
                //        console.log(data);},
                //    error: function (response) {
                //        debugger;
                //        console.log(response);  }
                //});

                function OnSuccessMaleCount(response) {

                    var maleCount = response['d'];

                    $('#lblMaleCount').html(maleCount);
                };

                //added for female count
                $.ajax({
                    type: "POST",
                    //url: "principalHome.aspx/ShowFeMaleCount",
                    url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowFeMaleCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessfeMaleCount,
                failure: function (response) {
                }
            });
                function OnSuccessfeMaleCount(response) {
                    var femaleCount = response['d'];

                    $('#lblFemaleCount').html(femaleCount);
                };

                //added for ACTIVE USER count
                $.ajax({
                    type: "POST",
                    //url: "principalHome.aspx/ActiveUserCount",
                    url: '<%=Page.ResolveUrl("~/principalHome.aspx/ActiveUserCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessActiveUserCount,
                failure: function (response) {
                }
            });
                function OnSuccessActiveUserCount(response) {
                    var activeUserCount = response['d'];
                    $('#lblActiveUser').html(activeUserCount);
                };

                //added for total student count
                $.ajax({
                    type: "POST",
                    //url: "principalHome.aspx/TotalStudentCount",
                    url: '<%=Page.ResolveUrl("~/principalHome.aspx/TotalStudentCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTotstudentCount,
                failure: function (response) {
                }
            });
                function OnSuccessTotstudentCount(response) {
                    var totStudentCount = response['d'];
                    $('#lblTotalStudent').html(totStudentCount);
                };

                // added for Students count
                $.ajax({
                    type: "POST",
                    //url: "principalHome.aspx/BindStudentsCount",
                    url: '<%=Page.ResolveUrl("~/principalHome.aspx/BindStudentsCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessStudentsCount,
                failure: function (response) {
                }
            });
                function OnSuccessStudentsCount(response) {
                    var StudentCount = response['d'];
                    loadStudentDetails(StudentCount);
                };
                function loadStudentDetails(StudentCount) {
                    var html = '';
                    html += '<thead class="bg-primary"><tr><th>Year</th><th class="text-center">Count</th></tr></thead>';
                    html += '<tbody>';
                    if (StudentCount != null) {
                        if (StudentCount.length > 0) {
                            var TotalCount = 0;
                            $.each(StudentCount, function (row, item) {
                                TotalCount += Number(item.Count);
                                html += '<tr>';
                                html += '<td>' + item.Year + '</td>';
                                html += '<td class="text-right">' + item.Count + '</td>';
                                html += '</tr>';
                            });
                            html += '</tbody>';
                            html += '<tfoot><tr class="text-danger">';
                            html += '<td> Total Students </td>';
                            html += '<td class="text-right">' + TotalCount + '</td>';
                            html += '</tr></tfoot>';
                        } else {
                            html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
                        }
                    } else {
                        html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
                    }
                    $('#tblstudsCount').append(html);
                };

                $.ajax({

                    type: "POST",
                    //url: "principalHome.aspx/BindLeaveCount",
                    url: '<%=Page.ResolveUrl("~/principalHome.aspx/BindLeaveCount")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessLeaveDetails,
                failure: function (response) {
                }
            });

                function OnSuccessLeaveDetails(response) {
                    // console.log("Data", response);
                    //debugger;
                    var ToTal_Applied = response['d'].ToTal_Applied;
                    var Approve_Leave = response['d'].Approve_Leave;
                    var Pending_Leave = response['d'].Pending_Leave;
                    //loadleavecount(leavecount);

                    $('#lblToTal_Applied').html(ToTal_Applied);
                    //alert(ToTal_Applied);
                    $('#lblApprove_Leave').html(Approve_Leave);
                    $('#lblPending_Leave').html(Pending_Leave);

                };

                // added for Activity Data
                $.ajax({
                    type: "POST",
                    //url: "principalHome.aspx/BindActivityDetails",
                    url: '<%=Page.ResolveUrl("~/principalHome.aspx/BindActivityDetails")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessActivityDetails,
                failure: function (response) {
                }
            });
                function OnSuccessActivityDetails(response) {
                    var ActivityData = response['d'];
                    loadActivityDetails(ActivityData);
                };
                function loadActivityDetails(ActivityData) {
                    var html = '';

                    if (ActivityData != null) {
                        if (ActivityData.length > 0) {
                            $.each(ActivityData, function (row, item) {
                                html += '<tr>';
                                html += '<td class="text-left">' + item.ActivityName + '</td>';
                                html += '<td class="text-center">' + item.SessionName + '</td>';
                                html += '<td class="text-center">' + item.StartDate + '</td>';
                                html += '<td class="text-center">' + item.EndDate + '</td>';
                                html += '<td class="text-center">' + item.ActivityStatus + '</td>';
                                html += '</tr>';
                            });
                        } else {
                            html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                        }
                    } else {
                        html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr>';
                    }
                    $('#tbodyActivity').append(html);
                };

                // Added For Admission Fees Data
                //$.ajax({
                //    type: "POST",
                //    url: "principalHome.aspx/BindAdmFeesDetails",
                //    data: '{}',
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: OnSuccessAdmFeesDetails,
                //    failure: function (response) {
                //    }
                //});

                //function OnSuccessAdmFeesDetails(response) {
                //    var FeesData = response['d'];
                //    loadAdmFeesDetails(FeesData);
                //};
                //function loadAdmFeesDetails(FeesData) {
                //    $('#lblYear').hide();
                //    var totalCollection = 0.00;
                //    var html = '';
                //    html += '<thead class="bg-primary"><tr><th>Receipt</th><th class="text-right" style="padding-right:15px">Total</th></tr></thead>';
                //    html += '<tbody>';
                //    if (FeesData != null) {
                //        if (FeesData.length > 0) {
                //            $.each(FeesData, function (row, item) {
                //                $('#lblYear').text(' - (' + item.Year + ')');
                //                $('#lblYear').show();
                //                totalCollection += Number(item.Fee);
                //                html += '<tr>';
                //                html += '<td class="text-left">' + item.Receipt + '</td>';
                //                html += '<td class="text-right">&#8377; ' + item.Fee + '</td>';
                //                html += '</tr>';
                //            });
                //            html += '<tbody>';
                //            html += '<tfoot><tr class="text-danger">';
                //            html += '<td>Total Collection</td>';
                //            html += '<td class="text-right">&#8377 ' + totalCollection.toFixed(2) + '</td>';
                //            html += '</tr></tfoot>';
                //        } else {
                //            html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
                //        }
                //    } else {
                //        html += '<tr><td colspan="2" style="font-weight:bold"> No records available </td></tr></tbody>';
                //    }
                //    $('#tblAdmFees').append(html);
                //};

                //--------------------------------------------------------------------------------------------
                // Added For News Data
                //  $.ajax({
                //      type: "POST",
                //      //url: "principalHome.aspx/ShowNewsData",
                //      url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowNewsData")%>',
                //      data: '{}',
                //      contentType: "application/json; charset=utf-8",
                //      dataType: "json",
                //      success: OnSuccessNews,
                //      failure: function (response) {
                //          var html = '<div style="text-align:center; font-size:15px; font-weigth:bold" class="info">No records to display..</div>';
                //          $('#newsDiv').html(html);
                //      }
                //  });
                //  function OnSuccessNews(response) {
                //      loadNewsData(response['d']);
                //  };
                //  function loadNewsData(data) {
                //      var html = '';
                //      if (data != null) {
                //          if (data.length > 0) {
                //              $.each(data, function (row, item) {
                //                  if (item.Link != '') {
                //                      html += '<article class="media event"><a class="pull-left date"><p class="month">' + item.Month + '</p><p class="day">' + item.Day + '</p></a><div class="media-body"><a class="title" target="_blank" href=' + item.Link + '>' + item.Title + '</a><p>' + item.NewsDesc + '</p></div></article>';
                //                  }
                //                  else {
                //                      html += '<article class="media event"><a class="pull-left date"><p class="month">' + item.Month + '</p><p class="day">' + item.Day + '</p></a><div class="media-body"><a class="title" href="#">' + item.Title + '</a><p>' + item.NewsDesc + '</p></div></article>';
                //
                //                  }
                //              });
                //          } else {
                //              html += '<div style="font-size:15px" class="text-center" class="info" >No records to display..</div>';
                //          }
                //      } else {
                //          html += '<div style="font-size:15px" class="text-center" class="info">No records to display..</div>';
                //      }
                //      $('#newsDiv').html(html);
                //  };
                //--------------------------------------------------------------------------------------------


                // Added For Result analysis Data
                $.ajax({
                    type: "POST",
                    //url: "principalHome.aspx/ShowResultData",
                    url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowResultData")%>',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessResultData,
                failure: function (response) {
                }
            });
                function OnSuccessResultData(response) {
                    var resultdata = response['d'];
                    loadReslutAnalysisDetails(resultdata);
                };
                function loadReslutAnalysisDetails(resultdata) {
                    $('.analy').hide();
                    var html = '';
                    if (resultdata != null) {
                        if (resultdata.tHeader.length > 0) {
                            html += '<thead class="bg-primary"><tr><th with="20%">Degree/Branch</th>';
                            $.each(resultdata.tHeader, function (row, item) {
                                html += '<th class="text-center">' + item.Header + '</th>';
                            });
                            html += '</tr></thead>';
                            html += '<tbody>';

                            if (resultdata.tBody != null) {
                                if (resultdata.tBody.length > 0) {
                                    $.each(resultdata.tBody, function (row, item) {
                                        $('#lblResultSession').text('(' + item.SessionName + ')');
                                        $('.analy').show();
                                        html += '<tr>';
                                        html += '<td class="text-left bg-success" data-container="body"  data-toggle="tooltip" data-original-title="' + item.BranchName + ' - ' + item.DegreeName + '"><strong>' + item.BranchShortName + ' (' + item.DegreeName + ')</strong></td>';

                                        // if (resultdata.tHeader.length== 3) {
                                        //condition start--added by Nikhil V.Lambe on 01/12/2020 to specify between sem1,sem2,sem3,sem4
                                        if (resultdata.tHeader.length == 1) {
                                            html += '<td class="text-center">' + item.Sem1 + '</td>';
                                        }
                                        else if (resultdata.tHeader.length == 2) {
                                            html += '<td class="text-center">' + item.Sem1 + '</td>';
                                            html += '<td class="text-center">' + item.Sem2 + '</td>';
                                        }
                                        else if (resultdata.tHeader.length == 3) {
                                            html += '<td class="text-center">' + item.Sem1 + '</td>';
                                            html += '<td class="text-center">' + item.Sem2 + '</td>';
                                            html += '<td class="text-center">' + item.Sem3 + '</td>';
                                        }
                                        else if (resultdata.tHeader.length == 4) {
                                            html += '<td class="text-center">' + item.Sem1 + '</td>';
                                            html += '<td class="text-center">' + item.Sem2 + '</td>';
                                            html += '<td class="text-center">' + item.Sem3 + '</td>';
                                            html += '<td class="text-center">' + item.Sem4 + '</td>';
                                        }
                                        // condition end  


                                        html += '</tr>';
                                    });
                                    html += '<tbody>';
                                } else {
                                    html += '<tr><td colspan="5" style="font-weight:bold">There are no records available to display...</td></tr></tbody>';
                                }
                            } else {
                                html += '<tr><td colspan="5" style="font-weight:bold">There are no records available to display...</td></tr></tbody>';
                            }
                        } else {
                            html += '<thead class="bg-primary"><tr><th colspan="5">There are no records available to display...</th></tr></thead>';
                        }
                    } else {
                        html += '<thead class="bg-primary"><tr><th colspan="5">There are no records available to display...</th></tr></thead>';
                    }
                    $('#table-1').append(html);
                    $('[data-toggle="tooltip"]').tooltip({
                        placement: "top"
                    });
                };

            });


        /************************ Quick Access ************************/
        $.ajax({
            type: "POST",
            //url: "principalHome.aspx/ShowQuickAccessData",
            url: '<%=Page.ResolveUrl("~/principalHome.aspx/ShowQuickAccessData")%>',
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
                        html += '<li class="list-group-item"><a href="' + item.Link + '?pageno=' + item.PageNo + '"  runat="server"  target="_blank"><i class="fa fa-star"></i>' + item.LinkName + '</a></li>';
                        LinkCount += 1;
                    });
                } else {
                    html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No records to display..</li>';
                }
            } else {
                html += '<li class="list-group-item text-center" style="font-size:15px; font-weigth:bold; background-color: #d9edf7; ">No records to display..</li>';
            }
            $('#ulQuickAccess').append(html);
        }
        /************************ Quick Access ************************/


    </script>
  
    
     
    <div class="container-fluid">

        <section class="statistics">

            <div class="row gutters-sm">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-box">
                        <i class="fa fa-male fa-fw icon-sm" style="background-color: #c23531"></i>
                        <div class="info">
                            <h3><label id="lblMaleCount"></label></h3>
                            <span>Male</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-box">
                        <i class="fa fa-female fa-fw danger icon-sm" style="background-color: #2f4554"></i>
                        <div class="info">
                            <h3>
                                <label id="lblFemaleCount"></label>
                            </h3>
                            <span>Female</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-box">
                        <i class="fa fa-users fa-fw icon-sm" style="background-color: #61a0a8"></i>
                        <div class="info">
                            <h3>
                                <label id="lblActiveUser"></label>
                            </h3>
                            <span>Active Users</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-box">
                        <i class="fa fa-graduation-cap fa-fw warning icon-sm" style="background-color: #d48265"></i>
                        <div class="info">
                            <h3>
                                <label id="lblTotalStudent"></label>
                            </h3>
                            <span>Total Student</span>

                        </div>
                    </div>
                </div>
            </div>

        </section>

        <div class="row equalHMRWrap flex gutters-sm">
            <div style="display: none">
                <div class="col-md-4 col-sm-6 col-xs-12 flex">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Today's Time Table</h2>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">

                            <table class="table table-hover small padd-sm">
                                <thead>
                                    <tr>
                                        <th>Time</th>
                                        <th>Subject</th>
                                        <th>Branch</th>
                                        <th>Semester</th>
                                        <th>Section</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>10:00 am</td>
                                        <td>Applied Mathematics</td>
                                        <td>Electronics</td>
                                        <td>1<sup>st</sup> Sem</td>
                                        <td>Sec A</td>
                                    </tr>
                                    <tr>
                                        <td>12:00 am</td>
                                        <td>Applied Mathematics</td>
                                        <td>Mechanical</td>
                                        <td>1<sup>st</sup> Sem</td>
                                        <td>Sec C</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-2 col-sm-6 col-xs-12 flex">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Admission Year</h2>

                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-hover small" id="tblstudsCount">
                        </table>

                    </div>
                </div>
            </div>
            <%--<div class="col-md-3 col-sm-6 col-xs-12 flex">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Admission Fees <span id="lblYear" class="text-black"></span></h2>

                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-hover small" id="tblAdmFees">
                        </table>

                    </div>
                </div>
            </div>--%>
            <div class="col-md-3 col-sm-6 col-xs-12 flex">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Quick Access</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar small">
                        <ul class="list-group with-border-bottom fav-list" id="ulQuickAccess">
                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 flex">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Staff Leaves</h2>

                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">

                        <table class="table table-hover small">
                            <thead class="bg-primary">
                                <tr>
                                    <th>Leave Types</th>
                                    <th class="text-center">Total</th>
                                </tr>
                               <%-- <tr>
                                    <td>Applied Leaves</td>

                                </tr>
                                <tr>
                                    <td>Approved Leaves</td>
                                </tr>
                                <tr>
                                    <td>Pending Leaves</td>
                                </tr>--%>
                            </thead>
                            <tr> 
                                <td>Applied Leaves</td>
                                <td> <label id="lblToTal_Applied"> </label></td>
                            </tr>
                            <tr>
                                <td>Approved Leaves</td>
                                <td> <label id="lblApprove_Leave"></label></td>
                            </tr>
                            <tr>
                                <td>Pending Leaves</td>
                                <td> <label id="lblPending_Leave"> </label></td>
                            </tr>
                           
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 col-xs-12 flex">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Result Analysis <span id="lblResultSession" class="text-black analy" style="display: none"></span><span class="text-red analy" style="display: none"></span> </h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-hover small " id="table-1">
                        </table>

                    </div>
                </div>
            </div>

            <div style="display: none">
                <div class="col-md-3 col-sm-6 col-xs-12 flex">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>In / Out Time</h2>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">

                            <table class="table table-hover small padd-sm">
                                <thead>
                                    <tr>
                                        <th>Day</th>
                                        <th>In-Time</th>
                                        <th>Out-Time</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Mon</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Tue</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Wed</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Thur</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Fri</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>
                                    <tr>
                                        <td>Sat</td>
                                        <td>10:00 am</td>
                                        <td>5:00 pm</td>
                                    </tr>

                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
            <div style="display: none">
                <div class="col-md-2 col-sm-6 col-xs-12 flex">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Quick Access</h2>
                            <ul class="nav navbar-right panel_toolbox" style="display: none">
                                <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                </li>
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li><a href="#">Settings 1</a>
                                        </li>
                                        <li><a href="#">Settings 2</a>
                                        </li>
                                    </ul>
                                </li>
                                <li><a class="close-link"><i class="fa fa-close"></i></a>
                                </li>
                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">

                            <ul class="list-group with-border-bottom fav-list small">
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Online Fees Payment</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Student Grade Cards</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Subject Faculty Feedback</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Student Detail Search</a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Grievance Application </a></li>
                                <li class="list-group-item"><a href="#"><i class="fa fa-star"></i>Student Announcement</a></li>
                            </ul>

                        </div>

                    </div>
                </div>
            </div>

            <div style="display: none">
                <div class="col-md-3 col-sm-6 col-xs-12 flex">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Notice</h2>

                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <article class="media event">

                                <div class="media-body">
                                    <a class="title" href="#">Placement</a>
                                    <p>15 students from IT got selected in top MNC's.</p>
                                </div>
                            </article>
                            <article class="media event">
                                <div class="media-body">
                                    <a class="title" href="#">Item Two Title</a>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                </div>
                            </article>
                            <article class="media event">
                                <div class="media-body">
                                    <a class="title" href="#">Item Two Title</a>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row equalHMRWrap flex gutters-sm">
            <div class="col-md-9 col-sm-8 col-xs-12 flex">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Academic Activities</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-250 scrollbar">
                        <table class="table table-bordered  small">
                            <thead class="bg-primary">
                                <tr>
                                    <th style="width: 25%" class="text-center">Activity</th>
                                    <th class="text-center">Session</th>
                                    <th class="text-center">Start Date</th>
                                    <th class="text-center">End Date</th>
                                    <th class="text-center">Activity Status</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyActivity">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div style="display: none">
                <div class="col-md-3 col-sm-4 col-xs-12 flex">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Tasks</h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content height-250 scrollbar">
                            <ul class="list-group with-border-bottom">
                                <li class="list-group-item"><a href="#">Approve Leaves</a></li>
                                <li class="list-group-item"><a href="#">Mark Entry <span class="ncount">1</span></a></li>
                                <li class="list-group-item"><a href="#">Attendance</a></li>
                                <li class="list-group-item"><a href="#">Upload Lecture Notes</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-12 flex">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Active Notice/News</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-150 scrollbar">
                        <asp:ListView ID="lvActiveNotice" runat="server">
                            <LayoutTemplate>
                                <table class="table table-hover small padd-sm" id="tblNotice">
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                    
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
<article class="media event"><a class="pull-left date"><p class="month"><%#Eval("MM")%></p><p class="day"><%#Eval("DD")%></p></a>
<div class="media-body"><asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
    <p><%#Eval("NEWSDESC") %></p></div></article>
                                </tr>

                            </ItemTemplate>
                            </asp:ListView>
                    </div>
                     <div class="x_title">
                        <h2>Expired Notice/News</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content height-150 scrollbar">
                        <asp:ListView ID="lvExpNotice" runat="server">
                            <LayoutTemplate>
                                <table class="table table-hover small padd-sm" id="tblExpNotice">
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                    
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
<article class="media event"><a class="pull-left date"><p class="month"><%#Eval("MM")%></p><p class="day"><%#Eval("DD")%></p></a>
<div class="media-body"><asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" Text='<%#Eval("TITLE")%>' NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%#  GetFileName(Eval("FILENAME"))%></asp:HyperLink>
    <p><%#Eval("NEWSDESC") %></p></div></article>
                                </tr>

                            </ItemTemplate>
                            </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var tableOffset = $("#table-1").offset().top;
        var $header = $("#table-1 > thead").clone();
        var $fixedHeader = $("#header-fixed").append($header);

        $(window).bind("scroll", function () {
            var offset = $(this).scrollTop();

            if (offset >= tableOffset && $fixedHeader.is(":hidden")) {
                $fixedHeader.show();
            }
            else if (offset < tableOffset) {
                $fixedHeader.hide();
            }
        });
    </script>
</asp:Content>
