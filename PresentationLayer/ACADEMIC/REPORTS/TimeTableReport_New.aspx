<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TimeTableReport_New.aspx.cs" Inherits="ACADEMIC_TIMETABLE_TimeTableReport_New" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
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

    <asp:UpdatePanel ID="updTimeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="ValidationTimeTable"></asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Faculty"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please SelectCollege & Scheme" InitialValue="0" ValidationGroup="Section"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Course"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="room">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="FacultywiseTimetableRe">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="ValidationTimeTable">
                                        </asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Faculty">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvSession_Sec" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Section">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Course">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="room">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="FacultywiseTimetableRe">
                                        </asp:RequiredFieldValidator>
                                    </div>




                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="ValidationTimeTable"></asp:RequiredFieldValidator>--%>


                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Faculty"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvDegree_Sec" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Section"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Course"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="room">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>


                                        <%--                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="Faculty"></asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="ValidationTimeTable"></asp:RequiredFieldValidator>


                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="Section"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="Course"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="room">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>


                                        <%--                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="Faculty"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="ValidationTimeTable"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvscheme_sec" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="Section"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="Course"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="room">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                            TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Faculty">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="ValidationTimeTable">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvsem_Sec" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Section">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvsem_Course" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Course">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlSem"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="room">
                                        </asp:RequiredFieldValidator>

                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSem"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="FacultywiseTimetableRe">
                                        </asp:RequiredFieldValidator>






                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true">Section</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true"
                                            TabIndex="7" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="ValidationTimeTable">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Faculty">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Course">
                                        </asp:RequiredFieldValidator>

                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="FacultywiseTimetableRe">
                                        </asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <label for="city">Subject Type</label>
                                        <asp:DropDownList ID="ddlSubjectType" TabIndex="8" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">

                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            TabIndex="9" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlCourse"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="Faculty">
                                </asp:RequiredFieldValidator>--%>

                                        <%--  <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="Course">
                                </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trgpmodule" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Gp Module</label>
                                        </div>
                                        <asp:DropDownList ID="ddlGPModule" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" TabIndex="10"
                                            OnSelectedIndexChanged="ddlGPModule_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGPModule"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select GP Module " ValidationGroup="course">
                                        </asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trBatch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="11" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">

                                            <label>Teacher Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true"
                                            TabIndex="12" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged" AutoPostBack="True" data-select2-enable="true"
                                            CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--  <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Teacher" ValidationGroup="Faculty">
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <label for="city">Floor</label>
                                        <asp:DropDownList ID="ddlFloor" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="13">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <label for="city">Room</label>
                                        <asp:DropDownList ID="ddlRoom" runat="server" AppendDataBoundItems="true" TabIndex="14"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlRoom"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Room" ValidationGroup="room">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivSlotype"> 
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true">Slot Type</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSlotType" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="15" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSlotType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlSlotType"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Slot Type">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSlotType"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Slot Type">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSlotType"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Slot Type">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divExistsDate">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <label>Existing Dates </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExistingDates" runat="server" TabIndex="10" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlExistingDates_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>







                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="pnlFromDate">
                                        <div class="label-dynamic">
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="FromDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="16" CssClass="form-control pull-right" AutoPostBack="true"
                                                placeholder="From Date" ToolTip="Please Select From Date" OnTextChanged="txtFromDate_TextChanged" />

                                            <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="FromDate" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>

                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />

                                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator--%>>

                                        </div>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="pnlTodate">
                                        <div class="label-dynamic">
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="ToDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="17" ValidationGroup="submit" placeholder="To Date" AutoPostBack="true"
                                                ToolTip="Please Select To Date" CssClass="form-control pull-right" OnTextChanged="txtTodate_TextChanged" />

                                            <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTodate" PopupButtonID="ToDate" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />

                                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Cumulative"></asp:RequiredFieldValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="IncompleteFacultyAttendance"></asp:RequiredFieldValidator>--%>

                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDateDetails" runat="server" visible="false">
                                        <br />
                                        <p class="text-center" style="border-style: double; font-size: 14px; font-weight: bold; color: #3c8dbc;">
                                            <asp:Label ID="lblTitleDate" runat="server" Text="Selected Session Start & End Date :"></asp:Label>
                                        </p>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnExcel" runat="server" ToolTip="select Session ,Scheme,Course,Section,Batch,Subject type and Teacher Name"
                                    TabIndex="18" ValidationGroup="Course" OnClick="btnExcel_Click" CssClass="btn btn-info" Visible="false">
                                 Excel Report
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnTimeTableReport" runat="server" ValidationGroup="ValidationTimeTable"
                                    TabIndex="19" OnClick="btnTimeTableReport_Click" CssClass="btn btn-info">
                        Time Table Report Format - I
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnfacultywiseReport" runat="server" ToolTip="select session ,Degree and Teacher Name"
                                    TabIndex="20" OnClick="btnfacultywiseReport_Click" ValidationGroup="Faculty" Visible="false" CssClass="btn btn-info">
                        Faculty Wise Report
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCourseWise" runat="server"
                                    TabIndex="21" OnClick="btnCourseWise_Click" ValidationGroup="Course" CssClass="btn btn-info">
                         Time Table Report Format - II</asp:LinkButton>
                                <asp:LinkButton ID="btnReport" runat="server" TabIndex="21" OnClick="btnReport_Click" CssClass="btn btn-info">
                         Master Time Table</asp:LinkButton>
                                <asp:LinkButton ID="btnRoomWiseReport" runat="server" ToolTip="select upto Semester then Room"
                                    Visible="false" OnClick="btnRoomWiseReport_Click" ValidationGroup="room" CssClass="btn btn-info" TabIndex="22">
                         Room Wise Report</asp:LinkButton>
                             
                                <asp:LinkButton ID="btnSectionWiseReport" runat="server" ToolTip="selection till section is compulsory" Visible="false" TabIndex="23"
                                    OnClick="btnSectionWiseReport_Click" ValidationGroup="Section" CssClass="btn btn-info">
                        Section Wise Report
                                </asp:LinkButton>

                                <%-- Added by vipul Tichakule on date 21-03-2024 --%>
                                 <asp:Button ID="btnreportt" runat="server"  Text="Faculty Wise Time Table"
                                    TabIndex="23" class="btn btn-primary" ValidationGroup="FacultywiseTimetableRe"  OnClick="btnreportt_Click" Visible="false"/>
                                   <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Clear"
                                    TabIndex="23" class="btn btn-warning" />
                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="FacultywiseTimetableRe" />

                                <asp:ValidationSummary ID="valFacultyWiseReport" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Faculty" />

                                <asp:ValidationSummary ID="valSectionWiseReport" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Section" />

                                <asp:ValidationSummary ID="valCourseWise" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Course" />

                                <asp:ValidationSummary ID="valRoomWiseReport" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="room" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />

                                <asp:ValidationSummary ID="ValidationTimeTable1212" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationTimeTable" />
                            </div>

          

                            <div class="form-group col-lg-7 col-md-12 col-12" style="display: none">
                                <div class=" note-div">
                                    <h5 class="heading">Note (Please Select)</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>For Time Table report selection upto section is compulsory </span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>For faculty wise report selection of session,degree and teacher is compulsory</span>  </p>
                                    <%--<p><i class="fa fa-star" aria-hidden="true"></i><span>For room wise report selection upto semester then room is compulsory</span>  </p>--%>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
        <%-- <Triggers><asp:PostBackTrigger ControlID="imgCancel"/></Triggers>--%>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnreportt" />
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
