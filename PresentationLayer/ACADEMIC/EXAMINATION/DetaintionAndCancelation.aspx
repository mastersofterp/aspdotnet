<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DetaintionAndCancelation.aspx.cs" Inherits="ACADEMIC_EXAMINATION_DetaintionAndCancelation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
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

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ELIGIBILITY CHECK BY INSTITUTE</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updDetained" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <legend>Eligibility Check By Institute</legend>
                                <div class="col-md-4" id="TrRadion" runat="server" visible="false" style="margin-top: 25px">
                                    <asp:RadioButtonList ID="rdbOptions" runat="server" RepeatColumns="2">
                                        <asp:ListItem Value="1">Detained By Fees Counter</asp:ListItem>
                                        <asp:ListItem Value="2">Detained By Institute</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-md-4">
                                    <label>Session</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Font-Bold="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <label>Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ValidationGroup="report">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <label>Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="report">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>Scheme</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" ValidationGroup="report">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>Semester</label>
                                    <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" ValidationGroup="report">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" id="SectionDetained" runat="server" visible="false">
                                    <label>Section</label>
                                    <asp:DropDownList ID="ddlSectionDetaintion" runat="server" TabIndex="4"
                                        AppendDataBoundItems="True" ValidationGroup="auto" ToolTip="Section">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlSectionCancel" runat="server" ControlToValidate="ddlSectionDetaintion"
                                        Display="None" ErrorMessage="Please Select Section" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-12" style="margin-top: 25px">
                                    <p class="text-center">
                                        <asp:Button ID="btnShowStudentDetaintion" runat="server" Text="Show Students" ValidationGroup="report"
                                            OnClick="btnShowStudentDetaintion_Click" CssClass="btn btn-info" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="True" Enabled="false" CssClass="btn btn-primary"
                                            ValidationGroup="submit" OnClientClick="return confirmDetaind();" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                        <asp:Button ID="btnReport" runat="server" Text="Report"
                                            OnClick="btnReport_Click" Visible="False" CssClass="btn btn-info" />
                                        <asp:Button ID="btnCancelStudReport" runat="server" OnClick="btnCancelStudReport_Click"
                                            Text="Admission Cancel Report" Visible="False" CssClass="btn btn-info" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                    </p>
                                    <div><span style="color: Red">Note : Do Following Entry Carefully. Once Eligibility Checked, Student Cannot Be Rollback.  </span></div>
                                    <div class="col-md-12 table table-responsive" id="tblBackLog" runat="server" visible="false">
                                        <asp:Repeater ID="lvDetained" runat="server">
                                            <HeaderTemplate>
                                                <div id="demo-grid">
                                                    Student List                                                                   
                                                </div>
                                                <table>
                                                    <thead class="table table-hover table-bordered">
                                                        <tr class="bg-light-blue">
                                                            <th>Sr.No.
                                                            </th>
                                                            <th>Check
                                                            </th>
                                                            <th>Name
                                                            </th>
                                                            <th>Enrollment No
                                                            </th>
                                                            <th>Select Status
                                                            </th>
                                                            <th>Remark
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr id="trRow" runat="server">
                                                    <td><%#Container.ItemIndex+1 %></td>
                                                    <td>
                                                        <asp:CheckBox ID="chkFinalDetain" runat="server" Checked='<%# Eval("FINAL_DETAIN").ToString() == "Y" ? true : false %>'
                                                            Enabled='<%# Eval("FINAL_DETAIN").ToString() ==  "Y" ? false : true %>' />
                                                        <asp:Label
                                                            ID="idNo" runat="server" ToolTip='<%#Eval("IDNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("SEATNO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Eval("ENROLLNO") %>' ToolTip='<%# Eval("ENROLLNO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                                            <asp:ListItem Value="">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="0">ELIGIBLE</asp:ListItem>
                                                            <asp:ListItem Value="1">DETAINED</asp:ListItem>
                                                            <asp:ListItem Value="2">PENDING</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS")%>' ToolTip='<%# Eval("STATUS")%>' Visible="false"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:TextBox ID="txtremarks" runat="server" Text='<%# Eval("REMARKS")%>' Enabled='<%# Eval("REMARKS").ToString() == string.Empty ? true : false %>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-md-12" id="tr" runat="server" visible="false">
                                        <asp:RadioButtonList ID="rbtlDetaind" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtlDetaind_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" Visible="False">
                                            <asp:ListItem Value="Prov Detain">Prov Detain</asp:ListItem>
                                            <asp:ListItem Value="Final Detain">Final Detain</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-12 tab;e table-responsive">
                                        <asp:Panel ID="pnlDetained" runat="server" Visible="false" ScrollBars="Auto" Height="100px">
                                            <asp:ListView ID="lvDetainedStudent" runat="server" Visible="False">
                                                <LayoutTemplate>
                                                    <div>
                                                        <h4>Students List</h4>
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                </tr>

                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trRow" runat="server">
                                                        <td>
                                                            <span>
                                                                <%# Eval("STUDNAME") %></span>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div visible="false" enabled="false" runat="server" id="divstudentcancellation" class="col-md-12 table table-responsive">
                        <fieldset>
                            <legend>STUDENT CANCELATION</legend>
                            <asp:UpdatePanel ID="updCancel" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-3">
                                        <label>Branch</label>
                                        <asp:DropDownList ID="ddlBranchCancel" runat="server" TabIndex="2" AppendDataBoundItems="True"
                                            ValidationGroup="auto" ToolTip="Branch" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchCancel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranchAuto" runat="server" ControlToValidate="ddlBranchCancel"
                                            Display="None" ErrorMessage="Please Select Branch" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Session</label>
                                        <asp:DropDownList ID="ddlSessionCancel" runat="server" AppendDataBoundItems="True"
                                            Font-Bold="True">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Scheme</label>
                                        <asp:DropDownList ID="ddlSchemeCancel" runat="server" TabIndex="3" AppendDataBoundItems="True"
                                            ValidationGroup="auto" ToolTip="Scheme" CausesValidation="True" OnSelectedIndexChanged="ddlSchemeCancel_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSchemeAuto" runat="server" ControlToValidate="ddlSchemeCancel"
                                            Display="None" ErrorMessage="Please Select Scheme" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Semester</label>
                                        <asp:DropDownList ID="ddlSemesterCancel" runat="server" TabIndex="4"
                                            AppendDataBoundItems="True" ValidationGroup="auto" ToolTip="Semester" CausesValidation="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemesterAuto" runat="server" ControlToValidate="ddlSemesterCancel"
                                            Display="None" ErrorMessage="Please Select Semester" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 25px">
                                        <p class="text-center">
                                            <asp:Button ID="Cancelation_Show_Students" runat="server" Text="Show Students" ValidationGroup="auto"
                                                OnClick="Cancelation_Show_Students_Click" />
                                            <asp:Button ID="btnCancelationCancel" runat="server" Text="Cancel" />
                                        </p>
                                    </div>
                                    <div class="col-md-3" id="SectionCancel" runat="server" visible="false">
                                        <label>Section</label>
                                        <asp:DropDownList ID="ddlSectionCancelation" runat="server" TabIndex="4"
                                            AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="auto" ToolTip="Section">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSectionAuto" runat="server" ControlToValidate="ddlSectionCancelation"
                                            Display="None" ErrorMessage="Please Select Section" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-9" style="margin-top: 20px">
                                        <span style="width: 100%; font-size: 9pt; color: Green; font-weight: bold;">Note : *
                                                            - Select upto Section<br />
                                            &#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;** - Select upto Semester
                                        </span>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Degree</label>
                                        <asp:DropDownList ID="ddlDegreeCancel" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegreeCancel_SelectedIndexChanged"
                                            TabIndex="1" ToolTip="Branch" ValidationGroup="auto">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegreeAuto" runat="server" ControlToValidate="ddlDegreeCancel"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                    </div>
                                    <p>
                                        <asp:ValidationSummary ID="vsAuto" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="auto" />
                                    </p>
                                    <div class="col-md-12" id="Table1" runat="server" visible="false">
                                        <asp:ListView ID="ListView2" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Students to be marked for Cancelation</h4>
                                                    <table class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Select
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Roll No.
                                                                </th>
                                                                <th>Remark
                                                                </th>
                                                                <th>Final Cancel
                                                                </th>
                                                                <th>Final Remark
                                                                </th>
                                                                <th>Remove
                                                                </th>
                                                                <th>Remove Remark
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trRow" runat="server">

                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("PROV_CANCEL").ToString() == string.Empty ? false : true %>' /><asp:Label
                                                            ID="idNo" runat="server" ToolTip='<%#Eval("IDNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("SEATNO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRoll" runat="server" Text='<%# Eval("SEATNO") + Eval("ROLLNO").ToString() == "0" ? string.Empty : Eval("ROLLNO") %>'> </asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemark" runat="server" Font-Bold="true" Text='<%# Eval("CANCEL_REMARK")%>'
                                                            TextMode="MultiLine" Width="40px" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkFinalDetain" runat="server" Checked='<%# Eval("FINAL_CANCEL").ToString() == string.Empty ? false : true %>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinalRemark" runat="server" Font-Bold="true" Text='<%# Eval("FINAL_CANCEL_REMARK")%>'
                                                            TextMode="MultiLine" Width="40px" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkRemove" runat="server" Enabled='<%# Eval("CANCEL_IDNO").ToString()   != ""  ? true : false  %>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemoveRemark" runat="server" Enabled='<%# Eval("CANCEL_IDNO").ToString()   != ""  ? true : false  %>'
                                                            Font-Bold="true" Text='<%# Eval("UNDO_DETAIN_REMARK")%>' TextMode="MultiLine"
                                                            Width="40px" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                    <div class="col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="Button1" runat="server" Font-Bold="True" OnClick="btnSubmit_Click"
                                                Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary" />
                                            <asp:Button ID="Button2" runat="server" Font-Bold="True" OnClick="btnCancel_Click"
                                                Text="Cancel" CssClass="btn btn-primary" />
                                        </p>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
    <script language="javascript" type="text/javascript">
        function confirmDetaind() {
            return confirm("Are you sure, you want to check eligibility for the selected student.");
        }
    </script>
</asp:Content>
