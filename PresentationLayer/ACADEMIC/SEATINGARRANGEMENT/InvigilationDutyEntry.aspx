<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="InvigilationDutyEntry.aspx.cs" Inherits="ACADEMIC_InvigilationDutyEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
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
                AUTO INVIGILATION DUTY ENTRY
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
                <legend class="legend">Invigilator Auto Duty Entry</legend>
                <table cellpadding="2" cellspacing="2" style="width: 90%;">
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Session :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlSession" Width="30%" AppendDataBoundItems="true" runat="server"
                                AutoPostBack="true" ValidationGroup="Show" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                Display="None" ValidationGroup="Show" InitialValue="0" ErrorMessage="Please Select Session">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Day No :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlDay" Width="30%" AppendDataBoundItems="true" runat="server"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Exam Date :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:Label ID="lblExamDate" Font-Bold="true" runat="server">
                            </asp:Label>
                        </td>
                        <td>
                        </td>
                        <%-- Total Students:
                        <asp:TextBox ID="txtStudent" onblur="IsNumeric(this)" runat="server" Enabled="false" Width="50px" />
                                &nbsp;&nbsp;&nbsp;<b>Seating Capacity Per Block</b>&nbsp;<asp:TextBox ID="txtSeat" onblur="IsNumeric(this)" runat="server" Width="50px" />
                            <asp:RequiredFieldValidator ID="rfvSeat" runat="server" ControlToValidate="txtSeat"
                                Display="None" ErrorMessage="Please Enter Seating Capacity Per Block!!" SetFocusOnError="True"
                                ValidationGroup="Show"></asp:RequiredFieldValidator>
                           <br />     <br />
                          Invigilator &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;Total Reliviers&nbsp; &nbsp;&nbsp;<B>Total</B>
                          <br />
                          <asp:TextBox ID="TextBox1" onblur="IsNumeric(this)" runat="server" Enabled="false" Width="50px" />
                           &nbsp;&nbsp; &nbsp;&nbsp;+&nbsp;&nbsp;  &nbsp;&nbsp;
                                 <asp:TextBox ID="TextBox2" onblur="IsNumeric(this)" runat="server" Enabled="true" Width="50px" />
                              &nbsp;&nbsp; &nbsp;&nbsp;= &nbsp;&nbsp; &nbsp;&nbsp;
                               <asp:TextBox ID="TextBox3" onblur="Total(this)" runat="server" Enabled="false" Width="50px" /></td>
                        td rowspan="4" >
                         <fieldset class="fieldset">
                    <legend class="legend">Exam</legend>
                    <asp:Panel ID="pnlExamInfo" Height="50px" runat="server">
                    <asp:ListView ID="lvExam" runat="server">
                    <LayoutTemplate>
                      <table cellpadding="0" cellspacing="0" class="datatable" width="50%">
                                   <thead>
                                      <tr class="header">
                                        <th style="width: 100%; text-align: center" align="center">
                                                 Rows * Columns
                                           </th>
                                        </tr>
                                     <tr id="itemPlaceholder" runat="server" />
                                 </thead>
                         </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                   <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                            <td style="width: 80%; text-align: center">
                                                                <asp:Label ID="lblRows" runat="server" Text='<%# Eval("Rows")%>' />
                                                                *
                                    <asp:Label ID="lblColumns" runat="server" Text='<%# Eval("col")%>' />
                       </td>
                           </tr>
                    
                    </ItemTemplate>
                    </asp:ListView>
                   
                    </asp:Panel>
                    </fieldset>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Slot :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlSlot" AppendDataBoundItems="true" Width="30%" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="ddlSlot_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" colspan="2">
                            <table width="75%">
                                <tr>
                                    <td class="form_left_label" style="text-align:left" colspan="3">
                                        Total Students:
                                    
                                        <asp:TextBox ID="txtStudent" onblur="IsNumeric(this)" runat="server" Enabled="false"
                                            Width="50px" />
                                    </td>
                                    <td class="form_left_text" colspan="2">
                                        Seating Capacity Per Block:
                                        <asp:TextBox ID="txtSeat" onblur="IsNumeric(this)" runat="server" Width="50px" />
                                        <asp:RequiredFieldValidator ID="rfvSeat" runat="server" ControlToValidate="txtSeat"
                                            Display="None" ErrorMessage="Please Enter Seating Capacity Per Block!!" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </td>
                                      <td style="width: 75%">&nbsp; </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        &nbsp
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        <b>Invigilator</b>
                                    </td>
                                    <td style="width: 15%; text-align: center">
                                        <b>+ Reliever</b>
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        <b>= Total</b>
                                    </td>
                                      <td style="width: 60%">&nbsp; </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 10%; text-align: center">
                                    </td>
                                    <td class="form_left_text" style="width: 10%; text-align: center">
                                        <asp:TextBox ID="txtInvig" runat="server" Enabled="false" onblur="IsNumeric(this)"
                                            Width="50px" />
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        &nbsp;<asp:TextBox ID="txtReliver" runat="server" Enabled="true" onblur="IsNumeric(this)"
                                            onkeyup="Total()" Width="50px" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtRelivere" runat="server" FilterType="Numbers"
                                            TargetControlID="txtReliver">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td style="width:10%; text-align: center">
                                        &nbsp;<asp:TextBox ID="txtTotal" runat="server" Enabled="false" Width="50px" />
                                    </td>
                                      <td style="width: 60%">&nbsp; </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <asp:ValidationSummary ID="vsShow" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Show" />
                                    </td>
                                    <td class="form_left_text"  colspan="4">
                                        <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate Duty"
                                            ValidationGroup="Show" Width="120px" />
                                        &nbsp;
                                        <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Day Wise Report"
                                            ValidationGroup="Show" Width="120px" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                            ValidationGroup="none" Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset class="fieldset" style="width: 99%; padding-top: 10px">
                <legend class="legend">Invigilator Wise Duty Report</legend>
                <table style="width: 90%" cellpadding="2" cellspacing="2" width="90%">
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Session :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlSession2" runat="server" Width="30%" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSession2_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession2"
                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--  <tr>
                            <td style="width: 10%">
                                Staff Type :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" Width="140px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                Distance :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDistance" runat="server" AppendDataBoundItems="true" Width="140px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDistance_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                    <tr>
                        <td class="form_left_label">
                            Invigilator :
                        </td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlInvigilator" runat="server" AppendDataBoundItems="true"
                                Width="30%" AutoPostBack="true" OnSelectedIndexChanged="ddlInvigilator_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label">
                            Day No :
                        </td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlDay2" runat="server" Width="30%" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDay2_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">
                            Exam Date :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:Label ID="lblExamDate2" Font-Bold="true" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label">
                            Slot :
                        </td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlSlot2" runat="server" Width="30%" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="form_left_label">
                            Order No :
                        </td>
                        <td class="form_left_text">
                            <asp:TextBox ID="txtOrderNo" runat="server" Width="143px" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="form_left_label">
                            Order Date :
                        </td>
                        <td class="form_left_text">
                            <asp:TextBox ID="txtDate" runat="server" Width="103px" />
                            <asp:Image ID="imgDate" runat="server" ImageUrl="~/images/calendar.png" />
                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDate"
                                TargetControlID="txtDate" />
                            <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDate" />
                            <%--  Enable the button so it can be played again --%>
                            <asp:RequiredFieldValidator ID="rfvCalender" runat="server" ControlToValidate="txtDate"
                                Display="None" ErrorMessage="Please Select Date"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                        </td>
                        <td class="form_left_text">
                            &nbsp;<asp:Button ID="btnReport2" runat="server" Text="Invigilator Wise Report" ValidationGroup="submit"
                                Width="150px" OnClick="btnReport2_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Width="150px" ValidationGroup="none"
                                OnClick="btnCancel_Click" />
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
