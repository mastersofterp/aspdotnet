<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BulkOperatorallotment.aspx.cs" Inherits="ACADEMIC_BulkOperatorallotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddetails"
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

    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="upddetails" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">EXAM MARK ENTRY OPERATOR ALLOCATION IN BULK</h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>

                                            <td style="width: 40%" rowspan="15" valign="top">
                                                <%-- <asp:UpdatePanel ID="updcour" runat="server"> <ContentTemplate>--%>
                                                <asp:Panel ID="pnlcour" runat="server" BorderColor="#CDCDCD"
                                                    BorderWidth="1px" Width="100%" ScrollBars="Vertical" Height="300 px" ToolTip="FeedBack Course List">

                                                    <asp:ListView ID="lvCurrentSubjects" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    Selected Semester Subjects
                                                                </div>
                                                                <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%; height: 50px">
                                                                    <thead>
                                                                        <tr class="header">
                                                                            <%--<th>
                                                    <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" onclick="SelectAll(this,1,'chkAccept');" />
                                                </th>--%>
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
                                                                            </th>
                                                                            <th>Course Code
                                                                            </th>
                                                                            <th width="45%">Course Name
                                                                            </th>


                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr id="trCurRow">
                                                                <%-- <td>
                                        <asp:CheckBox ID="chkAccept" runat="server"  ToolTip="Click to select this subject for registration" onclick="ChkHeader(1,'cbHead','chkAccept');" />
                                        <asp:HiddenField ID="hdnRegistered" runat="server" Value='<%# Eval("ACCEPTED") %>'/>
                                    </td>--%>
                                                                <td>
                                                                    <asp:CheckBox ID="chkRegister" runat="server" ToolTip="Click to select this subject for registration"
                                                                        onclick="ChkHeader(1,'cbHeadReg','chkRegister');" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                </td>
                                                                <td width="45%">
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                                </td>


                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>
                                                <%-- </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lvCurrentSubjects" />
                                           
                                        </Triggers>
                                        </asp:UpdatePanel>--%>
                                            </td>
                                            </tr>
                                                        <tr>
                                                            <td class="form_left_label">
                                                                <span class="validstar">*</span>College /School Name :
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot"
                                                                    AutoPostBack="True" Width="70%" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Degree :
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot"
                                                        AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Branch :
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot"
                                                        AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Scheme :
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" Width="30%"
                                                        ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Semester :
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Subject Type:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" TabIndex="7"
                                                        ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Course :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true"  TabIndex="8" ValidationGroup="teacherallot"
                                        Width="30%" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                        Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                                            <tr style="display: none">
                                                <td class="form_left_label">
                                                    <span class="validstar">&nbsp</span>Section :
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" TabIndex="9" ValidationGroup="teacherallot">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Internal/External :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlInEx" runat="server" AppendDataBoundItems="true" TabIndex="10" ValidationGroup="teacherallot">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Internal</asp:ListItem>
                                                        <asp:ListItem Value="2">External</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvInEx" runat="server" ControlToValidate="ddlInEx"
                                                        Display="None" ErrorMessage="Please Select Internal/External" InitialValue="0" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Select Operator
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlOprator" runat="server" AppendDataBoundItems="true" TabIndex="11" ValidationGroup="teacherallot">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Operator 1</asp:ListItem>
                                                        <asp:ListItem Value="2">Operator 2</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlOprator"
                                                        Display="None" ErrorMessage="Please Select Operator" InitialValue="0" ValidationGroup="teacherallot">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="form_left_label">&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox runat="server" ID="chkover" Text="Overwrite exist one" TabIndex="12" /></td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>User Type:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlTeachertype" runat="server" TabIndex="13" AppendDataBoundItems="true" Width="325px"
                                                        ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlTeachertype_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTeachertype"
                                                        Display="None" ErrorMessage="Please Select User Type" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="form_left_label">
                                                    <span class="validstar">*</span>Operator :
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlTeacher" runat="server" TabIndex="14" AppendDataBoundItems="true" Width="325px"
                                                        ValidationGroup="teacherallot">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                                        Display="None" ErrorMessage="Please Select Teacher" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="btnSave" runat="server" TabIndex="14" Text="Save" ValidationGroup="teacherallot"
                                                        Width="100px" OnClick="btnSave_Click" ToolTip="SAVE" />&nbsp;&nbsp;
                                      <asp:Button ID="btnReport" runat="server" Text="Print Report"
                                          Width="100px" ToolTip="Report" OnClick="btnReport_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnClear" runat="server" TabIndex="15" Text="Clear" Width="80px" OnClick="btnClear_Click" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="border-color: black">
                                                    <asp:Repeater ID="lvDetails" runat="server">
                                                        <HeaderTemplate>
                                                            <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                                                <thead>
                                                                    <tr class="header">

                                                                        <th align="left">College Name
                                                                        </th>
                                                                        <th align="left">Degree Name
                                                                        </th>

                                                                        <th align="left">Branch Name
                                                                        </th>
                                                                        <th align="left">Scheme Name
                                                                        </th>
                                                                        <th align="left">Semester
                                                                        </th>
                                                                        <th align="left">Course
                                                                        </th>
                                                                        <th align="left">Operator
                                                                        </th>
                                                                        <th align="left">Status
                                                                        </th>
                                                                        <th align="left">Operator name
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                    <thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">

                                                                <td>
                                                                    <%# Eval("COLLEGE_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DEGREENAME")%>
                                                                </td>

                                                                <td>
                                                                    <%# Eval("LONGNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("schemename") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("semesterNO") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COURSE_NAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("OPRTR") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STATUS") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("UA_FULLNAME") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody></table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                            </table>
                                                </fieldset>
                                        </div>
                                        <br />
                                        <%--STUDENT LIST--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnReport" />
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
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
        function SelectAll(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;
                    }
                    document.getElementById('<%=chkover.ClientID %>').checked = false;

                }

            }

            if (headchk.checked == true) {
                txtTot.value = hdfTot.value;
                txtCredits.value = hdfCredits.value;
            }
            else {
                txtTot.value = 0;
                txtCredits.value = 0;
            }
        }

    </script>

</asp:Content>
