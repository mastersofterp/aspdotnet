<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Estb_History_Appointments.aspx.cs" Inherits="Estb_History_Appointments" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   
   
   <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
               <div id="divMsg" runat="server"> </div>
                <tr>
                    <td class="vista_page_title_bar" style="border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;" colspan="6">
                      History For Appointment of Head & Deans
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                            border: solid 1px #D0D0D0;">
                        </div>
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
                    <td style="width: 5%">
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td colspan="4" style="padding-left:120px">
                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                    </td>
                </tr>
            </table>
       <center>
       <%-- <asp:UpdatePanel ID="updAdd" runat="server">
            <ContentTemplate>--%>

                <asp:Panel ID="pnlAdd" runat="server" Width="70%">
                 
                        <fieldset class="fieldset">
                            <legend class="legend">History For Appointment of Head & Deans</legend>                            
                              <table width="100%" cellpadding="1" cellspacing="1" border="0">     
                                 <tr align="center" >
                                   <td style="width: 10%; padding-left:20%" align="left" >
                                  From Date  <span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                    <td style="width: 20%" align="left">
                                        
                                 <asp:TextBox ID="txtStartDt" runat="server" MaxLength="10" Width="13%"  AutoPostBack="true" OnTextChanged="txtStartDt_TextChanged" />
                                                   
                                                    <asp:Image ID="imgCalStartDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="ceStDt" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtStartDt" PopupButtonID="imgCalStartDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeStDt" runat="server" TargetControlID="txtStartDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevStDt" runat="server" ControlExtender="meeStDt"
                                                        ControlToValidate="txtStartDt" EmptyValueMessage="Please Enter From Date"
                                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Holiday" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        
                                                        &nbsp;&nbsp;
                                                        To Date :
                                                     <asp:TextBox ID="txtEndDt" runat="server" MaxLength="10" Width="13%" />
                                                 
                                                    <asp:Image ID="imgCalEndDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="CalExtEndDt" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtEndDt" PopupButtonID="imgCalEndDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MeexEndDt" runat="server" TargetControlID="txtEndDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MEVEndDt" runat="server" ControlExtender="meeStDt"
                                                        ControlToValidate="txtEndDt" EmptyValueMessage="Please Enter To Date"
                                                        InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Holiday" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        
                                                           <asp:CompareValidator ID="CompEndDt" runat="server" ControlToValidate="txtEndDt" 
CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" 
ValidationGroup="History" ControlToCompare="txtStartDt" />  
                                                                     
                                    </td>
                                </tr>
                                   <tr align="center" >
                                   <td style="width: 10%; padding-left:20%" align="left" >
                                 As On Date
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                    <td style="width: 20%" align="left">
                                     <asp:CheckBox id="chkAsOnDate" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkAsOnDate_CheckedChanged"/>
                                       
                                    </td>
                                </tr>                           
                                <tr align="center" >
                                   <td style="width: 10%; padding-left:20%" align="left" >
                                  Appoint Type
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                    <td style="width: 20%" align="left">
                                       <asp:DropDownList ID="ddlType" runat="server" Width="47%">
                                                <asp:ListItem Selected="True" Text="All" Value="A"></asp:ListItem>
                                                <asp:ListItem  Text="Head" Value="H"></asp:ListItem>
                                                <asp:ListItem  Text="Dean" Value="D"></asp:ListItem>
                                            </asp:DropDownList>
                                       
                                    </td>
                                </tr> 
                                <tr>
                                    <td align="center" style="width: 499px">
                                        &nbsp
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="center" style="width: 499px" colspan="3">
                                          <asp:Button id="btnReportHistory" runat="server" Text="History Report"  OnClick="btnReportHistory_Click" ValidationGroup="History"/>
                                 
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />
                                        &nbsp;   
                                     <%--   <asp:Label ID="lblMsg" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>     --%>                
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="History" />
                                    </td>
                                </tr>
                                </table>
                        </fieldset>                  
                </asp:Panel>                
                         
            
      </center>
     
        <%--  Enable the button so it can be played again --%>   
    
            <script type="text/javascript">
                    ;debugger
                function ChangeAttendance(BtnStatus) {
                    //var st = vall.id.split("lvInfo_ctrl");
                    //var i = st[1].split("_BtnStatus");
                    var btnSelect = document.getElementById(BtnStatus);                 
                    if (btnSelect.value == 'P') {
                        var mySplitResult = BtnStatus.split("_");                        
                        document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_HiddenField1_' + mySplitResult[3]).value = '2';
                        btnSelect.value = 'A';   
                        btnSelect.setAttribute('data-value', '2');
                        btnSelect.style.backgroundColor = 'red';
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_ctrl' + index + '_lblstatus').value = 'A';
                        return false;
                    }
                    else if (btnSelect.value == 'A') {
                        btnSelect.value = 'L';
                        var mySplitResult = BtnStatus.split("_");
                        document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_HiddenField1_' + mySplitResult[3]).value = '3';
                       
                                               
                        btnSelect.setAttribute('data-value', '3');
                        btnSelect.style.backgroundColor = 'Orange';
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_ctrl' + index + '_lblstatus').value = 'L';
                        return false;
                    }                  
                    else if (btnSelect.value == 'L') {
                        btnSelect.value = 'P';
                        var mySplitResult = BtnStatus.split("_");
                        ctl00_ContentPlaceHolder1_lvInfo_ctrl0_btnStatus
                        document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_HiddenField1_' + mySplitResult[3]).value = '1';
                        btnSelect.style.backgroundColor = 'Green';
                        btnSelect.setAttribute('data-value', '1');
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_ctrl' + index + '_lblstatus').value = 'P';
                        return false;
                    }
                    else {
                        alert('ABC');
                        return false;
                    }
                }
                function ChangeAttendance_new(BtnStatus) {

                    var btnSelect = document.getElementById(BtnStatus);
                    if (btnSelect.value == 'P') {
                        var mySplitResult = BtnStatus.split("_");                      
                        document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_HiddenField1_' + BtnStatus).value = '2';
                        btnSelect.value = 'A';
                        btnSelect.setAttribute('data-value', '2');
                        btnSelect.style.backgroundColor = 'red';
                        return false;
                    }                 
                    else if (btnSelect.value == 'A') {
                        btnSelect.value = 'L';
                        var mySplitResult = BtnStatus.split("_");
                        document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_HiddenField1_' + BtnStatus).value = '3';
                        btnSelect.setAttribute('data-value', '3');
                        btnSelect.style.backgroundColor = 'Orange';
                        return false;
                    }
                    else if (btnSelect.value == 'L') {
                        btnSelect.value = 'P';
                        var mySplitResult = BtnStatus.split("_");
                        document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_HiddenField1_' + BtnStatus).value = '1';
                        btnSelect.setAttribute('data-value', '1');
                        btnSelect.style.backgroundColor = 'Green';
                        return false;
                    }
                    else {
                        alert('ABC');
                        return false;
                    }
                }
            </script>
            <script type="text/javascript">
                //  keeps track of the delete button for the row
                //  that is going to be removed
                var _source;
                // keep track of the popup div
                var _popup;

                function showConfirmDel(source) {
                    this._source = source;
                    this._popup = $find('mdlPopupDel');

                    //  find the confirm ModalPopup and show it    
                    this._popup.show();
                }

                function okDelClick() {
                    //  find the confirm ModalPopup and hide it    
                    this._popup.hide();
                    //  use the cached button as the postback source
                    __doPostBack(this._source.name, '');
                }

                function cancelDelClick() {
                    //  find the confirm ModalPopup and hide it 
                    this._popup.hide();
                    //  clear the event source
                    this._source = null;
                    this._popup = null;
                }
                function CheckNumeric(event, obj) {
                    var k = (window.event) ? event.keyCode : event.which;
                    //alert(k);
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                        obj.style.backgroundColor = "White";
                        return true;
                    }
                    if (k > 45 && k < 58) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter numeric Value');
                        obj.focus();
                    }
                    return false;
                }
              
                function CheckAlphabet(event, obj) {

                    var k = (window.event) ? event.keyCode : event.which;
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                       // alert('Please Enter Alphabets Only!');
                        obj.focus();
                    }
                    return false;
                }
                function changeAllToUpper() {


                    var inputs = document.getElementsByTagName('input');    // get all the text boxes
                    for (var k = 0; k < inputs.length; k++) {
                        var input = inputs[k]
                        if (input.type != 'text') continue;
                        input.value = input.value.toUpperCase();  // make values uppercase
                    }
                }

                function check(me) {
                    var inputs = document.getElementsByTagName('input');    // get all the text boxes
                    for (var k = 0; k < inputs.length; k++) {
                        var input = inputs[k]
                        if (input.type != 'text') continue;
                        input.value = input.value.toUpperCase();  // make values uppercase
                    }

                    if (document.getElementById("" + me.id + "").value.toUpperCase() != "Y" && document.getElementById("" + me.id + "").value.toUpperCase() != "A" && document.getElementById("" + me.id + "").value.toUpperCase() != "L") {
                        alert("Please Enter P Or A Or L ");
                        document.getElementById("" + me.id + "").value = "";
                        document.getElementById("" + me.id + "").focus();
                    }

                }
                function checkDate(sender, args) {

                    var dateToCompare = sender._selectedDate;

                    var currentDate = new Date();

                    if (dateToCompare >= currentDate) {

                        alert("You cannot select a day future than today!");

                        document.getElementById("<%= txtStartDt.ClientID %>").value = "";
                return false;
            }

        }
            </script>
    </asp:Content>


