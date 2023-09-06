<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" 
    CodeFile="EntranceMapping.aspx.cs" Inherits="ACADEMIC_EntranceMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:UpdatePanel ID="updGradeEntry" runat="server">
        <ContentTemplate>
              <div id="divMsg" runat="server">
                </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>QUALIFY EXAM MAPPING</b></h3>
                            
  <div class="pull-right"> 
        <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div> 
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <label><span style="color:red;">*</span>Admission Type</label>
                                    <asp:DropDownList ID="ddlAdmType" runat="server" TabIndex="1"
                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAdmType_SelectedIndexChanged" AutoPostBack="true"
                                        ToolTip="Please Select Admission Type" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Regular</asp:ListItem>
                                        <asp:ListItem Value="2">Lateral</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmType"
                                        Display="None" ErrorMessage="Please Select Admission Type." ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                    <label><span style="color:red;">*</span> Programme Type</label>
                                    <asp:DropDownList ID="ddlProgrammeType" runat="server" TabIndex="2"
                                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProgrammeType_SelectedIndexChanged"
                                        ToolTip="Please Select Programme Type." CssClass="form-control">                                        
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">UG</asp:ListItem>
                                        <asp:ListItem Value="2">PG</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProgrammeType"
                                        Display="None" ErrorMessage="Please Select Programme Type." ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label><span style="color:red;">*</span>Degree</label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged1"
                                                ToolTip="Please Select Degree." CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                 <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                    <label><span style="color:red;">*</span> Programme Code</label>
                                    <asp:DropDownList ID="ddlCode" runat="server" TabIndex="2"
                                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged"
                                        ToolTip="Please Select Programme Code." CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCode"
                                        Display="None" ErrorMessage="Please Select Programme Code." ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0" Visible="false"></asp:RequiredFieldValidator>
                                </div>
                                <%--<div class="col-md-4" style="display:none">
                                    <label><span style="color:red;">*</span> Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2"
                                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                        ToolTip="Please Select Degree">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" visible="false">
                                    <label><span style="color:red;">*</span>Programme/Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="1"
                                        AppendDataBoundItems="True"
                                        ToolTip="Please Select Programme/Branch" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Programme/Branch." ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <label><span style="color:red;">*</span> Mapping Type</label>
                                    <asp:DropDownList ID="ddlMappingType" runat="server" TabIndex="3"
                                        AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlMappingType_SelectedIndexChanged"
                                        ToolTip="Please Select Mapping Type." CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%--<asp:ListItem Value="E">Entrance Mapping</asp:ListItem>--%>
                                                            <asp:ListItem Value="Q">Qualification Mapping</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMappingType"
                                        Display="None" ErrorMessage="Please Select Mapping Type." ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                <label><span style="color:red;">*</span> Qualifying Exam</label>
                                <asp:DropDownList ID="ddlEntrance" runat="server" TabIndex="4"
                                    AppendDataBoundItems="True"
                                    ToolTip="Please Select Entrance Exam" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEntrance" runat="server" ControlToValidate="ddlEntrance"
                                    Display="None" ErrorMessage="Please Select Entrance Exam." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                <label><span style="color:red;"></span>Marks/Percentage</label>
                                <asp:DropDownList ID="ddlType" runat="server" TabIndex="4"
                                    AppendDataBoundItems="True"
                                    ToolTip="Please Select Score Type." CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem> 
                                    <asp:ListItem Value="1">Marks</asp:ListItem>                                    
                                    <asp:ListItem Value="2">Percentage</asp:ListItem>                                    
                                                                       
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlType" Visible="false"
                                    Display="None" ErrorMessage="Please Select Score Type." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <label><span style="color:red;"></span> Minimum Score/Percentage</label>
                                <asp:TextBox ID="txtScore" runat="server" CssClass="form-control" AppendDataBoundItems="true" MaxLength="5" TabIndex="6" AutoComplete="off" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtScore"
                                                Display="None" ValidationGroup="submit" Visible="false"
                                                ErrorMessage="Please Enter Minimum Score."></asp:RequiredFieldValidator>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtScore"
                                                ValidChars="0123456789." Enabled="True" />
                            </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCGPA" runat="server" visible="false">
                                    <label><span style="color:red"> </span>CGPA</label>
                                    <asp:TextBox ID="txtCGPA" runat="server" CssClass="form-control" MaxLength="5" TabIndex="7" ToolTip="Please Enter CGPA." AutoComplete="off" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCGPA" runat="server" ControlToValidate="txtCGPA" Display="None" ValidationGroup="submit" Visible="false" ErrorMessage="Please Enter CGPA."></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCGPA"
                                        ValidChars="0123456789." FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                </div> 
                                </div>
                                    </div>
                        </form>
                        <div class="box-footer">
                            <p class="text-center">
                                <td class="form_left_label">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                        TabIndex="3" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="4" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <div class="col-md-12">
                                        <asp:ListView ID="lvlist" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <h4>Qualify Exam Mapping List</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Edit </th>
                                                            <%--    <th>Programme Code - Qualify Mapping </th>--%>
                                                                <th>Programme Type</th>
                                                                <th>Admission Type</th>
                                                                <th>Degree Name</th>
                                                                <%--<th>Programme/Branch</th>--%>
                                                                <th>Marks/Percentage</th>
                                                                <th style="width:15%;text-align:center">Minimum Score/Percentage </th>
                                                                <th>CGPA</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="bntEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("ENTR_DEGREE_NO") %>' ImageUrl="~/IMAGES/edit.png" OnClick="bntEdit_Click" ToolTip="Edit" />
                                                        <%-- <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/IMAGES/delete.gif" CommandArgument='<%# Eval("ENTR_DEGREE_NO") %>'
                                            AlternateText="Delete Record" OnClientClick="return ConfirmToDelete(this);" OnClick="btnDel_Click"
                                            TabIndex="6" ToolTip="Delete" />--%></td>
                                                   <%-- <td><%# Eval("PROGRAMME_CODE") %>- <%# Eval("QUALIEXMNAME") %></td>--%>
                                                    <td><%#Eval("PROGRAMME_TYPE") %></td>
                                                    <td><%# Eval("ADMTYPE") %></td>
                                                    <td><%# Eval("DEGREENAME") %></td>
                                                 <%--   <td>
                                                        <%# Eval("BRANCHNAME") %>
                                                    </td>--%>
                                                    <td><%# Eval("MARKS_PER") %></td>
                                                    <td style="width:15%;text-align:center"><%# Eval("MIN_SCORE") %></td>
                                                    <td>
                                                        <%# Eval("CGPA") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                            </p>
                    </div>
                </div>
            </div>

            </div>



          <%--  <table cellpadding="0" cellspacing="0" width="98%">
                <div id="divMsg" runat="server">
                </div>
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">ENTRANCE EXAM MAPPING &nbsp;
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
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                    </td>
                </tr>
            </table>--%>
          <%--  <br />
            <table cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td>
                        <div class="tblpadd">
                            <div style="color: Red; font-weight: bold">
                                &nbsp;Note : * marked fields are Mandatory
                            </div>
                            <fieldset class="fieldset">
                                <legend class="legend">Entrance Exam Mapping</legend>
                                <table cellpadding="0" cellspacing="0" width="70%">
                                    <tr>
                                        <td>--%>
                                            <%--<table cellpadding="0" cellspacing="0" width="100%">--%>
                                               <%-- <tr>
                                                    <td class="form_left_label">
                                                        <span class="validstar">*</span>Degree :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" Width="30%"
                                                            AppendDataBoundItems="True"
                                                            ToolTip="Please Select Degree">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit"
                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>--%>
                                               <%-- <tr>
                                                    <td class="form_left_label">
                                                        <span class="validstar">*</span>Entrance Exam:
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlEntrance" runat="server" TabIndex="2" Width="30%"
                                                            AppendDataBoundItems="True"
                                                            ToolTip="Please Select Entrance Exam">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvEntrance" runat="server" ControlToValidate="ddlEntrance"
                                                            Display="None" ErrorMessage="Please Select Entrance Exam" ValidationGroup="submit"
                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>--%>

                                              <%--  <tr>
                                                    <td class="form_left_label">
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                            Width="80px" TabIndex="3" OnClick="btnSave_Click" />&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            Width="80px" TabIndex="4" OnClick="btnCancel_Click" />
                                                        &nbsp;
                                                      
                                                    </td>
                                                </tr>--%>
                                            <%--</table>--%>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
            </table>
            <%--VIEW RECORD--%>
         <%--   <table cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td style="padding-left: 15px;">--%>
                        <%--<asp:ListView ID="lvlist" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid" style="width: 50%; margin-left: 30%">
                                    <div class="titlebar">
                                        Entrance Exam Mapping List
                                    </div>
                                    <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                        <tr class="header">
                                            <th style="width: 5%">Action
                                            </th>
                                            <th style="width: 8%">Degree-Entrance Mapping
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/IMAGES/delete.gif" CommandArgument='<%# Eval("ENTR_DEGREE_NO") %>'
                                            AlternateText="Delete Record" OnClientClick="return ConfirmToDelete(this);" OnClick="btnDel_Click"
                                            TabIndex="6" ToolTip="Delete" />
                                    </td>
                                    <td style="width: 8%">
                                        <%# Eval("DEGREENAME") %>  - <%# Eval("QUALIEXMNAME") %>   
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>--%>
                 <%--   </td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

