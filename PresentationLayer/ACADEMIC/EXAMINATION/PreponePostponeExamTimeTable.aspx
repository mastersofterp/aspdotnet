<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" 
CodeFile="PreponePostponeExamTimeTable.aspx.cs" 
Inherits="ACADEMIC_EXAMINATION_PreponePostponeExamTimeTable" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExamdate"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                Loading..
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                PREPONE & POSTPONE EXAM TIMETABLE
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>
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
    <br />
    <asp:UpdatePanel ID="updExamdate" runat="server">
        <ContentTemplate>
            <fieldset class="fieldset" style="width: 98%">
                <legend class="legend"> Selection Criteria For Exam TimeTable</legend>
                <table cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="width: 16%">
                            Session :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Width="40%"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" />
                            <%--<asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select session"
                                InitialValue="0" SetFocusOnError="true" />--%>
                        </td>
                        <td rowspan="13" style="vertical-align: top" width="20%">
                            <asp:Panel ID="PanelLvDate" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvDate" runat="server" scrollbar="auto">
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <table class="datatable" cellpadding="0" cellspacing="0">
                                                <tr class="header">
                                                    <th width="10%">
                                                        DAY NO.
                                                    </th>
                                                    <th width="10%">
                                                        EXAM DATE
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <div align="center" class="data_label">
                                        </div>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td width="10%">
                                                <%# Eval("DAYNO")%>
                                            </td>
                                            <td width="10%">
                                                <%# Eval("EXAMDATE","{0:dd/MM/yyyy}")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="width: 16%">
                            Exam Time Table Type :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExTTType" runat="server" AppendDataBoundItems="true" Width="45%" AutoPostBack="true"
                                TabIndex="1" onselectedindexchanged="ddlExTTType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="rfvExTTType" runat="server" ControlToValidate="ddlExTTType"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Exam Time Table Type"
                                SetFocusOnError="true" InitialValue="0" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">Day No. : </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 37px">
                                        <asp:DropDownList ID="ddlDayNo" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="ddlDayNo_SelectedIndexChanged" TabIndex="2" Width="110px">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDayNo" runat="server" ControlToValidate="ddlDayNo"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Day No."
                                            SetFocusOnError="true" InitialValue="0" />--%></td>
                                    <td style="width: 42px">&nbsp; Slot : </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="true" AutoPostBack="true" onselectedindexchanged="ddlSlot_SelectedIndexChanged" TabIndex="4" Width="148px">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Slot" SetFocusOnError="true"
                                            InitialValue="0" />--%></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%; height: 39px;">Exam Date : </td>
                        <td style="height: 39px">
                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" ValidationGroup="submit" Width="100px" />
                            <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />
                            <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgExamDate" TargetControlID="txtExamDate" />
                            <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtExamDate" />
                            <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" ControlExtender="meExamDate" ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Exam Date" ErrorMessage="Please Enter Exam Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid" IsValidEmpty="false" SetFocusOnError="true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">Degree : </td>
                        <td style="width: 40%">
                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="5" Width="70%">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>--%></td>
                    </tr>
                    <tr>
                        <td style="width: 16%; vertical-align: top">Branch : </td>
                        <td style="width: 40%; vertical-align: top">
                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="6" Width="70%">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>--%></td>
                    </tr>
                    <tr>
                        <td style="width: 16%; vertical-align: top">Scheme : </td>
                        <td style="width: 40%; vertical-align: top">
                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="7" Width="70%">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>--%></td>
                    </tr>
                    <tr>
                        <td style="width: 16%">Semester : </td>
                        <td>
                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="8" Width="30%">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Semester"
                                SetFocusOnError="true" InitialValue="0" />--%></td>
                    </tr>
                    <tr>
                        <%-- <td style="width: 16%; vertical-align: top">
                            Course :
                        </td>--%>
                        <td>
                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="9" Visible="false" Width="290px">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvddlCourse" runat="server" ControlToValidate="ddlCourse"
                                Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="Submit"></asp:RequiredFieldValidator>--%></td>
                    </tr>
                    <tr>
                        <td style="width: 16%; vertical-align: top">&nbsp;</td>
                        <td style="width: 40%; vertical-align: top">
                            <asp:Label ID="lblStudCnt" runat="server" style="color: #990000"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr style="display: none">
                        <td style="width: 16%">
                            Day Name :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="true" Width="30%"
                                TabIndex="10">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDay" runat="server" ControlToValidate="ddlDay"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Day Name"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>
                    </tr>--%>
                   
                </table>
            </fieldset>
            
            <fieldset class="fieldset" style="width: 98%">
                <legend class="legend">Changes of Prepone & Postpone Exam TimeTable</legend>
                <table width="100%">
                <tr>
                        <td>
                         Day No. :
                        </td>
                           
                      
                    
                                    <td style="width: 0px" align="left">
                                        <asp:DropDownList ID="ddlDayNo1" runat="server" AppendDataBoundItems="true" Width="110px"
                                            TabIndex="2" AutoPostBack="True" >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDayNo1"
                                             Display="None" ErrorMessage="Please Select Day No."
                                            SetFocusOnError="true" InitialValue="0" ValidationGroup="update"/>
                                    </td>
                                   
                    
                 
                           
                        
                        <td >
                            Exam Date :
                            <asp:TextBox ID="txtExamDate1" runat="server" 
                                Width="100px" />
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" />
                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtExamDate1" PopupButtonID="Image1"  />
                            <ajaxToolKit:MaskedEditExtender ID="meExamDate1" runat="server" TargetControlID="txtExamDate1"
                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                MaskType="Date" />
                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                ControlExtender="meExamDate1" ControlToValidate="txtExamDate1" IsValidEmpty="false"
                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                InvalidValueBlurredMessage="*" ValidationGroup="update" SetFocusOnError="true" />
                        </td>
                                    </tr>
                                    <tr>
                                    
                                    <td>
                                   Slot :
                                    </td>
                                        
                                    
                                    <td>
                                        <asp:DropDownList ID="ddlSlot1" runat="server" AppendDataBoundItems="true" Width="148px" 
                                           TabIndex="4" >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSlot1"
                                            Display="None" ErrorMessage="Please Select Slot" SetFocusOnError="true" ValidationGroup="update"
                                            InitialValue="0" />
                                    </td>
                                    
                            
                    
                      
                       </tr>
                            
                 <tr>
                        <td style="width: 16%">
                            &nbsp;
                        </td>
                        <td>
                           &nbsp;
                        </td>
                        <td colspan="2" style="padding-left: 10px" >
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="update"
                                Width="80px" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="update" />
                        
                        </td>
                    </tr>
                    
                </table>
            </fieldset>
            <%--<fieldset class="fieldset" style="width: 98%">
                <legend class="legend">Time Table Report</legend>
                <table width="100%">
                    <tr>
                        <td style="width: 11%">
                            Session :
                        </td>
                        <td style="width: 42%">
                            <asp:DropDownList ID="ddlSessionReport" runat="server" AppendDataBoundItems="true"
                                Width="30%" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlSessionReport" runat="server" ControlToValidate="ddlSessionReport"
                                Display="None" ErrorMessage=" Please Select Session " InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="Report"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 11%">
                            Degree :
                        </td>
                        <td style="width: 42%">
                            <asp:DropDownList ID="ddlDegreeReport" runat="server" AppendDataBoundItems="true"
                                Width="30%" AutoPostBack="True" OnSelectedIndexChanged="ddlDegreeReport_SelectedIndexChanged"
                                TabIndex="1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlDegreeReport" runat="server" ControlToValidate="ddlDegreeReport"
                                Display="None" ErrorMessage=" Please Select Degree " InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="Report"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 11%">
                            Branch Name:
                        </td>
                        <td style="width: 42%">
                            <asp:DropDownList ID="ddlBranchReport" runat="server" AppendDataBoundItems="true"
                                Width="30%" AutoPostBack="True" TabIndex="2">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBranchReport" runat="server" ControlToValidate="ddlBranchReport"
                                Display="None" ErrorMessage=" Please Select Branch " InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="Report"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">
                            Exam Time Table Type :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExTTTypeReport" runat="server" AppendDataBoundItems="true" Width="30%"
                                TabIndex="1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExTTTypeReport" runat="server" ControlToValidate="ddlExTTTypeReport"
                                 Display="None" ErrorMessage="Please Select Exam Time Table Type"
                                SetFocusOnError="true" InitialValue="0" ValidationGroup="Report" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 11%">
                        </td>
                        <td style="padding-top: 10px">
                            <asp:Button ID="btnReport" runat="server" Text="Exam Time Table Report" OnClick="btnReport_Click"
                                ValidationGroup="Report" />&nbsp;&nbsp;<asp:Button ID="btnCancelReport" runat="server"
                                    Text="Cancel" Width="80px" OnClick="btnCancelReport_Click" />
                            <br />
                            <asp:ValidationSummary ID="vsReport" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="Report" />
                        </td>
                    </tr>
                </table>
            </fieldset>--%>
            <asp:Panel ID="PanelLvExamDays" runat="server">
                <asp:ListView ID="lvExamday" runat="server">
                    <LayoutTemplate>
                        <div id="demo-grid" class="vista-grid">
                            <div class="titlebar">
                              Select  Exam  List
                            </div>
                            <table class="datatable" cellpadding="0" cellspacing="0">
                                <tr class="header">
                                    <th>
                                        Select
                                         <asp:CheckBox ID="chkheader" runat="server"  onclick ="return totAll(this);"/>
                                    </th>
                                    
                                    <th>
                                        Day No
                                    </th>
                                    <th>
                                        Degree
                                    </th>
                                    <th>
                                        Branch
                                    </th>
                                    <th>
                                        Scheme
                                    </th>
                                    <th>
                                        Course
                                    </th>
                                    <th>
                                        Exam Date
                                    </th>
                                    <th>
                                        Slot
                                    </th>
                                    <th>
                                        Time
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                            <td>
                                <asp:CheckBox ID="chkExdtNO" runat="server" CausesValidation="false"
                                  ToolTip='<%# Eval("EXDTNO") %>'
                                    />
                            </td>
                            
                            <td>
                                <%# Eval("DAYNO")%>
                            </td>
                            <td>
                                <%# Eval("DEGREENAME")%>
                            </td>
                            <td>
                                <%# Eval("BRANCHNAME")%>
                            </td>
                            <td>
                                <%# Eval("SCHEMENAME")%>
                            </td>
                            <td>
                                <%# Eval("COURSE_NAME")%>
                            </td>
                            <td>
                                <%# Eval("EXAMDATE")%>
                            </td>
                            <td>
                                <%# Eval("SLOTNAME")%>
                            </td>
                            <td>
                                <%# Eval("TIMEFROM")%>
                                -
                                <%# Eval("TIMETO")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                            <td>
                            <asp:CheckBox ID="chkExdtNO" runat="server" CausesValidation="false"
                                  ToolTip='<%# Eval("EXDTNO") %>'
                                    />
                            </td>
                          
                            <td>
                                <%# Eval("DAYNO")%>
                            </td>
                            <td>
                                <%# Eval("DEGREENAME")%>
                            </td>
                            <td>
                                <%# Eval("BRANCHNAME")%>
                            </td>
                            <td>
                                <%# Eval("SCHEMENAME")%>
                            </td>
                            <td>
                                <%# Eval("COURSE_NAME")%>
                            </td>
                            <td>
                                <%# Eval("EXAMDATE")%>
                            </td>
                            <td>
                                <%# Eval("SLOTNAME")%>
                            </td>
                            <td>
                                <%# Eval("TIMEFROM")%>
                                -
                                <%# Eval("TIMETO")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
            </asp:Panel>
             
            <div id="divMsg" runat="Server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
</asp:Content>

