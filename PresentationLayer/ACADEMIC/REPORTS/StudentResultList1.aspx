<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentResultList1.aspx.cs" Inherits="ACADEMIC_REPORTS_StudentResultList"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                CONSOLIDATE REPORT
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
    <div style="padding-left: 10px; width: 99%">
        <fieldset class="fieldset1">
            <legend class="legend">Consolidate Report</legend>
            <asp:UpdatePanel ID="updUpdate" runat="server">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="form_left_label" style="width: 12%">
                                Session :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    Width="120px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvSessionGraph" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="reportgraph"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Degree :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    Width="300px" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Branch :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    Width="300px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Scheme :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlScheme" runat="server" Width="300px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvSchemeGraph" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="reportgraph"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Exam :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" Width="120px">
                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0">Regular Exam</asp:ListItem>
                                    <asp:ListItem Value="1">Re-Exam</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvexam" runat="server" ControlToValidate="ddlExam"
                                    Display="None" ErrorMessage="Please Select Exam" InitialValue="-1" ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Semester :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" Width="120px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvSemgraph" runat="server" ControlToValidate="ddlSem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="reportgraph"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Section :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSection" runat="server" Width="120px" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Student Status :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="120px">
                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                    <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStatus"
                                    Display="None" ErrorMessage="Please Select Student Status." InitialValue="-1"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="form_left_label">
                                Copy Case:
                            </td>
                            <td class="form_left_text">
                                <asp:CheckBox ID="chkCopyCase" runat="server" Text="Check/Uncheck" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                &nbsp;
                            </td>
                            <td class="form_left_text">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Student"
                                    ValidationGroup="report" Height="30px" Width="20%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label" colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Total Students Selected:
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="unwatermarked"
                                    Style="text-align: center;" Width="4%" Height="20px" BackColor="#FFFFCC" Font-Bold="True"
                                    Font-Size="Small" ForeColor="#000066"></asp:TextBox>
                                <%--meta:resourcekey="txtTotStudResource1"--%>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                <asp:HiddenField ID="hftot" runat="server" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="consolidated" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="form_left_text">
                                &nbsp;&nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="form_left_text">
                                <asp:Button ID="btnAnalysis" runat="server" OnClick="btnAnalysis_Click" Text="Result Analysis"
                                    ValidationGroup="report" Width="180px" />
                                &nbsp;<asp:Button ID="btnFail" runat="server" Text="Fail Student Roll List" Width="180px"
                                    OnClick="btnFail_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnCuttOff" runat="server" OnClick="btnCuttOff_Click" 
                                    Text="CUTOFF" ValidationGroup="report" Width="180px" Visible="false" />
                                &nbsp;&nbsp;<asp:Button ID="btnGraph" runat="server" OnClick="btnGraph_Click" 
                                    Text="Result Analysis graph" ValidationGroup="reportgraph" Width="180px" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                                    Text="Clear" Width="180px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_text">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="form_left_text">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="reportgraph" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListView ID="lvStudent" runat="server">
                                    <ItemTemplate>
                                        <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                            <td width="5%">
                                                <asp:CheckBox ID="chkStudent" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totSubjects(this);"
                                                    GroupName="BoxChk" />
                                            </td>
                                            <td>
                                                <%# Eval("ROLL_NO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <%-- <td>
                                        <%# Eval("BRANCHNAME")%>
                                    </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <div class="titlebar">
                                                Consolidate List Report
                                            </div>
                                            <table cellpadding="0" cellspacing="0" class="datatable">
                                                <tr class="header">
                                                    <th class="form_left_label" width="5%">
                                                        <asp:CheckBox ID="cbRows" runat="server" ToolTip="Check Record" onClick="SelectAll(this);" />
                                                    </th>
                                                    <th class=" form_left_label" width="10%">
                                                        Roll No.
                                                    </th>
                                                    <th class=" form_left_label" width="35%">
                                                        Student Name
                                                    </th>
                                                    <%--<th class=" form_left_label" width="40%">
                                                Branch
                                            </th>--%>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true )
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function SelectAll(chkbox) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_chkStudent');
                if (lst.type == 'checkbox') {
                    if (chkbox.checked == true)
                        lst.checked = true;
                    else
                        lst.checked = false;
                }

            }

            if (chkbox.checked == true) {
                txtTot.value = hftot.value;
            }
            else 
            {
                txtTot.value = 0;
            }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }
    </script>

</asp:Content>
