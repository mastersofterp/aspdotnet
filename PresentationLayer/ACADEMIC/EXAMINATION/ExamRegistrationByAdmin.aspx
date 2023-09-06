<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExamRegistrationByAdmin.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ExamRegistrationByAdmin"
    Title="" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlStart" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    EXAM REGISTRATION CONFIRMATION
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                </td>
            </tr>
        </table>
        <br />
        <div id="divNote" runat="server" visible="false" style="border: 2px solid #C0C0C0;
            background-color: #FFFFCC; padding: 20px; color: #990000;">
            <b>Note : </b>Steps to follow for Exam Registration.
            <div style="padding-left: 20px; padding-right: 20px">
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    1. Please Check the list of courses for Regular and Backlog (Supplimentary)
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    2. By default, Regular Courses will be displayed and selected on the left hand side.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    3. Backlog (Supplimentary) Courses will be displayed on the right hand side.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    4. Please verify and select Backlog (Supplimentary) Courses.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    5. Finally Click the Submit Button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                    <asp:Button ID="btnProceed" runat="server" Text="Proceed to Exam Registration" OnClick="btnProceed_Click" />
                </p>
            </div>
        </div>
        <div id="divCourses" runat="server" visible="true">
            <fieldset class="fieldset">
                <legend class="legend">Exam Registration</legend>
                <table cellpadding="0" id="tblSearch" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Enter Roll No :
                        </td>
                        <td>
                            &nbsp<asp:TextBox ID="txtEnrollno" runat="server" Width="25%"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click1" Text="Show" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="100%" id="tblInfo" runat="server">
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Student Name :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                        </td>
                        <td width="20%" rowspan="5" valign="top">
                            <asp:Image ID="imgPhoto" runat="server" Width="70%" Height="70%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            &nbsp;
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Enrollment No. :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Admission Batch :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                            &nbsp;&nbsp; Semester :
                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Degree / Branch :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PH :
                            <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Scheme :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="20%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <table width="100%">
                                <tr>
                                    <td width="50%" valign="top">
                                        <asp:ListView ID="lvFailSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        Exam Registered Course List</div>
                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                        <thead>
                                                            <tr class="header">
                                                                <%--<th>
                                                                    Delete
                                                                </th>--%>
                                                                <th>
                                                                    Course Code
                                                                </th>
                                                                <th>
                                                                    Course Name
                                                                </th>
                                                                <th>
                                                                    Semester
                                                                </th>
                                                                <th>
                                                                    Theory/Prac
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow" class="item">
                                                    <%-- <td>
                                                   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("COURSENO") %>'
                                                       ToolTip="Delete Record" OnClientClick="return confirm('Yor are deleting record permanently. \r\n Are you sure ?');"
                                                        OnClick="btnDelete_Click" />
                                                         <asp:HiddenField ID="hdfExamSem" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                       <asp:CheckBox ID="chkAccept" runat="server" Checked="true" Enabled="false" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblExamsem" runat="server" Text='<%# Eval("EXAMSEMESTER") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSubname" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <span style="background-color: #00E171; font-size: large; font-style: normal; border: 1px solid #000000;">
                                                     Registration Confirmed for Backlog Courses </span>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <div id="divHidPreviousReceipts" runat="server" style="text-align: center;">
                                <asp:ListView ID="lvPaidReceipts" runat="server">
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <div class="titlebar">
                                                Receipts Information</div>
                                            <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                <thead>
                                                    <tr class="header">
                                                        <th>
                                                            Select
                                                        </th>
                                                        <th>
                                                            Receipt Type
                                                        </th>
                                                        <th>
                                                            Receipt No
                                                        </th>
                                                        <th>
                                                            Date
                                                        </th>
                                                        <th>
                                                            Semester
                                                        </th>
                                                        <th>
                                                            Amount
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
                                                <asp:CheckBox ID="chkAccept" runat="server" Checked="true" ToolTip='<%# Eval("DCR_NO") %>' />
                                            </td>
                                            <td>
                                                <%# (Eval("RECIEPT_TITLE").ToString()) == string.Empty ? "SUPPLIMENTARY EXAM FEES" : Eval("RECIEPT_TITLE")%>
                                            </td>
                                            <td>
                                                <%# Eval("REC_NO") %>
                                            </td>
                                            <td>
                                                <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("TOTAL_AMT") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <span style="background-color: #00E171; font-size: large; font-style: normal; border: 1px solid #000000;">
                                            No Backlog Courses. </span>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ListView ID="lvConfirm" runat="server">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="titlebar">
                                            Exam Confirmed Course List</div>
                                        <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                            <thead>
                                                <tr class="header">
                                                    <th>
                                                        Course Code
                                                    </th>
                                                    <th>
                                                        Course Name
                                                    </th>
                                                    <th>
                                                        Semester
                                                    </th>
                                                    <th>
                                                        Theory/Prac
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
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblExamsem" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSubname" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="true" Width="80px"
                                OnClick="btnSubmit_Click" />
                            &nbsp
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="true" Width="80px"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding: 10px; text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
        <div id="divMsg" runat="server">
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


       
    </script>

</asp:Content>
