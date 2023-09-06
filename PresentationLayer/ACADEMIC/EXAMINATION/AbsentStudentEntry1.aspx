<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AbsentStudentEntry1.aspx.cs"
    Inherits="ACADEMIC_EXAMINATION_AbsentStudentEntry1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .myCss > thead > tr > th, .myCss > tbody > tr > th, .myCss > tfoot > tr > th, .myCss > thead > tr > td, .myCss > tbody > tr > td, .myCss > tfoot > tr > td {
            border: 1px solid #ddd;
        }

        .fixed {
            position: fixed;
            top: 0;
        }

        /* Tabs panel */
        .tabbable-panel {
            border: 1px solid #eee;
            padding: 10px;
        }

        /* Default mode */
        .tabbable-line > .nav-tabs {
            border: none;
            margin: 0px;
        }

            .tabbable-line > .nav-tabs > li {
                margin-right: 2px;
            }

                .tabbable-line > .nav-tabs > li > a {
                    border: 0;
                    margin-right: 0;
                    color: #737373;
                }

                    .tabbable-line > .nav-tabs > li > a > i {
                        color: #a6a6a6;
                    }

                .tabbable-line > .nav-tabs > li.open, .tabbable-line > .nav-tabs > li:hover {
                    border-bottom: 4px solid #fbcdcf;
                }

                    .tabbable-line > .nav-tabs > li.open > a, .tabbable-line > .nav-tabs > li:hover > a {
                        border: 0;
                        background: none !important;
                        color: #333333;
                    }

                        .tabbable-line > .nav-tabs > li.open > a > i, .tabbable-line > .nav-tabs > li:hover > a > i {
                            color: #a6a6a6;
                        }

                    .tabbable-line > .nav-tabs > li.open .dropdown-menu, .tabbable-line > .nav-tabs > li:hover .dropdown-menu {
                        margin-top: 0px;
                    }

                .tabbable-line > .nav-tabs > li.active {
                    border-bottom: 4px solid #3c8dbc;
                    position: relative;
                }

                    .tabbable-line > .nav-tabs > li.active > a {
                        border: 0;
                        color: #333333;
                    }

                        .tabbable-line > .nav-tabs > li.active > a > i {
                            color: #404040;
                        }

        .tabbable-line > .tab-content {
            margin-top: -3px;
            background-color: #fff;
            border: 0;
            border-top: 1px solid #eee;
            padding: 15px 0;
        }

        .portlet .tabbable-line > .tab-content {
            padding-bottom: 0;
        }
        /*END*/
    </style>

    <%-- <script src="../../jquery/AutoCompleteDD/jquery183.min.js"></script>
    <script src="../../jquery/AutoCompleteDD/jquery.searchabledropdown-1.0.8.min.js"></script>--%>

    <%--<script type="text/javascript">
        jq1833 = jQuery.noConflict();
        jq1833(document).ready(function () {
            jq1833("#<%=ddlExamDate.ClientID%>").searchable({
                maxListSize: 200, // if list size are less than maxListSize, show them all
                maxMultiMatch: 300, // how many matching entries should be displayed
                exactMatch: false, // Exact matching on search
                wildcards: true, // Support for wildcard characters (*, ?)
                ignoreCase: true, // Ignore case sensitivity
                latency: 200, // how many millis to wait until starting search
                warnMultiMatch: 'top {0} matches ...',
                warnNoMatch: 'no matches ...',
                zIndex: 'auto'
            });

            jq1833("#<%=ddlExamDate.ClientID%>").css('width', '');
            jq1833("#<%=ddlExamDate.ClientID%>").parent().css('width', '');
            jq1833("#<%=ddlExamDate.ClientID%>").css('height', '');

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            jq1833(document).ready(function () {
                jq1833("#<%=ddlExamDate.ClientID%>").searchable({
                    maxListSize: 200, // if list size are less than maxListSize, show them all
                    maxMultiMatch: 300, // how many matching entries should be displayed
                    exactMatch: false, // Exact matching on search
                    wildcards: true, // Support for wildcard characters (*, ?)
                    ignoreCase: true, // Ignore case sensitivity
                    latency: 200, // how many millis to wait until starting search
                    warnMultiMatch: 'top {0} matches ...',
                    warnNoMatch: 'no matches ...',
                    zIndex: 'auto'
                });

                jq1833("#<%=ddlExamDate.ClientID%>").css('width', '');
                jq1833("#<%=ddlExamDate.ClientID%>").parent().css('width', '');
                jq1833("#<%=ddlExamDate.ClientID%>").css('height', '');

            });

        });
    </script>--%>

    <%--   <script type="text/javascript">
        $(document).ready(function () {
            //Sticky Table Heads 
            var stickyOffset = $('.sticky').offset().top;
            $(window).scroll(function () {
                var sticky = $('.sticky'),
                    scroll = $(window).scrollTop();

                if (scroll >= stickyOffset) {
                    sticky.addClass('fixed');
                    $('.td1').width($('.th1').width());
                    $('.td2').width($('.th2').width());
                    $('.th3').width($('.td3').width());
                    $('.td4').width($('.th4').width());
                    $('.td5').width($('.th5').width());
                    $('.th6').width($('.td6').width());
                    $('.td7').width($('.th7').width());
                    $('.td8').width($('.th8').width());
                }
                else {
                    sticky.removeClass('fixed');
                }
            });
            //End
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {
                //Sticky Table Heads 
                var stickyOffset = $('.sticky').offset().top;
                $(window).scroll(function () {
                    var sticky = $('.sticky'),
                        scroll = $(window).scrollTop();

                    if (scroll >= stickyOffset) {
                        sticky.addClass('fixed');
                        $('.td1').width($('.th1').width());
                        $('.td2').width($('.th2').width());
                        $('.th3').width($('.td3').width());
                        $('.td4').width($('.th4').width());
                        $('.td5').width($('.th5').width());
                        $('.th6').width($('.td6').width());
                        $('.td7').width($('.th7').width());
                        $('.td8').width($('.th8').width());
                    }
                    else {
                        sticky.removeClass('fixed');
                    }
                });
                //End
            });

        });

    </script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row" id="divAbs" runat="server">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ABSENT STUDENT ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>School/College </label>
                                        </div>
                                        <asp:DropDownList ID="ddlcollege" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsessionforabsent" OnSelectedIndexChanged="ddlsessionforabsent_SelectedIndexChanged" data-select2-enable="true"
                                            runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsessionforabsent"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcourseforabset" runat="server" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlcourseforabset_SelectedIndexChanged" AutoPostBack="True" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlcourseforabset"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlexamnameabsentstudent" runat="server" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="EXTERMARK" CssClass="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlexamnameabsentstudent_SelectedIndexChanged" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlexamnameabsentstudent"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Sub Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubexamnameabsentstudent" runat="server" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Sub Exam Name" CssClass="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubexamnameabsentstudent_SelectedIndexChanged" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubexamnameabsentstudent"
                                            Display="None" ErrorMessage="Please Select Sub Exam Name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divexamtype" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlexam_type" AutoPostBack="true" runat="server" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Exam Type" CssClass="form-control"
                                            TabIndex="6">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">Backlog</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlexam_type"
                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3 d-none">
                                        <sup>* </sup>
                                        <label>Exam Date </label>
                                        <asp:DropDownList ID="ddlExamDate" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged"
                                            TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="ddlExamDate"
                                                    ValidationGroup="report" Display="None" ErrorMessage="Please Select Exam Date"
                                                    SetFocusOnError="true" InitialValue="0" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamDate"
                                                    ValidationGroup="Show" Display="None" ErrorMessage="Please Select Exam Date"
                                                    SetFocusOnError="true" InitialValue="0" />--%>
                                    </div>

                                    <div class="form-group col-md-3 d-none">
                                        <sup>* </sup>
                                        <label>Block </label>
                                        <asp:DropDownList ID="ddlblock" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlblock_SelectedIndexChanged" CssClass="form-control"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlblock"
                                            Display="None" ErrorMessage="Please Select Block" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-5 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Disabled CheckedBox Indicate 'I' GRADE Entry</span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%-- <div class="col-md-4">
                                        <div class="row">
                                            <fieldset class="fieldset" style="padding: 5px; color: Green">
                                                <legend style="text-align: center;border-top: dashed;border-bottom: dashed; font-size:large;">Note</legend>
                                                <span style="font-weight: bold; color: Red;">Save/Lock  : </span>
                                                <br />
                                                After Click on Submit Button, Click on Lock Button to Lock Absent
                                            <br />
                                                <span style="font-weight: bold; color: Red;">Present/Absent  : </span>
                                                <br />
                                                UnChecked for Absent Student.<br />
                                            </fieldset>
                                        </div>
                                    </div>--%>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="report"
                                    TabIndex="5" OnClick="btnShow_Click" CssClass="btn btn-primary" />

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                    OnClick="btnSubmit_Click" TabIndex="6" CssClass="btn btn-primary" ValidationGroup="report" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="7" CssClass="btn btn-warning" />

                                <asp:Button ID="btnAbsentReport1" runat="server" TabIndex="8" Text="Report" CssClass="btn btn-info"
                                    CausesValidation="false" OnClick="btnAbsentReport1_Click" />

                                <asp:Button ID="btnLock" runat="server" TabIndex="9" Text="Lock" CssClass="btn btn-primary"
                                    OnClick="btnLock_Click" BackColor="#FF9900" Style="display: none;" />

                                <asp:Button ID="btnReport" runat="server" TabIndex="100" Text="Report" CssClass="btn btn-info"
                                    OnClick="btnReport_Click1" Visible="False" />

                                <asp:Button ID="btnBlankDocket" runat="server" TabIndex="101"
                                    Text="Blank Docket" CssClass="btn btn-primary" OnClick="btnBlankDocket_Click" Visible="false" />

                                <asp:Button ID="btnDocket" runat="server" TabIndex="102" Text="Docket"
                                    CssClass="btn btn-primary" OnClick="btnDocket_Click" Visible="false" />


                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                            </div>

                            <div class="col-12" id="div_Result" runat="server" visible="false">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                    <asp:Repeater ID="rpt_Success" runat="server">
                                        <HeaderTemplate>
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th class="th1">
                                                        <center>SR.NO.</center>
                                                    </th>
                                                    <th class="th2">
                                                        <center>RRN</center>
                                                    </th>
                                                    <th class="th3">STUDENT NAME</th>
                                                    <th class="th4">
                                                        <center>SEMESTER</center>
                                                    </th>
                                                    <th class="th5">
                                                        <center>SECTION</center>
                                                    </th>
                                                    <th class="th6">FACULTY NAME</th>
                                                    <th class="th7">
                                                        <center>ABSENT</center>
                                                    </th>
                                                    <%-- <th class="th8" style="width: 7%">
                                                        <center>UFM</center>
                                                    </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <asp:HiddenField ID="hdf_IDNO" runat="server" Value='<%#Eval("IDNO")%>' />
                                                <asp:HiddenField ID="hdf_SEM" runat="server" Value='<%#Eval("SEMESTERNO")%>' />
                                                <asp:HiddenField ID="hdf_SEC" runat="server" Value='<%#Eval("SECTIONNO")%>' />
                                                <asp:HiddenField ID="hdf_SUBID" runat="server" Value='<%#Eval("SUBID")%>' />
                                                <td class="td1">
                                                    <center><%# Container.ItemIndex + 1 %></center>
                                                </td>
                                                <td class="td2" title='<%#Eval("IDNO")%>'>
                                                    <center><%#Eval("REGNO")%></center>
                                                </td>
                                                <td class="td3"><%#Eval("STUDNAME")%></td>
                                                <td class="td4">
                                                    <center><%#Eval("SEMESTERNAME")%></center>
                                                </td>
                                                <td class="td5">
                                                    <center><%#Eval("SECTIONNAME")%></center>
                                                </td>
                                                <td class="td6"><%#Eval("UA_FULLNAME") %></td>
                                                <%--<td class="td7">
                                                                <center><asp:CheckBox ID="chk_Absent" runat="server" class="chk_ab" Checked='<%# Eval("SMARK").ToString()=="902.00" ? true:false %>'  Enabled='<%# Convert.ToInt32(Eval("LOCK"))==0 ? true:false %>'/></center>
                                                            </td>
                                                            <td class="td8">
                                                                <center><asp:CheckBox ID="chk_Ufm" runat="server"  class="chk_uf" Checked='<%# Eval("SMARK").ToString()=="903.00" ? true:false %>' Enabled='<%# Convert.ToInt32(Eval("LOCK"))==0 ? true:false %>' /></center>
                                                            </td>--%>
                                                <td class="td7">
                                                    <%--<center><asp:CheckBox ID="chk_Absent" runat="server" class="chk_ab" Checked='<%# Eval("SMARK").ToString()=="902.00" ? true:false %>' Enabled='<%# Eval("GRADE").ToString().Equals("AB")?false:true %>'  /></center>--%>

                                                    <center><asp:CheckBox ID="chk_Absent" runat="server" class="chk_ab" Checked='<%# Eval("SMARK").ToString()=="902.00" ? true:false %>' Enabled='<%# (Eval("SMARK").ToString().Equals("902.00") || Eval("GRADE").ToString().Equals("I")) ? false : true %>'/></center>
                                                </td>
                                                <%--   <td class="td8">
                                                                <center><asp:CheckBox ID="chk_Ufm" runat="server"  class="chk_uf" Checked='<%# Eval("SMARK").ToString()=="903.00" ? true:false %>' Enabled='<%# Eval("GRADE").ToString().Equals("I")?false:true %>' /></center>
                                                            </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                </table>
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>Exam Roll No. </th>
                                                            <th>Student Name </th>
                                                            <th>Present/Absent Entry </th>
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
                                                <td><%# Eval("REGNO")%>
                                                    <asp:HiddenField ID="hfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td><%# Eval("STUDNAME")%></td>
                                                <td>
                                                    <asp:CheckBox ID="cbRow" runat="server" Checked='<%# Eval("EXAMMARKTYPE").ToString()== "-1" ? true:false 

%>'
                                                        Enabled='<%# Eval("LOCKFIELD").ToString() == "1" ? false:true %>' ToolTip='<%# Eval("IDNO")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 mt-4">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvMidsem" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Student List(Mid Exam)</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>Enrollment No. </th>
                                                            <th>Registration Type </th>
                                                            <th>Present/Absent Entry </th>
                                                            <th>UFM Entry </th>
                                                            <th>Withheld Entry </th>
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
                                                <td><%# Eval("REGNO")%>
                                                    <asp:HiddenField ID="hdfLock" runat="server" Value='<%# Eval("AB_CC_LOCK")%>' />
                                                    <asp:HiddenField ID="hfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td style="width: 15%"><%# Eval("EXAMTYPE")%></td>
                                                <td>
                                                    <asp:CheckBox ID="cbRow" runat="server" Checked='<%# Eval("S2MARK").ToString()=="-1" ? true:false %>'
                                                        ToolTip='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ChkUFM" runat="server" Checked='<%# Eval("S2MARK").ToString()=="403" ? true:false %>'
                                                        onclick="checkAll(this);" ToolTip='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ChkWithheld" runat="server" Checked='<%# Eval("S2MARK").ToString()=="402" ? true:false 

%>'
                                                        ToolTip='<%# Eval("IDNO")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
            <script type="text/javascript" language="javascript">
                function checkAll(chk) {
                    var chkin;
                    var chkboxid = chk.id;
                    var len = chkboxid.length;
                    if (chk.checked == true) {
                        if (len == 46) {
                            chkin = chkboxid.substring(0, 40) + 'cbRow';

                            if (document.getElementById('' + chkin + '').checked == true) {
                                chk.checked = true;
                            }
                            else {
                                alert('Student Cannot Be UFM Case As He Was Not Present In The Paper..!! Make Sure He Was Present In The Paper For UFM Case!!');
                                chk.checked = false;
                                return;
                            }
                        }
                        else if (len == 47) {
                            chkin = chkboxid.substring(0, 41) + 'cbRow';

                            if (document.getElementById('' + chkin + '').checked == true) {
                                chk.checked = true;
                            }
                            else {
                                alert('Student Cannot Be UFM Case As He Was Not Present In The Paper..!! Make Sure He Was Present In The Paper For UFM Case!!');
                                chk.checked = false;
                                return;
                            }
                        }
                        else
                            if (len == 48) {
                                chkin = chkboxid.substring(0, 42) + 'cbRow';

                                if (document.getElementById('' + chkin + '').checked == true) {
                                    chk.checked = true;
                                }
                                else {
                                    alert('Student Cannot Be UFM Case As He Was Not Present In The Paper..!! Make Sure He Was Present In The Paper For UFM Case!!');
                                    chk.checked = false;
                                    return;
                                }
                            }
                    }
                }

                function totAllSubjects(headchk) {

                    var frm = document.forms[0]
                    for (i = 0; i < document.forms[0].elements.length; i++) {
                        var e = frm.elements[i];
                        if (e.type == 'checkbox') {
                            if (headchk.checked == true)
                                e.checked = true;
                            else
                                e.checked = false;
                        }
                    }

                }
            </script>
            <script>
                $(".chk_up").change(function () {
                    //if ($(".chk_uf").attr("checked", false)) {
                    //    $(".chk_up").attr("checked", true);
                    //}
                    //else {
                    //    alert("You can't select both Upsent and UFM for single student !!");
                    //}
                    alert("ok");
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {

                    $(".chk_ab").change(function () {
                        if ($(this).children().prop('checked') == true) {
                            //$(".chk_uf").children().prop('checked', false);
                            $(this).parent().parent().parent().children().eq(11).find(".chk_uf").children().prop('checked', false);
                        }
                    });

                    $(".chk_uf").change(function () {
                        if ($(this).children().prop('checked') == true) {
                            //$(".chk_ab").children().prop('checked', false);
                            $(this).parent().parent().parent().children().eq(10).find(".chk_ab").children().prop('checked', false);
                        }
                    });

                });
            </script>
        </ContentTemplate>
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="btnAbsentReport1" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

