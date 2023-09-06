<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" 
AutoEventWireup="true" CodeFile="Inplant_student_list.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_Inplant_student_list_new" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                IN PLANT STUDENTS LIST &nbsp;
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
                            <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record--%>
                            <%--<asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
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
                                if (!ignoreSize) {
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
                <br />
                <asp:Panel ID="pnlSelect" runat="server">
                    <div style="text-align: left; width: 87%; padding-left: 10px;">
                        <fieldset class="fieldsetPay">
                            <legend class="legendPay">Report Selection Criteria</legend>
                            <table>
                            <tr>
                                <td colspan="2" class="form_left_label">
                                    <span style="font-size: 9pt; font-weight: bold; color: Maroon;">Note:- For 
                                    student inplant list, select degree &amp; branch only. </span>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Degree Name :
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" 
                                        Width="300px" onselectedindexchanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>                                
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegre" runat="server" ControlToValidate="ddlDegree"
                                    ValidationGroup="Report" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="true"
                                    Display="None"></asp:RequiredFieldValidator>
                                </td>                            
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Branch Name :
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" 
                                        Width="300px" onselectedindexchanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>                                
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranch"
                                    ValidationGroup="Report" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="true"
                                    Display="None"></asp:RequiredFieldValidator>
                                </td>                            
                            </tr>
                            <%--<tr>
                                <td class="form_left_label">
                                    Scheme :
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlScheme" runat="server" Width="50%" AppendDataBoundItems="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="form_left_label">
                                    Semester :
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" 
                                        Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>                                
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                    ValidationGroup="Report" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                    Display="None"></asp:RequiredFieldValidator>
                                </td>                            
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Student :
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="true" 
                                        Width="300px" onselectedindexchanged="ddlStudent_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>                                
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlStudent"
                                    ValidationGroup="Report" ErrorMessage="Please Select Student" InitialValue="0" SetFocusOnError="true"
                                    Display="None"></asp:RequiredFieldValidator>
                                </td>                            
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Company :                               
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlComp" runat="server" AppendDataBoundItems="true" 
                                        Width="300px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>                                
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvComp" runat="server" ControlToValidate="ddlComp"
                                    ValidationGroup="Report" ErrorMessage="Please Select Company" InitialValue="0" SetFocusOnError="true"
                                    Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="form_left_label">
                                    File Name :                               
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtFileNm" runat="server" MaxLength="60" Width="300px" />
                                                    <asp:RequiredFieldValidator ID="rfvFileNm" runat="server" ControlToValidate="txtFileNm"
                                                        Display="None" ErrorMessage="Please Enter File Name" ValidationGroup="Report"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td class="form_left_label">
                                    From Date :                               
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtFromDt" runat="server" MaxLength="60" Width="80px" />
                                    <asp:RequiredFieldValidator ID="rfvFromDt" runat="server" ControlToValidate="txtFromDt"
                                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Report"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                     <asp:Image ID="imgCalFromdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                      <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDt"
                                                        PopupButtonID="imgCalFromdt" Enabled="true" EnableViewState="true">
                                       </ajaxToolKit:CalendarExtender>
                                       <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" TargetControlID="txtFromdt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                       <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                                        ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Report" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        
                                                        &nbsp;&nbsp;&nbsp;To Date :&nbsp;
                                                    <asp:TextBox ID="txtTodt" runat="server" Width="80px" MaxLength="12" />
                                                    <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Report"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodt"
                                                        PopupButtonID="imgCalTodt" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" TargetControlID="txtTodt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt"
                                                        ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Report" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>                   
                                                   
                                </td>
                            </tr>--%>
                            <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="form_button" colspan="2" align="center">
                                        <asp:Button ID="btnShow" runat="server" Text="Inplant Letter" ValidationGroup="Report" OnClick="btnShow_Click"
                                            Width="100px" />
                                            <asp:Button ID="btnStudList" runat="server" Text="Inplant Student List" CausesValidation="false"  OnClick="btnStudList_Click"
                                            Width="130px" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"  OnClick="btnCancel_Click"
                                            Width="90px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Report"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PnlList" runat="server" Height="360px" ScrollBars="Auto">
                            <div style="width: 70%; padding: 10px">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                                Inplant Letter Generated Student List
                                            </div>
                                            <table class="datatable" cellpadding="0" cellspacing="0">
                                                <tr class="header">
                                                    <th>
                                                        Select <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="false" />
                                                    </th>
                                                    <th>
                                                        Name
                                                    </th>
                                                    
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td>
                                                <asp:CheckBox ID="chkStud" runat="server" ToolTip='<%# Eval("IDNO") %>' Checked='<%# Eval("LETTER_GENERATE") %>'/>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                            <td>
                                                <asp:CheckBox ID="chkStud" runat="server" ToolTip='<%# Eval("IDNO") %>' Checked='<%# Eval("LETTER_GENERATE") %>'/>
                                                <%--<asp:HiddenField ID="hfApplied" runat="server" Value='<%# Eval("LOCK") %>' />--%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Width="60px" Visible="false" />
                            </div>
                        </asp:Panel>
            </td>
            
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

