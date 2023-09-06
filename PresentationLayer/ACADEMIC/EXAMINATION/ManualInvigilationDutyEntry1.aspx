<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ManualInvigilationDutyEntry1.aspx.cs" Inherits="ACADEMIC_ManualInvigilationDutyEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 50px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updInvigAuto"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 189px; padding-left: 5px">
                    <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px" colspan="2">
                MANUAL INVIGILATION DUTY ENTRY
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
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
                        
                        <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                        <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                            <Move Horizontal="150" Vertical="-50" />
                            <Resize Width="260" Height="280" />
                            <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                        </Parallel>
                        
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
    <asp:UpdatePanel ID="updInvigAuto" runat="server">
        <ContentTemplate>
            <fieldset class="fieldset" style="width: 99%;">
                <legend class="legend">Manual Invigilator Duty Entry</legend>
                <table cellpadding="2" cellspacing="2" style="width: 90%;">
                    <tr>
                        <td class="form_left_label" style="width: 20%">
                            Session :
                        </td>
                        <td class="form_left_text" style="width: 8%">
                            <asp:DropDownList ID="ddlSession" Width="30%" AppendDataBoundItems="true" runat="server"
                                ValidationGroup="Show">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                Display="None" ValidationGroup="Show" InitialValue="0" ErrorMessage="Please Select Session">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Department :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlDepartment" Width="30%" AppendDataBoundItems="true" runat="server"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Slot :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlSlot" AppendDataBoundItems="true" Width="30%" runat="server">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            &nbsp;
                        </td>
                        <td class="form_button" style="width: 10%; text-align: left">
                            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                             <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnDaywiseReport" runat="server" Text="DaywiseReport" OnClick="btnDaywiseReport_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnInvigwiseReport" runat="server" Text="Invigilator Wise Report"
                                OnClick="btnInvigwiseReport_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlInvigDuty" runat="server" Visible="false"  Width="1000px" ScrollBars="Both">
                                <div id="demo-grid" class="vista-grid"  
                                    style="width:1000px; margin-left: 80px;">
                                    <div class="titlebar" style="width:1000px">
                                        Invigilation Duty</div>
                                    <asp:GridView ID="gvInvig" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvInvig_OnRowDataBound"
                                        CssClass="datatable">
                                        <HeaderStyle CssClass="gv_header" />
                                        <AlternatingRowStyle BackColor="#FFFFD2" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Faculty" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFac" runat="server" Text='<%# Bind("FACULTY") %>' ToolTip='<%# Bind("UA_NO") %>'
                                                        Font-Size="9pt" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY1" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK1" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO1")==  DBNull.Value ? false : Eval("DAYNO1").ToString().ToLower() == "true"? true :false %>' OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY2" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK2" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO2")==  DBNull.Value ? false : Eval("DAYNO2").ToString().ToLower() == "true"? true :false %>' AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY3" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK3" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO3") ==  DBNull.Value ? false : Eval("DAYNO3").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY4" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK4" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO4")==  DBNull.Value ? false : Eval("DAYNO4").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY5" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK5" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO5") ==  DBNull.Value ? false : Eval("DAYNO5").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY6" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK6" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO6") == DBNull.Value ? false : Eval("DAYNO6").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY7" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK7" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO7") ==  DBNull.Value ? false : Eval("DAYNO7").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY8"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK8" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO8") ==  DBNull.Value ? false : Eval("DAYNO8").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY9" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK9" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO9") ==  DBNull.Value ? false : Eval("DAYNO9").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY10"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK10" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO10")==  DBNull.Value ? false : Eval("DAYNO10").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY11" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK11" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO11")==  DBNull.Value ? false : Eval("DAYNO11").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY12"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK12" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO12")  ==  DBNull.Value ? false : Eval("DAYNO12").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY13"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK13" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO13")==  DBNull.Value ? false : Eval("DAYNO13").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY14" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK14" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO14") ==  DBNull.Value ? false : Eval("DAYNO14").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY15"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK15" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO15") ==  DBNull.Value ? false : Eval("DAYNO15").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY16"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK16" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO16") ==  DBNull.Value ? false : Eval("DAYNO16").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY17"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK17" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO17") ==  DBNull.Value ? false : Eval("DAYNO17").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY18"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK18" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO18")==  DBNull.Value ? false : Eval("DAYNO18").ToString().ToLower() == "true"?true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY19" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK19" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO19")==  DBNull.Value ? false : Eval("DAYNO19").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY20" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK20" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO20") ==  DBNull.Value ? false : Eval("DAYNO20").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY21" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK21" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO21") ==  DBNull.Value ? false : Eval("DAYNO21").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY22"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK22" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO22") ==  DBNull.Value ? false : Eval("DAYNO22").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY23" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK23" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO23") ==  DBNull.Value ? false : Eval("DAYNO23").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY24" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK24" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO24")==  DBNull.Value ? false : Eval("DAYNO24").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY25"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK25" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO25")==  DBNull.Value ? false : Eval("DAYNO25").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY26"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK26" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO26") ==  DBNull.Value ? false : Eval("DAYNO26").ToString().ToLower() == "true"? true :false %>' OnCheckedChanged="chkbox_OnCheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY27" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK27" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO27") ==  DBNull.Value ? false : Eval("DAYNO27").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY28" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK28" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO28") ==  DBNull.Value ? false : Eval("DAYNO28").ToString().ToLower() == "true"? true :false %>' AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY29" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK29" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO29") ==  DBNull.Value ? false : Eval("DAYNO29").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY30" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK30" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO30")==  DBNull.Value ? false : Eval("DAYNO30").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY31" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK31" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Eval("DAYNO31")  ==  DBNull.Value ? false : Eval("DAYNO31").ToString().ToLower() == "true"? true :false %>'  AutoPostBack="true" OnCheckedChanged="chkbox_OnCheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="DAY32" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK32" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY33" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK33" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY34" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK34" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY35" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK35" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY36" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK36" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY37" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK37" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY38" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK38" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY39" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK39" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY40" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CHK40" runat="server" ToolTip='<%# Bind("FACULTY") %>' Checked='<%# Bind("FACULTY") ==  DBNull.Value ? false : Bind("FACULTY").ToString() == "1": true :false %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Total Days"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                               <asp:TextBox ID="txtTotalDays" runat="server" Enabled = "false" Width="50%"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                      <asp:GridView ID="gvInvig1" CssClass="vista-grid" HeaderStyle-BackColor="#c0504d" AutoGenerateColumns="true"
                                            HeaderStyle-ForeColor="White" Visible="false"  runat="server" AlternatingRowStyle-BackColor="#efd3d2"
                                            Height="10px">
                                        </asp:GridView>
                                    </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
    function IsNumeric(txt)
    {
    if(txt != null && txt.value != ""){
    if(isNaN(txt.value)){
       alert("Please Enter only Numeric Characters");
       txt.value = "";
       txt.focus();
    }
    }
    }
    function Total()
   {
   var txtInvig = document.getElementById("ctl00_ContentPlaceHolder1_txtInvig");
   var txtReliver = document.getElementById("ctl00_ContentPlaceHolder1_txtReliver");
   var txtTotal = document.getElementById("ctl00_ContentPlaceHolder1_txtTotal");
    txtTotal.value = Number(txtInvig.value)+Number(txtReliver.value);
   }
    </script>

</asp:Content>
