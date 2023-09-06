<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AcademicActivityReports.aspx.cs" Inherits="ACADEMIC_AcademicActivityReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFeeTable"
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
    </div>--%>
    <%-- <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet"/>
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                    enableHTML: true,
                    templates: {
                        filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                        filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                    }
                    //dropRight: true,
                    //search: true,
                });

            });
        });
    </script>
    <style>
        .multiselect-container.dropdown-menu  {
            height: 300px!Important;
            overflow: scroll!Important;
        }
    </style>
    <asp:UpdatePanel ID="upReports" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">FEES REPORT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-md-5 col-lg-5 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeSession" runat="server" Visible="false" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ListBox ID="lstbxCollegeSession" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="6"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-12 btn-footer mt-4 mb-5">

                            <asp:Button ID="btnOfferedCourseStatus" runat="server" CssClass="btn btn-info" Text="Offered Course Status" OnClick="btnOfferedCourseStatus_Click" />
                            <asp:Button ID="btnCourseRegStatus" Visible="false" runat="server" CssClass="btn btn-info" Text="Course Registration Status" OnClick="btnCourseRegStatus_Click" />
                            <asp:Button ID="btnCourseTeacherAllotment" runat="server" CssClass="btn btn-info" Text="Course Teacher Allotment Status" OnClick="btnCourseTeacherAllotment_Click" />
                            <asp:Button ID="btnTimeTableStatus" runat="server" CssClass="btn btn-info" Text="Time Table Status" OnClick="btnTimeTableStatus_Click" />
                            <asp:Button ID="btnTeachingAttendanceStatus" runat="server" CssClass="btn btn-info" Text="Teaching Plan & Attendance Status" OnClick="btnTeachingAttendanceStatus_Click" />
                            <asp:Button ID="btnTimeTableCancel" runat="server" CssClass="btn btn-info" Text="Cancel Time Table Report(Excel)" OnClick="btnTimeTableCancel_Click" />


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOfferedCourseStatus" />
            <asp:PostBackTrigger ControlID="btnCourseRegStatus" />
            <asp:PostBackTrigger ControlID="btnCourseTeacherAllotment" />
            <asp:PostBackTrigger ControlID="btnTimeTableStatus" />
            <asp:PostBackTrigger ControlID="btnTeachingAttendanceStatus" />
            <asp:PostBackTrigger ControlID="btnTimeTableCancel" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>


