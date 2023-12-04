<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SubstituteExamApproval.aspx.cs" Inherits="ACADEMIC_EXAMINATION_SubstituteExamApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

        //function ChkApprove(val) {

        //    var chk = document.getElementById("")

        //}


    </script>

     <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>SUBSTITUTE EXAM APPROVAL</b></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="row">
                                   


                                     <div class="form-group col-md-4">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <label>College & Scheme</label>
                                          
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>



                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                         <div>
                                        <asp:DropDownList ID="ddlsessionforabsent" OnSelectedIndexChanged="ddlsessionforabsent_SelectedIndexChanged" runat="server"
                                            AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                             </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsessionforabsent"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>





                                   <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddldegree" TabIndex="3" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdegree" runat="server" ControlToValidate="ddldegree"
                                            Display="None" ErrorMessage="Please Select Degree." InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>--%>
                                   <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlbranch" TabIndex="4" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvbranch" runat="server" ControlToValidate="ddlbranch"
                                            Display="None" ErrorMessage="Please Select Branch." InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>--%>




                                <%--      <div class="form-group col-md-3">
                                        <label><span style="color: red;">*</span> Semester</label>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                            AutoPostBack="True" TabIndex="5" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>
                                     <div class="form-group col-md-3" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                       <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                            AutoPostBack="True" TabIndex="5" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="report1">
                                        </asp:RequiredFieldValidator>
                                    </div>




                                     <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcourseforabset" runat="server" AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="ddlcourseforabset_SelectedIndexChanged" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlcourseforabset"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report1"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlexamnameabsentstudent" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlexamnameabsentstudent_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlexamnameabsentstudent"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report1"></asp:RequiredFieldValidator>
                                    </div>




                                      <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsubexamname" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlsubexamname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsubexamname"
                                            Display="None" ErrorMessage="Please Select Sub Exam Name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report1"></asp:RequiredFieldValidator>
                                    </div>




                                  <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Date</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamDate" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged " data-select2-enable="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="ddlExamDate"
                                            ValidationGroup="report" Display="None" ErrorMessage="Please Select Exam Date"
                                            SetFocusOnError="true" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamDate"
                                            ValidationGroup="Show" Display="None" ErrorMessage="Please Select Exam Date"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>--%>
                                   

                                   <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                      
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Block</label>
                                        </div>
                                        <asp:DropDownList ID="ddlblock" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlblock_SelectedIndexChanged"
                                            AutoPostBack="True" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       
                                    </div>--%>
                                    <div class="form-group col-lg-5 col-md-12 col-12 d-none">
                                        <div class=" note-div">
                                            <h5 class="heading">Note (Please Select)</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Present/Absent <span style="color: green; font-weight: bold">UnChecked for Absent Student</span></span></p>
                                        </div>
                                    </div>
                                </div>

                            </div>
                       
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="report"
                                TabIndex="11" OnClick="btnShow_Click" CssClass="btn btn-primary" />

                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                OnClick="btnSubmit_Click" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="report" OnClientClick="javascript: return myFunction()"  />
                         <%--    <asp:Button ID="btnLock" runat="server" TabIndex="17" Text="Lock" CssClass="btn btn-info"
                                OnClick="btnLock_Click" BackColor="#FF9900" Visible="false" />--%>
                           
                            <asp:Button ID="btnReport" runat="server" TabIndex="14" Text="Report" CssClass="btn btn-info"
                                OnClick="btnReport_Click1" Visible="false" ValidationGroup="report"/>

                            <%-- <asp:Button ID="btnAbsentReport" runat="server"  CausesValidation="false"
                                    Text="Absent Report" CssClass="btn btn-info" OnClick="btnAbsentReport_Click" />--%>
                            <asp:Button ID="btnAbsentReport1" runat="server" TabIndex="14" Text="Report" CssClass="btn btn-info"
                                CausesValidation="false" OnClick="btnAbsentReport1_Click" Visible="true"/>

                            <asp:Button ID="btnBlankDocket" runat="server" TabIndex="15"
                                Text="Blank Docket" CssClass="btn btn-info"  Visible="false" />
                            <asp:Button ID="btnDocket" runat="server" TabIndex="16" Text="Docket"
                                CssClass="btn btn-info"  Visible="false" />
                           
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                TabIndex="13" CssClass="btn btn-warning" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                        </div>



                            <div class="col-12">
                                <asp:Panel ID="pnlSeqNum_advisor" runat="server">
                                    <asp:ListView ID="lvabsent" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SRNO </th>
                                                            <th>REG.NO</th>
                                                           <%-- <th>ENROLLNO</th>--%>
                                                            <th>STUDENT NAME</th>
                                                            <%--<th>Degree</th>--%>
                                                             <th>COURSE CODE</th>
                                                            <th>COURSE NAME</th>
                                                            <%--<th>Branch</th>--%>
                                                            <th>EXAM NAME</th>
                                                            <%-- <th>SUBEXAM NAME</th>--%>
                                                            <th>SEMESTER</th>
                                                            <th>APPLY DATE</th>
                                                            <th>EXISTING MARK</th>
                                                            <th>APPROVE</th>
                                                          <th>REJECT</th>
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
                                                    <asp:Label ID="lblExamNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' Font-Bold="true" />

                                               </td>                                                
                                                  <%--<td><asp:CheckBox ID="CheckId" runat="server"  ToolTip='<%# Eval("IDNO")%>'  /></td>--%>
                                                  <td> 
                                                     
                                                      <asp:Label ID="lblREGNO" runat="server" Text='<%#Eval("REGNO")%>' ToolTip='<%#Eval("FEES")%>' Font-Bold="true" />
                                                      <%--<asp:HiddenField ID="hdfexistMarks" Value='<%# Eval("EXISTS_MARK") %>' runat="server" />--%>
                                                  </td>                                                

                                               <%-- <td> <asp:Label ID="lblENROLLNO" runat="server" tooltip='<%#Eval("FEES")%>' Font-Bold="true" /></td>--%>

                                                <td> 
                                                    <asp:Label ID="lblSTUDNAME" runat="server" Text='<%#Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO") %>' Font-Bold="true" />

                                                </td>
                                                <td> 
                                                    <asp:Label ID="lblCOURSECODE" runat="server" Text='<%#Eval("COURSENO")%>' ToolTip='<%# Eval("COURSENO")%>' Font-Bold="true" />

                                                </td>
                                                <td> 
                                                    <asp:Label ID="lblCOURSENAME" runat="server" Text='<%#Eval("COURSE_NAME")%>' ToolTip='<%# Eval("COURSENO")%>' Font-Bold="true" />

                                                </td>

                                                <td> 
                                                    <asp:Label ID="lblExamname" runat="server" Text='<%#Eval("EXAMNAMENEW")%>' Font-Bold="true" />

                                                </td>
                                               <%-- <td> 
                                                    <asp:Label ID="lblSubExamName" runat="server" Text='<%#Eval("SUBEXAMNAME")%>' Font-Bold="true" />

                                                </td>--%>
                                                <td> 
                                                    <asp:Label ID="lblSEMESTERNAME" runat="server" Text='<%#Eval("SEMESTERNO")%>' ToolTip='<%# Eval("SEMESTERNO") %>' Font-Bold="true" />

                                                </td>
                                                <td> 
                                                    <asp:Label ID="lblapplydate" runat="server" Text='<%#Eval("REG_DATE")%>'  Font-Bold="true" />

                                                </td>
                                                <td> 
                                                    <asp:Label ID="lblexistingmark" runat="server" Text='<%#Eval("EXISTING_TEST_MARK")%>' Font-Bold="true" />
                                                  <%--  <asp:HiddenField ID="hdffee" runat="server" text='<%#Eval("FEES") %>' />--%>

                                                </td>
                                                 <td>                                                              
                                                      <asp:CheckBox ID="chk_Absent" runat="server" Checked='<%# Eval("ADVISOR_APPROVE").ToString()=="1" ? true:false %>'  onchange="CheckApprove(this);" 
                                                           />
                                                    <%-- Checkreject.BackColor != Color.Green--%>
                                                     <%--Checked='<%# Eval("SMARK").ToString()=="902.00" ? true:false %>' --%>
                                                 </td>
                                                  <td>
                                                 <asp:CheckBox ID="chk_ufm" runat="server" Checked='<%# Eval("ADVISOR_APPROVE").ToString()=="0" ? true:false %>'  onchange="CheckReject(this)" /><%--Checked='<%# Eval("SMARK").ToString()=="903.00" ? true:false %>' --%>
                                                 </td>                                 
         
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                
                                </asp:Panel>
                            </div>
                              <div class="col-12">
                                <asp:Panel ID="pnlSeqNum_admin" runat="server">
                                    <asp:ListView ID="ListView1" runat="server" Visible ="false">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SRNO </th>
                                                            <th>REG.NO</th>
                                                           <%-- <th>ENROLLNO</th>--%>
                                                            <th>STUDENT NAME</th>
                                                            <%--<th>Degree</th>--%>
                                                            <th>COURSE NAME</th>
                                                            <%--<th>Branch</th>--%>
                                                            <th>EXAM NAME</th>
                                                            <%-- <th>SUBEXAM NAME</th>--%>
                                                            <th>SEMESTER</th>
                                                            <th>APPLY DATE</th>
                                                            <th>EXISTING MARK</th>
                                                            <th>APPROVE</th>
                                                          <th>REJECT</th>
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
                                                    <asp:Label ID="lblExamNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' Font-Bold="true" />

                                               </td>                                                
                                                  <%--<td><asp:CheckBox ID="CheckId" runat="server"  ToolTip='<%# Eval("IDNO")%>'  /></td>--%>
                                                  <td> 
                                                     
                                                      <asp:Label ID="lblREGNO" runat="server" Text='<%#Eval("REGNO")%>' ToolTip='<%#Eval("FEES")%>' Font-Bold="true" />
                                                      <%--<asp:HiddenField ID="hdfexistMarks" Value='<%# Eval("EXISTS_MARK") %>' runat="server" />--%>
                                                  </td>                                                

                                               <%-- <td> <asp:Label ID="lblENROLLNO" runat="server" tooltip='<%#Eval("FEES")%>' Font-Bold="true" /></td>--%>

                                                <td> 
                                                    <asp:Label ID="lblSTUDNAME" runat="server" Text='<%#Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO") %>' Font-Bold="true" />

                                                </td>

                                                <td> 
                                                    <asp:Label ID="lblCOURSENAME" runat="server" Text='<%#Eval("COURSE_NAME")%>' ToolTip='<%# Eval("COURSENO")%>' Font-Bold="true" />

                                                </td>

                                                <td> 
                                                    <asp:Label ID="lblExamname" runat="server" Text='<%#Eval("EXAMNAMENEW")%>' Font-Bold="true" />

                                                </td>
                                               <%-- <td> 
                                                    <asp:Label ID="lblSubExamName" runat="server" Text='<%#Eval("SUBEXAMNAME")%>' Font-Bold="true" />

                                                </td>--%>
                                                <td> 
                                                    <asp:Label ID="lblSEMESTERNAME" runat="server" Text='<%#Eval("SEMESTERNO")%>' ToolTip='<%# Eval("SEMESTERNO") %>' Font-Bold="true" />

                                                </td>
                                                <td> 
                                                    <asp:Label ID="lblapplydate" runat="server" Text='<%#Eval("REG_DATE")%>'  Font-Bold="true" />

                                                </td>
                                                <td> 
                                                    <asp:Label ID="lblexistingmark" runat="server" Text='<%#Eval("EXISTING_TEST_MARK")%>' Font-Bold="true" />
                                                  <%--  <asp:HiddenField ID="hdffee" runat="server" text='<%#Eval("FEES") %>' />--%>

                                                </td>
                                                 <td>                                                              
                                                      <asp:CheckBox ID="chk_Absent1" runat="server"  onchange="CheckApprove2(this);" Checked='<%# Eval("ADMIN_APPROVE").ToString()=="1" ? true:false %>' />
                                                    <%-- Checkreject.BackColor != Color.Green--%>
                                                     <%--Checked='<%# Eval("SMARK").ToString()=="902.00" ? true:false %>' --%>
                                                 </td>
                                                  <td>
                                                 <asp:CheckBox ID="chk_ufm1" runat="server" onchange="CheckReject2(this)" Checked='<%# Eval("ADMIN_APPROVE").ToString()=="0" ? true:false %>' /><%--Checked='<%# Eval("SMARK").ToString()=="903.00" ? true:false %>' --%>
                                                 </td>                                 
         
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>


                        <div class="col-12">
                            <asp:Panel ID="pnldetails" runat="server"  Visible="false">
                                <asp:ListView ID="lvdetails" runat="server">
                                    <LayoutTemplate>
                                       <div class="sub-heading">
                                    <h5>ROOM CONFIGURATION</h5>
                                </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Sr No. </th>
                                                        <th>Enrollment No. </th>
                                                        <th>Student Name </th>
                                                        <th>Branch </th>
                                                        <th>Bench No </th>
                                                        <th>Room Name </th>
                                                        <th>Exam Date </th>
                                                        <th>Exam Slot </th>
                                                        <th>Present/Absent</th> 
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                       
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <td><%#Container.DataItemIndex+1 %></td>
                                        <td><%# Eval("REGNO")%>
                                            <asp:HiddenField ID="hfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                        </td>
                                        <asp:HiddenField ID="hdfLock" runat="server" Value='<%# Eval("AB_CC_LOCK")%>' />
                                        <td><%# Eval("STUDNAME")%></td>
                                        <td><%# Eval("SHORTNAME")%></td>
                                        <td><%# Eval("SEATNO")%></td>
                                        <td><%# Eval("ROOMNAME")%></td>
                                        <td><%# Eval("EXAMDATE")%></td>
                                        <td><%# Eval("SLOTNAME")%></td>
                                        <td>
                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO")%>' Checked='<%# Eval("EXTERMARK").ToString()=="-1.00" ? false:true %>' /></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="Panel1" runat="server" >
                                <asp:ListView ID="lvStudents" runat="server">
                                    <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th>Exam Roll No. </th>
                                                        <th>Student Name </th>
                                                        <th>Present/Absent Entry </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("REGNO")%>
                                                <asp:HiddenField ID="hfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td><%# Eval("STUDNAME")%></td>
                                            <td>
                                                <asp:CheckBox ID="cbRow" runat="server" Checked='<%# Eval("EXAMMARKTYPE").ToString()== "-1" ? true:false 

%>'
                                                    Enabled='<%# Eval("LOCKFIELD").ToString() == "1" ? false:true %>' ToolTip='<%# Eval("IDNO")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="Panel2" runat="server" >
                                <asp:ListView ID="lvMidsem" runat="server">
                                    <LayoutTemplate>
                                         <div class="sub-heading">
                                                <h5>Student List(Mid Exam)</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Enrollment No. </th>
                                                        <th>Registration Type </th>
                                                        <th>Present/Absent Entry </th>
                                                        <th>UFM Entry </th>
                                                        <th>Withheld Entry </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("REGNO")%>
                                                <asp:HiddenField ID="hdfLock" runat="server" Value='<%# Eval("AB_CC_LOCK")%>' />
                                                <asp:HiddenField ID="hfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td style="width: 15%"><%# Eval("EXAMTYPE")%></td>
                                            <td>
                                                <asp:CheckBox ID="cbRow" runat="server" Checked='<%# Eval("S2MARK").ToString()=="-1" ? true:false %>'
                                                    ToolTip='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkUFM" runat="server" Checked='<%# Eval("S2MARK").ToString()=="403" ? true:false %>'
                                                    onclick="checkAll(this);" ToolTip='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkWithheld" runat="server" Checked='<%# Eval("S2MARK").ToString()=="402" ? true:false 

%>'
                                                    ToolTip='<%# Eval("IDNO")%>' />
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

            <div id="divMsg" runat="server">
            </div>
            <script type="text/javascript" language="javascript">
                function checkAll(chk) {
                    var chkin;
                    var chkboxid = chk.id;
                    var len = chkboxid.length;
                    if (chk.checked == true) {
                        if (len == 46) {
                            chkin = chkboxid.substring(0, 40) + 'cbRow';

                            if (document.getElementById('' + chkin + '').checked == true) {
                                chk.checked = true;
                            }
                            else {
                                alert('Student Cannot Be UFM Case As He Was Not Present In The Paper..!! Make Sure He Was Present In The Paper For UFM Case!!');
                                chk.checked = false;
                                return;
                            }
                        }
                        else if (len == 47) {
                            chkin = chkboxid.substring(0, 41) + 'cbRow';

                            if (document.getElementById('' + chkin + '').checked == true) {
                                chk.checked = true;
                            }
                            else {
                                alert('Student Cannot Be UFM Case As He Was Not Present In The Paper..!! Make Sure He Was Present In The Paper For UFM Case!!');
                                chk.checked = false;
                                return;
                            }
                        }
                        else
                            if (len == 48) {
                                chkin = chkboxid.substring(0, 42) + 'cbRow';

                                if (document.getElementById('' + chkin + '').checked == true) {
                                    chk.checked = true;
                                }
                                else {
                                    alert('Student Cannot Be UFM Case As He Was Not Present In The Paper..!! Make Sure He Was Present In The Paper For UFM Case!!');
                                    chk.checked = false;
                                    return;
                                }
                            }
                    }
                }


                function totAllSubjects(headchk) {

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

                }
            </script>
                 <%--<script type="text/javascript">
                     $(".chk_up").change(function () {
                         //if ($(".chk_uf").attr("checked", false)) {
                         //    $(".chk_up").attr("checked", true);
                         //}
                         //else {
                         //    alert("You can't select both Upsent and UFM for single student !!");
                         //}
                         alert("ok");
                     });
                     var prm = Sys.WebForms.PageRequestManager;
                     prm.add_endRequest(function () {

                         $(".chk_ab").change(function () {
                             if ($(this).children().prop('checked') == true) {
                                 //$(".chk_uf").children().prop('checked', false);
                                 $(this).parent().parent().parent().children().eq(11).find(".chk_uf").children().prop('checked', false);
                             }
                         });

                         $(".chk_uf").change(function () {
                             if ($(this).children().prop('checked') == true) {
                                 //$(".chk_ab").children().prop('checked', false);
                                 $(this).parent().parent().parent().children().eq(10).find(".chk_ab").children().prop('checked', false);
                             }
                         });

                     });
            </script>--%>

            <script type="text/javascript">


                function CheckApprove(val) {
                    debugger;

                    try {
                        var length = $("[id*=mytable] td").closest("tr").length;

                        for (var i = 0; i < length; i++) {
                            var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_Absent");
                            var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                            if (chkid.type == "checkbox") {
                                if (chkid.checked == true) {
                                    //  var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkreject.disabled = true;
                                    chkid.disabled = false;
                                }
                                else {
                                    chkreject.disabled = false;

                                }
                            }
                            else if (chkreject.type == "checkbox") {
                                if (chkreject.checked == true) {
                                    //var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkid.disabled = true;
                                    chkreject.disabled = false;
                                }
                                else {
                                    chkid.disabled = false;
                                }
                            }
                            else {
                                chkid.disabled = false;
                                chkreject.disabled = false;
                            }
                        }

                        // }

                    }
                    catch (error) {
                        error.message;
                    }
                }
                function CheckApprove2(val) {
                    debugger;

                    try {
                        var length = $("[id*=mytable] td").closest("tr").length;

                        for (var i = 0; i < length; i++) {
                            var chkid = document.getElementById("ctl00_ContentPlaceHolder1_ListView1_ctrl" + i + "_chk_Absent1");
                            var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_ListView1_ctrl" + i + "_chk_ufm1");
                            if (chkid.type == "checkbox") {
                                if (chkid.checked == true) {
                                    //  var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkreject.disabled = true;
                                    chkid.disabled = false;
                                }
                                else {
                                    chkreject.disabled = false;

                                }
                            }
                            else if (chkreject.type == "checkbox") {
                                if (chkreject.checked == true) {
                                    //var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkid.disabled = true;
                                    chkreject.disabled = false;
                                }
                                else {
                                    chkid.disabled = false;
                                }
                            }
                            else {
                                chkid.disabled = false;
                                chkreject.disabled = false;
                            }
                        }

                        // }

                    }
                    catch (error) {
                        error.message;
                    }
                }
                function CheckReject(val) {
                    debugger;

                    try {
                        var length = $("[id*=mytable] td").closest("tr").length;

                        for (var i = 0; i < length; i++) {
                            //  var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_Absent");
                            var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                            var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_Absent")
                            if (chkreject.type == "checkbox") {
                                if (chkreject.checked == true) {
                                    //  var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkid.disabled = true;
                                    chkreject.disabled = false;

                                }
                                else {
                                    chkid.disabled = false;

                                }
                            }
                            else if (chkid.type == "checkbox") {
                                if (chkid.checked == true) {
                                    //var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkreject.disabled = true;
                                    chkid.disabled = false;
                                }
                                else {
                                    chkreject.disabled = false;
                                }
                            }
                            else {
                                chkreject.disabled = false;
                                chkid.disabled = true;
                            }
                        }

                        // }

                    }
                    catch (error) {
                        error.message;
                    }
                }
                function CheckReject2(val) {
                    debugger;

                    try {
                        var length = $("[id*=mytable] td").closest("tr").length;

                        for (var i = 0; i < length; i++) {
                            //  var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_Absent");
                            var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_ListView1_ctrl" + i + "_chk_ufm1");
                            var chkid = document.getElementById("ctl00_ContentPlaceHolder1_ListView1_ctrl" + i + "_chk_Absent1")
                            if (chkreject.type == "checkbox") {
                                if (chkreject.checked == true) {
                                    //  var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkid.disabled = true;
                                    chkreject.disabled = false;

                                }
                                else {
                                    chkid.disabled = false;

                                }
                            }
                            else if (chkid.type == "checkbox") {
                                if (chkid.checked == true) {
                                    //var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                                    chkreject.disabled = true;
                                    chkid.disabled = false;
                                }
                                else {
                                    chkreject.disabled = false;
                                }
                            }
                            else {
                                chkreject.disabled = false;
                                chkid.disabled = false;
                            }
                        }

                        // }

                    }
                    catch (error) {
                        error.message;
                    }
                }
            </script>
            <script>
                function myFunction() {
                    debugger;
                    var length = $("[id*=mytable] td").closest("tr").length;

                    for (var i = 0; i < length; i++) {
                        var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_ufm");
                        var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + i + "_chk_Absent")
                        if (chkreject.type == "checkbox" || chkid.type == "checkbox") {
                            var count = 0;
                            for (var j = 0; j < length; j++) {
                                var chkreject = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + j + "_chk_ufm");
                                var chkid = document.getElementById("ctl00_ContentPlaceHolder1_lvabsent_ctrl" + j + "_chk_Absent")
                                if (chkreject.checked == true || chkid.checked == true) {
                                    count++;
                                }
                            }
                            if (count == 0) {
                                alert('Please Select Atleast one Checkbox');
                                return false;
                            }

                            if (chkreject.checked == true) {
                                /// return true;

                                // chkreject.disabled = true;
                                //  chkid.disabled = false;
                            }
                            //else if ( chkid.checked == true) {
                            //    /// return true;
                            //    chkreject.disabled = false;
                            //}



                        }

                    }
                }
</script>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
