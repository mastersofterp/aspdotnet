<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_SalaryDepositeOB.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_SalaryDepositeOB" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        SALARY DEPOSIT OB ENTRY &nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
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
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px">
                        <asp:Panel ID="pnlSelect" runat="server" Style="padding-left: 10px;" Width="90%">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Select College</legend>
                                <br />
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                 <tr>
                                <td class="form_left_label" style="padding-left: 10px; width: 15%">
                                    College :<span style="color:Red">*</span>
                                </td>
                                <td class="form_left_text">
                                     <asp:DropDownList ID="ddlCollege" runat="server" Width="300px" 
                                         AppendDataBoundItems="true" AutoPostBack="true"
                                            TabIndex="19" onselectedindexchanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="payroll" 
                                        ErrorMessage="Please select College" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                    
                                </td>
                            </tr>
                                                                    
                                    <tr>
                                        <td class="form_left_label" width="10%">
                                            &nbsp; Staff :<span style="color:Red">*</span>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true" onselectedindexchanged="ddlStaff_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                <td class="form_left_label" style="padding-left: 10px; width: 15%">
                                    &nbsp;OB Date :<span style="color:Red">*</span>
                                </td>
                                <td class="form_left_text">
                                    <asp:TextBox ID="txtOBDate" runat="server" Width="80px" Enabled="true" ValidationGroup="emp"
                                    TabIndex="13" />
                                <asp:Image ID="imgCalOBDate" runat="server" ImageUrl="~/images/calendar.png"
                                    Style="cursor: pointer" TabIndex="14" />
                                <ajaxToolKit:CalendarExtender ID="ceOBDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtOBDate" PopupButtonID="imgCalOBDate" Enabled="true"
                                    EnableViewState="true">
                                </ajaxToolKit:CalendarExtender>
                                
                                <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtOBDate"
                        Display="None" ErrorMessage="Please enter OB Date" SetFocusOnError="true"
                        ValidationGroup="payroll" />
                                <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtOBDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeBirthDate"
                                    ControlToValidate="txtOBDate" EmptyValueMessage="Please Enter Birth Date"
                                    InvalidValueMessage="OB Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                    TooltipMessage="Please Enter Opening Balance Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                    ValidationGroup="emp" SetFocusOnError="True" />
                     
                                </td>
                            </tr>
                            
                            <tr>
                                        <td class="form_left_label" width="10%">
                                          
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Button ID="btnShow" runat="server" Text="Show" onclick="btnShow_Click" ValidationGroup="payroll"/>
                                              <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                
                <tr>
                    <td style="padding-left: 10px">
                        <asp:Panel ID="pnlAttendance" runat="server" Style="padding-left: 10px;" Width="90%">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:ListView ID="lvAttendance" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <center>
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        Enter Salary Deposit Amount
                                                        
                                                        
                                                        </div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr class="header">
                                                                <th width="5%">
                                                                    Idno
                                                                </th>
                                                                <th width="25%">
                                                                    Name
                                                                </th>
                                                                <th width="20%">
                                                                    Designation
                                                                </th>
                                                                <th width="20%">
                                                                    OB Date
                                                                </th>
                                                                <th width="15%">
                                                                    OB Amount
                                                                </th>
                                                                
                                                            </tr>
                                                            <thead>
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
                                                    <td width="5%">
                                                        <%#Eval("IDNO")%>
                                                    </td>
                                                    <td width="25%">
                                                        <%#Eval("EMPNAME")%>
                                                    </td>
                                                    <td width="20%">
                                                        <%#Eval("SUBDESIG")%>
                                                    </td>
                                                     <td>
                                                     <%#Eval("OB_DATE")%>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtDays" runat="server" MaxLength="8" Text='<%#Eval("SEPOSIT_AMT")%>' 
                                                            ToolTip='<%#Eval("IDNO")%>' Width="50px" />   <%--onkeyup="return check(this);"--%>
                                                      <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="txtDays"
                                                            Display="None" ErrorMessage="Please Enter OB Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvDays" runat="server" ControlToValidate="txtDays" Display="None"
                                                            ErrorMessage="Please Enter Numeric Value" SetFocusOnError="true" ValidationGroup="payroll"
                                                            Operator="DataTypeCheck" Type="Double">  
                                                        </asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td width="5%">
                                                        <%#Eval("IDNO")%>
                                                    </td>
                                                    <td width="25%">
                                                        <%#Eval("EMPNAME")%>
                                                    </td>
                                                    <td width="20%">
                                                        <%#Eval("SUBDESIG")%>
                                                    </td>
                                                    <td>
                                                     <%#Eval("OB_DATE")%>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtDays" runat="server" MaxLength="8" Text='<%#Eval("SEPOSIT_AMT")%>'
                                                            ToolTip='<%#Eval("IDNO")%>' Width="50px" />    <%--onkeyup="return check(this);"--%>
                                                        <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="txtDays"
                                                            Display="None" ErrorMessage="Please Enter OB Amt" ValidationGroup="payroll" SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvDays" runat="server" ControlToValidate="txtDays" Display="None"
                                                            ErrorMessage="Please Enter Numeric Value" SetFocusOnError="true" ValidationGroup="payroll"
                                                            Operator="DataTypeCheck" Type="Double">  
                                                        </asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" Width="80px"
                                            OnClick="btnSub_Click" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" Width="80px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function check(me)        
        {  
           if (ValidateNumeric(me)==true)
           { 
                var myArr = new Array();
                var myArrdays = new Array();
                myString = ""+me.id+"";
                myArr = myString.split("_");
                var index= myArr[3].substring(4,myArr[3].length);
                var  Attenddays= document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl"+index+"_txtDays");
                var Attend_days= Attenddays.value;
                myArrdays  = Attend_days.split(".");
                
                if(!(Attend_days > 31))
                {
                    if(myArrdays[1]>0)
                    {
                        if(myArrdays[1] > 5 || myArrdays[1] < 5)
                        {               
                           alert("Please enter 5 only after decimal");
                           document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl"+index+"_txtDays").value="";
                           document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl"+index+"_txtDays").focus();
                        }
                    }
                }
                else
                {
                   alert("Please enter days less than 32");
                   me.value = "";
                   me.focus();                   
                }
            }
        }
      
                
        function ValidateNumeric(txt)
        {   
        
          
            if (isNaN(txt.value))
            {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters alloewd");
                return false;
            }
            else
            {
                return true;
            }
        }
        
    </script>

</asp:Content>
