<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TimeTableReport.aspx.cs" Inherits="ACADEMIC_EXAMINATION_TimeTableReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="upupdDetained" runat="server" AssociatedUpdatePanelID="updDetained">
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


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Exam Time Table Report</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updDetained" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSessionReport" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSessionReport" runat="server" ControlToValidate="ddlSessionReport"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSess1" runat="server" ControlToValidate="ddlSessionReport"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlSessionReport"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="TimeTable">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" ToolTip="Please Select Institute"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            ValidationGroup="Report" Display="None" ErrorMessage="Please Select Institute"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="rfvcoll" runat="server" ControlToValidate="ddlCollege"
                                            ValidationGroup="show" Display="None" ErrorMessage="Please Select Institute"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegreeReport" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegreeReport_SelectedIndexChanged"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegreeReport" runat="server" ControlToValidate="ddlDegreeReport"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDeg" runat="server" ControlToValidate="ddlDegreeReport"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchReport" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlBranchReport_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranchReport" runat="server" ControlToValidate="ddlBranchReport"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvBr" runat="server" ControlToValidate="ddlBranchReport"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True" TabIndex="5" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            TabIndex="6">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            ValidationGroup="show" Display="None" ErrorMessage="Please Select Semester"
                                            SetFocusOnError="true" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="rfsem" runat="server" ControlToValidate="ddlSemester"
                                            ValidationGroup="Report" Display="None" ErrorMessage="Please Select Semester"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamName"
                                            ValidationGroup="Report" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="rfvExa" runat="server" ControlToValidate="ddlExamName"
                                            ValidationGroup="show" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList runat="server" ID="rdReg_Ex" RepeatDirection="Horizontal" RepeatColumns="2">
                                            <asp:ListItem Value="0" Selected="True">Regular&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">Ex-Student</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="FromDate" runat="server">
                                                <i class="fa fa-calendar" id="FromDate1"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="11" CssClass="form-control pull-right"
                                                placeholder="From Date" ToolTip="Please Select From Date" />

                                            <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="FromDate1" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>

                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />



                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="TimeTable"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="ToDate" runat="server">
                                                <i class="fa fa-calendar" id="ToDate1"></i>
                                            </div>

                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="12" ValidationGroup="submit" placeholder="To Date"
                                                ToolTip="Please Select To Date" CssClass="form-control pull-right" />

                                            <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTodate" PopupButtonID="ToDate1" Enabled="True">
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

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="TimeTable"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>For Date Wise Time Table Please Select Session And From Date & To Date </span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnReport" runat="server" Text="Exam Time Table Report" OnClick="btnReport_Click" ValidationGroup="Report" CssClass="btn btn-info" />

                                <asp:Button ID="btnStudAttndance" runat="server" Text="Student Attendence Sheet" CssClass="btn btn-info"
                                    OnClick="btnStudAttndance_Click" ValidationGroup="show" CommandArgument="Non-elective" />

                                <asp:Button ID="btnElectiveAttendanceSheet" runat="server" Text="(Elective) Student Attendence Sheet" CssClass="btn btn-info"
                                    OnClick="btnElectiveAttendanceSheet_Click" ValidationGroup="show" CommandArgument="Elective" />
                                <asp:Button ID="btnTheoryReport" runat="server" Text="TimeTable for Theory" ValidationGroup="Report" CssClass="btn btn-info" OnClick="btnTheoryReport_Click" Visible="false" />
                                <asp:Button ID="btnTimeTable" runat="server" Text="Date Wise Time Table(Excel)"
                                    TabIndex="16" OnClick="btnTimeTable_Click" ValidationGroup="TimeTable" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancelReport" runat="server" Text="Cancel" OnClick="btnCancelReport_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vsReport" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Report" />
                                <asp:ValidationSummary ID="vsAtt" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show" />


                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="TimeTable" />

                            </div>

                            <div id="divMsg" runat="server">
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnTimeTable" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>



    <script language="javascript" type="text/javascript">
        function confirmDetaind() {
            return confirm("Are you sure you want to detaind the selected student");
        }
    </script>

</asp:Content>
