<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CertificateMaster.aspx.cs" Inherits="ACADEMIC_CertificateMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <style>
       #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
    width: max-content!important;
}
    </style>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="nav-tabs-custom col-12">
                    <ul class="nav nav-tabs mt-2" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link" runat="server" visible="false" data-toggle="tab" href="#tabLC" tabindex="1">Transfer Certificate</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#tabBC" tabindex="2">Other Certificates</a>
                        </li>
                    </ul>

                    <div class="tab-content" id="my-tab-content">
                        <div class="tab-pane fade" id="tabLC">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updpnlExam" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-12 pl-0 pr-0" id="divTrans" runat="server" visible="false">
                                                <div class="box-header with-border">
                                                    <h3 class="box-title">LEAVING CERTIFICATES</h3>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-lg-9 col-md-12 col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Bulk Printing</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Admission Batch</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlAdmBatch1" runat="server" AppendDataBoundItems="True">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAdmBatch1"
                                                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0"
                                                                            SetFocusOnError="True" ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Session</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSession1" runat="server" AppendDataBoundItems="True">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession1"
                                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Degree</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlDegree1" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlDegree1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree1"
                                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                                            ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Branch</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlBranch1" runat="server" AppendDataBoundItems="True">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch1"
                                                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                                            ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Semester</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSemester1" runat="server" AppendDataBoundItems="True">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSemester1"
                                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                                                            ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12 d-none">
                                                                        <div class="label-dynamic">
                                                                            <label>Attendance</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlAttendance" runat="server">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Satisfactory</asp:ListItem>
                                                                            <asp:ListItem Value="2">Avarage</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-lg-3 col-md-12 col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Single Printing</h5>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Enrollment No.</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSearch_Enrollno_LC" runat="server" CssClass="unwatermarked"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvSearch_Enrollno_lc" runat="server" ControlToValidate="txtSearch_Enrollno_LC" Display="None"
                                                                            ErrorMessage="Please Enter Enrollment No." ValidationGroup="Search_LC"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                                        <asp:Button ID="btnSearch_LC" runat="server" OnClick="btnSearch_LC_Click" Text="Search" ValidationGroup="Search_LC" CssClass="btn btn-primary" />
                                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search_LC" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12 col-md-12 col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Transfer Detail</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Conduct</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlConduct" runat="server">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Good</asp:ListItem>
                                                                            <asp:ListItem Value="2">Bad</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvConduct" runat="server" ControlToValidate="ddlConduct"
                                                                            Display="None" ErrorMessage="Please Select Conduct Remark" ValidationGroup="Confirm"
                                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>
                                                                                Date Of Leaving
                                                                                <asp:CompareValidator ID="cvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                                                    EnableClientScript="False" ErrorMessage="Please enter a valid start date (mm/dd/yyyy)."
                                                                                    Operator="DataTypeCheck" Type="Date" ValidationGroup="Conform"></asp:CompareValidator>
                                                                                <asp:RequiredFieldValidator ID="rfvLeavindDate" runat="server" ControlToValidate="txtEndDate"
                                                                                    Display="None" ErrorMessage="Please Select Date of Leaving" SetFocusOnError="True"
                                                                                    ValidationGroup="Confirm"></asp:RequiredFieldValidator>
                                                                            </label>
                                                                        </div>
                                                                        <div class="input-group">
                                                                            <div class="input-group-addon" id="imgEndDate">
                                                                                <i class="fa fa-calendar"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                                                            <%-- <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                                                            <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                PopupButtonID="imgEndDate" TargetControlID="txtEndDate">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <ajaxToolKit:MaskedEditExtender ID="meEndDate" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                                                TargetControlID="txtEndDate">
                                                                            </ajaxToolKit:MaskedEditExtender>
                                                                            <ajaxToolKit:MaskedEditValidator ID="mvEndDate" runat="server" ControlExtender="meEndDate"
                                                                                ControlToValidate="txtEndDate" Display="None" EmptyValueMessage="Please Enter Date Of Leaving"
                                                                                ErrorMessage="Please Select Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                                IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="submit">                                                      
                                                                            </ajaxToolKit:MaskedEditValidator>

                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Reason</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                                                                            Display="None" ErrorMessage="Please Select Reason" ValidationGroup="Confirm"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Remark</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-12 btn-footer">
                                                                        <asp:Button ID="btnShowData1" runat="server" OnClick="btnShowData1_Click" Text="Show Students"
                                                                            ValidationGroup="Show1" CssClass="btn btn-primary" />
                                                                        <asp:Button ID="btnConfirm_LC" runat="server" OnClick="btnConfirm_LC_Click" Text="Confirm"
                                                                            CssClass="btn btn-primary" />
                                                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click"
                                                                            Text="Print Report" CssClass="btn btn-info" />
                                                                        <asp:Button ID="btnCancel_LC" runat="server" OnClick="btnCancel_LC_Click"
                                                                            Text="Cancel" CssClass="btn btn-warning" />
                                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Confirm" />
                                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                            ShowSummary="False" ValidationGroup="Show1" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="lvStudentRecords_LC" runat="server" EnableModelValidation="True">
                                                                <EmptyDataTemplate>
                                                                    <div>
                                                                        -- No Student Record Found --
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Search Results</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="totAll(this);" ToolTip="Select or Deselect All Records" />
                                                                                </th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>Reg No.<%--Roll No.--%></th>
                                                                                <th>Degree
                                                                                </th>
                                                                                <th>Branch
                                                                                </th>
                                                                                <th>Year
                                                                                </th>
                                                                                <th>Semester
                                                                                </th>
                                                                                <th>Batch
                                                                                </th>
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
                                                                            <asp:CheckBox ID="chkReport" runat="server" /><asp:HiddenField ID="hidIdNo" runat="server"
                                                                                Value='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STUDNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REGNO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CODE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("YEARNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BATCHNAME")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <asp:ListView ID="lvIssueCert" runat="server" EnableModelValidation="True">
                                                                <EmptyDataTemplate>
                                                                    <%--<div align="center" class="data_label">
                                                                            -- No Student Record Found --
                                                                        </div>--%>
                                                                </EmptyDataTemplate>

                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Search Results</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <%--<th width="5%">
                                                                                    <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="totAll(this);"  ToolTip="Select or Deselect All Records" />
                                                                                </th>--%>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>Reg No.<%--RegNo.--%></th>
                                                                                <th>Branch
                                                                                </th>
                                                                                <th>Semester
                                                                                </th>
                                                                                <th>IssuePerson
                                                                                </th>
                                                                                <th>IssueDate
                                                                                </th>
                                                                                <th>Status
                                                                                </th>
                                                                                <%--<th width="10%">
                                                                                    Action
                                                                                </th>--%>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <%-- <td width="5%">
                                                                                <asp:CheckBox ID="chkReport" runat="server" /><asp:HiddenField ID="hidIdNo" runat="server"
                                                                                    Value='<%# Eval("IDNO") %>' />
                                                                            </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("SHORTNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REGNO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("UANAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ISSUE_DATE", "{0:dd/MM/yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ISSUE_STATUS")%>
                                                                        </td>
                                                                        <%--<td>
                                                                                <asp:ImageButton ID="btnCan" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("CERT_TR_NO") %>'
                                                                                    AlternateText="Cancel Record" ToolTip="Click on button for Cancel Certificate"
                                                                                    OnClick="btnCan_Click" />
                                                                            </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="tab-pane active" id="tabBC">
                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam2"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updpnlExam2" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-12 pl-0 pr-0">
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Bulk Printing</h5>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <%-- <label>Adm Batch</label>--%>
                                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AppendDataBoundItems="True" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                                    Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Session</label>--%>
                                                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AppendDataBoundItems="True" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session"
                                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>College/School Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                                    Display="None" ErrorMessage="Please Select College" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>Degree</label>--%>
                                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None"
                                                                    ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>Branch</label>--%>
                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch" AutoPostBack="true" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None"
                                                                    ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Certificate Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCert" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCert_SelectedIndexChanged"
                                                                    AutoPostBack="True" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCertificateName" runat="server" ControlToValidate="ddlCert"
                                                                    Display="None" ErrorMessage="Please Select Certificate Name" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvCertificateNamePrint" runat="server" ControlToValidate="ddlCert"
                                                                    Display="None" ErrorMessage="Please Select Certificate Name" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="Print"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvCertificateNameSearch" runat="server" ControlToValidate="ddlCert"
                                                                    Display="None" ErrorMessage="Please Select Certificate Name" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-lg-12 col-md-12 col-12" id="divtcpartfull" runat="server" visible="false">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Select Certificate</label>
                                                                        </div>
                                                                        <asp:RadioButtonList ID="rdotcpartfull" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdotcpartfull_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="1">&nbsp;TC Certificate Full Time</asp:ListItem>
                                                                            <asp:ListItem Value="2">&nbsp;TC Certificate Part Time</asp:ListItem>
                                                                            <asp:ListItem Value="3">&nbsp;DisContinue</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>


                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>
                                                                                Date Of Issue
                                                                                <asp:CompareValidator ID="cvissuedate" runat="server" ControlToValidate="txtissuedate"
                                                                                    EnableClientScript="False" ErrorMessage="Please enter Date of Issue(mm/dd/yyyy)."
                                                                                    Operator="DataTypeCheck" Type="Date" ValidationGroup="Print"></asp:CompareValidator>
                                                                                <%--   <asp:RequiredFieldValidator ID="rfvissuedate" runat="server" ControlToValidate="txtissuedate"
                                                                            Display="None" ErrorMessage="Please Select Date of Issue" SetFocusOnError="True"
                                                                            ValidationGroup="ViewReport"></asp:RequiredFieldValidator>--%>
                                                                            </label>

                                                                        </div>

                                                                        <div class="input-group">
                                                                            <div class="input-group-addon" id="imgissuedate">
                                                                                <i class="fa fa-calendar"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtissuedate" runat="server"></asp:TextBox>
                                                                            <%-- <asp:Image ID="imgissuedate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                                                            <ajaxToolKit:CalendarExtender ID="ceissuedate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                PopupButtonID="imgissuedate" TargetControlID="txtissuedate">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <ajaxToolKit:MaskedEditExtender ID="meissuedate" runat="server" CultureAMPMPlaceholder=""
                                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                                                TargetControlID="txtissuedate">
                                                                            </ajaxToolKit:MaskedEditExtender>
                                                                            <%--   <ajaxToolKit:MaskedEditValidator ID="mvissuedate" runat="server" ControlExtender="meissuedate"
                                                                        ControlToValidate="txtissuedate" Display="None" EmptyValueMessage="Please Select Date of Issue"
                                                                        ErrorMessage="Please Select Date of Issue" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Print">                                                      
                                                                    </ajaxToolKit:MaskedEditValidator>--%>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>

                                                                            <label>Check If Branch name Not Required</label>
                                                                        </div>
                                                                        <asp:CheckBox ID="Chkstatus" runat="server" AutoPostBack="true" />
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Enter Branch/Programme</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtbranchcode" runat="server" CssClass="form-controll"
                                                                            ToolTip="Please Enter Inter Branch/Programme" MaxLength="100"></asp:TextBox>
                                                                        <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtConvocation" ErrorMessage="Please Enter Convocation Date"
                                                                ValidationGroup="Print" Display="None"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>Semester</label>--%>
                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" ErrorMessage="Please Select Semester"
                                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%-- <sup>* </sup>--%>
                                                                    <label>
                                                                        Date Of Leaving the College
                                                                                <asp:CompareValidator ID="cvleaving" runat="server" ControlToValidate="txtleaving"
                                                                                    EnableClientScript="False" ErrorMessage="Please enter Date of Leaving the College (mm/dd/yyyy)."
                                                                                    Operator="DataTypeCheck" Type="Date" ValidationGroup="Print"></asp:CompareValidator>
                                                                        <asp:RequiredFieldValidator ID="rfvleaving" runat="server" ControlToValidate="txtleaving"
                                                                            Display="None" ErrorMessage="Please Select Date of Leaving the College" SetFocusOnError="True"
                                                                            ValidationGroup="ViewReport"></asp:RequiredFieldValidator>
                                                                    </label>

                                                                </div>

                                                                <div class="input-group">
                                                                    <div class="input-group-addon" id="imgleaving">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtleaving" runat="server"></asp:TextBox>
                                                                    <%-- <asp:Image ID="imgleaving" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                                                    <ajaxToolKit:CalendarExtender ID="celeaving" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        PopupButtonID="imgleaving" TargetControlID="txtleaving">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="meleaving" runat="server" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                                        TargetControlID="txtleaving">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                    <%--   <ajaxToolKit:MaskedEditValidator ID="mvleaving" runat="server" ControlExtender="meleaving"
                                                                        ControlToValidate="txtleaving" Display="None" EmptyValueMessage="Please Select Date of Leaving  the College"
                                                                        ErrorMessage="Please Select Date of Leaving  the College" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Print">                                                      
                                                                    </ajaxToolKit:MaskedEditValidator>--%>
                                                                </div>
                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%-- <sup>* </sup>--%>
                                                                    <label>Reason For Leaving the College</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlreason" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="rfvreasonleaving" runat="server" ControlToValidate="ddlreason" Display="None" ErrorMessage="Please Select Reason For Leaving the College"
                                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="ViewReport"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divAcademic" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <%-- <sup>* </sup>--%>
                                                                    <label>Academic Year</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlAcademicYear" Display="None" ErrorMessage="Please Select Academic Year"
                                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="ViewReport"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="CompProgram" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <label>Whether completed the programme</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlcompleteporg" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlcompleteporg"
                                                                     Display="None" ErrorMessage="Please Select Whether completed the programme"
                                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="Print"></asp:RequiredFieldValidator>--%>
                                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                     <sup>* </sup>
                                                                    <label>Conduct and Character</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlconductcharacter" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Satisfactory</asp:ListItem>
                                                                    <asp:ListItem Value="2">Avarage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlconductcharacter"
                                                                     Display="None" ErrorMessage="Please Select Conduct and Character"
                                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="Print"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <%--<div class="form-group col-lg-3 col-md-6 col-12" id="divGregno" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <label>G.REG.No</label>
                                                                </div>
                                                                <asp:TextBox ID="txtGregno" runat="server" CssClass="unwatermarked"></asp:TextBox>
                                                            </div>--%>
                                                        </div>
                                                    </div>


                                                    <div class="col-lg-12 col-md-12 col-12" id="divCon" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic" id="lblConvocation" visible="false" runat="server">
                                                                    <sup>* </sup>
                                                                    <label>Convocation Date</label>
                                                                </div>
                                                                <asp:TextBox ID="txtConvocation" Visible="false" OnKeyPress="return blockSpecialChar(event)" runat="server" CssClass="form-controll"
                                                                    ToolTip="Please Enter Convocation Date" MaxLength="100"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvConvocation" runat="server" ControlToValidate="txtConvocation" ErrorMessage="Please Enter Convocation Date"
                                                                    ValidationGroup="Print" Display="None"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic d-none" id="lblClass" runat="server" visible="false">
                                                                    <sup>* </sup>
                                                                    <label>Class</label>
                                                                </div>
                                                                <asp:TextBox ID="txtClass" runat="server" MaxLength="15" Visible="false" Style="display: none;"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvClass" runat="server"  ControlToValidate="txtClass" ErrorMessage="Please Enter Class" SetFocusOnError="true"
                                                                    Display="None"  ValidationGroup="Print"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-12 col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Single Printing</h5>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <%--<label>Registration No</label>--%>
                                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSearch_Enrollno_BC" runat="server" CssClass="unwatermarked"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvSearch_Enrollno" runat="server" ControlToValidate="txtSearch_Enrollno_BC"
                                                                            ErrorMessage="Please Enter Registration No." Display="None" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtSearch_Enrollno_BC"
                                                                            WatermarkText="Enter PRN/Registration No " Enabled="True" />
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:Button ID="btnSearch_BC" runat="server" Text="Search" OnClick="btnSearch_BC_Click"
                                                                            ValidationGroup="Search" CssClass="btn btn-primary" />
                                                                        <asp:ValidationSummary ID="vsSearch" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                            ShowSummary="False" ValidationGroup="Search" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-md-12 col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Student Report Status</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            <%--<label>Registration No</label>--%>
                                                                        </div>
                                                                        <asp:TextBox ID="txtEnrollBonafide" runat="server" CssClass="unwatermarked"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEnrollBonafide"
                                                                            ErrorMessage="Please Enter PRN/Registration No." Display="None" ValidationGroup="SearchBonafide"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEnrollBonafide"
                                                                            WatermarkText="Enter PRN/Registration No" Enabled="True" />
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:Button ID="btnEnrollBonafide" runat="server" Text="Search" CssClass="btn btn-primary"
                                                                            ValidationGroup="SearchBonafide" OnClick="btnEnrollBonafide_Click" />
                                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                            ShowSummary="False" ValidationGroup="SearchBonafide" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnlFees" runat="server" Visible="False">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Fees Paid</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Tuition Fees</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTuitionFee" runat="server" MaxLength="10"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbTuition" runat="server"
                                                                        ValidChars="0123456789." TargetControlID="txtTuitionFee" Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Exam Fees</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtExamFee" runat="server" MaxLength="10"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                                        runat="server" ValidChars="0123456789." TargetControlID="txtExamFee"
                                                                        Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Other Fees</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtOtherFee" runat="server" MaxLength="10"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                                                        runat="server" ValidChars="0123456789." TargetControlID="txtOtherFee"
                                                                        Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Hostel Fees</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtHostelFee" runat="server" MaxLength="10"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                                                        runat="server" ValidChars="0123456789." TargetControlID="txtHostelFee"
                                                                        Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:ValidationSummary ID="valSummery0" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="Print" />

                                                    <div class="col-12" id="Tr1" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Td1" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:CheckBox ID="chkAddTextOption" runat="server"
                                                                    Text="He/She is a Day Scholar of our college" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="idConversion" runat="server" visible="False">
                                                                <div class="label-dynamic" id="Td2" runat="server" visible="False">
                                                                    <label>Select Conversion Certificate Types</label>
                                                                </div>
                                                                <asp:RadioButtonList ID="rdbConversion" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Selected="True">SGPA_WISE</asp:ListItem>
                                                                    <asp:ListItem Value="2">CGPA_WISE</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trBonaSemester" runat="server" visible="False">
                                                                <div class="label-dynamic" id="Td3" runat="server">
                                                                    <label>SGPA CGPA Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="semesterBonafied" runat="server" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShowData" runat="server" OnClick="btnShowData_Click" Text="Show Students"
                                                            ValidationGroup="Show" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View Leaving Certificate" ValidationGroup="ViewReport" Visible="false"
                                                            CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnConfirm_BC" runat="server" OnClick="btnConfirm_BC_Click" Text="Confirm"
                                                            CssClass="btn btn-primary" ValidationGroup="Print" CausesValidation="true" />
                                                        <asp:Button ID="btnReport" runat="server" Text="Print Report" ValidationGroup="Print"
                                                            CssClass="btn btn-info" OnClick="btnReport_Click" />
                                                        <asp:Button ID="btnStatsticalReport" runat="server" Text="Certificate Statistical Report" Visible="false"
                                                            CssClass="btn btn-info" OnClick="btnStatsticalReport_Click" />
                                                        <asp:Button ID="btnReportWithHeader" runat="server" Text="Print Report With Header" ValidationGroup="Print"
                                                            CssClass="btn btn-info" OnClick="btnReportWithHeader_Click" Visible="False" />
                                                        <asp:Button ID="btnExcelSheetReport" runat="server"
                                                            Text="Excel Provisional Report" CssClass="btn btn-info"
                                                            OnClick="btnExcelSheetReport_Click" Visible="False" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="Show" />
                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="ViewReport" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel3" runat="server">
                                                            <asp:ListView ID="lvStudentRecords" runat="server" EnableModelValidation="True" OnItemDataBound="lvStudentRecords_ItemDataBound">
                                                                <EmptyDataTemplate>
                                                                    <div>
                                                                        -- No Student Record Found --
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Search Results</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="totAll(this);"
                                                                                        ToolTip="Select or Deselect All Records" />
                                                                                </th>
                                                                                <%-- <th>Registartion No..
                                                                                </th>--%>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtYear" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th id="thconduct">Conduct and Character
                                                                                </th>
                                                                                <th id="thcount">Issue Count
                                                                                </th>
                                                                                <th id="thGReg">G.RegNo
                                                                                </th>
                                                                                <th>Remark
                                                                                </th>
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
                                                                            <asp:CheckBox ID="chkReport" runat="server" ToolTip='<%# Eval("IDNO") %>' /><asp:HiddenField ID="hidIdNo" runat="server"
                                                                                Value='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <%--<td>
                                                                            <%# Eval("ENROLLNO")%>
                                                                        </td>--%>
                                                                        <td>
                                                                            <%# Eval("REGNO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STUDNAME")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("CODE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("YEARNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BATCHNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlconductcharacter" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField ID="hfConductNo" runat="server" Value='<%# Eval("CONDUCT_NO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblcount" runat="server" CssClass="form-control" Text='<%# Eval("ISSUE_STATUS")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtGReg" runat="server" Text='<%# Eval("GENERAL_REGNO")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtRemark" runat="server" Visible="false"></asp:TextBox>
                                                                            <asp:DropDownList ID="ddlRemark" runat="server" AppendDataBoundItems="True" data-select2-enable="true" Visible="false">
                                                                                 <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                             <asp:HiddenField ID="hfdddlRemark" runat="server" Value='<%# Eval("REMARK")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel4" runat="server">
                                                            <asp:ListView ID="lvIssueCertBona" runat="server" EnableModelValidation="True">
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>

                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Student Reports Status</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <%-- <th>Registartion No..
                                                                                </th>--%>
                                                                                <th>Reg No.<%--Exam Roll No.--%></th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>Degree
                                                                                </th>
                                                                                <th>Branch
                                                                                </th>
                                                                                <th>Semester
                                                                                </th>
                                                                                <th>IssuePerson
                                                                                </th>
                                                                                <th>IssueDate
                                                                                </th>
                                                                                <th>IssueYear
                                                                                </th>
                                                                                <th>Remark
                                                                                </th>
                                                                                <th>Cert_Name
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>

                                                                        <%--  <td>
                                                                            <%# Eval("ENROLLNO")%>
                                                                        </td>--%>
                                                                        <td>
                                                                            <%# Eval("REGNO")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("SHORTNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CODE")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("UANAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ISSUE_DATE", "{0:dd/MM/yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ISSUE_YEAR")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REMARK")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CERTIFICATENAME")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnStatsticalReport" />
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server" />

    <script type="text/javascript">
        function SelectAll(chk) {

            var tbl = document.getElementById("tblStudentRecords");

            if (tbl != null && tbl.childNodes != null) {
                for (i = 0; i < tbl.getElementsByTagName("tr").length; i++) {
                    document.getElementById('ctl00_ContentPlaceHolder1_tcLeaving_tabBC_lvStudentRecords_ctrl' + i + '_chkReport').checked = chk.checked;
                }
            }
        }

        function SelectAll_LC(chk) {
            var tbl = document.getElementById("tblStudentRecords_LC");
            if (tbl != null && tbl.childNodes != null) {
                for (i = 0; i < tbl.getElementsByTagName("tr").length; i++) {
                    document.getElementById('ctl00_ContentPlaceHolder1_tcLeaving_tabLC_lvStudentRecords_LC_ctrl' + i + '_chkReport').checked = chk.checked;
                }
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
    <script type="text/javascript">
        function blockSpecialChar(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 47 && k <= 57) || k == 44);
        }
    </script>

</asp:Content>
