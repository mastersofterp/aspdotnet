<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarksEntryNotDone.aspx.cs" Inherits="Academic_REPORTS_MarksEntryNotDone" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });

    </script>
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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MARKS ENTRY / LOCK ENTRY  NOT DONE</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSessionID" runat="server" AppendDataBoundItems="True"
                                            CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSessionID_SelectedIndexChanged"
                                             TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldV" runat="server" ControlToValidate="ddlSessionID"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldV4" runat="server" ControlToValidate="ddlSessionID"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="MarkEntryStatus"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldV5" runat="server" ControlToValidate="ddlSessionID"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Status"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldVal5" runat="server" ControlToValidate="ddlSessionID"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Absent"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College</label>
                                        </div>
                                        <%--<asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True"
                                            CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>

                                        <asp:ListBox ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="1" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="rfStaff" runat="server" ControlToValidate="ddlSession" Display="None" SetFocusOnError="True"
                                            ErrorMessage="Please Select College/Session" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession" Display="None" SetFocusOnError="True"
                                            ErrorMessage="Please Select College/Session" ValidationGroup="MarkEntryStatus"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession" Display="None" SetFocusOnError="True"
                                            ErrorMessage="Please Select College/Session" ValidationGroup="Absent"></asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select College/Session"
                                            SetFocusOnError="True" ValidationGroup="Summary"></asp:RequiredFieldValidator>--%>

                                        <%--<asp:ListBox ID="ddlCollegeSession" runat="server" AppendDataBoundItems="true" ValidationGroup="configure" TabIndex="1"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true"></asp:ListBox>--%>

                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="MarkEntryStatus"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Status"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>


                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="MarkEntryStatus"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Status"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    <%--</div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" TabIndex="3"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="2"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Summary"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchool"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                            TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Summary"
                                            InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvMarkEntryStatus" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="MarkEntryStatus"
                                            InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Absent"
                                            InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFir5" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Absent"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Course Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubType" runat="server" AppendDataBoundItems="True" TabIndex="1" AutoPostBack="true"
                                            ValidationGroup="show" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>--%>
                                            <%--Added By Nikhil V.Lambe on 24/02/2021 for Sessional Sub Type in MAKAUT--%>
                                            <%--    <asp:ListItem Value="3">Sessional</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubType" runat="server" ControlToValidate="ddlSubType"
                                            Display="None" ErrorMessage="Please Select Course Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSubType"
                                            Display="None" ErrorMessage="Please Select Course Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Absent"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Pattern</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPattern" runat="server" TabIndex="1" AppendDataBoundItems="True" ToolTip="Pattern Name" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlPattern_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPattern" runat="server" ControlToValidate="ddlPattern"
                                            Display="None" ErrorMessage="Please Select Pattern" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPattern"
                                            Display="None" ErrorMessage="Please Select Pattern" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Absent"></asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPattern"
                                            Display="None" ErrorMessage="Please Select Pattern" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="ReportShow"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTest" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="0">MINOR-I</asp:ListItem>
                                        <asp:ListItem Value="1">MINOR-II</asp:ListItem>
                                        <asp:ListItem Value="2">MINOR-I & MINOR-II(INTERNAL EXAM)</asp:ListItem>
                                        <asp:ListItem Value="3">MID SEM-TH</asp:ListItem>
                                        <asp:ListItem Value="4">END SEM-PR</asp:ListItem>
                                        <asp:ListItem Value="5">END SEM-TH</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTest"
                                            Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSubExam" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Sub Exam</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubExam" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSubExam_SelectedIndexChanged"
                                            AppendDataBoundItems="true" TabIndex="1"
                                            data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubExam" runat="server" ControlToValidate="ddlSubExam"
                                            Display="None" ErrorMessage="Please Select Sub Exam" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trStud" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rblStud" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">Regular Student</asp:ListItem>
                                            <asp:ListItem Value="1">BackLog Student</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-7 col-md-12 col-12 ">
                                        <div id="Note2" class=" note-div" runat="server" visible="false">
                                            <h5 class="heading">Note (Please Select)</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Marks Entry Not Done Report - <span style="color: green; font-weight: bold">Session->School->Scheme->Semester->Exam</span></span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Lock Entry Not Done Report - <span style="color: green; font-weight: bold">Session-&gt;School-&gt;Scheme-&gt;Semester-&gt;Exam&nbsp; </span></span></p>
                                        </div>
                                    </div>
                                    <div id="Note" runat="server" class="form-group col-lg-7 col-md-12 col-12" visible="false">
                                        <div class=" note-div">
                                            <h5 class="heading">Note (Please Select)</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>For Status Button - <span style="color: green; font-weight: bold">School & Scheme->Session</span></span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport2" runat="server" Text="Mark Entry Not Done Report"
                                    CssClass="btn btn-info" OnClick="btnReport2_Click" ValidationGroup="Summary" TabIndex="1" />

                                <asp:Button ID="btnReportlock" runat="server" Text="Lock Entry Not Done Report"
                                    CssClass="btn btn-info" OnClick="btnReportlock_Click" ValidationGroup="Summary" TabIndex="1" />

                                <asp:Button ID="btnMarkStatus" runat="server" Text="Mark Entry Status"
                                    CssClass="btn btn-info" OnClick="btnMarkStatus_Click" ValidationGroup="MarkEntryStatus" TabIndex="1" Visible="false" />
                                <%--CausesValidation="false"--%>
                                <asp:Button ID="tbnStatus" runat="server" Text="Status"
                                    CssClass="btn btn-info" TabIndex="1"  OnClick="tbnStatus_Click" ValidationGroup="Summary" Visible="false" />
                                
                                
                                <asp:Button ID="btnAbsententryreport" runat="server" Text="Absent entry report" CssClass="btn btn-info" TabIndex="1" OnClick="btnAbsententryreport_Click" Visible="false" ValidationGroup="Absent" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Summary" />

                                <asp:ValidationSummary ID="VSMarkEntryStatus" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="MarkEntryStatus" />

                                <asp:ValidationSummary ID="VSStatus" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Status" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Absent" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="1" CausesValidation="false" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnMarkStatus" />
            <asp:PostBackTrigger ControlID="tbnStatus" />
            <asp:PostBackTrigger ControlID="btnAbsententryreport" />

        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        window.onload = function () {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://jsonip.appspot.com/?callback=DisplayIP";
            document.getElementsByTagName("head")[0].appendChild(script);
        };
        function DisplayIP(response) {
            document.getElementById("ipaddress").innerHTML = "Your Public IP Address is " + response.ip;
        }

    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>

