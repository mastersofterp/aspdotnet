<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BulkSemesterPromotionUG.aspx.cs" Inherits="ACADEMIC_BulkSemesterPromotionUG" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <img src="../IMAGES/anim_loading_75x75.gif" alt="Loading" />
                    <b>Please Wait..</b>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>--%>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="upddetails" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">BULK SEMESTER PROMOTION FOR UG</h3>                               
                            </div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="form-group col-md-4">
                                        <label>Session </label>
                                         <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>College /School Name </label>
                                         <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>Degree </label>
                                          <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>Branch </label>
                                          <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>Scheme </label>
                                           <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                        ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>Semester </label>
                                           <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-12"><asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label></div>
                                </div>
                            </form>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="14" Text="Show" ValidationGroup="teacherallot"
                                        CssClass="btn btn-primary" ToolTip="SHOW" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnSave" runat="server" TabIndex="15" Text="Promote" ValidationGroup="teacherallot"
                                         CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="PROMOTE" />
                                    <asp:Button ID="btnClear" runat="server" TabIndex="16" Text="Clear"   CssClass="btn btn-warning" ToolTip="CLEAR" OnClick="btnClear_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                                <div id="dvListView" class="col-md-12 table table-responsive" >
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                               <div id="demo-grid">                                                   
                                                        <h4>Select Student</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                        <tr class="bg-light-blue">

                                                           <th>
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Select" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
                                                            </th>
                                                             <th>Reg. no.
                                                            </th>
                                                             <th>Student Name
                                                            </th>
                                                             <th>Current sem.
                                                            </th>
                                                             <th>Promoted Sem.
                                                            </th>
                                                             <th>Eligible
                                                            </th>

                                                        </tr></thead>
                                                         <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                    </table>                                                
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <tr>
                                                   <td>
                                                        <asp:CheckBox ID="chkRegister" runat="server" ToolTip="Click to select this student for semester promotion" Enabled='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ? true :false) %>'
                                                            Text='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ?  " " : "PROMOTED" )%>'
                                                            ForeColor="Green" Font-Bold="true"
                                                            onclick="ChkHeader(1,'cbHeadReg','chkRegister');" />
                                                    </td>
                                                   <td>
                                                        <asp:Label runat="server" ID="lblregno" Text='<%# Eval("regno")%>' ToolTip='<%# Eval("idno")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblstudname" Text='<%# Eval("studname")%>' ToolTip='<%# Eval("sectionno")%>'></asp:Label>
                                                    </td>

                                                  <td>
                                                        <%# Eval("semesterno") %>
                                                    </td>
                                                   <td>
                                                        <%# Eval("promoted_sem") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblstatus" Text='<%# Eval("statusno") %>'></asp:Label>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                        </asp:Panel>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>





            <%--    <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="vista_page_title_bar" style="height: 30px" colspan="2">BULK SEMESTER PROMOTION FOR UG
                            <!-- Button used to launch the help (animation) -->
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <!-- "Wire frame" div used to transition from the button to the info panel -->
                            <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                            </div>
                            <!-- Info panel to be displayed as a flyout when the button is clicked -->
                            <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                    <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                        ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
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
                                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                    </p>
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
             <%--   <br />
                <div style="color: Red; font-weight: bold">
                    &nbsp;Note : * marked fields are Mandatory
                </div>
                <div style="width: 98%; padding-left: 10px">
                    <fieldset class="fieldset">
                        <legend class="legend">SELECTION CRITERIA</legend>
                        <table width="100%">--%>
                           <%-- <tr>
                                <td class="form_left_label" style="width: 10%">
                                    <span class="validstar">*</span>Session :
                                </td>
                                <td align="left" style="width: 40%">
                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>


                            </tr>--%>
                           <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>College /School Name :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot"
                                        AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                           <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Degree :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot"
                                        AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                           <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Branch :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot"
                                        AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                           <%-- <tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Scheme :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" Width="30%"
                                        ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <%--<tr>
                                <td class="form_left_label">
                                    <span class="validstar">*</span>Semester :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>



                          <%--  <tr>
                                <td class="form_left_label">&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label></td>
                            </tr>--%>
                           <%-- <tr>
                                <td>&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="14" Text="Show" ValidationGroup="teacherallot"
                                        Width="100px" ToolTip="SHOW" OnClick="btnShow_Click" />&nbsp;&nbsp;
                                      
                                    <asp:Button ID="btnSave" runat="server" TabIndex="15" Text="Promote" ValidationGroup="teacherallot"
                                        Width="100px" OnClick="btnSave_Click" ToolTip="PROMOTE" />&nbsp;&nbsp;
                                      
                                    <asp:Button ID="btnClear" runat="server" TabIndex="16" Text="Clear" Width="80px" ToolTip="CLEAR" OnClick="btnClear_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%--<div id="dvListView">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                               <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        Select Student
                                                    </div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0">
                                                        <tr class="header">

                                                           <th style="text-align: left;" width="15%">
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Select" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
                                                            </th>
                                                             <th style="text-align: left;" width="15%">Reg. no.
                                                            </th>
                                                             <th style="text-align: left;" width="15%">Student Name
                                                            </th>
                                                             <th style="text-align: center;" width="15%">Current sem.
                                                            </th>
                                                             <th style="text-align: center;" width="15%">Promoted Sem.
                                                            </th>
                                                             <th style="text-align: center;" width="15%">Eligible
                                                            </th>

                                                        </tr>
                                                    </table>
                                                    <div class="listview-container" style="height: 450px !important;">
                                                        <div id="demo-grid1" class="vista-grid">
                                                            <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <tr class="item">
                                                   <td style="text-align: left;" width="15%">
                                                        <asp:CheckBox ID="chkRegister" runat="server" ToolTip="Click to select this student for semester promotion" Enabled='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ? true :false) %>'
                                                            Text='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ?  " " : "PROMOTED" )%>'
                                                            ForeColor="Green" Font-Bold="true"
                                                            onclick="ChkHeader(1,'cbHeadReg','chkRegister');" />
                                                    </td>
                                                   <td style="text-align: left;" width="15%">
                                                        <asp:Label runat="server" ID="lblregno" Text='<%# Eval("regno")%>' ToolTip='<%# Eval("idno")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;" width="15%">
                                                        <asp:Label runat="server" ID="lblstudname" Text='<%# Eval("studname")%>' ToolTip='<%# Eval("sectionno")%>'></asp:Label>
                                                    </td>

                                                  <td style="text-align: center;" width="15%">
                                                        <%# Eval("semesterno") %>
                                                    </td>
                                                   <td style="text-align: center;" width="15%">
                                                        <%# Eval("promoted_sem") %>
                                                    </td>
                                                    <td style="text-align: center;" width="15%">
                                                        <asp:Label runat="server" ID="lblstatus" Text='<%# Eval("statusno") %>'></asp:Label>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </div>--%>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <br />
                <%--STUDENT LIST--%>
            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

        function SelectAll(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {

                    if (headchk.checked == true) {

                        e.checked = true;

                    }
                    else {
                        e.checked = false;
                    }
                }

            }

            if (headchk.checked == true) {
                txtTot.value = hdfTot.value;
                txtCredits.value = hdfCredits.value;
            }
            else {
                txtTot.value = 0;
                txtCredits.value = 0;
            }
        }

    </script>

</asp:Content>
