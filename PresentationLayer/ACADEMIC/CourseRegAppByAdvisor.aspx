<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CourseRegAppByAdvisor.aspx.cs" Inherits="ACADEMIC_CourseRegAppByAdvisor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .YES
        {
            color: green;
        }

        .NO
        {
            color: red;
        }

        .modal-footer-1
        {
            padding: 15px !important;
            text-align: none !important;
            border-top: 1px solid #e5e5e5 !important;
            border-top-color: rgb(229, 229, 229) !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl_details"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnl_details" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>COURSE REGISTRATION APPROVE BY ADVISOR/MENTOR</b></h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span>Session </label>
                                <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" ToolTip="Please Select Session" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span>School/Institute Name</label>
                                <asp:DropDownList ID="ddlCollegeName" runat="server" TabIndex="2" AppendDataBoundItems="True" AutoPostBack="true"
                                    ToolTip="Please Select School/Institute" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlCollegeName"
                                    Display="None" ErrorMessage="Please Select School/Institute Name " ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span class="validstar" style="color: red;">*</span> Degree </label>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"
                                    TabIndex="3" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="Show">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-4">
                                <label><span class="validstar" style="color: red;">*</span>Program/Branch </label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                    TabIndex="4" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Program/Branch" InitialValue="0" ValidationGroup="Show">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-4">
                                <label><span class="validstar" style="color: red;">*</span> Semester</label>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_OnSelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="Show">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="6" Text="Show Student"
                                    ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" TabIndex="6" Text="Approved Report"
                                    ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnBulkApprove" runat="server" Visible="false" OnClick="btnBulkApprove_Click" TabIndex="7" Text="Bulk Approve"
                                    ValidationGroup="BulkSelect" CssClass="btn btn-primary" />
                                <ajaxToolKit:ConfirmButtonExtender ID="cnfbExtender" runat="server" ConfirmText="Are you sure you want to approve course registration for selected student ?" Enabled="True" TargetControlID="btnBulkApprove"></ajaxToolKit:ConfirmButtonExtender>
                                <asp:TextBox ID="txtbulkSelection" Style="display: none" runat="server"></asp:TextBox>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="false" ValidationGroup="Show" />
                                <asp:RequiredFieldValidator ID="rfvBulkSelect" runat="server" ControlToValidate="txtbulkSelection"
                                    Display="None" ErrorMessage="Please Select Student for Course Registration Approval" ValidationGroup="BulkSelect">
                                </asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="false" ValidationGroup="BulkSelect" />
                                <div class="col-md-12">
                                    Total Selected Student : <span style="color: red">
                                        <asp:Label ID="txtTotChk" runat="server"></asp:Label></span>
                                </div>

                                <div class="col-md-12 table table-responsive" id="divCourseReg" runat="server">
                                    <asp:ListView ID="lvCourseRegistrationStudent" runat="server">
                                        <LayoutTemplate>
                                            <h4><b>Student List</b></h4>
                                            <div id="demo-grid" style="height: 300px; overflow: auto;">
                                                <table id="example" class="table table-bordered table-hover table-responsive table-striped" width="100%">
                                                    <thead style="height: 50px;">
                                                        <tr class="bg-light-blue" style="position: absolute;width: 96%;">
                                                            <td style="width:56px">
                                                                <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAllCheckboxes(this)" />
                                                                <%-- onclick="totAllSubjects(this,'Student')"--%>
                                                            </th>
                                                            <td style="width:119px">Registration No </th>
                                                            <td style="width:114px">Roll No </th>
                                                            <td style="width:256px">Name </th>
                                                            <td style="width:176px">Branch </th>
                                                            <td style="width:84px">Section</th>
                                                            <td style="width:195px">Approved by Advisor/Mentor</th>
                                                            <td style="width:86px">View</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                                <tr id="tr_example" runat="server">
                                                    <%--style="background-color:red"--%>
                                                    <td style="width:48px">
                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO")%>' onclick="SelectSingleCheckboxes(this)" />
                                                    </td>
                                                    <td style="width:100px"><%# Eval("REGNO")%></td>
                                                    <td style="width:95px"><%# Eval("ROLLNO")%></td>
                                                    <td style="width:210px"><%# Eval("STUDNAME")%></td>
                                                    <td style="width:149px"><%# Eval("BRANCH")%></td>
                                                    <td style="width:70px"><%# Eval("SECTIONNAME")%></td>
                                                    <td style="width:160px">
                                                        <asp:Label runat="server" ID="lblRegStatus" CssClass='<%# Eval("REGISTRATION_STATUS")%>' Text='<%# Eval("REGISTRATION_STATUS")%>'></asp:Label>
                                                    </td>
                                                    <td style="width:70px">
                                                        <asp:ImageButton ID="btnView" runat="server" CausesValidation="false" Style="text-decoration: underline;" CommandName='<%# Eval("REGNO")%>' CommandArgument='<%# Eval("IDNO") %>' ImageUrl="~/IMAGES/view.gif" AlternateText="View" ToolTip="View Student Course Registration Detail" OnClick="btnView_Click" />
                                                    </td>
                                                </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                        </div>

                    </div>
                </div>

                <div class="col-12">
                    <div class="row">
                        <div class="modal fade" style="padding-top: 5%;" data-backdrop="static" data-keyboard="false" aria-modal="true" id="Model_CourseDetail" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header text-center" style="background-color: #6a8296 !important">
                                        <h4 class="modal-title" style="font-style: normal; font-weight: bold; color: white">Student Course Registration Detail</h4>
                                    </div>

                                    <div class="modal-body">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div class="row">

                                                        <div class="col-sm-12">
                                                            <h4><b>Regisration No :</b> <span style="color: green">
                                                                <asp:Label ID="lblRegistrationNo" runat="server"></asp:Label></span></h4>
                                                            <asp:TextBox ID="txtSelectionRecord" Style="display: none" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCourseSelection" runat="server" ControlToValidate="txtSelectionRecord"
                                                                Display="None" ErrorMessage="Please Select Course" ValidationGroup="SingleSubmit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-sm-12 table table-responsive" runat="server" id="divCourseDetail">
                                                            <asp:ListView ID="lstCourseDetail" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid">
                                                                        <%--  <h4><b>Course Detail</b></h4>--%>
                                                                        <table id="example" class="table table-bordered table-hover table-responsive table-striped" width="100%">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>
                                                                                        <asp:CheckBox ID="cbHeadCourse" runat="server" onclick="SelectAllCourseCheckboxes(this)" />
                                                                                    </th>
                                                                                    <th>Course Code</th>
                                                                                    <th>Course Name</th>
                                                                                    <th>Credit's </th>
                                                                                    <th>Registed by Student </th>
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
                                                                        <td>
                                                                            <asp:CheckBox ID="cbRowCourse" runat="server" Checked='<%# Eval("STUDENT_REGISTRATION_STATUS").ToString() == "YES" ? true : false %>' ToolTip='<%# Eval("IDNO")%>' onclick="SelectSingleCourseCheckboxes(this)" />
                                                                            <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO")%>' />
                                                                            <asp:HiddenField ID="hdfCourseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                                            <asp:HiddenField ID="hdfSubId" runat="server" Value='<%# Eval("SUBID")%>' />
                                                                            <asp:HiddenField ID="hdfSrNo" runat="server" Value='<%# Eval("SRNO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblRegStatus" CssClass='<%# Eval("STUDENT_REGISTRATION_STATUS")%>' Text='<%# Eval("STUDENT_REGISTRATION_STATUS")%>'></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="modal-footer-1 text-center">
                                            <asp:Button ID="btnSingleSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" UseSubmitBehavior="false"
                                                OnClientClick="if (!Page_ClientValidate('SingleSubmit')){ return false; } this.disabled = true; this.value ='  Please Wait...';"
                                                TabIndex="1" ValidationGroup="SingleSubmit" OnClick="btnSingleSubmit_OnClick" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" data-dismiss="modal" OnClientClick="Checkboxesfalse()" CssClass="btn btn-danger" TabIndex="2"></asp:Button>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:ValidationSummary ID="vsSingleSubmit" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="SingleSubmit" DisplayMode="List" />
                                        </div>

                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSingleSubmit" />
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function Checkboxesfalse() {
            document.getElementById("<%=txtbulkSelection.ClientID %>").value = '';
            document.getElementById("<%=txtTotChk.ClientID %>").innerHTML = '';
            var IsChecked = false;
            var Parent = document.getElementById("<%=divCourseReg.ClientID %>");
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].type == "checkbox") {
                    items[i].checked = IsChecked;
                }
            }

        }

        function SelectAllCheckboxes(spanChk) {
            var Count = 0;
            document.getElementById("<%=txtbulkSelection.ClientID %>").value = '';
            var IsChecked = spanChk.checked;
            var cbxAll = spanChk;
            // var Parent = document.getElementById('lvStudents');
            var Parent = document.getElementById("<%=divCourseReg.ClientID %>");
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != cbxAll.id && items[i].type == "checkbox") {
                    items[i].checked = IsChecked;
                    if (IsChecked == true) {
                        Count = Number(Count) + 1;
                    }
                }
            }
            if (Number(Count) > 0) {
                document.getElementById("<%=txtbulkSelection.ClientID %>").value = Count;
            }

            document.getElementById("<%=txtTotChk.ClientID %>").innerHTML = Count;
        }

        function SelectSingleCheckboxes(spanChk) {
            var Count = document.getElementById("<%=txtbulkSelection.ClientID %>").value;

            if (spanChk.checked == true) {
                Count = Number(Count) + 1;
            }
            else {
                Count = Number(Count) - 1;
            }

            if (Number(Count) > 0) {
                document.getElementById("<%=txtbulkSelection.ClientID %>").value = Count;
            }
            document.getElementById("<%=txtTotChk.ClientID %>").innerHTML = Count;
        }

        function SelectAllCourseCheckboxes(spanChk) {
            var Count = 0;
            document.getElementById("<%=txtSelectionRecord.ClientID %>").value = '';
            var IsChecked = spanChk.checked;
            var cbxAll = spanChk;
            var Parent = document.getElementById("<%=divCourseDetail.ClientID %>");
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != cbxAll.id && items[i].type == "checkbox") {
                    items[i].checked = IsChecked;
                    if (IsChecked == true) {
                        Count = Number(Count) + 1;
                    }
                }
            }
            if (Number(Count) > 0) {
                document.getElementById("<%=txtSelectionRecord.ClientID %>").value = Count;
            }
        }

        function SelectSingleCourseCheckboxes(spanChk) {
            var Count = document.getElementById("<%=txtSelectionRecord.ClientID %>").value;

            if (spanChk.checked == true) {
                Count = Number(Count) + 1;
            }
            else {
                Count = Number(Count) - 1;
            }

            if (Number(Count) > 0) {
                document.getElementById("<%=txtSelectionRecord.ClientID %>").value = Count;
              }
              else {
                  document.getElementById("<%=txtSelectionRecord.ClientID %>").value = '';
              }
          }
    </script>

    <script language="javascript" type="text/javascript">
        function View() {
            $("#Model_CourseDetail").modal();
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

