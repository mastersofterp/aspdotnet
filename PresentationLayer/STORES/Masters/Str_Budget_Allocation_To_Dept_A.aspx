<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Budget_Allocation_To_Dept_A.aspx.cs" Inherits="Stores_Masters_Str_Budget_Allocation_To_Dept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate >
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
         <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x;
                        border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;" colspan="6">
                       BUDGET ALLOCATION TO DEPARTMENT
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                            border: solid 1px #D0D0D0;">
                        </div>
                    </td>
         
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 97%; padding-left: 10px;">
                    <fieldset class="fieldset">
                        <legend class="legend">Add/Edit Budget Allocation</legend>
                        <br />
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td class="form_left_label" style="padding-left:10px">
                                    Department :
                                </td>
                                <td class="form_left_text" >
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="dropdownlist" AppendDataBoundItems="true" Width="250px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"
                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="store" InitialValue="0"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="padding-left:10px">
                                    Budget :
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlBudget" runat="server" CssClass="dropdownlist" AppendDataBoundItems="true" Width="250px">
                                    <asp:ListItem Text= "Plese Select Budget" Value="0">
                                    </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlBudget" runat="server" ControlToValidate="ddlBudget"
                                        Display="None" ErrorMessage="Please Select Budget" ValidationGroup="store" InitialValue="0"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="padding-left:10px" >
                                    Amount :
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtAmount" runat="server"   CssClass="texbox" Width="245px" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtAmount" runat="server" ControlToValidate="txtAmount"
                                        Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="store"
                                       SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="amtrange" runat="server" ControlToValidate="txtAmount" type="Integer" MinimumValue="0"
                                     MaximumValue="999999999" ErrorMessage="Please Enter amount in the range 0-999999999" Display="None" ValidationGroup="store" SetFocusOnError="true">
                                    </asp:RangeValidator>
                                    <asp:CompareValidator ID="cmptxtAmount" runat="server" 
                                        ControlToValidate="txtAmount" ValidationGroup="store" 
                                        ErrorMessage="Enter Proper Amt." Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="padding-left:10px">
                                    Start Date :
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtStartDate"  runat="server"  CssClass="texbox" Width="80px" MaxLength="18" />
                                    &nbsp;<asp:Image ID="imgCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                        Style="cursor: pointer" />
                                    <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                        EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                        Display="None" ErrorMessage="Please Select Start Date in (dd/MM/yyyy Format)"
                                        ValidationGroup="store" SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolKit:MaskedEditExtender ID="meStartDate" runat="server" TargetControlID="txtStartDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <ajaxToolKit:MaskedEditValidator ID="mevtxtExpiryDate" runat="server" ControlExtender="meStartDate"
                                        ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                        InvalidValueMessage=" Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                        TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                        ValidationGroup="Store" SetFocusOnError="True" />
                                        <asp:RangeValidator runat="server"  ID="rngSdate" Display="None" SetFocusOnError="true" Type="Date"  ControlToValidate="txtStartDate" ValidationGroup="store"></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label" style="padding-left:10px">
                                    End Date :
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtEndDate" runat="server"  CssClass="texbox" Width="80px" MaxLength="18" />
                                    &nbsp;<asp:Image ID="imgCalEndDate" runat="server" ImageUrl="~/images/calendar.png"
                                        Style="cursor: pointer" />
                                    <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                        PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                        Display="None" ErrorMessage="Please Select End Date in (dd/MM/yyyy Format)" ValidationGroup="store"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolKit:MaskedEditExtender ID="meEndDate" runat="server" TargetControlID="txtEndDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true">
                                    </ajaxToolKit:MaskedEditExtender>
                                    
                                     <asp:RangeValidator  runat ="server" ID="rngEdate" Display="None" SetFocusOnError="true" Type="Date" ControlToValidate="txtEndDate" ValidationGroup="store"></asp:RangeValidator>
                                </td>
                            </tr>
                            <%--<asp:CompareValidator ID="comval1" runat="server" ControlToValidate="txtStartDate" 
                                        ControlToCompare="txtEndDate" Operator="lessthan" Type="date" ErrorMessage="Start Date Must Be Less than End Date"
                                         SetFocusOnError="true" ValidationGroup="Store" oninit="butSubmit_Click" 
                                        onload="butSubmit_Click"></asp:CompareValidator>--%>
                            <tr>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store" 
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="form_left_label ">
                                </td> 
                                <td class="form_left_text ">
                                    <br />
                                    <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" 
                                        Width="70px" OnClick="butSubmit_Click"  />
                                    <asp:Button ID="butCancel" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
                                    <asp:Button ID="btnshowrpt" Text="Report" runat="server" Width="70px" OnClick="butCancel_Click" />
                                </td>
                            </tr>
                            <%--   </table>
                    </fieldset>
                </div>
            </td>
        </tr>--%>
                           
                            <tr>
                                <td colspan="2" style="padding-left :5px; padding-right :5px " ><br />
                                    <asp:Panel ID="pnlBudAllocation" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: center">
                                            <tr>
                                                <td align="center">
                                                    <asp:ListView ID="lvBudAllocation" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    Budget Allocation</div>
                                                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr class="header">
                                                                        <th>
                                                                            Action
                                                                        </th>
                                                                        <th>
                                                                            DepartMent Name
                                                                        </th>
                                                                        <th>
                                                                            Budget
                                                                        </th>
                                                                        <th>
                                                                            Amount
                                                                        </th>
                                                                        <th>
                                                                            Start Date
                                                                        </th>
                                                                        <th>
                                                                            End Date
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SD_BUDNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MDNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("BHNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SD_BUDAMT")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SD_BUDSDATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SD_BUDEDATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SD_BUDNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MDNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("BHNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SD_BUDAMT")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SD_BUDSDATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SD_BUDEDATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>
                                                    <div class="vista-grid_datapager">
                                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvBudAllocation" PageSize="10"
                                                            OnPreRender="dpPager_PreRender">
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
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
    </ContentTemplate>
     </asp:UpdatePanel>
     </asp:Content>
