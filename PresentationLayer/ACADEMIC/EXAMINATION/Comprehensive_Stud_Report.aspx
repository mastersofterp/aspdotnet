<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Comprehensive_Stud_Report.aspx.cs" Inherits="ACADEMIC_Comprehensive_Stud_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

   <%-- <script type="text/javascript" charset="utf-8">
        $(document).ready(function() {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>

    <div style="width: 100%">
        <table width="100%" style="text-align: left">
            <tr>
                <td>
                    <table class="vista_page_title_bar" width="100%">
                        <tr>
                            <td style="height: 30px">
                                COMPREHENSIVE STUDENT INFORMATION
                               <%-- <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC;
                        background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right;">
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
                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlSearch" runat="server">
        <fieldset class="fieldset">
            <legend class="legend">Search Student</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        Roll. No :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="submit"></asp:TextBox>
                        &nbsp;
                        <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtEnrollmentSearch"
                            WatermarkText="Enter Roll No." WatermarkCssClass="watermarked" />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="15%" OnClick="btnSearch_Click"
                            ValidationGroup="submit" />
                        &nbsp;<asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                            Display="None" ErrorMessage="Please Enter Enrollment no." ValidationGroup="submit"></asp:RequiredFieldValidator><asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" Width="20%" ShowSummary="False" ValidationGroup="submit" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
        <table width="100%" style="text-align: left">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Student Information
                            </td>
                            <td align="right">
                                <img id="img1" style="cursor: pointer;" alt="" src="../IMAGES/collapse_blue.jpg"
                                    onclick="javascript:toggleExpansion(this,'divStudentInfo')" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divStudentInfo" style="display: block;">
                        <table cellpadding="1" cellspacing="1" width="100%" border="0">
                            <tr>
                                <td width="16%">
                                    Roll No. :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                    Branch :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Enrollment No.:</td>
                                <td width="35%">
                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></td>
                                <td width="15%">
                                    Semester :</td>
                                <td width="35%">
                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Student Name :
                                </td>
                                <td colspan="3" style="width: 70%">
                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" valign="top">
                                    Father's Name :
                                </td>
                                <td width="35%" valign="top">
                                    <asp:Label ID="lblMName" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%" valign="top">
                                    Photo :
                                </td>
                                <td width="35%" rowspan="5">
                                    <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px"/>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Date of Birth :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                </td>
                                <td width="35%">
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Caste :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblCaste" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                    
                                </td>
                                <td width="35%">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Category :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblCategory" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                    
                                </td>
                                <td width="35%">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Nationality :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("") %>' Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                    
                                </td>
                                <td width="35%">
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="15%">
                                    Religion :
                                </td>
                                <td width="30%">
                                    <asp:Label ID="lblReligion" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                   
                                </td>
                                <td width="35%">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Local Address :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblLAdd" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                    City :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblCity" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Landline No :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblLLNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">
                                    Mobile No :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Permanent Address :&nbsp;
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblPAdd" runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;
                                </td>
                                <td width="15%">
                                    City :&nbsp;
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblPCity" runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                 <td width="15%">
                                    Physical Handicapped :&nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblHandicap" runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    </td>
                                <td width="35%">
                                    
                                    </td>
                                <td width="15%">
                                    &nbsp;</td>
                                <td width="35%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Registration Status
                            </td>
                            <td align="right">
                               <%-- <img id="img2" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                    onclick="javascript:toggleExpansion(this,'divRegistrationStatus')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divRegistrationStatus" style="display: block;">
                    <asp:ListView ID="lvRegStatus" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                       Current Semester Registration Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 15%; text-align: center;">
                                                    Session
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                    Semester
                                                </th>
                                                <th style="width: 15%; text-align: center;">
                                                    CCode
                                                </th>
                                                <th style="width: 30%; text-align: center;">
                                                    Course Name
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                   Subject Type
                                                </th>
                                                
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                           
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("SESSION") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("SEMESTER") %>
                                    </td>
                                    <td style="width: 15%; text-align: left;">
                                        <%# Eval("CCODE") %>
                                    </td>
                                    <td style="width: 30%; text-align: left;">
                                       <%# Eval("COURSENAME") %>
                                    </td>
                                    <td style="width: 10%; text-align: left;">
                                         <%# Eval("SUBJECTTYPE") %>
                                    </td>
                                   
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                      <%--  <asp:Repeater ID="lvRegStatus" runat="server">
                            <HeaderTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar" style="width: 100%;">
                                        Current Semester Registration Details
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th style="width: 15%; text-align: center;">
                                                Session
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Semester
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                CCode
                                            </th>
                                            <th style="width: 50%; text-align: center;">
                                                Course Name
                                            </th>
                                            <th style="width: 15%; text-align: center;">
                                                Subject Type
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("SESSION") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("SEMESTER") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("CCODE") %>
                                    </td>
                                    <td style="width: 50%;">
                                        <%# Eval("COURSENAME") %>
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("SUBJECTTYPE") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                               Student Attendance
                            </td>
                            <td align="right">
                               <%-- <img id="img9" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                    onclick="javascript:toggleExpansion(this,'divAttendance')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                   </td>
            </tr>
            <tr>
                <td>
                 <div id="divAttendance" style="display:block">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div id="div3" style="display: block;">
                    <asp:ListView ID="lvAttendance" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                       Attendance Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 40%; text-align: center;">
                                                  Course Name
                                                </th>
                                                <th style="width: 20%; text-align: center;">
                                                  Faculty Name 
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                  Total Classes
                                                </th>
                                                 <th style="width: 5%; text-align: center;">
                                                   Present
                                                </th>
                                                <th style="width: 5%; text-align: center;">
                                                   Absent
                                                </th>
                                                 <th style="width: 10%; text-align: center;">
                                                   Percentage
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                           
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    
                                    <td style="width: 40%; text-align: left;">
                                       <%# Eval("COURSENAME") %>
                                    </td>
                                    <td style="width: 20%; text-align: left;">
                                       <%# Eval("UA_NAME") %> 
                                    </td>
                                     <td style="width: 10%; text-align: center;">
                                       <%# Eval("TOTAL_CLASSES") %>
                                    </td>
                                     <td style="width: 5%; text-align: center;">
                                       <%# Eval("PRESENT") %>   
                                    </td>
                                    <td style="width: 5%; text-align: center;">
                                       <%# Eval("ABSENT") %>
                                    </td>
                                     <td style="width: 10%; text-align: center;">
                                        <%# Eval("ATT_PERCENTAGE") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                     
                    </div> 
                                </td>
                            </tr>
                        </table>
                    </div>  
                   </td>
            </tr>
            <tr>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Examination Marks
                            </td>
                            <td align="right">
                              <%--  <img id="img6" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                    onclick="javascript:toggleExpansion(this,'divTestMark')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
            <div id="divTestMark" style="display:block">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div id="div1" style="display: block;">
                    <asp:ListView ID="lvTestMark" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                       Examination Marks Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                
                                               
                                                <th style="width: 15%; text-align: center;">
                                                    CCode
                                                </th>
                                                <th style="width: 30%; text-align: center;">
                                                    Course Name
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                  MINOR-I
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                  MINOR-I
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                  TOTAL(MINOR-I+MINOR-II)
                                                </th>
                                                <%--<th style="width: 10%; text-align: center;">
                                                   TA-TH
                                                </th>
                                                 <th style="width: 10%; text-align: center;">
                                                   TA-PR
                                                </th>--%>
                                                
                                               <%-- <th style="width: 10%; text-align: center;">
                                                   ENDSEM-TH
                                                </th>
                                                 <th style="width: 10%; text-align: center;">
                                                   ENDSEM-PR
                                                </th>--%>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                           
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                   
                                    
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("CCODE") %>
                                    </td>
                                    <td style="width: 30%; text-align: left;">
                                       <%# Eval("COURSENAME") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                         <%--<%# (Eval("T1MARK").ToString()) == string.Empty ? "-" : Eval("T1MARK")%>--%>
                                         <%# "-"%>
                                    </td>
                                     <td style="width: 10%; text-align: center;">
                                        <%-- <%# (Eval("T2MARK").ToString()) == string.Empty ? "-" : Eval("T2MARK")%>--%>
                                         <%# "-"%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%-- <%# (Eval("CTTH").ToString()) == string.Empty ? "-" : Eval("CTTH")%>--%>
                                         <%# "-"%>
                                    </td>
                                     <%--<td style="width: 10%; text-align: center;">
                                          <%# (Eval("TATH").ToString()) == string.Empty ? "-" : Eval("TATH")%>
                                    </td>
                                     <td style="width: 10%; text-align: center;">
                                          <%# (Eval("TAPR").ToString()) == string.Empty ? "-" : Eval("TAPR")%>
                                    </td>--%>
                                    
                                    
                                    <%--<td style="width: 10%; text-align: center;">
                                          <%# (Eval("ENDSEMTH").ToString()) == string.Empty ? "-" : Eval("ENDSEMTH")%>
                                    </td>
                                     <td style="width: 10%; text-align: center;">
                                          <%# (Eval("ENDSEMPR").ToString()) == string.Empty ? "-" : Eval("ENDSEMPR")%>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                     
                    </div> 
                                </td>
                            </tr>
                        </table>
                    </div>        
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Current Result Details
                            </td>
                            <td align="right">
                                <%--<img id="img3" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                    onclick="javascript:toggleExpansion(this,'divResult')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                   <div id="divResult" style="display: block;">
                        <asp:ListView ID="lvResult" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Result
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 15%; text-align: center;">
                                                    Session
                                                </th>
                                                <th style="width: 30%; text-align: center;">
                                                    Scheme
                                                </th>
                                                <th style="width: 15%; text-align: center;">
                                                    No. of Sub
                                                </th>
                                               <%-- <th style="width: 10%; text-align: center;">
                                                    Attempt
                                                </th>--%>
                                                <th style="width: 10%; text-align: center;">
                                                    Semester
                                                </th>
                                                <%--<th style="width: 10%; text-align: center;">
                                                    Outof
                                                </th>--%>
                                                <th style="width: 10%; text-align: center;">
                                                    Result
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <div align="center" class="info">
                                    Result not published or Converted in to new MIS
                                </div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                        <asp:LinkButton ID="lbReport" runat="server" OnClick="lbReport_Click"><%# Eval("SESSION")%></asp:LinkButton> 
                                        
                                    </td>
                                    <td style="width: 30%; text-align: center;">
                                        <%# Eval("SCHEME_NAME")%>
                                        <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                        <asp:HiddenField ID="hdfScheme" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                        <asp:HiddenField ID="hdfIDNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                        <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("TOTAL_SUBJ_REGD")%>
                                    </td>
                                   
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("SEMESTER")%>
                                    </td>
                                    <%--<td style="width: 10%; text-align: center;">
                                        <%# Eval("OUTOFMARKS")%>
                                    </td>--%>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("PASSFAIL")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                      <%--  <asp:Repeater ID="lvResult" runat="server">
                            <HeaderTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Result
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th style="width: 15%; text-align: center;">
                                                Session
                                            </th>
                                            <th style="width: 30%; text-align: center;">
                                                Scheme
                                            </th>
                                            <th style="width: 15%; text-align: center;">
                                                No. of Sub
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Attempt
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Total Marks
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Outof
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Result
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("SESSION") %>
                                    </td>
                                    <td style="width: 30%; text-align: center;">
                                        <%# Eval("SCHEME_NAME")%>
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("TOTAL_SUBJ_REGD")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("ATTEMPT")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("TOTAL_MARKS")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("OUTOFMARKS")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("PASSFAIL")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>--%>
                    </div> 
                </td>
            </tr>
         <%--   <tr>
                <td>
                </td>
            </tr>--%>
            <tr style="display:none">
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Cetificates Issued
                            </td>
                            <td align="right">
                                <%--<img id="img4" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                    onclick="javascript:toggleExpansion(this,'divCertificate')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divCertificate" style="display: none;">
                        <asp:ListView ID="lvCertificate" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Certificates Issued Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 25%">
                                                    Certificate Name
                                                </th>
                                                <th style="width: 30%">
                                                    Certificate No
                                                </th>
                                                <th style="width: 20%">
                                                    Issued Date
                                                </th>
                                                <th style="width: 25%">
                                                    Issued By
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <div align="center" class="info">
                                    No Certificates Issued
                                </div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 25%">
                                        <%# Eval("CERTIFICATENAME") %>
                                    </td>
                                    <td style="width: 30%">
                                        <%# Eval("CERTNO") %>
                                    </td>
                                    <td style="width: 20%">
                                        <%# Eval("ISSUE_DATE", "{0:dd-MMM-yyyy}")%>
                                    </td>
                                    <td style="width: 25%">
                                        <%# Eval("ISSUEDBY") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                      <%--  <asp:Repeater ID="lvCertificate" runat="server">
                            <HeaderTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Certificates Issued Details
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th style="width: 25%">
                                                Certificate Name
                                            </th>
                                            <th style="width: 30%">
                                                Certificate No
                                            </th>
                                            <th style="width: 20%">
                                                Issued Date
                                            </th>
                                            <th style="width: 25%">
                                                Issued By
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 25%">
                                        <%# Eval("CERTIFICATENAME") %>
                                    </td>
                                    <td style="width: 30%">
                                        <%# Eval("CERTNO") %>
                                    </td>
                                    <td style="width: 20%">
                                        <%# Eval("ISSUE_DATE", "{0:dd-MMM-yyyy}")%>
                                    </td>
                                    <td style="width: 25%">
                                        <%# Eval("ISSUEDBY") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Semesterwise Fees Paid
                            </td>
                            <td align="right">
                                <%--<img id="img5" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                     onclick="javascript:toggleExpansion(this,'divFeePaid')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
           <div id="divFeePaid" style="display: block;">
                     <asp:ListView ID="lvFees" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                         Fees Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 15%; text-align: center;">
                                                    Session
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                    Semester
                                                </th>
                                                <th style="width: 15%; text-align: center;">
                                                   Receipt Type
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                    Rec. No
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                    Rec. Date
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                  Amount paid
                                                </th>
                                                <th style="width: 30%; text-align: center;">
                                                     Remarks
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("SESSION") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                       <%# Eval("SEMESTER") %>
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                         <%# Eval("RECIEPT")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                         <%# Eval("REC_NO") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("REC_DT","{0:dd-MM-yyy}") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                       <%# Eval("TOTAL_AMT") %>
                                    </td>
                                    <td style="width: 30%; text-align: center;">
                                         <%# Eval("REMARK") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                       <%-- <asp:Repeater ID="lvFees" runat="server">
                            <HeaderTemplate>
                                <div class="vista-grid" style="width: 100%;">
                                    <div class="titlebar">
                                        Fees Details
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th style="width: 15%; text-align: center;">
                                                Session
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Semester
                                            </th>
                                            <th style="width: 15%; text-align: center;">
                                                Receipt Type
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Rec. No
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                Rec. Date
                                            </th>
                                            <th style="width: 15%; text-align: center;">
                                                Amount paid
                                            </th>
                                            <th style="width: 25%; text-align: center;">
                                                Remarks
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("SESSION") %>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("SEMESTER") %>
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("RECIEPT")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("REC_NO") %>
                                    </td>
                                    <td style="text-align: center;">
                                        <%# Eval("REC_DT","{0:dd-MM-yyy}") %>
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("TOTAL_AMT") %>
                                    </td>
                                    <td style="width: 25%;">
                                        <%# Eval("REMARK") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>--%>
                    </div>        
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr style="display:none">
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Student Remark
                                                      </td>
                            <td align="right">
                                <%--<img id="img7" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                    onclick="javascript:toggleExpansion(this,'divRemark')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divRemark" style="display: none;">
                         <asp:ListView ID="lvRemark" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Remarks Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 55%">
                                                    Remarks
                                                </th>
                                                <th>
                                                    Given By Faculty
                                                </th>
                                                <th>
                                                    Date
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <div align="center" class="info">
                                    No Remarks Given
                                </div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td id="remak" runat="server">
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK")%>'></asp:Label>
                                    </td>
                                    <td id="UANO" runat="server">
                                        <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NO") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbluaName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                    </td>
                                    <td id="remarkDate" runat="server">
                                        <asp:Label ID="lblRemarkDate" runat="server" Text='<%# Eval("REMARK_DATE","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                      <%--  <asp:Repeater ID="lvRemark" runat="server">
                            <HeaderTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Remarks Details
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th style="width: 55%">
                                                Remarks
                                            </th>
                                            <th>
                                                Given By Faculty
                                            </th>
                                            <th>
                                                Date
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td id="remak" runat="server">
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK")%>'></asp:Label>
                                    </td>
                                    <td id="UANO" runat="server">
                                        <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NO") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbluaName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                    </td>
                                    <td id="remarkDate" runat="server">
                                        <asp:Label ID="lblRemarkDate" runat="server" Text='<%# Eval("REMARK_DATE","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr style="display:none">
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">
                                Student Refund
                            </td>
                            <td align="right">
                                <%--<img id="img8" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                    onclick="javascript:toggleExpansion(this,'divRefund')" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divRefund" style="display: none;">
                    
                        <asp:ListView ID="lvRefund" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                          Fees Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 15%; text-align: center;">
                                                    Branch
                                                </th>
                                                <th style="width: 30%; text-align: center;">
                                                    Sem.
                                                </th>
                                                <th style="width: 15%; text-align: center;">
                                                   Payment Cat.
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                    Rec. Type
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                   Rec. No.
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                   Rec. Amt.
                                                </th>
                                                <th style="width: 10%; text-align: center;">
                                                      Refunded Amt.
                                                </th>
                                                 <th style="width: 10%; text-align: center;">
                                                      Refundable Amt.
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                       <%# Eval("BRANCHSNAME")%>
                                    </td>
                                    <td style="width: 30%; text-align: center;">
                                       <%# Eval("SEMESTER")%>
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                          <%# Eval("PTYPENAME")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                         <%# Eval("RECIEPT_TITLE")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                         <%# Eval("REC_NO")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                       <%# Eval("DCR_AMT")%>
                                    </td>
                                    <td style="width: 10%; text-align: center;">
                                        <%# Eval("REFUND_AMT")%>
                                    </td>
                                     <td style="width: 10%; text-align: center;">
                                         <%# Eval("REFUNDABLE_AMT")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                       <%-- <asp:Repeater ID="lvRefund" runat="server">
                            <HeaderTemplate>
                                <div class="vista-grid" style="width: 100%;">
                                    <div class="titlebar">
                                        Fees Details
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th>
                                                Branch
                                            </th>
                                            <th>
                                                Sem.
                                            </th>
                                            <th>
                                                Payment Cat.
                                            </th>
                                            <th>
                                                Rec. Type
                                            </th>
                                            <th>
                                                Rec. No.
                                            </th>
                                            <th>
                                                Rec. Amt.
                                            </th>
                                            <th>
                                                Refunded Amt.
                                            </th>
                                            <th>
                                                Refundable Amt.
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td>
                                        <%# Eval("BRANCHSNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("SEMESTER")%>
                                    </td>
                                    <td>
                                        <%# Eval("PTYPENAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("RECIEPT_TITLE")%>
                                    </td>
                                    <td>
                                        <%# Eval("REC_NO")%>
                                    </td>
                                    <td>
                                        <%# Eval("DCR_AMT")%>
                                    </td>
                                    <td>
                                        <%# Eval("REFUND_AMT")%>
                                    </td>
                                    <td>
                                        <%# Eval("REFUNDABLE_AMT")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>--%>
                    </div>
                </td>
            </tr>
        </table>
    </div>

   <%-- <script type="text/javascript" language="javascript">

    /* To collapse and expand page sections */
    function toggleExpansion(imageCtl, divId)
    {
        if(document.getElementById(divId).style.display == "block")
        {
            document.getElementById(divId).style.display = "none";
            imageCtl.src = "../IMAGES/expand_blue.jpg";
        }
        else if(document.getElementById(divId).style.display == "none")
        {
            document.getElementById(divId).style.display = "block";
            imageCtl.src = "../IMAGES/collapse_blue.jpg";
        }
    }  
    </script>--%>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
