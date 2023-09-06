<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Update_Student_Admission_Status.aspx.cs" Inherits="ACADEMIC_Update_Student_Admission_Status" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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

    <asp:UpdatePanel ID="updtime" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="True" ToolTip="Please Select Institute" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged"
                                            AppendDataBoundItems="True" ToolTip="Please Select Admbatch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Admbatch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>


                                    <div class="form-group col-md-3 col-sm-3 col-3">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%-- <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="report">
                                            </asp:RequiredFieldValidator>--%>

                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            ToolTip="Please Select Degree" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>

                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Branch" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%-- <label>Admission Year</label>--%>
                                            <asp:Label ID="lblDYtxtAdmYear" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" TabIndex="5" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Admission Year" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--<asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Year"
                                            ValidationGroup="show">
                                        </asp:RequiredFieldValidator>--%>

                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--   <label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" ToolTip="Please Select Semester" TabIndex="6" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmissionStatus" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmissionstatus" runat="server" CssClass="form-control" TabIndex="7" AppendDataBoundItems="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                </div>

                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click" TabIndex="8"
                                        ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnsubmit1" runat="server" Text="Submit" OnClick="btnsubmit1_Click" TabIndex="9"
                                        ToolTip="Shows Students under Selected Criteria." CssClass="btn btn-primary"   Visible="false" />

                                   <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="10"
                                                                ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="show" DisplayMode="List" />

                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:HiddenField ID="hftot" runat="server" />
                                    <asp:HiddenField ID="txtTotStud" runat="server" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlLV" runat="server">
                                        <asp:ListView ID="lvStudentRecords" runat="server">
                                            <LayoutTemplate>
                                                <div id="listViewGrid" class="vista-grid">
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <asp:Panel ID="pnlstudlist" runat="server">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" Visible="true" />
                                                                    </th>
                                                                    <th>PRN No.
                                                                    </th>

                                                                    <th>Student Name
                                                                    </th>

                                                                    <th>Branch                                                                         
                                                                    </th>

                                                                    <th>Semester                                                                       
                                                                    </th>

                                                                    <th>Current Status                                                                       
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkAdmStatus" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>

                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("LONGNAME")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("STUDENT_ADMISSION_STATUS_DESCRIPTION")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div id="divMsg" runat="server"></div>
                    </div>
                </div>
            </div>

            <script type="text/javascript">

                function totStudents(chk) {
                    var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

                    if (chk.checked == true)
                        txtTot.value = Number(txtTot.value) + 1;
                    else
                        txtTot.value = Number(txtTot.value) - 1;
                }




                function SelectAll(chk) {
                    debugger;
                    var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
                    var hftot = document.getElementById('<%= hftot.ClientID %>');

                    for (i = 0; i < hftot.value; i++) {

                        var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkAdmStatus');

                        if (lst.type == 'checkbox') {
                            if (chk.checked == true) {
                                lst.checked = true;

                                //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = false;
                                //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = false;
                            }
                            else {
                                lst.checked = false;


                                //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = true;
                                //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = true;

                                //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').prop('checked', false);
                                //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').prop('checked', false);
                            }
                        }

                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

