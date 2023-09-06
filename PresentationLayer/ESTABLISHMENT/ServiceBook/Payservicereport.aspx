<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payservicereport.aspx.cs" MasterPageFile="~/SiteMasterPage.master"
      Inherits="ESTABLISHMENT_ServiceBook_Payservicereport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ServiceBook Report</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlSelection" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Select College and Employee</div>
                                        <div class="panel panel-body">
                                            <asp:UpdatePanel ID="upEmployee" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4">
                                                            <label>College :</label>
                                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" ToolTip="Select College">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" ErrorMessage="Please select College" ValidationGroup="ServiceBookReport"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>                                                        
                                                        <div class="form-group col-md-4">
                                                            <label>Employee Name :</label>
                                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" TabIndex="2" ToolTip="Select Employee Name">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                                Display="None" ErrorMessage="Please select  Employee Name " ValidationGroup="ServiceBookReport"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Select <asp:CheckBox ID="chkAll" runat="server" Text="All" onclick="return checkAll();" /> 
                            &nbsp; Criteria</div>
                                        <div class="panel panel-body">
                                            <div class="col-md-12">
                                                <div class="form-group col-md-6">
                                                    <div class="form-group col-md-6" id="trDept" runat="server" visible="false">
                                                        <label>Department :</label>
                                                        <asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkPersonalMemoranda" runat="server" Text="Personal Memoranda"
                                                            Checked="true" TabIndex="3" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkEducationalQualification" runat="server" Text="Qualification Information" TabIndex="5" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkDeptExamAndOtherDetails" runat="server" Text="Departmental Exam Information" TabIndex="7" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="ChkNomination" runat="server" Text="Employee Nomination Information" TabIndex="9" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkTraning" runat="server" Text="Training/Short Term Course/Conference Attended" TabIndex="11" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkTrainingConducted" runat="server" Text="Training/Short Term Course/Conference Conducted" TabIndex="13"/>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkIncetmentTermination" runat="server" Text="Other Details Entries Like Increment termination etc" TabIndex="15"/>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkAdministrativeResponsibilities" runat="server" Text="Administrative Responsibilities" TabIndex="17"/>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkFamilyParticulars" runat="server" Text="Family Information" TabIndex="4" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkMatter" runat="server" Text="Matter" TabIndex="6" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkPreviousQualifyingService" runat="server" Text="Previous Experience" TabIndex="8" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="ChkDetailsOfLoansAndAdvances" runat="server" Text="Loans And Advances Information" TabIndex="10" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkLeaveRecords" runat="server" Text="Leave Records" TabIndex="12"/>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkPayRevisionOrPromotion" runat="server" Text="Pay Revision Information" TabIndex="14"/>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkPublicationDetails" runat="server" Text="Publication Details" TabIndex="16"/>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:CheckBox ID="chkInvitedTalks" runat="server" Text="Invited Talks" TabIndex="18"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divMsg" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="box-footer">
                    <div class="col-md-12">
                        <p class="text-center">
                            <asp:Button ID="btnShowReport" runat="server" Text="Show Report" ValidationGroup="ServiceBookReport" TabIndex="19"
                                OnClick="btnShowReport_Click" CssClass="btn btn-info" ToolTip="Click here to Show Report" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="20"
                                OnClick="btnCancel_Click" CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBookReport"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%--<td class="vista_page_title_bar" valign="top" style="height: 30px">ServiceBook Report&nbsp;              
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                    function checkAll() {
                        if (document.getElementById('ctl00_ContentPlaceHolder1_chkAll').checked) {
                            var countp = document.getElementsByTagName('ctl00_ContentPlaceHolder1_pnlCheck');
                            var Count = document.getElementsByTagName("input");
                            for (var r = 0; r < Count.length; r++) {
                                if (Count[r].type == "checkbox") {
                                    Count[r].checked = true;
                                }
                            }

                        }
                        else {
                            var countp = document.getElementsByTagName('ctl00_ContentPlaceHolder1_pnlCheck');
                            var Count = document.getElementsByTagName("input");
                            for (var r = 0; r < Count.length; r++) {
                                if (Count[r].type == "checkbox" && Count[r].id != "ctl00_ContentPlaceHolder1_chkPersonalMemoranda") {
                                    Count[r].checked = false;
                                }
                            }
                        }
                    }
                </script>

            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Panel ID="pnlSelection" runat="server" Style="text-align: left; width: 90%; padding-left: 20px;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Department and Employee</legend>
                        <br />
                        <asp:UpdatePanel ID="upEmployee" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_label" width="20%">Department:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" Width="300px"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please select Department" ValidationGroup="ServiceBookReport"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Employee Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" ErrorMessage="Please select  Employee Name " ValidationGroup="ServiceBookReport"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>--%>
                <%--Already Committed<tr>
                                        <td class="form_left_label">
                                            IdNo :
                                        </td>
                                        <td class="form_left_text" colspan="4">
                                            <asp:TextBox ID="txtIdno" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                <%--     <tr>
                                        <td>&nbsp
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </asp:Panel>--%>
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Panel ID="Panel1" runat="server" Style="text-align: left; width: 90%; padding-left: 20px;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Criteria</legend>
                        <br />--%>
                <%--Already Committed<asp:UpdatePanel ID="SelectCriteria" runat="server">
                            <ContentTemplate>--%>
                <%-- <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr id="trDept" runat="server" visible="false">
                                <td class="form_left_label" colspan="4" align="center">Department :<asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkPersonalMemoranda" runat="server" Text="Personal Memoranda"
                                        Checked="true" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkFamilyParticulars" runat="server" Text="Family Information" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkEducationalQualification" runat="server" Text="Qualification Information" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkMatter" runat="server" Text="Matter" />
                                </td>--%>
                <%--Alreaady Committed <td class="form_left_label">
                                            <asp:CheckBox ID="chkDetailsOfServiceBook" runat="server" Text="Details Of ServiceBook" />
                                        </td>--%>
                <%--</tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkDeptExamAndOtherDetails" runat="server" Text="Departmental Exam Information" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkPreviousQualifyingService" runat="server" Text="Previous Experience" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">--%>
                <%--Already Committed<asp:CheckBox ID="chkForeginService" runat="server" Text="Foreign Service Information" />--%>
                <%--<asp:CheckBox ID="ChkNomination" runat="server" Text="Employee Nomination Information" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="ChkDetailsOfLoansAndAdvances" runat="server" Text="Loans And Advances Information" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkTraning" runat="server" Text="Training/Short Term Course/Conference Attended" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkLeaveRecords" runat="server" Text="Leave Records" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">--%>
                <%--Already Committed<asp:CheckBox ID="chkTraning" runat="server" Text="Training Information" />--%>
                <%--<asp:CheckBox ID="chkTrainingConducted" runat="server" Text="Training/Short Term Course/Conference Conducted" />
                                </td>--%>
                <%--Already Committed<td class="form_left_label">
                                            <asp:CheckBox ID="chkGoodServiceAdvIncrcPunishMent" runat="server" Text="Good Service,Adv.Incrc.,PunishMent etc" />
                                        </td>--%>
                <%-- <td class="form_left_label">
                                    <asp:CheckBox ID="chkPayRevisionOrPromotion" runat="server" Text="Pay Revision Information" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkIncetmentTermination" runat="server" Text="Other Details Entries Like Increment termination etc" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkPublicationDetails" runat="server" Text="Publication Details" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkAdministrativeResponsibilities" runat="server" Text="Administrative Responsibilities" />
                                </td>
                                <td class="form_left_label">
                                    <asp:CheckBox ID="chkInvitedTalks" runat="server" Text="Invited Talks" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" ValidationGroup="ServiceBookReport"
                                        OnClick="btnShowReport_Click" Width="100px" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" Width="85px" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBookReport"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp
                                </td>
                            </tr>
                        </table>
                        <div id="divMsg" runat="server">
                        </div>--%>
                <%--Aready Committed</ContentTemplate>
                        </asp:UpdatePanel>--%>
                <%--</fieldset>
                </asp:Panel>--%>
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
    </table>
</asp:Content>
