<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="assign_link.aspx.cs" Inherits="asssign_link" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="jquery/jquery-ui-1.7.3.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $("[id*=tvLinks] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {

                            $(this).attr("checked", "checked");
                        } else {

                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {



                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {

                        ////                   
                        if (($("input[type=checkbox]:checked", parentDIV).length == 0)) {

                            $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                        }
                        else {
                            $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                        }
                    }
                }
            });
        })

    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title"><b>ASSIGN LINK MANAGEMENT</b></h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    <div class="box-tools pull-right">
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                    </div>
                </div>

                <div class="box-body">
                    <div class="form-group col-md-12">
                        <div class="col-md-6">
                            <div class="sub-heading">
                                <h5>Select User Type</h5>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-6">
                                <label>User Type</label>
                                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                    ValidationGroup="show">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ValidationGroup="show"
                                    ErrorMessage="Please Select User Type" ControlToValidate="ddlUserType" Display="None"
                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="box-footer col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="show"
                                        OnClick="btnShow_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                    <p>
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </p>


                                <asp:Panel ID="pnlListMain" runat="server" Visible="false" ScrollBars="Auto">
                                    <div class="col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnAssign" runat="server" Text="Assign Link" CssClass="btn btn-success" OnClick="btnAssign_Click" />
                                        </p>
                                        <%--<asp:Button ID="btnAssign" runat="server" Text="Assign Link" Width="100px" OnClientClick="return validateAssign();" 
                                            onclick="btnAssign_Click" />--%>
                                    </div>
                                    <asp:ListView ID="lvUsers" runat="server">
                                        <LayoutTemplate>
                                            <div class="vista-grid">

                                                <h4>User List </h4>
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="300px">
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>
                                                                    <asp:CheckBox ID="chkHead" runat="server" Checked="true" onclick="totAllSubjects(this)" />
                                                                </th>
                                                                <th>User Name
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>
                                            </div>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkAccept" runat="server" Checked="true" ToolTip='<%# Eval("UA_NO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("UA_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>




                            </div>
                        </div>
                        <div class="col-md-6">
                            <fieldset>
                                <div class="sub-heading">
                                    <h5>Assign Links</h5>
                                </div>
                                <asp:Panel ID="pnlTree" runat="server" ScrollBars="Auto" Height="470px">
                                    <asp:TreeView ID="tvLinks" runat="server" ExpandDepth="0" ShowCheckBoxes="All">
                                    </asp:TreeView>
                                </asp:Panel>
                            </fieldset>
                        </div>
                    </div>
                    <%--                </form>--%>
                </div>
            </div>
        </div>
    </div>
    <%--<table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                ASSIGN LINK MANAGEMENT
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
        </tr>
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">Select User Type</legend>
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label" style="vertical-align: top">
                                User Type :
                            </td>
                            <td class="form_left_text" style="vertical-align: top">
                                <asp:DropDownList ID="ddlUserType" runat="server" Width="200px" AppendDataBoundItems="True"
                                    ValidationGroup="show">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ValidationGroup="show"
                                    ErrorMessage="Please Select User Type" ControlToValidate="ddlUserType" Display="None"
                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Button ID="btnShow" runat="server" Text="Show" Width="80px" ValidationGroup="show"
                                    OnClick="btnShow_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CausesValidation="false"
                                    OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                            </td>
                            <td rowspan="2" style="text-align: left; vertical-align: top; padding: 10px; width: 50%">
                                <fieldset class="fieldset">
                                    <legend class="legend">Assign Links</legend>
                                    <asp:Panel ID="pnlTree" runat="server" ScrollBars="Both" Height="400px">
                                        <asp:TreeView ID="tvLinks" runat="server" ExpandDepth="0" ShowCheckBoxes="All">
                                        </asp:TreeView>
                                    </asp:Panel>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="form_left_text" style="padding: 5px; vertical-align: top">
                                <asp:Panel ID="pnlListMain" runat="server" Visible="false">
                                    <div style="width: 80%; text-align: center;padding:5px">
                                                      <asp:Button ID="btnAssign" runat="server" Text="Assign Link" Width="100px" 
                                            onclick="btnAssign_Click" />
                                    </div>
                                    <asp:ListView ID="lvUsers" runat="server">
                                        <LayoutTemplate>
                                            <div class="vista-grid">
                                                <div class="titlebar">
                                                    User List</div>
                                                <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <thead>
                                                        <tr class="header">
                                                            <th style="width: 10%">
                                                                <asp:CheckBox ID="chkHead" runat="server" Checked="true" onclick="totAllSubjects(this)" />
                                                            </th>
                                                            <th style="width: 30%">
                                                                User Name
                                                            </th>
                                                            <th style="width: 60%">
                                                                Name
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </thead>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item" >
                                                <td style="width: 10%">
                                                    <asp:CheckBox ID="chkAccept" runat="server" Checked="true" Tooltip='<%# Eval("UA_NO")%>' />
                                                </td>
                                                <td style="width: 30%">
                                                    <%# Eval("UA_NAME") %>
                                                </td>
                                                <td style="width: 60%">
                                                    <%# Eval("UA_FULLNAME") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                <td style="width: 10%">
                                                    <asp:CheckBox ID="chkAccept" runat="server" Checked="true" Tooltip='<%# Eval("UA_NO")%>'/>
                                                </td>
                                                <td style="width: 30%">
                                                    <%# Eval("UA_NAME") %>
                                                </td>
                                                <td style="width: 60%">
                                                    <%# Eval("UA_FULLNAME") %>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>--%>

    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkAccept')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else
                            e.checked = false;

                    }
                }
            }

            if (headchk.checked == false) hdfTot.value = "0";
        }

        function validateAssign() {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (hdfTot == 0) {
                alert('Please Select Atleast One User from User List');
                return false;
            }
            else
                return true;
        }
    </script>

    <div id="divMsg" runat="server"></div>
</asp:Content>
