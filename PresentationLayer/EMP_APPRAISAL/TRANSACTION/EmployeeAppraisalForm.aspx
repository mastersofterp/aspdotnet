<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeAppraisalForm.aspx.cs" Inherits="EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 700px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <%-- <script src="../../includes/prototype.js" type="text/javascript"></script>
    <script src="../../includes/scriptaculous.js" type="text/javascript"></script>--%>
    <%--<script src="../../includes/modalbox.js" type="text/javascript"></script>--%>

    <%--    <style type="text/css">
        .Calendar .ajax__calendar_body {
            width: 200px;
            height: 170px; /* modified */
            position: relative;
            border: solid 0px;
        }

        .Calendar .ajax__calendar_container {
            background-color: #ffffff;
            border: 1px solid #646464;
            color: #000000;
            width: 195px;
            height: 210px;
        }

        .Calendar .ajax__calendar_footer {
            border: solid top 1px;
            cursor: pointer;
            padding-top: 3px;
            height: 6px;
        }

        .Calendar .ajax__calendar_day {
            cursor: pointer;
            height: 17px;
            padding: 0 2px;
            text-align: right;
            width: 18px;
        }

        .Calendar .ajax__calendar_year {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            margin: 1px;
            height: 40px; /* added */
        }

        .Calendar .ajax__calendar_month {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            /* margin: 1px;
        height: 40px; /* added */
        }
    </style>


    <style type="text/css">
        .submitLink {
            background-color: transparent;
            text-decoration: underline;
            border: none;
            color: blue;
            cursor: pointer;
            text-decoration: underline;
        }

        submitLink:focus {
            outline: none;
        }
    </style>--%>

    <script type="text/javascript">
        function AutoExpand(txtbox) {
            txtbox.style.height = "1px";
            txtbox.style.height = (25 + txtbox.scrollHeight) + "px";

        }
    </script>

    <script type="text/javascript">

        function fnAllowNumeric() {
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8) {
                event.keyCode = 0;
                alert("Only Numeric Value Allowed..!");
                return false;
            }
        }

        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)


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
                //   InitAutoCompl();
            }

        });

    </script>
    <script type="text/javascript">
        function AddItem() {



            // alert('a');
        }
    </script>

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
    </style>
    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999; /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }

        .main_sub_head {
            border-bottom: 1px solid #ddd;
            padding-bottom: 3px;
            font-size: 17px;
            font-weight: 600;
        }
    </style>
    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }

    </script>




    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ANNUAL PERFORMANCE APPRAISAL REPORT (APAR) </h3>
                    <p class="text-center text-bold">
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </p>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-12">
                                Note <b>:</b> <span style="color: #FF0000">* Marked Fields Are Mandatory.</span>
                                <asp:UpdatePanel ID="updActivity" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPBASList" runat="server">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12 col-lg-3" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                        <div class="panel panel-info">
                                                            <div class="bg-light-blue">Click To Open Forms</div>
                                                            <div style="margin-left: 5px; background-color: #EEEEEE;">
                                                                <div style="background-color: #f2f2f2; border: solid 1px  #F2F2F2;">
                                                                    <div class="form-group col-md-12" id="divPartA" runat="server">
                                                                        <asp:Label ID="lblPartA" runat="server" Font-Bold="true" Text="PART A: GENERAL INFORMATION ">
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div class="form-group col-md-12" id="divPersonalInfo" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_PersonalInfo" runat="server" ForeColor="#344EAC" OnClick="LinkButton_PersonalInfo_Click"
                                                                            ToolTip="Add/Edit Personal Information">Personal Information</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="div22" runat="server">
                                                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="CATEGORY 1: TEACHING LEARNING AND EVALUATION RELATED ACTIVITIES: "></asp:Label>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divTeaching" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Teacher_Learning_Activities" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Teacher_Learning_Activities_Click"
                                                                            ToolTip="Add/Edit TEACHING LEARNING ACTIVITIES">Teaching Learning Activities</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divEngaging" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Performance_In_Engaging_Lectures" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Performance_In_Engaging_Lectures_Click"
                                                                            ToolTip="Add/Edit PERFORMANCE IN ENGAGING LECTURES">Performance In Engaging Lectures:</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divPerformance" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Attendance_Performance" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Attendance_Performance_Click"
                                                                            ToolTip="Add/Edit ATTENDANCE PERFORMANCE OF STUDENTS">Attendance Performance:</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divExcess" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Duties_In_Excess_Of_UGC_Norms" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Duties_In_Excess_Of_UGC_Norms_Click"
                                                                            ToolTip="Add/Edit DUTIES IN EXCESS OF UGC NORMS">Lectures And Academic Duties in Excess Of UGC Norms</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divAcademic" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Academic_Lectures" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Academic_Lectures_Click"
                                                                            ToolTip="Add/Edit LECTURES AND ACADEMIC DUTIES">Innovative Teaching Learning Methods:</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="div24" runat="server">
                                                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="CATEGORY 2: CO-CURRICULAR, EXTENSION AND PROFESSIONAL DEVELOPMENT RELATED ACTIVITIES: "></asp:Label>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divCurricular" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Student_Co_Curricular" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Student_Co_Curricular_Click"
                                                                            ToolTip="Add/Edit FIELD BASED ACTIVITIES">Student Related Co-Curricular, Extension And Field Based Activities:</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divCommunity" runat="server" hidden>
                                                                        <asp:LinkButton ID="LinkButton_Corporate_Life_Community_Work" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Corporate_Life_Community_Work_Click"
                                                                            ToolTip="Add/Edit COMMUNITY WORK">Community Work:</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divAdminisrative" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Administrative_And_Academic" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Administrative_And_Academic_Click"
                                                                            ToolTip="Add/Edit ADMINISTRATIVE AND ACADEMIC">Administrative And Academic</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="div2" runat="server">
                                                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="CATEGORY 3: RESEARCH, PUBLICATIONS AND ACADEMIC CONTRIBUTION: "></asp:Label>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divJournal" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Published_Journal" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Published_Journal_Click"
                                                                            ToolTip="Add/Edit PUBLISHED JOURNAL">Publication Details:</asp:LinkButton>
                                                                    </div>
                                                                    <div class="form-group col-md-12" id="divConference" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Papers_In_Conference" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Papers_In_Conference_Click"
                                                                            ToolTip="Add/Edit PAPERS IN CONFERENCE PROCEED">Papers In Conference Proceedings:</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divResearch" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Research_Guidance" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Research_Guidance_Click"
                                                                            ToolTip="Add/Edit RESEARCH GUIDANCE">Research Guidance/Qualification:</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="divPatent" runat="server">
                                                                        <asp:LinkButton ID="LinkButton_Patent_IPR" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Patent_IPR_Click"
                                                                            ToolTip="Add/Edit PATENT/IPR">Patent/IPR:</asp:LinkButton>
                                                                    </div>

                                                                    <%--<div class="form-group col-md-12" id="div16" runat="server">
                                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="CATEGORY 4: RESEARCH GUIDANCE/QUALIFICATION: "></asp:Label>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="div4" runat="server"> 
                                                                            <asp:LinkButton ID="LinkButton_Research_Guidance" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Research_Guidance_Click"
                                                                                ToolTip="Add/Edit RESEARCH GUIDANCE">Research Guidance/Qualification:</asp:LinkButton>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="div17" runat="server">
                                                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="CATEGORY 5: PATENT/IPR: "></asp:Label>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="div11" runat="server"> 
                                                                            <asp:LinkButton ID="LinkButton_Patent_IPR" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Patent_IPR_Click"
                                                                                ToolTip="Add/Edit PATENT/IPR">Patent/IPR:</asp:LinkButton>
                                                                        </div>--%>
                                                                    <%--<div class="form-group col-md-12" id="div11" runat="server"> 
                                                                            <asp:LinkButton ID="LinkButton_Patent_IPR" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit PATENT/IPR">Patent/IPR:</asp:LinkButton>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="divSelfAppraisal" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_SelfAppraisal" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit Self Appraisal Information">Self Apprasal Information</asp:LinkButton>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="div8" runat="server">
                                                                            <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="ACADEMIC RESEARCH AND PUBLICATION ELEMENT:: "></asp:Label>
                                                                        </div>--%>
                                                                    <%--<div class="form-group col-md-12" id="divAcademcResarch" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_Academic_Resarch" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit Academc Resarch Information">Academic_Resarch Information</asp:LinkButton>
                                                                        </div>--%>


                                                                    <%--<div class="form-group col-md-12" id="div7" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_ResarchPaper" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit Resarch_Paper Information">Research papers </asp:LinkButton>
                                                                        </div>--%>



                                                                    <%--<div class="form-group col-md-12" id="div3" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_Sponserd_Element" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit Sponserd_Element Information">Sponserd_Element Information</asp:LinkButton>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="div9" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_Sponserd_Extension" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit Sponserd_Extension_Element Information">Sponserd Extension Element</asp:LinkButton>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="div11" runat="server">
                                                                            <asp:Label ID="Label14" runat="server" Font-Bold="true" Text="PART C:RESEARCH, PUBLICATIONS AND ACADEMIC CONTRIBUTION "></asp:Label>
                                                                        </div>
                                                                        <div class="form-group col-md-12" id="div4" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_Published_Journal" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit Published Journal"> Published Paper In Journal:</asp:LinkButton>
                                                                        </div>--%>


                                                                    <%--<div class="form-group col-md-12" id="div10" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_OrgAcademicActivity" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit ACADEMIC ACTIVITIES">Other Organisation Activities:</asp:LinkButton>
                                                                        </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="div5" runat="server">
                                                                            <asp:LinkButton ID="LinkButton_ManagmentElement" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="Add/Edit MANAGEMENT DEVELOPMENT ELEMENTS">Management Development Element:</asp:LinkButton>
                                                                        </div>--%>


                                                                    <div class="form-group col-md-12">
                                                                        <asp:LinkButton ID="LinkButton_Print" runat="server" ForeColor="#990099" OnClick="linkbutton_Print_Click"
                                                                            ToolTip="Print APAR Proforma">Print APAR Proforma</asp:LinkButton>
                                                                    </div>

                                                                    <%--<div class="form-group col-md-12" id="div12" runat="server">
                                                                            <asp:Label ID="Label25" runat="server" Font-Bold="true" Text="ASSESSMENT OF THE REPORTING OFFICER">
                                                                            </asp:Label>
                                                                        </div>
                                                                        <div class="form-group col-md-12">
                                                                            <asp:LinkButton ID="LinkButton_Reporting_officer_CatI" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="ASSESSMENT">ASSESSMENT PART I</asp:LinkButton>
                                                                        </div>
                                                                        <div class="form-group col-md-12">
                                                                            <asp:LinkButton ID="LinkButton_Reporting_officer_CatII" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="ASSESSMENT">ASSESSMENT PART II</asp:LinkButton>
                                                                        </div>
                                                                        <div class="form-group col-md-12">
                                                                            <asp:LinkButton ID="LinkButton_Reporting_officer_CatIII" runat="server" ForeColor="#344EAC"
                                                                                ToolTip="ASSESSMENT">ASSESSMENT PART III</asp:LinkButton>
                                                                        </div>--%>

                                                                   <%-- <div class="form-group col-md-12" id="div13" runat="server">
                                                                        <asp:Label ID="Label35" runat="server" Font-Bold="true" Text="REMARKS BY REVIEWING OFFICER/S">
                                                                        </asp:Label>
                                                                    </div>--%>

                                                                    <%--<div class="form-group col-md-12" id="divReporting" runat="server" visible="false">
                                                                    <asp:LinkButton ID="LinkButton_Reporting_Officer" runat="server" ForeColor="#344EAC" Visible="false"
                                                                        ToolTip="Reporting Officer" OnClick="LinkButton_First_Reporting_Officer_Click">Reporting Officer's Remark</asp:LinkButton>
                                                                </div>--%>

                                                                    <div class="form-group col-md-12" id="divReviewing" runat="server" visible="true">
                                                                        <asp:LinkButton ID="LinkButton_Reviewing_Officers" runat="server" ForeColor="#344EAC" OnClick="LinkButton_Reviewing_Click"  
                                                                            ToolTip="ASSESSMENT">Remarks By Reviewing Officers</asp:LinkButton>
                                                                    </div>

                                                                    <div class="form-group col-md-12">
                                                                        <asp:LinkButton ID="LinkButton_FinalSubmit" runat="server" ForeColor="#344EAC"
                                                                            ToolTip="Final Submit" OnClick="LinkButton_FinalSubmit_Click">Final Submit</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 col-lg-9">
                                                        <div class="col-12 col-lg-12" id="trSession" runat="server" visible="true">
                                                            <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                <div class="main_sub_head">
                                                                    <strong>Select Session & Employee </strong>
                                                                </div>
                                                                <div class="row mt-3">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <label>Select Session :</label>
                                                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"
                                                                            AutoPostBack="true" ToolTip="Select Session" TabIndex="1" Enabled="false">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <label>Select Employee :</label>
                                                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                            ToolTip="Select Employee" Enabled="false" TabIndex="2" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <label>From Date:</label>
                                                                        <asp:TextBox ID="txtfromdate" runat="server" MaxLength="60" CssClass="form-control" TabIndex="12"
                                                                            Enabled="false" Style="text-transform: uppercase;"></asp:TextBox>

                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <label>To Date:</label>
                                                                        <asp:TextBox ID="txttodate" runat="server" MaxLength="60" CssClass="form-control" TabIndex="12"
                                                                            Enabled="false" Style="text-transform: uppercase;"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <label><span style="color: #FF0000"></span>Reporting Authority Path :</label>
                                                                        <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnPAPNO" runat="server" Value="" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div>
                                                        </div>
                                                        <div class="col-12 col-lg-12" id="Div14" runat="server">
                                                            <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">

                                                                <asp:Panel ID="pnlPBAS" runat="server">
                                                                    <div class="panel panel-info">
                                                                        <div class="bg-light-blue">Annual Performance Appraisal Report (APAR)</div>
                                                                        <div class="panel panel-body">
                                                                            <asp:MultiView ID="MultiView_Profile" runat="server">
                                                                                <asp:View ID="View_PersonalInfo" runat="server" OnActivate="View_PersonalInfo_Activate" OnDeactivate="View_PersonalInfo_Deactivate">
                                                                                    <asp:Panel ID="Panel_View_PersonalInfo" runat="server">
                                                                                        <div>
                                                                                            <br />

                                                                                        </div>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">

                                                                                             <div class="panel panel-heading"><b>Personal Details</b></div>
                                                                                            <div class="panel panel-body">
                                                                                                <div class="row">
                                                                                                    <div class="form-group col-md-6">
                                                                                                        <label><span style="color: red;"></span>Name of The Teacher:</label>
                                                                                                        <asp:TextBox ID="txtName" runat="server" MaxLength="60" CssClass="form-control" TabIndex="3" Enabled="false"
                                                                                                            ToolTip="Name In Block Letters" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        <%--<asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" Display="None"
                                                                                                            ErrorMessage="Please Enter Name." ValidationGroup="Personal" />--%>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeName" runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                                                                            TargetControlID="txtName" ValidChars=". ">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                                                                    </div>

                                                                                                    <div class="form-group col-md-6">
                                                                                                        <label><span style="color: red;"></span>Name of Institute:</label>
                                                                                                        <asp:DropDownList ID="ddlInstitute" runat="server" CssClass="form-control" TabIndex="5" Enabled="false"
                                                                                                            ToolTip="Select Institute">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlInstitute"
                                                                                                            Display="None" ErrorMessage="Please Select Institute."
                                                                                                            ValidationGroup="Personal" InitialValue="0" />
                                                                                                    </div>

                                                                                                    <div class="form-group col-md-6">
                                                                                                        <label><span style="color: red;"></span>Department :</label>
                                                                                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" TabIndex="5" Enabled="false"
                                                                                                            ToolTip="Select Department">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator runat="server" ID="rfvDept" ControlToValidate="ddlDept"
                                                                                                            Display="None" ErrorMessage="Please Select Department."
                                                                                                            ValidationGroup="Personal" InitialValue="0" />
                                                                                                    </div>

                                                                                                    <div class="form-group col-md-6">
                                                                                                        <label><span style="color: red;"></span>Designation :</label>
                                                                                                        <asp:DropDownList ID="ddlCDesig" runat="server" CssClass="form-control" TabIndex="4" Enabled="false"
                                                                                                            ToolTip="Current Designation">
                                                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator runat="server" ID="rfvDesig" ControlToValidate="ddlCDesig" Display="None"
                                                                                                            ErrorMessage="Please Select Designation."
                                                                                                            ValidationGroup="Personal" InitialValue="0" />
                                                                                                    </div>
                                                                                                    <div class="form-group col-md-6">
                                                                                                        <label><span style="color: red;"></span>Assessment Year:</label>
                                                                                                        <asp:TextBox ID="txtassesmentyear" runat="server" MaxLength="60" CssClass="form-control" TabIndex="3" Enabled="false"
                                                                                                            ToolTip=" Block Letters" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtassesmentyear" Display="None"
                                                                                                            ErrorMessage="Please Enter Assessment Year." ValidationGroup="Personal" />--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                    </asp:Panel>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>
                                                                                    <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="pnlPersonalSubmit" runat="server">
                                                                                            <p class="text-center">
                                                                                                <asp:Button ID="btnPersonalSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnPersonalSubmit_Click"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                <%--<asp:Button ID="btnPersonalNext" runat="server" Text="Next" TabIndex="16"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here To Go Next" />--%>
                                                                                                <asp:ValidationSummary ID="validsumm1" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                    ValidationGroup="Personal" />
                                                                                            </p>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </asp:View>

                                                                                <asp:View ID="Published_Journal" runat="server" OnActivate="View_Published_Journal_Activate" OnDeactivate="View_Published_Journal_Deactivate">
                                                                                     <asp:Label ID="lblempserbook" runat="server" Visible="false"   ForeColor="#cc3300" Text="Please Fill The Employee Service Book."></asp:Label>
                                                                                  
                                                                                     <asp:Panel ID="PnlJournal" runat="server">
                                                                                        <div>
                                                                                            <br />
                                                                                        </div>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <div class="bg-light-blue">Published Journal</div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PanelJournal" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:HiddenField ID="hdnPublication" Value="" runat="server" />
                                                                                                    <asp:ListView ID="lvpublicationJournal" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No.
                                                                                                                            </th>
                                                                                                                            <th>Title with Page No.
                                                                                                                            </th>
                                                                                                                            <th>Journal
                                                                                                                            </th>
                                                                                                                            <th>ISSN/ISBN No.
                                                                                                                            </th>
                                                                                                                            <th>Peer Reviewed
                                                                                                                            </th>
                                                                                                                            <th>Impact Factor
                                                                                                                            </th>
                                                                                                                            <th>Main Author
                                                                                                                            </th>
                                                                                                                            <th>No of Co-Author
                                                                                                                            </th>
                                                                                                                            <th>API Score Claimed
                                                                                                                            </th>
                                                                                                                            <th id="lblverapisc" runat="server">Verified API Score
                                                                                                                            </th>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("TITLE_PAGENO") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblJournal" runat="server" Text='<%# Eval("JOURNAL") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPeerReviewed" runat="server" Text='<%# Eval("PEER_REVIEWED") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblImpactFactor" runat="server" Text='<%# Eval("IMPACTFACTORS") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblMainAuthor" runat="server" Text='<%# Eval("MAIN_AUTHOR") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblCoAuthor" runat="server" Text='<%# Eval("CO_AUTHOR") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtJournalApiClaimed" runat="server" onblur="CalculatePublication('lvpublicationJournal','txtJournalApiClaimed','txtPubJournal')" MaxLength="50" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtJournalApiClaimed" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtJournalVerifiedApiScore" runat="server" onblur="CalculatePublication('lvpublicationJournal','txtJournalVerifiedApiScore','txtJournalPub')" MaxLength="50" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div43" runat="server">
                                                                                                            <label>API Score Claimed Total</label>
                                                                                                            <asp:TextBox ID="txtPubJournal" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div44" runat="server">
                                                                                                            <label id="lblverapi" runat="server">Verified API Score Total</label>
                                                                                                            <asp:TextBox ID="txtJournalPub" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            <asp:HiddenField ID="HiddenField11" runat="server" Value="0" />
                                                                                                            <asp:HiddenField ID="HiddenField12" runat="server" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>

                                                                                    <%--</asp:View>--%>

                                                                                    <asp:Panel ID="PnlBook" runat="server">
                                                                                        <div class="panel panel-info">
                                                                                            <div class="panel panel-heading">
                                                                                                Published Book
                                                                                            </div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PanelBook" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:ListView ID="lvPublishedBooks" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No.</th>
                                                                                                                            <th>Title of Book</th>
                                                                                                                            <th>Name of Publisher</th>
                                                                                                                            <th>Publication Types</th>
                                                                                                                            <th>ISSN/ISBN No.</th>
                                                                                                                            <th>Main Authors</th>
                                                                                                                            <th>No. of Co_Authors</th>
                                                                                                                            <th>API Score Claimed
                                                                                                                            </th>
                                                                                                                            <th id="lblverapis" runat="server">Verified API Score
                                                                                                                            </th>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblbookTitle" runat="server" Text='<%# Eval("Title_Of_Book") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPublisherName" runat="server" Text='<%# Eval("Publisher_Name") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPublicationTypes" runat="server" Text='<%# Eval("PUBLICATION_TYPE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblISSNNo" runat="server" Text='<%# Eval("ISBN") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblMainAuthor" runat="server" Text='<%# Eval("MAIN_AUTHOR") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblCoAuthor" runat="server" Text='<%# Eval("CO_AUTHOR") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtbookApiClaimed" runat="server" onblur="CalculatePublication('lvPublishedBooks','txtbookApiClaimed','txtPubBook')" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtbookApiClaimed" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtbookVerifiedApiScore" runat="server" onblur="CalculatePublication('lvPublishedBooks','txtbookVerifiedApiScore','txtbookpub')" MaxLength="50" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                </asp:Panel>
                                                                                                <div class="col-md-12">
                                                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="div41" runat="server">
                                                                                                        <label>API Score Claimed Total</label>
                                                                                                        <asp:TextBox ID="txtPubBook" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="div42" runat="server">
                                                                                                        <label id="lblvscore" runat="server">Verified API Score Total</label>
                                                                                                        <asp:TextBox ID="txtbookpub" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        <asp:HiddenField ID="HiddenField9" runat="server" Value="0" />
                                                                                                        <asp:HiddenField ID="HiddenField10" runat="server" />
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>


                                                                                    <asp:Panel ID="PnlChapters" runat="server">
                                                                                        <div class="panel panel-info">
                                                                                            <div class="panel panel-heading">
                                                                                                Chapters Published In Books
                                                                                            </div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PanelChapters" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:ListView ID="lvPublishedChapter" runat="server">

                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No.</th>
                                                                                                                            <th>Title of Book</th>
                                                                                                                            <th>Name of Publisher</th>
                                                                                                                            <th>Publication Types</th>
                                                                                                                            <th>ISSN/ISBN No.</th>
                                                                                                                            <th>No. of Chapters</th>
                                                                                                                            <th>API Score Claimed
                                                                                                                            </th>
                                                                                                                            <th id="lblvescore" runat="server">Verified API Score
                                                                                                                            </th>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblbookTitle" runat="server" Text='<%# Eval("Title_Of_Books") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPublisherName" runat="server" Text='<%# Eval("Publisher_Name") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPublicationTypes" runat="server" Text='<%# Eval("PUBLICATION_TYPE") %>'></asp:Label>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblISSNNo" runat="server" Text='<%# Eval("ISBN") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblNoChapters" runat="server" Text='<%#Eval("No_of_Chapters") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtChapterApiClaimed" runat="server" onblur="CalculatePublication('lvPublishedChapter','txtChapterApiClaimed','txtPubChapter')" MaxLength="50" Text='<%#Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtChapterApiClaimed" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtChapterVerifiedApiScore" runat="server" onblur="CalculatePublication('lvPublishedChapter','txtChapterVerifiedApiScore','txtChapterPub')" MaxLength="50" Text='<%#Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div45" runat="server">
                                                                                                            <label>API Score Claimed Total</label>
                                                                                                            <asp:TextBox ID="txtPubChapter" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div46" runat="server">
                                                                                                            <label id="lvlvescore" runat="server">Verified API Score Total</label>
                                                                                                            <asp:TextBox ID="txtChapterPub" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            <asp:HiddenField ID="HiddenField13" runat="server" Value="0" />
                                                                                                            <asp:HiddenField ID="HiddenField14" runat="server" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>



                                                                                    <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="pnlPublishedSubmit" runat="server">
                                                                                            <p class="text-center">
                                                                                                <asp:Button ID="btnPublishedDetailsSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnPublishedDetailsSubmit_Click"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                <%--<asp:Button ID="btnPublishedDetailsSubmit" runat="server" Text="Save" TabIndex="15"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />--%>
                                                                                                <asp:Button ID="btnPublishedNext" runat="server" Text="Next" TabIndex="16"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Papers_In_Conference_Click" />
                                                                                                <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                    ValidationGroup="Personal" />
                                                                                            </p>
                                                                                        </asp:Panel>
                                                                                    </div>




                                                                                </asp:View>

                                                                                <asp:View ID="Papers_In_Conference_Proceeds" runat="server" OnActivate="View_Papers_In_Conference_Activate" OnDeactivate="View_Papers_In_Conference_Deactivate">
                                                                                     <asp:Label ID="lblconfempserbook" runat="server"  Visible="false" Text="Please Fill Employee service book" ForeColor="#cc3300"></asp:Label>
                                                                                  
                                                                                     <asp:Panel ID="PnlConference" runat="server">
                                                                                        <div>
                                                                                            <br />
                                                                                        </div>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <div class="bg-light-blue">Papers In Conference Proceedings</div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PanelConference" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:HiddenField ID="hdnConference" Value="" runat="server" />
                                                                                                    <asp:ListView ID="lvConference" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No
                                                                                                                            </th>
                                                                                                                            <th>Title with Page No.
                                                                                                                            </th>
                                                                                                                            <th>Publication Type
                                                                                                                            </th>
                                                                                                                            <th>Conference Publication</th>
                                                                                                                            <th>Abstract
                                                                                                                            </th>
                                                                                                                            <th>ISSN/ISBN No.
                                                                                                                            </th>
                                                                                                                            <th>No of Co_Author
                                                                                                                            </th>
                                                                                                                            <th>Main Author
                                                                                                                            </th>
                                                                                                                            <th>API Score Claimed
                                                                                                                            </th>
                                                                                                                            <th id="lblvesco" runat="server">Verified API Score
                                                                                                                            </th>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                                                                </td>

                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("TITLE_PAGENO") %>' />

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPublicationType" runat="server" Text='<%# Eval("TYPE") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPubConference" runat="server" Text='<%# Eval("CONFERENCE_PUBLICATION") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblAbstract" runat="server" Text='<%# Eval("ABSTRACT") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblCoAuthor" runat="server" Text='<%# Eval("CO_AUTHOR") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblMainAuthor" runat="server" Text='<%# Eval("MAIN_AUTHOR") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtApiScore" runat="server" onblur="CalculateConference('lvConference','txtApiScore','txtPubConfer')" MaxLength="50" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                 <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtApiScore" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                     </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtVerifiedApi" runat="server" onblur="CalculateConference('lvConference','txtVerifiedApi','txtConferencePub')" MaxLength="50" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div47" runat="server">
                                                                                                            <label>API Score Claimed Total</label>
                                                                                                            <asp:TextBox ID="txtPubConfer" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div48" runat="server">
                                                                                                            <label id="lblverfy" runat="server">Verified API Score Total</label>
                                                                                                            <asp:TextBox ID="txtConferencePub" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            <asp:HiddenField ID="HiddenField15" runat="server" Value="0" />
                                                                                                            <asp:HiddenField ID="HiddenField16" runat="server" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>


                                                                                    <asp:Panel ID="PnlAvishkar" runat="server">
                                                                                        <div class="panel panel-info">
                                                                                            <div class="panel panel-heading">
                                                                                                Avishkar
                                                                                            </div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PanelAvishkar" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:ListView ID="lvAvishkar" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No</th>
                                                                                                                            <th>TITLE_PAPER</th>
                                                                                                                            <th>AVISHKAR</th>
                                                                                                                            <th>PRIZE_WON</th>
                                                                                                                            <th>MAIN_AUTHOR</th>
                                                                                                                            <th>CO_AUTHOR</th>
                                                                                                                            <th>API Score Claimed</th>
                                                                                                                            <th id="lblvers" runat="server">Verified API Score</th>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTitlePaper" runat="server" Text='<%# Eval("TITLE_PAPER") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblAvishkar" runat="server" Text='<%# Eval("AVISHKAR") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPrizeWon" runat="server" Text='<%# Eval("PRIZE_WON") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblMainAuthor" runat="server" Text='<%# Eval("MAIN_AUTHOR") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblCoAuthor" runat="server" Text='<%# Eval("CO_AUTHOR") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtApiScore" runat="server" onblur="CalculateConference('lvAvishkar','txtApiScore','txtPubliAvishkar')" MaxLength="50" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="Filter1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtApiScore" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                     </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtVerifiedApi" runat="server" onblur="CalculateConference('lvAvishkar','txtVerifiedApi','txtAvishkarPubl')" MaxLength="50" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtVerifiedApi" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div49" runat="server">
                                                                                                            <label>API Score Claimed Total</label>
                                                                                                            <asp:TextBox ID="txtPubliAvishkar" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div50" runat="server">
                                                                                                            <label id="lblver12" runat="server">Verified API Score Total</label>
                                                                                                            <asp:TextBox ID="txtAvishkarPubl" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            <asp:HiddenField ID="HiddenField17" runat="server" Value="0" />
                                                                                                            <asp:HiddenField ID="HiddenField18" runat="server" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>


                                                                                    <asp:Panel ID="PnlCompletedProject" runat="server">
                                                                                        <div class="panel panel-info">
                                                                                            <div class="panel panel-heading">
                                                                                                Ongoing And Completed Research Projecs And Consultancies
                                                                                            </div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PnlProjects" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:ListView ID="lvProjects" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No</th>
                                                                                                                            <th>TITLE</th>
                                                                                                                            <th>AGENCY</th>
                                                                                                                            <th>PERIOD</th>
                                                                                                                            <th>AMOUNT_MOBILIZED</th>
                                                                                                                            <th>PROJECT_TYPE</th>
                                                                                                                            <th>PRICIPAL_INVESTIGATOR</th>
                                                                                                                            <th>NO_OF_CO_INVESTOR</th>
                                                                                                                            <th>API Score Claimed
                                                                                                                            </th>
                                                                                                                            <th id="lblvscore" runat="server">Verified API Score
                                                                                                                            </th>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("TITLE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblAgency" runat="server" Text='<%# Eval("AGENCY") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("PERIOD") %>'></asp:Label>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblAmountM" runat="server" Text='<%# Eval("AMOUNT_MOBILIZED") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblProjectType" runat="server" Text='<%#Eval("PROJECT_TYPE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPrincipal" runat="server" Text='<%#Eval("PRICIPAL_INVESTIGATOR") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblInvestor" runat="server" Text='<%#Eval("NO_OF_CO_INVESTOR") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtApiScore" runat="server" onblur="CalculateConference('lvProjects','txtApiScore','txtApiProject')" MaxLength="50" Text='<%#Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                               <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers" TargetControlID="txtApiScore" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                      </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtVerifiedApi" runat="server" onblur="CalculateConference('lvProjects','txtVerifiedApi','txtConsultancyApi')" MaxLength="50" Text='<%#Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                 <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" TargetControlID="txtVerifiedApi" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                     </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div51" runat="server">
                                                                                                            <label>API Score Claimed Total</label>
                                                                                                            <asp:TextBox ID="txtApiProject" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div52" runat="server">
                                                                                                            <label id="lblver13" runat="server">Verified API Score Total</label>
                                                                                                            <asp:TextBox ID="txtConsultancyApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            <asp:HiddenField ID="HiddenField19" runat="server" Value="0" />
                                                                                                            <asp:HiddenField ID="HiddenField20" runat="server" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>

                                                                                    <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlConferenceSubmit" runat="server">
                                                                                            <p class="text-center">
                                                                                                <asp:Button ID="btnConferenceSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnConferenceSubmit_Click"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                <asp:Button ID="PnlConferenceNext" runat="server" Text="Next" TabIndex="16"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Research_Guidance_Click" />
                                                                                                <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                    ValidationGroup="Personal" />
                                                                                            </p>
                                                                                        </asp:Panel>
                                                                                    </div>

                                                                                </asp:View>

                                                                                <asp:View ID="Patent_IPR" runat="server" OnActivate="View_Patent_IPR_Activate" OnDeactivate="View_Patent_IPR_Deactivate">
                                                                                     <asp:Label ID="lblpatentempserbook" runat="server" Visible="false" ForeColor="#cc3300" Text="Please Fill Employee Service Book." ></asp:Label>
                                                                                  
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlPatent" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Patent/IPR</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="form-group col-md-6"></div>
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PanelPatent" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnPatent" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvPatent" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Sr.No</th>
                                                                                                                                    <th>Title</th>
                                                                                                                                    <th>REG. NO.</th>
                                                                                                                                    <th>Submitted</th>
                                                                                                                                    <th>Granted</th>
                                                                                                                                    <th>API Score Claimed</th>
                                                                                                                                    <th id="lvlver" runat="server">Verified API Score</th>
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
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="Label15" runat="server" Text='<%# Eval("PCNO") %>'></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("TITLE") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REG_NO") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSubmitted" runat="server" Text='<%# Eval("SUBMITTED") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblGranted" runat="server" Text='<%# Eval("GRANTED") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtApiPatent" runat="server" onblur="CalculatePatent('lvPatent','txtApiPatent','txtPatentApiScore')" MaxLength="50" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                       <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers" TargetControlID="txtApiPatent" ValidChars="txtApiPatent"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtVerifiedPatent" runat="server" onblur="CalculatePatent('lvPatent','txtVerifiedPatent','txtPatentVerified')" MaxLength="50" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                       <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" TargetControlID="txtVerifiedPatent" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                            <div class="col-md-12">
                                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div4" runat="server">
                                                                                                                    <label>API Score Claimed</label>
                                                                                                                    <asp:TextBox ID="txtPatentApiScore" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div6" runat="server">
                                                                                                                    <label id="lblveri" runat="server">Verified API Score</label>
                                                                                                                    <asp:TextBox ID="txtPatentVerified" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                                    <asp:HiddenField ID="HiddenField23" runat="server" Value="0" />
                                                                                                                    <asp:HiddenField ID="HiddenField24" runat="server" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </asp:Panel>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                        <div>
                                                                                            <br />
                                                                                        </div>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <asp:Panel ID="Panel12" runat="server">
                                                                                                <p class="text-center">
                                                                                                    <asp:Button ID="btnPatentSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnPatentSubmit_Click"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                    <asp:Button ID="btnPatentNext" runat="server" Text="Next" TabIndex="16"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here To Go Next" />
                                                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                        ValidationGroup="Personal" />
                                                                                                </p>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="Panel4" runat="server"  >
                                                                                            <div class="panel panel-heading"><b>CATEGORY-III:</b></div><br />
                                                                                           <div class="row">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="div66" runat="server">
                                                                                                                <label><b>TOTAL API SCORE CLAIMED</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12" id="divpc" runat="server">
                                                                                                            
                                                                                                    <asp:TextBox ID="txtpclaim" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>
                                                                                                 <div class="row" id="divpv" runat="server" visible="false">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="div69" runat="server">
                                                                                                                <label><b>TOTAL API SCORE VERIFY</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12" >
                                                                                                            
                                                                                                    <asp:TextBox ID="txtpver" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>

                                                                                        </asp:Panel>
                                                                                        </div>
                                                                                        <div>
                                                                                            <br />
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:View>

                                                                                <asp:View ID="Research_Guidance" runat="server" OnActivate="View_Research_Guidance_Activate" OnDeactivate="View_Research_Guidance_Deactivate">
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlGuidance" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Research Guidance/Qualification:</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="row" id="divguaidance" runat="server">
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divGuidance" runat="server">
                                                                                                            <div class="label-dynamic">
                                                                                                                <label>Research Guidance</label>
                                                                                                            </div>
                                                                                                            <asp:DropDownList ID="ddlGuidance" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Research Guidance" TabIndex="5">
                                                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">M.Phil/ME/M Pharm</asp:ListItem>
                                                                                                                <asp:ListItem Value="2">MCA/M.ed/MSC</asp:ListItem>
                                                                                                                <asp:ListItem Value="3">Ph.D. or Equivalent</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divThesis" runat="server">
                                                                                                            <div class="label-dynamic">
                                                                                                                <label>Thesis Submitted</label>
                                                                                                            </div>
                                                                                                            <asp:DropDownList ID="ddlThesis" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Thesis Submitted" TabIndex="6">
                                                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label><span style="color: #FF0000">*</span>Number Enrolled:</label>
                                                                                                            <asp:TextBox ID="txtEnrolled" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Enrolled Number" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Enrolled Number" Style="text-transform: uppercase;" Visible="false"></asp:TextBox>
                                                                                                            <asp:TextBox ID="TextBox2" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Enrolled Number" Style="text-transform: uppercase;" Visible="false"></asp:TextBox>
                                                                                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtEnrolled" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div18" runat="server">
                                                                                                            <div class="label-dynamic">
                                                                                                                <label>Degree Awarded</label>
                                                                                                            </div>
                                                                                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Degree Awarded" TabIndex="6">
                                                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group col-md-6"></div>
                                                                                                    <div class="form-group col-md-6">
                                                                                                        <p class="text-center">
                                                                                                            <p>
                                                                                                                <asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label>
                                                                                                            </p>
                                                                                                            <asp:Button ID="btnResarchGuidance" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnResarchGuidance_Click"
                                                                                                                ValidationGroup="resarchpaper" ToolTip="Click here to Add Research Guidance" TabIndex="31" />
                                                                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                                ValidationGroup="resarchpaper" />



                                                                                                        </p>

                                                                                                    </div>

                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlReearchGuid" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnGuidance" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvGuidance" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">

                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Remove   </th>
                                                                                                                                    <th>Research Guidance
                                                                                                                                    </th>
                                                                                                                                    <th>Number Enrolled
                                                                                                                                    </th>
                                                                                                                                    <th>Thesis Submitted
                                                                                                                                    </th>
                                                                                                                                    <th>Degree Awarded
                                                                                                                                    </th>
                                                                                                                                    <th>API Score Claimed
                                                                                                                                    </th>
                                                                                                                                    <th id="lblvery" runat="server">Verified API Score
                                                                                                                                    </th>
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
                                                                                                                        <td>
                                                                                                                            <asp:ImageButton ID="btnEditResarchPaper" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                                                                                OnClick="btnEditResearchGuidance_Click" ImageUrl="~/images/delete.png" ToolTip="Edit Record" />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblRGuidance" runat="server" Text='<%# Eval("Research_Guidance") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblNEnrolled" runat="server" Text='<%# Eval("Number_Enrolled") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblThesis" runat="server" Text='<%# Eval("Thesis_Submitted") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblAwarded" runat="server" Text='<%# Eval("Degree_Awarded") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtApiScore" runat="server" onblur="CalculateGuidance('lvGuidance','txtApiScore','txtGuidApi')" MaxLength="50" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtVerifiedApiScore" runat="server" onblur="CalculateGuidance('lvGuidance','txtVerifiedApiScore','txtVerifiedGuidApi')" MaxLength="50" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                       
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                         <div class="col-md-12">
                                                                                                            <asp:Panel runat="server" ID="PnlGuidanceApi" ScrollBars="Auto">
                                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div53" runat="server">
                                                                                                                    <label>API Score Claimed Total</label>
                                                                                                                    <asp:TextBox ID="txtGuidApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div54" runat="server" >
                                                                                                                    <label id="lblverify14" runat="server">Verified API Score Total</label>
                                                                                                                    <asp:TextBox ID="txtVerifiedGuidApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                                    <asp:HiddenField ID="HiddenField21" runat="server" Value="0" />
                                                                                                                    <asp:HiddenField ID="HiddenField22" runat="server" />
                                                                                                                </div>
                                                                                                            </asp:Panel>
                                                                                                        </div>
                                                                                    </div>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlQualification" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Research Qualification:</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="row">
                                                                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div19" runat="server">
                                                                                                            <div class="label-dynamic">
                                                                                                                <label>Qualification</label>
                                                                                                            </div>
                                                                                                            <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Research Qualification" TabIndex="5">
                                                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">Ph.D.</asp:ListItem>
                                                                                                                <asp:ListItem Value="2">M.Phil/ME/M Pharm</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div20" runat="server">
                                                                                                            <div class="label-dynamic">
                                                                                                                <label>Submitted</label>
                                                                                                            </div>
                                                                                                            <asp:DropDownList ID="ddlSubmitted" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Submitted" TabIndex="6">
                                                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div21" runat="server">
                                                                                                            <div class="label-dynamic">
                                                                                                                <label>Awarded</label>
                                                                                                            </div>
                                                                                                            <asp:DropDownList ID="ddlAwarded" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Awarded" TabIndex="6">
                                                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group col-md-6"></div>
                                                                                                    <div class="form-group col-md-6">
                                                                                                        <p class="text-center">
                                                                                                            <p>
                                                                                                                <asp:Label ID="Label5" runat="server" Text="" Visible="false"></asp:Label>
                                                                                                            </p>
                                                                                                            <asp:Button ID="btnResarchQualification" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnResarchQualification_Click"
                                                                                                                ValidationGroup="resarchpaper" ToolTip="Click here to Add Research Qualification" TabIndex="31" />
                                                                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                                ValidationGroup="resarchpaper" />
                                                                                                            <p>
                                                                                                            </p>
                                                                                                            <p>
                                                                                                            </p>
                                                                                                        </p>
                                                                                                    </div>
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlResQualification" ScrollBars="Auto">
                                                                                                            <asp:ListView ID="lvQualification" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">

                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Remove</th>
                                                                                                                                    <th>Qualification
                                                                                                                                    </th>
                                                                                                                                    <th>Submitted
                                                                                                                                    </th>
                                                                                                                                    <th>Awarded
                                                                                                                                    </th>
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
                                                                                                                        <td>
                                                                                                                            <asp:ImageButton ID="btnEditResarchQualifiPaper" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                                                                                OnClick="btnEditResearchQualification_Click" ImageUrl="~/images/delete.png" ToolTip="Edit Record" />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblRQualification" runat="server" Text='<%# Eval("Qualification") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSubmitted" runat="server" Text='<%# Eval("Submitted") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblAwarded" runat="server" Text='<%# Eval("Awarded") %>' />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                    <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlResearchSubmit" runat="server">
                                                                                            <p class="text-center">
                                                                                                <asp:Button ID="btnResearchSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnResearchSubmit_Click"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                <asp:Button ID="btnResearchNext" runat="server" Text="Next" TabIndex="16"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Patent_IPR_Click" />
                                                                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                    ValidationGroup="Personal" />
                                                                                            </p>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </asp:View>

                                                                                <asp:View ID="Innovative_Teaching" runat="server" OnActivate="View_Innovative_Teaching_Activate" OnDeactivate="View_Innovative_Teaching_Deactivate">
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlInnovative" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Innovative Teaching Learning Methods (MAXIMUM SCORE: 20 POINTS):</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="form-group col-md-6"></div>
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PanelInnovative" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnApiCount" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvInnovative" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Sr.No.   </th>
                                                                                                                                    <th>Study Material/ Resources
                                                                                                                                    </th>
                                                                                                                                    <th id="lblapiscore" runat="server">API Score Claimed
                                                                                                                                    </th>
                                                                                                                                    <th id="lblverifiedapiscore" runat="server">Verified API Score
                                                                                                                                    </th>
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
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SN_ID") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblMaterial" runat="server" Text='<%# Eval("STUDY_MATERIAL") %>' />

                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtAPIScore" runat="server" onblur="CalculateInnovative('lvInnovative','txtAPIScore','txtApiScoretotal');" MaxLength="200" Text='<%# Eval("API_Score_Claimed") %>' />
                                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FiBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIScore" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtAPIVerified" runat="server" onblur="CalculateInnovative('lvInnovative','txtAPIVerified','txtVerifiedApiS');" MaxLength="200" Text='<%# Eval("Verified_API_Score") %>' />
                                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIVerified" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div9" runat="server">
                                                                                                                <label>API Score Claimed</label>
                                                                                                                <asp:TextBox ID="txtApiScoretotal" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div10" runat="server">
                                                                                                                <label id="lblvapi1" runat="server">Verified Api Score</label>
                                                                                                                <asp:TextBox ID="txtVerifiedApiS" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                                                                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>

                                                                                        <asp:Panel ID="PnlFeedback" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Students Feedback:</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="form-group col-md-6"></div>
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PanelFeedback" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnFeedback" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvFeedback" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Sr.No.</th>
                                                                                                                                    <th>Class</th>
                                                                                                                                    <th>No. of Students Involved In Feedback
                                                                                                                                    </th>
                                                                                                                                    <th>Feedback Frequency Per Course
                                                                                                                                    </th>
                                                                                                                                    <th>Methodology
                                                                                                                                    </th>
                                                                                                                                    <th id="lblapi1" runat="server">API Score Claimed
                                                                                                                                    </th>
                                                                                                                                    <th id="lblapi2" runat="server">API Score Verified</th>
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
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SN_ID") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtClass" runat="server" MaxLength="200" Text='<%# Eval("Class") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtStudInvolved" runat="server" MaxLength="200" Text='<%# Eval("No_Of_Stud_Involve_Feedback") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtFeedback" runat="server" MaxLength="200" Text='<%# Eval("Feedback_Frequency_Per_Course") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblMethodology" runat="server" Text='<%# Eval("Methodology") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtAPIScores" runat="server" onblur="CalculateFeedback('lvFeedback','txtAPIScores','txtApi');" MaxLength="200" Text='<%# Eval("Api_Score_Claime") %>' />
                                                                                                                       <ajaxToolKit:FilteredTextBoxExtender ID="FiBoxE7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIScores" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtAPIVerified" runat="server" onblur="CalculateFeedback('lvFeedback','txtAPIVerified','txtVerified');" MaxLength="200" Text='<%# Eval("Api_Score_Verified") %>' />
                                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIVerified" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div16" runat="server">
                                                                                                                <label>API Score Claimed</label>
                                                                                                                <asp:TextBox ID="txtApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div17" runat="server">
                                                                                                                <label id="lblver" runat="server">Verified Api Score</label>
                                                                                                                <asp:TextBox ID="txtVerified" runat="server" CssClass="form-control" MaxLength="20" ToolTip="Sem II Total" Enabled="false"></asp:TextBox>
                                                                                                                <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                                                                                                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>

                                                                                        <asp:Panel ID="PnlExam" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Examination Related Work:</b></div>
                                                                                                <div class="panel panel-body">

                                                                                                    <div class="form-group col-md-6"></div>
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlExamination" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnExamination" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvExamination" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Sr.No.</th>
                                                                                                                                    <th>Type of Examination Work</th>
                                                                                                                                    <th id="lblapi2" runat="server">API Score Claimed
                                                                                                                                    </th>
                                                                                                                                    <th id="lblver2" runat="server">API Score Verified</th>
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
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SN_ID") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblWork" runat="server" Text='<%# Eval("TYPE_OF_WORK") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtAPIScores" runat="server" onblur="CalculateExamination('lvExamination','txtAPIScores','txtAi')" MaxLength="200" Text='<%# Eval("Api_Score_Claime") %>' />
                                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilExtender12" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIScores" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtAPIVerified" runat="server" onblur="CalculateExamination('lvExamination','txtAPIVerified','txtApiVerify')" MaxLength="200" Text='<%# Eval("Api_Score_Verified") %>' />
                                                                                                                         <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIVerified" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div23" runat="server">
                                                                                                                <label>API Score Claimed</label>
                                                                                                                <asp:TextBox ID="txtAi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div27" runat="server">
                                                                                                                <label id="lblverify" runat="server">Verified Api Score</label>
                                                                                                                <asp:TextBox ID="txtApiVerify" runat="server" CssClass="form-control" MaxLength="20" ToolTip="Sem II Total" Enabled="false"></asp:TextBox>
                                                                                                                <asp:HiddenField ID="HiddenField5" runat="server" Value="0" />
                                                                                                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>

                                                                                        


                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <asp:Panel ID="Panel15" runat="server">
                                                                                                <p class="text-center">
                                                                                                    <asp:Button ID="btnInnovativeSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnInnovativeSubmit_Click"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                    <asp:Button ID="btnInnovativeNext" runat="server" Text="Next" TabIndex="16"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Student_Co_Curricular_Click" />
                                                                                                </p>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="Pnlexamwork" runat="server">
                                                                                            <div class="panel panel-heading"><b>CATEGORY-1:</b></div><br />
                                                                                           <div class="row">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="divcategory" runat="server">
                                                                                                                <label><b>TOTAL API SCORE CLAIMED</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12" id="div30" runat="server">
                                                                                                            
                                                                                                    <asp:TextBox ID="txtcategorytot" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>
                                                                                                 <div class="row" id="div32" runat="server" visible="false">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="div31" runat="server">
                                                                                                                <label><b>TOTAL API SCORE VERIFY</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12" >
                                                                                                            
                                                                                                    <asp:TextBox ID="txtveryf" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>

                                                                                        </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>

                                                                                </asp:View>
                                                                                <asp:View ID="Teacher_Learning_Activities" runat="server" OnActivate="View_Teacher_Learning_Activities_Activate" OnDeactivate="View_Teacher_Learning_Activities_Deactivate">
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlLearningActivity" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Teaching Learning Activities</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlLearning" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdRowCount" Value="" runat="server" />

                                                                                                            <%--<asp:ListView ID="lvTeachActivity" runat="server" OnItemDataBound="lvTeachActivity_ItemDataBound">--%>
                                                                                                            <asp:ListView ID="lvTeachActivity" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table id="lvfieldsTbl" runat="server" class="table table-bordered table-hover">
                                                                                                                            <tr class="bg-light-blue">
                                                                                                                                <th>Sr.No</th>
                                                                                                                                <th>SEMESTER</th>
                                                                                                                                <th>SEM I</th>
                                                                                                                                <th>SEM II</th>
                                                                                                                            </tr>
                                                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                                                        </table>
                                                                                                                    </div>
                                                                                                                </LayoutTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SN_ID") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtSem1" runat="server" CssClass="form-control" Text='<%# Eval("SEM1") %>'
                                                                                                                                onblur="CalculateTotalMark('lvTeachActivity','txtSem1','txtsemI');" MaxLength="15"></asp:TextBox>
                                                                                                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilText7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtSem1" ValidChars=".0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtSem2" runat="server" CssClass="form-control" Text='<%# Eval("SEM2") %>'
                                                                                                                                onblur="CalculateTotalMark('lvTeachActivity','txtSem2','txtsemII');" MaxLength="15"></asp:TextBox>
                                                                                                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilTextExt16" runat="server" FilterType="Custom, Numbers" TargetControlID="txtSem2" ValidChars=".0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divSemI" runat="server">
                                                                                                                <label>SEM-I Total</label>
                                                                                                                <asp:TextBox ID="txtsemI" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divSemII" runat="server">
                                                                                                                <label>SEM-II Total</label>
                                                                                                                <asp:TextBox ID="txtsemII" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                                <asp:HiddenField ID="hdnFieldsCount" runat="server" Value="0" />
                                                                                                                <asp:HiddenField ID="hdnrowcount" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <asp:Panel ID="Panel20" runat="server">
                                                                                                <p class="text-center">
                                                                                                    <asp:Button ID="btnTeachingActivitySubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnTeachingActivitySubmit_Click"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                    <%--<asp:Button ID="btnTeachingActivitySubmit" runat="server" Text="Save" TabIndex="15"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />--%>
                                                                                                    <asp:Button ID="btnNextSemester" runat="server" Text="Next" TabIndex="16"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Performance_In_Engaging_Lectures_Click" />
                                                                                                </p>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:View>

                                                                                <asp:View ID="Performance_In_Engaging_Lectures" runat="server" OnActivate="View_Performance_In_Engaging_Lectures_Activate" OnDeactivate="View_Performance_In_Engaging_Lectures_Deactivate">
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlEngaged" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <%--<div class="panel panel-heading"><b>Performance In Engaging Lectures</b></div>--%>
                                                                                                 <div class="panel panel-heading"><b>1.1A)  PERFORMANCE IN ENGAGING LECTURES / PRACTICALS/ TUTORIALS /ADMINISTRATIVE LOAD/RESEARCH SUPERVISION/PROJECT GUIDANCE (MAXIMUM SCORE: 50 POINTS)</b></div>
                                                                                               <br /> <div class="panel panel-body">
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlLectures" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnEngagingLecture"  runat="server" />
                                                                                                            <asp:ListView ID="lvEngagingLectures" runat="server">  
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table id="lvEngaging" runat="server" class="table table-bordered table-hover">
                                                                                                                            <%--<table class="table table-bordered table-hover">--%>
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Sr.No</th>
                                                                                                                                    <th>COURSE</th>
                                                                                                                                    <th>SUBJECT TAUGHT</th>
                                                                                                                                    <th>No. OF HOURS TARGETED</th>
                                                                                                                                    <th>HRS ACTUALLY ACHIEVED</th>
                                                                                                                                    <th>% TARGET ACHIEVED</th>
                                                                                                                                     
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
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SN_ID") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtCourse" runat="server" MaxLength="200" CssClass="form-control" Text='<%# Eval("CLASS") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("SUBJECT_TAUGHT") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:Label ID="lblSem1" runat="server" Text='<%# Eval("Sem_1") %>' />--%>
                                                                                                                            <%--<asp:TextBox ID="txtHrsTargeted" runat="server" MaxLength="200" CssClass="form-control" onblur="CalculateEngagLecture1(lvEngagingLectures,txtHrsTargeted,txtHrsEngaged,txtPercentTarget);" />--%><%--</asp:TextBox>--%>
                                                                                                                            <asp:TextBox ID="txtHrsTargeted" runat="server" MaxLength="200" CssClass="form-control" Text='<%# Eval("HRS_TARGETED") %>'  onblur="CalculateEngLactureAvg('lvEngagingLectures','txtHrsTargeted','txtHrsEngaged','txtPercentTarget');" />
                                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers" TargetControlID="txtHrsTargeted" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                              </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:TextBox ID="txtHrsEngaged" runat="server" MaxLength="200" CssClass="form-control" onblur="CalculateEngLactureAvg(lvEngagingLectures,txtHrsTargeted,txtHrsEngaged,txtPercentTarget);" />--%>
                                                                                                                            <asp:TextBox ID="txtHrsEngaged" runat="server" MaxLength="200" CssClass="form-control" Text='<%# Eval("HRS_ENGAGED") %>' onblur="CalculateEngLactureAvg('lvEngagingLectures','txtHrsTargeted','txtHrsEngaged','txtPercentTarget');"  />
                                                                                                                       <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Custom, Numbers" TargetControlID="txtHrsEngaged"  ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                        <td>
                                                                                                                            <%--<asp:Label ID="lblPercentTarget" runat="server" Text='<%# Eval("PERCENT_TARGET_ACHIEVED") %>' />--%>
                                                                                                                            <asp:TextBox ID="txtPercentTarget" runat="server" MaxLength="100" CssClass="form-control" Text='<%# Eval("PERCENT_TARGET_ACHIEVED") %>'  />
                                                                                                                            <%--<asp:TextBox ID="txtPercentTarget" runat="server" CssClass="form-control" Text='<%# Eval("PERCENT_TARGET_ACHIEVED")%>' onblur="return CalTargetAchieved(this);"></asp:TextBox>--%>
                                                                                                                       <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Custom, Numbers" TargetControlID="txtPercentTarget" ValidChars="0123456789."></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                       
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>

                                                                                        <asp:Panel ID="pnlcalculation" runat="server">
                                                                                            <div class="row">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div13" runat="server">
                                                                                                                <label>Average</label>
                                                                                                                 <asp:TextBox ID="txtavg1" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>

                                                                                                            </div>
                                                                                                 <div class="form-group col-lg-6 col-md-6 col-12" id="div15" runat="server">
                                                                                                                <label>Performance & Multiplying Factor</label>
                                                                                                                <asp:TextBox ID="txtlbl" runat="server" CssClass="form-control"  Enabled="false" ></asp:TextBox>

                                                                                                            </div>
                                                                                                </div>
                                                                                             <div class="row">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div25" runat="server">
                                                                                                                <label>Max.Weight</label>
                                                                                                                 <asp:TextBox ID="txtmaxw" runat="server" CssClass="form-control"  Text="50" Enabled="false"></asp:TextBox>
                                                                                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilTex11" runat="server" FilterType="Custom, Numbers" TargetControlID="txtmaxw" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>--%>

                                                                                                            </div>
                                                                                                  <div class="form-group col-lg-6 col-md-6 col-12" id="div26" runat="server">
                                                                                                                <label>API Score Claimed</label>
                                                                                                                 <asp:TextBox ID="txtapic" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                                 <ajaxToolKit:FilteredTextBoxExtender ID="FilTex18" runat="server" FilterType="Custom, Numbers" TargetControlID="txtapic" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                            </div>
                                                                                                 </div>
                                                                                              <div class="row">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="diver" runat="server" visible="false">
                                                                                                                <label>API Score Verify</label>
                                                                                                                 <asp:TextBox ID="txtapiv" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                                                                 <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterType="Custom, Numbers" TargetControlID="txtapiv" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                            </div>
                                                                                           
                                                                                        </asp:Panel>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <asp:Panel ID="Panel23" runat="server">
                                                                                                <p class="text-center">
                                                                                                    <asp:Button ID="btnEngagingLecturesSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnEngagingLecturesSubmit_Click"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                    <%--<asp:Button ID="btnEngagingLecturesSubmit" runat="server" Text="Save" TabIndex="15"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" /> --%>
                                                                                                    <asp:Button ID="btnEngageNext" runat="server" Text="Next" TabIndex="16"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Attendance_Performance_Click" />
                                                                                                </p>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:View>

                                                                                <asp:View ID="Lectures_Academic_Duties" runat="server" OnActivate="View_Lectures_Academic_Duties_Activate" OnDeactivate="View_Lectures_Academic_Duties_Deactivate">
                                                                                    <%--<asp:Panel ID="PnlDuties" runat="server">--%>
                                                                                    <%-- <div>
                                                                                           <br />                                                                                         
                                                                                   </div>--%>
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlDuties" runat="server">
                                                                                            <div class="bg-light-blue">Lectures And Academic Duties In Excess Of UGC Norms</div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PnlUGCNorms" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:HiddenField ID="hdnExcess" Value="" runat="server" />
                                                                                                    <asp:ListView ID="lvAcadDuties" runat="server" >
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No
                                                                                                                            </th>
                                                                                                                            <th>Type of Activity
                                                                                                                            </th>
                                                                                                                            <th>No. of Students Benefited
                                                                                                                            </th>
                                                                                                                            <th>No. of Hrs Engaged for the Activity
                                                                                                                            </th>
                                                                                                                            <%-- <th>API Score Claimed
                                                                                                                            </th>
                                                                                                                            <th>Verified API Score
                                                                                                                            </th>--%>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSN" runat="server" Text='<%# Eval("SN_ID") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblActivity" runat="server" Text='<%# Eval("TYPE_OF_ACTIVITY") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtStudBenefited" runat="server" MaxLength="200" CssClass="form-control" Text='<%# Eval("NO_OF_STUDENTS_BENIFITED") %>'  /> <%--onblur="CalculateExcess('lvAcadDuties','txtStudBenefited','txtapi1')"--%>
                                                                                                                 <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Custom, Numbers" TargetControlID="txtStudBenefited" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                     </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtHrsEngaged" runat="server" MaxLength="200" CssClass="form-control" Text='<%# Eval("NO_OF_HRS_ENGAGED_FOR_THE_ACTIVITY") %>' onblur="CalculateExcess('lvAcadDuties','txtHrsEngaged','txtapi1')" />
                                                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" TargetControlID="txtHrsEngaged" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                     </td>
                                                                                                                <%-- <td>
                                                                                                                    <%--<asp:TextBox ID="txtApiScoreClaimed" runat="server" onblur="CalculateExcess('lvAcadDuties','txtStudBenefited','txtHrsEngaged','txtApiScoreClaimed')" MaxLength="20"></asp:TextBox>--%>
                                                                                                                <%--  <asp:TextBox ID="txtApiScoreClaimed" runat="server" onblur="CalculateExcess('lvAcadDuties','txtStudBenefited','txtUGCApi')" MaxLength="20"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtVerifiedApiScore" runat="server" onblur="CalculateExcess('lvAcadDuties','txtHrsEngaged','txtUGCVerifiedapi')" MaxLength="20"></asp:TextBox>
                                                                                                                </td>--%>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                </asp:Panel>
                                                                                                <div class="col-md-12">
                                                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="div12" runat="server">
                                                                                                        <label id="LBLAPITSC" runat="server" visible="false">API Score Claimed</label>
                                                                                                        <%--  <asp:TextBox ID="txtUGCApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        --%>
                                                                                                        <asp:Label ID="LBLTXTUGAPI" runat="server" CssClass="form-control" BorderColor="White"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="div11" runat="server">
                                                                                                        <label id="LBLVAPI" runat="server">API Score Claimed</label>
                                                                                                        <asp:TextBox ID="txtapi1" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>

                                                                                                    </div>
                                                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="div55" runat="server" visible="false" >
                                                                                                        <label id="lblvaps" runat="server">Verfied Api Score</label>
                                                                                                         <asp:TextBox ID="txtUGCVerifiedapi" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                                                                                                        
                                                                                                        <asp:Label ID="LBLUGVAPI" runat="server" CssClass="form-control" BorderColor="White"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>

                                                                                    <%--</asp:View>--%>

                                                                                    <asp:Panel ID="PnlResources" runat="server">
                                                                                        <div>
                                                                                            <br />
                                                                                        </div>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <div class="bg-light-blue">Preparation of Study Material and Resources</div>
                                                                                        </div>
                                                                                        <div class="panel panel-body">
                                                                                            <asp:Panel ID="PnlMaterial" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                <asp:HiddenField ID="hdnMaterial" Value="" runat="server" />
                                                                                                <asp:ListView ID="lvResources" runat="server">
                                                                                                    <LayoutTemplate>
                                                                                                        <div id="lgv1">
                                                                                                            <table class="table table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr class="bg-light-blue">
                                                                                                                        <th>Sr.No</th>
                                                                                                                        <th>STUDY MATERIAL</th>
                                                                                                                        <th>API Score Claimed</th>
                                                                                                                        <th id="lblverify" runat="server">Verified API Score</th>
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
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSNID" runat="server" Text='<%# Eval("SN_ID") %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("STUDY_MATERIAL") %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtApiScore" runat="server" onblur="CalculateMaterial('lvResources','txtApiScore','txtMaterialApiScore')" MaxLength="20" Text='<%# Eval("API_SCORED_CLAIMED") %>'></asp:TextBox>
                                                                                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Custom, Numbers" TargetControlID="txtApiScore" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtVerifiedApi" runat="server" onblur="CalculateMaterial('lvResources','txtVerifiedApi','txtMaterialVerifiedApi')" MaxLength="21" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" TargetControlID="txtVerifiedApi" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                 </td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </asp:Panel>
                                                                                            <div class="col-md-12">
                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div35" runat="server">
                                                                                                    <label>API Score Claimed</label>
                                                                                                    <asp:TextBox ID="txtMaterialApiScore" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div40" runat="server">
                                                                                                    <label id="lblverify1" runat="server">Verfied Api Score</label>
                                                                                                    <asp:TextBox ID="txtMaterialVerifiedApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <%--</div>--%>
                                                                                    </asp:Panel>
                                                                                    <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="Panel27" runat="server">
                                                                                            <p class="text-center">
                                                                                                <%--<asp:Button ID="Button5" runat="server" Text="Save" TabIndex="15" OnClick="btnPerformanceSubmit_Click"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />--%>
                                                                                                <asp:Button ID="btnAcademicDutiesSubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnAcademicDutiesSubmit_Click"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                <asp:Button ID="btnAcadNext" runat="server" Text="Next" TabIndex="16"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Academic_Lectures_Click" />
                                                                                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                    ValidationGroup="Personal" />
                                                                                            </p>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </asp:View>



                                                                                <asp:View ID="Performance_In_Attendance" runat="server" OnActivate="View_Performance_In_Attendance_Activate" OnDeactivate="View_Performance_In_Attendance_Deactivate">
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlAttendance" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Performance In Attendance Of Students:</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="row" id="divat" runat="server">
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>Class/Course:</label>
                                                                                                            <asp:TextBox ID="txtCourse" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Course" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>Subject Taught</label>
                                                                                                            <asp:TextBox ID="txtSubject" runat="server" MaxLength="200" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Subject Taught" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>Sum of Students Present</label>
                                                                                                            <asp:TextBox ID="txtStudPresent" runat="server" MaxLength="200" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Students Present" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilTextEx16" runat="server" FilterType="Custom, Numbers" TargetControlID="txtStudPresent" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>Lectures Actually Engaged</label>
                                                                                                            <asp:TextBox ID="txtEngaged" runat="server" MaxLength="200" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Lectures Engaged" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Custom, Numbers" TargetControlID="txtEngaged" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>Students on Roll</label>
                                                                                                            <asp:TextBox ID="txtStudRoll" runat="server" MaxLength="200" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Students on Roll" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="Custom, Numbers" TargetControlID="txtStudRoll" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                        </div>
                                                                                                        <%--  <div class="form-group col-md-6">
                                                                                                            <label>Avg Attendance</label>
                                                                                                            <asp:TextBox ID="txtAvgAttendance" runat="server" MaxLength="15" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Avg Attendance"></asp:TextBox>
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                    <div class="form-group col-md-6"></div>

                                                                                                    <div class="form-group col-md-6">

                                                                                                        <p class="text-center">
                                                                                                            <p>
                                                                                                                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                                                                                                            </p>
                                                                                                            <asp:Button ID="btnStudentsAttendance" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnStudentsAttendance_Click" 
                                                                                                                ValidationGroup="resarchpaper" ToolTip="Click here to Add Performance in Students Attendance" TabIndex="31"   />
                                                                                                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                                ValidationGroup="resarchpaper" />
                                                                                                            <p>
                                                                                                            </p>

                                                                                                            <p>
                                                                                                            </p>

                                                                                                        </p>

                                                                                                    </div>

                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlAttend" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnattendance" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvAttendance" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table id="divAttendance" runat="server" class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Remove   </th>
                                                                                                                                    <th>Class/Course
                                                                                                                                    </th>
                                                                                                                                    <th>Subject Taught
                                                                                                                                    </th>
                                                                                                                                    <th>Sum of Students Present
                                                                                                                                    </th>
                                                                                                                                    <th>Lectures Actually Engaged
                                                                                                                                    </th>
                                                                                                                                    <th>Students on Roll</th>
                                                                                                                                   <th>Attendance</th>
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
                                                                                                                        <td>
                                                                                                                            <asp:ImageButton ID="btnEditStudentsAttendance" runat="server" CommandArgument='<%# Eval("SR_NO") %>'
                                                                                                                                OnClick="btnEditStudentsAttendance_Click" ImageUrl="~/images/delete.png" ToolTip="Edit Record" />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>' />

                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSubjectTaught" runat="server" Text='<%# Eval("Subject_Taught") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblStudPresent" runat="server" Text='<%# Eval("PRESENT_STUDENTS") %>' onclientclick="CalculateAttendance('lvAttendance','lblStudPresent','txtAverage')" />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblLecturesEngaged" runat="server" Text='<%# Eval("LECTURES") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblStudentsRoll" runat="server" Text='<%# Eval("STUDENT_ROLL") %>' />
                                                                                                                        </td>
                                                                                                                         <td>
                                                                                                                             <asp:Label ID="lvlavgattendance" runat="server" Text='<%# Eval("Attendance") %>' />
                                                                                                                              
                                                                                                                        </td>

                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                    </div>


                                                                                                    <asp:Panel ID="pnltest" runat="server">
                                                                                                        <div class="row">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div36" runat="server">
                                                                                                                <label>Avg. Attendance</label>
                                                                                                                <asp:TextBox ID="txtAverage" runat="server" MaxLength="20" Enabled="false" CssClass="form-control" TabIndex="27"
                                                                                                                    TollTip="Avg Attendance" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                                            </div>
                                                                                                            <asp:Label ID="lblavattendance" runat="server" BackColor="White" ForeColor="White" />
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div37" runat="server">
                                                                                                                <label>Performance & Multiplying Factor</label>
                                                                                                                <asp:TextBox ID="txtFactor" runat="server" Enabled="false" MaxLength="50" CssClass="form-control" TabIndex="27"
                                                                                                                    TollTip="Performance & Multiplying Factor" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div38" runat="server">
                                                                                                                <label>Max Weight</label>
                                                                                                                <asp:TextBox ID="txtWeight" runat="server"  CssClass="form-control"  Text="20" Readonly="true"  ></asp:TextBox>
                                                                                                                      
                                                                                                                <%--OnTextChanged="txtWeight_TextChanged --%>
                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div39" runat="server">
                                                                                                                <label>API Score Claimed</label>
                                                                                                                <asp:TextBox ID="txtScore" runat="server" MaxLength="50" CssClass="form-control" TabIndex="27" ReadOnly="true"
                                                                                                                    TollTip="API Score Claimed" onblur="multiplyBy()" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                                  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTetBoxExtender14" runat="server" FilterType="Custom, Numbers" TargetControlID="txtScore" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>


                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divattendance" runat="server" visible="false">
                                                                                                                <label>API Score Verify</label>
                                                                                                                <asp:TextBox ID="txtapiperverify" runat="server" MaxLength="50" CssClass="form-control" TabIndex="27"
                                                                                                                    TollTip="API Score Verify" onblur="multiplyBy()" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                                  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterType="Custom, Numbers" TargetControlID="txtapiperverify" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>


                                                                                                            </div>
                                                                                                            
                                                                                                        </div>
                                                                                                    </asp:Panel>
                                                                                                     
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlPerformingResult" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Performance In Results:</b></div>
                                                                                                <div class="panel panel-body">

                                                                                                    <div class="row">
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>Class/Course:</label>
                                                                                                            <asp:TextBox ID="txtClass" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Class/Course" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtEnrolled" Display="None"
                                                                                        ErrorMessage="Please Enter Enrolled Number." ValidationGroup="resarchpaper" />--%>
                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>Subject Taught</label>
                                                                                                            <asp:TextBox ID="txtSubjectT" runat="server" MaxLength="200" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Subject Taught" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>% Result of the Same Subject Last Year</label>
                                                                                                            <asp:TextBox ID="txtLstYrResult" runat="server" MaxLength="200" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Last Year Result" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilTxtBEx" runat="server" FilterType="Custom, Numbers" TargetControlID="txtLstYrResult" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>


                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6">
                                                                                                            <label>% Result of the Same Subject in the Institute</label>
                                                                                                            <asp:TextBox ID="txtInstitute" runat="server" MaxLength="200" CssClass="form-control" TabIndex="28"
                                                                                                                ToolTip="Institute Result" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilTetExt" runat="server" FilterType="Custom, Numbers" TargetControlID="txtInstitute" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>


                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="form-group col-md-6"></div>

                                                                                                    <div class="form-group col-md-6">

                                                                                                        <p class="text-center">
                                                                                                            <p>
                                                                                                                <asp:Label ID="Label9" runat="server" Text="" Visible="false"></asp:Label>
                                                                                                            </p>
                                                                                                            <asp:Button ID="btnPerformanceInResult" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnPerformanceInResult_Click"
                                                                                                                ValidationGroup="resarchpaper" ToolTip="Click here to Add Performance In Results" TabIndex="31" />
                                                                                                            <%--<asp:Button ID="btnResarchQualification" runat="server" Text="Add" CssClass="btn btn-primary"
                                                                                            ValidationGroup="resarchpaper" ToolTip="Click here to Add Research Qualification" TabIndex="31" />--%>
                                                                                                            <asp:ValidationSummary ID="ValidationSummary9" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                                ValidationGroup="resarchpaper" />

                                                                                                            <p>
                                                                                                            </p>

                                                                                                            <p>
                                                                                                            </p>

                                                                                                        </p>

                                                                                                    </div>

                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlResult" ScrollBars="Auto">
                                                                                                            <asp:ListView ID="lvResults" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">

                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Remove</th>
                                                                                                                                    <th>Class/Course
                                                                                                                                    </th>
                                                                                                                                    <th>Subject Taught
                                                                                                                                    </th>
                                                                                                                                    <th>% Result of the Same Subject Last Year
                                                                                                                                    </th>
                                                                                                                                    <th>% Result of the Same Subject in the Institute
                                                                                                                                    </th>
                                                                                                                                    <th>Average</th>

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
                                                                                                                        <td>
                                                                                                                            <asp:ImageButton ID="btnEditPerformanceResults" runat="server" CommandArgument='<%# Eval("SR_NO") %>'
                                                                                                                                OnClick="btnEditPerformanceResults_Click" ImageUrl="~/images/delete.png" ToolTip="Edit Record" />
                                                                                                                            <%--<asp:ImageButton ID="btnDelRes" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("SRNO") %>'
                                                                                                         AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeletedRes_Click"
                                                                                                          OnClientClick="showConfirmDel(this); return false;" />--%>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblClass" runat="server" Text='<%# Eval("COURSE") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSubjectT" runat="server" Text='<%# Eval("Subject_Taught") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblLstYear" runat="server" Text='<%# Eval("LAST_YEAR_RESULT") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblInstitute" runat="server" Text='<%# Eval("CURRENT_INSTITUTE_RESULT") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblaverage" runat="server" Text='<%# Eval("Average") %>' />
                                                                                                                        </td>

                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                    </div>

                                                                                                    <div class="row">
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div3" runat="server">
                                                                                                            <label>Avg. Attendance</label>
                                                                                                            <asp:TextBox ID="txtAvg" runat="server" MaxLength="20" Enabled="false" CssClass="form-control" TabIndex="27"
                                                                                                                TollTip="Avg Attendance" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div5" runat="server">
                                                                                                            <label>Performance & Multiplying Factor</label>
                                                                                                            <asp:TextBox ID="txtMulFactor" runat="server" MaxLength="50" Enabled="false" CssClass="form-control" TabIndex="27"
                                                                                                                TollTip="Performance & Multiplying Factor" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div7" runat="server">
                                                                                                            <label>Max Weight</label>
                                                                                                            <asp:TextBox ID="txtMaxWeight" runat="server" MaxLength="50" CssClass="form-control" TabIndex="27" Text="20" Enabled="false"
                                                                                                                TollTip="Max Weight" Style="text-transform: uppercase;" ></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="div8" runat="server">
                                                                                                            <label>API Score Claimed</label>
                                                                                                            <asp:TextBox ID="txtClaimed" runat="server" MaxLength="50" CssClass="form-control" TabIndex="27" ReadOnly="true"
                                                                                                                TollTip="API Score Claimed" onblur="multiplyBy1()" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers" TargetControlID="txtClaimed" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                        </div>
                                                                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divresult" runat="server" visible="false">
                                                                                                            <label>API Score Verify</label>
                                                                                                            <asp:TextBox ID="txtVerify" runat="server" MaxLength="50" CssClass="form-control" TabIndex="27"
                                                                                                                TollTip="API Score Verify" onblur="multiplyBy1()" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" FilterType="Custom, Numbers" TargetControlID="txtVerify" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                        </div>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                    <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="Panel32" runat="server">
                                                                                            <p class="text-center">
                                                                                                <asp:Button ID="btnPerformanceResults" runat="server" Text="Save" TabIndex="15" OnClick="btnPerformanceSubmit_Click"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                <%--<asp:Button ID="btnPerformanceResults" runat="server" Text="Save" TabIndex="15"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />--%>
                                                                                                <asp:Button ID="btnPerformanceNext" runat="server" Text="Next" TabIndex="16"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Duties_In_Excess_Of_UGC_Norms_Click" />
                                                                                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                    ValidationGroup="Personal" />
                                                                                            </p>
                                                                                        </asp:Panel>
                                                                                    </div>

                                                                                </asp:View>

                                                                                <asp:View ID="Field_Based_Activity" runat="server" OnActivate="View_Field_Based_Activity_Activate" OnDeactivate="View_Field_Based_Activity_Deactivate">
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlCurricular" runat="server">
                                                                                            <div class="panel panel-info">
                                                                                                <div class="panel panel-heading"><b>Student Related Co-Curricular, Extension And Field Based Activity</b></div>
                                                                                                <div class="panel panel-body">
                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="PnlStudCurricular" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnCurricular" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvCurricular" runat="server">
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Sr.No</th>
                                                                                                                                    <th>Name of Activity</th>
                                                                                                                                    <th>API Score Allotted</th>
                                                                                                                                    <th>API Score Claimed</th>
                                                                                                                                    <th id="lblverify" runat="server">Verified API Score</th>
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
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SN_ID") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblNActivity" runat="server" Text='<%# Eval("NAME_OF_ACTIVITY") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblAPIAlloted" runat="server" Text='<%# Eval("API_SCORE_ALLOTTED") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtAPIS" runat="server" onblur="CalculateCurricular('lvCurricular','txtAPIS','txtclaim')" MaxLength="200" onchange="ValidateStudentRealted(this)" CssClass="form-control" Text='<%# Eval("API_SCORE_CLAIMED") %>' />
                                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FiExtnder7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIS" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                             </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtVerifyAPI" runat="server" onblur="CalculateCurricular('lvCurricular','txtVerifyAPI','txtVery')" MaxLength="200" onchange="ValidateStudentRealted(this)" CssClass="form-control" Text='<%# Eval("VERIFIED_API_SCORE") %>' />
                                                                                                                         <ajaxToolKit:FilteredTextBoxExtender ID="FilTextEx16" runat="server" FilterType="Custom, Numbers" TargetControlID="txtVerifyAPI" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div28" runat="server">
                                                                                                                <label>API Score Claimed</label>
                                                                                                                <asp:TextBox ID="txtclaim" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="div29" runat="server">
                                                                                                                <label id="lblveify" runat="server">Verified Api Score</label>
                                                                                                                <asp:TextBox ID="txtVery" runat="server" CssClass="form-control" MaxLength="20" ToolTip="Sem II Total" Enabled="false"></asp:TextBox>
                                                                                                                <asp:HiddenField ID="HiddenField7" runat="server" Value="0" />
                                                                                                                <asp:HiddenField ID="HiddenField8" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>

                                                                                        <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <asp:Panel ID="Panel3" runat="server">
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel panel-heading"><b>Community Work:</b></div>
                                                                                                    <div class="panel panel-body">
                                                                                                        <div class="row">
                                                                                                            <div class="form-group col-md-6">
                                                                                                                <label>Name of Activity:</label>
                                                                                                                <asp:TextBox ID="txtActivity" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                    ToolTip="Name of Activity" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                                  </div>
                                                                                                            <div class="form-group col-md-6">
                                                                                                                <label>API Score Allotted:</label>
                                                                                                                <asp:TextBox ID="txtAPIAlloted" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                    ToolTip="API Score Allotted" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                                  
                                                                                                            </div>
                                                                                                            <div class="form-group col-md-6">
                                                                                                                <label>API Score Claimed:</label>
                                                                                                                <asp:TextBox ID="txtAPIScoreClaimed" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                    ToolTip="API Score Claimed" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAPIScoreClaimed" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                     
                                                                                                            </div>
                                                                                                            <div class="form-group col-md-6" id="divverifys" runat="server">
                                                                                                                <label>Verified API Score:</label>
                                                                                                                <asp:TextBox ID="txtVerifiedScore" runat="server" MaxLength="3000" CssClass="form-control" TabIndex="28"
                                                                                                                    ToolTip="Verified API Score" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers" TargetControlID="txtVerifiedScore" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                     
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="form-group col-md-6"></div>

                                                                                                        <%--<div class="form-group col-md-6">

                                                                                    <p class="text-center">
                                                                                        <p>
                                                                                            <asp:Label ID="Label8" runat="server" Text="" Visible="false"></asp:Label>
                                                                                        </p>
                                                                                        <asp:Button ID="btnActivity" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnActivity_Click"
                                                                                            ValidationGroup="resarchpaper" ToolTip="Click here to Add Student Co_Curricular Activity" TabIndex="31" />                                                                                        
                                                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                            ValidationGroup="resarchpaper" />                                                                                      
                                                                                      
                                                                                        <p>
                                                                                        </p>                                                                                      
                                                                                      
                                                                                        <p>
                                                                                        </p>                                                                                 
                                                                                      
                                                                                    </p>

                                                                                </div>--%>

                                                                                                        <div class="form-group col-md-12">
                                                                                                            <asp:Panel runat="server" ID="PnlActivity" ScrollBars="Auto">
                                                                                                                <asp:ListView ID="lvActivity" runat="server">
                                                                                                                    <LayoutTemplate>
                                                                                                                        <div id="lgv1">

                                                                                                                            <table class="table table-bordered table-hover">
                                                                                                                                <thead>
                                                                                                                                    <tr class="bg-light-blue">
                                                                                                                                        <th>Remove   </th>
                                                                                                                                        <th>Name of Activity
                                                                                                                                        </th>
                                                                                                                                        <th>API Score Alloted
                                                                                                                                        </th>
                                                                                                                                        <th>API Score Claimed
                                                                                                                                        </th>
                                                                                                                                        <th>Verified API Score
                                                                                                                                        </th>

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
                                                                                                                            <td>
                                                                                                                                <%--<asp:ImageButton ID="btnEditActivities" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                                                            OnClick="btnEditActivities_Click" ImageUrl="~/images/delete.png" ToolTip="Edit Record" />--%>
                                                                                                                                <asp:ImageButton ID="btnEditActivities" runat="server" CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/delete.png" ToolTip="Edit Record" />
                                                                                                                                <%--<asp:ImageButton ID="btnDelRes" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("SRNO") %>'
                                                                                                         AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeletedRes_Click"
                                                                                                          OnClientClick="showConfirmDel(this); return false;" />--%>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("Activity_Name") %>' />

                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblScoreAlloted" runat="server" Text='<%# Eval("API_Score_Allotted") %>' />
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblScoreClaimed" runat="server" Text='<%# Eval("API_Score_Claimed") %>' />
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblVerifiedScore" runat="server" Text='<%# Eval("Verified_API_Score") %>' />
                                                                                                                            </td>

                                                                                                                        </tr>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:ListView>
                                                                                                            </asp:Panel>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                        <div>
                                                                                            <br />
                                                                                        </div>
                                                                                        <%--<div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                <asp:Panel ID="Panel11" runat="server">
                                                                                    <p class="text-center">
                                                                                        <asp:Button ID="btnActivity" runat="server" Text="Save" TabIndex="15" OnClick="btnActivity_Click"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />

                                                                                                                                                                                                                                                                        
                                                                                        <asp:Button ID="Button9" runat="server" Text="Next" TabIndex="16"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here To Go Next" />
                                                                                        <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                            ValidationGroup="Personal" />
                                                                                    </p>
                                                                                </asp:Panel>
                                                                               </div>--%>

                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <asp:Panel ID="Panel17" runat="server">
                                                                                                <p class="text-center">
                                                                                                    <asp:Button ID="btnFieldBasesActivitySubmit" runat="server" Text="Save" TabIndex="15" OnClick="btnFieldBasesActivitySubmit_Click"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                    <asp:Button ID="btnFieldBasesActivityNext" runat="server" Text="Next" TabIndex="16"
                                                                                                        CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Administrative_And_Academic_Click" />
                                                                                                </p>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:View>

                                                                                <asp:View ID="View_Administrative_Academic" runat="server" OnActivate="View_Administrative_Academic_Activate" OnDeactivate="View_Administrative_Academic_Deactivate">
                                                                                    <div class="col-12 col-lg-12" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="PnlAdministrative" runat="server">
                                                                                            <div class="bg-light-blue">Administrative And Academic</div>
                                                                                            <div class="panel panel-body">
                                                                                                <asp:Panel ID="PnlAdmin" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                    <asp:HiddenField ID="hdnAdminAcademic" Value="" runat="server" />
                                                                                                    <asp:ListView ID="lvAcademic" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="lgv1">
                                                                                                                <table class="table table-bordered table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="bg-light-blue">
                                                                                                                            <th>Sr.No
                                                                                                                            </th>
                                                                                                                            <th>NAME OF ACTIVITY
                                                                                                                            </th>
                                                                                                                            <th>API Score Allotted
                                                                                                                            </th>
                                                                                                                            <th id="lblapisc" runat="server">API Score Claimed
                                                                                                                            </th>
                                                                                                                            <th id="lblapive" runat="server">Verified API Score
                                                                                                                            </th>
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
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSN" runat="server" Text='<%# Eval("SN_ID") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblNameActivity" runat="server" Text='<%# Eval("NAME_OF_ACTIVITY") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblApiAlloted" runat="server" Text='<%# Eval("API_SCORE_ALLOTTED") %>' />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtAcadDevelopApi" runat="server" onblur="CalculateDevelopment('lvAcademic','txtAcadDevelopApi','txtAcademicApi')" MaxLength="2" onChange="ValidateResearchGui(this);" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtAcadDevelopApi" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtAcadDevelopVerify" runat="server" onblur="CalculateDevelopment('lvAcademic','txtAcadDevelopVerify','txtAcademicVerifiedApi')" MaxLength="2" onchange="ValidateResearchGui1(this);" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                </asp:Panel>
                                                                                                <div class="col-md-12">
                                                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="div56" runat="server">
                                                                                                        <label>API Score Claimed</label>
                                                                                                        <asp:TextBox ID="txtAcademicApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                        <asp:HiddenField  ID="hdnscore1" runat="server"/>
                                                                                                        <asp:HiddenField ID="hdnscore2" runat="server" />
                                                                                                    </div>
                                                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="div57" runat="server">
                                                                                                        <label id="lblver1" runat="server">Verified Api Score</label>
                                                                                                        <asp:TextBox ID="txtAcademicVerifiedApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>

                                                                                                 <div class="col-md-12">
                                                                                                <div class="row">
                                                                                                    <div class="form-group col-lg-3 col-md-3 col-12" id="div67" runat="server">
                                                                                                        <label>Name Of Activity</label>
                                                                                                        <asp:TextBox ID="txtactivity1" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="form-group col-lg-3 col-md-3 col-12" id="div68" runat="server">
                                                                                                        <label>API Score Allotted</label>
                                                                                                        <asp:TextBox ID="txtapial" runat="server" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                                                                                                   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Custom, Numbers" TargetControlID="txtapial" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                          </div>
                                                                                                     <div class="form-group col-lg-3 col-md-3 col-12" id="div71" runat="server">
                                                                                                         
                                                                                                  <asp:Button ID="btnaddotheractivity" runat="server" Text="Add" CssClass="btn btn-primary"   
                                                                                                                ValidationGroup="resarchpaper" ToolTip="" TabIndex="31" OnClick="btnaddotheractivity_Click"   />

                                                                                                            </div>

                                                                                                    <div class="form-group col-md-12">
                                                                                                        <asp:Panel runat="server" ID="panelotheracivty" ScrollBars="Auto">
                                                                                                            <asp:HiddenField ID="hdnotheractivity" Value="" runat="server" />
                                                                                                            <asp:ListView ID="lvotheractivity" runat="server"> 
                                                                                                                <LayoutTemplate>
                                                                                                                    <div id="lgv1">
                                                                                                                        <table id="divotheractivity" runat="server" class="table table-bordered table-hover">
                                                                                                                            <thead>
                                                                                                                                <tr class="bg-light-blue">
                                                                                                                                    <th>Remove</th>
                                                                                                                                    <th>NAME OF ACTIVITY
                                                                                                                                    </th>
                                                                                                                                    <th>API Score Allotted
                                                                                                                                    </th>
                                                                                                                                    <th  id="lblapisc" runat="server">API Score Claimed
                                                                                                                                    </th>
                                                                                                                                    <th id="lblapive" runat="server"> Verified API Score
                                                                                                                                    </th>
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
                                                                                                                        <td>
                                                                                                                            <asp:ImageButton ID="btneditOtherActivity" runat="server" CommandArgument='<%# Eval("SN_ID") %>'
                                                                                                                                 OnClick="btneditOtherActivity_Click"   ImageUrl="~/images/delete.png" ToolTip="Edit Record" />
                                                                                                                            <asp:HiddenField ID="hdnsnid" runat="server" Value='<%# Eval("SN_ID") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblnameofactivity" runat="server" Text='<%# Eval("NameOfActivity") %>' />

                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblapiscoreallot" runat="server" Text='<%# Eval("ApiScoreAllotted") %>' />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtapiscore" runat="server" Text='<%# Eval("ApiScoreClaime") %>' onblur="Calculateotheractivity('lvotheractivity','txtapiscore','txtAcademicApi')"></asp:TextBox>
                                                                                                                            <%--<asp:Label ID="lblapiscorecl" runat="server" Text='<%# Eval("ApiScoreClaime") %>' onclientclick="CalculateAttendance('lvAttendance','lblStudPresent','txtAverage')" />
                                                                                                                      --%>  </td>
                                                                                                                       <%-- <td>
                                                                                                                            <asp:Label ID="lblLecturesEngaged" runat="server" Text='<%# Eval("SR_NO") %>' />
                                                                                                                        </td>--%>
                                                                                                                       <td>
                                                                                                                              <asp:TextBox ID="txtapiver" runat="server" Text='<%# Eval("VERIFIED_API_SCORE") %>'  onblur="Calculateotheractivity('lvotheractivity','txtapiver','txtAcademicVerifiedApi')"></asp:TextBox>
                                                                                                                         
                                                                                                                       </td>

                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:ListView>
                                                                                                        </asp:Panel>
                                                                                                    </div>
                                                                                            </div>

                                                                                        </asp:Panel>


                                                                                    </div>
                                                                                    <div>
                                                                                        <br />
                                                                                    </div>

                                                                                    <asp:Panel ID="PnlProfessionalDevelopment" runat="server">
                                                                                        <div>
                                                                                            <br />
                                                                                        </div>
                                                                                        <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                            <div class="bg-light-blue">Professional Development Activity</div>
                                                                                        </div>
                                                                                        <div class="panel panel-body">
                                                                                            <asp:Panel ID="PnlDevelopment" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; max-height: 550px;">
                                                                                                <asp:HiddenField ID="hdnDevelopment" Value="" runat="server" />
                                                                                                <asp:ListView ID="lvDevelopment" runat="server">
                                                                                                    <LayoutTemplate>
                                                                                                        <div id="lgv1">
                                                                                                            <table class="table table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr class="bg-light-blue">
                                                                                                                        <th>Sr.No</th>
                                                                                                                        <th>NAME OF ACTIVITY</th>
                                                                                                                        <th>API SCORE ALLOTTED</th>
                                                                                                                        <th>API SCORE CLAIMED</th>
                                                                                                                        <th id="lblver" runat="server">VERIFIED API SCORE</th>
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
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSNID" runat="server" Text='<%# Eval("SN_ID") %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblactivity" runat="server" Text='<%# Eval("NAME_OF_ACTIVITY") %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtApiAlloted" runat="server" MaxLength="50" Text='<%# Eval("API_SCORE_ALLOTTED") %>'></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtDevelopApiScore" runat="server" onblur="CalculateProfessional('lvDevelopment','txtDevelopApiScore','txtDevelopmentApi')" MaxLength="20" Text='<%# Eval("API_SCORE_CLAIMED") %>'></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtDevelopVerifiedApi" runat="server" onblur="CalculateProfessional('lvDevelopment','txtDevelopVerifiedApi','txtDevelopmentVerify')" MaxLength="21" Text='<%# Eval("VERIFIED_API_SCORE") %>'></asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </asp:Panel>
                                                                                            <div class="col-md-12">
                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div58" runat="server">
                                                                                                    <label>API Score Claimed</label>
                                                                                                    <asp:TextBox ID="txtDevelopmentApi" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="div59" runat="server">
                                                                                                    <label id="lblvs" runat="server">Verfied Api Score</label>
                                                                                                    <asp:TextBox ID="txtDevelopmentVerify" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <%--</div>--%>
                                                                                    </asp:Panel>
                                                                                    <div class="" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                                                        <asp:Panel ID="Panel18" runat="server">
                                                                                            <p class="text-center">
                                                                                                <%--<asp:Button ID="Button5" runat="server" Text="Save" TabIndex="15" OnClick="btnPerformanceSubmit_Click"
                                                                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />--%>
                                                                                                <asp:Button ID="btnAdminAcademic" runat="server" Text="Save" TabIndex="15" OnClick="btnAdministrativeAcademicSubmit_Click"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="Personal" />
                                                                                                <asp:Button ID="Button2" runat="server" Text="Next" TabIndex="16"
                                                                                                    CssClass="btn btn-primary" ToolTip="Click here To Go Next" OnClick="LinkButton_Published_Journal_Click" />
                                                                                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                    ValidationGroup="Personal" />
                                                                                            </p>
                                                                                        </asp:Panel>
                                                                                         <asp:Panel ID="Panel1" runat="server"  >
                                                                                            <div class="panel panel-heading"><b>CATEGORY-II:</b></div><br />
                                                                                           <div class="row">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="div33" runat="server">
                                                                                                                <label><b>TOTAL API SCORE CLAIMED</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12" id="div34" runat="server">
                                                                                                            
                                                                                                    <asp:TextBox ID="txtcatII" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>
                                                                                                 <div class="row" id="div61" runat="server" visible="false">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="div60" runat="server">
                                                                                                                <label><b>TOTAL API SCORE VERIFY</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12" >
                                                                                                            
                                                                                                    <asp:TextBox ID="TXTCATIIV" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>

                                                                                        </asp:Panel>

                                                                                        <asp:Panel ID="Panel2" runat="server">
                                                                                            <div class="panel panel-heading"><b>CATEGORY-1 + CATEGORY-2:</b></div><br />
                                                                                           <div class="row">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="div62" runat="server">
                                                                                                                <label><b>TOTAL API SCORE CLAIMED</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12" id="div63" runat="server">
                                                                                                            
                                                                                                    <asp:TextBox ID="txttot1" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>
                                                                                                 <div class="row" id="div65"  runat="server"  visible="false">
                                                                                                            <div class="form-group col-lg-3 col-md-3 col-12" id="div64" runat="server">
                                                                                                                <label><b>TOTAL API SCORE VERIFY</b></label>
                                                                                                                
                                                                                                            </div>
                                                                                               <div class="form-group col-lg-4 col-md-4 col-12">
                                                                                                            
                                                                                                    <asp:TextBox ID="txttot2" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                                                                                   </div>
                                                                                             
                                                                                               </div>

                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </asp:View>

                                                                                <asp:View ID="View_Reviewing" runat="server" OnActivate="View_Reviewing_Activate" OnDeactivate="View_Reviewing_Deactivate">
                                                                                    <asp:Panel ID="PnlReviewing" runat="server">
                                                                                        <div class="panel panel-default with-collapse">
                                                                                            <div class="panel-heading">Reviewing Officer Remark</div>
                                                                                            <div class="panel panel-body">
                                                                                                <br />
                                                                                                <div class="form-group col-md-12">
                                                                                                    <div class="row">
                                                                                                        <div class="form-group col-md-12">
                                                                                                            <label><span style="color: red;">*</span>Length of service under the Reviewing Officer:</label>
                                                                                                            <asp:TextBox ID="txtLengthOfService" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtLengthOfService"
                                                                                                                Display="None" ErrorMessage="Please Enter Length of service under the Reviewing Officer." ValidationGroup="Review" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="form-group col-md-12">
                                                                                                            <label>In case of disagreement, please specify the reason. Is there anything you wish to modify or add?</label>
                                                                                                            <asp:TextBox ID="txtReviewingReason" runat="server" CssClass="form-control" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'2000');" onkeyup="textCounter(this, this.form.remLen, 2000);"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="row">
                                                                                                        <div class="form-group col-md-12">
                                                                                                            <label>Pen Picture by Reviewing Officer. Please comment (in about 100 words) on the overall qualities of the faculty including area of strengths and lesser strength and his attitude towards weaker sections<span style="color: red;"></span></label>
                                                                                                            <asp:TextBox ID="txtPenPicture_Comment_reviewing" runat="server" CssClass="form-control" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'200');" onkeyup="textCounter(this, this.form.remLen, 200);"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="form-group col-md-8">
                                                                                                            <label>Overall numerical grading  and grade on the basis of weightage given in section A,B, and C in assessment section of the report:</label>
                                                                                                        </div>
                                                                                                        <div class="form-group col-md-2">
                                                                                                            <asp:TextBox ID="txtNumericalGrading" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>


                                                                                    <asp:Panel ID="pnlbtnReviewing" runat="server">
                                                                                        <p class="text-center">
                                                                                            <asp:Button ID="btnReviewing" runat="server" Text="Save" TabIndex="19" CssClass="btn btn-primary" ToolTip="Click here to Submit"
                                                                                                ValidationGroup="Review" OnClick="btnReviewing_Click" />
                                                                                            <asp:ValidationSummary ID="vs6" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                                                                ValidationGroup="Review" />
                                                                                        </p>
                                                                                    </asp:Panel>

                                                                                </asp:View>


                                                                            </asp:MultiView>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>



    <div id="divMsg" runat="server">
    </div>



    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />

    <asp:Button runat="server" ID="Button12" Style="display: none" />
    <script type="text/javascript">

        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

    </script>

    <script language="javascript" type="text/javascript">
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>


    <script type="text/javascript">
        function minmax(value, min, max) {
            var maxValue = max;
            var minValue = min;
            if (parseInt(value) < min) {
                alert('Plese Enter Number ,Minimum Value Should be ' + minValue);

            }
            else if (parseInt(value) > max) {
                alert('Plese Enter Number ,Maximum Value Should be ' + maxValue);


            }
            return 0;
        }
    </script>

    <%--<script language="javascript" type="text/javascript">
        function CalculateAttendance(lvname, txtname, TxtSum) {
            {
                debugger;
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnattendance.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }

                //var txts = document.getElementById("ctl00_ContentPlaceHolder1_lblavattendance").value;
                //var total;
                //total = txts * 100;
                //document.getElementById("ctl00_ContentPlaceHolder1_div36").value = total;  

            }
        }
    </script>--%>


    <script language="javascript" type="text/javascript">
        function CalculateTotalMark(lvname, txtname, TxtSum) {
            {

                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdRowCount.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

        function CalculateEngagingLecture(lvname, txtTarget, txtEngage, TxtSum) {
            debugger;
            var listview = lvname;
            var textboxname = txtTarget;
            var txtEng = txtEngage;
            var textSum = TxtSum;
             var ROWS = document.getElementById('<%=hdRowCount.ClientID%>').value;
            var i = 0;
            var score = 0;

            for (i = 0; i < ROWS; i++) {
            score1 = Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + txtTarget + "").value);
            score2 = Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + txtEngage + "").value);
            tot = score1 / score2;
            document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = tot;
            }   
        }
    </script>

    <script language="javascript" type="text/javascript">
        //function CalculateEngagingLecture(UpAll) {
        //    //var st = vall.id.split("lvInfo_");
        //   // var i = st[1].split("txtHrsEngaged_");
        //   // var index = i[1];

        //    var abc = document.getElementById('ContentPlaceHolder1_txtHrsTargeted').value;
        //    var pqr = document.getElementById('ContentPlaceHolder1_txtHrsEngaged').value;
        //    //  var xyz = document.getElementById('ContentPlaceHolder1_txtPercentTarget').value;

        //    var TotalPercent = pqr / abc;

        //    document.getElementById('ContentPlaceHolder1_txtPercentTarget_' + index).value = TotalPercent;

        //}

        //var st = vall.id.split("lvInfo_");
        //var i = st[1].split("txtDays_");
        //var index = i[1];
        //var workingdays = document.getElementById('ContentPlaceHolder1_lvInfo_txtDays_' + index).value;
        //var RetentionDays = document.getElementById('ContentPlaceHolder1_txtTotRetention').value;
        //var Retention = (RetentionDays);
        //var totaldaysEL = (workingdays);
        //// var totalEL = (totaldaysEL / 2);
        //var totalEL = Math.round((30 * totaldaysEL) / Retention);
        //document.getElementById('ContentPlaceHolder1_lvInfo_txtTotalEL_' + index).value = totalEL;


    </script>


    <script language="javascript" type="text/javascript">


        function multiplyBy() {
            debugger;

            //alert("tanu");
            var num1 = document.getElementById('ctl00_ContentPlaceHolder1_txtScore').value;
            ////alert(num1);
            //var num2 = document.getElementById('ctl00_ContentPlaceHolder1_txtWeight').value;
            //// alert(num2);
            //var num3 = num1 * num2;
            //document.getElementById('ctl00_ContentPlaceHolder1_txtScore').value = num3;

            if (num1 > 20) {

                alert("Api Score Should Not Be Greater Than 20")
                document.getElementById('ctl00_ContentPlaceHolder1_txtScore').value = "";
               // document.getElementById('ctl00_ContentPlaceHolder1_txtWeight').value = "";

                return;
            }
        }

    </script>


    <script language="javascript" type="text/javascript">


        function multiplyBy1() {
            debugger;

            //alert("tanu");
            var num1 = document.getElementById('ctl00_ContentPlaceHolder1_txtClaimed').value;
            ////alert(num1);
            //var num2 = document.getElementById('ctl00_ContentPlaceHolder1_txtMaxWeight').value;
            //// alert(num2);
            //var num3 = num1 * num2;
            //document.getElementById('ctl00_ContentPlaceHolder1_txtClaimed').value = num3;

            if (num1 > 20) {

                alert("Api Score Should Not Be Greater Than 20")
                document.getElementById('ctl00_ContentPlaceHolder1_txtClaimed').value = "";
               // document.getElementById('ctl00_ContentPlaceHolder1_txtClaimed').value = "";

                return;
            }
        }

    </script>



    <script language="javascript" type="text/javascript">
        function CalculateInnovative(lvname, txtname, TxtSum) {
            {

                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnApiCount.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }


            }

           
        }
    </script>
    <script language="javascript" type="text/javascript">
        function CalculateFeedback(lvname, txtname, TxtSum) {
            {

                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnFeedback.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }

            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function CalculateExamination(lvname, txtname, TxtSum) {
            {

                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnExamination.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }

            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function CalculateCurricular(lvname, txtname, TxtSum) {
            {
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnCurricular.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        debugger;
        function CalculateMaterial(lvname, txtname, TxtSum) {
            {
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnMaterial.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                    
                }
               
            }

        }
    </script>
    <script language="javascript" type="text/javascript">
        function CalculatePublication(lvname, txtname, TxtSum) {
            {
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnPublication.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }
            }
        }
    </script>
    <%--<script language="javascript" type="text/javascript">
        function CalculatePublishBook(lvname, txtname, TxtSum) {
            {
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnMaterial.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }
            }
        }
    </script>--%>
    <script language="javascript" type="text/javascript">
        function CalculateConference(lvname, txtname, TxtSum) {
            {
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnConference.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }
            }
        }
    </script>
   <script language="javascript" type="text/javascript">

       function CalculateExcess(lvname, txtname, TxtSum) {
           {
               var listview = lvname;
               var textboxname = txtname;
               var textSum = TxtSum;
               var ROWS = document.getElementById('<%=hdnExcess.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }

                  var A = document.getElementById("ctl00_ContentPlaceHolder1_txtapi1").value;
                 var C = (A / 30) * 2;
                document.getElementById("ctl00_ContentPlaceHolder1_txtapi1").value = parseInt(C);


            }
        }
    </script>

    <script language="javascript" type="text/javascript">



        function CalculatePatent(lvname, txtname, TxtSum) {
            {
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnPatent.ClientID%>').value;
                var i = 0;
                var score = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }
            }
        }




    </script>
    <script language="javascript" type="text/javascript">

        debugger;
        function CalculateDevelopment(lvname, txtname, TxtSum) {

 
            var listview = lvname;
            var textboxname = txtname;
            var textSum = TxtSum;
            var ROWS = document.getElementById('<%=hdnAdminAcademic.ClientID%>').value;
            var i = 0;
            var score = 0;
            

            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                // document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                document.getElementById('ctl00_ContentPlaceHolder1_hdnscore1').value = score;
                
              
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_hdnscore1').value == "") {
                var score1 = 0;

            }
            else {
                var score1 = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_hdnscore1').value);
            }

            if (document.getElementById('ctl00_ContentPlaceHolder1_hdnscore2').value == "") {
                var score2 = 0;
            }
            else {
                var score2 = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_hdnscore2').value);
            }


               document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score1 + score2;


        }
    </script>

    <script language="javascript" type="text/javascript">

        debugger;
        function Calculateotheractivity(lvname, txtname, TxtSum) {


            var listview = lvname;
            var textboxname = txtname;
            var textSum = TxtSum;
            var ROWS = document.getElementById('<%=hdnotheractivity.ClientID%>').value;
            var i = 0;
            var score = 0;


            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                // document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                document.getElementById('ctl00_ContentPlaceHolder1_hdnscore2').value = score;
                
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_hdnscore1').value == "") {
                var score1 = 0;

            }
            else {
                var score1 = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_hdnscore1').value);
            }

            if (document.getElementById('ctl00_ContentPlaceHolder1_hdnscore2').value == "") {
                var score2 = 0;
            }
            else {
                var score2 = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_hdnscore2').value);
            }
            
            // document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = ok;
            document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score1 + score2;

        }
    </script>






    <script language="javascript" type="text/javascript">
        function CalculateProfessional(lvname, txtname, TxtSum) {
            {

                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnDevelopment.ClientID%>').value;
                var i = 0;
                var score = 0;
                var score1 = 0;
                var score2 = 0;

                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                    score1 = score;
                }
            }
        }


        function CalculateGuidance(lvname, txtname, TxtSum) {
            {
                debugger;
                var listview = lvname;
                var textboxname = txtname;
                var textSum = TxtSum;
                var ROWS = document.getElementById('<%=hdnGuidance.ClientID%>').value;
                var i = 0;
                var score = 0;


                for (i = 0; i < ROWS; i++) {
                    score += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxname + "").value);
                    document.getElementById('ctl00_ContentPlaceHolder1_' + textSum + '').value = score;
                }
            }
        }





        function ValidateResearchGui(txt) {

            var apiScoreUser = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl1_txtAcadDevelopApi').value;
            var apiScoreUser1 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl0_txtAcadDevelopApi').value;

            var apiScoreUser2 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl2_txtAcadDevelopApi').value;
            var apiScoreUser3 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl3_txtAcadDevelopApi').value;

            var apiScoreUser4 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl4_txtAcadDevelopApi').value;
            var apiScoreUser5 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl5_txtAcadDevelopApi').value;

            var apiScoreUser6 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl6_txtAcadDevelopApi').value;
            var apiScoreUser7 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl7_txtAcadDevelopApi').value;

            var apiScoreUser8 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl8_txtAcadDevelopApi').value;
            var apiScoreUser9 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl9_txtAcadDevelopApi').value;

            if (apiScoreUser > 10) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 10');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser1 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser2 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser3 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser4 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser5 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser6 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser7 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser8 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUser9 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;

            }
        }




        debugger;

        function ValidateStudentRealted(txt) {

            var apiscoreuser = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl0_txtAPIS').value;
            var apiscore1 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl1_txtAPIS').value;
            var apiscore2 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl2_txtAPIS').value;
            var apiscore3 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl3_txtAPIS').value;
            var apiscore4 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl4_txtAPIS').value;
            var apiscore5 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl5_txtAPIS').value;
            var apiscore6 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl6_txtAPIS').value;
            var apiscore7 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl7_txtAPIS').value;
            var apiscore8 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl8_txtAPIS').value;

            var apiscore9 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl9_txtAPIS').value;
            var apiscore10 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl10_txtAPIS').value;
            var apiscore11 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl11_txtAPIS').value;
            var apiscore12 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl12_txtAPIS').value;
            var apiscore13 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl13_txtAPIS').value;
            var apiscore14 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl14_txtAPIS').value;
            var apiscore15 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl15_txtAPIS').value;
            var apiscore16 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl16_txtAPIS').value;

            var apiscore17 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl17_txtAPIS').value;
            var apiscore18 = document.getElementById('ctl00_ContentPlaceHolder1_lvCurricular_ctrl18_txtAPIS').value;







            if (apiscoreuser > 10) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 10');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore1 > 8) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 8');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore2 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }

            if (apiscore3 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }

            if (apiscore4 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }


            if (apiscore5 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }

            if (apiscore6 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }

            if (apiscore7 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore8 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore9 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore10 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore11 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore12 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore13 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore14 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore15 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore16 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;

            }
            if (apiscore17 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            if (apiscore18 > 5) {
                alert('Api Score Claimed  Of Student Related Co-Curricular Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }

        }



















        function ValidateResearchGui1(txt) {


            var apiScoreUserr = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl1_txtAcadDevelopVerify').value;
            var apiScoreUserr1 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl0_txtAcadDevelopVerify').value;

            var apiScoreUserr2 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl2_txtAcadDevelopVerify').value;
            var apiScoreUserr3 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl3_txtAcadDevelopApi').value;

            var apiScoreUserr4 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl4_txtAcadDevelopApi').value;
            var apiScoreUserr5 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl5_txtAcadDevelopVerify').value;

            var apiScoreUserr6 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl6_txtAcadDevelopVerify').value;
            var apiScoreUserr7 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl7_txtAcadDevelopVerify').value;

            var apiScoreUserr8 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl8_txtAcadDevelopVerify').value;
            var apiScoreUserr9 = document.getElementById('ctl00_ContentPlaceHolder1_lvAcademic_ctrl9_txtAcadDevelopVerify').value;

            if (apiScoreUserr > 10) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 10');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr1 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr2 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr3 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr4 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr5 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr6 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr7 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr8 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;
            }
            else if (apiScoreUserr9 > 5) {
                alert('Api Score Claimed  Of Administrative And Academic Should Be Less Than 5');
                txt.value = 0;
                txt.focus();
                return;

            }
        }





        debugger;
        function CalPercentage(txt) {


            var txthrsT1 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl0_txtHrsTargeted').value;
            var txteng1 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl0_txtHrsEngaged').value;
            var tcttarget = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl0_txtPercentTarget').value;

            var txthrsT2 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl1_txtHrsTargeted').value;
            var txteng2 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl1_txtHrsEngaged').value;
            var tcttarget2 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl1_txtPercentTarget').value;

            var txthrsT3 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl2_txtHrsTargeted').value;
            var txteng3 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl2_txtHrsEngaged').value;
            var tcttarget3 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl2_txtPercentTarget').value;

            var txthrsT4 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl3_txtHrsTargeted').value;
            var txteng4 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl3_txtHrsEngaged').value;
            var tcttarget4 = document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl3_txtPercentTarget').value;


            if (txteng1 != "") {
                tcttarget = txteng1/txthrsT1  * 100;
                document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl0_txtPercentTarget').value = tcttarget;
            }
            
            if (txteng2 != "") {
                tcttarget2 = txteng2 / txthrsT2 * 100;
                document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl1_txtPercentTarget').value = tcttarget2;

           }

            if (txteng3 != "") {
                tcttarget3 = txteng3 / txthrsT3 * 100;
                document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl2_txtPercentTarget').value = tcttarget3;
                
               
              

            }
            if (txteng4 != "") {
                tcttarget4 = txteng4 / txthrsT4 * 100;
                document.getElementById('ctl00_ContentPlaceHolder1_lvEngagingLectures_ctrl3_txtPercentTarget').value = tcttarget4;

               
            }
           
           

        }

      

    </script>



    <script>
       
        function CalculateEngLactureAvg(lvname, txtbocOne, txtboxtwo, TxtSum) {
            {
                debugger;
                var listview = lvname;
                var textboxone = txtbocOne;
                var textboxtwo = txtboxtwo;
                var textSum = TxtSum;
                // var ROWS = document.getElementById('ctl00_ContentPlaceHolder1_hdnEngagingLecture').value;
                var ROWS = document.getElementById('<%=hdnEngagingLecture.ClientID%>').value;
                var i = 0;
                var finalAvg = 0;
                var count = 0;

                for (i = 0; i < ROWS; i++) {
                    var score = 0;
                    var avg = 0;
                    var textone=(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxone + "").value);
                    var texttwo=(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textboxtwo + "").value);

                    score = (texttwo) / (textone);
                  
                    avg  = score * 100; 
                    (document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textSum + "").value) = avg.toFixed(4); 
                    
                }


                for (i = 0; i < ROWS; i++) {
                  
                    if (!isNaN(Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textSum + "").value)))
                    {
                        finalAvg += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textSum + "").value);
                        count++;
                    }
                }
                var calculateavg = finalAvg / count;
                document.getElementById('ctl00_ContentPlaceHolder1_txtavg1').value = calculateavg.toFixed(4);

               
                if 
                    (document.getElementById('ctl00_ContentPlaceHolder1_txtavg1').value >= 80) {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtlbl').value = "Good";
                    document.getElementById('ctl00_ContentPlaceHolder1_txtapic').value = document.getElementById('ctl00_ContentPlaceHolder1_txtmaxw').value * 0.7;
                }
                
                if 
                   (document.getElementById('ctl00_ContentPlaceHolder1_txtavg1').value <= 80) {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtlbl').value = "Unsatisfactory";
                    document.getElementById('ctl00_ContentPlaceHolder1_txtapic').value = document.getElementById('ctl00_ContentPlaceHolder1_txtmaxw').value * 0;
                }
                if 
                   (document.getElementById('ctl00_ContentPlaceHolder1_txtavg1').value >= 91) {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtlbl').value = "Excellent";
                    document.getElementById('ctl00_ContentPlaceHolder1_txtapic').value = document.getElementById('ctl00_ContentPlaceHolder1_txtmaxw').value * 1;
                }
            }
        }


    </script>


    <script>

        function CalMalFACT(txt) {

            var CAL = document.getElementById('ctl00_ContentPlaceHolder1_txtAverage').value;


            if
                    (CAL >= 80) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtFactor').value = "Good";
            }

            if
                   (CAL  <= 80) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtFactor').value = "Unsatisfactory";
            }
            if
                   (CAL  >= 91) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtFactor').value = "Excellent";
            }

        }
    </script>

    <script>
        function CalAvgStu(lvname,TxtSum) {
            {
                var textSum = TxtSum;
                for (i = 0; i < ROWS; i++) {

                    if (!isNaN(Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textSum + "").value))) {
                        finalAvg += Number(document.getElementById("ctl00_ContentPlaceHolder1_" + listview + "_ctrl" + i + "_" + textSum + "").value);
                        count++;
                    }
                }
            }
        }
        var calculateavg = finalAvg / count;
        document.getElementById('ctl00_ContentPlaceHolder1_txtavg1').value = calculateavg;
    </script>


    <script>
        function CategoryItotal(txt) {
            var sum1 = document.getElementById('ctl00_ContentPlaceHolder1_txtapic').value;
            var sum2 = document.getElementById('ctl00_ContentPlaceHolder1_txtScore').value;
            var sum3 = document.getElementById('ctl00_ContentPlaceHolder1_txtClaimed').value;
            var sum4 = document.getElementById('ctl00_ContentPlaceHolder1_txtApi').value;
            var sum5 = document.getElementById('ctl00_ContentPlaceHolder1_txtAi').value;

            var categorytot = sum1 + sum2 + sum3 + sum4 + sum5;
            document.getElementById('ctl00_ContentPlaceHolder1_txtcategorytot').value = categorytot;

        }
    </script>

    <script>

        function CalAllCategory(txt) {
          
            var catsum1 = document.getElementById('ctl00_ContentPlaceHolder1_txttot2').value;
           
            var catsum2 = document.getElementById('ctl00_ContentPlaceHolder1_txtpver').value;
            
            var tot = catsum1 + catsum2;
           
            document.getElementById('ctl00_ContentPlaceHolder1_txtNumericalGrading').value = tot;


        }
    </script>



    <ajaxToolKit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
        BackgroundCssClass="modalBackground" RepositionMode="RepositionOnWindowScroll"
        TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="div" CancelControlID="btnNoDel">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Button runat="server" ID="Button3" Style="display: none" />

    <asp:Panel ID="div" runat="server" Style="display: block" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    <td>&nbsp;&nbsp;Are you sure to final submit APAR Proforma..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" OnClick="btnOkDel_Click" TabIndex="180" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" TabIndex="181" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        BackgroundCssClass="modalBackground" RepositionMode="RepositionOnWindowScroll"
        TargetControlID="Button12" PopupControlID="Panel16" CancelControlID="btnokno">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Button runat="server" ID="Button4" Style="display: none" />
    <asp:Panel ID="Panel16" runat="server" Style="display: block" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/warning.gif" />
                    <td>&nbsp;&nbsp;Kindly click on save button on every tab of APAR for proper data reflection in report</td>
                    <div class="text-center">
                        <asp:Button ID="btnokyes" runat="server" Text="Yes" CssClass="btn-primary" TabIndex="180" />
                        <asp:Button ID="btnokno" runat="server" Text="No" CssClass="btn-primary" TabIndex="181" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>




