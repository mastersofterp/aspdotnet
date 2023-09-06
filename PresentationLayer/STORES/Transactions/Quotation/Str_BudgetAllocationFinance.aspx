<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_BudgetAllocationFinance.aspx.cs" Inherits="STORES_Transactions_Quotation_Str_BudgetAllocationFinance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                colspan="6">Budget Allocation To Proposal &nbsp;
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
            </td>
        </tr>
        <tr>
            <td>
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
            <td>
                <fieldset class="fieldset" style="width: 40%">
                    <legend class="legend">Budget Allocation To Proposal</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 40%">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;Note : * marked field is Mandatory
                                </div>
                                <br />
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="form_left_label">
                                            <span id="spanPA0" style="color: #FF0000">*</span>&nbsp; Quotation
                                        </td>
                                        <td style="width: 2%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlQuotation" runat="server" AppendDataBoundItems="true" Width="65%"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlQuotation_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlQuotation" runat="server" ControlToValidate="ddlQuotation"
                                                Display="None" ErrorMessage="Please select Quotation From List" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvddlQuotationStatus" runat="server" ControlToValidate="ddlQuotation"
                                                Display="None" ErrorMessage="Please select Quotation From List" SetFocusOnError="true"
                                                ValidationGroup="status" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            <span id="span1" style="color: #FF0000">*</span>&nbsp; Vendor 
                                        </td>
                                        <td style="width: 2%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlVendor" runat="server" AppendDataBoundItems="true" Width="65%" AutoPostBack="True" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlVendor" runat="server" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Please select Vendor From List" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvddlVendorStatus" runat="server" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Please select Vendor From List" SetFocusOnError="true"
                                                ValidationGroup="status" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">&nbsp;  
                                        </td>
                                        <td style="width: 2%">&nbsp;
                                        </td>
                                        <td class="form_button" style="text-align: left; width: 60%; padding-left: 8%">
                                            <asp:Button ID="btnShow" runat="server" Text="Show Proposal" ValidationGroup="PAPath"
                                                OnClick="btnShow_Click" />&nbsp;                                                                  
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            <span id="span2" style="color: #FF0000">*</span>&nbsp; Budget 
                                        </td>
                                        <td style="width: 2%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlBudget" runat="server" AppendDataBoundItems="true" Width="65%"
                                                AutoPostBack="False">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlBudget" runat="server" ControlToValidate="ddlBudget"
                                                Display="None" ErrorMessage="Please select Budget From List" SetFocusOnError="true"
                                                ValidationGroup="status" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">&nbsp;
                                        </td>
                                        <td style="width: 2%">&nbsp;
                                        </td>
                                        <td class="form_button" style="text-align: left; width: 60%; padding-left: 8%">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Allocate Budget" ValidationGroup="status"
                                                OnClick="btnSubmit_Click" />&nbsp;                                                                  
                                        <asp:ValidationSummary ID="vsstatus" runat="server" ValidationGroup="status"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:ListView ID="lvPAPath" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demp_grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            Budget Allocated
                                                        </div>
                                                        <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr class="header">
                                                                <th>&nbsp;</th>
                                                                <th>Quotation No</th>
                                                                <th>Vendor</th>
                                                                <th>Budget</th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("BANO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                            
                                                        </td>
                                                        <td>
                                                            <%# Eval("QUOTNO")%>
                                                        </td>
                                                         <td>
                                                            <%# Eval("PNAME")%>
                                                        </td>
                                                         <td>
                                                            <%# Eval("BHNAME")%>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>

                                            </asp:ListView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%">&nbsp;
                            </td>
                            <%--<td style="width: 30%">
                                <asp:ListView ID="lvPAPath" runat="server">
                                    <LayoutTemplate>
                                        <div id="demp_grid" class="vista-grid">
                                            <div class="titlebar">
                                                Status Of Proposal
                                            </div>
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                <tr class="header">
                                                    <th>Name</th>
                                                    <th>Status</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">                                            
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS")%>
                                            </td>
                                        </tr>

                                    </ItemTemplate>

                                </asp:ListView>
                            </td>--%>
                        </tr>

                    </table>

                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>

