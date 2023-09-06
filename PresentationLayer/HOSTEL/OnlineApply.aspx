<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="OnlineApply.aspx.cs" Inherits="HOSTEL_OnlineApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                APPLICATION FORM FOR HOSTEL
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
    <div>
        <fieldset class="fieldset">
            <legend class="legend">Apply Hostel</legend>
            <table>
                <tr id="trRegno" runat="server" visible="false">
                    <td style="width: 112px">
                    </td>
                    <td>
                        Enter Enroll No. :
                    </td>
                    <td>
                        <asp:TextBox ID="txtRegno" runat="server" Enabled="false"></asp:TextBox>&nbsp;&nbsp;
                        <%--  Enable the button so it can be played again --%>
                        <a href="#" title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;">
                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1"
                                AlternateText="Search Student by IDNo, Name, Reg. No, Branch, Semester" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                        </a>
                    </td>
                    <%-- <td>
                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                        <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" />
                    </td>--%>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div id="divStudInfo" runat="server" visible="false">
                            <fieldset class="fieldset">
                                <legend class="legend2">Student Information</legend>
                                <table width="100%" cellpadding="2" cellspacing="2" border="0">
                                    <tr>
                                        <td style="width: 16%">
                                            Roll No.:
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblRegNo" CssClass="data_label" runat="server" />
                                        </td>
                                        <td width="11%">
                                            Degree:
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblDegree" CssClass="data_label" runat="server" />
                                        </td>
                                        <td rowspan="5">
                                            <asp:Image ID="imgPhoto" runat="server" Width="96 px" Height="110px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%">
                                            Student's Name:
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblStudName" CssClass="data_label" runat="server" />
                                        </td>
                                        <td>
                                            Branch:
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblBranch" CssClass="data_label" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%">
                                            Gender:
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblSex" CssClass="data_label" runat="server" />
                                        </td>
                                        <td>
                                            Year:
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblYear" CssClass="data_label" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%">
                                            Date of Admission:
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblDateOfAdm" CssClass="data_label" runat="server" />
                                        </td>
                                        <td>
                                            Current Sem :
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblSemester" CssClass="data_label" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%">
                                           Student Category :
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblCategory" CssClass="data_label" runat="server" />
                                        </td>
                                        <td>
                                            Apply Sem :
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblAppSem" CssClass="data_label" runat="server" />
                                            <%-- <asp:DropDownList ID="ddlAppSem" runat="server" AppendDataBoundItems="True" Width="100px" Enabled="false">
                                            </asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 16%">
                                            Alloted Category :
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblPaymentType" CssClass="data_label" runat="server" />
                                        </td>
                                        <td>
                                            Batch:
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblBatch" CssClass="data_label" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="trAIEEE" runat="server">
                                         <td style="width: 16%">
                                           JEE/AIEEE Rank :
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblAIEEE" CssClass="data_label" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            For Session :
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Hostel Session"
                                                Width="150px" Enabled="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                                                ControlToValidate="ddlSession" InitialValue="0" SetFocusOnError="True" ValidationGroup="Apply"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                        <td colspan="3" width="100%">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:ListView ID="lvResult" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr class="header">
                                                                            <th style="text-align: center">
                                                                                Last Semester
                                                                            </th>
                                                                            <th style="text-align: center">
                                                                                SGPA
                                                                            </th>
                                                                            <th style="text-align: center">
                                                                                CGPA
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td style="text-align: center">
                                                                        <%# Eval("SEMESTER") %>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("SGPA") %>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Eval("CGPA") %>
                                                                    </td>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </fieldset>
                            <fieldset class="fieldset">
                                <legend class="legend2">Permanent Address</legend>
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td width="15%" valign="top">
                                            <span class="validstar">&nbsp</span>Permanent
                                            <br />
                                            <span class="validstar">&nbsp</span>Address :
                                        </td>
                                        <td colspan="6" style="width: 46%">
                                            <asp:TextBox ID="txtPermAddress" runat="server" TextMode="MultiLine" Width="46%"
                                                Rows="3" MaxLength="200" Height="69px" ToolTip="Please Enter Permenant Address"
                                                TabIndex="31" />
                                            <asp:RequiredFieldValidator ID="rfvPermAddress" runat="server" ControlToValidate="txtPermAddress"
                                                Display="None" ErrorMessage="Please Enter Permenant Address" SetFocusOnError="True"
                                                ValidationGroup="Apply"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtPdistrict" runat="server" TabIndex="33" Visible="False"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />
                                        </td>
                                        <td width="15%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                    <td width="10%">
                                            <span class="validstar">&nbsp</span>Mobile No. :
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="15"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMobileNo"
                                                Display="None" ErrorMessage="Please Enter Mobile No." SetFocusOnError="True"
                                                ValidationGroup="Apply" ></asp:RequiredFieldValidator>
                                        </td>
                                        <td width="10%">
                                            <span class="validstar">&nbsp</span>City :
                                        </td>
                                        <td width="20%">
                                            <asp:DropDownList ID="ddlPermCity" runat="server" Width="90%" TabIndex="32" AppendDataBoundItems="True"
                                                ToolTip="Please Select City">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlPermCity"
                                                Display="None" ErrorMessage="Please Select Permanent City" SetFocusOnError="True"
                                                ValidationGroup="Apply" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                        <td width="5%">
                                        </td>
                                        <td width="10%">
                                            <span class="validstar">&nbsp</span>State :
                                        </td>
                                        <td width="20%">
                                            <asp:DropDownList ID="ddlPermState" runat="server" Width="90%" AppendDataBoundItems="True"
                                                ToolTip="Please Select State" TabIndex="35" />
                                            <asp:RequiredFieldValidator ID="rfvPermState" runat="server" ControlToValidate="ddlPermState"
                                                Display="None" ErrorMessage="Please Select Permanent State" SetFocusOnError="True"
                                                ValidationGroup="Apply" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                        <td width="10%">
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset class="fieldset">
                                <legend class="legend2">Bank Details</legend>
                                <table width="100%" cellpadding="2" cellspacing="2" border="0">
                                    <tr>
                                        <td style="width: 15%">
                                            Bank :
                                        </td>
                                        <td style="width: 26%">
                                            <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="True" Width="200px"
                                                ToolTip="Please Select Bank">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="14%">
                                            Account No :
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="txtAccNo" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td>
                                            Bank Branch :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBBranch" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Apply" OnClick="btnSubmit_Click"
                                            ValidationGroup="Apply" />
                                        <asp:Button ID="btnAppform" runat="server" Text="Application Form" OnClick="btnAppform_Click"
                                            Enabled="false" />
                                        <asp:Button ID="btnMess" runat="server" Text="Mess Slip" Visible="false" OnClick="btnMess_Click" ValidationGroup="Mess"/>
                                        <asp:Button ID="btnHostel" runat="server" Text="Hostel Slip" Visible="false" OnClick="btnHostel_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Apply" />
                                          <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Mess" />    
                                    </td>
                                </tr>
                            </table>
                            </div>
                            <div id="divMessSlip" runat="server" visible="false">
                            <fieldset class="fieldset"  runat="server">
                                <legend>Mess Slip</legend>
                                <table cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td width="30%">
                                        </td>
                                        <td width="10%">
                                            Mess Slip For
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdlMess" runat="server">
                                                <asp:ListItem Value="1">Half Payment ( 11500 )</asp:ListItem>
                                                <asp:ListItem Value="0">Full Payment ( 20500 )</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvMess" runat="server" ErrorMessage="Please Select Mess Slip For" ControlToValidate="rdlMess" Display="None" SetFocusOnError="True" ValidationGroup="Mess"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                      </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
    </div>
    <div id="divdemo2" style="display: none; height: 550px">
        <asp:UpdatePanel ID="updEdit" runat="server">
            <ContentTemplate>
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td>
                            Search Criteria:
                        </td>
                        <td>
                            <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                            <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                            <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                            <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                            <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Enrollmentno" GroupName="edit"
                                Checked="True" />
                            <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Search String :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" />
                            <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                <ProgressTemplate>
                                    <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                    Loading.. Please Wait!
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="100%">
                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="titlebar">
                                            Login Details</div>
                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <thead>
                                                <tr class="header">
                                                    <th style="width: 20%">
                                                        Name
                                                    </th>
                                                    <th style="width: 15">
                                                        IdNo
                                                    </th>
                                                    <th style="width: 20%">
                                                        Roll No.
                                                    </th>
                                                    <th style="width: 30%">
                                                        Branch
                                                    </th>
                                                    <th style="width: 15%">
                                                        Semester
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                    <div class="listview-container">
                                        <div id="demo-grid" class="vista-grid">
                                            <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td style="width: 20%">
                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                OnClick="lnkId_Click"></asp:LinkButton>
                                        </td>
                                        <td style="width: 15%">
                                            <%# Eval("idno")%>
                                        </td>
                                        <td style="width: 20%">
                                            <%# Eval("EnrollmentNo")%>
                                        </td>
                                        <td style="width: 30%">
                                            <%# Eval("longname")%>
                                        </td>
                                        <td style="width: 15%">
                                            <%# Eval("semesterno")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="popup" runat="server" style="display: none; height: 550px">
        <table cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
    function submitPopup(btnsearch)
    {    
            var rbText;
            var searchtxt;
            if(document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name";
            else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true) 
                rbText = "idno";
            else if (document.getElementById ('<%=rbBranch.ClientID %>').checked == true) 
                rbText = "branch";
            else if (document.getElementById('<%=rbSemester.ClientID %>').checked == true) 
                rbText = "sem"; 
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked==true)
                rbText = "enrollmentno";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";    

            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
           
           __doPostBack(btnsearch,rbText+','+searchtxt);

        return true;
    }
    
    function ClearSearchBox(btncancelmodal)
    {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
         __doPostBack(btncancelmodal,'');
         return true;
    }
     
    </script>

</asp:Content>
