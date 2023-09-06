<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TeacherAllotment_Reval.aspx.cs" Inherits="ACADEMIC_teacherallotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
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
                            <%--<h3 class="box-title">TEACHER STUDENT ALLOTMENT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none"  style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlClgname" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                         <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme." ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div> 

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlColgSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="2" AppendDataBoundItems="true" Font-Bold="True" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem>Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>School/Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitute" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                               <%--         <asp:RequiredFieldValidator ID="rfvSchoolInstitute" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" InitialValue="0" Visible="false" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" InitialValue="0" Visible="false" ErrorMessage="Please Select School/Institute Name" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                     
   
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none"  style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    <%--    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="Report">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                     
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Course</label>--%>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                           

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                <%--        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Teacher</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="5"
                                            ValidationGroup="teacherallot" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                            Display="None" ErrorMessage="Please Select Teacher" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                                    </div>
                                    <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Total Selected Students</label>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" Enabled="false"></asp:TextBox>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dvTH_Batch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Th Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlThbatch" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlThbatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlThbatch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Batch" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remaining Students" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" ValidationGroup="teacherallot"
                                    OnClick="btnFilter_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="teacherassign"
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" ValidationGroup="Report" Visible="false" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Report"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="teacherassign" />
                            </div>

                            <div class="col-12" id="divlvStudDetails" runat="server" >
                                <div class="sub-heading" id="divlvStudentHeading" runat="server" visible="false">
                                    <h5>Student List</h5>
                                </div>
                                <asp:Panel ID="Panel1" runat="server">

                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudents">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)"  />
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Teacher Name
                                                        </th>
                                                       <%-- <th>Teacher Name [PR]
                                                        </th>
                                                        <th>Teacher Name [TU]
                                                        </th>--%>
                                                     <%--   <th>
                                                            <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
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
                                                <td>
                                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totStud(this);" ToolTip='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ROLLNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SECTIONNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                </td>
                                            <%--    <td>
                                                    <%# Eval("UA_FULLNAME_PRAC")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME_TUTR")%>
                                                </td>--%>
                                              <%--  <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="ddlDepartment" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk)
        {
            var frm = document.forms[0]
            var count = 0;
            for (i = 0; i < document.forms[0].elements.length; i++)
            {
               // alert("check1");
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                    {
                        e.checked = true;
                        //count++;
                    }
                    else
                        e.checked = false;
                }
            }
            if (headchk.checked == true) {
                count = $('[id*=tblStudents] td').closest("tr").length;
            }
            document.getElementById('<%=txtTotStud.ClientID%>').value = count;
        }


    </script>
    <script>
        function totStud() {
            var numberToChecked = $('[id*=tblStudents] td input:checkbox:checked').length;
            document.getElementById('<%=txtTotStud.ClientID%>').value = numberToChecked;
            //var frm = document.forms[0]
            //var count = 0;
            //alert(document.forms[0].elements.length);
            //for (i = 0; i < document.forms[0].elements.length; i++) {
            //    var e = frm.elements[i];
            //    if (e.type == 'checkbox') {
            //        if (e.checked == true) {
            //            count++;

            //        }
            //        else {
            //            e.checked = false;
            //        }
            //        //alert(count);
            //    }
            //}
            //alert(count);
            //document.getElementById('<%=txtTotStud.ClientID%>').value = count;
        }
    </script>
    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });
    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>
