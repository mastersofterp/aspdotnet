<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="QualifyMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_QualifyMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
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
    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updBatch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Qualify/Entrance Entry<asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgrammeType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Programme Type" AutoPostBack="True" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="1">Qualify</asp:ListItem>
                                            <asp:ListItem Value="2">Entrance</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStatusType"
                                            Display="None" ErrorMessage="Please Select Programme Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Qualify/Entrance Name</label>
                                        </div>
                                        <asp:TextBox ID="txtExamName" runat="server" TabIndex="1" MaxLength="50"
                                            ToolTip="Please Enter Qualify/Entrance Exam" />
                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="txtExamName"
                                            Display="None" ErrorMessage="Please Enter Qualify/Entrance Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatusType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Status Type" AutoPostBack="True" OnSelectedIndexChanged="ddlStatusType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Qualify</asp:ListItem>
                                            <asp:ListItem Value="2">Entrance</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEducation" runat="server" ControlToValidate="ddlStatusType"
                                            Display="None" ErrorMessage="Please Select Status Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trDegree" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Degree" AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="trQualLevel" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Qualification level</label>
                                        </div>
                                        <asp:DropDownList ID="ddlQualification" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Qualification level" AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="ddlQualification"
                                            Display="None" ErrorMessage="Please Select Qualification level" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="3" />
                                <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click" Visible="false"
                                    TabIndex="4" Text="Report" ToolTip="Show Report" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="5" />

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <asp:Repeater ID="lvQualifyExamName" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>Qualify/Entrance Exam Name List &nbsp;&nbsp;
                                                        <lable style="color: red;">Note : ( Qualify = Q & Entrance = E )</lable>
                                                    </h5>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Qualify/Entrance Name
                                                        </th>
                                                        <th>Status Type
                                                        </th>
                                                        <%-- <th hidden="hidden">Qualification Level
                                                        </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("QUALIFYNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("QUALIEXMNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("QEXAMSTATUS")%>
                                                    </td>
                                                    <%-- <td hidden="hidden">
                                                        <%# Eval("QUALILEVELNAME")%>
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>



            <%--<table cellpadding="0" cellspacing="0" width="98%">
               
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">QUALIFY/ENTRANCE EXAM MASTER&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
              
                <tr>
                    <td align="center" style="color: #FF0000">
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
                                    Edit Record
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

                        <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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

            <%--ADD RECORD--%>
            <%--  <table cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td>
                        <div class="tblpadd">
                            <div style="color: Red; font-weight: bold">
                                &nbsp;Note : * marked fields are Mandatory
                            </div>
                            <fieldset class="fieldset">
                                <legend class="legend">Qualify/Entrance Entry</legend>
                                <table cellpadding="0" cellspacing="0" width="70%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">--%>
            <%-- <tr>
                                                    <td class="form_left_label">
                                                        <span class="validstar">*</span>Qualify/Entrance Name :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:TextBox ID="txtExamName" runat="server" Width="200px" TabIndex="1" MaxLength="50"
                                                            ToolTip="Please Enter Qualify/Entrance Exam" />
                                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="txtExamName"
                                                            Display="None" ErrorMessage="Please Enter Qualify/Entrance Name" ValidationGroup="submit"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>--%>
            <%--<tr>
                                                    <td class="form_left_label">
                                                        <span class="validstar">*</span>Status Type :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlStatusType" runat="server" TabIndex="2" Width="200px"
                                                            ToolTip="Please Select Status Type" AutoPostBack="True" OnSelectedIndexChanged="ddlStatusType_SelectedIndexChanged" >
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Qualify</asp:ListItem>
                                                            <asp:ListItem Value="2">Entrance</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvEducation" runat="server" ControlToValidate="ddlStatusType"
                                                            Display="None" ErrorMessage="Please Select Status Type" ValidationGroup="submit"
                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>--%>
            <%--   <tr id="trDegree" runat="server" visible="false">
                                                    <td class="form_left_label">
                                                        <span class="validstar">&nbsp;</span>Degree :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlDegree" runat="server" Width="200px"
                                                            ToolTip="Please Select Degree" AppendDataBoundItems="True">
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit"
                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>--%>
            <%--  <tr id="trQualLevel" runat="server" visible="false">
                                                    <td class="form_left_label">
                                                        <span class="validstar">*</span>Qualification level :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlQualification" runat="server" Width="200px"
                                                            ToolTip="Please Select Qualification level" AppendDataBoundItems="True">
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="ddlQualification"
                                                            Display="None" ErrorMessage="Please Select Qualification level" ValidationGroup="submit"
                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>--%>
            <tr>
                <td class="form_left_label">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                </td>
                <%--<td class="form_left_text">
                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                            Width="80px" OnClick="btnSave_Click" TabIndex="3" />&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            Width="80px" OnClick="btnCancel_Click" TabIndex="4" />
                                                        &nbsp;
                                                        <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click"
                                                            TabIndex="5" Text="Report" ToolTip="Show Report" Width="80px" />
                                                    </td>--%>
            </tr>
            <%--  </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">&nbsp; &nbsp;&nbsp; Note : ( Qualify = Q&nbsp;&nbsp; &amp;&nbsp; Entrance = E )
                    </td>
                </tr>
            </table>--%>
            <%--VIEW RECORD--%>
            <table cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td style="padding-left: 15px;">
                        <%--<asp:ListView ID="lvBatchName" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Batch Name List</div>
                                    <table class="datatable" cellpadding="0" cellspacing="0">
                                        <tr class="header">
                                            <th>
                                                Action
                                            </th>
                                            <th>
                                                Batch Name
                                            </th>
                                            <th>
                                                Subject Type
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" >
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("BATCHNO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("BATCHNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("SUBJECTTYPE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("BATCHNO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("BATCHNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("SUBJECTTYPE")%>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>--%>
                        <%--<asp:Repeater ID="lvQualifyExamName" runat="server">
                            <HeaderTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Qualify/Entrance Exam Name List
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th width="10%">Action
                                            </th>
                                            <th align="left">Qualify/Entrance Name
                                            </th>
                                            <th align="left">Status Type
                                            </th>
                                            <th align="left">
                                                Qualification Level
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item">
                                    <td align="center" width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("QUALIFYNO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                    </td>
                                    <td align="left">
                                        <%# Eval("QUALIEXMNAME")%>
                                    </td>
                                    <td align="left">
                                        <%# Eval("QEXAMSTATUS")%>
                                    </td>
                                    <td align="left">
                                        <%# Eval("QUALILEVELNAME")%>
                                    </td>
                                </tr>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
