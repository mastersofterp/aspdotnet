<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="ModificationExamRegStatus.aspx.cs" Inherits="ACADEMIC_ModificationExamRegStatus" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updExam"
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
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="updExam" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>BULK EXAM FORM CONFIRMATION</b></h3>
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True" CssClass="form-control" data-select2-enable="true">
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
                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
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
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
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
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5"
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
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot" data-select2-enable="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label></td>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnShow" runat="server" TabIndex="7" Text="Show " ValidationGroup="teacherallot"
                                        ToolTip="Show data" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSubmit" runat="server" Enabled="false" TabIndex="8" Text="Bulk Exam Form Confirmation" ValidationGroup="teacherallot"
                                        ToolTip="Bulk Exam Registration" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSlip" runat="server" CssClass="btn btn-info" Text="Bulk Registration Slip" Enabled="false"
                                        ValidationGroup="teacherallot" OnClick="btnSlip_Click" />

                                    <asp:Button ID="btnClear" runat="server" TabIndex="9" Text="Clear" OnClick="btnClear_Click" ToolTip="Clear" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvStudents" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />
                                                                Select Exam Reg. Status </th>
                                                            <th>Reg No. </th>
                                                            <th>Name </th>
                                                            <th>Student Exam Form Fillup Status</th>
                                                            <th>Exam Form Type</th>
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
                                                        <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" ForeColor='<%# (Convert.ToInt32(Eval("exam_registered") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'
                                                            onclick="CountSelection();" Text='<%# (Convert.ToInt32(Eval("exam_registered") )== 1 ?  "Confirmed" : "Not Confirmed" )%>' ToolTip='<%# (Convert.ToInt32(Eval("exam_registered") )== 1 ?  "Select Status for Not Confirm" : "Select Status for Confirm" )%>'
                                                            Enabled='<%# (Convert.ToInt32(Eval("STUD_EXAM_REGISTERED") )== 1 ? true : false )%>' />
                                                    </td>
                                                    <td><%# Eval("REGNO")%>
                                                        <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                        <asp:HiddenField ID="hdnstatus" runat="server" Value='<%# Eval("exam_registered")%>' />
                                                    </td>
                                                    <td><%# Eval("STUDNAME")%></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblStatus" Text='<%# (Convert.ToInt32(Eval("STUD_EXAM_REGISTERED") )== 1 ?  "Done" : "Pending" )%>'
                                                            ForeColor='<%# (Convert.ToInt32(Eval("STUD_EXAM_REGISTERED") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" Font-Bold="true" ID="lblExamFormStatus" Text='<%# (Convert.ToInt32(Eval("PREV_STATUS") )== 1 ?  "BACKLOG" : "REGULAR" )%>'
                                                            ForeColor="Red"></asp:Label>
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
                <%--STUDENT LIST--%>
            </ContentTemplate>
            <Triggers>
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
