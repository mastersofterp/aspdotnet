<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Exam_Registration_Report.aspx.cs" Inherits="Exam_Registration_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EXAM REGISTRATION REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                            </div>
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" runat="server" id="spInstitue">
                                            <sup>*</sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AutoPostBack="True" AppendDataBoundItems="true" ToolTip="Please Select Institute"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvCollegeInst" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="ShowExcel"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSessionExcel" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="ShowExcel"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" runat="server" id="spDegree">
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        --%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" runat="server" id="spBranch">
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--   <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        --%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="spScheme" runat="server">
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvScheme" runat="server" InitialValue="0" SetFocusOnError="true"
                                                ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Scheme"
                                                ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" runat="server" id="spSemester">
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" CssClass="form-control" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic" runat="server" id="spCourse">
                                            <sup>* </sup>
                                            <label>Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" data-select2-enable="true" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" Enabled="false" Visible="false" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic" runat="server" id="spStudenttype">
                                            <label>Student Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true"
                                            CssClass="form-control" TabIndex="8">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Ex</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--   <asp:RequiredFieldValidator ID="rfvExamType" runat="server" Enabled="false"  ControlToValidate="ddlExamType"
                                                Display="None"
                                                ErrorMessage="Please Select Student Type."
                                                InitialValue="-1" SetFocusOnError="true"
                                                ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Order By</label>
                                        </div>
                                        <asp:DropDownList ID="ddlOrderBy" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Rollno</asp:ListItem>
                                            <asp:ListItem Value="1">Enrollno</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="label-dynamic">
                                    <label>Report</label>
                                </div>
                                <asp:RadioButtonList ID="rdbReport" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbReport_SelectedIndexChanged"
                                    AutoPostBack="true" RepeatColumns="2" TabIndex="1" CssClass="col-12 radio-button-list-style">
                                    <%--<asp:ListItem Selected="True" Value="1">Students who have filled exam form</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="2">Students who have yet to fill exam form</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="13">Students exam form approved by department</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="14">Students exam form yet to approved by department</asp:ListItem>--%>
                                    <asp:ListItem Value="3" Selected="True">Students whose payment is confirmed</asp:ListItem>
                                    <%--<asp:ListItem Value="4">Students who have filled exam form but payment pending</asp:ListItem>--%>
                                    <asp:ListItem Value="15">Students who have not to filled exam form</asp:ListItem>
                                    <%-- <asp:ListItem  Value="5">&nbsp;&nbsp;Student Attendance List</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="9">Students who yet to download the admit card</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="10">Students who have downloaded admit cards</asp:ListItem>--%>
                                    <%-- Commented by swapnil dated on 06082021 for not avaliable detained student.--%>
                                    <%--<asp:ListItem Value="11">&nbsp;&nbsp;Students who are detained</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="6">Exam form fill and Exam Registration Summary</asp:ListItem>--%>
                                    <%--<asp:ListItem Value="12">Exam form fill and Exam Registration Status</asp:ListItem>--%>
                                    <asp:ListItem Value="7">Session wise Exam Registration Count</asp:ListItem>
                                    <asp:ListItem Value="8">Session wise Exam Registration Details</asp:ListItem>
                                    <asp:ListItem Value="16">Student Registered but Mark Entry not Done</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="BtnShow" runat="Server" Text="Show" ValidationGroup="Show" OnClick="BtnShow_Click"
                                    CssClass="btn btn-primary" TabIndex="1" />
                                <asp:Button ID="btnReport" runat="Server" Text="Report" ValidationGroup="Show" OnClick="btnReport_Click"
                                    CssClass="btn btn-info" TabIndex="1" Style="display: none" />
                                <asp:Button ID="btnExcel" runat="Server" Text="Excel Report" ValidationGroup="ShowExcel"
                                    OnClick="btnExcel_Click" CssClass="btn btn-info" TabIndex="1" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="1" />

                                <asp:ValidationSummary ID="VSERR" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ShowExcel" />

                            </div>

                            <asp:Panel runat="server" ID="pnlSendSMSEmail">
                                <div class="col-12" id="divSendOptions" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sending Options</label>
                                            </div>
                                            <asp:RadioButtonList ID="rbNet" runat="server" TextAlign="Right" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbNet_SelectedIndexChanged" AutoPostBack="true" onclick="EmailHiding(this);" TabIndex="1">
                                                <asp:ListItem Value="1">SMS &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Email</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSubject" runat="server">
                                            <div id="lblSubject" class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Subject</label>
                                            </div>
                                            <asp:TextBox ID="txtSubject" Style="margin-bottom: 10px; display: none" runat="server" CssClass="form-control" TabIndex="1" AutoComplete="OFF" AutoCompleteType="Disabled" ToolTip="Please Enter the Subject" placeholder="Please Enter the Subject"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divMessage" runat="server">
                                            <div id="lblMessage" class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Message</label>
                                            </div>
                                            <asp:TextBox ID="txtMatter" runat="server" AutoComplete="OFF" Style="margin-bottom: 10px; display: none" TextMode="MultiLine" CssClass="form-control" TabIndex="1" ToolTip="Please Enter the Message" placeholder="Please Enter the Message"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMatter"
                                                Display="None" ErrorMessage="Please Enter the Message" SetFocusOnError="true"
                                                ValidationGroup="Email"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSendSMS" runat="server" Text="Send Email & SMS" Enabled="true" Style="display: none" Visible="false" OnClick="btnSendSMS_Click" ValidationGroup="Email" CssClass="btn btn-primary" TabIndex="1" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                    <asp:ValidationSummary ID="VSEmail" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Email" />
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                <asp:HiddenField ID="hdncount1" runat="server" />
                                <asp:HiddenField ID="hdncount2" runat="server" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Students List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" id="tbluser" style="width: 100%;">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">
                                                            <asp:CheckBox ID="chkAll" runat="server" ToolTip="Select All" onclick="SelectAll(this);" />
                                                        </th>
                                                        <th style="text-align: center">Sr.No.</th>
                                                        <th>Student Name</th>
                                                        <th>Registration No. </th>
                                                        <th>Mobile No.</th>
                                                        <th>Email ID</th>
                                                        <%-- <th>STATUS</th>--%>
                                                        <th id="BatchTheory1" style="text-align: center">STATUS
                                                                   
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="Tr1" runat="server">
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("REGNO") %>' /></td>
                                                <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>

                                                <td>
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div id="Div2" class="col-12 btn-footer" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Attach files</label>
                                        </div>
                                        <asp:FileUpload ID="fuAttachment" runat="server" AllowMultiple="true" TabIndex="14" />
                                        <div id="divMsg" runat="server"></div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnSendSMS" />
            <asp:PostBackTrigger ControlID="rbNet" />
            <asp:PostBackTrigger ControlID="BtnShow" />
        </Triggers>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function onreport() {
            var a = document.getElementById("ctl00_ContentPlaceHolder1_rdbReport_4");
            if (a.checked) {
                document.getElementById("ctl00_ContentPlaceHolder1_rfvDegree").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvBranch").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvProgram").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvSemester").enabled = false;
            }
        }
    </script>
    <script>
        function EmailHiding(rdo) {

            var selectedvalue = $("#" + rdo.id + " input:radio:checked").val();
            if (selectedvalue == "1") {
                document.getElementById("lblSubject").style.display = 'none';
                document.getElementById("lblMessage").style.display = 'block';
                document.getElementById("<%=txtMatter.ClientID%>").style.marginBottom = '10px';
                document.getElementById("<%=txtMatter.ClientID%>").style.display = 'block';
                document.getElementById("<%=txtSubject.ClientID%>").style.display = 'none';
                document.getElementById("<%=txtSubject.ClientID%>").style.marginBottom = '10px';
                document.getElementById("<%=btnSendSMS.ClientID%>").style.display = 'block';
                document.getElementById("<%=btnSendSMS.ClientID%>").value = 'Send SMS';
            }
            else if (selectedvalue == "2") {
                document.getElementById("lblSubject").style.display = 'block';
                document.getElementById("lblMessage").style.display = 'block';
                document.getElementById('<%=txtMatter.ClientID%>').style.display = 'block';
                document.getElementById('<%=txtSubject.ClientID%>').style.display = 'block';
                document.getElementById("<%=txtMatter.ClientID%>").style.marginBottom = '10px';
                document.getElementById("<%=txtSubject.ClientID%>").style.marginBottom = '10px';
                document.getElementById("<%=btnSendSMS.ClientID%>").style.display = 'block';
                document.getElementById("<%=btnSendSMS.ClientID%>").value = 'Send Email';
            }
    }

    </script>



    <%--<style>
        .tableFixHead          { overflow-y: auto; height: 300px; }
.tableFixHead thead th { position: sticky; top: 0; }

/* Just common table stuff. Really. */
table  { border-collapse: collapse; width: 100%; }
th, td { padding: 8px 16px; }
th     { background:#758aa8; }


    </style>
    --%>





    <%--   <script>
       $(document).ready(function () {
           $('#tableFixHead').DataTable({
               "scrollY": 200,
               "scrollX": true,
               "paging": true,
               "lengthChange": false,
               "searching": true,
               "ordering": false,
               "info": false,
               "autoWidth": false
           });

       });
    </script>--%>







    <script>

        function SelectAll(chk) {
            //debugger;
            var hdnVal = document.getElementById('<%= hdncount1.ClientID %>');
            //alert(hdnVal.value);


            for (i = 0; i < hdnVal.value; i++) {
                // alert('aa');
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_chkSelect');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }
        }
    </script>


    <%--<script>
                $(document).ready(function () {
                   // alert('ff');
                    bindDataTable();
                    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
                });
                function bindDataTable() {
                    //alert('aa');
                    //var myDT = $('#tableFixHead').DataTable({
                    //    "bDestroy": true,

                    //});
                    var myDT = $('#studlist').DataTable({
                        "bDestroy": true,
                    });
                }

    </script>--%>
    <%-- <script>
        $(document).ready(function () {
            $('table.display').DataTable();
        });
    </script>--%>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('#studlist').DataTable();
        //});
        //$(document).ready(function () {
        //    $('#studlist').DataTable({
        //        scrollY: '50vh',
        //        "scrollX": true,
        //        // scrollCollapse: true,
        //        "paging": true,
        //        "ordering": true,

        //        "info": true
        //    }
        //    );
        //});

        //var parameter = Sys.WebForms.PageRequestManager.getInstance();
        //parameter.add_endRequest(function () {
        //    $('#studlist').DataTable({
        //        scrollY: '50vh',
        //        //  scrollCollapse: true,
        //        "scrollX": true,
        //        "paging": false,
        //        "margin-top": '3vh',
        //        "ordering": false,
        //        "info": false,

        //        "columnDefs": [
        //      { "width": "10%", "targets": 0 }]
        //    }
        //    );
        //});

    </script>




    <script>
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tbluser').DataTable({
                "bPaginate": false,
                'pageLength': 50,
                'lengthMenu': [50, 100, 200, 500]
            });
        }
    </script>









</asp:Content>
