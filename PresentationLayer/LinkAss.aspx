<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LinkAss.aspx.cs" Inherits="ADMINISTRATION_LinkAss" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">ASSIGN LINK DEPARTMENT WISE</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-9 col-12">
                                <div class="sub-heading">
                                    <h5>User Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>User Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvuser" runat="server" ErrorMessage="Please Select User Type."
                                            ControlToValidate="ddlUser" Display="None" SetFocusOnError="True" ValidationGroup="Show"
                                            InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" id="trDept" runat="server">
                                        <div class="label-dynamic">
                                            <%--<label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="2" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-12" id="pnlStudent" runat="server" visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="4" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="5" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="Show" TabIndex="6" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="7"
                                            Enabled="false" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="8" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                    </div>

                                    <%--<div class="col-12">
                                        <asp:Panel ID="pnlDetail" runat="server" Visible="false">
                                            <asp:ListView ID="lvDetail" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Details</h5>
                                                    </div>
                                                    <table id="tblHead" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                        <thead class="bg-light-blue">
                                                            <tr id="trRow">
                                                                <th id="thHead">
                                                                    <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                </th>
                                                                <th>User Name
                                                                </th>
                                                                <th>Full Name
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NAME")%>' ToolTip='<%# Eval("UA_NO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>--%>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlDetail" runat="server" Visible="false">
                                            <asp:ListView ID="lvDetail" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Details</h5>
                                                    </div>
                                                  <%--  <table id="tblHead" class="table table-striped table-bordered nowrap display" style="width: 100%;" >--%>
                                                        <table class="table table-striped table-bordered nowrap" id="tblHead">
                                                        <thead class="bg-light-blue">
                                                            <tr id="trRow">
                                                                <th id="thHead">
                                                                    <asp:CheckBox ID="cbHead" runat="server"/>
                                                                </th>
                                                                <th>User Name
                                                                </th>
                                                                <th>Full Name
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NAME")%>' ToolTip='<%# Eval("UA_NO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-6 col-12">
                                <div class="sub-heading">
                                    <h5>Access Link</h5>
                                </div>
                                <asp:UpdatePanel ID="Upd_treepnl" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group col-12">
                                            <asp:Panel ID="pnlTree" runat="server" Height="250px" ScrollBars="Both" BorderStyle="Solid"
                                                BorderWidth="1px" BorderColor="Gray">
                                                <asp:TreeView ID="tvLinks" runat="server" ExpandDepth="0">
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger ControlID="btnCheckID" EventName="Click" />--%>
                                        <%--<asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>




                </div>
            </div>
        </div>
    </div>

    <%--<table cellpadding="2" cellspacing="0"  style="width:100%; margin:auto;">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px" colspan="4">
                Link
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
                            <td colspan ="3" >
                             <div style="color: Red; font-weight: bold; ">
                                &nbsp;Note : * marked fields are Mandatory<br />
                            </div>
                            </td>
                            <td></td>
                         </tr>
        <tr>
            <td style="width: 1%;">
            </td>
            <td style="width: 60%;" valign="top">
                <fieldset class="fieldset">
                    <legend class="legend">Details</legend>
                    <table cellpadding="2" cellspacing="1" width="100%">
                        
                        <tr>
                            <td style="width: 113px"><span style="color: Red;">*</span>
                                User Type :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="True" Width="180px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvuser" runat="server" ErrorMessage="Please Select User Type"
                                    ControlToValidate="ddlUser" Display="None" SetFocusOnError="True" ValidationGroup="Show"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trDept" runat="server">
                            <td>
                                &nbsp Department :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" Width="180px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                       <asp:Panel ID="pnlStudent" runat="server" Visible="false" >
                            <tr>
                                <td style="width: 113px">
                                    Degree :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" Width="180px"
                                    AutoPostBack="True" onselectedindexchanged="ddlDegree_SelectedIndexChanged" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 113px">
                                    Branch :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" Width="180px"
                                    >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 113px">
                                    Semester :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" Width="180px"
                                   >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            </tr>
                       </asp:Panel>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="Show" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    Enabled="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Panel ID="pnlDetail" runat="server" Visible="false">
                                    <asp:ListView ID="lvDetail" runat="server">
                                        <LayoutTemplate>
                                            <div class="vista-grid">
                                                <div class="titlebar">
                                                    Details</div>
                                                <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <thead>
                                                        <tr class="header" id="trRow">
                                                            <th id="thHead">
                                                                <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                            </th>
                                                            <th>
                                                                User Name
                                                            </th>
                                                            <th>
                                                                Full Name
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>
                                            <div class="listview-container">
                                                <div id="demo-grid" class="vista-grid">
                                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                <td>
                                                    <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NAME")%>' ToolTip='<%# Eval("UA_NO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                <td>
                                                    <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NAME")%>' ToolTip='<%# Eval("UA_NO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
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
            <td style="width: 30%;" valign="top">
                <fieldset class="fieldset">
                    <legend class="legend">Link</legend>
                    <asp:UpdatePanel ID="Upd_treepnl" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlTree" runat="server" ScrollBars="Both" Height="450px">
                                <asp:TreeView ID="tvLinks" runat="server" ExpandDepth="0">
                                </asp:TreeView>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            
                        </Triggers>
                    </asp:UpdatePanel>
                </fieldset>
            </td>
        </tr>
    </table>--%>

    <script type="text/javascript" language="javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvDetail$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvDetail$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            var table = $('#tblHead').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

                aLengthMenu: [
                    [100, 200, 500, 1000,],
                    [100, 200, 500, 1000, "All"]
                ],

                dom: 'lBfrtip',
                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblHead').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblHead').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblHead').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblHead').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
                //order : "desc",
            });

            $('#ctl00_ContentPlaceHolder1_lvDetail_cbHead').on('click', function () {
                // Get all rows with search applied
                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            // Handle click on checkbox to set state of "Select all" control
            $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#ctl00_ContentPlaceHolder1_lvDetail_cbHead').get(0);
                    // If "Select all" control is checked and has 'indeterminate' property
                    if (el && el.checked && ('indeterminate' in el)) {
                        // Set visual state of "Select all" control
                        // as 'indeterminate'
                        el.indeterminate = true;
                    }
                }
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblHead').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblHead').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblHead').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblHead').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblHead').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                    //order: "desc",
                });
                $('#ctl00_ContentPlaceHolder1_lvDetail_cbHead').on('click', function () {

                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });

                // Handle click on checkbox to set state of "Select all" control
                $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvDetail_cbHead').get(0);
                        // If "Select all" control is checked and has 'indeterminate' property
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control
                            // as 'indeterminate'
                            el.indeterminate = true;
                        }
                    }
                });
            });
        });
    </script>
</asp:Content>
