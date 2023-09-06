<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Phd_Mark_entry.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="ACADEMIC_PHD_Phd_Mark_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="udpphd" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Course Creation </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1" id="tab1">Ph.D. Mark Entry</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Ph.D. Mark Entry Criteria</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updmarkEntry"
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

                                        <asp:UpdatePanel ID="updmarkEntry" runat="server">
                                            <ContentTemplate>
                                                <%-- <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box box-primary">--%>
                                                <%--     <div class="box-header with-border">
                                                                <h3 class="box-title">
                                                                    <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                                                                </h3>
                                                            </div>--%>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Admission Batch</label>
                                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                                        ControlToValidate="ddlAdmBatch" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                                        ControlToValidate="ddlAdmBatch" Display="None" SetFocusOnError="true" ValidationGroup="excel" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>School Applied for</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" ToolTip="Please select school applied for">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Please select school applied for."
                                                                    ControlToValidate="ddlSchool" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--  <sup>* </sup>--%>
                                                                    <label>PhD/Programme Applied For</label>
                                                                    <asp:DropDownList ID="ddlprogram" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlprogram_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" ErrorMessage="Please Select PhD/Programme Applied For"
                                                ControlToValidate="ddlprogram" Display="None" SetFocusOnError="true" ValidationGroup="show" />--%>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label>
                                                                    <%--<sup>*</sup>--%>
                                            Mode of pursuing PhD</label>
                                                                <asp:DropDownList ID="ddlPhDMode" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" ToolTip="Please Select Mode of pursuing PhD" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPhDMode_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Full Time</asp:ListItem>
                                                                    <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                                    <%--<asp:ListItem Value="3">Full Time with Fellowship</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator44" InitialValue="0" runat="server" ErrorMessage="Please Select Mode of pursuing PhD"
                                            ControlToValidate="ddlPhDMode" Display="None" SetFocusOnError="true" ValidationGroup="show" />--%>
                                                                <asp:HiddenField ID="hdftestmark" runat="server" />
                                                                <asp:HiddenField ID="hdfinterview" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--     <div class="col-12">
                                <div id="div1" runat="server">
                                    <label style="color:red; text-align:left;font:bold">Note : AB For absent Student</label>
                                </div>
                            </div>--%>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnshow" runat="server" TabIndex="4" Text="Show" OnClick="btnshow_Click" CssClass="btn btn-primary" ValidationGroup="show" />
                                                        <asp:Button ID="btnSubmit" runat="server" TabIndex="5" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" Visible="false" />
                                                        <asp:Button ID="btnreport" runat="server" TabIndex="6" Text="Excel Report" OnClick="btnreport_Click" CssClass="btn btn-primary" ValidationGroup="excel" />
                                                        <asp:Button ID="btncancel" runat="server" TabIndex="7" Text="Cancel" CssClass="btn btn-warning" OnClick="btncancel_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="show"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="excel"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                    <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                        <asp:ListView ID="LvPhdMark" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Search List</h5>
                                                                    <label style="color: red; text-align: left; font: bold; font-size: large">Note : Enter AB For Absent Student</label>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="lstTable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center">Sr.No.
                                                                            </th>
                                                                            <th>Application ID</th>
                                                                            <th>Student Name</th>
                                                                            <th>PhD/Programme Applied For</th>
                                                                            <th>Mode of pursuing PhD</th>
                                                                            <th id="header1" runat="server">Column 1</th>
                                                                            <th id="header2" runat="server">Column 2</th>       
                                                                            <th>Total Mark</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblusername" runat="server" Text='<% #Eval("USERNAME")%>' ToolTip='<%# Eval("USERNO")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblname" runat="server" Text='<% #Eval("STUDENT_NAME")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblbranch" runat="server" Text='<% #Eval("SHORTNAME")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblphdmode" runat="server" Text='<% #Eval("PHD_MOD_NAME")%>'></asp:Label>
                                                                        <asp:Label ID="lblmode" runat="server" Text='<% #Eval("PHD_MODE")%>' Visible="false"></asp:Label>
                                                                    </td>                
                                                                    <td>
                                                                        <asp:TextBox ID="TxtTestmark" runat="server" onblur="return validation_testmark(this)" CssClass="uppercase" Text='<% #Eval("TEST_MARK")%>' onkeyup="return UpdateTotalAndBalance(this)"
                                                                            MaxLength="4" Font-Bold="true" Style="text-align: center; font-size: small;" />
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TxtTestmark" ValidChars="0123456789AB." FilterMode="ValidChars">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtInterMarks" runat="server" onblur="return validation_interviewmark(this)" CssClass="uppercase" onkeyup="return UpdateTotalAndBalance(this)" Text='<% #Eval("INTERVIEW_MARK")%>'
                                                                            MaxLength="4" Font-Bold="true" Style="text-align: center; font-size: small;" />
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtInterMarks" ValidChars="0123456789AB." FilterMode="ValidChars">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtTotal" runat="server" Text='<% #Eval("TOTAL_MARK")%>' CssClass="uppercase" onkeyup="return UpdateTotalAndBalance(this)"
                                                                            MaxLength="4" Font-Bold="true" Style="text-align: center; font-size: small;" Enabled="false" />
                                                                        <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtTotal" ValidChars="0123456789." FilterMode="ValidChars">
                                                        </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                                <%--       </div>
                                                    </div>
                                                </div>--%>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnreport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="upDesig" runat="server" AssociatedUpdatePanelID="updDesignation">
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
                                            <asp:UpdatePanel ID="updDesignation" runat="server">
                                                <ContentTemplate>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Admission Batch</label>
                                                                        <asp:DropDownList ID="ddlAdmBatch1" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                                            ControlToValidate="ddlAdmBatch1" Display="None" SetFocusOnError="true" ValidationGroup="MARK" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-lg-2 col-md-6 col-12" runat="server" style="display: none">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Is Ph.D. Test Mark Entry</label>
                                                                    </div>
                                                                    <asp:CheckBox ID="chkMark" runat="server" OnCheckedChanged="chkMark_CheckedChanged" TabIndex="2" AutoPostBack="true" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Ph.D. Test Maximum Mark </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTestMark" runat="server" AutoComplete="off" AutoPostBack="true" CssClass="uppercase" TabIndex="2" ToolTip="Please Enter Maximum Test Mark" MaxLength="4" Font-Bold="true" Style="text-align: center; font-size: small;" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtTestMark" ValidChars="0123456789." FilterMode="ValidChars">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Ph.D. Test Maximum Mark"
                                                                        ControlToValidate="txtTestMark" Display="None" SetFocusOnError="true" ValidationGroup="MARK" />
                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-6 col-12" runat="server" style="display: none">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Is Ph.D. Interview Mark Entry</label>
                                                                    </div>
                                                                    <asp:CheckBox ID="chkInterview" runat="server" OnCheckedChanged="chkInterview_CheckedChanged" TabIndex="3" AutoPostBack="true" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Ph.D. Interview Maximum Mark </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtInterviewMark" runat="server" AutoComplete="off" AutoPostBack="true" CssClass="form-control" TabIndex="3" ToolTip="Please Enter Maximum Interview Mark" MaxLength="4" Font-Bold="true" Style="text-align: center; font-size: small;" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtInterviewMark" ValidChars="0123456789." FilterMode="ValidChars">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Ph.D. Interview Maximum Mark "
                                                                        ControlToValidate="txtInterviewMark" Display="None" SetFocusOnError="true" ValidationGroup="MARK" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnMarkSubmit" runat="server" Text="Submit" ToolTip="Submit"
                                                                CssClass="btn btn-primary" ValidationGroup="MARK" TabIndex="5" OnClick="btnMarkSubmit_Click" />

                                                            <asp:Button ID="btnEntryCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                CssClass="btn btn-warning" TabIndex="6" OnClick="btnEntryCancel_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="MARK"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>
                                                        <div class="col-12 mt-3">
                                                            <div class="table-responsive">
                                                                <asp:Panel ID="PnlCriteria" runat="server" Visible="false">
                                                                    <div class="sub-heading">
                                                                        <h5>Ph.D. Mark Entry Criteria</h5>
                                                                    </div>
                                                                    <asp:ListView ID="lvCriteria" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                                        <LayoutTemplate>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="DivlvCriteria">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Admission Batch
                                                                                        </th>
                                                                                        <th>Test Mark
                                                                                        </th>
                                                                                        <th>Interview Mark
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel runat="server" ID="updTemplate">
                                                                                <ContentTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <%# Eval("ADMBATCHNAME")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("TEST_MARK")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("INTERVIEW_MARK")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ContentTemplate>

                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvPhdMark$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvPhdMark$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script>
        function validation_testmark(txtMarks) {
            var txtMaxMark = document.getElementById('<%= hdftestmark.ClientID%>').value;
            if (txtMarks.value != "") {

                if (Number(txtMarks.value) > Number(txtMaxMark)) {
                    alert("Please Enter Marks Less than with Max Mark " + txtMaxMark + "");
                    txtMarks.value = '';
                    return;
                }
            }
        }
    </script>
    <script>
        function UpdateTotalAndBalance() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                list = 'LvPhdMark';
                var dataRows = document.getElementsByTagName('tr');
                var FinalAmount = 0;
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var testmark = 0;
                        var interview = 0;
                        testmark = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'TxtTestmark').value;
                        interview = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'TxtInterMarks').value;
                        if (testmark == null || testmark == 'NaN' || testmark == "" || testmark == "AB" || testmark == "ab") {
                            testmark = 0;
                        }
                        if (interview == null || interview == 'NaN' || interview == "" || interview == "AB" || testmark == "ab") {
                            interview = 0;
                        }
                        FinalAmount = parseFloat(interview) + parseFloat(testmark);
                        document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'TxtTotal').value = FinalAmount;
                    }
                }


            }
            catch (e) {
            }
        }
    </script>
    <script>
        function validation_interviewmark(txtMarks) {
            var txtMaxMark = document.getElementById('<%= hdfinterview.ClientID%>').value;
            if (txtMarks.value != "") {

                if (Number(txtMarks.value) > Number(txtMaxMark)) {
                    alert("Please Enter Marks Less than with Max Mark " + txtMaxMark + "");
                    txtMarks.value = '';
                    return;
                }
            }
        }
    </script>
</asp:Content>
