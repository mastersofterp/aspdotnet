<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LockUnlockExamAssesment.aspx.cs" Inherits="ACADEMIC_EXAMINATION_LockUnlockExamAssesment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLockUnlock"
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
    <asp:UpdatePanel ID="updLockUnlock" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Lock/Unlock Exam Component</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeIdDepMap" runat="server" AutoPostBack="true" AppendDataBoundItems="true"  CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlCollegeIdDepMap_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ddlcollege" runat="server" ControlToValidate="ddlCollegeIdDepMap"
                                            Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" 
                                            TabIndex="2" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivSubtype">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Course/Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcourse" runat="server" TabIndex="3" AutoPostBack="true" AppendDataBoundItems="true" 
                                            ToolTip="Please SelectCourse/Subject" data-select2-enable="true" OnSelectedIndexChanged="ddlcourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlcourse" runat="server" ControlToValidate="ddlcourse"
                                            Display="None" ErrorMessage="Please Select Subject Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                      <div class="form-group col-lg-3 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked=<span style="color: green; font-weight: bold">Lock</span></span>  </p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked=<span style="color: green; font-weight: bold">UnLock</span></span>  </p>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Font-Bold="False" OnClick="btnSave_Click"
                                    TabIndex="11" Text="Lock/Unlock" ValidationGroup="save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummaryShow" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="save" />
                                <asp:ValidationSummary ID="ValidationSummaryShow0" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Status" />
                            </div>
                             <div class="col-12">
                                <asp:Panel ID="pnlStudents" runat="server">
                                    <asp:ListView ID="lvExamStatus" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Lock/Unlock Exam Component</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Sr No.</th>
                                                        <th>Exam Component
                                                        </th>
                                                        <th>Faculty Name
                                                        </th>
                                                        <th>Lock/Unlock  &nbsp; <asp:CheckBox ID="cbHead" runat="server" onclick="totAll(this)" ToolTip="Select/Select all" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="trCurRow">
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td>
                                                    <asp:Label ID="lblCourseNo" runat="server" Text='<%# Eval("SUBEXAMNAME")%>' ToolTip='<%# Eval("SUBEXAMNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFaculty" runat="server" Text='<%# Eval("UA_FULLNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chklock" runat="server" TabIndex="10" ToolTip='Select to Lock' Checked='<%# Eval("LOCK").ToString() == "True" ? true : false %>' />

                                                </td>
                                            </tr>
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
     <script type="text/javascript">
         function totAll(headchk) {
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
                 }
             }
         }
    </script>
</asp:Content>

