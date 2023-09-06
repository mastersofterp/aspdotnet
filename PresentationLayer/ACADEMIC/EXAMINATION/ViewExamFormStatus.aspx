<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="ViewExamFormStatus.aspx.cs" Inherits="ACADEMIC_ViewExamFormStatus" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
   <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="updExam" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">APPROVAL/VIEW EXAM FORM STATUS</h3>
                            </div>
                            <div class="box-body" id="divInfo" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>School/Institute Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDept" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepartment"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Programme/Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="5" ValidationGroup="teacherallot" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Scheme</label>
                                            </div>
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="6"
                                                ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="7" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Student Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudType" runat="server" AppendDataBoundItems="true" TabIndex="8" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="0">Regular</asp:ListItem>
                                                <asp:ListItem Value="1">Backlog</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlStudType"
                                                Display="None" InitialValue="-1" ErrorMessage="Please Select Student Type" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label></td>
                                </div>
                                <div class="col-12 btn-footer" id="divFooter" runat="server">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="9" Text="Show " ValidationGroup="teacherallot"
                                        ToolTip="Show data" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSubmit" runat="server" Enabled="false" TabIndex="10" Text="Exam Form Approval" ValidationGroup="teacherallot"
                                        ToolTip="Exam Form Approval" OnClick="btnSubmit_Click" CssClass="btn btn-primary" Visible="true" />

                                    <asp:Button ID="btnExcel" runat="server" Enabled="false" TabIndex="11" Text="View Exam Form Status(Excel)" ValidationGroup="teacherallot"
                                        ToolTip="Bulk Exam Registration" OnClick="btnExcel_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSlip" runat="server" CssClass="btn btn-info" Text="Bulk Registration Slip" Enabled="false" TabIndex="12"
                                        ValidationGroup="teacherallot" OnClick="btnSlip_Click" Visible="false" />

                                    <asp:Button ID="btnClear" runat="server" TabIndex="13" Text="Clear" OnClick="btnClear_Click" ToolTip="Clear" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudents" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table id="tblStudents" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />
                                                                Select</th>
                                                            <th>Reg No. </th>
                                                            <th>Name </th>
                                                            <th>HOD Approval Status</th>
                                                            <th>Student Exam Form Fillup Status</th>

                                                            <th>Exam Form Confirmation Status</th>
                                                            <th>Student Admit Card Download  Status </th>
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
                                                        <asp:CheckBox ID="chkRow" runat="server"
                                                            onclick="CountSelection();"
                                                            Enabled='<%# (Convert.ToInt32(Eval("INCH_EXAM_REG") )== 0 ? true : false )%>' />
                                                    </td>
                                                    <td><%# Eval("REGNO")%>
                                                        <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                        <asp:HiddenField ID="hdnstatus" runat="server" Value='<%# Eval("exam_registered")%>' />
                                                    </td>
                                                    <td><%# Eval("STUDNAME")%></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label2" Text='<%# (Convert.ToInt32(Eval("INCH_EXAM_REG") )== 1 ?  "Done" : "Pending" )%>'
                                                            ForeColor='<%# (Convert.ToInt32(Eval("INCH_EXAM_REG") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblStatus" Text='<%# (Convert.ToInt32(Eval("STUD_EXAM_REGISTERED") )== 1 ?  "Done" : "Pending" )%>'
                                                            ForeColor='<%# (Convert.ToInt32(Eval("STUD_EXAM_REGISTERED") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label runat="server" ID="lblConfirmStatus" Text='<%# (Convert.ToInt32(Eval("EXAM_REGISTERED") )== 1 ?  "Done" : "Pending" )%>'
                                                            ForeColor='<%# (Convert.ToInt32(Eval("EXAM_REGISTERED") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# (Convert.ToInt32(Eval("STUD_GENERATE") )== 1  ?  "Done" : "Pending" )%>'
                                                            ForeColor='<%# (Convert.ToInt32(Eval("STUD_GENERATE") )== 1  ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
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
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExcel" />
                <asp:PostBackTrigger ControlID="btnShow" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (e.disabled == false) {
                            e.checked = true;
                        }
                        else {
                            e.checked = false;
                        }
                    }

                    else {
                        e.checked = false;
                    }
                }
            }
        }
    </script>

</asp:Content>
