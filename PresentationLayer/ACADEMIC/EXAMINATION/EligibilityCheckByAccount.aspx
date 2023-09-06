<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EligibilityCheckByAccount.aspx.cs" Inherits="ACADEMIC_EXAMINATION_EligibilityCheckByAccount" %>


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
                <h3 class="box-title">ELIGIBILITY CHECK BY ACCOUNT</h3>               
            </div>
            <asp:UpdatePanel ID="updDetained" runat="server">
                <ContentTemplate>
                     
                                     <div class="box-body">
                        <div class="form-group col-md-4" id="TrRadion" runat="server" visible="false">
                            <asp:RadioButtonList ID="rdbOptions" runat="server" RepeatColumns="2">
                                <asp:ListItem Value="1">Detained By Fees Counter</asp:ListItem>
                                <asp:ListItem Value="2">Detained By Institute</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-group col-md-4">
                            <label>Session</label>
                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Font-Bold="True"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-4">
                            <label>Degree</label>
                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ValidationGroup="report">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-4">
                            <label>Branch</label>
                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="report">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-4">
                            <label>Scheme</label>
                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" ValidationGroup="report">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-4">
                            <label>Semester</label>
                            <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" ValidationGroup="report">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-4" id="SectionDetained" runat="server" visible="false">
                            <label>Section</label>
                            <asp:DropDownList ID="ddlSectionDetaintion" runat="server" TabIndex="4"
                                AppendDataBoundItems="True" ValidationGroup="auto" ToolTip="Section">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlSectionCancel" runat="server" ControlToValidate="ddlSectionDetaintion"
                                Display="None" ErrorMessage="Please Select Section" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnShowStudentDetaintion" runat="server" Text="Show Students" ValidationGroup="report"
                                OnClick="btnShowStudentDetaintion_Click" CssClass="btn btn-primary"/>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="True" Enabled="false"  CssClass="btn btn-primary"
                                ValidationGroup="submit" OnClientClick="return confirmDetaind();" OnClick="btnSubmit_Click" />
                            <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"  CssClass="btn btn-warning"/>
                            <asp:Button ID="btnReport" runat="server" Text="Report"  CssClass="btn btn-info"
                                OnClick="btnReport_Click" Visible="False" />
                              <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                        </p>
                        <div class="col-md-12" id="tblBackLog" runat="server" visible="false">
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:Repeater ID="lvDetained" runat="server">
                                    <HeaderTemplate>
                                        <div id="demo-grid">
                                            <h4>Student List </h4>
                                        </div>
                                        <table class="table table-hover table-bordered table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Check
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Enrollment No
                                                    </th>
                                                    <th>Demand Created
                                                    </th>
                                                    <th>Amount Paid
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
                                        <tr class="item" id="trRow" runat="server">
                                            <td>><%#Container.ItemIndex+1 %></td>
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
                                                <asp:Label ID="lblDemandCreated" runat="server" Text='<%# Eval("DEMAND_CREATE") %>' ToolTip='<%# Eval("DEMAND_CREATE")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAmountPaid" runat="server" Text='<%# Eval("PAID_AMT") %>' ToolTip='<%# Eval("PAID_AMT")%>' />
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
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>
                        <div class="col-md-12">
                            <span style="color: Red">Note : Do Following Entry Carefully. Once Eligibility Checked, Student Cannot Be Rollback.  </span>
                        </div>
                    </div>
                       
    <div id="divMsg" runat="server">
    </div>         
                   
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
  
    <script language="javascript" type="text/javascript">
        function confirmDetaind() {
            return confirm("Are you sure, you want to check eligibility for the selected student.");
        }
    </script>
</asp:Content>

