<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LockMarksBySchemeSelectedStud.aspx.cs" Inherits="ACADEMIC_LockMarksBySchemeSelectedStud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                            <%-- <h3 class="box-title">--%>
                            <%--<asp:Label ID="lblDYmarkentryunlock" runat="server"></asp:Label>--%>
                            <h3 class="box-title"><b>Student Wise Unlock</b></h3>
                        </div>



                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                           ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true"
                                            runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="save" ></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" data-select2-enable="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" CssClass="form-control" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" TabIndex="1" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection" Display="None" ErrorMessage="Please Select Section" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlsub" runat="server" AppendDataBoundItems="True" data-select2-enable="true" CssClass="form-control" OnSelectedIndexChanged="ddlsub_SelectedIndexChanged" TabIndex="1" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsub" runat="server" ControlToValidate="ddlsub" Display="None" ErrorMessage="Please Select Subject Type" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1" ValidationGroup="show" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamType" runat="server" ControlToValidate="ddlExamType" Display="None" ErrorMessage="Please Select Exam Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvExamType0" runat="server" ControlToValidate="ddlExamType" Display="None" ErrorMessage="Please Select Exam Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="LockAll"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>SubExam Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubExam1" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1" ValidationGroup="show" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubExam_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubExam1" Display="None" ErrorMessage="Please Select Sub-Exam Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubExam1" Display="None" ErrorMessage="Please Select Sub-Exam Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="LockAll"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" data-select2-enable="true" ValidationGroup="show" CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="1" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse" Display="None" ErrorMessage="Please Select Course" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStuType" runat="server" AppendDataBoundItems="True" data-select2-enable="true" ValidationGroup="show" CssClass="form-control" OnSelectedIndexChanged="ddlStuType_SelectedIndexChanged" TabIndex="1" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStuType" runat="server" ControlToValidate="ddlStuType" Display="None" ErrorMessage="Please Select Student Type" ValidationGroup="save" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty </label>
                                        </div>
                                        <asp:DropDownList ID="ddlFac" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" ValidationGroup="show" OnSelectedIndexChanged="ddlFac_SelectedIndexChanged" TabIndex="1" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFac" runat="server" ControlToValidate="ddlFac" Display="None" ErrorMessage="Please Select Faculty" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-12 col-12" runat="server" id="spnNote" visible="false">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked=<span style="color: green; font-weight: bold">Lock</span></span>  </p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked=<span style="color: green; font-weight: bold">UnLock</span></span>  </p>
                                        </div>

                                    </div>
                                     
                                          
                                       <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="activityname" visible="false">
                                        <div class="label-dynamic">
                                         <label>Activity Name </label>
                                        </div>
                                        <asp:Label ID="lblActivity" ForeColor="red" Font-Bold="true"  runat="server"  textFont-Bold="true"></asp:Label>
                                     </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="activitystart" visible="false">
                                        <div class="label-dynamic">
                                         <label>Start Date </label>
                                        </div>
                                        <asp:Label ID="lblstart" ForeColor="red" Font-Bold="true"  runat="server" textFont-Bold="true"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12"  runat="server" id="activityend" visible="false">
                                        <div class="label-dynamic">
                                         <label>End Date </label>
                                        </div>
                                        <asp:Label ID="lblEnd" ForeColor="red" Font-Bold="true" runat="server" textFont-Bold="true"></asp:Label>
                                    </div>
                                      
                                    

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Font-Bold="False" OnClick="btnShow_Click" TabIndex="1" Text="Show" ValidationGroup="save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSave" runat="server" Visible="false" Font-Bold="False" OnClick="btnSave_Click" TabIndex="1" Text="Unlock" ValidationGroup="save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="1" Text="Cancel" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummaryShow" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="save" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="LockAll" />

                                <asp:ValidationSummary ID="ValidationSummaryShow0" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Status" />
                            </div>


                            <div class="col-12">
                                <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudList" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Student for Unlock Marks</h5>
                                            </div>
                                            <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <%--<th>Sr.No.</th>--%>
                                                            <th>Lock</th>
                                                            <th>Enroll. No.</th>
                                                            <th>Student Name</th>
                                                            <th>Lock Status</th>
                                                            <th>Marks</th>
                                                            <th>Exam Type</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                                </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>--%>
                                                <td>
                                                    <asp:CheckBox ID="chklckStud" runat="server" TabIndex="1" ToolTip='Checked means locked' />
                                                    <asp:HiddenField ID="hdnChkBoxStud" runat="server" Value='<%# Eval ("LOCK") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUD_NAME")%>'> </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLockStatus" runat="server" Text='<%# Eval("LOCK")%>'> </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblmarkEnrySts" runat="server" Text='<%# Eval("MARK")%>'> </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Exam1" runat="server" Text='<%# Eval ("EXAM") %>' ToolTip='<%# Eval("EXAMCODE")%>'></asp:Label>
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

    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
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

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
