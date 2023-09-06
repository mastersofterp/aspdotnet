<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentCourseFileUploadUG.aspx.cs" Inherits="ACADEMIC_StudentCourseFileUploadUG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>SEE MARKS UPLOAD</b></h3>
                             <div class="pull-right">
                                 <div style="color: red; font-weight: bold;">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        </div>

                        <div class="box-body">
                            <div class="col-md-9">
                            <div class="form-group col-md-4">
                               <span style="color:red">*</span> <label>Session</label>
                                  <asp:DropDownList ID="ddlSession" TabIndex="1" runat="server"  AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Select Session"
                                            ControlToValidate="ddlSession" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="academic">
                                        </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <span style="color:red">*</span> <label>Institute Name</label>
                                    <asp:DropDownList ID="ddlCollege" TabIndex="2" runat="server"  AppendDataBoundItems="True" AutoPostBack="true" ToolTip="Please Select Institute"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Institute"
                                            ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="academic">
                                        </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <span style="color:red">*</span> <label>Degree</label>
                                 <asp:DropDownList ID="ddlDegree" TabIndex="3" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="academic" InitialValue="0" ErrorMessage="Please Select Degree">
                                        </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <span style="color:red">*</span> <label>Branch</label>
                                  <asp:DropDownList ID="ddlBranch" TabIndex="4" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Branch"
                                            ControlToValidate="ddlBranch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="academic">
                                        </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <span style="color:red">*</span> <label>Semester</label>
                                <asp:DropDownList ID="ddlSemester" TabIndex="5" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="academic" />
                            </div>
                            <div class="form-group col-md-4" style="display:none">
                                <label>Student Type</label>
                                  <asp:DropDownList ID="ddlStudentType" TabIndex="5" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvStudentType" runat="server" ControlToValidate="ddlStudentType"
                                            Display="None" ErrorMessage="Please Select Student Type" InitialValue="-1" ValidationGroup="academic" />--%>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Excel File</label>
                                <asp:FileUpload ID="FileUpload1" TabIndex="6" runat="server" ToolTip="Select file to Import" />
                                
                                <asp:Button ID="btnUpload" runat="server"  ValidationGroup="academic" CssClass="btn btn-primary" OnClick="btnUpload_Click"
                                            TabIndex="7" Text="Upload and Verify" ToolTip="Click to Upload" />
                            </div>
                                </div>
                            <div class="col-md-3">
                                 <fieldset>
                                            <legend>Download Format</legend>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbExcelFormat"  runat="server" style="font-weight: bold;" OnClick="lbExcelFormat_Click">Pre-requisite excel format for upload</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                            </div>
                            <br /><br />
                             <div class="col-md-3">
                                 <fieldset class="fieldset" style="width:250px ;" color: Green">
                                  <legend class="legend">Note</legend>
                                  <span style="color:green">Providing section in Excel sheet is optional</span>                            
                                 
                                  </fieldset>
                            </div>
                        </div>
                        <div class="row text-center">
                            <div class="col-md-12">
                                 <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnVerifyRegister" runat="server" ValidationGroup="academic" OnClick="btnVerifyRegister_Click"
                                    TabIndex="8" Text="Save and Lock" Enabled="false" ToolTip="Click to Verify and Register Students" CssClass="btn btn-primary"/>
                                <asp:Button ID="btnCancel" runat="server" TabIndex="9" Text="Cancel" ToolTip="Click To Cancel"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning"/>
                                <asp:Button ID="btnreport" runat="server" TabIndex="9" Text="Marks Check List" ToolTip="Click To check report"
                                    OnClick="btnreport_Click" ValidationGroup="academic" CssClass="btn btn-warning"/>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="academic" />
                            </p>
                            <div class="col-md-12" id="tblCollegeChange" runat="server" >
                                <div>
                                    <asp:Button ID="btnCollegeChange" runat="server"  TabIndex="5" Text="Change College" ToolTip="Click to College Change" style="display:none"
                                        OnClientClick="return showConfirm();" OnClick="btnCollegeChange_Click" CssClass="btn btn-default" />
                                </div>
                                <div class="col-md-12 table table-responsive">
                                    <asp:Panel ID="Panel1" runat="server" Visible="false" ScrollBars="Auto">
                                    <asp:ListView ID="lvCollegeChange" runat="server">
                                                        <LayoutTemplate>
                                                            <div>                                                               
                                                                   <h4> College Change Students List                                                               </h4>
                                                                <table id="tblHead" class="table table-hover table-bordered">
                                                                 
                                                                        <tr class="bg-light-blue" id="trRow">
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHeadAll" runat="server" Text="Select" ToolTip="Select all"
                                                                                    onclick="SelectAll(this,1,'chkSelect');" />
                                                                            </th>
                                                                            <th>USN. No.
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Old College
                                                                            </th>
                                                                        </tr>
                                                                         <tr id="itemPlaceholder" runat="server" />
                                                                   </table>
                                                                                                                     
                                                            </div>                                                            
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Click to select this student for college change"
                                                                        onclick="ChkHeader(1,'cbHeadAll','chkSelect');" />
                                                                    <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REGNO") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("OLD_COLLEGE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>                                                        
                                                    </asp:ListView>
                                        </asp:Panel>
                                </div> 
                                <div class="col-md-12 table table-responsive" id="tblList" Visible="false" runat="server" >
                                    <asp:Panel ID="Panel2"  runat="server" ScrollBars="Auto" Height="350px">
                                    <asp:ListView ID="lvCourse" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Course Mismatch List</h4>
                                                <table id="tblHead" class="table table-hover table-bordered">
                                                   
                                                        <tr class="bg-light-blue" id="trRow">
                                                            <th>Sr.No.
                                                            </th>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                        </tr>
                                                          <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                   
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("SRNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("CCODE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SNAME") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                        </asp:Panel>
                                </div> 
                                <div class="col-md-12 table table-responsive" id="tblusn" runat="server" Visible="false">
                                    <asp:Panel ID="Panel3" runat="server"  ScrollBars="Auto" Height="350px">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>USN No Mismatch List </h4>
                                                <table id="tblHead" class="table table-hover table-bordered">
                                                  
                                                        <tr class="bg-light-blue" id="trRow">
                                                            <th>Sr.No.
                                                            </th>
                                                            <th>USN. No.
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                        </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                   </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("SRNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SNAME") %>
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
            </ContentTemplate>
            <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                            <asp:PostBackTrigger ControlID="lbExcelFormat" />
                        </Triggers>
        </asp:UpdatePanel>
    </div>


     <div id="divMsg" runat="server">
                </div>
    <table cellpadding="0" cellspacing="0" style="width: 100%;">
     <%--   <tr>
            <td class="vista_page_title_bar" style="height: 30px">FILE UPLOAD
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
                <div id="divMsg" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                          
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
                            
                            <ScriptAction Script="Cover($get('ctl00$ctp$btnHelp'), $get('flyout'));" />
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
        <tr>
            <td>
                <br />
                <div style="color: Red; font-weight: bold">
                    &nbsp;Note : * marked fields are Mandatory
                </div>
            </td>
        </tr>--%>
       <%-- <tr>
            <td colspan="2">
                <fieldset class="fieldset">
                    <legend class="legend">Student Record Upload</legend>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" class="datatable">
                                <tr>--%>
                                    <%--<td style="width: 15%" class="form_left_label">Session<span style="Color: Red;">*</span> :
                                    </td>
                                    <td style="width: 50%" class="form_left_text">
                                        <asp:DropDownList ID="ddlSession" runat="server" Width="60%" AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Select Session"
                                            ControlToValidate="ddlSession" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="academic">
                                        </asp:RequiredFieldValidator>
                                    </td>--%>
                                    <td style="width: 30%" rowspan="6">
                                       <%-- <fieldset class="fieldset">
                                            <legend class="legend">Download Format</legend>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbExcelFormat" runat="server" OnClick="lbExcelFormat_Click">Pre-requisite excel format for upload</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>--%>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td class="form_left_label">College<span style="Color: Red;">*</span> :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlCollege" runat="server" Width="60%" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select College"
                                            ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="academic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td class="form_left_label">Degree<span style="Color: Red;">*</span> :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlDegree" runat="server" Width="60%" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="academic" InitialValue="0" ErrorMessage="Please Select Degree">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td class="form_left_label">Branch<span style="Color: Red;">*</span> :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="60%" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Branch"
                                            ControlToValidate="ddlBranch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="academic">
                                        </asp:RequiredFieldValidator>

                                    </td>
                                </tr>--%>
                             <%--   <tr>
                                    <td class="form_left_label">Semester<span style="Color: Red;">*</span> :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSemester" runat="server" Width="150px" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="academic" />
                                    </td>
                                </tr>--%>
                              <%--  <tr>
                                    <td class="form_left_label">Student Type<span style="Color: Red;">*</span> :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlStudentType" runat="server" Width="150px" AppendDataBoundItems="True">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStudentType" runat="server" ControlToValidate="ddlStudentType"
                                            Display="None" ErrorMessage="Please Select Student Type" InitialValue="-1" ValidationGroup="academic" />
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td class="form_left_label">Excel File<span style="color: #FF0000">*</span> :
                                    </td>
                                    <td class="form_left_label">
                                        <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to Import" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnUpload" runat="server" ValidationGroup="academic" OnClick="btnUpload_Click"
                                            TabIndex="2" Text="Upload" ToolTip="Click to Upload" />
                                        <asp:Button ID="btnVerifyRegister" runat="server" ValidationGroup="academic" OnClick="btnVerifyRegister_Click"
                                            TabIndex="2" Text="Verify and Register" ToolTip="Click to Verify and Register Students" />
                                        <asp:Button ID="btnCancel" runat="server" TabIndex="5" Text="Cancel" ToolTip="Click To Cancel"
                                            OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="academic" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 100%" colspan="3">
                                        <br />
                                        <table  cellpadding="0" cellspacing="0" width="100%" >
                                           <%-- <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnCollegeChange" runat="server" TabIndex="5" Text="Change College" ToolTip="Click to College Change"
                                                         OnClientClick="return showConfirm();" OnClick="btnCollegeChange_Click"/>
                                                
                                                    <br />
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 80%" valign="top">
                                                    <%--<asp:ListView ID="lvCollegeChange" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    College Change Students List
                                                                </div>
                                                                <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <thead>
                                                                        <tr class="header" id="trRow">
                                                                            <th style="text-align: left; width: 10%">
                                                                                <asp:CheckBox ID="cbHeadAll" runat="server" Text="Select" ToolTip="Select all"
                                                                                    onclick="SelectAll(this,1,'chkSelect');" />
                                                                            </th>
                                                                            <th style="text-align: left; width: 10%">Reg. No.
                                                                            </th>
                                                                            <th style="text-align: left; width: 40%">Name
                                                                            </th>
                                                                            <th style="text-align: left; width: 40%">Old College
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                            <div class="listview-container">
                                                                <div id="Div1" class="vista-grid">
                                                                    <table id="tblData" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td style="text-align: left; width: 10%">
                                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Click to select this student for college change"
                                                                        onclick="ChkHeader(1,'cbHeadAll','chkSelect');" />
                                                                    <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                </td>
                                                                <td style="text-align: left; width: 10%">
                                                                    <%# Eval("REGNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 40%">
                                                                    <%# Eval("SNAME") %>
                                                                </td>
                                                                <td style="text-align: left; width: 40%">
                                                                    <%# Eval("OLD_COLLEGE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem">
                                                                <td style="text-align: left; width: 10%">
                                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Click to select this student for college change"
                                                                        onclick="ChkHeader(1,'cbHeadAll','chkSelect');" />
                                                                    <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                </td>
                                                                <td style="text-align: left; width: 10%">
                                                                    <%# Eval("REGNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 40%">
                                                                    <%# Eval("SNAME") %>
                                                                </td>
                                                                <td style="text-align: left; width: 40%">
                                                                    <%# Eval("OLD_COLLEGE") %>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>--%>
                                                </td>
                                                <td style="width: 40%"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="3">
                                        <br />
                                        <table cellpadding="0" cellspacing="0" width="100%" >
                                            <tr>
                                                <td style="width: 45%" valign="top">
                                                    <%--<asp:ListView ID="lvCourse" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    Course Mismatch List
                                                                </div>
                                                                <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <thead>
                                                                        <tr class="header" id="trRow">
                                                                            <th style="text-align: left; width: 10%">Sr.No.
                                                                            </th>
                                                                            <th style="text-align: left; width: 30%">Course.
                                                                            </th>
                                                                            <th style="text-align: left; width: 60%">Student Name
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                            <div class="listview-container">
                                                                <div id="Div1" class="vista-grid">
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
                                                                <td style="text-align: left; width: 10%">
                                                                    <%# Eval("SRNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 30%">
                                                                    <%# Eval("CCODE") %>
                                                                </td>
                                                                <td style="text-align: left; width: 60%">
                                                                    <%# Eval("SNAME") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem">
                                                                <td style="text-align: left; width: 10%">
                                                                    <%# Eval("SRNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 30%">
                                                                    <%# Eval("CCODE") %>
                                                                </td>
                                                                <td style="text-align: left; width: 60%">
                                                                    <%# Eval("SNAME") %>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>--%>
                                                </td>
                                                <td style="width: 10%"></td>
                                                <td style="width: 45%" valign="top">
                                                    <%--<asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    Roll No Mismatch List
                                                                </div>
                                                                <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <thead>
                                                                        <tr class="header" id="trRow">
                                                                            <th style="text-align: left; width: 10%">Sr.No.
                                                                            </th>
                                                                            <th style="text-align: left; width: 30%">Roll. No.
                                                                            </th>
                                                                            <th style="text-align: left; width: 60%">Student Name
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                            <div class="listview-container">
                                                                <div id="Div1" class="vista-grid">
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
                                                                <td style="text-align: left; width: 10%">
                                                                    <%# Eval("SRNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 30%">
                                                                    <%# Eval("REGNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 60%">
                                                                    <%# Eval("SNAME") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem">
                                                                <td style="text-align: left; width: 10%">
                                                                    <%# Eval("SRNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 30%">
                                                                    <%# Eval("REGNO") %>
                                                                </td>
                                                                <td style="text-align: left; width: 60%">
                                                                    <%# Eval("SNAME") %>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                     
    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headerid, headid, chk) {
            try {
                var tbl = document.getElementById('tblData');//'tblCollegeChange';//
                var list = 'lvCollegeChange';
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function ChkHeader(chklst, head, chk) {
            try {
                var tbl = document.getElementById('tblData');//'tblCollegeChange';//
                var list = 'lvCollegeChange';
                var headid = document.getElementById('ctl00_ContentPlaceHolder1_'+list+'_' + head);
                var chkcnt = 0;

                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length; i++) {
                        var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk);
                        if (chkid.checked)
                            chkcnt++;
                    }
                }
                if (chkcnt > 0)
                    headid.checked = true;
                else
                    headid.checked = false;
            }
            catch (e) {
                alert(e);
            }
        }

        function showConfirm() {
            try {
                var validate = false;
                if (Page_ClientValidate()) {
                    if (ValidatorOnSubmit()) {
                        var tbl = document.getElementById('tblData');//'tblCollegeChange';//
                        var list = 'lvCollegeChange';
                        //var headid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + head);
                        var chkcnt = 0;

                        var dataRows = tbl.getElementsByTagName('tr');
                        if (dataRows != null) {
                            for (i = 0; i < dataRows.length; i++) {
                                var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_chkSelect');
                                if (chkid.checked)
                                    chkcnt++;
                            }
                        }
                        if (chkcnt > 0) {
                            var ret = confirm('Do you really want to change the college of selected students to ' + document.getElementById('<%= ddlCollege.ClientID %>').options[document.getElementById('<%= ddlCollege.ClientID %>').selectedIndex].text + ' ?\nOnce changed it cannot be modified.');
                            if (ret == true) {
                                //FreezeScreen('Your Data is Being Processed...');
                                validate = true;
                            }
                        }
                        else {
                            alert('Please select atleast one student from the list to change college.');//ctl00_ContentPlaceHolder1_ddlCollege
                            validate = false;
                        }
                    }
                }
            }
            catch (e) {
                alert(e);
            }
            return validate;
        }
    </script>
</asp:Content>
