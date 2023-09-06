<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BranchChangeAppered.aspx.cs" Inherits="ACADEMIC_ExamRegistration" Title=""
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlStart" runat="server" ScrollBars="Auto">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                   APPLY BRANCH CHANGE
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
                    1. Please click Proceed to Exam Registration button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    2. Please select the one semester in Backlog Semester dropdownlist and Click the show button.
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
                <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                    <asp:Button ID="btnProceed" runat="server" Text="Proceed to Exam Registration" OnClick="btnProceed_Click" />
                </p>
            </div>
        </div>
        <asp:Panel ID="pnlSearch" runat="server">
                <table cellpadding="0" id="tblSearch" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Enter Roll No :
                        </td>
                        <td>
                            &nbsp<asp:TextBox ID="txtEnrollno" runat="server" Width="25%"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnProceed_Click" 
                                Text="Show" />
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
                </asp:Panel>
        <div id="divCourses" runat="server" visible="false">
            <fieldset class="fieldset">
                <legend class="legend">Apply Branch Change</legend>
                <table cellpadding="0" cellspacing="0" width="100%" id="tblInfo" runat="server">
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Student Name :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                        </td>
                        <td rowspan="23" width="20%" valign="top">
                       
                        <asp:Image ID="imgPhoto" runat="server" Width="60%" Height="90%" 
                                ImageUrl="~/IMAGES/nophoto.jpg" />
                       
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
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            <asp:HiddenField ID="hdfDegreeno" runat="server" />
                        </td>
                        <td class="form_left_text">

                            <asp:HiddenField ID="hdfCategory" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 4px; padding-bottom: 4px;" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
                                ID="Label1" runat="server" Font-Bold="True">Please Select Branch Preference</asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 1 :</td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref1" runat="server" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref1_SelectedIndexChanged" Width="50%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPref" runat="server" 
                                ControlToValidate="ddlBranchPref1" Display="None" 
                                ErrorMessage="Please Select Pref1" InitialValue="0" SetFocusOnError="True" 
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 2 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref2" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 3 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref3" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 4 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref4" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 5 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref5" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref5_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 6 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref6" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref6_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 7 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref7" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref7_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 8 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref8" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref8_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 9 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref9" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranchPref9_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            Pref 10 :</td><td class="form_left_text">
                            <asp:DropDownList ID="ddlBranchPref10" runat="server" Width="50%" 
                                AppendDataBoundItems="True" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            &nbsp;</td><td class="form_left_text">
                            &nbsp;</td></tr><tr>
                        <td style="text-align: right; padding-top: 4px; padding-bottom: 4px;" 
                            colspan="2">
                             <div style="border: 2px solid #C0C0C0;
            background-color: #FFFFCC; padding: 20px; color: #990000;text-align:left;">
                           <b> Note :- </b> Student submitting application for change of branch are advised to consult their parent before exercising the above option. Once the branch is allotted as per the above option, it will be mandatory to the student to accept the change and no further request will be entertained. Further, student submitting the option will be deemed to have consulted with their parents and no further request from the parent shall be entertained in this regard. Student may also exercise option of branch where vacancies are indicated as NIL as vacancies may create subsequently in allotment process.
                           </div>
                            &nbsp;</td></tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                            &nbsp;</td>
                        <td class="form_left_text">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:center; padding-top: 4px; padding-bottom: 4px;" 
                            colspan="2">
                            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" OnClientClick="javascript:return confirm('Once the branch is allotted as per the above option, it will be mandatory to the student to accept the change and no further request will be entertained....Are you sure?');" Text=" Submit " Width="12%" ValidationGroup="Submit"/>
                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="12%" onclick="btnCancel_Click" 
                                />
                            &nbsp;<asp:Button ID="btnReport" runat="server" onclick="btnReport_Click" 
                                Text=" Print " Visible="false" Width="12%" />
                            &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false" 
                                ValidationGroup="Submit" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        &nbsp;</td></tr></table></td></tr><tr>
                     <td colspan="3" style="padding: 10px; text-align: center;">
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        &nbsp;</td></tr></table></td></tr><tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            &nbsp;</td></tr><tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        &nbsp;</div><asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
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

</asp:Content>
