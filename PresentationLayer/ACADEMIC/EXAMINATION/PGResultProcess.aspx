<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage2.master" AutoEventWireup="true" CodeFile="PGResultProcess.aspx.cs" Inherits="ACADEMIC_EXAMINATION_PGResultProcess" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPgresult"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                  <img src="../../IMAGES/anim_loading_75x75.gif" alt="Loading" />
                <br />
               <b> Please wait..</b>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div><asp:UpdatePanel ID="updPgresult" runat="server">
            <ContentTemplate>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
               PG RESULT PROCESS
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
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
    </table>
    <div style="padding-left: 10px; width: 99%">
        <fieldset class="fieldset1">
            <legend class="legend2">Result Process</legend>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="form_left_label" style="width: 15%;">
                        Session :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                            Font-Bold="True">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td class="form_left_label" style="width: 15%;">
                        College / School Name:
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" >
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlColg"
                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Degree :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"> <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Branch :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            Width="35%" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Scheme :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlScheme" runat="server" Width="35%" AppendDataBoundItems="true"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Semester :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="form_left_label">
                        Section :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                            >
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                            Display="None" ErrorMessage="Please Select Section" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="display:none;" >
                    <td class="form_left_label">
                        Student Type:
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlStudentstatus" runat="server" AppendDataBoundItems="True"
                            Width="150px">
                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            <asp:ListItem Value="0">Regular Student</asp:ListItem>
                            <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStudentstatus"
                            Display="None" ErrorMessage="Please Select Student Type" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="form_left_label">
                        Exam Type:
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" Width="150px"
                            AutoPostBack="True" >
                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            <asp:ListItem Value="1">Regular</asp:ListItem>
                            <asp:ListItem Value="2">Revaluation</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamType"
                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="form_left_label">
                        Student :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlStudent" runat="server" Width="50%" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvStudent" runat="server" ControlToValidate="ddlStudent"
                            Display="None" ErrorMessage="Please Select Student" SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                    </td>
                    <td class="form_left_text">
                          <asp:Button ID="btnShow" runat="server"
                            Text="Show" ValidationGroup="report" Width="100px" onclick="btnShow_Click" />
                        <asp:Button ID="btnProcessResult" runat="server" OnClick="btnProcessResult_Click"
                            Text="Process Result" ValidationGroup="report" Width="180px"  Enabled="false"/>
                        &nbsp; <asp:Button ID="btnLock" runat="server" 
                            Text="Lock" ValidationGroup="report"  OnClientClick="return ConfirmLock();" Width="100px"  Enabled="false" OnClick="btnLock_Click"/>
                        &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Clear"
                            Width="80px" CausesValidation="False" />
                        &nbsp;
                    </td>
                    
                </tr>
                </table>
                </fieldset>
               
                   <table>
                <tr>
                  
                                    <td colspan="4" style="text-align: center; width:70%;">
                                            <asp:ListView ID="lvStudent" runat="server" >
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="vista-grid">
                                                        <div class="titlebar">
                                                            Select Student
                                                        </div>
                                                        <table class="datatable" cellpadding="0" cellspacing="0" >
                                                            <tr class="header">
                                                             <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 <th>
                                                               &nbsp;
                                                               </th>
                                                                 <th>
                                                                  &nbsp;
                                                                 </th>
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                <th>
                                                                    Select
                                                                    <asp:CheckBox ID="chkheader" runat="server"  onclick="return totAll(this);" />
                                                   
                                                                </th>
                                                                <th>
                                                                  &nbsp;
                                                                 </th>
                                                                   <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                               <th>
                                                               &nbsp;
                                                               </th>
                                                               
                                                                 <th>
                                                                  &nbsp;
                                                                 </th>
                                                                  <th>
                                                                    Roll No.
                                                                </th>
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                               
                                                             
                                                                  
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                    
                                                                 <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 <th>
                                                                    Student Name
                                                                </th>
                                                                <th>
                                                                  &nbsp;
                                                                 </th>
                                                               
                                                             
                                                                  
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                    
                                                                 <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 <th>
                                                                  Date
                                                                 </th>
                                                                <th>
                                                                  &nbsp;
                                                                 </th>
                                                               
                                                             
                                                                  
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                    
                                                                 <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                  <th>
                                                                  Process Status
                                                                 </th>
                                                                <th>
                                                                  &nbsp;
                                                                 </th>
                                                               
                                                             
                                                                  
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                    
                                                                 <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 
                                                                  <th>
                                                                  &nbsp;
                                                                 </th>
                                                                 <th>
                                                                  Lock Status
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
                                                       &nbsp;
                                                       </td> 
                                                       <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkStudent" runat="server" ToolTip="Select to view tabulation chart" />
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO")%>
                                                        </td>
                                                        
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStudname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                        </td>
                                                         <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        </td>
                                                         <td>
                                                            <%# Eval("RESULTDATE")%>
                                                        </td>
                                                         <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        </td>
                                                         <td>
                                                            <asp:Label runat="server" ID="lblPstatus" Font-Bold="true"  Text='<%# (Eval("PROCESS_STATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("pstatus") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>' 
                                                           ></asp:Label>
                                                        </td>
                                                         <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        </td>
                                                         <td>
                                                             <asp:Label runat="server" ID="lblLockstatus" Font-Bold="true"  Text='<%# (Eval("LOCK_STATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("LOCK") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>' 
                                                           ></asp:Label>
                                                            
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                    </td>
                                </tr>
                                            </table>
                                        </div>
          </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnShow" />
                <asp:PostBackTrigger ControlID="btnProcessResult" />

            </Triggers>
        </asp:UpdatePanel>                        
    <div id="divMsg" runat="server">
    </div>
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
    <script language="javascript" type="text/javascript">
        function ConfirmLock() {
            var ret = confirm('You can not reprocess the result after lock are you sure?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>

