<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ResultProcessingAuto.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ResultProcessingAuto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updResultProcess"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; background-color: Aqua; padding-left: 5px">
                    <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                RESULT PROCESSING 
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
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
                    function totSubjects(chk) {
                        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

                        if (chk.checked == true)
                            txtTot.value = Number(txtTot.value) + 1;
                        else
                            txtTot.value = Number(txtTot.value) - 1;
                    }

                    function totAllSubjects(headchk) {
                        var chkid = headchk.id;
                        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
                        var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
                        var frm = document.forms[0];
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            var e = frm.elements[i];
                            if (e.type == 'checkbox') {
                                if (headchk.checked == true) {
                                    e.checked = true;
                                    txtTot.value = hdfTot.value;
                                }
                                else {
                                    e.checked = false;
                                    txtTot.value = 0;
                                }
                            }
                        }
                    }

                    function validateAssign() {
                        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

                        if (txtTot == 0) {
                            alert('Please Select atleast one student from student list');
                            return false;
                        }
                        else
                            return true;
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
    <table cellpadding="2" cellspacing="2" style="width: 100%">
        <tr>
            <td style="padding-top: 5px; width: 100%">
                <asp:UpdatePanel ID="updResultProcess" runat="server">
                    <ContentTemplate>
                        <fieldset class="fieldset">
                            <legend class="legend">Result Processing</legend>
                            <table style="width: 100%;" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        Session :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Width="60%"
                                            Font-Bold="true" TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Degree :
                                    </td>
                                    <td style="width: 62%">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" Width="60%"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Branch :
                                    </td>
                                    <td style="width: 62%">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" Width="60%"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Scheme :
                                    </td>
                                    <td style="width: 62%;">
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" Width="60%"
                                            AutoPostBack="True" TabIndex="4" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Semester :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="200px" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td style="width: 10%">
                                        Student Category :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                            TabIndex="6" Width="200px" AutoPostBack="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular Student(s)</asp:ListItem>
                                            <asp:ListItem Value="1">Ex-Student(s)</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                                            Display="None" ErrorMessage="Please Select Category" InitialValue="-1" 
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="width: 10%">
                                        Student :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="True" TabIndex="7"
                                            Width="60%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        Result Date :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResultDate" runat="server" TabIndex="8" ValidationGroup="submit"
                                            Width="100px" />
                                        <asp:Image ID="imgResultDate" runat="server" ImageUrl="~/images/calendar.png" />
                                        <ajaxToolKit:CalendarExtender ID="ceResultDate" runat="server" Format="MM/dd/yyyy"
                                            TargetControlID="txtResultDate" PopupButtonID="imgResultDate" />
                                        <ajaxToolKit:MaskedEditExtender ID="meResultDate" runat="server" TargetControlID="txtResultDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            MaskType="Date" PromptCharacter="_" />
                                        <%--<ajaxToolKit:MaskedEditValidator ID="mvResultDate" runat="server" EmptyValueMessage="Please Enter Result Date"
                                                ControlExtender="meResultDate" ControlToValidate="txtResultDate" IsValidEmpty="false"
                                                InvalidValueMessage="Result Date is invalid" Display="None" ErrorMessage="Please Enter Result Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />--%>
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="9" 
                                            Text="Show Students" ValidationGroup="Submit" />
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td style="width: 15%">
                                        Result Type :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlResultType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlResultType_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <%--<asp:ListItem Value="2">Revaluation</asp:ListItem>
                                                <asp:ListItem Value="3">ReTotal</asp:ListItem>--%>
                                            <asp:ListItem Value="4">With Held</asp:ListItem>
                                            <%--<asp:ListItem Value="0">Any Other</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvResultType" runat="server" ControlToValidate="ddlResultType"
                                            Display="None" ErrorMessage="Please Select Result Type" InitialValue="-1" 
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;
                                        </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" colspan="2">
                                        Total Selected Students :
                                        <asp:TextBox ID="txtTotStud" runat="server" CssClass="watermarked" Enabled="false"
                                            Style="text-align: center" Width="30px" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                            DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                        <asp:Timer ID="timUpdate" runat="server" Enabled="False" Interval="1000" OnTick="timUpdate_Tick">
                                        </asp:Timer>
                                    </td>
                                    <td style="padding-top: 5px; padding-bottom: 15px">
                                        <asp:Button ID="btnProcess" runat="server" Text="Process" TabIndex="8" ValidationGroup="Submit"
                                            Width="80px" OnClientClick="return validateAssign();" OnClick="btnProcess_Click"
                                            Enabled="False" />&nbsp;&nbsp;
                                        <asp:Button ID="btnLock" runat="server" Text="Lock" TabIndex="9" ValidationGroup="Submit"
                                            Width="80px" OnClick="btnLock_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnUnlock" runat="server" Text="UnLock" TabIndex="9" ValidationGroup="Submit"
                                            Width="80px" OnClick="btnUnlock_Click" />&nbsp;&nbsp;
                                        
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                                            Text="Cancel" Width="80px" />
                                        
                                        <br />
                                        <br />
                                        <asp:Label ID="lblmsg" runat="server" Style="text-decoration: blink; font-weight: bold;
                                            color: Red;" Visible="false" Text="T-Result Represents the Withheld Results." />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 20px; text-align: center">
                                        <div style="width: 100%; text-align: center">
                                            <asp:ListView ID="lvGrade_Not_Alloted_Courses" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            GRADE NOT ALLOTED STUDENT COUNT (COURSE-WISE)
                                                        </div>
                                                        <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                            <tr class="header">
                                                                <th>
                                                                    Course.
                                                                </th>
                                                                <th>
                                                                    Student Count
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                        <td valign="middle">
                                                            <%# Eval("COURSENAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("COUNT")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 20px; text-align: center">
                                        <div style="width: 100%; text-align: center">
                                            <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            Student List
                                                        </div>
                                                        <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                            <tr class="header">
                                                                <th style="width: 5%">
                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                                </th>
                                                                <th>
                                                                    ROLL No - Section.
                                                                </th>
                                                                <th>
                                                                    Registration No.
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
                                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                        <td style="width: 5%">
                                                            <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                                        </td>
                                                        <td valign="middle">
                                                            <asp:HiddenField ID="hdfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                            <%# Eval("ROLL_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("NAME")%>
                                                            <span style="color: Red">
                                                                <%# (Eval("TR_IDNO").ToString() == "True") ? "[ T-Result ]": "" %>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
