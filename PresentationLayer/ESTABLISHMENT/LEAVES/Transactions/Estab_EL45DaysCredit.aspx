<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Estab_EL45DaysCredit.aspx.cs" 
Inherits="ESTABLISHMENT_LEAVES_Transactions_Estab_EL45DaysCredit" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                CL 45 DAYS CREDIT
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
    <fieldset class="fieldsetPay" style="width:83%">
        <legend class="legendPay">CL CREDIT</legend>
        <table cellpadding="2" cellspacing="2" style="width: 70%">
    
             <tr>
                <td class="form_left_label" style="width: 25%">
                    College Name 
                </td>
                 <td style="width:1%"><b>:</b></td>
                <td class="form_left_text">
                      <asp:DropDownList ID="ddlCollege" runat="server" Width="80%" AppendDataBoundItems ="true" 
                       AutoPostBack="true" onselectedindexchanged="ddlCollege_SelectedIndexChanged">                                               
                      </asp:DropDownList>  
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                        Display="None" ErrorMessage="Please Select College Name " ValidationGroup="submit"
                        SetFocusOnError="true">
                    </asp:RequiredFieldValidator>                                                 
                </td>
            </tr>
           
          <tr>
                <td class="form_left_label">
                    Select Department 
                </td>
                <td style="width:1%"><b>:</b></td>
                <td class="form_left_text">
                    <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" Width="80%">
                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvdept" runat="server" ControlToValidate="ddldept"
                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                    &nbsp;
                </td>
            </tr>
           
            <tr>
                <td class="form_left_label">
                    Staff Type 
                </td>
                <td style="width:1%"><b>:</b></td>
                <td class="form_left_text">
                    <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" Width="80%">
                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlStaffType"
                        Display="None" ErrorMessage="Please Select Staff Type" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                    &nbsp;
                </td>
            </tr>
         <%--   <tr>
                <td class="form_left_label">
                    Year 
                </td>
                <td style="width:1%"><b>:</b></td>
                <td class="form_left_text">               
                      <asp:DropDownList ID="ddlYear" runat="server" Width="200px">                                                
                      </asp:DropDownList>                   
                </td>
            </tr>--%>
             <tr>
                                                <td class="form_left_label" style="width: 25%">
                                                    Period
                                                </td>
                                               <td style="width:1%"><b>:</b></td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlPeriod" runat="server" Width="110px" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ControlToValidate="ddlPeriod"
                                                        Display="None" ErrorMessage="Please Select Period" ValidationGroup="Leave" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td class="form_left_label" style="width: 25%">
                                                    Year
                                                </td>
                                                <td style="width:1%"><b>:</b></td>
                                                <td class="form_left_text">
                                                   <asp:DropDownList ID="ddlYear" runat="server" Width="110px" AutoPostBack="true" 
                                                    onselectedindexchanged="ddlYear_SelectedIndexChanged">     
                                                           <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>                                             
                                                  </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                        Display="None" ErrorMessage="Please Select Year" ValidationGroup="Leave" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                             <tr>
                                                  <td class="form_left_label" style="width: 25%">
                                                    From Date
                                                </td>
                                                <td style="width:1%"><b>:</b></td>
                                                <td class="form_left_text">
                                                   
                                                    <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" Width="20%" />
                                                    <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Leave"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:Image ID="imgCalholidayDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                        ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter  From Date"
                                                        InvalidValueMessage=" From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        
                                                        &nbsp;&nbsp;
                                                        To Date <span style="color: #FF0000">*</span>:
                                                     <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" Width="20%" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToDt"
                                                        Display="None" ErrorMessage="Please Enter  To Date" ValidationGroup="Leave"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:Image ID="imgCalToDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                                        ControlToValidate="txtToDt" EmptyValueMessage="Please Enter  To Date"
                                                        InvalidValueMessage=" To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter  To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        
                                                           <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt" 
CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" 
ValidationGroup="submit" ControlToCompare="txtFromDt" />
                                                       
                                                </td>
                                            </tr>
            <tr align="center">
                
                 <td class="form_left_label" colspan="3">
                     <asp:Button ID="btnShow" runat="server" Text="Show" onclick="btnShow_Click" Font-Bold="true"  ValidationGroup="submit"/>
                     <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="true" OnClick="btnSubmit_Click"
                     Enabled="false" />
                      <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="true" OnClick="btnCancel_Click"/>
                &nbsp;<asp:ValidationSummary ID="ValidationSummury" runat="server" DisplayMode="List"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
              
            </tr>
            
        </table>
    </fieldset>
    <table cellpadding="2" cellspacing="2" style="width: 95%">
        <tr>
            <td>
            <div style="width:100%">
                <asp:Panel ID="pnlList" runat="server"  Width="90%"
                    Visible="false">
                    <asp:ListView ID="lvLeave" runat="server" style="width:100%">
                        <LayoutTemplate>
                            <div id="demo-grid"  class="vista-grid">
                                <div class="titlebar">
                                    Employee List
                                </div>
                                <table class="datatable" cellpadding="0" cellspacing="0"  >
                                    <tr>
                                        <th style="width: 30%;padding-left:10px" align="left">
                                            Name
                                        </th>
                                        <th style="width: 15%;padding-left:10px"" align="left">
                                            CL to Credit
                                        </th>
                                         <th style="width: 20%;padding-left:10px"" align="left">
                                         Date of Joining
                                        </th>
                                         <th style="width: 30%;padding-left:10px"" align="left">
                                         Date of Allocation
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td style="width: 30%">
                                    <%# Eval("NAME")%>
                                </td>
                               <td style="width: 15%" align="left" >
                                    <asp:TextBox ID="txtCLCredit" runat="server" TabIndex="4" Text='<%# Eval("CL")%>' 
                                       ToolTip='<%# Eval("IDNO") %>' Width="80px"/>
                                    <%--<asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" Width="150px" />--%>
                                    <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("idno")%>' />
                                </td>
                                <td style="width: 15%" align="left" >
                                    <asp:TextBox ID="txtDOJ" runat="server" Text='<%# Eval("DOJ")%>' Width="100px" Enabled="False"></asp:TextBox>
                                </td>
                                 <td style="width: 20%" align="left" >
                                    <asp:TextBox ID="txAllocateDate" runat="server" Style="text-align: left" Width="100px" Text='<%# Eval("DOJ")%>'></asp:TextBox>
                                     <asp:Image ID="imgFrmDt" runat="server" ImageUrl="~/images/calendar.png" Width="16px"/>
                   
                                     <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Enabled="true"    
                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFrmDt" TargetControlID="txAllocateDate"/>
                                     
                                        
                                     <ajaxToolKit:MaskedEditExtender ID="meeFrmDt" runat="server" Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus"
                                        MessageValidatorTip="true" MaskType="Date"  AcceptNegative="None" ErrorTooltipEnabled="true" TargetControlID="txAllocateDate"
                                        OnInvalidCssClass="errordate"  ClearMaskOnLostFocus="true" />
                                                                                                                 
                                     <ajaxToolKit:MaskedEditValidator ID="mevFrmDt" runat="server" ControlExtender="meeFrmDt"
                                        ControlToValidate="txAllocateDate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="true"
                                        ErrorMessage="Please Enter Valid Date In format dd/MM/yyyy" EmptyValueBlurredText="*" 
                                        InvalidValueMessage="Please Enter Valid Date In format" Display="None" ValidationGroup="submit" SetFocusOnError="true"/>
                                                            
                                </td>
                   
                                
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                <td style="width: 30%">
                                    <%# Eval("NAME")%>
                                </td>
                                <td style="width: 15%" align="left" >
                                    <asp:TextBox ID="txtCLCredit" runat="server" TabIndex="4" Text='<%# Eval("CL")%>' 
                                       ToolTip='<%# Eval("IDNO") %>' Width="80px"/>
                                    <%--<asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" Width="150px" />--%>
                                    <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("idno")%>' />
                                </td>
                                 <td style="width: 15%" align="left" >
                                    <asp:TextBox ID="txtDOJ" runat="server" Text='<%# Eval("DOJ")%>' Width="100px" Enabled="False"></asp:TextBox>
                                </td>
                                 <td style="width: 20%" align="left" >
                                    <asp:TextBox ID="txAllocateDate" runat="server" Style="text-align: left" Width="100px" Text='<%# Eval("DOJ")%>'></asp:TextBox>
                                     <asp:Image ID="imgFrmDt" runat="server" ImageUrl="~/images/calendar.png" Width="16px"/>
                   
                                     <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Enabled="true"    
                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFrmDt" TargetControlID="txAllocateDate"/>
                                     
                                        
                                     <ajaxToolKit:MaskedEditExtender ID="meeFrmDt" runat="server" Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus"
                                        MessageValidatorTip="true" MaskType="Date"  AcceptNegative="None" ErrorTooltipEnabled="true" TargetControlID="txAllocateDate"
                                        OnInvalidCssClass="errordate"  ClearMaskOnLostFocus="true" />
                                                                                                                 
                                     <ajaxToolKit:MaskedEditValidator ID="mevFrmDt" runat="server" ControlExtender="meeFrmDt"
                                        ControlToValidate="txAllocateDate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="true"
                                        ErrorMessage="Please Enter Valid Date In format dd/MM/yyyy" EmptyValueBlurredText="*" 
                                        InvalidValueMessage="Please Enter Valid Date In format" Display="None" ValidationGroup="submit" SetFocusOnError="true"/>
                                                            
                                </td>
                      
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </asp:Panel>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
               <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="true" OnClick="btnSubmit_Click"
                    ValidationGroup="submit" />
                &nbsp;<asp:ValidationSummary ID="ValidationSummury" runat="server" DisplayMode="List"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />--%>
            </td>
        </tr>
    </table>
</asp:Content>

