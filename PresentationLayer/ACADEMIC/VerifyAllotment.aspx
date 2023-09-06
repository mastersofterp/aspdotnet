<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VerifyAllotment.aspx.cs" Inherits="ACADEMIC_BacklogCourseTeacherAllot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><%--COURSE TEACHER ALLOTMENT BULK--%>
                         <asp:Label ID="lblVerifyAllot" runat="server">Verify Allotment</asp:Label>
                        <%--<asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>--%>
                          <%--<lable style="font-size:20px; font-family:Verdana;">Verify Allotment</lable>--%>
                    </h3>
                </div>

                <div class="box-body">

                    <div>
                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel1"
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
                    <asp:UpdatePanel ID="updPanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="divCourses" runat="server">
                                <div class="col-12 mt-2">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">

                                            <div class="label-dynamic">
                                                <span style="color: red;">*</span>
                                               <%-- <label><span style="color: red;">*</span>College & Scheme</label>--%>
                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>


                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                                ValidationGroup="teacherallot" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvstudent" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select College & Scheme." ValidationGroup="teacherallot"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <span style="color: red;">*</span>
                                           <%--   <label><span style="color: red;">*</span>Session</label>--%>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true"  OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." ValidationGroup="teacherallot"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <span style="color: red;"></span>
                                            <%--  <label><span style="color: red;"></span>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Semester." ValidationGroup="teacherallot"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show" OnClick="btnShow_Click" ValidationGroup="teacherallot" />                                  
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" ValidationGroup="teacherallot"/>
                                     <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />


                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvBacklogCourse" runat="server" OnItemDataBound="lvBacklogCourse_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>BACKLOG COURSES</h5>
                                            </div>
                                           <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tableid">--%>
                                             <table class="table table-striped" style="width: 100%" id="tableid">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" ToolTip="Select all" runat="server" AutoPostBack="false" onclick="selectAll(this),EnableTextBox(this);"/>
                                                        </th>

                                                        <th>
                                                            <asp:Label ID="lblDYddlCourse_Tab" runat="server" Font-Bold="true"></asp:Label></th>

                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label></th>

                                                        <th>Teacher</th>
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
                                                    <asp:CheckBox ID="chkCourseno" runat="server" ToolTip='<%# Eval("COURSENO")%>' OnCheckedChanged="chkCourseno_CheckedChanged" onclick="EnableTextBox(this);"/>
                                                       
                                                </td>
                                                <td>
                                                    <%# Eval("COURSENAME")%>
                                                 <asp:HiddenField ID="hdnCourseCode" runat="server" Value='<%# Eval("CCODE")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNO")%>
                                                    <asp:Label ID="lblSemesterNo" runat="server" Tooltip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnSemesterno" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                </td>
                                                <td>

                                                    <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdntecher" runat="server" />
                                                     <asp:HiddenField ID="hdnuano" runat="server" Value='<%#Eval("UA_NO")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyItemTemplate>
                                            <p>No record found! </p>
                                        </EmptyItemTemplate>
                                    </asp:ListView>


                                </div>

                            </div>

                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>


            </div>
        </div>
    </div>

    <script>
        function selectAll(chk) {
            var totalCheckboxes = $('[id*=tableid] td input:checkbox').length;
            //alert(totalCheckboxes);
            for (var i = 0; i < totalCheckboxes; i++) {
                if (chk.checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvBacklogCourse_ctrl" + i + "_chkCourseno").checked = true;
                }
                else
                    document.getElementById("ctl00_ContentPlaceHolder1_lvBacklogCourse_ctrl" + i + "_chkCourseno").checked = false;
            }
        }

        
        //Ebnable text box sequence
        function EnableTextBox(txt) {
            try {

                debugger;

                var tbl = document.getElementById('tableid');
                var dataRows = tbl.getElementsByTagName('tr');
                var lv = 'lvBacklogCourse';
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var chkroom = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_chkCourseno';
                        var txtRoomId = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_ddlTeacher';
                    
                        if (document.getElementById(chkroom).checked == true) {
                            document.getElementById(txtRoomId).disabled = false;
                        }
                        else {
                            document.getElementById(txtRoomId).disabled = true;
                        }
                    }

                }
            }

            catch (e) {
                alert(e);
            }
        }

    </script>

    <%--<script>
        $(document).ready(function () {
            //$('#tableid_filter').DataTable({
            $('.dataTables_filter').DataTable({
                columnDefs: [
                    {
                        target: 2,
                        visible: false,
                        searchable: false,
                    },
                    {
                        target: 3,
                        visible: false,
                    },
                ],
            });
        });

      
    </script>--%>

</asp:Content>

