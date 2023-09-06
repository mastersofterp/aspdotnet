<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CancelExamReg.aspx.cs" Inherits="ACADEMIC_EXAMINATION_CancelExamReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnl"
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


    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Cancel Exam Registration</h3>
                    </div>
                    <div class="box-body">
                        <div class="nav-tabs-custom" id="Tabs" role="tabpanel">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_1" id="tab1">Cancel Single Students Exam Registration</a>

                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2">Cancel Bulk Students Exam Registration</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="my-tab-content">
                                <div class="tab-pane active" id="tab_1">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updExamSingle"
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
                                    <asp:UpdatePanel ID="updExamSingle" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div>
                                                        <div class="box-body">
                                                            <div id="div3" runat="server"></div>
                                                            <div id="divSingle" runat="server">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                                <b>&</b>
                                                                                <asp:Label ID="lblDYddlSession_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlSessionSingle" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSessionSingle_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvalidator7" runat="server" ControlToValidate="ddlSessionSingle" ErrorMessage="Please Select Session" Display="None" InitialValue="0" ValidationGroup="ShowSingle" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%--<label>Semester</label>--%>
                                                                                <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlSemesterSingle" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemesterSingle_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSemesterSingle" ErrorMessage="Please Select Semester" Display="None" InitialValue="0" ValidationGroup="ShowSingle" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRegNo" ErrorMessage="Please Enter Registration No" Display="None" ValidationGroup="ShowSingle" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Remark</label>
                                                                            </div>
                                                                            <asp:TextBox ID="txtRemark_Single" runat="server" CssClass="form-control" TabIndex="4" MaxLength="50"></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" InvalidChars="~`!@#$%^&*()_-+={[}]|\:;'<,>?/"
                                                                                FilterMode="InvalidChars" TargetControlID="txtRemark_Single">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12 btn-footer">
                                                                    <asp:Button ID="btnShowSingle" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="ShowSingle" TabIndex="5" OnClick="btnShowSingle_Click" />
                                                                    <asp:Button ID="btnSubmitSingle" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="ShowSingle" TabIndex="17" Enabled="false" OnClientClick="return ConfirmMessage(this,'val');" OnClick="btnSubmitSingle_Click" />
                                                                    <asp:Button ID="btnCancelSingle" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" TabIndex="18" OnClick="btnCancelSingle_Click" />
                                                                    <asp:ValidationSummary ID="validSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ShowSingle" />
                                                                </div>
                                                            </div>
                                                            <div class="col-12" id="divSubjectsList" runat="server" visible="false">
                                                                <asp:Panel ID="pnlSubjects" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvSubjects" runat="server" Visible="true">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Subjects List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tbllist">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center">Select
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll_Single(this);" />
                                                                                        </th>
                                                                                        <th style="text-align: center">Sr.No.</th>
                                                                                        <th>Subject Code</th>
                                                                                        <th>Subject Name</th>
                                                                                        <th style="text-align: center">Status</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center;width: 5%">
                                                                                    <asp:CheckBox ID="chkSubject" runat="server" ToolTip='<%#Eval("COURSENO")%>' />
                                                                                    <asp:HiddenField ID="hdnCancel" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' />
                                                                                </td>
                                                                                <td style="text-align: center;width: 5%;">
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </td>
                                                                                <td style="width: 20%">
                                                                                    <asp:Label ID="lblCcode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 25%">
                                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SCHEMENO") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align:center; width: 10%">
                                                                                    <asp:Label runat="server" ID="lblStatusSub"></asp:Label>
                                                                                </td>
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
                                    </asp:UpdatePanel>

                                </div>

                                <div class="tab-pane fade" id="tab_2">
                                    <div>
                                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExam"
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

                                    <asp:UpdatePanel ID="updExam" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div>
                                                        <div id="div1" runat="server"></div>
                                                        <%--<div class="box-header with-border">
                                                            <h3 class="box-title">Cancel Exam Registration</h3>
                                                        </div>--%>
                                                        <div class="box-body">
                                                            <div id="divMultiple" runat="server">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%--  <label>College & Scheme</label>--%>
                                                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                                                ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                                                Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="Show">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%--<label>Session</label>--%>
                                                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2" ValidationGroup="Show" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true" ErrorMessage="Please Select Session" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSession" ErrorMessage="Please Select Session" Display="None" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divdepartment" runat="server">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%--<label>Department Name</label>--%>
                                                                                <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="3" ValidationGroup="Show" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" SetFocusOnError="true" ControlToValidate="ddlDepartment" ErrorMessage="Please Select Department Name" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%--<label>Semester</label>--%>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="4" ValidationGroup="Show" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true" ControlToValidate="ddlSemester" ErrorMessage="Please Select Semester" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%--<label>Subject</label>--%>
                                                                                <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="5" ValidationGroup="Show" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList><br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="true" ControlToValidate="ddlSubject" ErrorMessage="Please Select Subject" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Remark</label>
                                                                            </div>
                                                                            <asp:TextBox ID="txtRemark_Multiple" runat="server" CssClass="form-control" TabIndex="6" MaxLength="50"></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" InvalidChars="~`!@#$%^&*()_-+={[}]|\:;'<,>?/"
                                                                                FilterMode="InvalidChars" TargetControlID="txtRemark_Multiple">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12 btn-footer">
                                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="7" ValidationGroup="Show" OnClick="btnShow_Click" />
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="8" ValidationGroup="Show" OnClick="btnSubmit_Click" Enabled="false" OnClientClick="return ConfirmMessageMult(this,'val');" />
                                                                    <%--<asp:Button ID="btnExcel" runat="server" Text="I Grade Entry Report(Excel)" TabIndex="19" ValidationGroup="Report" OnClick="btnExcel_Click" CssClass="btn btn-primary" Visible="false" />--%>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="9" CausesValidation="false" OnClick="btnCancel_Click" />
                                                                    <asp:ValidationSummary ID="validSummary" runat="server" ValidationGroup="Show" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Report" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvStudents" runat="server" Visible="true">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Students List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center;">Select
                                                            <asp:CheckBox ID="chkMulAll" runat="server" onclick="SelectAll(this);" />
                                                                                        </th>
                                                                                        <th>Sr.No.</th>
                                                                                        <th>Registration No</th>
                                                                                        <th>Student Name</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align:center;">
                                                                                    <asp:CheckBox ID="chkSelect" runat="server"  ToolTip='<%#Eval("IDNO")%>' />
                                                                                    <%--<asp:HiddenField ID="hdnCancel" runat="server" Value='<%# Eval("CANCEL") %>' />--%>
                                                                                </td>
                                                                                <td style="text-align: center">
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("REGNO") %>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("STUDNAME") %>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:HiddenField ID="hdncount" runat="server" Value="0" />
                                                                <div id="divMsg" runat="server">
                                                                </div>
                                                            </div>
                                                        </div>
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

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function ConfirmMessage() {
            var ret = confirm('Are you sure to cancel the Exam Registration for the student?');
            if (ret == true)
                return true;
            else
                return false;
        }
        function ConfirmMessageMult() {
            var ret = confirm('Are you sure to cancel the Exam Registration of selected students?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function SelectAll_Single(chk) {

            var hftot = document.getElementById('<%= hdncount.ClientID %>');
            //alert(hftot.value);
            for (i = 0; i < hftot.value; i++) {
                // alert(hftot);
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvSubjects_ctrl' + i + '_chkSubject');

                //alert(lst.id);
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        // hftot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        //txtTot.value = 0;
                    }
                }

            }
        }

    </script>
    <script type="text/javascript">
        function SelectAll(chkAll) {
            var val = document.getElementById('<%=hdncount.ClientID%>');
                    <%--var hftot = document.getElementById('<%= hdncount.ClientID %>');--%>
            //alert(val.value);
            //alert('a');
            for (i = 0; i < val.value; i++) {
                // alert(hftot);
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_chkSelect');

                //alert(lst.id);
                if (lst.type == 'checkbox') {
                    if (chkAll.checked == true) {
                        lst.checked = true;
                        // hftot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        //txtTot.value = 0;
                    }
                }

            }
        }
    </script>
</asp:Content>

