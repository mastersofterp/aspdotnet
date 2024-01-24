<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentAdmitCardReport.aspx.cs" Inherits="ACADEMIC_StudentAdmitCardReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EXAM ADMIT CARD</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%-- <div class="col-lg-3 col-md-6 col-12 form-group">--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>School/Institute Name/Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" CssClass="form-control"
                                            AutoPostBack="True" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Institute Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <%--<asp:TextBox ID="lblSession" runat="server"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                            AutoPostBack="True" ToolTip="Please Select Session" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="eligible"></asp:RequiredFieldValidator>
                                        <%--<asp:Label ID="lblSession" runat="server"  ></asp:Label>--%>
                                    </div>

                                    <%--<div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                            AutoPostBack="True" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>--%>

                                    <%--<div id="Div1" class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" data-select2-enable="true" runat="server" AppendDataBoundItems="True" TabIndex="3"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select scheme" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>--%>


                                    <div class="col-lg-3 col-md-6 col-12 form-group" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" data-select2-enable="true" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" data-select2-enable="true" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Branch" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" data-select2-enable="true" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" data-select2-enable="true" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Section" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Date of Issue</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="imgExamDate1"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtDateofissue" TabIndex="1" ToolTip="Please Enter Date"></asp:TextBox>
                                            <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateofissue" PopupButtonID="imgExamDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtDateofissue"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Date of Issue"
                                                ControlExtender="meExamDate" ControlToValidate="txtDateofissue" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Issue"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateofissue" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Select/Enter Date of Issue" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group" id="divRemark" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" ToolTip="Please Enter Remark" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark" ErrorMessage="Please Enter Remark"
                                            ValidationGroup="Report" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamname" runat="server" data-select2-enable="true" AppendDataBoundItems="True" ToolTip="Please Select Semester" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlExamname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamname" SetFocusOnError="True"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnEligibleStudReport" runat="server" Text="Eligible Student List" Visible="false" TabIndex="1" CssClass="btn btn-info"
                                    OnClick="btnEligibleStudReport_Click" ToolTip="Print Eligible Student under Selected Criteria." ValidationGroup="eligible" />

                                <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click" TabIndex="1"
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" CssClass="btn btn-primary" />

                                <asp:Button ID="btnPrintReport" runat="server" Text="Admit Card" TabIndex="1" CssClass="btn btn-info"
                                    OnClick="btnPrintReport_Click" ToolTip="Print Card under Selected Criteria." Enabled="false" ValidationGroup="Report" />
                                <asp:Button ID="btnAttendance" runat="server" Text="Attendance Report" TabIndex="5" CssClass="btn btn-info"
                                     ToolTip="Print Card under Selected Criteria." Visible="true"  OnClick="btnAttendance_Click" ValidationGroup="Report" />

                                <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" TabIndex="999" CssClass="btn btn-info"
                                    OnClick="btnSendEmail_Click1" ToolTip="Send Card By Email" ValidationGroup="show" Visible="false" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="1"
                                    ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="eligible" />

                                <asp:ValidationSummary ID="vsRemark" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />

                            </div>
                            <div class="col-md-2" style="display: none">
                                <label>
                                    Total Selected</label>
                                <asp:TextBox ID="txtTotStud" runat="server" Enabled="False" Font-Bold="True" Font-Size="Small" Style="text-align: center;" Text="0"></asp:TextBox>
                                <%--  Reset the sample so it can be played again --%>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" Enabled="True" TargetControlID="txtTotStud" WatermarkCssClass="watermarked" WatermarkText="0" />
                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>
                            <div class="col-12 ">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvStudentRecords" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblStudents">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>

                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" />
                                                            </th>
                                                            <th>Reg. No. </th>
                                                            <th>Student Name </th>
                                                            <th style="display: none">Student Email </th>
                                                            <th>Semester </th>
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
                                                    <asp:CheckBox ID="chkReport" runat="server" onClick="totSubjects(this);" ToolTip='<%# Eval("IDNO") %>' />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td><%# Eval("REGNO")%></td>
                                                <td><%# Eval("STUDNAME")%>
                                                    <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                </td>
                                                <td style="display: none"><%# Eval("EMAILID_INS")%>
                                                    <asp:HiddenField ID="Hdfemail" runat="server" Value='<%# Eval("EMAILID_INS") %>' />
                                                </td>
                                                <td><%# Eval("SEMESTERNAME")%></td>
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

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="lvStudentRecords" />
            <%--<asp:PostBackTrigger ControlID="btnShow" />--%>
        </Triggers>
    </asp:UpdatePanel>
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
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

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
    <%-- <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tblStudents').DataTable({
                //scrollX: 'true'
                //"pageLength": 10
            });
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn-info {
            margin-bottom: 0px;
        }
    </style>
</asp:Content>

