<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MiscHeadMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_MiscHeadMaster" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">MISCELLANIOUS FEES</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit Counter</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Cash Book</label>
                                </div>
                                <asp:DropDownList ID="ddlMisc" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="true" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlMisc_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMisc"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Cash Book"
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Head Code</label>
                                </div>
                                <asp:TextBox ID="txtHeadCode" runat="server" TabIndex="2" MaxLength="8" />
                                <asp:RequiredFieldValidator ID="valPrintName" runat="server" ControlToValidate="txtHeadCode"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Head Code." />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Description</label>
                                </div>
                                <asp:TextBox ID="txtDescription" runat="server" TabIndex="3" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Description." />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="4" CssClass="btn btn-primary" ValidationGroup="Submit" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="5" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:ListView ID="lvCounters" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Miscellaneous Fee Head </h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit
                                                </th>
                                                <th>Head Code
                                                </th>
                                                <th>Description
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
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                CommandArgument='<%# Eval("MISCHEADSRNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" TabIndex="10" />
                                        </td>
                                        <td>
                                            <%# ((Eval("MISCHEADCODE").ToString() != string.Empty) ? Eval("MISCHEADCODE") : "--")%>
                                        </td>
                                        <td>
                                            <%# ((Eval("MISCHEAD").ToString() != string.Empty) ? Eval("MISCHEAD") : "--")%>
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

    <%--<table cellpadding="2" cellspacing="2" border="0" width="100%">
        
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                MISCELLANIOUS FEES
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
                function Cover(bottom, top, ignoreSize) 
                {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) 
                    {
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
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">Add/Edit Counter</legend>
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td width="150px">
                                Cash Book:
                            </td>
                             <td>
                                <asp:DropDownList ID="ddlMisc" runat="server" Width="300px" 
                                     AppendDataBoundItems="true" AutoPostBack="True" 
                                     onselectedindexchanged="ddlMisc_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMisc"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Miscellanious Fees."
                                    InitialValue="0" />
                            </td>                          
                        </tr>
                        <tr>
                            <td>
                                Head Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtHeadCode" runat="server" />
                                <asp:RequiredFieldValidator ID="valPrintName" runat="server" ControlToValidate="txtHeadCode"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Head Code." />
                            </td>
                        </tr>
                       <tr>
                            <td>
                                Description:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Description." />
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="lvCounters" runat="server">
                    <LayoutTemplate>
                        <div id="demo-grid" class="vista-grid">
                            <div class="titlebar">
                                List of Miscellaneous Fee Head
                            </div>
                            <table class="datatable" cellpadding="0" cellspacing="0">
                                <tr class="header">
                                    <th>
                                        Edit
                                    </th>
                                    <th>
                                       Head Code
                                    </th>
                                    <th>
                                       Description
                                    </th>
                                   
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item" >
                            <td>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                    CommandArgument='<%# Eval("MISCHEADSRNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                    OnClick="btnEdit_Click" TabIndex="10" />
                            </td>
                            <td>
                                <%# ((Eval("MISCHEADCODE").ToString() != string.Empty) ? Eval("MISCHEADCODE") : "--")%>
                            </td>
                            <td>
                                <%# ((Eval("MISCHEAD").ToString() != string.Empty) ? Eval("MISCHEAD") : "--")%>
                            </td>
                        
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <br />
                <br />
            </td>
        </tr>
    </table>--%>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
