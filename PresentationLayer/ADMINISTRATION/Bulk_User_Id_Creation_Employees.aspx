<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Bulk_User_Id_Creation_Employees.aspx.cs" Inherits="ADMINISTRATION_Bulk_User_Id_Creation_Employees"
    %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

       <div class="row">

        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                <h3 class="box-title"><b>Bulk User Creation For Employee</b></h3>   
                <div class="pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div>            
            </div>
                <div id="div1" runat="server"></div>  
                    <div class="box-body">
                      <%--  <legend class="legendPay">Bulk User Creation For Employee</legend>--%>
                          <div class="col-md-3">
                            <label>Institute Name</label>
                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                        </div>
                         <div class="col-md-3">
                            <label>Staff</label>
                            <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStaff"
                                Display="None" ErrorMessage="Please Select Staff" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label><span style="color:red;">*</span> User Type</label>
                             <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="True"
                                   CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlEmployeeType" runat="server" ControlToValidate="ddlEmployeeType"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee User Type"
                                    InitialValue="0" ValidationGroup="Admin" ></asp:RequiredFieldValidator>
                        </div>
                       </div>    
                        <div class="box-footer col-md-12">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Admin"
                                    OnClick="btnShow_Click" CssClass="btn btn-primary" /> <%----%>
                                <asp:Button ID="btnUpdate" runat="server" Text="Create Users" ValidationGroup="Admin" Enabled="false"
                                    OnClick="btnUpdate_Click" CssClass="btn btn-success" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CausesValidation="False" CssClass="btn btn-danger" />
                                <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Admin" />
                            </p>
                            <div id="dvListView" class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvStudents" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="demo-grid">

                                            <h4>Employee List</h4>

                                            <table id="tblStudents" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />Select
                                                        </th>
                                                        <th>Emp. Code
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
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
                                                <asp:CheckBox ID="chkRow" runat="server" onclick="CountSelection();" Font-Bold="true" ForeColor="Green"
                                                    Enabled='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 1 ?  false : true )%>' Text='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 1 ?  "CREATED" : "" )%>'
                                                    Checked='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 0 ?  false : true )%>' />
                                                <asp:HiddenField ID="hidStudentId" runat="server"
                                                    Value='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblreg" Text='<%# Eval("PFILENO")%>' ToolTip='<%# Eval("PFILENO")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblstud" Text='<%# Eval("NAME")%>'></asp:Label>
                                            </td>
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                    </asp:Panel>
                            </div>
                             <div id="divMsg" runat="server">
    </div>
                        </div>
                            
            </div>
        </div>
    </div>





    <table cellpadding="0" cellspacing="0" style="width:100%; margin:auto;">
        <%--<tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                BULK USER CREATION FOR EMPLOYEE
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%-- <tr>
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                    <Animations>
                            <OnClick>
                                <Sequence>
                                    <EnableAction Enabled="false" />
                                    
                                    <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                    <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                    
                                    <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                        <Move Horizontal="150" Vertical="-50" />
                                        <Resize Width="260" Height="280" />
                                        <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                                    </Parallel>
                                    
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
        <%--<tr>
            <td colspan="2" style="vertical-align: top; text-align: left; padding-left: 20px">
                <br />
                <fieldset class="fieldset">
                    <legend class="legend">Bulk Employee User Creation</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="form_left_text">
                                Employee Type:
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="True"
                                    Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlEmployeeType" runat="server" ControlToValidate="ddlEmployeeType"
                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Type" InitialValue="0" ValidationGroup="Admin"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Button ID="btnUpdate" runat="server" Text="Create Users" ValidationGroup="Admin"
                                    OnClick="btnUpdate_Click" Width="90px" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CausesValidation="False" Width="90px" />
                                <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Admin" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>--%>
    </table>
<%--    <div id="divMsg" runat="server">
    </div>--%>
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
    </script>
</asp:Content>
