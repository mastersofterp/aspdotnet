<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TabulationChartPC.aspx.cs" Inherits="ACADEMIC_EXAMINATION_TabulationChartPC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
     </div>

     <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Tabulation Chart/Professional Courses</h3>
                        </div>
                            <div class="box-body">
                                <div class="col-md-8">
                                    <fieldset>
                                        <legend>Selection Criteria</legend>
                                        <div class="form-group col-md-4">
                                            <label>Admission Batch</label>
                                              <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary"
                                                    AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddladmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Tabulation"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Session</label>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary"
                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Tabulation"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>College</label>
                                             <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary"
                                                 AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Tabulation"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Degree</label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" ValidationGroup="Summary"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree"
                                                InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDegree" Display="None"
                                                ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Tabulation"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Branch</label>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" ValidationGroup="Summary"
                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None"
                                                ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlBranch" Display="None"
                                                ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Tabulation"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Scheme</label>
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="Tabulation">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Semester</label>
                                              <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="Summary" 
                                                  OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" 
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSemester" Display="None" 
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Tabulation"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester" Display="None" 
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Student Type </label>
                                             <asp:DropDownList ID="ddlStuType" runat="server" AppendDataBoundItems="True" AutoPostBack="True" 
                                                 onselectedindexchanged="ddlStuType_SelectedIndexChanged" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Result date</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtDeclareDate" TabIndex="6" ToolTip="Please Enter Date"></asp:TextBox>
                                                <%--<asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDeclareDate" PopupButtonID="imgExamDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtDeclareDate"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Date of Issue"
                                                    ControlExtender="meExamDate" ControlToValidate="txtDeclareDate" IsValidEmpty="false"
                                                    InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Issue"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDeclareDate"
                                                    Display="None" ErrorMessage="Please Select/Enter Date" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Date Of Issue</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtDateOfIssue" TabIndex="6" ToolTip="Please Enter Issue Date"></asp:TextBox>
                                                <%--<asp:Image ID="imageDateOFIssue" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceIssueDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateOfIssue" PopupButtonID="imageDateOFIssue" />
                                                <ajaxToolKit:MaskedEditExtender ID="MeIssueDate" runat="server" TargetControlID="txtDateOfIssue"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolKit:MaskedEditValidator ID="MvIssueDate" runat="server" EmptyValueMessage="Please Enter Issue Date"
                                                    ControlExtender="meExamDate" ControlToValidate="txtDateOfIssue" IsValidEmpty="false"
                                                    InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Issue Date"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Summary" SetFocusOnError="true" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDateOfIssue"
                                                    Display="None" ErrorMessage="Please Select/Enter Issue Date" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                          <asp:Label ID="lblmsg" runat="server" style="color:red;"></asp:Label>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-4">
                                     <fieldset class="fieldset" style="padding: 5px; color: Green">
                                            <legend class="legend">Note</legend>Please Select<br />
                                            <span style="font-weight: bold; color: Red;">Grade Card : </span>
                                            <br />
                                            Session->College->Degree->Branch->Semester->
                                         Admission Batch->Student type<br />
                                            <span style="font-weight: bold; color: Red;">Grade Card Without Header : </span>
                                            <br />
                                            Session->College->Degree->Branch->Semester->
                                         Admission Batch->Student type<br />
                                            <span style="font-weight: bold; color: Red;">Tabulation Report : </span>
                                            <br />
                                             Session->College->Degree->Branch->Semester->
                                         Admission Batch->Student type
                                       </fieldset>
                                        <br />
                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                </div>
                            </div>
                            <div class="box-footer">
                         <p class="text-center">
                               <asp:Button ID="btnBranchwise" Text="Summary Result Sheet Branchwise" runat="server"
                                            TabIndex="6" Visible="false" CssClass="btn btn-info"/>
                                      
                                        <asp:Button ID="btnReport" Text="Summary Result Sheet" runat="server"
                                            TabIndex="6" Visible="false" CssClass="btn btn-info"/>
                                     
                                        <asp:Button ID="btnGradeCard" Text="Grade Card" runat="server" TabIndex="8" 
                                            OnClick="btnGradeCard_Click1" ValidationGroup="Summary" CssClass="btn btn-info"/>
                                       
                                        <asp:Button ID="btnGradeCardHeader" runat="server" TabIndex="8" Text="Grade Card Without Header"
                                            ValidationGroup="Summary" OnClick="btnGradeCardHeader_Click" CssClass="btn btn-info"/>
                                        
                                        <asp:Button ID="btnSummary" runat="server" Text="Result Checklist" ValidationGroup="Summary"
                                            TabIndex="9" Visible="false" CssClass="btn btn-primary"/>
                                        
                                        <asp:Button ID="btnGradeCardBackReport" runat="server" TabIndex="8" Text="Grade Card Back Report"
                                              Visible="false"  CssClass="btn btn-info"/>
                                         
                                        <asp:Button ID="btnRankList" runat="server" Text="Rank List" ValidationGroup="Summary"
                                            TabIndex="9" Visible="false" CssClass="btn btn-info"/>

                                        <asp:Button ID="btntabulation" runat="server" Text="Tabulation Report" ValidationGroup="Tabulation"
                                            TabIndex="9" OnClick="btntabulation_Click" CssClass="btn btn-info"/>
                                     
                                        <asp:Button ID="brnphdresultSheet" runat="server" Text="P.hd Result sheet" ValidationGroup="Summary"
                                            TabIndex="9" Visible="false" CssClass="btn btn-info"/>
                                      
                                       <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Summary"/>
                                       <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Tabulation"/>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show"/>
                         </p>
                         <p class="text-center">
                              <asp:Button ID="btnPassedStud" Text="Passed Students" runat="server" CssClass="btn btn-info"
                                           ValidationGroup="Show" TabIndex="6" Visible="false" OnClick="btnPassedStud_Click"/>

                                        <asp:Button ID="btnNotificationPdf" Text="Notification (PDF)" runat="server" CssClass="btn btn-info"
                                            TabIndex="11" ValidationGroup="Tabulation" OnClick="btnNotificationPdf_Click" />

                                        <asp:Button ID="btnNotificationWord" Text="Notification (Word)" runat="server" CssClass="btn btn-info"
                                            TabIndex="12" ValidationGroup="Tabulation" OnClick="btnNotificationWord_Click" />

                                       <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Cancel"
                                            TabIndex="11"  CssClass="btn btn-warning"/>
                         </p>
                     </div>
                    </div>
                </div>
            </div>

            <table cellpadding="0" cellspacing="0" border="0" width="100%">
              
                <tr>
                    <td>
                       
                            <table style="width: 100%" cellpadding="2" cellspacing="2">
                           
                          

                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Panel ID="pnlStudent" Visible="false" runat="server" Width="700px" Height="500px"
                                            ScrollBars="both">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="vista-grid">
                                                        <div class="titlebar">
                                                            Select Student
                                                        </div>
                                                        <table class="datatable" cellpadding="0" cellspacing="0">
                                                            <tr class="header">
                                                                <th>
                                                                    Select
                                                                    <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" />
                                                                </th>
                                                                <th>
                                                                    Roll No.
                                                                </th>
                                                               <%-- <th>
                                                                     Roll No.
                                                                </th>--%>
                                                                <th>
                                                                   Student Name
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                        <td>
                                                            <asp:CheckBox ID="chkStudent" runat="server" ToolTip="Select to view tabulation chart" />
                                                        </td>
                                                        <td>
                                                             <%# Eval("REGNO")%>
                                                        </td>
                                                      <%--  <td>
                                                           <%# Eval("ROLL_NO")%>
                                                        </td>--%>
                                                        <td>
                                                            <asp:Label ID="lblStudname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
</asp:Content>

