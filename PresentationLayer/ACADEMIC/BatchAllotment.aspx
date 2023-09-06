<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BatchAllotment.aspx.cs" Inherits="ACADEMIC_BatchAllotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
     <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
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

    <%--<asp:Panel ID="pnlMain" runat="server">--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">

                        <div class="box-header with-border">
                            <%--<h3 class="box-title">BATCH ALLOTMENT</h3>--%>
                            <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                             <sup>* </sup>
                                             <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                             <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlClgname" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme." ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                         </div>
                                     </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="2" 
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12"  style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <%-- <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:ListBox ID="ddlDepartment" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                            CssClass="form-control multi-select-demo" AppendDataBoundItems="true" AutoPostBack="true"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvdepartment" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" ToolTip="Please Select School/Institute" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/ Institute Name" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>
                                    

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                          TabIndex="3"  ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Subject Type</label>--%>
                                             <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                            CssClass="form-control" data-select2-enable="true" ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                         TabIndex="5"   ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                         TabIndex="6"   ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection" Enabled="false"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="trFilter" runat="server">
                                            <sup>* </sup>
                                            <label>Filter By</label>
                                        </div>
                                        <div id="trRollNo" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>By Roll No</label>
                                            </div>
                                            <asp:TextBox ID="txtFromRollNo" runat="server" CssClass="form-control" />
                                            To
                                            <asp:TextBox ID="txtToRollNo" runat="server" CssClass="form-control" />
                                            <asp:CompareValidator ID="svFromTo" runat="server" ControlToCompare="txtFromRollNo"
                                                ControlToValidate="txtTotStud" Display="None" ErrorMessage="Please Valid Range"
                                                Operator="LessThanEqual" Type="Integer" ValidationGroup="teacherallot"></asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trRdo" runat="server">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remaining Students" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" ValidationGroup="teacherallot" TabIndex="10"
                                    OnClick="btnFilter_Click" CssClass="btn btn-primary" OnClientClick="return validation();"/>
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-warning" OnClick="btnClear_Click" TabIndex="11" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dvAttFor" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Attendance For Tutorial/Practical</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAttfor" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherassign"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="1">Tutorial</asp:ListItem>
                                                <asp:ListItem Value="2">Practical</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAttfor" runat="server" ControlToValidate="ddlAttfor"
                                            Display="None" ErrorMessage="Please Select Attendance For Tutorial/Practical" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Serial No. From</label>
                                        </div>
                                        <div class="form-inline">
                                            <asp:TextBox ID="txtEnrollFrom" runat="server" CssClass="form-control" ValidationGroup="EnrollText" Enabled="false" TabIndex="7" />
                                            <asp:RequiredFieldValidator ID="rfvEnrollFrom" runat="server" ControlToValidate="txtEnrollFrom" 
                                                Display="None" ErrorMessage="Please Select Registration No From Range" ValidationGroup="EnrollText"></asp:RequiredFieldValidator>

                                            &nbsp;&nbsp;
                                            <label>To</label>
                                            &nbsp;&nbsp;

                                            <asp:TextBox ID="txtEnrollTo" runat="server" CssClass="form-control" ValidationGroup="EnrollText" Enabled="false" TabIndex="8" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollTo"
                                                Display="None" ErrorMessage="Please Select Registration No To Range" ValidationGroup="EnrollText"></asp:RequiredFieldValidator>&nbsp;&nbsp;

                                            <asp:Button ID="btnConfirm" runat="server" TabIndex="9" CssClass="btn btn-primary" OnClick="btnConfirm_Click" Text="Confirm Students" ValidationGroup="EnrollText" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherassign"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                            Display="None" ErrorMessage="Please Select Batch" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="teacherassign"
                                            class="btn btn-primary" OnClick="btnSave_Click" />
                                        <%--OnClientClick="return validateAssign();"--%>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherassign"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Total Selected Students </label>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" Enabled="false" CssClass="form-control" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStatus2" runat="server" SkinID="Errorlbl" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="sub-heading" id="divlvStudentHeading" runat="server" visible="false">
                                    <h5>Student List</h5>
                                </div>
                                <asp:Panel ID="pnlStudent" runat="server">
                                    <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                        </th>
                                                        <th><asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Tutorial Batch
                                                        </th>
                                                        <th>Practical Batch
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
                                                    <asp:CheckBox ID="cbRow" runat="server" Text='<%# Container.DataItemIndex + 1 %>' onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ROLLNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TH_BATCHNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PR_BATCHNAME")%>
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
            <%-- <asp:PostBackTrigger ControlID="ddlCollege" />
                <asp:PostBackTrigger ControlID="ddlBranch" />
                <asp:PostBackTrigger ControlID="ddlScheme" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <%-- </asp:Panel>--%>

    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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

            if (headchk.checked == true)
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

        if (txtTot == 0) {
            alert('Please Select at least one student from student list');
            return false;
        }
        else
            return true;
    }

    </script>
    <script>
        $(document).ready(function () {
            $(".multi-select-demo").multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $(".multi-select-demo").multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });

    </script>

    <script>
        function validation() {
            var ddlClgScheme = $('[id*=ctl00_ContentPlaceHolder1_ddlClgname]').val();
            var ddlSession = $('[id*=ctl00_ContentPlaceHolder1_ddlSession]').val();
            var ddlSemester = $('[id*=ctl00_ContentPlaceHolder1_ddlSemester]').val();
            var ddlSubjectType = $('[id*=ctl00_ContentPlaceHolder1_ddlSubjectType]').val();
            var ddlCourse = $('[id*=ctl00_ContentPlaceHolder1_ddlCourse]').val();
            var ddlSection = $('[id*=ctl00_ContentPlaceHolder1_ddlSection]').val();
            var lblClgSchem = $('[id*=ctl00_ContentPlaceHolder1_lblDYddlColgScheme]').text();
            var lblSession = $('[id*=ctl00_ContentPlaceHolder1_lblDYddlSession]').text();
            var lblSemester = $('[id*=ctl00_ContentPlaceHolder1_lblDYddlSemester]').text();
            var lblCoursetype = $('[id*=ctl00_ContentPlaceHolder1_lblDYddlCourseType]').text();
            var lblCourse = $('[id*=ctl00_ContentPlaceHolder1_lblDYddlCourse]').text();
            var lblSection= $('[id*=ctl00_ContentPlaceHolder1_lblDYddlSection]').text();
            var msg = "";
           
            if (ddlClgScheme == 0) {
                msg += 'Please Select ' + lblClgSchem + '.\n';
            }
            if (ddlSession == 0) {
                msg += 'Please Select ' + lblSession + '.\n';
            }
            if (ddlSemester == 0) {
                msg += 'Please Select ' + lblSemester + '.\n';
            }
            if (ddlSubjectType == 0) {
                msg += 'Please Select ' + lblCoursetype + '.\n';
            }
            if (ddlCourse == 0) {
                lblCourse = lblCourse.replace("TypeCourse", "");
                msg += 'Please Select ' + lblCourse + '.\n';
            }
            if (ddlSection == 0 || ddlSection == "") {
                msg += 'Please Select ' + lblSection + '.\n';
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else
                return true;

        }
    </script>

</asp:Content>
