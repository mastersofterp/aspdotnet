<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NoDuesStsForAdmitCard.aspx.cs"
    Inherits="ACADEMIC_NoDuesStsForAdmitCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   

<%--    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        $.noConflict();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
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
    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Bulk Dues Status Allotment</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="label-dynamic" style="display: none">
                                    <sup>* </sup>
                                    <label>Search By</label>
                                </div>
                                <asp:RadioButton ID="rdoSingleStudent" runat="server" Text="Single Student" GroupName="search" Visible="false" AutoPostBack="true" OnCheckedChanged="rdoSingleStudent_CheckedChanged" />
                                <asp:RadioButton ID="rdoBulkStudent" runat="server" Text="Bulk Student" Checked="true" GroupName="search" AutoPostBack="true" Visible="false" OnCheckedChanged="rdoBulkStudent_CheckedChanged" />
                                <asp:RadioButton ID="rdoIdno" runat="server" Text="Id No." Checked="true" GroupName="search" Visible="false" />
                            </div>
                            <asp:Panel ID="pnlBulkStud" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <%--<asp:TextBox ID="lblSession" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="4"
                                                AutoPostBack="True" ToolTip="Please Select Session" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="DuesStatus"></asp:RequiredFieldValidator>
                                            <%--<asp:Label ID="lblSession" runat="server"  ></asp:Label>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>School/Institute Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" TabIndex="5" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" data-select2-enable="true"
                                                AutoPostBack="True" ToolTip="Please Select School/Institute Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                                Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="6"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Branch/Programme</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="7"
                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Branch/Programme" AutoPostBack="True" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch/Programme" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" TabIndex="8" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Dues</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDuesType" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="9">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Dues Clear</asp:ListItem>
                                                <asp:ListItem Value="2">Dues Pending</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlDuesType"
                                        Display="None" ErrorMessage="Please Select Dues" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Date of Issue</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtDateofissue" TabIndex="10" ToolTip="Please Enter Date"></asp:TextBox>
                                                <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateofissue" PopupButtonID="imgExamDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtDateofissue"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Date of Issue"
                                                    ControlExtender="meExamDate" ControlToValidate="txtDateofissue" IsValidEmpty="false"
                                                    InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Issue"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateofissue"
                                            Display="None" ErrorMessage="Please Select/Enter Date" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="11" AutoPostBack="true" OnSelectedIndexChanged="rbRegEx_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True">&nbsp;Regular Student &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1">&nbsp;Ex Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <%--OnClientClick="return Enable_Radio(this);"--%>
                                    <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click" TabIndex="12"
                                        ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="13"
                                        ValidationGroup="show" CssClass="btn btn-primary" OnClientClick="return validateAssign();" CausesValidation="false" />
                                    <asp:Button ID="btnDuesStatus" runat="server" Text="Dues Status Allotment" TabIndex="14"
                                        OnClick="btnDuesStatus_Click" CssClass="btn btn-primary" ValidationGroup="DuesStatus" />
                                    <asp:Button ID="btnPrintReport" runat="server" Text="Admit Card" TabIndex="15" CssClass="btn btn-info"
                                        OnClick="btnPrintReport_Click" ToolTip="Print Card under Selected Criteria." ValidationGroup="show" Visible="false" />
                                    <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" TabIndex="16" CssClass="btn btn-primary"
                                        OnClick="btnSendEmail_Click1" ToolTip="Send Card By Email" ValidationGroup="show" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="17"
                                        ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />
                                </div>
                                <div class="form-group col-12">
                                    <%-- <label>Total Selected</label>--%>
                                    <%--   <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" 
                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>--%>
                                    <%--  Reset the sample so it can be played again --%>
                                    <%--  <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                    <asp:HiddenField ID="hftot" runat="server" />
                                    <asp:HiddenField ID="txtTotStud" runat="server" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudentRecords" runat="server" OnItemDataBound="lvStudentRecords_ItemDataBound">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table id="tblStudent" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr id="Tr1" runat="server">
                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" />
                                                            </th>
                                                            <th>Reg. No.
                                                            </th>
                                                            <th>Roll No.</th>
                                                            <th>Student Name
                                                            </th>
                                                            <th style="display: none">Student Email
                                                            </th>
                                                            <th>Branch/Programme
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Dues Status</th>
                                                        </tr>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </thead>
                                                </table>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <%--  <asp:UpdatePanel runat="server" ID="updList">
                                                <ContentTemplate>--%>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkReport" runat="server" ToolTip='<%# Eval("IDNO") %>' Checked='<%# Convert.ToInt32(Eval("NODUES_STATUS")) == 1 ? true : false %>' onClick="totStudents(this);" />
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td><%# Eval("ROLLNO")%></td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                        <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                    </td>
                                                    <td style="display: none">
                                                        <%# Eval("EMAILID_INS")%>
                                                        <asp:HiddenField ID="Hdfemail" runat="server" Value='<%# Eval("EMAILID_INS") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("LONGNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                        <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblStatus" Text='<%# Convert .ToInt32(Eval("NODUES_STATUS"))==1 ? "Dues Clear" : "Dues Pending" %>'
                                                            ForeColor='<%# Convert .ToInt32(Eval("NODUES_STATUS"))==1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Font-Bold="true"></asp:Label>
                                                        <%--<asp:RadioButton ID="rdoYes" runat="server"  GroupName="Sex" TabIndex="10" Text="Complete" Checked="true" ></asp:RadioButton>
                                                     <asp:RadioButton ID="rdoNo" runat="server" GroupName="Sex" TabIndex="11" Text="Pending" ></asp:RadioButton>--%>
                                                    </td>
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
                                    ShowSummary="False" ValidationGroup="DuesStatus" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                                <div id="divMsg" runat="server">
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlSingleStud" runat="server" Visible="false">
                                <div id="pnlSearch" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Session Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSessionSingleStud" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Session"
                                                    ControlToValidate="ddlSessionSingleStud" Display="None" ValidationGroup="search" InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Enter Reg. No</label>
                                                </div>
                                                <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search."  TabIndex="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                                    Display="None" ErrorMessage="Please enter text to search." SetFocusOnError="true"
                                                    ValidationGroup="search" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="search" />
                                                <asp:Button ID="btnSearch" runat="server" OnClick="btnProceed_Click" Text="Show" ValidationGroup="search" CssClass="btn btn-primary" TabIndex="3"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divCourses" runat="server" visible="false">
                                    <div class="col-12" runat="server" id="noDuesSingleStud">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="false" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Enrollment No :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="false" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Admission Batch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="false" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="false" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Degree / Branch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="false" />
                                                            <asp:Label ID="lblDegrreno" runat="server" Font-Bold="false" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Scheme :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="false" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>College :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSingCollege" runat="server" Font-Bold="false" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>No Dues Status :</b>
                                                        <a class="sub-label">
                                                            <asp:RadioButton ID="rdoYesSingle" runat="server" GroupName="Sex" TabIndex="10" Text="Complete" Style="color: green" Checked="true"></asp:RadioButton>
                                                            <asp:RadioButton ID="rdoNoSingle" runat="server" GroupName="Sex" TabIndex="11" Text="Pending" Style="color: red"></asp:RadioButton>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                                                <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                            </div>
                                            <div class="col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>Photo</label>
                                                </div>
                                                <asp:Image ID="imgPhoto" runat="server" Width="50%" Height="80%" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-3 btn-footer">
                                        <%--<asp:Button ID="Button1" runat="server" OnClick="btnShow_Click1" Text="Show Courses" ValidationGroup="backsem"
                                            CssClass="btn btn-primary" />--%>
                                        <%--<asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Print Reciept"
                                                ValidationGroup="backsem" CssClass="btn btn-primary" Visible="false" />--%>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        <asp:Button ID="btnSubmitForSingleStu" runat="server" OnClick="btnSubmitForSingleStu_Click" Text="Submit"
                                            Font-Bold="true" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </asp:Panel>
                            
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvStudentRecords" />
            <%--<asp:PostBackTrigger ControlID="btnSearch" />--%>
            <asp:PostBackTrigger ControlID="btnDuesStatus" />
        </Triggers>
        <%--<Triggers>   
            <asp:AsyncPostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
               <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <script type="text/javascript">
    </script>
    <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = false;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = false;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = true;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = true;
                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').prop('checked', false);
                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').prop('checked', false);
                    }
                }
            }
        }
        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgCollegeLogo").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeLogo").value;
        }
    </script>
    <script type="text/javascript">
        var count = 0;
        var count2 = 0;
        var isClicked = false;
        function Call() {
            debugger;
            if (document.getElementById('tblStudent') != null) {
                var dataRows = document.getElementById('tblStudent').getElementsByTagName('tr');
                if (dataRows.length > 0) {
                    isClicked = true;
                    for (i = 0; i <= (dataRows.length - 1) ; i++) {
                        if ((document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport').checked) && (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport').disabled == false)) {
                            //if (isClicked) {
                            // alert('checked')
                            totSelectedCount();
                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = false;
                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = false;
                            //alert(count);
                            isClicked = false;
                            // }
                        }
                        else if (!document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport').checked) {
                            //alert('unchecked')
                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = true;
                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = true;

                            $('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').prop('checked', false);
                            $('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').prop('checked', false);

                            //for (j = 0; j <= (dataRows.length - 1) ; j++) {
                            //    if (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + j + '_chkReport').checked) {
                            //        if (isClicked) {
                            //            alert('aaaaaaaaaa')
                            //            totSelectedCount();
                            //            isClicked = false;
                            //        }
                            //    }
                            //}
                        }
                    }

                }
            }
        }
        function totSelectedCount() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            txtTot.value = Number(txtTot.value) + 1;
        }
        function totStudents(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

    </script>
    <%--<script>
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable() {
            var myDT = $('#tblStudent').DataTable({
                "bDestroy": true,
                "bPaginate": false,
                "lengthChange": false
                //'lengthMenu': [50, 100, 250]

            });
        }
    </script>--%>
</asp:Content>
