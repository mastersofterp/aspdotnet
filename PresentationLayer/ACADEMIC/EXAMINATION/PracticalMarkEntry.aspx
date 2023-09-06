<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PracticalMarkEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_PracticalMarkEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="../../FreezeHeaderScripts/jquery-1.4.1.min.js"></script>
    <script src="../../FreezeHeaderScripts/ScrollableGridViewPlugin_ASP.NetAJAXmin.js"></script>--%>
   <%-- <script type="text/javascript">
        function FreezHeader() {
            $('#<%=gvStudent.ClientID %>').Scrollable({
                ScrollHeight: 300,
                IsInUpdatePanel: true
            });
        }
    </script>--%>
    <script type="text/javascript">
        $(document).bind("contextmenu", function (e) { 
            e.preventDefault();
        });
        $(document).keydown(function (e) {
            if (e.which === 123) {
                return false;
            }
            else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
                return false;
            }
        });
    </script>
    <script language="javascript" type="text/javascript">
        function getScrollBottom(p_oElem) {
            return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarkEntry"
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
    <style>
        .GridHeader
        {
            text-align: center !important;
        }
        .GVFixedHeader
        {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }

        .GVFixedFooter
        {
            font-weight: bold;
            background-color: Green;
            position: relative;
            bottom: expression(getScrollBottom(this.parentNode.parentNode.parentNode.parentNode));
        }
    </style>
    <style style="text/css">
        .bounce
        {
            height: 30px;
            overflow: hidden;
            position: relative;
            background: white;
            color: red;
            padding: 5px;
        }

            .bounce p
            {
                position: absolute;
                width: 100%;
                height: 50%;
                margin: 0;
                line-height: 30px;
                text-align: center;
                /* Starting position */
                -moz-transform: translateX(50%);
                -webkit-transform: translateX(50%);
                transform: translateX(50%);
                /* Apply animation to this element */
                -moz-animation: bouncing-text 10s linear infinite alternate;
                -webkit-animation: bouncing-text 10s linear infinite alternate;
                animation: bouncing-text 10s linear infinite alternate;
            }
        /* Move it (define the animation) */
        @-moz-keyframes bouncing-text
        {
            0%
            {
                -moz-transform: translateX(50%);
            }

            100%
            {
                -moz-transform: translateX(-50%);
            }
        }

        @-webkit-keyframes bouncing-text
        {
            0%
            {
                -webkit-transform: translateX(50%);
            }

            100%
            {
                -webkit-transform: translateX(-50%);
            }
        }

        @keyframes bouncing-text
        {
            0%
            {
                -moz-transform: translateX(50%); /* Browser bug fix */
                -webkit-transform: translateX(50%); /* Browser bug fix */
                transform: translateX(50%);
            }

            100%
            {
                -moz-transform: translateX(-50%); /* Browser bug fix */
                -webkit-transform: translateX(-50%); /* Browser bug fix */
                transform: translateX(-50%);
            }
        }

        .bounce p:hover
        {
            -moz-animation-play-state: paused;
            -webkit-animation-play-state: paused;
            animation-play-state: paused;
        }
    </style>
    <style>
        .Srln1
        {
            width: 10%;
            position: absolute;
            right: 88.5%;
            padding-top: 4%;
            align-content: center;
            height: 27%;
            background: #6a8296;
        }


        .EnrollNo1
        {
            width: 27.8%;
            position: absolute;
            right: 65.4%;
            padding-top: 4%;
            align-content: center;
            height: 27%;
            background: #6a8296;
        }

        .StudentName1
        {
            width: 37.5%;
            position: absolute;
            right: 28%;
            height: 27%;
            align-content: center;
            background: #6a8296;
            padding-top: 4%;
        }

        .Srln
        {
            width: 108px;
            position: absolute;
            right: 88.5%;
            padding-top: 4%;
            align-content: center;
            height: 27%;
            background: #6a8296;
        }

        .EnrollNo
        {
            width: 208px;
            position: absolute;
            right: 72.7%;
            padding-top: 4%;
            align-content: center;
            height: 27%;
            background: #6a8296;
        }

        .StudentName
        {
            width: 286px;
            position: absolute;
            right: 45.9%;
            height: 27%;
            align-content: center;
            background: #6a8296;
            padding-top: 4%;
        }

        .Head
        {
            width: 122px;
            position: absolute;
            right: 35.4%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head1
        {
            width: 122px;
            position: absolute;
            right: 24.3%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head2
        {
            width: 121px;
            position: absolute;
            right: 13.5%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head3
        {
            width: 117px;
            position: absolute;
            right: 2.8%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head4
        {
            width: 9.5%;
            position: absolute;
            right: 2.8%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head5
        {
            width: 13%;
            position: absolute;
            right: 15.9%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head6
        {
            width: 13%;
            position: absolute;
            right: 3%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .EntryStatusCourseName
        {
            width: 230px;
            position: absolute;
            right: 3%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .EnrollNo_8
        {
            width: 327px;
            position: absolute;
            right: 58.1%;
            padding-top: 4%;
            align-content: center;
            height: 27%;
            background: #6a8296;
        }

        .EnrollNo1_8
        {
            width: 28.8%;
            position: absolute;
            right: 64.4%;
            padding-top: 4%;
            align-content: center;
            height: 27%;
            background: #6a8296;
        }

        .StudentName_8
        {
            width: 438px;
            position: absolute;
            right: 18.6%;
            height: 27%;
            align-content: center;
            background: #6a8296;
            padding-top: 4%;
        }

        .StudentName_8_8
        {
            width: 438px;
            position: absolute;
            right: 17.6%;
            height: 27%;
            align-content: center;
            background: #6a8296;
            padding-top: 4%;
        }

        .StudentName1_8
        {
            width: 37.5%;
            position: absolute;
            right: 45.6%;
            height: 27%;
            align-content: center;
            background: #6a8296;
            padding-top: 4%;
        }

        .Head_8
        {
            width: 183px;
            position: absolute;
            right: 2.4%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head_8_8
        {
            width: 183px;
            position: absolute;
            right: 1.4%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head1_8
        {
            width: 122px;
            position: absolute;
            right: 23.5%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head2_8
        {
            width: 121PX;
            position: absolute;
            right: 12.5%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head3_8
        {
            width: 119px;
            position: absolute;
            right: 16px;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head4_8
        {
            width: 9.5%;
            position: absolute;
            right: 1.3%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head5_8
        {
            width: 13.2%;
            position: absolute;
            right: 14.4%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }

        .Head6_8
        {
            width: 13%;
            position: absolute;
            right: 1.5%;
            height: 27%;
            align-content: center;
            background: #6a8296;
        }
    </style>
    <style>
        .GVEntryStatusCourseName
        {
            width: 319px;
            position: absolute;
            right: 69.3%;
            height: 61px;
            background: #6a8296;
        }

        .GVEntryStatusExamName
        {
            height: 61px;
            width: 200px;
            position: absolute;
            right: 52.9%;
            background: #6a8296;
        }

        .GVEntryStatusMarksEntryStatus
        {
            height: 61px;
            position: absolute;
            right: 37.5%;
            background: #6a8296;
            width: 185px;
        }

        .GVEntryStatusExamMarksEntryReport
        {
            position: absolute;
            right: 19.7%;
            height: 61px;
            background: #6a8296;
            width: 201px;
        }

        .GVEntryStatusCourseMarkReport
        {
            position: absolute;
            width: 190px;
            right: 2.7%;
            background: #6a8296;
            height: 61px;
        }
    </style>

  
    <asp:UpdatePanel runat="server" ID="updMarkEntry">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">IA Exam Mark's Entry</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div id="divstatus" runat="server" visible="false">
                                    <span style="color: red">
                                        <marquee onmouseover="this.stop();" onmouseout="this.start();"><b><asp:Label ID="lblstatusmark" runat="server"></asp:Label> , so kindly enter the marks and lock for your respective subjects !!!!!</b></marquee>
                                    </span>
                                </div>
                            </div>

                            <asp:Panel ID="pnlSelection" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged" runat="server" AppendDataBoundItems="true" Font-Bold="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Course Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="selcourse">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div id="TRCourses" runat="server">
                                    <div class="col-12">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvCourse" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Course Name
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStatus" Visible="false" runat="server" Style="color: red; margin-right: 912px;"></asp:Label>
                                    </div>

                                    <div class="col-12">
                                        <div style="overflow : auto; width:100%;">
                                            <asp:GridView ID="GVEntryStatus" runat="server" AutoGenerateColumns="false" OnPreRender="GVEntryStatus_PreRender" OnRowDataBound="GVEntryStatus_RowDataBound"
                                               CssClass="table table-bordered table-hover">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="COURSE NAME"><%--HeaderStyle-Width="350"--%>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                                                CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO") %>'
                                                                OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>' />
                                                            <asp:HiddenField ID="hdnfld_courseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                            <asp:HiddenField ID="hdnsem" runat="server" Value='<%# Eval("semesterno")%>' />
                                                            <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                            <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("CCODE")%>' />
                                                            <asp:HiddenField ID="hffldname" runat="server" Value='<%# Eval("FLDNAME")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="GVEntryStatusCourseName" />
                                                        <%--CssClass="EntryStatusCourseName" --%>
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="EXAMNAME" HeaderText="EXAM NAME"  HeaderStyle-CssClass="GVEntryStatusExamName" />
                                                    <%--ItemStyle-Width="100" HeaderStyle-Height="50%"--%>

                                                    <asp:BoundField DataField="MARK_ENTRY_STATUS" HeaderText="MARK ENTRY STATUS" HeaderStyle-CssClass="GVEntryStatusMarksEntryStatus" />
                                                    <%--ItemStyle-Width="200" HeaderStyle-Height="40%" ItemStyle-BorderStyle="None" --%>

                                                    <asp:TemplateField HeaderText="EXAMWISE MARKS REPORT" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnreportexamwise" runat="server" CausesValidation="false" CommandName='<%# Eval("SECTIONNO")%>' CssClass="btn btn-primary"
                                                                OnClick="btnreportexamwise_Click" Text="Print" CommandArgument='<%# Eval("EXAMNAME") %>' ToolTip='<%# Eval("CCODE")%>' />
                                                            <asp:HiddenField ID="hdnsemester" runat="server" Value='<%# Eval("semesterno")%>' />
                                                            <asp:HiddenField ID="hdncorseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                            <asp:HiddenField ID="hdnExamField" runat="server" Value='<%# Eval("EXAMNAME1")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="GVEntryStatusExamMarksEntryReport" />
                                                        <ItemStyle  />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="COURSEWISE MARKS REPORT" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnCourseWISE" runat="server" Text="Print" CssClass="btn btn-primary" CommandArgument='<%# Eval("SECTIONNO")%>'
                                                                OnClick="btnCourseWISE_Click" ToolTip='<%# Eval("COURSENAME") %>' CommandName='<%# Eval("COURSENO")%>' />

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="GVEntryStatusCourseMarkReport" />
                                                        <ItemStyle  />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle  />
                                                <HeaderStyle />
                                                <PagerStyle />
                                                <RowStyle />
                                                <SelectedRowStyle  Font-Bold="True"  />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                </fieldset>
                            </asp:Panel>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlMarkEntry" runat="server">
                                    <fieldset>
                                        <legend>Selection Criteria</legend>
                                        <div class="col-md-8">
                                            <div class="col-md-12">
                                                <div class="form-group col-md-6">
                                                    <label>Session :</label>
                                                    <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" Font-Bold="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-md-6">
                                                    <label>Course Name :</label><br />
                                                    <asp:Label ID="lblCourse" runat="server" Font-Bold="true" />
                                                    <asp:HiddenField ID="hdfSection" runat="server" />
                                                    <asp:HiddenField ID="hdfBatch" runat="server" />
                                                    <asp:HiddenField ID="hdfSemNo" runat="server" />
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label><span style="color: red;">*</span>Exam :</label>
                                                    <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True"
                                                        ValidationGroup="show" OnSelectedIndexChanged="ddlExam_OnSelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                        Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row text-center">
                                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Font-Bold="True" OnClick="btnShow_Click"
                                                    Text="Show Student" ValidationGroup="show" />
                                                <asp:Button ID="btnBack" runat="server" Font-Bold="true" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-primary" />

                                                <asp:Button ID="btnSave" runat="server" Enabled="false" Font-Bold="true"
                                                    OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" ValidationGroup="val" />

                                                <asp:Button ID="btnConsolidateMark" style="display:none" runat="server" Enabled="false" CssClass="btn btn-primary"  
                                                    Text="Consolidate Marks" Font-Bold="true" ValidationGroup="val" OnClientClick="return ConfirmConsolidateMark();" OnClick="btnConsolidateMark_Click"/>
                                                <asp:Button ID="btnCancel2" runat="server" Font-Bold="true" OnClick="btnCancel2_Click"
                                                    Text="Cancel" CssClass="btn btn-warning" Visible="False" />

                                                <asp:Button ID="btnMIDReport" runat="server" Font-Bold="true" Text="MID Report" CssClass="btn btn-info"
                                                    OnClick="btnTAReport_Click" Visible="False" Height="26px" />

                                                <asp:Button ID="btnConsolidateReport" runat="server" Font-Bold="true" Visible="False"
                                                    Text="Consolidate Report" CssClass="btn btn-info" OnClick="btnConsolidateReport_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div>
                                                <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <fieldset class="fieldset" style="padding: 5px; color: Green">
                                                <legend class="legend">Note</legend>
                                                <span style="font-weight: bold; color: Red;">Please Check Lock checkbox and click on Save button for Final Mark entry lock.<%--Please Save and Lock for Final Submission of Marks--%></span><br /><b>Enter :<br />
                                                    "-1" for Absent Student<br />
                                                    "-2" for UFM(Copy Case)<br />
                                                    "-3" for WithDraw Student<br />
                                                    "-4" for Drop Student<br />
                                                </b>
                                            </fieldset>
                                        </div>
                                        <div class="box-footer col-md-12">
                                            <p class="text-center">
                                            </p>

                                            <div class="col-md-12" id="tdStudent" runat="server">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <%--ScrollBars="Auto"--%>
                                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                                        <asp:UpdatePanel ID="up" runat="server">
                                                            <ContentTemplate>
                                                                <div style="overflow: auto; height: 400px; width: 100%;">
                                                                    <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        OnRowDataBound="gvStudent_RowDataBound">
                                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SR.No" ItemStyle-HorizontalAlign="Center"
                                                                                ItemStyle-Width="5%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1%>
                                                                                </ItemTemplate>
                                                                              <%--  <ItemStyle HorizontalAlign="Center" Width="4%" />--%>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <%--CssClass="Srln"--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Registration No./Roll No." ItemStyle-HorizontalAlign="Center"
                                                                                ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO1") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                                        Font-Size="9pt" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <%-- CssClass="EnrollNo"--%>
                                                                                <%--<ItemStyle HorizontalAlign="Center" Width="20%" />--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Student Name" ItemStyle-HorizontalAlign="Center"
                                                                                ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStudent" runat="server" Text='<%# Bind("STUDNAME") %>'
                                                                                        Font-Size="9pt" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <%--CssClass="StudentName"--%>
                                                                                <%--<ItemStyle HorizontalAlign="Center" Width="20%" />--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="S1T1MARK" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHS1T1Marks" runat="server"></asp:Label><br />
                                                                                    <asp:CheckBox ID="chkS1T1MarksLock" runat="server" Text="Lock" OnCheckedChanged="chkS1T1MarksLock_OnCheckedChanged" AutoPostBack="true" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtS1T1Marks" runat="server" Text='<%# Bind("MARK") %>' Width="90%" Font-Bold="true" Style="text-align: center" TabIndex="4" ToolTip='<%# Bind("MARK") %>' />
                                                                                    <asp:Label ID="lblMaxS1T1Marks" runat="server" Text='<%# Bind("MAXMARK") %>' ToolTip='<%# Bind("LOCK") %>' Visible="false" />
                                                                                    <asp:Label ID="lblMinS1T1Marks" runat="server" Text='<%# Bind("MINMARK") %>' Visible="false" />
                                                                                    <asp:HiddenField ID="hdnS1T1Marks" runat="server" Value='<%# Bind("MARK") %>' />
                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtS1Marks" runat="server" FilterType="Custom" ValidChars="0123456789.-" TargetControlID="txtS1T1Marks">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="Head" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="8.5%" />
                                                                            </asp:TemplateField>


                                                                        </Columns>

                                                                    </asp:GridView>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </asp:Panel>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </fieldset>

                                </asp:Panel>

                            </div>
                       </div>
                        </div>
                    </div>
                </div>
                    </ContentTemplate>
                 <%--   <Triggers>
                        <asp:PostBackTrigger ControlID="btnShow" />
                    </Triggers>--%>
                </asp:UpdatePanel>
      

    <div id="divMsg" runat="server">
    </div>
    <asp:HiddenField ID="hdfDegree" runat="server" Value="" />

    <script language="javascript" type="text/javascript">
        function validateMark(metxt, maxmrk, minmark, col, lock, hdnmark) {
            //MID SEM
            if ((col == 1 || col == 2 || col == 3 || col == 4 || col == 5 || col == 6 || col == 7 || col == 8 || col == 9 || col == 10) && metxt != null && metxt.value != '') {

                if (metxt.value != '' & (Number(metxt.value) > maxmrk || Number(metxt.value) < minmark) || Number(metxt.value) == -1 || Number(metxt.value) == -2 || Number(metxt.value) == -3 || Number(metxt.value) == -4) {
                    if (Number(metxt.value) == -1 || Number(metxt.value) == -2 || Number(metxt.value) == -3 || Number(metxt.value) == -4) {
                        //Nothing 
                    }
                    else {
                        metxt.focus();
                        alert("Please Enter Marks in the Range of " + minmark.toString() + " to " + maxmrk.toString() + ' & Note : -1 for Absent, -2 for UFM(Copy Case), -3 for WithDraw Student & -4 for Drop Student');
                        metxt.value = '';
                        //metxt.select();
                    }
                }
            }
            if (Number(lock) == 1) {
                if (Number(hdnmark) == Number(metxt.value)) {
                    metxt.disabled = true;
                }
                else {
                    metxt.value = hdnmark;
                    metxt.disabled = true;
                }
            }
        }

        function ConfirmConsolidateMark() {
            if (confirm('Are you sure,You want to Calculate Best of Two Average Marks ?'))
                return true;
            else
                return false;

        }
    </script>

</asp:Content>

