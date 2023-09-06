<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DemandReport.aspx.cs" 
Inherits="VEHICLE_MAINTENANCE_Reports_Demand_Report" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                Demand Report
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%-- Flash the text/border red and fade in the "close" button --%>        <%--  Shrink the info panel out of view --%>
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
                            <%--  Reset the sample so it can be played again --%>
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
    <br />
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td class="form_left_label" style="width: 15%;">
                     Academic Year <span style="color: Red">*</span>
                    </td>
                    <td style="width: 2%;">
                        <b>:</b>
                    </td>
                    <td class="form_left_text">
                      <asp:DropDownList ID="ddlAcadYear" runat="server" AppendDataBoundItems="true" Width="35%">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ErrorMessage="Please Select Academic year."
                                    ControlToValidate="ddlAcadYear" InitialValue="0" Display="None" ValidationGroup="Submit">
                                </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        Route Name </td>
                      <td style="width: 2%;">
                        <b>:</b>
                        </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlRoute" runat="server" AppendDataBoundItems="true" Width="35%" >
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr> 
                <tr>
                   <td class="form_left_label">
                         From Date <span style="color: Red">*</span>
                        </td>
                        <td>
                            <b>:</b>
                        </td>
                        <td class="form_left_text">
                    <table  cellpadding="0" cellspacing="0" >
                        <tr>
                            <td >
                                <asp:TextBox ID="txtFDate" runat="server" Width="80px">
                                </asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ErrorMessage="Please Enter From Date"
                                      ControlToValidate="txtFDate" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                <asp:Image ID="ImgBntCalc" runat="server" ImageUrl="~/images/calendar.png" />
                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                    Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                </ajaxToolKit:CalendarExtender>
                                 <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" 
                                    AcceptNegative="Left" 
                                    DisplayMoney="Left"
                                    ErrorTooltipEnabled="true" 
                                    Mask="99/99/9999" 
                                    MaskType="Date" 
                                    MessageValidatorTip="true"
                                    OnInvalidCssClass="errordate" 
                                    TargetControlID="txtFDate" 
                                    ClearMaskOnLostFocus="true">
                                </ajaxToolKit:MaskedEditExtender>
                            </td>
                            <td>
                                To Date <span style="color: Red">*</span>
                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" 
                                    ControlExtender="medt" ControlToValidate="txtFDate" Display="None" 
                                    EmptyValueMessage="Please Enter Date" 
                                    ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" 
                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" 
                                    IsValidEmpty="true" Text="*" ValidationGroup="Submit">
                                </ajaxToolKit:MaskedEditValidator>
                            </td>
                            <td>
                                <b>:</b>
                            </td>
                            <td >
                                <asp:TextBox ID="txtTDate" runat="server" Width="80px" ></asp:TextBox>  
                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ErrorMessage="Please Enter To Date"
                                            ControlToValidate="txtTDate" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                 <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTDate" PopupButtonID="Image1">
                                                </ajaxToolKit:CalendarExtender>                                              
                                                 <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                    AcceptNegative="Left" 
                                                    DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" 
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" 
                                                    TargetControlID="txtTDate" 
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" 
                                                    ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtTDate" 
                                                    EmptyValueMessage="Please Enter Date" 
                                                    IsValidEmpty="true"
                                                    ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" 
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" 
                                                    Text="*" 
                                                    ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                                
                            </td>
                        </tr>
                    </table>
                </td>
                    
                </tr>
                <tr>
                    <td class="form_left_label">
                        Report in
                    </td>
                    <td>
                        <b>:</b></td>
                    <td class="form_left_text">
                        <asp:RadioButtonList ID="rdoReportType" runat="server" 
                            RepeatDirection="Horizontal" TabIndex="11">                         
                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                            <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                            <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                   <td class="form_left_label" style="width: 15%;">
                   </td>
                   <td style="width:2%">
                   </td>
                   <td class="form_left_text">
                        <asp:Button ID="btnSubmit" runat="server" Text="Report" ValidationGroup="Submit"
                            OnClick="btnSubmit_Click" Width="80px" CausesValidation="true" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            Width="80px" CausesValidation="true" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit"  HeaderText="Following Field(s) are mandatory" />
                    </td>
                </tr>
            </table>
            <br />
            <div style="width: 90%; padding: 10px">
            </div>
        </ContentTemplate>
            
    </asp:UpdatePanel>
    
     <div id="divMsg" runat="server">
    </div>
</asp:Content>
