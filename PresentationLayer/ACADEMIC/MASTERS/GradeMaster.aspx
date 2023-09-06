<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" 
CodeFile="GradeMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_GradeMaster" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript">
      function RunThisAfterEachAsyncPostback()
       {
            RepeaterDiv();

       }
    
   function RepeaterDiv()
{
  $(document).ready(function() {
      
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
 
}
    </script>

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:UpdatePanel ID="updGrade" runat="server">
        <ContentTemplate>

<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title"><b>GRADE TYPE</b></h3>
                <div class="box-tools">
                                 <div class="pull-right">
                                        <div style="color: Red; font-weight: bold;">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                                        </div>        
                </div>
            </div>
         
                <div class="box-body">
                    
                              <div class="col-md-12"><asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label></div> 
                     
                        <div class="col-md-4">
                            <label><span style="color:red;">*</span> Grade Name</label>
                            <asp:TextBox ID="txtGradeName" runat="server" TabIndex="1"
                                MaxLength="50" ToolTip="Please Enter Grade Name " />
                            <asp:RequiredFieldValidator ID="rfvGradeName" runat="server" ControlToValidate="txtGradeName"
                                Display="None" ErrorMessage="Please Enter Grade Name" ValidationGroup="submit"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4" style="margin-top: 25px">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                TabIndex="3" OnClick="btnSave_Click" CssClass="btn btn-success" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                TabIndex="4" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                        <div class="col-md-12">
                <asp:Repeater ID="lvGradeType" runat="server">
                            <HeaderTemplate>
                                <div id="demo-grid">                                   
                                       <h4> Grade Name List</h4>
                                </div>
                                <table class="table table-hover table-bordered">
                                    <thead>
                                        <tr class="bg-light-blue">
                                            <th>
                                                Action
                                            </th>
                                            <th>
                                                Grade Type
                                            </th>
                                            <th>
                                                Grade Name
                                            </th>
                                        </tr>                                       
                                        </thead>
                                            <tbody>
                                                 <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("GRADE_TYPE") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click"/>
                                    </td>
                                    <td>
                                        <%# Eval("GRADE_TYPE")%>
                                    </td>
                                    <td>
                                        <%# Eval("GRADE_TYPE_NAME")%>
                                    </td>
                                </tr>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>
            </div>
                    </fieldset>
                </div>
           
            
        </div>
    </div>
</div>


<%--            <table cellpadding="0" cellspacing="0" width="98%">
                <div id="divMsg" runat="server">
                </div>
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        GRADE TYPE &nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
               
                <tr>
                    <td align="center" style="color: #FF0000">
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
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                    </td>
                </tr>
            </table>--%>
            <br />
           <%--  <table cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td>
                       <div class="tblpadd">
                            <div style="color: Red; font-weight: bold">
                                &nbsp;Note : * marked fields are Mandatory</div>
                            <fieldset class="fieldset">
                                <legend class="legend">Grade Type Entry</legend>
                                <table cellpadding="0" cellspacing="0" width="70%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">--%>
                                               
                                                <%--<tr>
                                                    <td class="form_left_label">
                                                        <span class="validstar">*</span>Grade Name :
                                                    </td>
                                                    <td class="form_left_text">
                                                         <asp:TextBox ID="txtGradeName" runat="server" Width="200px" TabIndex="1" 
                                                            MaxLength="50" ToolTip="Please Enter Grade Name " />
                                                        <asp:RequiredFieldValidator ID="rfvGradeName" runat="server" ControlToValidate="txtGradeName"
                                                            Display="None" ErrorMessage="Please Enter Grade Name" ValidationGroup="submit"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td class="form_left_label">
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                            Width="80px"  TabIndex="3" onclick="btnSave_Click" />&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            Width="80px"  TabIndex="4" onclick="btnCancel_Click" />
                                                      
                                                    </td>
                                                </tr>--%>
                                           <%-- </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
            </table>--%>
            <table cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td style="padding-left: 15px;">
                      
                        <%--<asp:Repeater ID="lvGradeType" runat="server">
                            <HeaderTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Grade Name List</div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th width="5%">
                                                Action
                                            </th>
                                            <th>
                                                Grade Type
                                            </th>
                                            <th>
                                                Grade Name
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" >
                                    <td align="center" width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("GRADE_TYPE") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click"/>
                                    </td>
                                    <td align="center">
                                        <%# Eval("GRADE_TYPE")%>
                                    </td>
                                    <td align="center">
                                        <%# Eval("GRADE_TYPE_NAME")%>
                                    </td>
                                </tr>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></FooterTemplate>
                        </asp:Repeater>--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


