<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" 
CodeFile="ProcessCPISemesterwise.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ProcessCPISemesterwise" 
Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                 SEMESTERWISE CPI PROCESS 
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
      <div style="padding-left: 10px; width: 49%">

                <fieldset class="fieldset1">
                    <legend class="legend">Semesterwise Result Process</legend>
                   
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                       
                      <tr>
                      <td class="form_left_label" style="width: 114px">
                     &nbsp; &nbsp;&nbsp;<span style="color:Red">*</span> Roll No :&nbsp;
                      </td>
                      <td class="form_left_label">
                          <asp:TextBox ID="txtRollno" runat="server"  ></asp:TextBox>
                 <%--         <ajaxToolKit:TextBoxWatermarkExtender ID="txtw" runat="server"  TargetControlID="txtRollno"
                       WatermarkText="Type Roll No. Here"></ajaxToolKit:TextBoxWatermarkExtender>--%>
                      <%--<ajaxToolKit:FilteredTextBoxExtender id="ftbreg" runat="server" TargetControlID="txtRollno" FilterType="Numbers"
                       FilterMode="ValidChars" ></ajaxToolKit:FilteredTextBoxExtender>--%><%-- ValidChars="1234567890"--%>
                    
                      </td>
                      </tr>
                   <tr>
                   <td style="width: 114px">
                   <span></span>
                   </td>
                   <td>
                       &nbsp;</td>
                   </tr>
                      <tr>
                      <td style="width: 114px"></td>
                      <td>&nbsp;&nbsp;<asp:Button ID="btnOk" runat="server" Text="Search" BorderWidth="1" 
                              onclick="btnOk_Click" />
                          </td>
                      </tr>
                      <tr>
                      <td style="width: 114px">&nbsp;</td>
                      <td>&nbsp;</td>
                      </tr>
                        <tr>
                            <td class="form_left_label" style="width: 114px">
&nbsp; &nbsp;&nbsp;<span style="color:Red">*</span> Semester :
                            </td>
                            <td class="form_left_label">
                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" 
                                    Width="100px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    
                    </table>

                    <table width="100%">
                        <tr>
                            <td class="form_left_text" align="center">
                                <asp:Button ID="btnShow" runat="server" Text="Show"  OnClientClick="return Validate();" onclick="btnShow_Click" />
                              &nbsp;  <asp:Button ID="btnProcess" runat="server" Text="Process"
                                ValidationGroup="report" Width="80px"
                                    onclick="btnProcess_Click"/>
                            &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                Width="80px" CausesValidation="False" />
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                            </td>
                        </tr>
                    </table>
                </fieldset>

    </div>
          <div style="padding-left: 10px; width: 55%">
                <fieldset class="fieldset1">
                    <legend class="legend">Student details</legend>
                  <table width="100%">  <tr>
                            <td colspan="2" width="100%" align="center">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <ItemTemplate>
                                        <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                             <td>
                                                <%# Eval("SEMESTERNO")%>
                                            </td>
                                             <td>
                                                <%# Eval("SGPA")%> 
                                            </td>
                                             <td align="center" >
                                                <%# Eval("CGPA")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <div class="titlebar">
                                                Student List
                                            </div>
                                            <table cellpadding="0" cellspacing="0" class="datatable">
                                                <tr class="header">
                                                    <th class=" form_left_label" width="15%">
                                                        Roll. No.
                                                    </th>
                                                    <th class=" form_left_label" width="35%">
                                                        Student Name
                                                    </th>
                                                   <th class=" form_left_label" width="30%">
                                                        Semester
                                                    </th>
                                                    <th class=" form_left_label" width="30%">
                                                        SPI
                                                    </th>
                                                     <th class=" form_left_label" align="center" width="30%">
                                                        CPI
                                                    </th>
                                                    <th></th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </td>
                        </tr></table>
                       
                        </fieldset>
                        </div>
                        <script  type="text/javascript" language="javascript">
                            function Validate() {
                                if (document.getElementById('<%= txtRollno.ClientID %>').value == '') {
                                    alert('Please Insert Roll No!');
                                }
                                else if (document.getElementById('<%=ddlSem.ClientID%>').selectedIndex == 0) {
                                    alert('Please Select Semester!');
                                    return false;
                                }
                            }
                               


                        </script>
</asp:Content>

