<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ChangeCourseteacher.aspx.cs" Inherits="ACADEMIC_ChangeCourseteacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("../plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                    enableHTML: true,
                    templates: {
                        filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                        filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                    }
                    //dropRight: true,
                    //search: true,
                });

            });
        });
    </script>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
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
                    <asp:UpdatePanel ID="updPanel1" runat="server">
                        <ContentTemplate>
                            <div id="divCourses" runat="server">
                                <div class="col-12 mt-2">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
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
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                                AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." ValidationGroup="teacherallot"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="ddlSemester" runat="server" TabIndex="2" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:ListBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester." ValidationGroup="teacherallot"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="ddlCourse" runat="server" TabIndex="2" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:ListBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCourse"
                                                Display="None" ErrorMessage="Please Select Course." ValidationGroup="teacherallot"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>



                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" TabIndex="4" Text="Show Current Faculty" OnClick="btnShow_Click" ValidationGroup="teacherallot" />
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="4" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                        OnClientClick="return fnConfirmDelete();" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" CausesValidation="false" TabIndex="4" Text="Cancel" OnClick="btnCancel_Click" />


                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <asp:Label ID="lblFaculty" runat="server" Text="Faculty" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlFaculty" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlFaculty"
                                                Display="None" ErrorMessage="Please Select Faculty." ValidationGroup="Submit"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TextMode="MultiLine" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRemark"
                                                Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="Submit" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="pull-right">
                                    <div style="color: Red; font-weight: bold; padding-top: 10px">
                                        &nbsp;&nbsp;&nbsp;Note : If you change course teacher allotement it will reflect on TimeTable,Attendance and Mark Entry Related.
                                    </div>
                                    <br />
                                    <br />
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvBacklogCourse" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>COURSES</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tableid">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" TabIndex="5" ToolTip="Select all" runat="server" AutoPostBack="false" onclick="selectAll(this);" />
                                                        </th>

                                                        <th>
                                                            <asp:Label ID="lblDYddlCourse_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label></th>

                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label></th>

                                                        <th>Current Faculty</th>
                                                        <th hidden>New Faculty</th>
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
                                                    <asp:CheckBox ID="chkCourseno" TabIndex="5" runat="server" ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("COURSENAME")%>
                                                    <asp:HiddenField ID="hdnCourseCode" runat="server" Value='<%# Eval("CCODE")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SECTIONNAME")%>
                                                    <asp:HiddenField ID="hfSectionNo" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                    <asp:HiddenField ID="hfCtno" runat="server" Value='<%# Eval("CT_NO")%>' />

                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME")%>  
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                    <asp:Label ID="lblSemesterNo" runat="server" ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnSemesterno" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                </td>
                                                <td>
                                                    <%--      <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                    <%# Eval("UA_FULLNAME")%>
                                                    <asp:HiddenField ID="hdntecher" runat="server" />
                                                    <asp:HiddenField ID="hdnuano" runat="server" Value='<%#Eval("UA_NO")%>' />
                                                </td>
                                                <td hidden>
                                                    <asp:DropDownList ID="ddlNewTeacher" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
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
                            <%--<asp:PostBackTrigger ControlID="btnShow" />--%>
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
    </script>

    <script type="text/javascript">
        function fnConfirmDelete() {
            if (Page_ClientValidate("Submit")) {
                return confirm("Are you sure you want to Change Course Teacher Allotment. If you change course teacher allotement it will reflect on TimeTable, Attendance and Mark Entry Related.");
            }
        }
    </script>

</asp:Content>

