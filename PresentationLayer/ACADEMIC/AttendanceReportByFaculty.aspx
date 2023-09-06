<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceReportByFaculty.aspx.cs" Inherits="ACADEMIC_AttendanceReportByFaculty" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReport"
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
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <asp:UpdatePanel ID="updReport" runat="server">
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
                                        <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSchool" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSchool" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchool" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" ValidationGroup="Consolidate" Enabled="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true">Session</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" Font-Bold="True" TabIndex="1" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSessConsole" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Consolidate" Enabled="true"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                        <%--asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvDegConsole" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Consolidate"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label id="branch" runat="server">Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="rfBranchConsole" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="Consolidate"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div id="dvFaculty" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label id="faculty" runat="server">Faculty</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="3" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Faculty" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="rfvSchemeConsole" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="Consolidate"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSemConsole" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Consolidate"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged" data-select2-enable="true"
                                            ValidationGroup="Submit" TabIndex="1" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--                                <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                    ErrorMessage="Please Select Subject Type" InitialValue="0" Display="None" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator> --%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true">Section</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"
                                            ValidationGroup="teacherallot" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  Reset the sample so it can be played again --%>
                                        <%-- <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                    Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%> <%--ValidationGroup="Daywise"--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Theory/Practical/Tutorial</label>
                                        </div>
                                        <asp:DropDownList ID="ddltheorypractical" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddltheorypractical_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>
                                            <asp:ListItem Value="3">Tutorial</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvLTP" runat="server" ControlToValidate="ddltheorypractical"
                                 Display="None" ErrorMessage="Please Select Theory or Practical or Tutorial Type course" InitialValue="0" ValidationGroup="SubPercentage" ></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="idPopup" runat="server">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit"
                                                TabIndex="1" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="idPopup" TargetControlID="txtFromDate" Animated="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Report" />
                                            <ajaxToolKit:MaskedEditValidator ID="meFromConsole" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Consolidate" Enabled="false" />

                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="idPopuptodate" runat="server">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="1" ValidationGroup="submit" />
                                            <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" PopupButtonID="idPopuptodate"
                                                TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />

                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Report" />

                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                            <ajaxToolKit:MaskedEditValidator ID="meToConsole" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Consolidate" Enabled="false" />

                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 25px;">
                                        <asp:RadioButton ID="rdoPerBtn" runat="server" OnCheckedChanged="rdoPerBtn_CheckedChanged" TabIndex="1" AutoPostBack="true" Text=" Between Percentage" GroupName="GetPercent" />
                                    </div>
                                    <%--Added by Nikhil Vinod Lambe on 25022020--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 25px; display: none">
                                        <asp:RadioButton ID="rdoOpr" runat="server" AutoPostBack="true" Text="With Operator" TabIndex="1" OnCheckedChanged="rdoOpr_CheckedChanged" GroupName="GetPercent" />
                                    </div>
                                    <%--*********************************************************************--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="lblperfrom" runat="server" visible="false">
                                            <sup>* </sup>
                                            <label>Percentage From</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentageFrom" MaxLength="3" Visible="false" TabIndex="1" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPercentageFrom"
                                            Display="None" ErrorMessage="Please Enter From Percentage." ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="lblPerTo" runat="server" visible="false">
                                            <sup>* </sup>
                                            <label>Percentage To</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentageTo" MaxLength="3" Visible="false" TabIndex="1" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPercentageTo" runat="server" ControlToValidate="txtPercentageTo" Display="None"
                                            ErrorMessage="Please Enter To Percentage " ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--Added by Nikhil Vinod Lambe on 25022020--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label id="lblOperator" runat="server" visible="false">Operator</label>
                                        <asp:DropDownList ID="ddlOperator" runat="server" AutoPostBack="true" TabIndex="9" Visible="false" OnSelectedIndexChanged="ddlOperator_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem Selected="True">&gt;=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label id="lblPercentage" runat="server" visible="false">Percentage</label>
                                        <asp:TextBox ID="txtPercentage" runat="server" AutoPostBack="true" MaxLength="3"
                                            TabIndex="10" Visible="false"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            Display="None" ValidationGroup="SubPercentage" ErrorMessage="Please Enter Percentage"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            Display="None" ErrorMessage="Please Enter Valid Percentage." MaximumValue="101"
                                            MinimumValue="0" Type="Integer"></asp:RangeValidator>

                                    </div>
                                    <%-- <div class="form-group col-md-3">
                                <label id="Label2" runat="server" visible="false">Percentage</label>
                                <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" MaxLength="3"
                                    TabIndex="10" Visible="false"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPercentage"
                                    Display="None" ErrorMessage="Please Enter Percentage." ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPercentage"
                                    Display="None" ErrorMessage="Please Enter Valid Percentage." MaximumValue="101"
                                    MinimumValue="0" Type="Integer"></asp:RangeValidator>
                            </div>--%>
                                    <%--***********************************************************************************************************--%>
                                    <div id="dvBatch" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                        <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"><span style="color: red;">&nbsp;</span></asp:Label>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" ValidationGroup="SubPercentage">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-lg-10 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note</h5>
                                    <p runat="server" id="spanNote"><i class="fa fa-star" aria-hidden="true"></i><span>Consolidated Attendance Report Format-I(Excel),Consolidated Attendance Report Format-II(Excel) only Session Selection is Required</span></p>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">


                                <%--Added By Nikhil Vinod lambe on 24022020--%>
                                <asp:Button ID="btnShow" runat="server" Text="Show Attendance Details" OnClick="btnShow_Click" TabIndex="1"
                                    ValidationGroup="SubPercentage" CausesValidation="true" CssClass="btn btn-info" />
                                <%--*******************************************************************************************************--%>
                                <asp:Button ID="btnSubjectwise" runat="server" Text="Consolidated Attendance Report Format-I(Excel)"
                                    TabIndex="1" OnClick="btnSubjecwise_Click"
                                    ValidationGroup="Consolidate" CssClass="btn btn-info" />
                                <asp:Button ID="btnSubjectwise2" runat="server" Text="Consolidated Attendance Report Format-II(Excel)"
                                    TabIndex="1" OnClick="btnSubjectwise2_Click"
                                    ValidationGroup="Consolidate" CssClass="btn btn-info" />
                                <asp:Button ID="btnSubjectwiseExpected" runat="server" Text="Subject Wise Summary Attendance Report"
                                    TabIndex="1" OnClick="btnSubjectwiseExpected_Click" Visible="false"
                                    ValidationGroup="SubPercentage" CssClass="btn btn-info" />
                                <asp:Button ID="btnHODReport" runat="server" Text="Attendance Pending Report" OnClick="btnHODReport_Click"
                                    CssClass="btn btn-info" Visible="false" TabIndex="1" ValidationGroup="Report" />

                                <asp:Button ID="btnPercentage" runat="server" Visible="false" OnClick="btnPercentage_Click" Text="Percentage Wise Report" ValidationGroup="SubPercentage" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    TabIndex="1" OnClick="btnCancel_Click1" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="SubPercentage" Style="text-align: center" />
                                <asp:ValidationSummary ID="vsConsole" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Consolidate" Style="text-align: center" />

                                <asp:ValidationSummary ID="validationsummary2" runat="server" ShowMessageBox="true" ShowSummary="false" Style="text-align: center" ValidationGroup="Report" />
                                <%--&nbsp;<asp:ValidationSummary ID="validationsummary3" runat="server" ShowMessageBox="true" ShowSummary="false" Style="text-align: center" ValidationGroup="Consolidate" />--%>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlAttendence" runat="server">
                                    <asp:ListView ID="lvAttendence" runat="server">
                                        <LayoutTemplate>
                                            <table id="divattendencelist" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>REGISTRATION NO</th>
                                                        <th>STUDENT NAME</th>
                                                        <th>SCHEME</th>
                                                        <th>SEMESTER</th>
                                                        <th>PROGRAMME/BRANCH</th>
                                                        <th>SECTION</th>
                                                        <th>OVERALL ATTENDENCE</th>
                                                        <th>OVERALL PRESENT</th>
                                                        <th>OVERALL ABSENT</th>
                                                        <th>TOTAL PERCENTAGE</th>
                                                        <th>Faculty Adv.(LG Name)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceHolder" runat="server">
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%-- <td style="text-align: center;">
                                                     <asp:ImageButton ID="btnEdit" runat="server" Visible="false" ImageUrl="~/images/edit1.gif"
                                                        CommandArgument='<%# Eval("SESSIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>--%>
                                                <td><%#Eval("ENROLLMENT_NO")%></td>
                                                <td><%#Eval("STUDENT_NAME")%></td>
                                                <td><%#Eval("SCHEME")%></td>
                                                <td><%#Eval("SEMESTER")%></td>
                                                <td><%#Eval("BRANCH")%></td>
                                                <td><%#Eval("SECTION")%></td>
                                                <td><%#Eval("TOTAL_CLASSES")%></td>
                                                <td><%#Eval("TOTAL_ATTENDED_CLASSES")%></td>
                                                <td><%#Eval("TOTAL_ABSENT_CLASSES")%></td>
                                                <td><%#Eval("TOTAL_PERCENTAGE")%></td>
                                                <td><%#Eval("FAC_ADVISOR")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlByPercent" runat="server">
                                    <asp:ListView ID="lvByPercent" runat="server">
                                        <LayoutTemplate>
                                            <table id="divAttendanceByPer" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>REGISTRATION NO</th>
                                                        <th>STUDENT NAME</th>
                                                        <th>SCHEME</th>
                                                        <th>SEMESTER</th>
                                                        <th>PROGRAM/BRANCH</th>
                                                        <th>SECTION</th>
                                                        <th>OVERALL ATTENDENCE</th>
                                                        <th>OVERALL PRESENT</th>
                                                        <th>OVERALL ABSENT</th>
                                                        <th>TOTAL PERCENTAGE</th>
                                                        <th>Faculty Adv.(LG Name)</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceHolder" runat="server">
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%-- <td style="text-align: center;">
                                                     <asp:ImageButton ID="btnEdit" runat="server" Visible="false" ImageUrl="~/images/edit1.gif"
                                                        CommandArgument='<%# Eval("SESSIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>--%>
                                                <td><%#Eval("ENROLLMENT_NO") %></td>
                                                <td><%#Eval("STUDENT_NAME")%></td>
                                                <td><%#Eval("SCHEME")%></td>
                                                <td><%#Eval("SEMESTER")%></td>
                                                <td><%#Eval("BRANCH")%></td>
                                                <td><%#Eval("SECTION")%></td>
                                                <td><%#Eval("TOTAL_CLASSES")%></td>
                                                <td><%#Eval("TOTAL_ATTENDED_CLASSES")%></td>
                                                <td><%#Eval("TOTAL_ABSENT_CLASSES")%></td>
                                                <td><%#Eval("TOTAL_PERCENTAGE")%></td>
                                                <td><%#Eval("FAC_ADVISOR")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnSubjectwise" />
            <asp:PostBackTrigger ControlID="btnSubjectwise2" />
        </Triggers>
    </asp:UpdatePanel>



    <script lang="javascript" type="text/javascript">

        function checkSessionList() {
            var ddl = document.getElementById('<%= ddlSession.ClientID %>');

            if (ddl.value == "0") {
                alert('Please Select Session from Session List for Modifying');
                return false;
            }
            else
                return true;
        }
    </script>

    <div>
        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
    </div>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
