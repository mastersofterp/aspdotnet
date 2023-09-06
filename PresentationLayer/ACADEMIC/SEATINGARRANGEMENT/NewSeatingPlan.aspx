<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="NewSeatingPlan.aspx.cs" Inherits="ACADEMIC_NewSeatingPlan" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../INCLUDES/prototype.js" type="text/javascript"></script>

    <script src="../../INCLUDES/scriptaculous.js" type="text/javascript"></script>

    <script src="../../INCLUDES/modalbox.js" type="text/javascript"></script>

    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                SEATING PLAN
                <%-- Button used to launch the help (animation) --%>
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Reset the sample so it can be played again --%>
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
                <%--  Enable the button so it can be played again --%>
                <%--  <ajaxToolKit:TabContainer ID="tcSeating" runat="server" Width="100%" ActiveTabIndex="1">
                    <ajaxToolKit:TabPanel ID="tabSeat" runat="server" HeaderText="Seating Configuration"
                        Visible="true" TabIndex="0">
                        <ContentTemplate>--%>
                <fieldset class="fieldset">
                    <legend class="legend">Seating Plan Entry</legend>
                    <table width="100%" style="margin-right: 0px">
                        <tr>
                            <td class="form_left_label" style="width: 9%">
                                Session :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSession" Width="30%" runat="server" AppendDataBoundItems="True"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="process"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Degree :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" Width="30%"
                                    TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="process"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="vsSeating" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" Style="margin-bottom: 1px" ValidationGroup="process" />
                            </td>
                            <td class="form_left_text">
                                <asp:Button ID="btnProcess" runat="server" Text="Process Seating Plan" ValidationGroup="process"
                                    TabIndex="3" OnClick="btnProcess_Click" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="4" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table width="100%" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="width: 5%;">
                            <fieldset class="fieldset">
                                <legend class="legend">Exam Dates</legend>
                                <table width="100%" id="tblShift" style="margin-right: 0px">
                                    <tr>
                                        <td style="padding: 10px; text-align: center; width: 100%" align="center" valign="top"
                                            colspan="2">
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="titlebar">
                                                    Exam Dates</div>
                                                <asp:Panel ID="pnlDate" runat="server" Height="250px" ScrollBars="Auto">
                                                    <asp:ListView ID="lvDate" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <table id="tblSearchResults" class="datatable" cellpadding="0" cellspacing="0">
                                                                    <tr class="header">
                                                                        <th style="width: 10%">
                                                                            Select
                                                                        </th>
                                                                        <th style="width: 60%">
                                                                            Date Of Exam
                                                                        </th>
                                                                        <th style="width: 30%">
                                                                            Shift
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
                                                                <td width="10%">
                                                                    <asp:RadioButton ID="rdoSelect" runat="server" onclick="SetUniqueRadioButton(this)"
                                                                        GroupName="Exam" />
                                                                </td>
                                                                <td width="60%">
                                                                    <asp:Label ID="lblExamDate" runat="server" Text=' <%# Eval("EXAMDATE")%>'> </asp:Label>
                                                                </td>
                                                                <td width="30%">
                                                                    <asp:Label ID="lblSlotname" runat="server" Text=' <%# Eval("SLOTNAME")%>' ToolTip='<%# Eval("SLOTNO") %>'> </asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="text-align: center">
                                            <asp:Button ID="btnShiftDetails" runat="server" Text="Shift Details" OnClick="btnShiftDetails_Click"
                                                OnClientClick="return ShowShift();" TabIndex="5" />
                                            &nbsp;<asp:Button ID="btnClearall" runat="server" TabIndex="6" Text="Cancel" OnClick="btnClearall_Click" />
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoSelection" runat="server" RepeatDirection="Horizontal"
                                                TabIndex="7" Style="margin-right: 1px">
                                                <asp:ListItem Value="0">Reg</asp:ListItem>
                                                <asp:ListItem Value="1">Ex</asp:ListItem>
                                                <asp:ListItem Value="2" Selected="True">Both</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td width="5%">
                            <fieldset class="fieldset">
                                <legend class="legend">Shifts</legend>
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <div id="Div6" class="vista-grid">
                                                <div class="titlebar">
                                                    Shift</div>
                                                <asp:Panel ID="pnlShift" runat="server" Height="200px" ScrollBars="Auto">
                                                    <asp:GridView ID="gvShift" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        CssClass="datatable" Style="margin-right: 4px">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sem">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNAME")%>' />
                                                                    <asp:HiddenField ID="hdfSem" Value='<%# Eval("SEMESTERNAME")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Branch">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHNAME")%>' ToolTip='<%# Eval("BRANCHNAME")%>' />
                                                                    <asp:HiddenField ID="hdfBranch" Value='<%# Eval("BRANCHNAME")%>' runat="server">
                                                                    </asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CourseName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSENAME")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    <asp:HiddenField ID="hdfCourse" Value='<%# Eval("COURSENAME")%>' runat="server">
                                                                    </asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReg" runat="server" Text='<%# Eval("REG")%>' ToolTip='<%# Eval("REG")%>' />
                                                                    <asp:HiddenField ID="hdfREG" Value='<%# Eval("REG")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ex">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEx" runat="server" Text='<%# Eval("EX")%>' ToolTip='<%# Eval("EX")%>' />
                                                                    <asp:HiddenField ID="hdfEX" Value='<%# Eval("EX")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tot">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTot" runat="server" Text='<%# Eval("TOT")%>' ToolTip='<%# Eval("TOT")%>' />
                                                                    <asp:HiddenField ID="hdfTOT" Value='<%# Eval("TOT")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbStatus" runat="server" Width="5%" Style="text-align: right; font-weight: bold;"
                                                                        Enabled="false" ToolTip="Seating Arrangement Done if selected" onclick="StudentCapacity(this)"
                                                                        Checked='<%# Eval("STATUS").ToString() == "1" ? true: false%> ' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ExDtNo" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblexDtno" runat="server" Width="5%" Style="text-align: right; font-weight: bold;"
                                                                        ToolTip='<%# Eval("EXDTNO")%>' Visible="false" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Width="5%" Style="text-align: right; font-weight: bold;"
                                                                        ToolTip='<%# Eval("STATUS")%>' Visible="false" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="gv_header" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td width="5%">
                            <fieldset class="fieldset">
                                <legend class="legend">Room Preference</legend>
                                <table id="tblRoom" cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td>
                                            <div id="Div7" class="vista-grid">
                                                <div class="titlebar">
                                                    Room Preference</div>
                                                <asp:Panel ID="pnlRoom" runat="server" Height="200px" ScrollBars="Auto">
                                                    <asp:GridView ID="gvRoom" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        CssClass="datatable" Style="margin-right: 3px">
                                                        <AlternatingRowStyle BackColor="#FFFFD2" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ROOMNAME" HeaderText="RoomName">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="DEPTCODE" HeaderText="Department">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CONFIGURED" HeaderText="Configured">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Capacity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCapacity" runat="server" Text='<%# Eval("ACTUALCAPACITY").ToString() == "0"? Eval("ROOM_CAPACITY"):Eval("ACTUALCAPACITY")  %>'
                                                                        ToolTip='<%# Eval("ACTUALCAPACITY")%>' />
                                                                    <asp:HiddenField ID="hdfcapacity" Value='<%# Eval("ACTUALCAPACITY")%>' runat="server">
                                                                    </asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Preference">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPreference" MaxLength="3" runat="server" Width="30%" Style="text-align: center;
                                                                        font-weight: bold;" ToolTip='<%# Eval("ROOM_NO") %>' onblur="return valiadatePreference(this);"
                                                                        Text='<%# Convert.ToInt16(Eval("PREFERENCE")) >0 ? Eval("PREFERENCE"):Eval("srno") %>' />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtPreference" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtPreference">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <asp:HiddenField ID="hdfpreference" Value='<%# Eval("PREFERENCE")%>' runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbRoom" runat="server" Width="7%" Style="text-align: center; font-weight: bold;"
                                                                        ToolTip='<%# Eval("ACTUALCAPACITY")%>' onclick="CheckOne(this)" Checked="false" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Locked" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="gv_header" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 9%; text-align: left;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnRevert" runat="server" Text="Revert to Default" OnClick="btnRevert_Click"
                                                            TabIndex="7" />
                                                        <asp:HiddenField ID="hdfPreferenceList" runat="server" />
                                                        <asp:HiddenField ID="hdfTotRoom" runat="server" />
                                                        <asp:Button ID="btnSet" runat="server" Text="Set as Default" OnClick="btnSet_Click"
                                                            TabIndex="8" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAllRoom" runat="server" Enabled="False" Text="0" Width="30%"
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <table width="100%">
                                <tr>
                                    <td>
                                        Odd Columns Capacity
                                        <asp:TextBox ID="txtOdd" runat="server" Width="20%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        Even Columns Capacity
                                        <asp:TextBox ID="txtEven" runat="server" Width="20%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        Selected Room Capacity
                                        <asp:TextBox ID="txtSelected" runat="server" Width="20%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        Total Appearing
                                        <asp:TextBox ID="txtTotal" runat="server" Width="20%" ReadOnly="True" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td width="5%">
                            <fieldset class="fieldset">
                                <legend class="legend">Branches in Odd Column</legend>
                                <table id="tblodd" cellpadding="2" cellspacing="2" width="100%" style="margin-right: 0px">
                                    <tr>
                                        <td colspan="2">
                                            <div id="Div2" class="vista-grid">
                                                <div class="titlebar">
                                                    Branches in Odd Column</div>
                                                <asp:Panel ID="pnlOdd" runat="server" ScrollBars="Auto" Height="200px">
                                                    <asp:GridView ID="gvOdd" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        CssClass="datatable" Style="margin-right: 3px" OnRowCommand="gvOdd_RowCommand">
                                                        <AlternatingRowStyle BackColor="#FFFFD2" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sem">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNAME")%>' />
                                                                    <asp:HiddenField ID="hdfSem" Value='<%# Eval("SEMESTERNAME")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Branch">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHNAME")%>' ToolTip='<%# Eval("BRANCHNAME")%>' />
                                                                    <asp:HiddenField ID="hdfBranch" Value='<%# Eval("BRANCHNAME")%>' runat="server">
                                                                    </asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CourseName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSENAME")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    <asp:HiddenField ID="hdfCourse" Value='<%# Eval("COURSENAME")%>' runat="server">
                                                                    </asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReg" runat="server" Text='<%# Eval("REG")%>' ToolTip='<%# Eval("REG")%>' />
                                                                    <asp:HiddenField ID="hdfREG" Value='<%# Eval("REG")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ex">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEx" runat="server" Text='<%# Eval("EX")%>' ToolTip='<%# Eval("EX")%>' />
                                                                    <asp:HiddenField ID="hdfEX" Value='<%# Eval("EX")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tot">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTOT" runat="server" Text='<%# Eval("TOT")%>' ToolTip='<%# Eval("TOT")%>' />
                                                                    <asp:HiddenField ID="hdfTOT" Value='<%# Eval("TOT")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <%-- <asp:CheckBox ID="cbodd" runat="server" Width="5%" Style="text-align: right; font-weight: bold;"
                                                            ToolTip='<%# Eval("TOT")%>' Checked="false" onclick="deleteRow(this)"/>--%>
                                                                    <%--<asp:ImageButton ID="btnDeleteOddDetail" runat="server" OnClick="btnDeleteOddDetail_Click"
                                                            CommandArgument='<%#Eval("COURSENO")%>' CommandName= '<%# Eval("EXDTNO")%>' ImageUrl="~/images/delete.gif" ToolTip='<%# Eval("TOT")%>' />--%>
                                                                    <asp:ImageButton ID="btnDeleteOddDetail" runat="server" OnClick="btnDeleteOddDetail_Click"
                                                                        CommandArgument='<%#Eval("COURSENO")%>' ImageUrl="~/images/delete.gif" ToolTip='<%# Eval("TOT")%>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="gv_header" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Remaining Capacity
                                            <asp:TextBox runat="server" ID="txtRemainingOdd1" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td width="5%">
                            <fieldset class="fieldset">
                                <legend class="legend">Branches in Even Column</legend>
                                <table id="tblEven" cellpadding="2" cellspacing="2" width="100%" style="margin-right: 0px">
                                    <tr>
                                        <td colspan="2">
                                            <div id="Div4" class="vista-grid">
                                                <div class="titlebar">
                                                    Branches in Even Column</div>
                                                <asp:Panel ID="pnlEven" runat="server" Height="200px" ScrollBars="Auto">
                                                    <asp:GridView ID="gvEven" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        CssClass="datatable" Style="margin-right: 3px">
                                                        <AlternatingRowStyle BackColor="#FFFFD2" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sem">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNAME")%>' />
                                                                    <asp:HiddenField ID="hdfSem" Value='<%# Eval("SEMESTERNAME")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Branch">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHNAME")%>' ToolTip='<%# Eval("BRANCHNAME")%>' />
                                                                    <asp:HiddenField ID="hdfBranch" Value='<%# Eval("BRANCHNAME")%>' runat="server">
                                                                    </asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CourseName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSENAME")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                    <asp:HiddenField ID="hdfCourse" Value='<%# Eval("COURSENAME")%>' runat="server">
                                                                    </asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReg" runat="server" Text='<%# Eval("REG")%>' ToolTip='<%# Eval("REG")%>' />
                                                                    <asp:HiddenField ID="hdfREG" Value='<%# Eval("REG")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ex">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEx" runat="server" Text='<%# Eval("EX")%>' ToolTip='<%# Eval("EX")%>' />
                                                                    <asp:HiddenField ID="hdfEX" Value='<%# Eval("EX")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tot">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTOT" runat="server" Text='<%# Eval("TOT")%>' ToolTip='<%# Eval("TOT")%>' />
                                                                    <asp:HiddenField ID="hdfTOT" Value='<%# Eval("TOT")%>' runat="server"></asp:HiddenField>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <%-- <asp:CheckBox ID="cbeven" runat="server" Width="5%" Style="text-align: right; font-weight: bold;"
                                                        <asp:ImageButton ID="btnDeleteEvenDetail" runat="server" OnClick="btnDeleteEvenDetail_Click"
                                                            CommandArgument='<%#Eval("COURSENO")%>' CommandName='<%#Eval("EXDTNO")%>' ImageUrl="~/images/delete.gif" ToolTip='<%# Eval("TOT")%>' />--%>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="btnDeleteEvenDetail_Click"
                                                                        CommandArgument='<%#Eval("COURSENO")%>' ImageUrl="~/images/delete.gif" ToolTip='<%# Eval("TOT")%>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="3%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="gv_header" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Remaining Capacity
                                            <asp:TextBox runat="server" ID="txtRemainEven1" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%;" colspan="2">
                            <fieldset class="fieldset">
                                <legend class="legend">Branch Combinations</legend>
                                <table width="100%" id="tblbranch" style="margin-right: 0px;">
                                    <tr>
                                        <td style="padding: 10px; text-align: center; width: 50%" rowspan="7" align="center">
                                            <div id="Div3" class="vista-grid">
                                                <div class="titlebar" style="text-align: center;">
                                                    Odd Column</div>
                                                <br />
                                                BRANCH 1
                                                <asp:DropDownList ID="ddlBench1" Width="50%" runat="server" AppendDataBoundItems="True"
                                                    TabIndex="9" AutoPostBack="True" OnSelectedIndexChanged="ddlBench_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <br />
                                                BRANCH 3
                                                <asp:DropDownList ID="ddlBench3" Width="50%" runat="server" AppendDataBoundItems="True"
                                                    TabIndex="10" AutoPostBack="True" OnSelectedIndexChanged="ddlBench_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px; padding-top: 5px; text-align: center; width: 50%" align="center">
                                            <div id="Div5" class="vista-grid">
                                                <div class="titlebar" style="text-align: center">
                                                    Even Column</div>
                                                <br />
                                                BRANCH 2
                                                <asp:DropDownList ID="ddlBench2" Width="50%" runat="server" AppendDataBoundItems="True"
                                                    TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlBench_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <br />
                                                BRANCH 4
                                                <asp:DropDownList ID="ddlBench4" Width="50%" runat="server" AppendDataBoundItems="True"
                                                    TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlBench_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td width="5%" style="text-align: center" colspan="2">
                            <asp:Button ID="btnSeating" runat="server" Text="Seating Plan" Width="12%" OnClick="btnSeating_Click"
                                TabIndex="13" />
                            &nbsp;
                            <asp:Button ID="btnRoomwise" runat="server" Text="Roomwise Report" Width="15%" ValidationGroup="process"
                                OnClick="btnRoomwise_Click" TabIndex="14" />
                            &nbsp;
                            <asp:Button ID="btnStastical" runat="server" Text="Statistical Report" ValidationGroup="process"
                                Width="15%" OnClick="btnStastical_Click" TabIndex="15" />
                            &nbsp;
                            <asp:Button ID="btnRoomSeats" runat="server" Text="Room Seat Report" ValidationGroup="process"
                                Width="15%" OnClick="btnRoomSeats_Click" TabIndex="16" />
                            &nbsp;
                            <asp:Button ID="btnCancel3" runat="server" Width="15%" TabIndex="17" Text="Cancel"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

       function ShowShift() {
          var recValue = "";
          var tbl = document.getElementById('tblSearchResults');
          if (tbl != null && tbl.rows && tbl.rows.length > 0) {
          for (i = 1; i < tbl.rows.length; i++) {
           var dataRow = tbl.rows[i];
           var dataCell = dataRow.firstChild;
           var rdo = dataCell.firstChild;
           if (rdo.checked) {
            recValue = rdo.value;
              }
            }
          }
          if (recValue =="")
          {
         alert("Please Select a Record");
         return false;
         }
         return true;
        }
        function GetSelectedRecord() {
            var recordValue = "";
            try {
                var tbl = document.getElementById('tblSearchResults');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var rdo = dataCell.firstChild;
                        if (rdo.checked) {
                            recordValue = rdo.value;
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return recordValue;
        }


        function RomCapacity(chk) {
            var chkboxid = chk.id;
            var txtTot = document.getElementById('<%= txtSelected.ClientID %>');
            var txtodd = document.getElementById('<%= txtOdd.ClientID %>');
            var txteven = document.getElementById('<%= txtEven.ClientID %>');

            var hdfcapacityname = chkboxid.substring(0, 39) + 'hdfcapacity';

            var hdfcapacity = document.getElementById(hdfcapacityname).value;

            if (chk.checked == true) {
                txtTot.value = Number(txtTot.value) + Number(hdfcapacity) * 2;
                txteven.value = Number(txtTot.value) / 2;
                txtodd.value = Number(txtTot.value) - Number(txteven.value);
            }
            else {
                txtTot.value = Number(txtTot.value) - Number(hdfcapacity)*2;
                txteven.value = Number(txtTot.value) / 2;
                txtodd.value = Number(txtTot.value) - Number(txteven.value);
            }
        }

       
        //----------------------------------------------------------------------------------------//

        function StudentCapacity(chk) {
            var chkboxidno = chk.id;
            var txtTOT = document.getElementById('<%= txtTotal.ClientID %>');
            var hdfTOTNO = chkboxidno.substring(0, 40) + 'hdfTOT';

            var hdftot = document.getElementById(hdfTOTNO).value;

            if (true == chk.checked) {
                txtTOT.value = Number(txtTOT.value) + Number(hdftot);
            }
            else {
                txtTOT.value = Number(txtTOT.value) - Number(hdftot);
            }
        }

        //--------------------------------------------------------------------------------//
        function txtChanged(mytxt) {
            document.all.txtTotal.value = 1;
        }
        //----------------------------------------------------------------------------------//
        function deleteRow(chk) {
            try {
                var chkbox = chk.id;
                var table = document.getElementById('tblodd');
                var rowCount = table.rows.length;

                for (var i = 0; i < rowCount; i++) {
                    var row = table.rows[i];
                    chkbox = row.cells[0];
                    if (null != chkbox && true == chkbox.checked) {
                        table.deleteRow(i);
                        rowCount--;
                        i--;
                    }
                }
            } catch (e) {
                alert(e);
            }
        }
        //---------------------------------------------------------------------------//

        function deleteRowEven(chk) {
            try {
                var chkbox = chk.id;
                var tabledata = document.getElementById('tblEven');
                var rowCountEven = tabledata.rows.length;

                for (var i = 0; i < rowCountEven; i++) {
                    var row = tabledata.rows[i];
                    chkbox = row.cells[0];
                    if (null != chkbox && true == chkbox.checked) {
                        tabledata.deleteRow(i);
                        rowCountEven--;
                        i--;
                    }
                }
            } catch (e) {
                alert(e);
            }
        }
        //----------------------------------------------------------------------------------//
        function totRooms(chk) {
            var txtTot = document.getElementById('<%= txtAllRoom.ClientID %>');
            if (chk.checked == true) {
                if ((Number(txtTot.value) + 1) > totNoRooms) {
                    chk.checked = false;
                    alert('Only ' + totNoRooms + ' Rooms Allowed for Allotment!!!');
                }
                else {
                    //txtTot.value = Number(txtTot.value) + 1 ;   
                }
            }
            else {
                //txtTot.value = Number(txtTot.value) - 1 ;   
            }
        }
        //----------------------------------------------------------------------------//

        function Checkuncheck(chk) {        
            var obj = document.getElementById('cbStatus');
            //for disable the control
            if (obj.checked) {
                obj.Enabled = false;
            }
            else //for enable make it to null like ''
            {
                obj.Enabled = true;
            }
        }

        //-------------------------------------------------------------------------------//
        function EnableCheck(chk) {
            var obj = document.getElementById('cbStatus');
            var clk = document.getElementById('btnDeleteOddDetail') 
            //for disable the control
            if (clk.clicked) {
                obj.Enabled = true;
            }
            else //for enable make it to null like ''
            {
                obj.Enabled = false;
            }
        }
                       
      
        //-------------------------------------------------------------------------------//
        function SetUniqueRadioButton(current) {
            var tbl = document.getElementById('tblSearchResults');
            if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                for (i = 0; i < tbl.rows.length - 1; i++) {
                    var elm = document.getElementById('ctl00_ContentPlaceHolder1_lvDate_ctrl' + i + '_rdoSelect');
                    if (elm.type == 'radio') {
                        elm.checked = false;
                    }
                }
            }
            current.checked = true;
        }
        //-------------------------------------------------------------------------------//
        function valiadatePreference(txt){
            var hdf = document.getElementById('<%= hdfPreferenceList.ClientID %>'); 
            var ValidChars = "0123456789";
            var num = true;
            var mChar;
            if(hdf.value.indexOf(","+txt.defaultValue+",") >= 0){
                var frmStr = hdf.value.substring(0,hdf.value.indexOf(","+txt.defaultValue+","));
                var toStr = hdf.value.substring(hdf.value.indexOf(","+txt.defaultValue+",")+txt.defaultValue.length + 1)
                hdf.value = frmStr + toStr;
            }
            
            if(txt.value == ""){
                alert("Preference cannot be Blank ");
                txt.value = '';
                 txt.focus();
             }
            else{
                 if(txt.value == "0"){
                  alert("Preference cannot be Zero");
                  txt.value = '';
                  txt.focus();
                 }
                  else  {
                     for (i=0 ; i < txt.value.length && num == true;i++){
				        mChar = txt.value.charAt(i);
        				if (ValidChars.indexOf(mChar) == -1){
		    	    	num = false;
			    	    txt.value = '';
					    alert("Error! Only Numeric Values Are Allowed")
					    txt.select();
					    txt.focus();
				       }
			         }
			        if(hdf.value.indexOf(","+ txt.value +",")>=0){
			            num = false;
                        txt.value = '';
                        alert("Preference no. already exists! Preference must be Unique!")
                        txt.focus();
                        }
                    else{
                        hdf.value = hdf.value + "," +txt.value+",";
                        }
                    }
              }
        }
        //-------------------------------------------------------------------
        function CheckOne(chk){
        var hdf = document.getElementById('<%= hdfTotRoom.ClientID %>'); 
         var elm;
         for (i=2; i<=Number(hdf.value)+1; i++) {
        
         if(i<10)
              elm = document.getElementById('ctl00_ContentPlaceHolder1_gvRoom_ctl0' + i + '_cbRoom');
         else
              elm = document.getElementById('ctl00_ContentPlaceHolder1_gvRoom_ctl' + i + '_cbRoom');
        if(elm.type == "checkbox")
               elm.checked = false;
         }
            chk.checked = true;
        }  
    </script>

</asp:Content>
