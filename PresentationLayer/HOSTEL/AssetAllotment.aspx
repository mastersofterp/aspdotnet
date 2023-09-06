<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AssetAllotment.aspx.cs" Inherits="Hostel_AssetAllotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                ASSET ALLOTMENT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>
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
    </table>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <br />
                <fieldset class="fieldset" style="width: 60%">
                    <legend class="legend">Add/Edit Asset Allotment</legend>
                    <table width="100%" cellpadding="2" cellspacing="2" border="0">
                        <tr>
                            <td>
                                &nbsp;&nbsp;Hostel:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlHostel" runat="server" Width="200px" TabIndex="1" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                    Display="None" ErrorMessage="Please select hostel." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Block:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBlock" runat="server" Width="200px" TabIndex="2" AppendDataBoundItems="True"
                                    AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBlock" runat="server" ControlToValidate="ddlBlock"
                                    Display="None" ErrorMessage="Please select block." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Floor:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFloor" runat="server" Width="200px" TabIndex="3" AppendDataBoundItems="True"
                                    AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFloor" runat="server" ControlToValidate="ddlFloor"
                                    Display="None" ErrorMessage="Please select floor." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Room :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRoom" runat="server" Width="200px" TabIndex="4" 
                                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valBlock" runat="server" ControlToValidate="ddlRoom"
                                    Display="None" ErrorMessage="Please select room." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Asset:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAsset" runat="server" Width="200px" TabIndex="5" 
                                    AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAsset_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valFloor" runat="server" ControlToValidate="ddlAsset"
                                    Display="None" ErrorMessage="Please select asset." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Asset Quantity:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAssetQty" runat="server" Width="50px" TabIndex="6" MaxLength="3" />
                                <asp:RequiredFieldValidator ID="rfvAssetQty" runat="server" ControlToValidate="txtAssetQty"
                                    Display="None" ErrorMessage="Please enter asset quantity." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                                <ajaxToolKit:FilteredTextBoxExtender ID ="ftbAssetQty" runat="server" FilterType="Numbers" TargetControlID="txtAssetQty"></ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Allotment Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAllotmentDate" runat="server" Width="120px" TabIndex="7" ValidationGroup="Submit" />
                                <asp:Image ID="imgAllotmentDate" runat="server" ImageUrl="~/IMAGES/calendar.png"
                                    Width="16px" AlternateText="sdfgserafgsdrgsdfg" />
                                <ajaxToolKit:CalendarExtender ID="ceAllotmentDate" runat="server" Format="dd/MM/yyyy"
                                    PopupButtonID="imgAllotmentDate" TargetControlID="txtAllotmentDate">
                                </ajaxToolKit:CalendarExtender>
                                <ajaxToolKit:MaskedEditExtender ID="meAllotmentDate" runat="server" TargetControlID="txtAllotmentDate"
                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                    MaskType="Date" />
                                <ajaxToolKit:MaskedEditValidator ID="mvAllotmentDate" runat="server" EmptyValueMessage="Please Enter AllotmentDate"
                                    ControlExtender="meAllotmentDate" ControlToValidate="txtAllotmentDate" IsValidEmpty="false"
                                    InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                    InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                &nbsp;&nbsp;Allotment Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAllotmentCode" runat="server" TabIndex="8" MaxLength="50" ValidationGroup="Submit" />
                                <asp:RequiredFieldValidator ID="valAllotmentCode" runat="server" ControlToValidate="txtAllotmentCode"
                                    Display="None" ErrorMessage="Please enter allotment code." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" TabIndex="9" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" TabIndex="10" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:ListView ID="lvAssetAllotment" runat="server">
                    <LayoutTemplate>
                        <div id="demo-grid" class="vista-grid">
                            <div class="titlebar">
                                List of Asset Allotment
                            </div>
                            <table class="datatable" cellpadding="0" cellspacing="0">
                                <tr class="header">
                                    <th>
                                        Edit
                                    </th>
                                    <th>
                                        Room Name
                                    </th>
                                    <th>
                                        Asset Name
                                    </th>
                                    <th>
                                        Quantity
                                    </th>
                                    <th>
                                        Allotment Date
                                    </th>
                                    <th>
                                        Allotment Code
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                            <td>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                    CommandArgument='<%# Eval("ASSET_ALLOTMENT_NO") %>' AlternateText="Edit Record"
                                    ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="10" />
                            </td>
                            <td>
                                <%# Eval("ROOM_NAME")%>
                            </td>
                            <td>
                                <%# Eval("ASSET_NAME")%>
                            </td>
                            <td>
                                <%# Eval("QUANTITY") %>
                            </td>
                            <td>
                                <%# Eval("ALLOTMENT_DATE", "{0:d}")%>
                            </td>
                            <td>
                                <%# Eval("ALLOTMENT_CODE")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <div class="vista-grid_datapager">
                    <asp:DataPager ID="dpAessetAllotment" runat="server" PagedControlID="lvAssetAllotment"
                        PageSize="10" OnPreRender="dpAessetAllotment_PreRender">
                        <Fields>
                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                            <asp:NumericPagerField ButtonType="Link" ButtonCount="6" CurrentPageLabelCssClass="current" />
                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
