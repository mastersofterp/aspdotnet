<%@ Control Language="C#" AutoEventWireup="true" CodeFile="qualificationMas.ascx.cs"
    Inherits="Masters_qualificationMas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">QUALIFICATION MANAGEMENT</h3>
                        <p class="text-center text-bold">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                        </p>
                        <%-- <div class="box-tools pull-right">
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                </div>--%>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlQualification" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Add/Edit Qualification</div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <label>Qualification Type :</label>
                                                        <asp:DropDownList ID="ddlQType" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                            ToolTip="Please Select Qualification Type" TabIndex="1">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvQType" runat="server" InitialValue="0" ControlToValidate="ddlQType"
                                                            Display="None" ErrorMessage="Please Select Qualification Type" ValidationGroup="qual"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Qualification :</label>
                                                        <asp:TextBox ID="txtQualification" runat="server" MaxLength="20" TabIndex="2" CssClass="form-control"
                                                            ToolTip="Please Enter Qualification(only alphabets)" />
                                                        <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="txtQualification"
                                                            Display="None" ErrorMessage="Please Enter Qualification" ValidationGroup="qual"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                                ValidationGroup="qual" ToolTip="Click here to Submit" TabIndex="3" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning"
                                CausesValidation="False" ToolTip="Click here to Cancel" TabIndex="4" />
                            <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False" CssClass="btn btn-info"
                                ToolTip="Click here to Show Report" TabIndex="5" OnClick="btnShowReport_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="qual"
                                DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                        </p>
                        <div class="col-md-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvQualification" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <h4 class="box-title">Qualification List</h4>
                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Qualification
                                                        </th>
                                                        <th>Qualification Level
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("qualino") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <asp:Label ID="lblQLevelNo" runat="server" Text='<%# Eval("qualilevelno") %>' Visible="false" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblQuali" runat="server" Text='<%# Eval("quali") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("qualilevelname") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <div class="vista-grid_datapager">
                            <div class="text-center">
                                <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvQualification"
                                    PageSize="10">
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="vista_page_title_bar" style="height: 30px" colspan="2"><%--QUALIFICATION MANAGEMENT--%>
            <!-- Button used to launch the help (animation) -->
            <%-- <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                AlternateText="Page Help" ToolTip="Page Help" />--%>
        </td>
    </tr>
    <%--PAGE HELP--%>
    <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
                        Edit Record
                        <br />
                        The above button is used for selecting a record to modify.<br />
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
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <%-- Flash the text/border red and fade in the "close" button --%>
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
                            <%--  Shrink the info panel out of view --%>
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                            <%--  Reset the sample so it can be played again --%>
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
                            <%--  Enable the button so it can be played again --%>
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
        <td colspan="2" align="center">
            <%--<asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />--%>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="padding-left: 10px">
            <%--<asp:Panel ID="pnlQualification" runat="server" Style="text-align: left; width: 90%;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Add/Edit Qualification</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">Qualification Type :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlQType" runat="server" AppendDataBoundItems="true" Width="300px"
                                    ToolTip="Please Select Qualification Type" TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvQType" runat="server" InitialValue="0" ControlToValidate="ddlQType"
                                    Display="None" ErrorMessage="Please Select Qualification Type" ValidationGroup="qual"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Qualification :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtQualification" runat="server" MaxLength="20" Width="300px" TabIndex="2"
                                    ToolTip="Please Enter Qualification(only alphabets)" />
                                <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="txtQualification"
                                    Display="None" ErrorMessage="Please Enter Qualification" ValidationGroup="qual"></asp:RequiredFieldValidator>--%>
            <%--  <asp:RegularExpressionValidator ID="revQualification" runat="server" ErrorMessage="Please Enter Alphabets Only for Qualification"
                                    ValidationGroup="qual" ControlToValidate="txtQualification" ValidationExpression="^[a-zA-Z ]+$"
                                    Display="None" />--%>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    ValidationGroup="qual" ToolTip="Submit" TabIndex="3" Width="80px" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CausesValidation="False" ToolTip="Cancel" TabIndex="4" Width="80px" />&nbsp;
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False"
                                    ToolTip="Show Report" TabIndex="5" Width="80px" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="qual"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />--%>
        </td>
    </tr>
    <%--</table>
                    <br />
                </fieldset>
            </asp:Panel>--%>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="padding-left: 10px">
            <table cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td>
                        <%--<asp:ListView ID="lvQualification" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Qualification List
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="header">
                                            <th>Action
                                            </th>
                                            <th>Qualification
                                            </th>
                                            <th>Qualification Level
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("qualino") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        <asp:Label ID="lblQLevelNo" runat="server" Text='<%# Eval("qualilevelno") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblQuali" runat="server" Text='<%# Eval("quali") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("qualilevelname") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("qualino") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        <asp:Label ID="lblQLevelNo" runat="server" Text='<%# Eval("qualilevelno") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblQuali" runat="server" Text='<%# Eval("quali") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("qualilevelname") %>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>--%>
                        </div>
                       <%-- <div class="vista-grid_datapager">
                            <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvQualification"
                                PageSize="10">
                                <Fields>
                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                </Fields>
                            </asp:DataPager>
                            </div>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
