<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AcademicDashboard.aspx.cs" Inherits="ACADEMIC_EXAMINATION_AcademicDashboard" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<style>
        th {
            /*text-align: left !important;*/
            padding-top: 5px !important;
            padding-bottom: 5px !important;
            text-align: center !important;
        }

        td, th {
            border: 1px solid #ddd !important;
            padding: 2px !important;
        }

        sup {
            color: red !important;
        }
    </style>

    <script src='https://kit.fontawesome.com/a076d05399.js' crossorigin='anonymous'></script>

    <style>
        table.dataTable thead > tr > th.sorting_asc, table.dataTable thead > tr > th.sorting_desc, table.dataTable thead > tr > th.sorting, table.dataTable thead > tr > td.sorting_asc, table.dataTable thead > tr > td.sorting_desc, table.dataTable thead > tr > td.sorting
        {
            padding-right: 30px;
            background-color: #909090 !important;
            color: #FCFCFC;
        }
    </style>--%>

    <!-- Progress bar -->
    <style>
        .progress, .progress .progress-bar, .progress > .progress-bar, .progress > .progress-bar .progress-bar {
            border-radius: 5px !important;
        }

        .progress {
            height: 6px !important;
            margin-bottom: 12px;
            overflow: hidden;
            background-color: #ccc !important;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 2px rgba(0,0,0,.1);
            box-shadow: inset 0 1px 2px rgba(0,0,0,.1);
        }

        .progress-bar {
            float: left;
            width: 0%;
            height: 100%;
            font-size: 9px !important;
            line-height: 6px !important;
            color: #fff;
            text-align: center;
            background-color: #053769 !important;
            -webkit-box-shadow: inset 0 -1px 0 rgba(0,0,0,.15);
            box-shadow: inset 0 -1px 0 rgba(0,0,0,.15);
            -webkit-transition: width .6s ease;
            -o-transition: width .6s ease;
            transition: width .6s ease;
        }
    </style>

    <!-- Pending/Completed Class-->
    <%--<style>
       .Pending {
           color:red;
       }
       .Completed {
           color:green;
       }
   </style>--%>

    <style>
        .card-boxes {
            background: #fff;
            box-shadow: 0px 0px 5px #999;
            border-radius: 4px;
            padding: 15px;
        }

        .incomplete-status {
            color: #005ab5;
            font-weight: 600;
            padding-bottom: 5px;
        }

        .full-status {
            font-size: 12px;
            line-height: 1.4;
        }

        .labeltext {
            color: #f55407;
        }

        .ajax__tab_xp .ajax__tab_header {
            font-family: 'open_sansregular' !important;
            font-size: 13px !important;
            background: #fff !important;
            border-bottom: 1px solid #dee2e6;
        }

        .ajax__tab_xp .ajax__tab_outer {
            padding-right: 0px !important;
            background: #fff !important;
        }

        .ajax__tab_xp .ajax__tab_inner {
            padding-left: 0px !important;
            background: #fff !important;
        }

        .ajax__tab_xp .ajax__tab_tab {
            height: 35px !important;
            padding: 6px !important;
            margin: 0 !important;
            background: #fff !important;
        }

        .ajax__tab_xp .ajax__tab_tab {
            border: 1px solid transparent;
            border-top-left-radius: .25rem;
            border-top-right-radius: .25rem;
        }

            .ajax__tab_xp .ajax__tab_tab:hover {
                color: #255282 !important;
                background-color: #fff !important;
                border-top: 1px solid #255282 !important;
                border-color: #255282 #255282 #fff !important;
            }

        .ajax__tab_xp .ajax__tab_active .ajax__tab_tab {
            color: #255282 !important;
            background-color: #fff !important;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>
    <link href="../../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../plugins/multi-select/bootstrap-multiselect.js"></script>
    <style type="text/css">
        .ms-options-wrap,
        .ms-options-wrap * {
            box-sizing: border-box;
        }

            .ms-options-wrap > button:focus,
            .ms-options-wrap > button {
                position: relative;
                width: 100%;
                text-align: left;
                border: 1px solid #aaa;
                background-color: #fff;
                padding: 5px 20px 5px 5px;
                margin-top: 1px;
                font-size: 13px;
                color: #aaa;
                outline-offset: -2px;
                white-space: nowrap;
            }

                .ms-options-wrap > button > span {
                    display: inline-block;
                }

                .ms-options-wrap > button[disabled] {
                    background-color: #e5e9ed;
                    color: #808080;
                    opacity: 0.6;
                }

                .ms-options-wrap > button:after {
                    content: ' ';
                    height: 0;
                    position: absolute;
                    top: 50%;
                    right: 5px;
                    width: 0;
                    border: 6px solid rgba(0, 0, 0, 0);
                    border-top-color: #999;
                    margin-top: -3px;
                }

            .ms-options-wrap.ms-has-selections > button {
                color: #333;
            }

            .ms-options-wrap > .ms-options {
                position: absolute;
                left: 0;
                width: 100%;
                margin-top: 1px;
                margin-bottom: 20px;
                background: white;
                z-index: 2000;
                border: 1px solid #aaa;
                overflow: auto;
                visibility: hidden;
            }

            .ms-options-wrap.ms-active > .ms-options {
                visibility: visible;
            }

            .ms-options-wrap > .ms-options > .ms-search input {
                width: 100%;
                padding: 4px 5px;
                border: none;
                border-bottom: 1px groove;
                outline: none;
            }

            .ms-options-wrap > .ms-options .ms-selectall {
                display: inline-block;
                font-size: .9em;
                text-transform: lowercase;
                text-decoration: none;
            }

                .ms-options-wrap > .ms-options .ms-selectall:hover {
                    text-decoration: underline;
                }

            .ms-options-wrap > .ms-options > .ms-selectall.global {
                margin: 4px 5px;
            }

            .ms-options-wrap > .ms-options > ul,
            .ms-options-wrap > .ms-options > ul > li.optgroup ul {
                list-style-type: none;
                padding: 0;
                margin: 0;
            }

                .ms-options-wrap > .ms-options > ul li.ms-hidden {
                    display: none;
                }

                .ms-options-wrap > .ms-options > ul > li.optgroup {
                    padding: 5px;
                }

                    .ms-options-wrap > .ms-options > ul > li.optgroup + li.optgroup {
                        border-top: 1px solid #aaa;
                    }

                    .ms-options-wrap > .ms-options > ul > li.optgroup .label {
                        display: block;
                        padding: 5px 0 0 0;
                        font-weight: bold;
                    }

                .ms-options-wrap > .ms-options > ul label {
                    position: relative;
                    display: inline-block;
                    width: 100%;
                    padding: 4px 4px 4px 20px;
                    margin: 1px 0;
                    border: 1px dotted transparent;
                }

            .ms-options-wrap > .ms-options.checkbox-autofit > ul label,
            .ms-options-wrap > .ms-options.hide-checkbox > ul label {
                padding: 4px;
            }

            .ms-options-wrap > .ms-options > ul label.focused,
            .ms-options-wrap > .ms-options > ul label:hover {
                background-color: #efefef;
                border-color: #999;
            }

            .ms-options-wrap > .ms-options > ul li.selected label {
                background-color: #efefef;
                border-color: transparent;
            }

            .ms-options-wrap > .ms-options > ul input[type="checkbox"] {
                margin: 0 5px 0 0;
                position: absolute;
                left: 4px;
                top: 7px;
            }

            .ms-options-wrap > .ms-options.hide-checkbox > ul input[type="checkbox"] {
                position: absolute !important;
                height: 1px;
                width: 1px;
                overflow: hidden;
                clip: rect(1px 1px 1px 1px);
                clip: rect(1px, 1px, 1px, 1px);
            }
    </style>
    <%-- End --%>
    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }

        #ms-list-1 ul li {
            list-style-type: none;
        }
    </style>
    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>
    <!-- jQuery library -->
    <script type="text/javascript">

        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }
            function InitAutoCompl() {
                $('select[multiple]').val('').multiselect({
                    columns: 5,     // how many columns should be use to show options            
                    search: true, // include option search box
                    searchOptions: {
                        delay: 250,                         // time (in ms) between keystrokes until search happens
                        clearSelection: true,
                        showOptGroups: false,                // show option group titles if no options remaining

                        searchText: true,                 // search within the text

                        searchValue: true,                // search within the value

                        onSearch: function (element) { } // fires on keyup before search on options happens
                    },

                    // plugin texts

                    texts: {

                        placeholder: 'Select Semester', // text to use in dummy input

                        search: 'Search',         // search input placeholder text

                        selectedOptions: ' selected',      // selected suffix text

                        selectAll: 'Select all Semester',     // select all text

                        unselectAll: 'Unselect all Semester',   // unselect all text

                        //noneSelected: 'None Selected'   // None selected text

                    },

                    // general options

                    selectAll: true, // add select all option

                    selectGroup: false, // select entire optgroup

                    minHeight: 200,   // minimum height of option overlay

                    maxHeight: null,  // maximum height of option overlay

                    maxWidth: null,  // maximum width of option overlay (or selector)

                    maxPlaceholderWidth: null, // maximum width of placeholder button

                    maxPlaceholderOpts: 10, // maximum number of placeholder options to show until "# selected" shown instead

                    showCheckbox: true,  // display the checkbox to the user

                    optionAttributes: [],
                });
            }
        });

    </script>
    <script>
        function getVal() {
            var array = []
            var semNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (semNo == undefined) {
                    semNo = checkboxes[i].value + ',';
                }
                else {
                    semNo += checkboxes[i].value + ',';
                }
            }
            //alert(semNo);
            if ($('[id*=ctl00_ContentPlaceHolder1_ddlexamSession]').val() == 0) {
                alert('Please select Session.')
                return false
            }
            //alert($('[id*=ctl00_ContentPlaceHolder1_ddlexamSession]').val());
            //alert($('[id*=ctl00_ContentPlaceHolder1_ddlSession]').val());
            if ($('[id*=ctl00_ContentPlaceHolder1_ddlSession]').val() == 0) {
                alert('Please select School/Institute.')
                return false
            }
            if (semNo == undefined) {
                alert('Please select atleast one Semester.')
                return false
            }
            else {

                $('#<%= hdnsemesterno.ClientID %>').val(semNo);
                return true;
            }
            //document.getElementById(inpHide).value = "degreeNo";
        }
    </script>
    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                // document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    // document.getElementById('load').style.visibility = "hidden";
                    // document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAcademicDashboard"
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

    <asp:UpdatePanel runat="server" ID="updAcademicDashboard">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Academic/Exam Dashboard</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlexamSession" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            placeholder="Please Select Session" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlexamSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute</label>
                                            <%--<asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            placeholder="Please Select School/Institute." runat="server" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlCollege" ControlToValidate="ddlSession" InitialValue="0"
                                            Display="None" ValidationGroup="Show" runat="server" ErrorMessage="Please select School/Institute."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="showSemester" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlInstMultiCheck" runat="server" CssClass="form-control " TabIndex="2" multiple="multiple">
                                        </asp:DropDownList>
                                        <%--<asp:ListBox ID="ddlInstMultiCheck" runat="server" SelectionMode="Multiple"   
                                          CssClass="form-control multi-select-demo" AppendDataBoundItems="true">                                            
                                          </asp:ListBox>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="Show" OnClientClick="return getVal();" OnClick="btnShow_OnClick" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        <asp:HiddenField ID="hdnsemesterno" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-md-12" style="text-align: left">
                                <div style="color: Red; font-weight: bold;">
                                    <asp:Label ID="lblmag" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div runat="server" id="divProgressbar" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-2 col-md-4 col-12 pl-1 pr-1">
                                            <div class="card-boxes">
                                                <div class="label-dynamic">
                                                    <label>Course Registration</label>
                                                </div>
                                                <asp:Label ID="lblCourseRegPercentage" runat="server"></asp:Label>
                                                <div class="progress" style="display: block">
                                                    <div class="progress-bar progress-bar-warning" role="progressbar" id="ProgressCourseRegistration" aria-valuenow="90" runat="server"
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    </div>
                                                </div>
                                                <div class="incomplete-status">
                                                    <asp:Label ID="lblCourseRegIncompleteStatus" runat="server"></asp:Label>
                                                </div>
                                                <div class="full-status">
                                                    <asp:Label ID="lblCourseRegFullStatus" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-2 col-md-4 col-12 pl-1 pr-1">
                                            <div class="card-boxes">
                                                <div class="label-dynamic">
                                                    <label>Teacher Allotment</label>
                                                </div>
                                                <asp:Label ID="lblTeacherAllotmentPercentage" runat="server"></asp:Label>
                                                <div class="progress" style="display: block">
                                                    <div class="progress-bar progress-bar-warning" role="progressbar" id="progressTeacherAllotment" aria-valuenow="90" runat="server"
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    </div>
                                                </div>
                                                <div class="incomplete-status">
                                                    <asp:Label ID="lblIncompleteTeacherAllotment" runat="server"></asp:Label>
                                                </div>
                                                <div class="full-status">
                                                    <asp:Label ID="lblTeacharAllotmentFullStatus" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-2 col-md-4 col-12 pl-1 pr-1">
                                            <div class="card-boxes">
                                                <div class="label-dynamic">
                                                    <label>Class Time Table</label>
                                                </div>
                                                <asp:Label ID="lblClassTimeTablePercentage" runat="server"></asp:Label>
                                                <div class="progress" style="display: block">
                                                    <div class="progress-bar progress-bar-warning" role="progressbar" id="progressClassTimeTable" aria-valuenow="90" runat="server"
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    </div>
                                                </div>
                                                <div class="incomplete-status">
                                                    <asp:Label ID="lblIncompleteClassTimeTable" runat="server"></asp:Label>
                                                </div>
                                                <div class="full-status">
                                                    <asp:Label ID="lblClassTimeTableFullStatus" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-2 col-md-4 col-12 pl-1 pr-1">
                                            <div class="card-boxes">
                                                <div class="label-dynamic">
                                                    <label>Exam Time Table</label>
                                                </div>
                                                <asp:Label ID="lblExamTimeTablePercentage" runat="server"></asp:Label>
                                                <div class="progress" style="display: block">
                                                    <div class="progress-bar progress-bar-warning" role="progressbar" id="progressExamTimeTable" aria-valuenow="90" runat="server"
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    </div>
                                                </div>
                                                <div class="incomplete-status">
                                                    <asp:Label ID="lblIncompleteExamTimeTable" runat="server"></asp:Label>
                                                </div>
                                                <div class="full-status">
                                                    <asp:Label ID="lblExamTimeTableFullStatus" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-2 col-md-4 col-12 pl-1 pr-1">
                                            <div class="card-boxes">
                                                <div class="label-dynamic">
                                                    <label>Exam Registration</label>
                                                </div>
                                                <asp:Label ID="lblExamRegistrationPercentage" runat="server"></asp:Label>
                                                <div class="progress" style="display: block">
                                                    <div class="progress-bar progress-bar-warning" role="progressbar" id="progressExamRegistration" aria-valuenow="90" runat="server"
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    </div>
                                                </div>
                                                <div class="incomplete-status">
                                                    <asp:Label ID="lblIncompleteExamRegistration" runat="server"></asp:Label>
                                                </div>
                                                <div class="full-status">
                                                    <asp:Label ID="lblExamRegistrationFullStatus" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-2 col-md-4 col-12 pl-1 pr-1">
                                            <div class="card-boxes">
                                                <div class="label-dynamic">
                                                    <label>Result Processing</label>
                                                </div>
                                                <asp:Label ID="lblResultProcessPercentage" runat="server"></asp:Label>
                                                <div class="progress" style="display: block">
                                                    <div class="progress-bar progress-bar-warning" role="progressbar" id="progressResultProcessing" aria-valuenow="90" runat="server"
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    </div>
                                                </div>
                                                <div class="incomplete-status">
                                                    <asp:Label ID="lblIncompleteResultprocess" runat="server"></asp:Label>
                                                </div>
                                                <div class="full-status">
                                                    <asp:Label ID="lblResultProcessFullStatus" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server" id="divAcademicDashboard" visible="false">
                                <div class="row">
                                    <div style="align-content: center" class="col-md-12">
                                        <div class="form-group">
                                            <ajaxToolKit:TabContainer ID="tabContainerAcademicDashboard" runat="server" Width="100%" ActiveTabIndex="4" OnActiveTabChanged="tabContainerAcademicDashboard_ActiveTabChanged" AutoPostBack="true">

                                                <!--Course Registration Progress -->
                                                <ajaxToolKit:TabPanel ID="tabCourseRegistrationProgress" Style="padding-right: 10px" TabIndex="1" runat="server" HeaderText="Course Registration">
                                                    <HeaderTemplate>
                                                        Course Registration
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-info">
                                                                <div class="panel-body">
                                                                    <hr />
                                                                    <div id="divCourseRegistrationProgress" class="col-sm-12" runat="server">
                                                                        <div class="panel panel-primary">
                                                                            <div class="panel-heading">
                                                                                <span class="glyphicon glyphicon-list-alt"></span>
                                                                                <span>Course Registration Progress</span>
                                                                            </div>
                                                                            <div class="panel-body" id="divCourseRegistrationDetail" style="display: block">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div style="padding: 10px">
                                                                                            <asp:GridView ID="gvParent" runat="server" DataKeyNames="COLLEGE_ID" Width="100%"
                                                                                                CssClass="datatable" AutoGenerateColumns="False"
                                                                                                BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParent_RowDataBound" GridLines="Horizontal" EmptyDataText="There are no data records to display.">

                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="Black" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="COLLEGE_NAME" HeaderText="School/Institute Name">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle HorizontalAlign="Left" Width="35%" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="TOTAL_COUNT" HeaderText="Total Course Registration Count">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="COURSE_REG_COMPLETE_COUNT" HeaderText="Complete Course Registration Count">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:TemplateField HeaderText="Pending Course Registration Count">
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divcR" runat="server">
                                                                                                                <a href="JavaScript:divexpandcollapse('divCourseRegistrationDepartment<%# Eval("COLLEGE_ID") %>');">
                                                                                                                    <asp:Label ID="lblCourseRegProcess" CssClass='<%# Convert.ToInt32(Eval("COURSE_REG_PENDING_COUNT"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("COURSE_REG_PENDING_COUNT") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%">
                                                                                                                    <div id='divCourseRegistrationDepartment<%# Eval("COLLEGE_ID") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                        <asp:GridView ID="gvChildCourseRegistrationDepartment" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                            CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChildCourseRegistrationDepartment_RowDataBound"
                                                                                                                            Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                            <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                            <RowStyle />
                                                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                            <Columns>
                                                                                                                                <asp:BoundField DataField="DEPTNAME" ItemStyle-Width="35%" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="LEFT" />
                                                                                                                                <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="20%" HeaderText="Total Course Registration Count" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:BoundField DataField="COURSE_REG_COMPLETE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Course Registration Count" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Course Registration Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <div id="divcR" runat="server">
                                                                                                                                            <a href="JavaScript:divexpandcollapse('div4<%# Eval("DEPTNOSRNO") %>');">
                                                                                                                                                <asp:Label ID="lblCourseRegProcess" CssClass='<%# Convert.ToInt32(Eval("COURSE_REG_PENDING_COUNT"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("COURSE_REG_PENDING_COUNT") %>'></asp:Label>
                                                                                                                                                <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                        </div>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField HeaderText="Send E-Mail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Button ID="btnSendCourseRegistrationMail" Visible='<%# Convert.ToInt32(Eval("COURSE_REG_PENDING_COUNT"))==0 ?false:true %>' OnClick="btnSendCourseRegistrationMail_OnClick" CommandArgument='<%# Eval("DEPTNO") %>' Text="Send Mail" ToolTip="Send Mail" runat="server" />
                                                                                                                                    </ItemTemplate>
                                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                                    <ItemStyle Width="10%" />
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="100%">
                                                                                                                                                <div id='div4<%# Eval("DEPTNOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                    <asp:GridView ID="gvChild" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                        CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_RowDataBound"
                                                                                                                                                        Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                        <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                        <RowStyle />
                                                                                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                        <Columns>

                                                                                                                                                            <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="13%" HeaderText="Scheme Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                            <asp:BoundField DataField="STUDENT_COUNT" ItemStyle-Width="5%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="Center" />

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstH" Style="text-align: center" Text="1st Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondtH3rd" Style="text-align: center" Text="3rd Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondtH5th" Style="text-align: center" Text="5th Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondtH7th" Style="text-align: center" Text="7th Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstH2nd" Style="text-align: center" Text="2nd Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondtH4th" Style="text-align: center" Text="4th Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondtH6th" Style="text-align: center" Text="6th Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondtH8th" Style="text-align: center" Text="8th Sem Course Registration" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCourseRegistrationSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div id="divcR" runat="server">
                                                                                                                                                                        <a href="JavaScript:divexpandcollapse('divCourseRegistration<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                            <asp:Label ID="lblCourseRegProcess" CssClass='<%# Convert.ToInt32(Eval("PENDINGCOUNT"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("PENDINGCOUNT") %>'></asp:Label>
                                                                                                                                                                            <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                                            <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                                            <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="100%">
                                                                                                                                                                            <div id='divCourseRegistration<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                <asp:GridView ID="gvChild_StudentList" runat="server" DataKeyNames="SCHEMENO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                    CssClass="datatable" BorderColor="#f5511e"
                                                                                                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <RowStyle />
                                                                                                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Sr. No."
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <%# Container.DataItemIndex+1 %>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                                                        <asp:BoundField DataField="REGNO" ItemStyle-Width="20%" HeaderText="Registration No." HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                        <asp:BoundField DataField="STUDNAME" ItemStyle-Width="30%" HeaderText="Student Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="LEFT" />

                                                                                                                                                                                        <asp:BoundField DataField="LONGNAME" ItemStyle-Width="30%" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                        <asp:BoundField DataField="SEMESTERNAME" ItemStyle-Width="10%" HeaderText="Semester" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                    </Columns>
                                                                                                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                </asp:GridView>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                        </Columns>
                                                                                                                                                        <%--  <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />--%>
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                            </Columns>
                                                                                                                            <%-- <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />--%>
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolKit:TabPanel>

                                                <!--Teacher Allotment Status-->
                                                <ajaxToolKit:TabPanel ID="tabTeacherAllotmentStatus" runat="server" TabIndex="2" HeaderText="Teacher Allotment" Style="padding-left: 10px;">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-info">
                                                                <div class="panel-body">
                                                                    <hr />
                                                                    <div id="divHTeacherAllotmentStatus" class="col-sm-12" runat="server">
                                                                        <div class="panel panel-primary">
                                                                            <div class="panel-heading">
                                                                                <span class="glyphicon glyphicon-list-alt"></span>
                                                                                <span>Teacher Allotment Status</span>
                                                                            </div>
                                                                            <div class="panel-body" id="divTeacherAllotmentStatus" style="display: block">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div style="padding: 10px">
                                                                                            <asp:GridView ID="gvParent_TeacherAllotment" runat="server" DataKeyNames="DEPTNO" Width="100%"
                                                                                                CssClass="datatable" AutoGenerateColumns="false"
                                                                                                BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParent_TeacherAllotment_RowDataBound" GridLines="Horizontal"
                                                                                                ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="DEPTNAME" ItemStyle-Width="35%" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="LEFT" />
                                                                                                    <asp:BoundField DataField="TOTAL_COURSE_COUNT" ItemStyle-Width="20%" HeaderText="Total Teacher Allotment" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="COMPLETED_COURSE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Teacher Allotment" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Teacher Allotment"
                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divcR" runat="server">
                                                                                                                <a href="JavaScript:divexpandcollapse('div5<%# Eval("DEPTNO") %>');">
                                                                                                                    <asp:Label ID="lblTeacherAllotment" CssClass='<%# Convert.ToInt32(Eval("PENDING_COUNT"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("PENDING_COUNT") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Send E-Mail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Button ID="btnSendTeacherAllotmentMail" Visible='<%# Convert.ToInt32(Eval("PENDING_COUNT"))==0 ?false:true %>' OnClick="btnSendTeacherAllotmentMail_OnClick" CommandArgument='<%# Eval("DEPTNO") %>' Text="Send Mail" ToolTip="Send Mail" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="10%" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%">
                                                                                                                    <div id='div5<%# Eval("DEPTNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                        <asp:GridView ID="gvChild_TeacherAllotment" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                            CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_TeacherAllotment_RowDataBound"
                                                                                                                            Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                            <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                            <RowStyle />
                                                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                            <Columns>
                                                                                                                                <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="13%" HeaderText="Scheme Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                                                                                <asp:BoundField DataField="TOTAL_COURSE_COUNT" ItemStyle-Width="5%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstH" Style="text-align: center" Text="1st Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondtH3rd" Style="text-align: center" Text="3rd Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondtH5th" Style="text-align: center" Text="5th Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondtH7th" Style="text-align: center" Text="7th Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstH2nd" Style="text-align: center" Text="2nd Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondtH4th" Style="text-align: center" Text="4th Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondtH6th" Style="text-align: center" Text="6th Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondtH8th" Style="text-align: center" Text="8th Sem Teacher Allotment" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblTeacherAllotmentSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderText="Total Pending"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <div id="divcR" runat="server">
                                                                                                                                            <a href="JavaScript:divexpandcollapse('div6<%# Eval("SCHEMENO") %>');">
                                                                                                                                                <asp:Label ID="lblTeacherAllotment_Subject" CssClass='<%# Convert.ToInt32(Eval("TOTAL_PENDING"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("TOTAL_PENDING") %>'></asp:Label>
                                                                                                                                                <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                        </div>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="100%">
                                                                                                                                                <div id='div6<%# Eval("SCHEMENO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                    <asp:GridView ID="gvChild_TeacherAllotment_Subject" runat="server" DataKeyNames="SCHEMENO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                        CssClass="datatable" BorderColor="#f5511e"
                                                                                                                                                        Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                                                        <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                        <RowStyle />
                                                                                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:BoundField DataField="COURSENAME" ItemStyle-Width="70%" HeaderText="Subject Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                            <asp:BoundField DataField="SUBNAME" ItemStyle-Width="30%" HeaderText="Subject Type" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                        </Columns>
                                                                                                                                                        <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                            </Columns>
                                                                                                                            <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolKit:TabPanel>

                                                <!--Class Time Table Status -->
                                                <ajaxToolKit:TabPanel ID="tabClassTimeTableStatus" runat="server" TabIndex="3" HeaderText="Class Time Table">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-info">
                                                                <div class="panel-body">
                                                                    <hr />
                                                                    <div id="divHClassTimeTableStatus" class="col-sm-12" runat="server">
                                                                        <div class="panel panel-primary">
                                                                            <div class="panel-heading">
                                                                                <span class="glyphicon glyphicon-list-alt"></span>
                                                                                <span>Class Time Table Status</span>
                                                                            </div>
                                                                            <div class="panel-body" id="divClassTimeTableStatus" style="display: block">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div style="padding: 10px">
                                                                                            <asp:GridView ID="gvParent_ClassTimeTable" runat="server" DataKeyNames="DEPTNO" Width="100%"
                                                                                                CssClass="datatable" AutoGenerateColumns="false"
                                                                                                BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParent_ClassTimeTable_RowDataBound" GridLines="Horizontal"
                                                                                                ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="DEPTNAME" ItemStyle-Width="35%" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="LEFT" />
                                                                                                    <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="20%" HeaderText="Total Class Time Table" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="COMPLETE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Class Time Table" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Class Time Table"
                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divcR" runat="server">
                                                                                                                <a href="JavaScript:divexpandcollapse('div7<%# Eval("DEPTNO") %>');">
                                                                                                                    <asp:Label ID="lblClassTimeTable" runat="server" CssClass='<%# Convert.ToInt32(Eval("PENDING_COUNT"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("PENDING_COUNT") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Send E-Mail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Button ID="btnSendClassTimeTableMail" Visible='<%# Convert.ToInt32(Eval("PENDING_COUNT"))==0 ?false:true %>' OnClick="btnSendClassTimeTableMail_OnClick" CommandArgument='<%# Eval("DEPTNO") %>' Text="Send Mail" ToolTip="Send Mail" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="10%" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%">
                                                                                                                    <div id='div7<%# Eval("DEPTNO") %>' style="display: none; position: relative; left: 30px; overflow: auto">
                                                                                                                        <asp:GridView ID="gvChild_ClassTimeTable" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                            CssClass="datatable" BorderColor="#f5511e"
                                                                                                                            Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                            <%-- OnRowDataBound="gvChild_RowDataBound"--%>
                                                                                                                            <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                            <RowStyle />
                                                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                            <Columns>

                                                                                                                                <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="16%" HeaderText="Scheme Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                    ItemStyle-HorizontalAlign="left" />
                                                                                                                                <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="8%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstH" Style="text-align: center" Text="1st Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondtH3rd" Style="text-align: center" Text="3rd Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondtH5th" Style="text-align: center" Text="5th Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondtH7th" Style="text-align: center" Text="7th Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstH2nd" Style="text-align: center" Text="2nd Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondtH4th" Style="text-align: center" Text="4th Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondtH6th" Style="text-align: center" Text="6th Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondtH8th" Style="text-align: center" Text="8th Sem Class Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblClassTimeTableSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                            </Columns>
                                                                                                                            <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolKit:TabPanel>

                                                <!--Exam Time Table Status -->
                                                <ajaxToolKit:TabPanel ID="tabExamTimeTableStatus" runat="server" TabIndex="4" HeaderText="Exam Time Table">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-info">
                                                                <div class="panel-body">
                                                                    <hr />
                                                                    <div id="divHExamTimeTableStatus" class="col-sm-12" runat="server">
                                                                        <div class="panel panel-primary">
                                                                            <div class="panel-heading">
                                                                                <span class="glyphicon glyphicon-list-alt"></span>
                                                                                <span>Exam Time Table Status</span>
                                                                            </div>
                                                                            <div class="panel-body" id="divExamTimeTableStatus" style="display: block">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div style="padding: 10px">
                                                                                            <asp:GridView ID="gvParent_ExamTimeTable" runat="server" DataKeyNames="DEPTNO" Width="100%"
                                                                                                CssClass="datatable" AutoGenerateColumns="false"
                                                                                                BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParent_ExamTimeTable_RowDataBound" GridLines="Horizontal"
                                                                                                ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="DEPTNAME" ItemStyle-Width="35%" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="LEFT" />
                                                                                                    <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="20%" HeaderText="Total Exam Time Table" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="COMPLETE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Exam Time Table" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Exam Time Table"
                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divcR" runat="server">
                                                                                                                <a href="JavaScript:divexpandcollapse('div8<%# Eval("DEPTNO") %>');">
                                                                                                                    <asp:Label ID="lblClassTimeTable" CssClass='<%# Convert.ToInt32(Eval("PENDDING_COUNT"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("PENDDING_COUNT") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Send E-Mail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Button ID="btnSendExamTimeTableMail" Visible='<%# Convert.ToInt32(Eval("PENDDING_COUNT"))==0 ?false:true %>' OnClick="btnSendExamTimeTableMail_OnClick" CommandArgument='<%# Eval("DEPTNO") %>' Text="Send Mail" ToolTip="Send Mail" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="10%" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%">
                                                                                                                    <div id='div8<%# Eval("DEPTNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                        <asp:GridView ID="gvChild_ExamTimeTable" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                            CssClass="datatable" BorderColor="#f5511e"
                                                                                                                            Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                            <%-- OnRowDataBound="gvChild_RowDataBound"--%>
                                                                                                                            <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                            <RowStyle />
                                                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                            <Columns>

                                                                                                                                <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="16%" HeaderText="Scheme Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                    ItemStyle-HorizontalAlign="left" />
                                                                                                                                <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="8%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstH" Style="text-align: center" Text="1st Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondtH3rd" Style="text-align: center" Text="3rd Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondtH5th" Style="text-align: center" Text="5th Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondtH7th" Style="text-align: center" Text="7th Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstH2nd" Style="text-align: center" Text="2nd Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondtH4th" Style="text-align: center" Text="4th Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondtH6th" Style="text-align: center" Text="6th Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <HeaderTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondtH8th" Style="text-align: center" Text="8th Sem Exam Time Table" runat="server"></asp:Label>
                                                                                                                                        <br></br>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                    </HeaderTemplate>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                        <span>|</span>
                                                                                                                                        <asp:Label ID="lblExamTimeTableSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                            </Columns>
                                                                                                                            <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolKit:TabPanel>

                                                <!--Exam Registration Status -->
                                                <ajaxToolKit:TabPanel ID="tabExamRegistrationStatus" runat="server" TabIndex="5" HeaderText="Exam Registration">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-info">
                                                                <div class="panel-body">
                                                                    <hr />
                                                                    <div id="divHExamRegistrationStatus" class="col-sm-12" runat="server">
                                                                        <div class="panel panel-primary">
                                                                            <div class="panel-heading">
                                                                                <span class="glyphicon glyphicon-list-alt"></span>
                                                                                <span>Exam Registration Status</span>
                                                                            </div>
                                                                            <div class="panel-body" id="divExamRegistrationStatus" style="display: block">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div style="padding: 10px">
                                                                                            <asp:GridView ID="gvParent_ExamRegistration" runat="server" DataKeyNames="COLLEGE_ID" Width="100%"
                                                                                                CssClass="datatable" AutoGenerateColumns="false"
                                                                                                BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParent_ExamRegistration_RowDataBound" GridLines="Horizontal"
                                                                                                ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                <Columns>

                                                                                                    <asp:BoundField DataField="COLLEGE_NAME" ItemStyle-Width="35%" HeaderText="School/Institute Name" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="LEFT" />
                                                                                                    <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="20%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="COURSE_EXAM_REG_COMPLETE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Count" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Count"
                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divcR" runat="server">
                                                                                                                <a href="JavaScript:divexpandcollapse('divExamRegistrationDepartment<%# Eval("COLLEGE_ID") %>');">
                                                                                                                    <asp:Label ID="lblExamRegistration" runat="server" CssClass='<%# Convert.ToInt32(Eval("COURSE_EXAM_REG_PENDING_COUNT"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("COURSE_EXAM_REG_PENDING_COUNT") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdfCollegeno" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%">
                                                                                                                    <div id='divExamRegistrationDepartment<%# Eval("COLLEGE_ID") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                        <asp:GridView ID="gvChild_ExamRegistration_Department" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                            CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_ExamRegistration_Department_RowDataBound"
                                                                                                                            Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                            <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                            <RowStyle />
                                                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                            <Columns>
                                                                                                                                <asp:BoundField DataField="DEPTNAME" ItemStyle-Width="35%" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="LEFT" />
                                                                                                                                <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="20%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:BoundField DataField="COURSE_EXAM_REG_COMPLETE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Exam Registration" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Exam Registration"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <div id="divcR" runat="server">
                                                                                                                                            <a href="JavaScript:divexpandcollapse('div9<%# Eval("DEPTNOSERNO") %>');">
                                                                                                                                                <asp:Label ID="lblExamRegistration" runat="server" CssClass='<%# Convert.ToInt32(Eval("COURSE_EXAM_REG_PENDING_COUNT"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("COURSE_EXAM_REG_PENDING_COUNT") %>'></asp:Label>
                                                                                                                                                <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                        </div>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField HeaderText="Send E-Mail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Button ID="btnSendExamRegistrationMail" Visible='<%# Convert.ToInt32(Eval("COURSE_EXAM_REG_PENDING_COUNT"))==0 ?false:true %>' OnClick="btnSendExamRegistrationMail_OnClick" CommandArgument='<%# Eval("DEPTNO") %>' Text="Send Mail" ToolTip="Send Mail" runat="server" />
                                                                                                                                    </ItemTemplate>
                                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                                    <ItemStyle Width="10%" />
                                                                                                                                </asp:TemplateField>


                                                                                                                                <asp:TemplateField>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="100%">
                                                                                                                                                <div id='div9<%# Eval("DEPTNOSERNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                    <asp:GridView ID="gvChild_ExamRegistration" runat="server" DataKeyNames="DEPTNO" Width="100%"
                                                                                                                                                        CssClass="datatable" AutoGenerateColumns="false" OnRowDataBound="gvChild_ExamRegistration_RowDataBound"
                                                                                                                                                        BorderStyle="Solid" BorderWidth="1px" GridLines="Horizontal"
                                                                                                                                                        ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">
                                                                                                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                        <%--OnRowDataBound="gvChild_ExamRegistration_RowDataBound"--%>
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="23%" HeaderText="Scheme Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="left" />
                                                                                                                                                            <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="5%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                            <asp:TemplateField ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <span style="text-align: center">
                                                                                                                                                                        <asp:Label ID="lblExamFormFillup" Style="text-align: center" Text="Exam Form Fillup" runat="server"></asp:Label></span>
                                                                                                                                                                    <asp:Label ID="lblExamFormFillupComplete" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblExamFormFillupPending" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div id="divcR_Exam_Fillup_Form" runat="server">
                                                                                                                                                                        <a href="JavaScript:divexpandcollapse('divExamFillupForm<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                            <span style="text-align: center">
                                                                                                                                                                                <asp:Label ID="lblExamFillupFormCompleted" runat="server" Style="color: green" Text='<%# Eval("EXAM_FORM_FILLUP_COUNT_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                <asp:Label ID="lblExamFillupFormPending" runat="server" Style="color: red" Text='<%# Eval("EXAM_FORM_FILLUP_COUNT_PENDING") %>'></asp:Label></span>
                                                                                                                                                                            <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                                            <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                                            <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblExamPayment" Style="text-align: center" Text="Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                    <asp:Label ID="lblExamPaymentComplete" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblExamPaymentPending" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div id="divcR_Exam_Payment" runat="server">
                                                                                                                                                                        <a href="JavaScript:divexpandcollapse('divExamPayment<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                            <asp:Label ID="lblExamPaymentCompleted" runat="server" Style="color: green" Text='<%# Eval("PAYMENT_CONFIRMED_COUNT_COMPLETED") %>'></asp:Label>
                                                                                                                                                                            <span>|</span>
                                                                                                                                                                            <asp:Label ID="lblExamPaymentPending" runat="server" Style="color: red" Text='<%# Eval("PAYMENT_CONFIRMED_COUNT_PENDING") %>'></asp:Label></span>
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderText=""
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="Label2" Style="text-align: center" Text="HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                    <asp:Label ID="lblHODComplete" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblHODPending" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div id="divcR_HOD_Approval" runat="server">
                                                                                                                                                                        <a href="JavaScript:divexpandcollapse('divHODApproval<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                            <asp:Label ID="lblHODApprovalCompleted" runat="server" Style="color: green" Text='<%# Eval("HOD_APPROVAL_COUNT_COMPLETED") %>'></asp:Label>
                                                                                                                                                                            <span>|</span>
                                                                                                                                                                            <asp:Label ID="lblHODApprovalPending" runat="server" Style="color: red" Text='<%# Eval("HOD_APPROVAL_COUNT_PENDING") %>'></asp:Label></span>
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending Admit  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblCardDownload" Style="text-align: center" Text="Card Download" runat="server"></asp:Label>
                                                                                                                                                                    <asp:Label ID="lblCardDownloadComplete" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblCardDownloadPending" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div id="divc_R_CardDownload" runat="server">
                                                                                                                                                                        <a href="JavaScript:divexpandcollapse('divAdmitCardDownload<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                            <asp:Label ID="lblAdmitCardDownloadCompleted" runat="server" Style="color: green" Text='<%# Eval("ADMIT_CARD_DOWNLOAD_COUNT_COMPLETED") %>'></asp:Label>
                                                                                                                                                                            <span>|</span>
                                                                                                                                                                            <asp:Label ID="lblAdmitCardDownloadPending" runat="server" Style="color: red" Text='<%# Eval("ADMIT_CARD_DOWNLOAD_COUNT_PENDING") %>'></asp:Label></span>
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="100%">
                                                                                                                                                                            <div id='divExamFillupForm<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                <asp:GridView ID="gvChild_ExamRegistration_Exam_Form_Fillup" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                    CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_ExamRegistrationExam_FormPending_RowDataBound"
                                                                                                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <RowStyle />
                                                                                                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="13%" HeaderText="Total Exam Form Fillup Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Center" />

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstH" Style="text-align: center" Text="1st Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondtH3rd" Style="text-align: center" Text="3rd Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondtH5th" Style="text-align: center" Text="5th Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondtH7th" Style="text-align: center" Text="7th Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstH2nd" Style="text-align: center" Text="2nd Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondtH4th" Style="text-align: center" Text="4th Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondtH6th" Style="text-align: center" Text="6th Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondtH8th" Style="text-align: center" Text="8th Sem Exam Form Fillup" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamFormFillupSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center" HeaderText="Total Exam Form Fillup Pending"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <div id="divc_R_ExamFormFillupPending" runat="server">
                                                                                                                                                                                                    <a href="JavaScript:divexpandcollapse('divExamFormFillupPending<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                                                        <asp:Label ID="lblExamFormFillupTotalPending" runat="server" CssClass='<%# Convert.ToInt32(Eval("TOTAL_PENDING"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("TOTAL_PENDING") %>'></asp:Label>
                                                                                                                                                                                                        <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                                                                                </div>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <tr>
                                                                                                                                                                                                    <td colspan="100%">
                                                                                                                                                                                                        <div id='divExamFormFillupPending<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                                            <asp:GridView ID="gvChildExamFormFillupPending_StudentList" runat="server" DataKeyNames="SCHEMENO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                                                CssClass="datatable" BorderColor="#f5511e"
                                                                                                                                                                                                                Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                                                                                <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                                                <RowStyle />
                                                                                                                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                                                <Columns>
                                                                                                                                                                                                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Sr. No."
                                                                                                                                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                                                            <%# Container.DataItemIndex+1 %>
                                                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                    <asp:BoundField DataField="REGNO" ItemStyle-Width="20%" HeaderText="Registration No." HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="STUDNAME" ItemStyle-Width="30%" HeaderText="Student Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="LEFT" />

                                                                                                                                                                                                                    <asp:BoundField DataField="LONGNAME" ItemStyle-Width="30%" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="SEMESTERNAME" ItemStyle-Width="10%" HeaderText="Semester" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                                </Columns>
                                                                                                                                                                                                                <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                                                        </div>
                                                                                                                                                                                                    </td>
                                                                                                                                                                                                </tr>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                    </Columns>
                                                                                                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                </asp:GridView>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="100%">
                                                                                                                                                                            <div id='divExamPayment<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                <asp:GridView ID="gvChild_ExamRegistration_ExamPayment" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                    CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_ExamRegistration_ExamPaymentPending_RowDataBound"
                                                                                                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <RowStyle />
                                                                                                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="13%" HeaderText="Total Exam Payment Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Center" />


                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstH" Style="text-align: center" Text="1st Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondtH3rd" Style="text-align: center" Text="3rd Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondtH5th" Style="text-align: center" Text="5th Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondtH7th" Style="text-align: center" Text="7th Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstH2nd" Style="text-align: center" Text="2nd Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lbExamPaymentFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondtH4th" Style="text-align: center" Text="4th Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondtH6th" Style="text-align: center" Text="6th Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondtH8th" Style="text-align: center" Text="8th Sem Exam Payment" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblExamPaymentSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center" HeaderText="Total Exam Payment Pending"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <div id="divc_R_ExamPaymentPending" runat="server">
                                                                                                                                                                                                    <a href="JavaScript:divexpandcollapse('divExamPaymentPending<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                                                        <asp:Label ID="lblExamPayment_TotalPending" runat="server" CssClass='<%# Convert.ToInt32(Eval("TOTAL_PENDING"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("TOTAL_PENDING") %>'></asp:Label>
                                                                                                                                                                                                        <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                                                                                </div>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <tr>
                                                                                                                                                                                                    <td colspan="100%">
                                                                                                                                                                                                        <div id='divExamPaymentPending<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                                            <asp:GridView ID="gvChildExamPaymentPending_StudentList" runat="server" DataKeyNames="SCHEMENO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                                                CssClass="datatable" BorderColor="#f5511e"
                                                                                                                                                                                                                Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                                                                                <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                                                <RowStyle />
                                                                                                                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                                                <Columns>
                                                                                                                                                                                                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Sr. No."
                                                                                                                                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                                                            <%# Container.DataItemIndex+1 %>
                                                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                    <asp:BoundField DataField="REGNO" ItemStyle-Width="20%" HeaderText="Registration No." HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="STUDNAME" ItemStyle-Width="30%" HeaderText="Student Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="LEFT" />

                                                                                                                                                                                                                    <asp:BoundField DataField="LONGNAME" ItemStyle-Width="30%" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="SEMESTERNAME" ItemStyle-Width="10%" HeaderText="Semester" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                                </Columns>
                                                                                                                                                                                                                <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                                                        </div>
                                                                                                                                                                                                    </td>
                                                                                                                                                                                                </tr>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                    </Columns>
                                                                                                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                </asp:GridView>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="100%">
                                                                                                                                                                            <div id='divHODApproval<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                <asp:GridView ID="gvChild_ExamRegistration_HODApproval" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                    CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_ExamRegistration_HODApprovalPending_RowDataBound"
                                                                                                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <RowStyle />
                                                                                                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="13%" HeaderText="Total HOD Approval Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Center" />

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstH" Style="text-align: center" Text="1st Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondtH3rd" Style="text-align: center" Text="3rd Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondtH5th" Style="text-align: center" Text="5th Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondtH7th" Style="text-align: center" Text="7th Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstH2nd" Style="text-align: center" Text="2nd Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondtH4th" Style="text-align: center" Text="4th Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondtH6th" Style="text-align: center" Text="6th Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondtH8th" Style="text-align: center" Text="8th Sem HOD Approval" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblHODApprovalSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center" HeaderText="Total HOD Approval Pending"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <div id="divc_R_HODApprovalPending" runat="server">
                                                                                                                                                                                                    <a href="JavaScript:divexpandcollapse('divHODApprovalPending<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                                                        <asp:Label ID="lblHODApproval_TotalPending" runat="server" CssClass='<%# Convert.ToInt32(Eval("TOTAL_PENDING"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("TOTAL_PENDING") %>'></asp:Label>
                                                                                                                                                                                                        <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                                                                                </div>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <tr>
                                                                                                                                                                                                    <td colspan="100%">
                                                                                                                                                                                                        <div id='divHODApprovalPending<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                                            <asp:GridView ID="gvChildHODApprovalPending_StudentList" runat="server" DataKeyNames="SCHEMENO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                                                CssClass="datatable" BorderColor="#f5511e"
                                                                                                                                                                                                                Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                                                                                <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                                                <RowStyle />
                                                                                                                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                                                <Columns>
                                                                                                                                                                                                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Sr. No."
                                                                                                                                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                                                            <%# Container.DataItemIndex+1 %>
                                                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                    <asp:BoundField DataField="REGNO" ItemStyle-Width="20%" HeaderText="Registration No." HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="STUDNAME" ItemStyle-Width="30%" HeaderText="Student Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="LEFT" />

                                                                                                                                                                                                                    <asp:BoundField DataField="LONGNAME" ItemStyle-Width="30%" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="SEMESTERNAME" ItemStyle-Width="10%" HeaderText="Semester" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                                </Columns>
                                                                                                                                                                                                                <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                                                        </div>
                                                                                                                                                                                                    </td>
                                                                                                                                                                                                </tr>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                                                    </Columns>
                                                                                                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                </asp:GridView>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="100%">
                                                                                                                                                                            <div id='divAdmitCardDownload<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                <asp:GridView ID="gvChild_ExamRegistration_AdmitCardDownload" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                    CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_ExamRegistration_AdmitCardPending_RowDataBound"
                                                                                                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <RowStyle />
                                                                                                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="13%" HeaderText="Total Admit Card Download Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Center" />

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstH" Style="text-align: center" Text="1st Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondtH3rd" Style="text-align: center" Text="3rd Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondtH5th" Style="text-align: center" Text="5th Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondtH7th" Style="text-align: center" Text="7th Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstH2nd" Style="text-align: center" Text="2nd Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondtH4th" Style="text-align: center" Text="4th Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondtH6th" Style="text-align: center" Text="6th Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondtH8th" Style="text-align: center" Text="8th Sem Admit Card Download" runat="server"></asp:Label>
                                                                                                                                                                                                <br></br>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                                                <span>|</span>
                                                                                                                                                                                                <asp:Label ID="lblAdmitCardDownloadSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Total Admit Card Download Pending"
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <div id="divc_R_AdmitCardDownloadPending" runat="server">
                                                                                                                                                                                                    <a href="JavaScript:divexpandcollapse('divAdmitCardDownloadPending<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                                                        <asp:Label ID="lblAdmitCardDownload_TotalPending" runat="server" CssClass='<%# Convert.ToInt32(Eval("TOTAL_PENDING"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("TOTAL_PENDING") %>'></asp:Label>
                                                                                                                                                                                                        <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                                                                        <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                                                                                </div>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                        <asp:TemplateField>
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <tr>
                                                                                                                                                                                                    <td colspan="100%">
                                                                                                                                                                                                        <div id='divAdmitCardDownloadPending<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                                            <asp:GridView ID="gvChildAdmitCardDownloadPending_StudentList" runat="server" DataKeyNames="SCHEMENO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                                                CssClass="datatable" BorderColor="#f5511e"
                                                                                                                                                                                                                Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                                                                                <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                                                <RowStyle />
                                                                                                                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                                                <Columns>
                                                                                                                                                                                                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Sr. No."
                                                                                                                                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                                                                            <%# Container.DataItemIndex+1 %>
                                                                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                                                                    <asp:BoundField DataField="REGNO" ItemStyle-Width="20%" HeaderText="Registration No." HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="STUDNAME" ItemStyle-Width="30%" HeaderText="Student Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="LEFT" />

                                                                                                                                                                                                                    <asp:BoundField DataField="LONGNAME" ItemStyle-Width="30%" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                                                    <asp:BoundField DataField="SEMESTERNAME" ItemStyle-Width="10%" HeaderText="Semester" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                                                </Columns>
                                                                                                                                                                                                                <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                                            </asp:GridView>
                                                                                                                                                                                                        </div>
                                                                                                                                                                                                    </td>
                                                                                                                                                                                                </tr>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                                                    </Columns>
                                                                                                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                </asp:GridView>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                        </Columns>
                                                                                                                                                        <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                            </Columns>
                                                                                                                            <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolKit:TabPanel>

                                                <!--Result Processing Status -->
                                                <ajaxToolKit:TabPanel ID="tabResultProcessingStatus" runat="server" TabIndex="7" HeaderText="Result Processing">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-info">
                                                                <div class="panel-body">
                                                                    <hr />
                                                                    <div id="divHResultProcessingStatus" class="col-sm-12" runat="server">
                                                                        <div class="panel panel-primary">
                                                                            <div class="panel-heading">
                                                                                <span class="glyphicon glyphicon-list-alt"></span>
                                                                                <span>Result Processing Status</span>
                                                                            </div>
                                                                            <div class="panel-body" id="divResultProcessingStatus" style="display: block">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div style="padding: 10px">
                                                                                            <asp:GridView ID="gvParent_ResultProcessing" runat="server" DataKeyNames="COLLEGE_ID" Width="100%"
                                                                                                CssClass="datatable" AutoGenerateColumns="false"
                                                                                                BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParent_ResultProcessing_RowDataBound" GridLines="Horizontal"
                                                                                                ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                                                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="COLLEGE_NAME" ItemStyle-Width="35%" HeaderText="School/Institute Name" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="LEFT" />
                                                                                                    <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="20%" HeaderText="Total Result Processing" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="COMPLETE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Result Processing" HeaderStyle-HorizontalAlign="Center"
                                                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Result Processing"
                                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divcR" runat="server">

                                                                                                                <a href="JavaScript:divexpandcollapse('div10<%# Eval("COLLEGE_ID") %>');">
                                                                                                                    <asp:Label ID="lblResultProcessingTable" CssClass='<%# Convert.ToInt32(Eval("PENDING_COUNT"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("PENDING_COUNT") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%">
                                                                                                                    <div id='div10<%# Eval("COLLEGE_ID") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                        <asp:GridView ID="gvChild_ResultProcessingDepartment" runat="server" OnRowDataBound="gvChild_ResultProcessingDepartment_RowDataBound" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                            CssClass="datatable" BorderColor="#f5511e"
                                                                                                                            Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                            <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                            <RowStyle />
                                                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                            <Columns>

                                                                                                                                <asp:BoundField DataField="DEPTNAME" ItemStyle-Width="35%" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="LEFT" />
                                                                                                                                <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="20%" HeaderText="Total Result Processing" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:BoundField DataField="COMPLETE_COUNT" ItemStyle-Width="25%" HeaderText="Complete Result Processing" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                                                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Result Processing"
                                                                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <div id="divcR" runat="server">
                                                                                                                                            <a href="JavaScript:divexpandcollapse('div11<%# Eval("DEPTNOSRNO") %>');">
                                                                                                                                                <asp:Label ID="lblResultProcessingTable" CssClass='<%# Convert.ToInt32(Eval("PENDING_COUNT"))>0 ? "Pending":"Completed" %>' runat="server" Text='<%# Eval("PENDING_COUNT") %>'></asp:Label>
                                                                                                                                                <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                        </div>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderText="Send E-Mail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Button ID="btnSendResultProcessingMail" Visible='<%# Convert.ToInt32(Eval("PENDING_COUNT"))==0 ?false:true %>' OnClick="btnSendResultProcessingMail_OnClick" CommandArgument='<%# Eval("DEPTNO") %>' Text="Send Mail" ToolTip="Send Mail" runat="server" />
                                                                                                                                    </ItemTemplate>
                                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                                    <ItemStyle Width="10%" />
                                                                                                                                </asp:TemplateField>

                                                                                                                                <asp:TemplateField>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="100%">
                                                                                                                                                <div id='div11<%# Eval("DEPTNOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                    <asp:GridView ID="gvChild_ResultProcessing" runat="server" DataKeyNames="DEPTNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                        CssClass="datatable" BorderColor="#f5511e" OnRowDataBound="gvChild_ResultProcessingPending_RowDataBound"
                                                                                                                                                        Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                        <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                        <RowStyle />
                                                                                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                        <Columns>

                                                                                                                                                            <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="11%" HeaderText="Scheme Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="left" />
                                                                                                                                                            <asp:BoundField DataField="TOTAL_COUNT" ItemStyle-Width="5%" HeaderText="Total Count" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                ItemStyle-HorizontalAlign="Center" />

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstH" Style="text-align: center" Text="1st Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstCompleteH" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstPendingH" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstCompleted" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstPending" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondtH3rd" Style="text-align: center" Text="3rd Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleteH3rd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPendingH3rd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleted3rd" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPending3rd" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondtH5th" Style="text-align: center" Text="5th Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleteH5th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPendingH5th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleted5th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPending5th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondtH7th" Style="text-align: center" Text="7th Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleteH7th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPendingH7th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleted7th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPending7th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstH2nd" Style="text-align: center" Text="2nd Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstCompleteH2nd" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstPendingH2nd" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstCompleted2nd" runat="server" Style="color: green" Text='<%# Eval("FIRST_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingFirstPending2nd" runat="server" Style="color: red" Text='<%# Eval("FIRST_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondtH4th" Style="text-align: center" Text="4th Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleteH4th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPendingH4th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleted4th" runat="server" Style="color: green" Text='<%# Eval("SECOND_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPending4th" runat="server" Style="color: red" Text='<%# Eval("SECOND_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondtH6th" Style="text-align: center" Text="6th Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleteH6th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPendingH6th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleted6th" runat="server" Style="color: green" Text='<%# Eval("THIRD_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPending6th" runat="server" Style="color: red" Text='<%# Eval("THIRD_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderText="Completed/Pending  Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <HeaderTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondtH8th" Style="text-align: center" Text="8th Sem Result Processing" runat="server"></asp:Label>
                                                                                                                                                                    <br></br>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleteH8th" Style="color: green" Text="Completed" runat="server"></asp:Label><span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPendingH8th" Style="color: red" Text="Pending" runat="server"></asp:Label>
                                                                                                                                                                </HeaderTemplate>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondCompleted8th" runat="server" Style="color: green" Text='<%# Eval("FOUR_COMPLETED") %>'></asp:Label>
                                                                                                                                                                    <span>|</span>
                                                                                                                                                                    <asp:Label ID="lblResult_ProcessingSecondPending8th" runat="server" Style="color: red" Text='<%# Eval("FOUR_PENDING") %>'></asp:Label></span>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderText="Pending Count"
                                                                                                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div id="divc_R_Result_ProcessingPending" runat="server">
                                                                                                                                                                        <a href="JavaScript:divexpandcollapse('divResult_ProcessingPending<%# Eval("DEPTNOSCHEMENOSRNO") %>');">
                                                                                                                                                                            <asp:Label ID="lblResult_ProcessingPendingTotalPending" runat="server" CssClass='<%# Convert.ToInt32(Eval("TOTAL_PENDING"))>0 ? "Pending":"Completed" %>' Text='<%# Eval("TOTAL_PENDING") %>'></asp:Label>
                                                                                                                                                                            <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                                                            <asp:HiddenField ID="hdfDeptno" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                                                            <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                            <asp:TemplateField>
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <tr>
                                                                                                                                                                        <td colspan="100%">
                                                                                                                                                                            <div id='divResult_ProcessingPending<%# Eval("DEPTNOSCHEMENOSRNO") %>' style="display: none; position: relative; left: 24px; overflow: auto">
                                                                                                                                                                                <asp:GridView ID="gvChildResultPending_StudentList" runat="server" DataKeyNames="SCHEMENO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                                                                                                    CssClass="datatable" BorderColor="#f5511e"
                                                                                                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                                                                                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                                                                                                    <RowStyle />
                                                                                                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                                                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="black" />
                                                                                                                                                                                    <Columns>
                                                                                                                                                                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Sr. No."
                                                                                                                                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                                                <%# Container.DataItemIndex+1 %>
                                                                                                                                                                                            </ItemTemplate>
                                                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                                                        <asp:BoundField DataField="REGNO" ItemStyle-Width="20%" HeaderText="Registration No." HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                        <asp:BoundField DataField="STUDNAME" ItemStyle-Width="30%" HeaderText="Student Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="LEFT" />

                                                                                                                                                                                        <asp:BoundField DataField="LONGNAME" ItemStyle-Width="30%" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Left" />
                                                                                                                                                                                        <asp:BoundField DataField="SEMESTERNAME" ItemStyle-Width="10%" HeaderText="Semester" HeaderStyle-HorizontalAlign="Left"
                                                                                                                                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                                                                                                                                    </Columns>
                                                                                                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                                                </asp:GridView>
                                                                                                                                                                            </div>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>

                                                                                                                                                        </Columns>
                                                                                                                                                        <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>

                                                                                                                            </Columns>
                                                                                                                            <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                </Columns>
                                                                                            </asp:GridView>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolKit:TabPanel>

                                            </ajaxToolKit:TabContainer>
                                        </div>
                                        <!-- Div Academic Dashboard -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="modal fade" style="padding-top: 1%;" data-backdrop="static" data-keyboard="false" aria-modal="true" id="Model_Message" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header text-center" style="background-color: #00C6D7 !important">
                    <h4 class="modal-title" style="font-style: normal; font-weight: bold; color: white">Send Mail</h4>
                </div>

                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-sm-12 form-group">
                                        <div class="row">

                                            <div class="col-sm-12 form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>To<sup>*</sup></label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txt_emailid" placeholder="Email ID" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hdfEmail" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter To Email "
                                                            ControlToValidate="txt_emailid" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_emailid"
                                                            Display="None" ErrorMessage="Please Enter Valid To Email " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="login1"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>Cc</label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtCc" placeholder="Cc Email ID" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revCc" runat="server" ControlToValidate="txtCc"
                                                            Display="None" ErrorMessage="Please Enter Valid Cc Email " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="login1"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>Bcc</label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtBcc" placeholder="Bcc Email ID" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revBcc" runat="server" ControlToValidate="txtCc"
                                                            Display="None" ErrorMessage="Please Enter Valid Bcc Email " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="login1"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>Subject<sup>*</sup></label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtSubject" runat="server" TabIndex="4" MaxLength="128" placeholder="Enter Subject" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
                                                            ErrorMessage="Please Enter Subject" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>Attachment</label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:FileUpload runat="server" ID="fileUploadAttachement" TabIndex="5" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>Message Body<sup>*</sup></label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtBody" runat="server" TabIndex="6" MaxLength="8000" TextMode="MultiLine" placeholder="Enter Message" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBody"
                                                            ErrorMessage="Please Enter Message  " Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer text-center">
                        <asp:Button ID="btnSent" runat="server" Text="Send" CssClass="btn btn-primary" UseSubmitBehavior="false"
                            OnClientClick="if (!Page_ClientValidate('EmailSend')){ return false; } this.disabled = true; this.value ='  Please Wait...';"
                            TabIndex="7" ValidationGroup="EmailSend" OnClick="btnSent_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Close" data-dismiss="modal" OnClientClick="ClearMessageText();" CssClass="btn btn-danger" TabIndex="8"></asp:Button>
                        <asp:HiddenField ID="hdfReceiver_id" runat="server" />
                        <asp:ValidationSummary ID="vsMessage" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EmailSend" DisplayMode="List" />
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../IMAGES/minus.png";
            }
            else {
                div.style.display = "none";
                img.src = "../IMAGES/plus.gif";
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        /* To collapse and expand page sections */
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }
    </script>

    <script>
        function View(txtvalue) {
            $("#Model_Message").modal();
            document.getElementById('ctl00_ContentPlaceHolder1_txt_emailid').value = txtvalue;
            document.getElementById('ctl00_ContentPlaceHolder1_txtSubject').focus();
        }

        //Clear Message Popup
        function ClearMessageText() {
            document.getElementById('<%=txtSubject.ClientID%>').value = "";
            document.getElementById('<%=txtBody.ClientID%>').value = "";
        }
    </script>


</asp:Content>

