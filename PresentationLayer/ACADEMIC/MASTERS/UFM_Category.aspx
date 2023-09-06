<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UFM_Category.aspx.cs" Inherits="ACADEMIC_MASTERS_UFM_Category" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title"><b>UFM CATEGORY</b></h3>   
                  <div class="pull-right">
                                        <div style="color: Red; font-weight: bold;">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                                        </div>            
            </div>
            <div class="box-body">
                
                    <div class="col-md-4 form-group">
                        <label><span style="color:red;">*</span> Category </label>
                        <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Please Enter Category" ControlToValidate="txtCategory" ValidationGroup="Submit" SetFocusOnError="True" Display="None">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4 form-group">
                        <label><span style="color:red;">*</span> Category Description</label>
                        <asp:TextBox ID="txtCatDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Category Description" ControlToValidate="txtCatDesc" ValidationGroup="Submit" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4 form-group">
                        <label><span style="color:red;">*</span> Category Punishment</label>
                        <asp:TextBox ID="txtCatPunishment" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Category Punishment" ControlToValidate="txtCatPunishment" ValidationGroup="Submit" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-8 form-group">
                        <label>Exam Name :</label>
                        <asp:CheckBoxList ID="chkExam" runat="server" AppendDataBoundItems="True" RepeatDirection="Horizontal" RepeatColumns="5">
                        </asp:CheckBoxList>
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Debarred For</label>
                        <asp:DropDownList ID="ddlDebarred" runat="server" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                            <asp:ListItem Value="1">One</asp:ListItem>
                            <asp:ListItem Value="2">Two</asp:ListItem>
                            <asp:ListItem Value="3">Three</asp:ListItem>
                            <asp:ListItem Value="4">Four</asp:ListItem>
                            <asp:ListItem Value="5">Five</asp:ListItem>
                            <asp:ListItem Value="6">Six</asp:ListItem>
                        </asp:DropDownList>
                    </div>

            </div>      
            
            <div class="box-footer">
                <p class="text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit" CssClass="btn btn-success" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowSummary="False" ShowMessageBox="True" DisplayMode="List" />
                </p>
                <div class="col-md-12 table table-responsive">
                    <asp:ListView ID="lvUFM" runat="server">
                        <LayoutTemplate>
                            <div id="demo-grid">
                                <h4>UFM Category Details</h4>
                                <table class="table table-hover table-bordered">
                                    <thead>
                                        <tr class="bg-light-blue">
                                            <th>Edit</th>
                                            <th>Category
                                            </th>
                                            <th>Category Description
                                            </th>
                                            <th>Category Punishment
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                               
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                        CommandArgument='<%# Eval("UFMNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                        OnClick="btnEdit_Click" TabIndex="10" />
                                </td>
                                <td>
                                    <%# Eval("UFM_NAME")%>
                                </td>
                                <td>
                                    <%# Eval("UFM_DESC")%>
                                </td>
                                <td>
                                    <%# Eval("UFM_PUNISHMENT")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>   
        </div>
    </div>
</div>
<%-- <table width="100%" cellpadding="2" cellspacing="2" border="0">
      
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                UFM CATEGORY
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
                            Edit Record
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
      <%--  <fieldset class="fieldset">
        <legend class="legend">Add/Edit</legend>
       
        <table cellpadding ="1" cellspacing="1" width="100%">--%>
       <%-- <tr>
        <td style="width:50px"></td>
            <td style="width:150px">
                <span style="color:Red">* </span>Category :
            </td>
            <td>
                <asp:TextBox ID="txtCategory" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Please Enter Category" ControlToValidate="txtCategory" ValidationGroup="Submit" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
       <%-- <tr>
         <td style="width:50px"></td>
            <td>
                <span style="color:Red">* </span>Category Desc :
            </td>
            <td>
                <asp:TextBox ID="txtCatDesc" runat="server" Width="350px" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Category Description" ControlToValidate="txtCatDesc" ValidationGroup="Submit" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            
            </td>
        </tr>--%>
        <%--<tr>
         <td style="width:50px"></td>
            <td>
             <span style="color:Red">* </span>Category Punishment :
            </td>
            <td>
                <asp:TextBox ID="txtCatPunishment" runat="server" Width="350px" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Category Punishment" ControlToValidate="txtCatPunishment" ValidationGroup="Submit" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
            
            </td>
        </tr>--%>
       <%-- <tr>
        <td style="width:50px"></td>
            <td ><span style="color:Red">&nbsp; </span> Exam Name:</td>
            <td>
                <asp:CheckBoxList ID="chkExam" runat="server" AppendDataBoundItems="True" RepeatDirection="Horizontal" RepeatColumns="5">
                </asp:CheckBoxList>
            </td>
        </tr>--%>
       <%-- <tr>
         <td style="width:50px"></td>
            <td ><span style="color:Red">&nbsp; </span> Debarred For:</td>
            <td>
                <asp:DropDownList ID="ddlDebarred" runat="server" AppendDataBoundItems="True" Width="150px">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
                <asp:ListItem Value="1">One</asp:ListItem>
                <asp:ListItem Value="2">Two</asp:ListItem>
                <asp:ListItem Value="3">Three</asp:ListItem>
                <asp:ListItem Value="4">Four</asp:ListItem>
                <asp:ListItem Value="5">Five</asp:ListItem>
                <asp:ListItem Value="6">Six</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
       <%-- <tr>
            <td></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowSummary="False" ShowMessageBox="True" DisplayMode="List" />
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit"/>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
                
            </td>
        </tr>--%>
       
                    <%--<asp:ListView ID="lvUFM" runat="server">
                    <LayoutTemplate>
                        <div id="demo-grid" class="vista-grid">
                            <div class="titlebar">
                               UFM Category Details
                            </div>
                            <table class="datatable" cellpadding="0" cellspacing="0">
                                <tr class="header">
                                    <th style="width:2%">Edit</th>
                                    <th style="width:8%">
                                        Category
                                    </th>
                                    <th style="width:60%">
                                        Category Description
                                    </th>
                                    <th style="width:30%">
                                        Category Punishment
                                    </th>
                                   
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item" >
                            <td style="width:2%">
                             <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                    CommandArgument='<%# Eval("UFMNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                    OnClick="btnEdit_Click" TabIndex="10" />
                            </td>
                          <td style="width:8%">
                                <%# Eval("UFM_NAME")%>
                            </td>
                            <td style="width:60%">
                                <%# Eval("UFM_DESC")%>
                            </td>
                            <td style="width:30%">
                                <%# Eval("UFM_PUNISHMENT")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>--%>
            
</asp:Content>

