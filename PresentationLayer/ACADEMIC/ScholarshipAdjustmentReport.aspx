<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ScholarshipAdjustmentReport.aspx.cs" Inherits="ACADEMIC_ScholarshipAdjustmentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime1"
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

     <asp:UpdatePanel ID="updtime1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Scholarship Adjustment Report</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlBulkStud" runat="server" Visible="true">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch</label>
                                            </div>
                                            <%--<asp:TextBox ID="lblSession" runat="server"></asp:TextBox>--%>
                                            <%--<asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                                AutoPostBack="True" ToolTip="Please Select Session" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                ToolTip="Please Select Admission Batch" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                            <%--<asp:Label ID="lblSession" runat="server"  ></asp:Label>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                 ToolTip="Please Select Degree" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"  CssClass="form-control" data-select2-enable="true" TabIndex="3"
                                                ToolTip="Please Select Branch" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" CssClass="form-control" data-select2-enable="true" TabIndex="4" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <%--OnClientClick="return Enable_Radio(this);"--%>
                                    <asp:Button ID="btnShow" runat="server" Text="Show Student"  TabIndex="5"
                                        ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" Visible="false" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"  TabIndex="6"
                                        ValidationGroup="show" CssClass="btn btn-primary"   Visible="false" Enabled="false" OnClientClick="return validateAssign();"  CausesValidation="false"  />

                                    <asp:Button ID="btnExcelReport" runat="server" Text="Report" TabIndex="999" OnClick="btnExcelReport_Click" CssClass="btn btn-info"
                                         ToolTip="Print Card under Selected Criteria." ValidationGroup="show" Visible="true" />

                                    <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" TabIndex="9" CssClass="btn btn-primary"
                                        ToolTip="Send Card By Email" ValidationGroup="show" Visible="false" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="7"
                                        ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:HiddenField ID="hftot" runat="server" />
                                    <asp:HiddenField ID="txtTotStud" runat="server" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudentRecords" runat="server" >
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student Listt</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                    <thead class="bg-light-blue">
                                                        <tr id="Tr1" runat="server">
                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this)"  ToolTip="Select or Deselect All Records" Visible="true" />
                                                            </th>
                                                            <th>Reg. No.
                                                            </th>
                                                            <th>Roll No.</th>
                                                            <th>Student Name
                                                            </th>
                                                            <th style="display: none">Student Email
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Scholarship Amount</th>
                                                            <th>Scholarship Type</th>
                                                            <th>Scholarship Adjusted Amount</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                     </tbody>   
                                                </table>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <%--  <asp:UpdatePanel runat="server" ID="updList">
                                                <ContentTemplate>--%>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkReport" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totStudents(this);" />
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td><%# Eval("ROLLNO")%></td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                        <asp:HiddenField ID="hdfBranchno" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("ENROLLNO") %>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td style="display: none">
                                                        <%--<%# Eval("EMAILID_INS")%>
                                                        <asp:HiddenField ID="Hdfemail" runat="server" Value='<%# Eval("EMAILID_INS") %>' />--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SCHL_AMOUNT") %>
                                                      <asp:HiddenField ID="hdfDegree" runat="server" Value='<%# Eval("DEGREENO") %>'/>
                                                        <asp:Label ID="lblschamt" runat="server" Text='<%# Eval("SCHL_AMOUNT") %>' ToolTip='<%# Eval("SEMESTERNO") %>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDDLData" runat="server" Text='<%# Eval("SCHOLARSHIP_ID") %>' ToolTip='<%# Eval("LONGNAME") %>' Visible="false"></asp:Label>
                                                        <%#Eval("SCHOLORSHIPNAME") %>
                                                    </td>
                                                    <td><%#Eval("SCH_ADJ_AMT") %></td>
                                                </tr>
                                                <%--</ContentTemplate>
                                                <Triggers>
                                                     <asp:PostBackTrigger ControlID="rdoYes" />
                                                     <asp:PostBackTrigger ControlID="rdoNo" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                                <div id="divMsg" runat="server">
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
        <%--<Triggers>   
            <asp:AsyncPostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
               <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>--%>
    </asp:UpdatePanel>

    <%--<script language="javascript" type="text/javascript">
        function check()
        {
            var a = document.getElementsByName("chkIdentityCard")
            var j=0
            for(i=0;i<=a.length;i++)
            {
                if(a[i].checked==true)
                {
                    j=j+1;
                }
            }
            if (j==0)
            {
                alert("please select checkbox")
            }
        } 
    </script>
   --%>

       <script type="text/javascript">
           function SelectAll(headchk) {
               var i = 0;
               var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
               var hftot = document.getElementById('<%= hftot.ClientID %>').value;
               var count = 0;
               for (i = 0; i < Number(hftot) ; i++) {

                   var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                   if (lst.type == 'checkbox') {
                       if (headchk.checked == true) {
                           if (lst.disabled == false) {
                               lst.checked = true;
                               count = count + 1;
                           }
                       }
                       else {
                           lst.checked = false;
                       }
                   }
               }

               if (headchk.checked == true) {
                   document.getElementById('<%= txtTotStud.ClientID %>').value = count;
               }
               else {
                   document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
               }
           }

           function validateAssign() {
               var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
                   if (txtTot == 0) {
                       alert('Please Check atleast one student ');
                       return false;
                   }
                   else
                       if (confirm("Are you sure to adjust scholarship for selected students?"))
                           return true;
                   return false;

               }
               function confirmSearch() {
                   if (confirm("Are you sure to adjust scholarship for selected students?"))
                       return true;
                   return false;

               }
               function totStudents(chk) {
                   var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

                   if (chk.checked == true)
                       txtTot.value = Number(txtTot.value) + 1;
                   else
                       txtTot.value = Number(txtTot.value) - 1;
               }


    </script>

</asp:Content>
