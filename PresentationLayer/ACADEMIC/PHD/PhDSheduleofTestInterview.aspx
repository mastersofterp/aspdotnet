<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhDSheduleofTestInterview.aspx.cs" Inherits="ACADEMIC_PHD_PhDSheduleofTestInterview" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <asp:UpdatePanel ID="updSChedul" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom" id="Tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Add Schedule </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Send Email to Student</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdd"
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
                                        <asp:UpdatePanel ID="updAdd" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Admission Batch</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Admission Batch"
                                                                ControlToValidate="ddlAdmBatch" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="DivMultipleSelect" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>School Applied for</label>
                                                            </div>
                                                            <asp:ListBox ID="lboSchool" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>
                                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>School Applied for</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" ToolTip="Please select school applied for">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Please select school applied for."
                                                                ControlToValidate="ddlSchool" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                                        </div>--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>PhD Schedule Of </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlPhDSch" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" TabIndex="2" ToolTip="Please Select PhD Schedule of" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Entrance_Exam</asp:ListItem>
                                                                <asp:ListItem Value="2">Interview</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator44" InitialValue="0" runat="server" ErrorMessage="Please Select PhD Schedule of"
                                                                ControlToValidate="ddlPhDSch" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Exam Mode</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlExammode" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" TabIndex="2" ToolTip="Please Select Exam Mode" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlExammode_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Online</asp:ListItem>
                                                                <asp:ListItem Value="2">Offline</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server" ErrorMessage="Please Select Exam Mode"
                                                                ControlToValidate="ddlExammode" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><span style="color: red">* </span>Date</label>
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <div class="fa fa-calendar text-blue" id="icon"></div>
                                                                </div>
                                                                <asp:TextBox ID="txtStartDate" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Date" AutoComplete="off"></asp:TextBox>
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtStartDate" PopupButtonID="icon" Enabled="true">
                                                            </ajaxToolKit:CalendarExtender>

                                                            <ajaxToolKit:MaskedEditExtender ID="meStartdate" runat="server" TargetControlID="txtStartDate"
                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="rfvStartdate" runat="server" ControlToValidate="txtStartDate"
                                                                Display="None" ValidationGroup="regsubmit" ErrorMessage="Please Enter Date."></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><span style="color: red">* </span>Time</label>

                                                            <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                                            <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtStartTime"
                                                                Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                                MaskType="Time" />
                                                            <%--<ajaxToolKit:MaskedEditValidator ID="EarliestTimeValidator" runat="server" ControlExtender="meStarttime" ControlToValidate="txtStartTime" 
                                                Display="None" ValidationGroup="regsubmit" InvalidValueMessage="Please enter a valid time." 
                                                ValidationExpression="^(1[0-2]|0[1-9]):([0-5][0-9])(\s*)(i?[AP]M])$" SetFocusOnError="True"></ajaxToolKit:MaskedEditValidator>--%>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid time."
                                                                ControlToValidate="txtStartTime" Display="NONE" SetFocusOnError="true" ValidationGroup="regsubmit"
                                                                ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime"
                                                                Display="None" ValidationGroup="regsubmit" ErrorMessage="Please Enter Time."></asp:RequiredFieldValidator>
                                                            <%-- <asp:TextBox ID="txtStartTime" runat="server" MaxLength="5"
                                                TabIndex="2" ToolTip="Please Enter Time From" CssClass="form-control">
                                            </asp:TextBox>
                                            <asp:CheckBox ID="chkFrom" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkFrom_CheckedChanged"
                                                Text="AM" ToolTip="Unchecked for PM" />
                                            <label style="color: green">( Unchecked for PM ) </label>

                                            <asp:RequiredFieldValidator ID="rfvTimeFrom" runat="server"
                                                ControlToValidate="txtStartTime" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Enter Time from" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterMode="InvalidChars" FilterType="Custom" 
                                                InvalidChars="~`!@#$%^*()_+=,./;<>?'{}[]\|-&&quot;qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM'" TargetControlID="txtStartTime" />--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divvenue" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Venue</label>
                                                            </div>
                                                            <asp:TextBox ID="txtVenue" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Please Enter Venue" AutoComplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVenue"
                                                                Display="None" ValidationGroup="regsubmit" ErrorMessage="Please Enter Venue"></asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <%--  <asp:Button ID="btnShow" runat="server" Text="SHOW" CssClass="btn btn-info" TabIndex="6" OnClick="btnShow_Click" ValidationGroup="submit"  />--%>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info" TabIndex="6"  OnClientClick="return validate(this);" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" class="btn btn-warning" TabIndex="7" OnClick="btnCancel_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="regsubmit" Style="text-align: center" />
                                                    <%--<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" Style="text-align: center" />--%>
                                                </div>
                                                <asp:UpdatePanel ID="updlvAdd" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlAdd" runat="server">
                                                                <asp:ListView ID="lvAddSch" runat="server" EnableModelValidation="True">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>PhD Student List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblPhdStudList">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit
                                                                                    </th>
                                                                                    <th>Admission Batch
                                                                                    </th>
                                                                                    <th>School Applied for
                                                                                    </th>
                                                                                    <th>PhD Schedule Of
                                                                                    </th>
                                                                                    <th>Date
                                                                                    </th>
                                                                                    <th>Time
                                                                                    </th>
                                                                                    <th>Exam Mode
                                                                                    </th>
                                                                                    <th>Venue
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
                                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                                                    AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("SCHEDULENO")%>' TabIndex="6" OnClick="btnEdit_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ADMBATCH")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("COLLEGE")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SCHEDULEFOR")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SCHEDULEDATE")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SCHEDULETIME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("EXAM_MODE")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("VENUE")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSend"
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
                                        <asp:UpdatePanel ID="updSend" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Admission Batch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdmbatchS" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmbatchS_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Admission Batch"
                                                                    ControlToValidate="ddlAdmbatchS" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                                    ControlToValidate="ddlAdmbatchS" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>School Applied for</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCollgeid" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollgeid_SelectedIndexChanged" ToolTip="Please select school applied for">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select school applied for."
                                                                    ControlToValidate="ddlCollgeid" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Schedule</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSchedule" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSchedule1" runat="server" ErrorMessage="Please Select Schedule"
                                                                    ControlToValidate="ddlSchedule" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="Send" />
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnShow" runat="server" Text="SHOW" CssClass="btn btn-info" TabIndex="3" OnClick="btnShow_Click" ValidationGroup="submit" />
                                                            <asp:Button ID="btnSend" runat="server" Text="Send Email" CssClass="btn btn-info" TabIndex="4" OnClick="btnSend_Click" ValidationGroup="Send" />
                                                            <asp:Button ID="btncancel1" runat="server" Text="Cancel" CausesValidation="false" class="btn btn-warning" TabIndex="5" OnClick="btncancel1_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Send" Style="text-align: center" />
                                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" Style="text-align: center" />
                                                        </div>
                                                        <%--     <asp:UpdatePanel ID="updPhdlistview" runat="server">
                                                            <ContentTemplate>--%>
                                                        <div class="col-12">
                                                            <asp:Panel ID="Panel3" runat="server">
                                                                <asp:ListView ID="lvPhdA" runat="server" EnableModelValidation="True">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>PhD Student List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" id="tblemailstud">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Select All
                                                                                             <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="totAll(this);" ToolTip="Select or Deselect All Records" />
                                                                                    </th>
                                                                                    <th>Application Id
                                                                                    </th>
                                                                                    <th>Student Name 
                                                                                    </th>
                                                                                    <th>EmailId 
                                                                                    </th>
                                                                                    <th>Schedule 
                                                                                    </th>
                                                                                    <th>Status</th>
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
                                                                                <asp:CheckBox ID="chkReport" runat="server" ToolTip='<%# Eval("USERNO") %>' />
                                                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("USERNO") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblUsername" runat="server" Text=' <%# Eval("USERNAME")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDENT_NAME")%>'></asp:Label>
                                                                                <asp:HiddenField ID="hidBranch" runat="server" Value='<%# Eval("BRANCH") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label>
                                                                            </td>
                                                                             <td>
                                                                                  <asp:Label ID="Label1" runat="server" Text='<%# Eval("SCHEDULE")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                 <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("EMAILID_STATUS")%>' Font-Bold="true" ForeColor='<%# (Convert.ToString(Eval("EMAILID_STATUS"))=="Send" ?  System.Drawing.Color.Green : System.Drawing.Color.Red)%>' ></asp:Label>
                                                                                <%--<asp:Label ID="lblstatus" runat="server" Text='<% #Eval("EMAILID")%>' Font-Bold="true" ForeColor='<%# (Convert.ToInt32(Eval("EMAILID") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>--%>
                                                                            </td>
                                                                            <%--  <td>
                                                                 <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("EMAILSTATUS")%>' ForeColor='<%#Eval("EMAILSTATUS").ToString().Equals("NOT SEND")?System.Drawing.Color.Red : System.Drawing.Color.Green %>'></asp:Label>
                                                            </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                        <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server" />
     <script>
         function validate() {
             var ddlAdmBatch = document.getElementById("ctl00_ContentPlaceHolder1_ddlAdmBatch").value;
             var ddlPhDSch = document.getElementById("ctl00_ContentPlaceHolder1_ddlPhDSch").value;
             var ddlExammode = document.getElementById("ctl00_ContentPlaceHolder1_ddlExammode").value;
             var txtStartDate = $("ctl00_ContentPlaceHolder1_txtStartDate").val();
             var txtStartTime = document.getElementById("ctl00_ContentPlaceHolder1_txtStartTime").value;
             var txtVenue = document.getElementById("ctl00_ContentPlaceHolder1_txtVenue").value;

             if (ddlAdmBatch == "0") {

                 alert("Please Select Admission Batch.");
                 return false;
             }
             var lboSchool = $("[id$=lboSchool]").attr("id");
             var lboSchool = document.getElementById(lboSchool);
             if (lboSchool.value == 0) {
                 alert('Please Select Atleast One School', 'Warning!');
                 $(lboSchool).focus();
                 return false;
             }
             if (ddlPhDSch == "0") {
                 alert("Please Select PhD Schedule of.");
                 return false;
             }
             if (ddlExammode == "0") {
                 alert("Please Select Exam Mode.");
                 return false;
             }
             if (txtStartDate == "" || txtStartTime == "DD/MM/YYYY") {
                 alert("Please Enter Attendance Start Date.");
                 return false;
             }
             if (txtStartTime == "") {

                 alert("Please Enter Start Time.");
                 return false;
             }
             if (txtVenue == "") {
                 alert("Please Enter Venue.");
                 return false;
             }
         }

    </script>
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
    
    <script>
        function validation()
        {
            var ddlAdmBatch = document.getElementById("ctl00_ContentPlaceHolder1_ddlAdmBatch").value;
            var ddlPhDSch = document.getElementById("ctl00_ContentPlaceHolder1_ddlPhDSch").value;
            var ddlExammode = document.getElementById("ctl00_ContentPlaceHolder1_ddlExammode").value;
            var txtStartDate = $("ctl00_ContentPlaceHolder1_txtStartDate").val();
            var txtStartTime = document.getElementById("ctl00_ContentPlaceHolder1_txtStartTime").value;
            var txtVenue = document.getElementById("ctl00_ContentPlaceHolder1_txtVenue").value;

            if (ddlAdmBatch == "0") {
               
                alert("Please Select Admission Batch.");
                return false;
            }
            var lboSchool = $("[id$=lboSchool]").attr("id");
            var lboSchool = document.getElementById(lboSchool);
            if (lboSchool.value == 0) {
                alert('Please Select Atleast One School', 'Warning!');
                $(lboSchool).focus();
                return false;
            }
            if (ddlPhDSch == "0") {
                alert("Please Select PhD Schedule of.");
                return false;
            }
            if (ddlExammode == "0") {
                alert("Please Select Exam Mode.");
                return false;
            }
            if (txtStartDate == "" || txtStartTime == "DD/MM/YYYY") {
                alert("Please Enter Attendance Start Date.");
                return false;
            }
            if (txtStartTime == "") {

                alert("Please Enter Start Time.");
                return false;
            }
            if (txtVenue == "") {
                alert("Please Enter Venue.");
                return false;
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

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblPhdStudList').DataTable({
                responsive: true,
                lengthChange: true,
                //scrollY: 320,
                //scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblPhdStudList').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblPhdStudList').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblPhdStudList').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblPhdStudList').DataTable({
                    responsive: true,
                    lengthChange: true,
                    //scrollY: 320,
                    //scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblPhdStudList').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblPhdStudList').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblPhdStudList').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblemailstud').DataTable({
                responsive: true,
                lengthChange: true,
                //scrollY: 320,
                //scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblemailstud').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblemailstud').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblemailstud').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblemailstud').DataTable({
                    responsive: true,
                    lengthChange: true,
                    //scrollY: 320,
                    //scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblemailstud').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblemailstud').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblemailstud').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>

</asp:Content>

