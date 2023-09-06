<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="courseAllot1.aspx.cs" Inherits="Academic_courseAllot" Title="" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>--%>
    <%--<table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                COURSE ALLOTMENT
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
                                    
                                    <EnableAction Enabled="false" />
                                    
                                    <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                    <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                    
                                    
                                    <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                    <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                    <FadeIn AnimationTarget="info" Duration=".2"/>
                                    <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                    
                                    
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
                                    
                                    <StyleAction Attribute="overflow" Value="hidden"/>
                                    <Parallel Duration=".3" Fps="15">
                                        <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                        <FadeOut />
                                    </Parallel>
                                    
                                    
                                    <StyleAction Attribute="display" Value="none"/>
                                    <StyleAction Attribute="width" Value="250px"/>
                                    <StyleAction Attribute="height" Value=""/>
                                    <StyleAction Attribute="fontSize" Value="12px"/>
                                    <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                    
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
    </table>--%>
    <div style="padding-left: 10px; width: 90%">
        <fieldset class="fieldset">
            <legend class="legend">Course Allotment</legend>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="form_left_label" style="width: 20%;">
                        Session :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="True"
                            Width="30%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Degree :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" Width="70%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Branch :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlBranch" runat="server" Width="70%" AppendDataBoundItems="true"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="course"></asp:RequiredFieldValidator>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="DropDownList2" runat="server" Visible="false" Width="50px"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Scheme :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlScheme" runat="server" Width="70%" AppendDataBoundItems="true"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                            Display="None" InitialValue="0" ErrorMessage="Please Select Progam" ValidationGroup="course"></asp:RequiredFieldValidator>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="ddlHidden" runat="server" Visible="false" Width="50px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Semester :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" Width="30%"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="course">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Section :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                            Width="30%" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Subject Type :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Course Name :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlCourse" runat="server" Width="70%" AppendDataBoundItems="true"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                            Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="course">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td class="form_left_label">
                        Teacher Name :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlTeacher" runat="server" Width="50%" AppendDataBoundItems="true"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                            Display="None" InitialValue="0" ErrorMessage="Please Select Teacher"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Teacher from Department :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlDeptName" runat="server" Width="250px" TabIndex="1" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label" style="vertical-align: top">
                        Teacher Name :
                    </td>
                    <td class="form_left_text">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Vertical" Height="150px" BorderColor="#CDCDCD"
                            BorderStyle="Solid" BorderWidth="1px">
                            <asp:ListView ID="lvAdTeacher" runat="server">
                                <LayoutTemplate>
                                    <table style="width: 90%" cellpadding="1" cellspacing="1">
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkIDNo" runat="server" ToolTip='<%# Eval("UA_NO") %>' Text='<%# Eval("UA_FULLNAME") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="form_left_label">
                        Total Student(s) :
                    </td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtTot" runat="server" CssClass="watermarked" Enabled="False" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        Repor in:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                            <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                            <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                    </td>
                    <td class="form_left_text">
                        <asp:Button ID="btnAd" runat="server" Text="Submit" Width="80px" ValidationGroup="course"
                            OnClick="btnAd_Click" />&nbsp;
                        <asp:Button ID="btnPrint" runat="server" Text="Report" Width="80px" ValidationGroup="course"
                            CausesValidation="False" OnClick="btnPrint_Click" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Clear" Width="80px" OnClick="btnCancel_Click"
                            CausesValidation="False" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="course"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        &nbsp;
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    
    <div style="padding-left: 10px; width: 90%">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="center" colspan="2">
                    <asp:ListView ID="lvCourse" runat="server">
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Course Allotment</div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="header">
                                        <th style="width: 6%">
                                            Action
                                        </th>
                                        <th style="width: 5%">
                                            Sec
                                        </th>
                                        <th style="width: 20%">
                                            Course Name
                                        </th>
                                        <th style="width: 14%">
                                            Subject Type
                                        </th>
                                        <th style="width: 15%">
                                            Teacher Name
                                        </th>
                                        <%-- <th style="width: 40%">
                                                    Additional Teacher
                                                </th>--%>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td style="width: 6%">
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("COURSENO") %>'
                                        AlternateText='<%# Eval("UA_NO") %>' ToolTip="Delete Record" OnClick="btnDelete_Click"
                                        CausesValidation="False" />
                                </td>
                                <td style="width: 5%">
                                    <%#Eval("SECTIONNAME") %>
                                </td>
                                <td style="width: 20%">
                                    <%# Eval("COURSE_NAME")%>
                                </td>
                                <td style="width: 14%">
                                    <%# Eval("SUBNAME")%>
                                    <asp:HiddenField ID="hdfSubId" runat="server" Value='<%# Eval("SUBID")%>' />
                                </td>
                                <td style="width: 15%">
                                    <%# Eval("UA_FULLNAME")%>
                                </td>
                                <%--<td style="width: 40%">
                                            <%# GetAdTeachers(Eval("ADTEACHER"))%>
                                        </td>--%>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                <td style="width: 6%">
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("COURSENO") %>'
                                        AlternateText='<%# Eval("UA_NO") %>' ToolTip="Delete Record" OnClick="btnDelete_Click"
                                        CausesValidation="False" />
                                </td>
                                <td style="width: 5%">
                                    <%#Eval("SECTIONNAME") %>
                                </td>
                                <td style="width: 20%">
                                    <%# Eval("COURSE_NAME")%>
                                </td>
                                <td style="width: 14%">
                                    <%# Eval("SUBNAME")%>
                                    <asp:HiddenField ID="hdfSubId" runat="server" Value='<%# Eval("SUBID")%>' />
                                </td>
                                <td style="width: 15%">
                                    <%# Eval("UA_FULLNAME")%>
                                </td>
                                <%-- <td style="width: 40%">
                                            <%# GetAdTeachers(Eval("ADTEACHER"))%>
                                        </td>--%>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--  Enable the button so it can be played again --%>
    <%-- <th style="width: 40%">
                                                    Additional Teacher
                                                </th>--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" AlternateText="Warning" />
                    </td>
                    <td>
                        &nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;
    
    function showConfirmDel(source){
        this._source = source;
        this._popup = $find('mdlPopupDel');
        
        //  find the confirm ModalPopup and show it    
        this._popup.show();
    }
    
    function okDelClick(){
        //  find the confirm ModalPopup and hide it    
        this._popup.hide();
        //  use the cached button as the postback source
        __doPostBack(this._source.name, '');
    }
    
    function cancelDelClick(){
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }
    function confirmDelete(txt)
    {
        return confirm('Are u sure to delete this record..');
    }
    </script>

    <div runat="server" id="divMsg">
    </div>
</asp:Content>
