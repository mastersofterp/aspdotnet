<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="selectCourse.aspx.cs" Inherits="Itle_selectCourse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .box-tools.pull-right {
            position: absolute;
            right: 10px;
            top: 4px;
            font-size: 12px;
        }
    </style>

    <style>
        .profesionaldiv {
            background-color: #e9eff9;
            padding: 20px;
            margin: 5px;
            width: 100%;
            border-radius: 10px;
            color: black;
            height: 118px;
            display: flex;
            align-items: center;
            justify-content: center;
            transition: 1s;
        }

        profesionaldiv.pointer {
            cursor: pointer;
        }

        .profesionaldiv:hover {
            background-color: #ffffff;
            transform: scale(1.1);
        }

        .profesionaldiv span {
            font-weight: 300;
        }

        .box {
        }
    </style>
    <style>
        .tablestyle {
            border-collapse: collapse;
        }
        /*.tablestyle.pointer {
                 cursor: pointer;
            }
        .tablestyle:hover {
                 background-color:#ffffff;
                 
            }*/



        .header {
            background-color: #fff;
            padding: 20px;
        }
    </style>
    <style>
        .tablestyletd {
            padding: 5px;
            background-color: #fff;
        }
    </style>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SELECT COURSE TO WORK IN ITLE SESSION</h3>
                            <div class="box-tools pull-right">
                                <%--<asp:ImageButton ID="imgNotify" ToolTip="Open Notifications" runat="server" Height="30px" ImageUrl="~/Images/Notification2.jpg"
                                    OnClick="imgNotify_Click" Width="35px" />--%>
                                <asp:LinkButton ID="imgNotify" ToolTip="Open Notifications" runat="server" OnClick="imgNotify_Click" CssClass="fa fa-bell notiList"></asp:LinkButton>
                            </div>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelectCourse" runat="server">
                                <asp:Panel ID="pnlCourse" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1" AutoPostBack="true" ToolTip="Select Session">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="trSession" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session Term</label>
                                                </div>
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True" Enabled="false" ForeColor="Green"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-12" id="pnllvCourseList" runat="server">
                                        <asp:Panel ID="pnlCourseList" runat="server">
                                            <asp:ListView ID="lvCourse" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Course Name</th>
                                                                <th>Subject Type</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnlnkSelect" runat="server" Text='<%# Eval("COURSENAME")%>'
                                                                CommandName='<%# Eval("CourseNo") %>' CommandArgument='<%# Eval("CourseNo") %>'
                                                                ToolTip="Click here Select Course"
                                                                OnClick="btnlnkSelect_Click" TabIndex="2"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBNAME")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                                    </p>
                                                </EmptyDataTemplate>
                                            </asp:ListView>

                                            <div class="row" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                <asp:Repeater ID="RpCourse" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <asp:LinkButton ID="btnlncardkSelect" runat="server"
                                                                CommandName='<%# Eval("COURSENAME") %>' CommandArgument='<%# Eval("CourseNo") %>'
                                                                ToolTip="Click here Select Course"
                                                                TabIndex="2" OnClick="btnlncardkSelect_Click">
                                                                <div class="profesionaldiv" style="box-shadow: rgba(0, 0, 0, 0.2) 1px 5px 5px; padding: 10px; margin-bottom: 15px; border-radius: 5px;">
                                                                    <div class="col-12">
                                                                        <div class="row">

                                                                            <div class="col-lg-9 col-md-6 col-12">
                                                                                <table class="tablestyle">
                                                                                    <tr>
                                                                                        <th>
                                                                                            <span style="color: #247dfd;"><%# Eval("COURSENAME")%>  </span> <span><%# Eval("SECTIONNAME")%> </span></th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th>Type:<span>  <%# Eval("SUBNAME")%> </span> 
                                                                                        </th>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th> <span><%# Eval("UA_FULLNAME")%> </span>
                                                                                        </th>

                                                                                    </tr>
                                                                                    

                                                                                </table>
                                                                            </div>
                                                                             <div class="col-lg-2 col-md-6 col-12">
                                                                              <asp:Image ID="Image1" runat="server" ImageUrl="~/images/degreecap.png" Width="70px"
                                                                                    Height="80px" />
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </div>

                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                                <div class="col-12 mt-4" id="divNotification" runat="server">
                                    <asp:Panel ID="pnlNotifications" runat="server" Visible="false">
                                        <div id="divGeneralEvents">
                                            <div class="sub-heading">
                                                <h5>General Notifications</h5>
                                            </div>
                                            <div class="table-responsive">
                                                <asp:Panel ID="pnlGeneralEvents" runat="server">
                                                    <div class="form-group col-md-12" id="trNewMail" runat="server" style="height: 40px" visible="false">
                                                        <br />
                                                        <div class="box-tools pull-left">
                                                            <asp:Image ID="imgMail" runat="server" ImageUrl="~/images/NewMail.GIF" Width="40px"
                                                                Height="36px" />
                                                            <br />
                                                        </div>
                                                        <asp:Label ID="lblComment" Text="You have " runat="server"></asp:Label>
                                                        <asp:Label ID="lblMailCount" runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblComment1" Text="new mail message!" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-12" id="trForum" style="height: 40px" visible="false" runat="server">
                                                        <br />
                                                        <div class="box-tools pull-left">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/NewForum2.jpg" Width="40px"
                                                                Height="36px" />
                                                        </div>
                                                        <div>
                                                            <%=NewForum%>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="col-12" id="tblUpcomingTest" runat="server">
                                            <div id="divUpcomingTests">
                                                <div class="sub-heading">
                                                    <h5>Upcoming Tests List</h5>
                                                </div>
                                                <asp:Panel ID="PnlList" runat="server">
                                                    <asp:ListView ID="lvTest" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Test Name</th>
                                                                        <th>Course Name</th>
                                                                        <th>Test Date</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%# Eval("TESTNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COURSE_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STARTDATE", "{0:dd-MMM-yyyy}")%> To <%# Eval("ENDDATE", "{0:dd-MMM-yyyy}")%>                                                                  
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            <p class="text-center text-bold">
                                                                <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                                            </p>
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="col-12" id="divAssignments">
                                            <div class="sub-heading">
                                                <h5>Assignments List</h5>
                                            </div>
                                            <asp:Panel ID="pnlAssignment" runat="server">
                                                <asp:ListView ID="lvNewAssignment" runat="server" Visible="false">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Assignment</th>
                                                                    <th>Course Name</th>
                                                                    <th>Students Replied</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("SUBJECT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("COURSE_NAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ASSIGNDATE", "{0:dd-MMM-yyyy}")%>                                                                  
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBMITDATE", "{0:dd-MMM-yyyy}")%>                                                                  
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            You don&#39;t have any Assignment                                                                                   
                                                        </p>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>

                                                <asp:ListView ID="lvFacultyAssingment" runat="server" Visible="false">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Assignment</th>
                                                                    <th>Course Name</th>
                                                                    <th>Students Replied</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("SUBJECT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("COURSE_NAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("studreply")%>                                                                
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            You don&#39;t have any Assignment                                                                                   
                                                        </p>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function PanelExpansion(imageCtl, divId) {

            if (document.getElementById(divId).style.display == "none") {
                alert('find');
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "block") {

                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../IMAGES/Notification1.jpg";
            }
        }
        function AssignmentExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }
    </script>


    <script type="text/javascript" language="javascript">
        function validate(coursename, imagem) {
            if (coursename != '') {
                //Swal.fire(
                //    '<i>You have selected the course ' + coursename + '<i>',
                //   '',
                //    'success'

                //    )
                Swal.fire({

                    title: '<i><font size="+1">You have selected the course ' + coursename + '<font><i>',
                    imageUrl: '' + imagem + '',
                    imageWidth: 100,
                    imageHeight: 100,
                    icon: 'success',
                })
                return false;
            }
            else {
                Swal.fire(

                  'Please Select Course .\n'

                  )
            }
        }
    </script>

</asp:Content>
