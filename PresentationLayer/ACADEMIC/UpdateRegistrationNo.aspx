<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UpdateRegistrationNo.aspx.cs" Inherits="ACADEMIC_UpdateRegistrationNo" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; background-color: Aqua; padding-left: 5px">
                    <img src="../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK REG NO ALLOTMENT</h3>                          
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group col-md-4">
                                    <label>Admissio Batch</label>
                                      <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>College / School Name</label>
                                     <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="submit" AutoPostBack="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClgname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" InitialValue="0"
                                            ErrorMessage="Please Select College Name" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Degree</label>
                                      <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="submit" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="submit" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Semester</label>
                                      <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </form>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show" 
                                    OnClick="btnShow_Click" ValidationGroup="submit" CssClass="btn btn-primary" />
                                <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click"
                                    Text="Save"  ValidationGroup="submit" CssClass="btn btn-primary" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear"
                                    OnClick="btnClear_Click" CssClass="btn btn-warning"  />

                                <asp:Button ID="btnReport" runat="server"
                                    Text="Report"  OnClick="btnReport_Click" ValidationGroup="submit" CssClass="btn btn-info" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </p>
                            <div> <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                        <asp:HiddenField ID="hdnTot" runat="server" /></div>
                            <div class="col-md-12 table table-responsive">
                                <asp:Panel ID="pnlStudent" runat="server">
                <div class="table table-responsive">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                    <asp:ListView ID="lvStudents" runat="server">
                        <LayoutTemplate>
                            <div id="demo-grid">                               
                                    <h4>Student List</h4>
                                <table class="table table-bordered table-hover">
                                    <thead>
                                    <tr class="bg-light-blue">
                                        <th>Sr. No.
                                        </th>
                                        <th>Name
                                        </th>
                                        <th>Roll No.
                                        </th>
                                        <th>Registration No.
                                        </th>
                                        <th>
                                            <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" />
                                            Select 
                                        </th>
                                    </tr></thead>
                                    <tbody>
                                    <tr id="itemPlaceholder" runat="server" /></tbody>
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRegno" runat="server" Text='<%# Container.DataItemIndex+1%>' />
                                    <asp:HiddenField ID="hfdIdno" runat="server" Value='<%# Eval("IDNO")%>' />                                
                                </td>
                                <td>
                                    <%# Eval("STUDNAME")%>
                                </td>
                                <td>
                                    <%# Eval("REGNO")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRollNo" runat="server" Text='<%# Eval("ENROLLNO")%>' />
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>                        
                    </asp:ListView>
                        </asp:Panel>
                </div>
            </asp:Panel>
                            </div> 
                        </div>
                    </div>
                </div>
            </div>

 <div id="divMsg" runat="server"></div>
        </ContentTemplate>
        <Triggers>
           <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>


            <table cellpadding="0" cellspacing="0" width="100%">
           <%--     <tr>
                    <td class="vista_page_title_bar" style="height: 30px">BULK REG NO ALLOTMENT
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
                </tr>--%>
              <%--  <tr>
                    <td style="padding: 10px">
                        <div style="color: Red; font-weight: bold">
                            &nbsp;Note : * marked fields are Mandatory
                        </div>
                        <fieldset class="fieldset" style="width: 70%">
                            <legend class="legend">Reg No Allotment</legend>
                            <table cellpadding="0" cellspacing="0" width="100%">--%>
                                <%--<tr>
                                    <td class="form_left_label">
                                        <span class="validstar">*</span>Admission Batch :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" Width="300px"
                                            ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td class="form_left_label">
                                        <span class="validstar">*</span>College / School Name :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" Width="300px"
                                            ValidationGroup="submit" AutoPostBack="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClgname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" InitialValue="0"
                                            ErrorMessage="Please Select College Name" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td class="form_left_label">
                                        <span class="validstar">*</span>Degree :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" Width="300px"
                                            ValidationGroup="submit" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td class="form_left_label">
                                        <span class="validstar">*</span>Branch :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" Width="300px"
                                            ValidationGroup="submit" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td class="form_left_label">
                                        <span class="validstar">*</span>Semester :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" Width="300px"
                                            ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                                <%-- <tr>
                                    <td class="form_left_label">
                                        IDType :</td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlIdtype" runat="server" AppendDataBoundItems="true" onselectedindexchanged="ddlIdtype_SelectedIndexChanged" 
                                            ValidationGroup="submit" Width="150px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="form_left_text">&nbsp;<asp:Button ID="btnShow" runat="server" Text="Show" Width="80px"
                                        OnClick="btnShow_Click" ValidationGroup="submit" />
                                        <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click"
                                            Text="Save" Width="132px" ValidationGroup="submit" />
                                        &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px"
                                            OnClick="btnClear_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnReport" runat="server"
                                            Text="Report" Width="80px" OnClick="btnReport_Click" ValidationGroup="submit" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>--%>
                               <%-- <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                        <asp:HiddenField ID="hdnTot" runat="server" />
                                    </td>
                                </tr>--%>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <%--<asp:Panel ID="pnlStudent" runat="server">
                <div style="width: 70%; padding: 10px;">
                    <asp:ListView ID="lvStudents" runat="server">
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Student List
                                </div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="header">

                                        <th style="width: 10%">Sr. No.
                                        </th>
                                        <th style="width: 40%">Name
                                        </th>
                                        <th style="width: 20%">Roll No.
                                        </th>
                                        <th style="width: 20%">Registration No.
                                        </th>
                                        <th style="width: 10%">
                                            <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" />
                                            Select 
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item">

                                <td style="width: 10%">
                                    <asp:Label ID="lblRegno" runat="server" Text='<%# Container.DataItemIndex+1%>' Width="90px" />
                                    <asp:HiddenField ID="hfdIdno" runat="server" Value='<%# Eval("IDNO")%>' />                                
                                </td>
                                <td style="width: 40%">
                                    <%# Eval("STUDNAME")%>
                                </td>
                                <td style="width: 20%">
                                    <%# Eval("REGNO")%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtRollNo" runat="server" Text='<%# Eval("ENROLLNO")%>' Width="90px" />
                                </td>
                                <td style="width: 10%">
                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">

                                <td style="width: 10%">

                                    <asp:Label ID="lblRegno" runat="server" Text='<%# Container.DataItemIndex+1%>' Width="90px" />
                                    <asp:HiddenField ID="hfdIdno" runat="server" Value='<%# Eval("IDNO")%>' />
                                </td>
                                <td style="width: 40%">
                                    <%# Eval("STUDNAME")%>
                                </td>
                                <td style="width: 20%">
                                    <%# Eval("REGNO")%>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtRollNo" runat="server" Text='<%# Eval("ENROLLNO")%>' Width="90px" />
                                </td>
                                <td style="width: 10%">
                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </div>
            </asp:Panel>--%>
           
    <script type="text/javascript">
        function SelectAll(chk) {

            var hftot = document.getElementById('<%= hdnTot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;

                    }
                    else {
                        lst.checked = false;

                    }
                }

            }
        }
    </script>
</asp:Content>

