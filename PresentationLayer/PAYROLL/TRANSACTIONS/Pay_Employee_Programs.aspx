<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Employee_Programs.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Employee_Programs"
     %>

<%@ Register Src="~/PayRoll/Transactions/Pay_PersonalMemoranda.ascx" TagName="PersonalMemoranda"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_FamilyParticulars.ascx" TagName="FamilyParticulars"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/PAY_Qualification.ascx" TagName="Qualification"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Training.ascx" TagName="Training" TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_DepartmentalExam.ascx" TagName="DepartmentalExam"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_PreviousService.ascx" TagName="PreviousService"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_ForeginService.ascx" TagName="ForeginService"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_LoansAndAdvance.ascx" TagName="LoansAndAdvance"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Nomination.ascx" TagName="Nomination"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Leave.ascx" TagName="Leave" TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Matters.ascx" TagName="Matters" TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_PayRevision.ascx" TagName="PayRevision"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Increment_Termination.ascx" TagName="Increment_Termination"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_ImageUpload.ascx" TagName="ImageUpload"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_publication_Details.ascx" TagName="PublicationDetails"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Invited_Talks.ascx" TagName="InvitedTalks"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Admin_Responsibilities.ascx" TagName="AdminResponsibilities"
    TagPrefix="uc1" %>
<%@ Register Src="~/PAYROLL/TRANSACTIONS/Pay_Training_Conducted.ascx" TagName="Traningconducted"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                Update Training and Publication Details&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <tr>
            <td>
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
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize)
                            {
                                var location = Sys.UI.DomElement.getLocation(bottom);
                                top.style.position = 'absolute';
                                top.style.top = location.y + 'px';
                                top.style.left = location.x + 'px';
                                if (!ignoreSize)
                                {
                                    top.style.height = bottom.offsetHeight + 'px';
                                    top.style.width = bottom.offsetWidth + 'px';
                                }
                            }
                </script>

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlSelection" runat="server" Style="text-align: left; width: 98%;
                    padding-left: 5px;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Criteria</legend>
                        <br />
                        <asp:UpdatePanel ID="upEmployee" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_label" style="width: 20%; padding-left: 15px">
                                            Order By:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" AutoPostBack="true"
                                                runat="server" Width="100px" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                <asp:ListItem Value="2">Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">
                                            Employee Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="400px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                ValidationGroup="ServiceBook" InitialValue="0"></asp:RequiredFieldValidator>
                                            <td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td style="padding-left: 5px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" class="vista-gridServiceBook" border="0" cellspacing="0" width="100%"
                            style="border-style: solid;">
                            <tr>
                                <td nowrap="nowrap">
                                    <div id="Div29" class="titlebar-ServiceBook" runat="server" onclick="__doPostBack('div','15');"
                                        style="cursor: pointer;" title="Publication Details">
                                        Publication Details
                                    </div>
                                </td>
                                
                                
                                 <td nowrap="nowrap" colspan="3">
                                    <div id="Div18" class="titlebar-ServiceBook" runat="server" onclick="__doPostBack('div','4');"
                                        style="cursor: pointer;" title="Training/Short Term Course/Conference Attended">
                                        Training/Short Term Course/Conference Attended
                                    </div>
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <div id="Div21" class="titlebar-ServiceBook" runat="server" onclick="__doPostBack('div','7');"
                                        style="cursor: pointer;" title="Training/Short Term Course/Conference Conducted">
                                        Training/Short Term Course/Conference Conducted
                                    </div>
                                </td>
                                
                                
                               
                            </tr>
                            <tr>
                                <td nowrap="nowrap">
                                    <div id="Div31" class="titlebar-ServiceBook" runat="server" onclick="__doPostBack('div','17');"
                                        style="cursor: pointer;" title="Invited Talks">
                                        Invited Talks
                                    </div>
                                </td>
                                 <td nowrap="nowrap">
                                    <div id="Div30" class="titlebar-ServiceBook" runat="server" onclick="__doPostBack('div','16');"
                                        style="cursor: pointer;" title="Administrative Responsibilities">
                                        Administrative Responsibilities
                                    </div>
                                </td>
                                 <td nowrap="nowrap">
                                    <div class="titlebar-ServiceBook">
                                    </div>
                                </td>
                                <td nowrap="nowrap">
                                    <div class="titlebar-ServiceBook">
                                    </div>
                                </td>
                                 <td nowrap="nowrap">
                                    <div class="titlebar-ServiceBook">
                                    </div>
                                </td>
                                 <td nowrap="nowrap">
                                    <div class="titlebar-ServiceBook">
                                    </div>
                                </td>
                            </tr>
                          
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="30">
                <asp:UpdatePanel ID="upWebUserControl" runat="server">
                    <ContentTemplate>
                        <%--<uc1:PersonalMemoranda ID="UserControl1" runat="server" />
                        <uc1:FamilyParticulars ID="UserControl2" runat="server" Visible="false" />
                        <uc1:Qualification ID="UserControl3" runat="server" Visible="false" />--%>
                        <uc1:Training ID="UserControl4" runat="server" Visible="false" />
                        <%--<uc1:DepartmentalExam ID="UserControl5" EnableViewState="true" runat="server" Visible="false" />
                        <uc1:PreviousService ID="UserControl6" runat="server" Visible="false" />--%>
                        <%--  <uc1:ForeginService ID="UserControl7" runat="server" Visible="false" />--%>
                        <uc1:Traningconducted ID="UserControl7" runat="server" Visible="false" />
                        <%--<uc1:LoansAndAdvance ID="UserControl8" runat="server" Visible="false" />
                        <uc1:Nomination ID="UserControl9" runat="server" Visible="false" />
                        <uc1:Leave ID="UserControl10" runat="server" Visible="false" />
                        <uc1:Matters ID="UserControl11" runat="server" Visible="false" />
                        <uc1:PayRevision ID="UserControl12" runat="server" Visible="false" />
                        <uc1:Increment_Termination ID="UserControl13" runat="server" Visible="false" />--%>
                        <uc1:PublicationDetails ID="UserControl15" runat="server" Visible="false" />
                        <uc1:AdminResponsibilities ID="UserControl16" runat="server" Visible="false" />
                        <uc1:InvitedTalks ID="UserControl17" runat="server" Visible="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <uc1:ImageUpload ID="UserControl14" runat="server" Visible="false" />
                <%-- <div id="divTab1" runat="server">
                            personal
                        </div>
                        <div id="divTab2" style="display: none" runat="server">
                            family
                        </div>
                        <div id="divTab3" runat="server">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                                  <uc1:Qualification ID="Qualification" runat="server" />
                             </ContentTemplate>
                             </asp:UpdatePanel>  
                        </div>
                        <div id="divTab4" style="display: none" runat="server">
                            traning
                        </div>
                        <div id="divTab5" style="display: none" runat="server">
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                              <ContentTemplate>
                                <uc1:DepartmentalExam ID="DepartmentalExam" runat="server"/>
                             </ContentTemplate>
                             </asp:UpdatePanel> 
                        </div>
                        <div id="divTab6" style="display: none" runat="server">
                            Previous Quali.Service
                        </div>
                        <div id="divTab7" style="display: none" runat="server">
                            Foregin Service
                        </div>
                        <div id="divTab8" style="display: none" runat="server">
                            Loans & Advance
                        </div>
                        <div id="divTab9" style="display: none" runat="server">
                            Nomination
                        </div>
                        <div id="divTab10" style="display: none" runat="server">
                            Leave
                        </div>
                        <div id="divTab11" style="display: none" runat="server">
                            Matters
                        </div>
                        <div id="divTab12" style="display: none" runat="server">
                            Pay Revision
                        </div>
                        <div id="divTab13" style="display: none" runat="server">
                            Increment / Termination
                        </div>--%>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function ShowTab(tabNo)
        {
            for(i = 1; i<=17; i++)
            {   
               
                var tab = document.getElementById("ctl00_ContentPlaceHolder1_divTab" + i);
                if(tab != null)
                {    
                    
                    if(i == tabNo)
                    {   
                        document.getElementById("ctl00_ContentPlaceHolder1_divTab" + i).style.display = "block";
                        //document.getElementById("ctl00_ContentPlaceHolder1_UpdatePanel2").disabled = true;
                        //document.getElementById("ctl00_ContentPlaceHolder1_DepartmentalExam").disabled = true;
                    }
                    else
                    {  
                        document.getElementById("ctl00_ContentPlaceHolder1_divTab" + i).style.display = "none";
                        
                    }                   
                }
            }
        }              
    
    </script>

</asp:Content>
