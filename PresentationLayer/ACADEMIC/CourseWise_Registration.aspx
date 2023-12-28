<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true" 
    CodeFile="CourseWise_Registration.aspx.cs" Inherits="CourseWise_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>--%>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dvSession" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Allotment"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dvCollege" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                            <%--<asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>

                                        <asp:ListBox runat="server" ID="ddlCollege" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                        <%-- <asp:ListBox ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true"></asp:ListBox>--%>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" ControlToValidate="ddlCollege" InitialValue=""
                                            Display="None" ValidationGroup="Show" runat="server" ErrorMessage="Please select College."></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCollege" InitialValue=""
                                            Display="None" ValidationGroup="Excel" runat="server" ErrorMessage="Please select College."></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Report Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReportType" runat="server" AppendDataBoundItems="True" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Core Courses</asp:ListItem>
                                            <asp:ListItem Value="2">Elective Courses</asp:ListItem>
                                            <asp:ListItem Value="3">Global Elective Courses</asp:ListItem>
                                            <asp:ListItem Value="4">All Courses</asp:ListItem>
                                            <asp:ListItem Value="5">Pending Courses</asp:ListItem>
                                            <asp:ListItem Value="6">Consolidated Registration Report</asp:ListItem>
                                            <asp:ListItem Value="7">Consolidated Student Wise Course Registration Report</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlReportType"
                                            Display="None" ErrorMessage="Please Select Report Type." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlReportType"
                                            Display="None" ErrorMessage="Please Select Report Type." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPendingCourseType" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourseType" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Core Courses</asp:ListItem>
                                            <asp:ListItem Value="2">Elective Courses</asp:ListItem>
                                            <asp:ListItem Value="3">Global Elective Courses</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCourseType"
                                            Display="None" ErrorMessage="Please Select Course Type." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCourseType"
                                            Display="None" ErrorMessage="Please Select Course Type." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Report Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbReport" runat="server" TabIndex="2" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbReport_SelectedIndexChanged"
                                            CssClass="">
                                            <asp:ListItem Value="1">Summary Details</asp:ListItem>
                                            <asp:ListItem Value="2">Statistical Report</asp:ListItem>
                                            <asp:ListItem Value="3">Pending Course Registration By Students</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvReport" runat="server" ControlToValidate="rdbReport" SetFocusOnError="true"
                                            ErrorMessage="Please Select Report Type." Display="None" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdbReport" SetFocusOnError="true"
                                            ErrorMessage="Please Select Report Type." Display="None" ValidationGroup="Excel">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12 d-none" id="divCourse" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbCourse" runat="server" TabIndex="2" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbCourse_SelectedIndexChanged"
                                            CssClass="">
                                            <asp:ListItem Value="0">&nbsp;Core&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">&nbsp;Elective&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Global Elective&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbCourse" SetFocusOnError="true"
                                            ErrorMessage="Please Select Course Type." Display="None" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:CheckBox ID="chkEnabledEmailSubject" Visible="false" Text="&nbsp;Checked to Send Email" AutoPostBack="true" runat="server" OnCheckedChanged="chkEnabledEmailSubject_CheckedChanged" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dvSubject" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Subject</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" TextMode="MultiLine" CssClass="form-control"
                                            Placeholder="Please Enter Email Subject here"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtSubject" runat="server" ControlToValidate="txtSubject"
                                            Display="None" ErrorMessage="Please Enter Email Subject." ValidationGroup="Email"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dvEmail" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Email</label>
                                        </div>
                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control"
                                            Placeholder="Please Enter Email Message here"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvtxtMessage" runat="server" ControlToValidate="txtMessage"
                                            Display="None" ErrorMessage="Please Enter Email Message." ValidationGroup="Email"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Visible="false" Text="Show" ValidationGroup="Show" OnClick="btnShow_Click" CssClass="btn btn-primary"></asp:Button>
                                <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" Visible="false" ValidationGroup="Email" OnClick="btnSendEmail_Click" CssClass="btn btn-primary"></asp:Button>
                                <asp:Button ID="btnExcel" runat="Server" ToolTip="Excel" Text="Excel Report" TabIndex="3" ValidationGroup="Excel" OnClick="btnExcel_Click1"
                                    CssClass="btn btn-info" />
                                 <asp:Button ID="btnStudentAllotment" runat="Server" Visible="false" ToolTip="Excel" Text="Student Allotment Report(Excel)" TabIndex="3" ValidationGroup="Allotment" OnClick="btnStudentAllotment_Click"
                                    CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" ToolTip="Cancel" Text="Cancel" TabIndex="4" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Email" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Excel" />
                                 <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Allotment" />
                            </div>
                        </div>
                        <div class="col-12 d-none">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Total Selected</label>
                                    </div>
                                    <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                        Style="text-align: center;" Font-Bold="True" Font-Size="Small" Visible="false"></asp:TextBox>
                                    <%--  Reset the sample so it can be played again --%>
                                    <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                        WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                    <asp:HiddenField ID="hftot" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                <asp:ListView ID="lvStudents" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>COURSES</h5>
                                        </div>
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Please Select Students to Send Email</label>
                                        </div>
                                        <table class="table table-bordered table-hover table-striped" id="tblCourseList">
                                            <thead class="bg-light-blue">
                                                <tr id="tr1">
                                                    <%--style="background-color: #F3F3F3;"--%>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllSelect" runat="server" OnClick="checkAllCheckbox(this);" ToolTip="Select or Deselect All Records" />
                                                    </th>
                                                    <th>Reg. No. </th>
                                                    <th>Student Name </th>
                                                    <th>Semester Name </th>
                                                    <th>Scheme Name </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="bg-pista text-darkgray">
                                            <td style="width: 5%;">
                                                <asp:CheckBox ID="chkreport" runat="server" onclick="SelectAll(this);" ToolTip='<%# Eval("IDNO")%>' />
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME")%>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme" runat="server" Text='<%# Eval("SCHEMENAME")%>' ToolTip='<%# Eval("SCHEMENO")%>' />
                                            </td>
                                            <%-- <td>
                                                <asp:Label ID="lblApproval" runat="server" Text='<%# Eval("CREDITS")%>' ToolTip='<%# Eval("COURSENO")%>'/>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lvPendingStudentList" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Pending Student List</h5>
                                        </div>
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Please Select Students to Send Email</label>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" id="tblCourseList" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr id="tr1">
                                                    <%--style="background-color: #F3F3F3;"--%>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllSelect" runat="server" OnClick="checkAllCheckboxPending(this);" ToolTip="Select or Deselect All Records" />
                                                    </th>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Reg. No. </th>
                                                    <th>Student Name </th>
                                                    <th>Semester Name </th>
                                                    <th>Scheme Name </th>
                                                    <th>Student Email</th>
                                                    <th>Student Mobile No.</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="bg-pista text-darkgray">
                                            <td style="width: 5%;">
                                                <asp:CheckBox ID="chkreport" runat="server" onclick="SelectAll(this);" ToolTip='<%# Eval("IDNO")%>' />
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>

                                            <td><%# Container.DataItemIndex + 1%> </td>

                                            <td>
                                                <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME")%>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme" runat="server" Text='<%# Eval("SCHEMENAME")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("EMAILID")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("STUDENTMOBILE")%>' />
                                            </td>
                                            <%-- <td>
                                                <asp:Label ID="lblApproval" runat="server" Text='<%# Eval("CREDITS")%>' ToolTip='<%# Eval("COURSENO")%>'/>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="col-12" visible="false" id="dvNote" runat="server">
                                    <asp:Label ID="lblNoteHeader" Font-Size="Medium" runat="server"></asp:Label>
                                    <asp:Label ID="lblNote" ForeColor="Red" Font-Size="Medium" runat="server"></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnStudentAllotment" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

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

    <script type="text/javascript">
        //function validateAssign() {
        //    var btnSendEmails = $('ctl00_ContentPlaceHolder1_btnSendEmail').val();
        //    var lvstud = $('ctl00_ContentPlaceHolder1_lvStudents').val();
        //    var lvstudCnt = lvstud.length;
        //    var checkedCB = '';
        //    for (var i = 0; i < lvstudCnt; i++) //ctl00_ContentPlaceHolder1_lvStudents_ctrl0_chkSelect
        //    {
        //        checkedCB = $('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_chkSelect').val();
        //        if (checkedCB.checked == 'true')
        //            break;
        //    }
        //    if (checkedCB != 'true') {
        //        alert('Please Select atleast One student to send Email', 'Warning!');
        //        btnSendEmails.enabled = false;
        //        return false;
        //    }
        //    else {
        //        btnSendEmails.enabled = true;
        //        return true;
        //    }
        //}

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvStudents$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvStudents$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        if (headchk.checked == false)
                            e.checked = false;
                }
            }
        }

        function SelectAll(chk) {
            debugger;
            var txtTot = $('#<%= txtTotStud.ClientID %>').val();
            var hftot = $('#<%= hftot.ClientID %>').val();
            for (i = 0; i < hftot; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {

                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }
        function totSubjects(chk) {

            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }
    </script>


    <script type="text/javascript">
      
        function checkAllCheckboxPending(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvPendingStudentList$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvPendingStudentList$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        if (headchk.checked == false)
                            e.checked = false;
                }
            }
        }

        function SelectAll(chk) {
            debugger;
            var txtTot = $('#<%= txtTotStud.ClientID %>').val();
            var hftot = $('#<%= hftot.ClientID %>').val();
            for (i = 0; i < hftot; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvPendingStudentList_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {

                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }
        function totSubjects(chk) {

            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }
    </script>

</asp:Content>
