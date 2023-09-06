<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExamRegistrationPhD.aspx.cs" Inherits="ACADEMIC_ExamRegistration" Title=""
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            blinkeffect('#divalert');
            blinkeffect('#divalert1');
        }
    )
        function blinkeffect(selector) {
            $(selector).fadeOut('slow', function () {
                $(this).fadeIn('slow', function () {
                    blinkeffect(this);
                });
            });
        }
    </script>
    <asp:Panel ID="pnlStart" runat="server">

        <div class="col-sm-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">PHD EXAM REGISTRATION </h3>
                    <%--  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />--%>
                </div>
                <br />
                <div class="box-body">

                    <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
                        <b>Note : </b>Steps to follow for Exam Registration.
            <table width="100%">
                <tr>
                    <td style="width: 50%">
                        <div style="width: 100%;">
                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                1. Please click Proceed to Exam Registration button.
                            </p>
                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                2. Please select the one semester in Backlog Semester dropdownlist and Click the
                                show button.
                            </p>
                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                3. Backlog (Supplimentary) Courses will be displayed on the below for selected semester.
                            </p>
                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                4. Please verify and select Backlog (Supplimentary) Courses.
                            </p>
                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                5. Finally Click the Submit Button.
                            </p>
                            <p>
                                6. Please print MIS generated challan and examination form.
                            </p>
                            <p>
                                7. Requisite fee should be submitted to bank and one copy challan should be submitted
                                with examination form to respective Head of the Department.
                            </p>
                            <p>
                                8. After confirmation by HoD student can able to print their admit card from MIS.
                            </p>
                            <p>
                                9. Admit card is a mandatory requirement in order to appear in examination.
                            </p>
                            <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                                <asp:Button ID="btnProceed" runat="server" Text="Proceed to Exam Registration" OnClick="btnProceed_Click" />
                            </p>
                            <p>
                            </p>
                        </div>
                    </td>
                    <td colspan="2"></td>
                    <%-- <td style="text-align: right">
                        <asp:Image ID="Image2" runat="server" Height="50%" ImageUrl="~/IMAGES/hindi instruction.jpg"
                            Width="80%" />
                    </td>--%>
                </tr>
            </table>
                        <table style="width: 30px; float: right;">
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="updEdit" runat="server">
                        <ContentTemplate>
                            <div class="col-12" id="divPanelSearch" runat="server">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Search Criteria</label>
                                </div>

                                <%--onchange=" return ddlSearch_change();"--%>
                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                <asp:Panel ID="pnltextbox" runat="server">
                                    <div id="divtxt" runat="server" style="display: block">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search String</label>
                                        </div>
                                        <%--onkeypress="return Validate()"--%>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlDropdown" runat="server">
                                    <div id="divDropDown" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <%-- <label id="lblDropdown"></label>--%>
                                            <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <%-- OnClientClick="return submitPopup(this.name);"--%>
                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                            <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="Panellistview" runat="server">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Name
                                                            </th>
                                                            <th style="display: none">IdNo
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th><%--Branch--%>
                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th><%--Semester--%>
                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>Father Name
                                                            </th>
                                                            <th>Mother Name
                                                            </th>
                                                            <th>Mobile No.
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </asp:Panel>
                                        </div>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                            </td>
                                            <td style="display: none">
                                                <%# Eval("idno")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("FATHERNAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("MOTHERNAME") %>
                                            </td>
                                            <td>
                                                <%#Eval("STUDENTMOBILE") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                           </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lvStudent" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                        <asp:Panel ID="pnlSearch" runat="server" Style="display: none">

                            <div class="col-sm-12 form-group">
                                <div class="col-sm-2 text-right">
                                    <label>Enter Roll No :</label>
                                </div>
                                <div class="form-group col-sm-4">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control"></asp:TextBox>

                                        <div class="input-group-btn">
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnProceed_Click" Text="Show" CssClass="btn btn-primary text-center" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row" id="divCourses" runat="server" visible="false">
                            <div class="col-sm-12">
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">PHD EXAM REGISTRATION </h3>
                                    </div>
                                    <div class="row" id="tblInfo" runat="server">
                                        <div class="col-sm-12">
                                            <div class="form-group col-sm-8 text-left">
                                                <div class="form-group col-sm-12 text-left">
                                                    <label>Student Name :</label>
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                </div>

                                                <div class="form-group col-sm-8 text-right">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                                </div>

                                                <div class="form-group col-sm-8 text-left">
                                                    Enrollment No. :
                                               <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                                </div>


                                                <div class="form-group col-sm-8 text-left">
                                                    Admission Batch :
                                               <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                                                    &nbsp;&nbsp; Semester :
                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-sm-12 text-left">
                                                    Degree / Branch :
                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp; PH :
                            <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label>
                                                </div>

                                                <div class="form-group col-sm-8 text-left">
                                                    Scheme : 
                                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-sm-8 text-left">
                                                    <span style="color: red">*</span> Backlog Semester :<asp:DropDownList ID="ddlBackLogSem" runat="server" AppendDataBoundItems="True"
                                                        ValidationGroup="backsem">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlBackLogSem"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hdfCategory" runat="server" />
                                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                                </div>
                                                <div class="form-group col-sm-8 text-center">
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click1" Text="Show" ValidationGroup="backsem"
                                                        Enabled="true" CssClass="btn btn-primary text-center" />
                                                    &nbsp;<asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Print Reciept"
                                                        ValidationGroup="backsem" Visible="false" CssClass="btn btn-primary text-center" />
                                                    <%--   <asp:Button ID="btnHallTicket" runat="server" Text="Hall Ticket" Visible="true" Enabled="true"
                                OnClick="btnHallTicket_Click" ValidationGroup="backsem" Width="15%" />--%>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="backsem"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>

                                            <%--   <div class="form-group col-sm-4 text-left">
                                                <asp:Image ID="imgPhoto" runat="server"  Width="150" Height="180" />
                                        </div>--%>
                                        </div>

                                        <div class="col-sm-12" id="divReceipts" runat="server" visible="false">
                                            <img alt="Show/Hide" style="display: none" src="../images/action_down.gif" onclick="ShowHideDiv('ctl00_ContentPlaceHolder1_divHidPreviousReceipts', this);" />
                                            <%-- <h4> <label class="label label-default">Exam Receipts</label></h4>--%>
                                            <div id="divHidPreviousReceipts" runat="server" style="text-align: center; color: red">
                                                <br />

                                                <asp:Repeater ID="lvPaidReceipts" runat="server">
                                                    <HeaderTemplate>
                                                        <div id="listViewGrid" class="vista-grid">
                                                            <div class="titlebar text-left">
                                                                <h4>
                                                                    <label class="label label-default">Exam Receipts Information</label>
                                                                </h4>
                                                            </div>
                                                        </div>
                                                        <table id="example2" class="table table-bordered table-hover table-fixed text-left ">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="display: none">Print
                                                                    </th>
                                                                    <%--  <th>
                                                                Receipt Type
                                                            </th>--%>
                                                                    <th>Receipt No
                                                                    </th>
                                                                    <th>Date
                                                                    </th>
                                                                    <th>Semester
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>
                                                                </tr>
                                                                <%--  <tr id="itemPlaceholder" runat="server" />--%>
                                                            <thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                            <td style="display: none">
                                                                <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click" />
                                                                <%--  CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>'--%>
                                                           
                                                            </td>
                                                            <%--  <td>
                                                        <%# Eval("RECIEPT_TITLE") %>--%>
                                                            <%-- <asp:Label ID="lblfeesdisplay" runat="server"  
                                                          Text='<%# (Eval("RECIEPT_TITLE").ToString())%>'></asp:Label>
                                                     <%--     == string.Empty ? "PHD EXAM FEES" : Eval("RECIEPT_TITLE")%>'></asp:Label>--%>
                                                            <%--</td>--%>
                                                            <td>
                                                                <%# Eval("REC_NO") %>
                                                            </td>
                                                            <td>
                                                                <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SEMESTERNAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("TOTAL_AMT") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody></table>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </div>
                                        </div>


                                        <div class="form-group col-sm-12 table-responsive">

                                            <asp:ListView ID="lvFailCourse" runat="server">
                                                <LayoutTemplate>
                                                    <div class="vista-grid">
                                                        <div class="titlebar">
                                                            <div class="titlebar">
                                                                <h4>
                                                                    <label class="label label-default">Course List </label>
                                                                </h4>
                                                            </div>
                                                            <table id="id2" class="table table-bordered table-hover text-center">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>
                                                                            <%--<asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />--%>
                                                                    Select
                                                                        </th>
                                                                        <th>Course Code
                                                                        </th>
                                                                        <th>Course Name
                                                                        </th>
                                                                        <th>Semester
                                                                        </th>
                                                                        <th>Theory/Prac
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </thead>
                                                            </table>
                                                        </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow" class="item">
                                                        <td>
                                                            <%-- <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' Enabled='<%# Eval("ACCEPTED").ToString() == "0" ? true : false %>' />--%>
                                                            <asp:CheckBox ID="chkAccept" runat="server" Checked="true" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("SEMESTER") %>
                                                        </td>
                                                        <td style="font-weight: bold" align="center">
                                                            <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : "Practical"%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <%--<EmptyDataTemplate>
                                            <span style="background-color: #00E171; font-size:large; font-style:normal; border:1px solid #000000;">
                                                  No Backlog Courses.
                                            </span>
                                            </EmptyDataTemplate>--%>
                                            </asp:ListView>

                                        </div>
                                        <div class="form-group col-sm-12 table-responsive">

                                            <asp:ListView ID="lvFailInaggre" runat="server">
                                                <LayoutTemplate>
                                                    <div class="vista-grid">
                                                        < class="titlebar">
                                                       
                                                     <h4>
                                                         <label class="label label-default">Improve Fail In Aggregate</label>
                                                     </h4>
                                                    </div>
                                                    <table id="id2" class="table table-bordered table-hover text-center">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>
                                                                    <%--<asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />--%>
                                                                    Select
                                                                </th>
                                                                <th>Course Code
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Semester
                                                                </th>
                                                                <th>Theory/Prac
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow" class="item">
                                                        <td>
                                                            <asp:CheckBox ID="chkAcceptSub" runat="server" onclick="CheckSelectionCount(this)" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("SEMESTER") %>
                                                        </td>
                                                        <td style="font-weight: bold" align="center">
                                                            <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : "Practical"%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                        <div class="col-sm-12 text-center">
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary text-center" Font-Bold="true" />
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-12">
                            <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                        </div>


                        <div id="divMsg" runat="server">
                        </div>
                 
                </div>
            </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headchk) {
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
        function CheckSelectionCount(chk) {
            var count = -1;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }


    </script>









    <%--    <script src="../plugins/jQuery/jQuery-2.2.0.min.js"></script>

   
    <script>
        $(function () {
            $("#example2").DataTable();
            $('#example1').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false,
                "ordering": true,
                "info": true,
                "autoWidth": false
            });
        });
</script>--%>
</asp:Content>
