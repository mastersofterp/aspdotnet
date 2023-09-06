<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ManualInvigilationDutyEntry.aspx.cs" Inherits="ACADEMIC_ManualInvigilationDutyEntry" %>

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
    
    <table cellpadding="0" cellspacing="0" width="100%">
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
    <table>
    <tr>
    <td width="10%">
     Session :
    </td>
     <td style="width: 937px">
      <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server"
                                ValidationGroup="Show" Width="200px">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                Display="None" ValidationGroup="Show" InitialValue="0" ErrorMessage="Please Select Session">
                            </asp:RequiredFieldValidator>
    </td>
    <td rowspan="4">
       
    </td>
    </tr>
    <tr>
    <td>
    Slot :
    </td>
     <td style="width: 937px">
      <asp:DropDownList ID="ddlSlot" AppendDataBoundItems="true" 
                                runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlSlot_SelectedIndexChanged" 
             Width="200px">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
     Department :
    </td>
     <td style="width: 937px">
     <asp:DropDownList ID="ddlDepartment"  AppendDataBoundItems="true" 
                                runat="server" Width="250px">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
    
    </td>
     <td style="width: 937px">
        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                             &nbsp;&nbsp;
                            <asp:Button ID="btnDaywiseReport" runat="server" Text="DaywiseReport" OnClick="btnDaywiseReport_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnInvigwiseReport" runat="server" Text="Invigilator Wise Report"
                                OnClick="btnInvigwiseReport_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </td>
    </tr>
    <tr>
    <td colspan="2">
        <asp:Panel ID="pnlInvigDuty" runat="server" ScrollBars="Both" 
            style="text-align: left" Visible="false" Width="900px">
            <div ID="demo-grid" class="vista-grid" style="width:900px;">
                <div class="titlebar" style="width:1360px;">
                    Invigilation Duty</div>
                <asp:GridView ID="gvInvig" runat="server" AutoGenerateColumns="False" 
                    CssClass="datatable" OnRowDataBound="gvInvig_OnRowDataBound" Width="900px">
                    <HeaderStyle CssClass="gv_header" />
                    <AlternatingRowStyle BackColor="#FFFFD2" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Faculty" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblFac" runat="server" Font-Size="9pt" 
                                    Text='<%# Bind("FACULTY") %>' ToolTip='<%# Bind("UA_NO") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY1">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK1" runat="server" 
                                    Checked='<%# Eval("DAYNO1")==  DBNull.Value ? false : Eval("DAYNO1").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                <asp:Label ID="lblChk1" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY2">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK2" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO2")==  DBNull.Value ? false : Eval("DAYNO2").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk2" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY3">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK3" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO3") ==  DBNull.Value ? false : Eval("DAYNO3").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk3" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY4">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK4" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO4")==  DBNull.Value ? false : Eval("DAYNO4").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk4" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY5">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK5" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO5") ==  DBNull.Value ? false : Eval("DAYNO5").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk5" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY6">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK6" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO6") == DBNull.Value ? false : Eval("DAYNO6").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk6" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY7">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK7" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO7") ==  DBNull.Value ? false : Eval("DAYNO7").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk7" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY8">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK8" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO8") ==  DBNull.Value ? false : Eval("DAYNO8").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk8" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY9">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK9" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO9") ==  DBNull.Value ? false : Eval("DAYNO9").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk9" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY10">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK10" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO10")==  DBNull.Value ? false : Eval("DAYNO10").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk10" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY11">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK11" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO11")==  DBNull.Value ? false : Eval("DAYNO11").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk11" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY12">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK12" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO12")  ==  DBNull.Value ? false : Eval("DAYNO12").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk12" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY13">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK13" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO13")==  DBNull.Value ? false : Eval("DAYNO13").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk13" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY14">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK14" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO14") ==  DBNull.Value ? false : Eval("DAYNO14").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk14" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY15">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK15" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO15") ==  DBNull.Value ? false : Eval("DAYNO15").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk15" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY16">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK16" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO16") ==  DBNull.Value ? false : Eval("DAYNO16").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk16" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY17">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK17" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO17") ==  DBNull.Value ? false : Eval("DAYNO17").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk17" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY18">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK18" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO18")==  DBNull.Value ? false : Eval("DAYNO18").ToString().ToLower() == "true"?true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk18" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY19">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK19" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO19")==  DBNull.Value ? false : Eval("DAYNO19").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk19" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY20">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK20" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO20") ==  DBNull.Value ? false : Eval("DAYNO20").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk20" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY21">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK21" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO21") ==  DBNull.Value ? false : Eval("DAYNO21").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk21" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY22" 
                            ItemStyle-Width="10px">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK22" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO22") ==  DBNull.Value ? false : Eval("DAYNO22").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk22" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="10px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY23">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK23" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO23") ==  DBNull.Value ? false : Eval("DAYNO23").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk23" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY24">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK24" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO24")==  DBNull.Value ? false : Eval("DAYNO24").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk24" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY25">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK25" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO25")==  DBNull.Value ? false : Eval("DAYNO25").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk25" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY26">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK26" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO26") ==  DBNull.Value ? false : Eval("DAYNO26").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk26" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY27">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK27" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO27") ==  DBNull.Value ? false : Eval("DAYNO27").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk27" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY28">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK28" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO28") ==  DBNull.Value ? false : Eval("DAYNO28").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk28" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY29">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK29" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO29") ==  DBNull.Value ? false : Eval("DAYNO29").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk29" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY30">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK30" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO30")==  DBNull.Value ? false : Eval("DAYNO30").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk30" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DAY31">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK31" runat="server" AutoPostBack="true" 
                                    Checked='<%# Eval("DAYNO31")  ==  DBNull.Value ? false : Eval("DAYNO31").ToString().ToLower() == "true"? true :false %>' 
                                    OnCheckedChanged="chkbox_OnCheckedChanged" ToolTip='<%# Bind("FACULTY") %>' />
                                    <asp:Label ID="lblChk31" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
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
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Total Days">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTotalDays" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                <asp:Label ID="lblTotalDays" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="gvInvig1" runat="server" 
                    AlternatingRowStyle-BackColor="#efd3d2" AutoGenerateColumns="true" 
                    CssClass="vista-grid" HeaderStyle-BackColor="#c0504d" 
                    HeaderStyle-ForeColor="White" Height="10px" Visible="false">
                </asp:GridView>
            </div>
        </asp:Panel>
    
    </td>
    <td >
    <asp:Panel ID="pnlGrdCount" runat="server" Visible="false"  Width="250px" 
                                ScrollBars="Auto" style="text-align: left" >
                          <div id="Div1" class="vista-grid"  
                                    style="width:250px;" align = "left">
                        <div class="titlebar" style="width:250px">
                                        Student and Faculty Count</div>
                                    <asp:GridView ID="grdNoofStud" runat="server" AutoGenerateColumns="False" Width="250px" 
                                        CssClass="datatable">
                                        <HeaderStyle CssClass="gv_header" />
                                        <AlternatingRowStyle BackColor="#FFFFD2" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="10px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDayNo" runat="server" Text='<%# Bind("dayno") %>' 
                                                        Font-Size="9pt" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10px" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Exam Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("examdate") %>'
                                                        Font-Size="9pt" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="No.of Faculty" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoFac" runat="server" Text='<%# Bind("facultycnt") %>' 
                                                        Font-Size="9pt" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Student" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotStud" runat="server" Text='<%# Bind("totstud") %>' 
                                                        Font-Size="9pt" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                          </div>
                          </asp:Panel>
    </td>
    </tr>
    </table>
    
  </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
   <div align="center">
    <asp:Button ID="btnExport" runat="server"  Text="Export" OnClick="btnExport_Click" Width="15%"/>
    </div>
   
   
    
         
            

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
