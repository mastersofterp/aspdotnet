<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DetaintionAndCancelation1.aspx.cs" Inherits="ACADEMIC_EXAMINATION_DetaintionAndCancelation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <%-- <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
     function RunThisAfterEachAsyncPostback()
       {
            RepeaterDiv();

       }
        function RepeaterDiv()
{
  $(document).ready(function() {
      
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
 
}
    </script>--%>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                DETAINTION AND CANCELATION
               <%-- <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />--%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.png" AlternateText="Delete Record" />
                            Delete Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
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

                    
                </script>

                <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
                </script>

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                    <Animations>
                            <OnClick>
                                <Sequence>
                                    <%-- Disable the button so it can't be clicked again --%>
                                    <EnableAction Enabled="false" />
                                    
                                    <%-- Position the wire frame on top of the button and show it --%>
                                    <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                    <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                    
                                    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                                    <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                    <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                    <FadeIn AnimationTarget="info" Duration=".2"/>
                                    <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                    
                                    <%-- Flash the text/border red and fade in the "close" button --%>
                                    <Parallel AnimationTarget="info" Duration=".5">
                                        <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                        <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                    </Parallel>
                                    <Parallel AnimationTarget="info" Duration=".5">
                                        <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                        <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                        <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                    </Parallel>
                                </Sequence>
                            </OnClick>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                    <Animations>
                            <OnClick>
                                <Sequence AnimationTarget="info">
                                    <%--  Shrink the info panel out of view --%>
                                    <StyleAction Attribute="overflow" Value="hidden"/>
                                    <Parallel Duration=".3" Fps="15">
                                        <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                        <FadeOut />
                                    </Parallel>
                                    
                                    <%--  Reset the sample so it can be played again --%>
                                    <StyleAction Attribute="display" Value="none"/>
                                    <StyleAction Attribute="width" Value="250px"/>
                                    <StyleAction Attribute="height" Value=""/>
                                    <StyleAction Attribute="fontSize" Value="12px"/>
                                    <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                    
                                    <%--  Enable the button so it can be played again --%>
                                    <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                                </Sequence>
                            </OnClick>
                            <OnMouseOver>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                            </OnMouseOver>
                            <OnMouseOut>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                            </OnMouseOut>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>
        <tr>
            <td style="padding: 10px">
                <ajaxToolKit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                    Width="99%" ActiveTabIndex="0" ScrollBars="Both">
                    <ajaxToolKit:TabPanel ID="tabPerInfo" runat="server" >
                        <HeaderTemplate>
                            Detaintion Entry
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="padding-left: 10px; width: 99%">
                                <fieldset class="fieldset">
                                    <legend class="legend">Detaintion Entry</legend>
                                    <asp:UpdatePanel ID="updDetained" runat="server">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td class="form_left_label" style="width: 10%">
                                                        Session :
                                                    </td>
                                                    <td class="form_left_text" style="width: 60%">
                                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Font-Bold="True"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <%--  student list--%><td class="form_left_text" rowspan="6" style="vertical-align: top;">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rbtlDetaind" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtlDetaind_SelectedIndexChanged"
                                                                        RepeatDirection="Horizontal" Visible="False">
                                                                        <asp:ListItem Value="Prov Detain">Prov Detain</asp:ListItem>
                                                                        <asp:ListItem Value="Final Detain">Final Detain</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="5">
                                                                    <asp:Panel ID="pnlDetained" runat="server" Height="100px" Visible="false" ScrollBars="Vertical">
                                                                        <asp:ListView ID="lvDetainedStudent" runat="server" Visible="False">
                                                                            <LayoutTemplate>
                                                                                <div class="vista-grid">
                                                                                    <div class="titlebar">
                                                                                        Students List
                                                                                    </div>
                                                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                                        <thead>
                                                                                            <tr class="header">
                                                                                                <th>
                                                                                                    Name
                                                                                                </th>
                                                                                            </tr>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </thead>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="item" id="trRow" runat="server" onmouseout="this.style.backgroundColor='#FFFFFF'"
                                                                                    onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                                    <td>
                                                                                        <span style="font-size: 8pt">
                                                                                            <%# Eval("STUDNAME") %></span>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%--  student list--%></tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 10%; height: 7px;">
                                                        Degree :
                                                    </td>
                                                    <td class="form_left_text" style="height: 7px">
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                            Width="67%" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ValidationGroup="report">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 10%">
                                                        Branch :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                            Width="67%" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="report">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 10%">
                                                        Scheme :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlScheme" runat="server" Width="67%" AppendDataBoundItems="True"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" ValidationGroup="report">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 20%">
                                                        Semester :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" ValidationGroup="report">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="SectionDetained" runat="server" visible="false">
                                                    <td class="form_left_label" style="width: 20%">
                                                        Section :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlSectionDetaintion" runat="server" Width="50%" TabIndex="4"
                                                            AppendDataBoundItems="True" ValidationGroup="auto" ToolTip="Section">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSectionCancel" runat="server" ControlToValidate="ddlSectionDetaintion"
                                                            Display="None" ErrorMessage="Please Select Section" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label">
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                                    </td>
                                                    <td class="form_left_text" colspan="2">
                                                        <asp:Button ID="btnShowStudentDetaintion" runat="server" Text="Show Students" ValidationGroup="report"
                                                            OnClick="btnShowStudentDetaintion_Click"  />&nbsp;&nbsp
                                                             <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="True"
                                                            ValidationGroup="submit" OnClientClick="return confirmDetaind();" OnClick="btnSubmit_Click" />&nbsp;&nbsp
                                                            <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                           />&nbsp;&nbsp;
                                                        <asp:Button ID="btnReport" runat="server" Text="Report"
                                                            OnClick="btnReport_Click" Visible="False" />&nbsp;&nbsp
                                                        <asp:Button ID="btnCancelStudReport" runat="server" OnClick="btnCancelStudReport_Click"
                                                            Text="Admission Cancel Report" Visible="False" />
                                                    </td>
                                                </tr>
                                                    <tr>
                                                        <td class="form_left_label" colspan="3">
                                                        <span style="color:Red"> Note : Do Detaintion Entry carefully. Once Final Detain Student cannot be rollback.  </span>
                                                        </td>
                                                    </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table id="tblBackLog" runat="server" visible="false" cellpadding="0" cellspacing="0"
                                                            width="60%">
                                                            <tr>
                                                                <td style="padding: 10px; text-align: center;">
                                                                    <asp:Repeater ID="lvDetained" runat="server">
                                                                        <HeaderTemplate>
                                                                            <div id="demo-grid" class="vista-grid" style="width: 100%">
                                                                                <div class="titlebar">
                                                                                    Student Detention List</div>
                                                                            </div>
                                                                            <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                                                                <thead>
                                                                                    <tr class="header">
                                                                                        <th style="width: 15%; text-align: center">
                                                                                            <asp:Label ID="lblHeader" runat="server" Text=" Provisional Detain"></asp:Label>
                                                                                        </th>
                                                                                        <th style="width: 55%; text-align: left">
                                                                                            Name
                                                                                        </th>
                                                                                        <th style="width: 15%; text-align: center">
                                                                                            Roll No.
                                                                                        </th>
                                                                                        <th style="width: 15%; text-align: center">
                                                                                            Final Detain
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </thead>
                                                                                <tbody>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="item" id="trRow" runat="server" 
                                                                                >
                                                                                <td style="width: 15%; text-align: center">
                                                                                    <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("PROV_DETAIN").ToString() == string.Empty ? false : true %>' /><asp:Label
                                                                                        ID="idNo" runat="server" ToolTip='<%#Eval("IDNO")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 55%; text-align: left">
                                                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("SEATNO")%>' />
                                                                                </td>
                                                                                <td style="width: 15%; text-align: left">
                                                                                    <asp:Label ID="lblRoll" runat="server" Text='<%# Eval("SEATNO") + Eval("ROLLNO").ToString() == "0" ? string.Empty : Eval("ROLLNO") %>'></asp:Label><asp:Label
                                                                                        ID="lblSection" runat="server" Text='<%# Eval("SECTION")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="width: 15%; text-align: center">
                                                                                    <asp:CheckBox ID="chkFinalDetain" runat="server" Checked='<%# Eval("FINAL_DETAIN").ToString() == "Y" ? true : false %>' />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody></table></FooterTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </fieldset>
                            </div>
                        </ContentTemplate>
                    </ajaxToolKit:TabPanel>
                    <ajaxToolKit:TabPanel ID="tabRegCourses" runat="server" HeaderText="AUTONOMOUS" Visible="false" Enabled="false">
                        <HeaderTemplate>
                            CANCELATION
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="padding-left: 10px; padding-right: 10px; padding-top: 10px">
                                <fieldset class="fieldset">
                                    <legend class="legend">STUDENT CANCELATION</legend>
                                    <asp:UpdatePanel ID="updCancel" runat="server">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="form_left_label" style="width: 15%">
                                                        Branch :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlBranchCancel" runat="server" Width="60%" TabIndex="2" AppendDataBoundItems="True"
                                                            ValidationGroup="auto" ToolTip="Branch" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchCancel_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvBranchAuto" runat="server" ControlToValidate="ddlBranchCancel"
                                                            Display="None" ErrorMessage="Please Select Branch" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 15%">
                                                        Session :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlSessionCancel" runat="server" AppendDataBoundItems="True"
                                                            Font-Bold="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 15%">
                                                        Scheme :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlSchemeCancel" runat="server" Width="60%" TabIndex="3" AppendDataBoundItems="True"
                                                            ValidationGroup="auto" ToolTip="Scheme" CausesValidation="True" OnSelectedIndexChanged="ddlSchemeCancel_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSchemeAuto" runat="server" ControlToValidate="ddlSchemeCancel"
                                                            Display="None" ErrorMessage="Please Select Scheme" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 15%">
                                                        Semester :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlSemesterCancel" runat="server" Width="60%" TabIndex="4"
                                                            AppendDataBoundItems="True" ValidationGroup="auto" ToolTip="Semester" CausesValidation="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSemesterAuto" runat="server" ControlToValidate="ddlSemesterCancel"
                                                            Display="None" ErrorMessage="Please Select Semester" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 15%">
                                                        &#160;&#160;
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:Button ID="Cancelation_Show_Students" runat="server" Text="Show Students" ValidationGroup="auto"
                                                            OnClick="Cancelation_Show_Students_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                                                ID="btnCancelationCancel" runat="server" Text="Cancel" />
                                                    </td>
                                                </tr>
                                                <tr id="SectionCancel" runat="server" visible="false">
                                                    <td class="form_left_label" style="width: 15%">
                                                        Section :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlSectionCancelation" runat="server" Width="60%" TabIndex="4"
                                                            AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="auto" ToolTip="Section">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSectionAuto" runat="server" ControlToValidate="ddlSectionCancelation"
                                                            Display="None" ErrorMessage="Please Select Section" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_text" align="left" colspan="3">
                                                        <span style="width: 100%; font-size: 9pt; color: Green; font-weight: bold;">Note : *
                                                            - Select upto Section<br />
                                                            &#160;&#160;&#160;&#160; &#160;&#160;&#160;&#160;&#160;&#160;** - Select upto Semester
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_label" style="width: 15%">
                                                        Degree :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlDegreeCancel" runat="server" AppendDataBoundItems="True"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegreeCancel_SelectedIndexChanged"
                                                            TabIndex="1" ToolTip="Branch" ValidationGroup="auto" Width="60%">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDegreeAuto" runat="server" ControlToValidate="ddlDegreeCancel"
                                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True" ValidationGroup="auto"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="form_left_text" align="center" colspan="2">
                                                        <asp:ValidationSummary ID="vsAuto" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="auto" />
                                                    </td>
                                                    <td colspan="2">
                                                        <table id="Table1" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                                            width="100%">
                                                            <tr>
                                                                <td style="padding: 10px; text-align: center;">
                                                                    <asp:ListView ID="ListView2" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="vista-grid">
                                                                                <div class="titlebar">
                                                                                    Students to be marked for Cancelation
                                                                                </div>
                                                                                <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                                    <thead>
                                                                                        <tr class="header">
                                                                                            <th style="width: 2%; text-align: center">
                                                                                                Select
                                                                                            </th>
                                                                                            <th style="width: 2%; text-align: left">
                                                                                                Name
                                                                                            </th>
                                                                                            <th style="width: 10%; text-align: center">
                                                                                                Roll No.
                                                                                            </th>
                                                                                            <th style="width: 20%; text-align: center">
                                                                                                Remark
                                                                                            </th>
                                                                                            <th style="width: 2%; text-align: center">
                                                                                                Final Cancel
                                                                                            </th>
                                                                                            <th style="width: 20%; text-align: center">
                                                                                                Final Remark
                                                                                            </th>
                                                                                            <th style="width: 2%; text-align: center">
                                                                                                Remove
                                                                                            </th>
                                                                                            <th style="width: 20%; text-align: center">
                                                                                                Remove Remark
                                                                                            </th>
                                                                                        </tr>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </thead>
                                                                                </table>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr id="trRow" runat="server" class="item" onmouseout="this.style.backgroundColor='#FFFFFF'"
                                                                                onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                                <td style="width: 2%; text-align: center">
                                                                                    <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("PROV_CANCEL").ToString() == string.Empty ? false : true %>' /><asp:Label
                                                                                        ID="idNo" runat="server" ToolTip='<%#Eval("IDNO")%>'></asp:Label>
                                                                                </td>
                                                                                <%--<td style="width: 20%; text-align: center">
                                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                                </td>--%><td style="width: 30%; text-align: left">
                                                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("SEATNO")%>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblRoll" runat="server" Text='<%# Eval("SEATNO") + Eval("ROLLNO").ToString() == "0" ? string.Empty : Eval("ROLLNO") %>'> </asp:Label>
                                                                                </td>
                                                                                <td style="width: 15%; text-align: center">
                                                                                    <asp:TextBox ID="txtRemark" runat="server" Font-Bold="true" Text='<%# Eval("CANCEL_REMARK")%>'
                                                                                        TextMode="MultiLine" Width="40px" />
                                                                                </td>
                                                                                <td style="width: 2%; text-align: center">
                                                                                    <asp:CheckBox ID="chkFinalDetain" runat="server" Checked='<%# Eval("FINAL_CANCEL").ToString() == string.Empty ? false : true %>' />
                                                                                </td>
                                                                                <td style="width: 15%; text-align: center">
                                                                                    <asp:TextBox ID="txtFinalRemark" runat="server" Font-Bold="true" Text='<%# Eval("FINAL_CANCEL_REMARK")%>'
                                                                                        TextMode="MultiLine" Width="40px" />
                                                                                </td>
                                                                                <td style="width: 2%; text-align: center">
                                                                                    <asp:CheckBox ID="chkRemove" runat="server" Enabled='<%# Eval("CANCEL_IDNO").ToString()   != ""  ? true : false  %>' />
                                                                                </td>
                                                                                <td style="width: 15%; text-align: center">
                                                                                    <asp:TextBox ID="txtRemoveRemark" runat="server" Enabled='<%# Eval("CANCEL_IDNO").ToString()   != ""  ? true : false  %>'
                                                                                        Font-Bold="true" Text='<%# Eval("UNDO_DETAIN_REMARK")%>' TextMode="MultiLine"
                                                                                        Width="40px" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="padding: 10px; text-align: center;">
                                                                    <asp:Button ID="Button1" runat="server" Font-Bold="True" OnClick="btnSubmit_Click"
                                                                        Text="Submit" ValidationGroup="submit" Width="80px" />&#160;&#160;
                                                                    <asp:Button ID="Button2" runat="server" Font-Bold="True" OnClick="btnCancel_Click"
                                                                        Text="Cancel" Width="80px" />&#160;&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <%-- /* listview */ --%></table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </fieldset>
                            </div>
                        </ContentTemplate>
                    </ajaxToolKit:TabPanel>
                </ajaxToolKit:TabContainer>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
   <script language="javascript" type="text/javascript">
        function confirmDetaind() 
        {
            return confirm("Are you sure you want to detaind the selected student");
        }
    </script>
</asp:Content>
