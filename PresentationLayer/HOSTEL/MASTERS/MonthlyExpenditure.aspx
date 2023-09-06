<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MonthlyExpenditure.aspx.cs" Inherits="HOSTEL_MASTERS_MonthlyExpenditure"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>--%>
        
        <script language="javascript" type="text/javascript">

    function Calculate(me) {
        var count = 0.00;
        try {

            for (i = 0; i <= 39; i++) {

                count = Number(count) + Number(document.getElementById("ctl00_ContentPlaceHolder1_Lsv1_ctrl" + i + "_txtshead").value);

            }

            document.getElementById("ctl00_ContentPlaceHolder1_Lsv1_txttotal").value = count;
            //document.getElementById("ctl00_ctp_lblamt").value = count;
            document.getElementById("ctl00_ContentPlaceHolder1_hdnTotal").value = count;

        }
        catch (e) {
            document.getElementById("ctl00_ContentPlaceHolder1_Lsv1_txttotal").value = count;
            //document.getElementById("ctl00_ctp_lblamt").value = count;
            document.getElementById("ctl00_ContentPlaceHolder1_hdnTotal").value = count;
        }
    }

  </script>
  <script type="text/javascript" language="javascript">
        function checkForSecondDecimal(sender, e) {
            formatBox = document.getElementById(sender.id);
            strLen = sender.value.length;
            strVal = sender.value;
            hasDec = false;
            e = (e) ? e : (window.event) ? event : null;


            if (e) {
                var charCode = (e.charCode) ? e.charCode :
                            ((e.keyCode) ? e.keyCode :
                            ((e.which) ? e.which : 0));


                if ((charCode == 46) || (charCode == 110) || (charCode == 190)) {
                    for (var i = 0; i < strLen; i++) {
                        hasDec = (strVal.charAt(i) == '.');
                        if (hasDec)
                            return false;
                    }
                }
            }
            return true;
        }

    </script>
    
    <script type="text/javascript">
     function update(obj) {

         try {
             var mvar = obj.split('¤');
             document.getElementById(mvar[1]).value = mvar[0];
             document.getElementById('ctl00_ContentPlaceHolder1_Hdn1').value = mvar[0] + "  ";
             setTimeout('__doPostBack(\'' + mvar[1] + '\',\'\')', 0);
         }
         catch (e) {
             alert(e);
         }
     }

function onCalendarShown()
  {
 var cal = $find("calendar1");

         //Setting the default mode to month

         cal._switchMode("months", true);



         //Iterate every month Item and attach click event to it

         if (cal._monthsBody) {

             for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                 var row = cal._monthsBody.rows[i];

                 for (var j = 0; j < row.cells.length; j++) {

                     Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);

                 }

             }

         }

     }



     function onCalendarHidden() {

         var cal = $find("calendar1");

         //Iterate every month Item and remove click event from it

         if (cal._monthsBody) {

             for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                 var row = cal._monthsBody.rows[i];

                 for (var j = 0; j < row.cells.length; j++) {

                     Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);

                 }

             }

         }



     }


     function call(eventElement) {

         var target = eventElement.target;

         switch (target.mode) {

             case "month":

                 var cal = $find("calendar1");

                 cal._visibleDate = target.date;

                 cal.set_selectedDate(target.date);

                 cal._switchMonth(target.date);

                 cal._blur.post(true);

                 cal.raiseDateSelectionChanged();

                 break;

         }

     }



     
  </script>
  
  <style type="text/css">
        .style2
        {
            height: 519px;
        }
        .style3
        {
            width: 35%;
        }
    </style>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        MESS MONTHLY EXPENDITURE&nbsp;
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
               <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpanel">
                    <ProgressTemplate>
                        <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                            style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none;
                            background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                            <table style="width: 100%; height: 100%">
                                <tr>
                                    <td valign="middle" align="center" class="style2">
                                        <img alt="PresentationLayer\images" src="../images/anim_loading_75x75.gif" /><br />
                                        <%--Progressing Request....  
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
                    <tr>
                        <td colspan="6">
                            <!-- "Wire frame" div used to transition from the button to the info panel -->
                            <!-- Info panel to be displayed as a flyout when the button is clicked -->
                            <div id="Div2" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                                font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                <div id="Div3" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return false;" Text="X"
                                        ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                        font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                </div>
                                <div>
                                    <p class="page_help_head">
                                        <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                        <%--  Shrink the info panel out of view --%>
                                    </p>
                                    <p class="page_help_text">
                                        <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" /></p>
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
                            <ScriptAction Script="Cover($get('ctl00$ctp$btnHelp'), $get('flyout'));" />
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
                            <ajaxToolKit:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="btnClose">
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
                        <td colspan="3">
                            Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 1%">
                            &nbsp;
                        </td>
                        <td colspan="3">
                            <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                Visible="false" Width="60%">
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../images/error.gif" align="absmiddle" alt="Error" />
                                        </td>
                                        <td style="width: 97%">
                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                            </font>
                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px;
                                                color: #CD0A0A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                Visible="false" Width="60%">
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../images/confirm.gif" align="absmiddle" alt="confirm" />
                                        </td>
                                        <td style="width: 97%">
                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana;
                                                font-size: 11px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td style="width: 63%; vertical-align: top;">
                            <table id="Table1" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
                                <tr>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">Session <span style="color: #FF0000">*</span>
                                    </td>
                                     <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 20%">
                                         <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" 
                                            TabIndex="1" Width="92%" ValidationGroup="Submit" ToolTip="Please Select Session"
                                            Font-Names="Verdana">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" ControlToValidate="ddlSession" runat="server" ErrorMessage="Please Select Session" Display="None" InitialValue="0" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">Hostel <span style="color: #FF0000">*</span>
                                    </td>
                                     <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 20%">
                                         <asp:DropDownList ID="ddlHostel" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            TabIndex="1" Width="92%" ValidationGroup="Submit" ToolTip="Please Select Hostel"
                                            Font-Names="Verdana" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvHostel" ControlToValidate="ddlHostel" runat="server" ErrorMessage="Please Select Hostel" Display="None" InitialValue="0" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                <tr id="Tr1" runat="server">
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">
                                        Mess <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlMess" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            TabIndex="1" Width="92%" ValidationGroup="Submit" ToolTip="Please Select Mess"
                                            Font-Names="Verdana" OnSelectedIndexChanged="ddlMess_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMess" ControlToValidate="ddlMess" runat="server" ErrorMessage="Please Select Mess" Display="None" InitialValue="0" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server">
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">
                                        Month <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtDate" runat="server" Style="text-align: right" TabIndex="2" ToolTip="Please enter date"
                                            Width="40%"></asp:TextBox>
                                        <asp:Image ID="imgCal_dt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="ceAllotdt" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgCal_dt" PopupPosition="BottomLeft" TargetControlID="txtDate"
                                            OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" BehaviorID="calendar1">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meAllotdt" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MevDate" runat="server" ControlExtender="meAllotdt"
                                            ControlToValidate="txtDate" EmptyValueMessage="Please Enter Alloted Date." InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Date." EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="true">
                                        </ajaxToolKit:MaskedEditValidator>
                                        <asp:RequiredFieldValidator ID="rfvAllotdt" runat="server" ControlToValidate="txtDate"
                                            Display="None" ErrorMessage="Please Enter Date." SetFocusOnError="true" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                        
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server">
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">
                                    </td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server">
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">
                                    </td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Save" Width="70px" OnClick="btnSubmit_Click"
                                            Style="font-family: Verdana; font-size: 11px; width: 70; padding: 2px" ValidationGroup="submit"
                                            TabIndex="3" ToolTip="Click to Save" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" OnClick="btnCancel_Click"
                                            Style="font-family: Verdana; font-size: 11px; width: 70; padding: 2px" CausesValidation="False"
                                            TabIndex="4" ToolTip="Click to Cancel" />
                                        <asp:Button ID="btnPrint" runat="server" CausesValidation="False" OnClick="btnPrint_Click"
                                            Style="font-family: Verdana; font-size: 11px; width: 70; padding: 2px" Width="70px"
                                            Text="Print" TabIndex="5" ToolTip="Click to Print" Visible="false" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 1%">
                                        &nbsp;
                                    </td>
                                    <td colspan="3" align="center">
                                        <table width="90%">
                                            <tr>
                                                <td style="width: 100%; vertical-align: top; height: 60%">
                                                    <asp:ListView ID="Lsv1" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="Div3" class="vista-grid">
                                                                <div class="titlebar">
                                                                    Mess Expenditure
                                                                </div>
                                                                <table cellpadding="1" cellspacing="1" runat="server" id="tblAmt" class="datatable"
                                                                    width="99%">
                                                                    <tr class="header">
                                                                        <th style="width: 40%; text-align: left; font-family: Verdana; font-size: 11px">
                                                                            Total Amount
                                                                        </th>
                                                                        <th style="width: 10%; text-align: right">
                                                                            <asp:Label ID="lblRs" runat="server" Style="text-align: right" Font-Names="Rupee Foradian"
                                                                                Font-Size="Large" Text="`"></asp:Label>
                                                                        </th>
                                                                        <th style="width: 23%; text-align: right">
                                                                            <span style="color: #FF0000">
                                                                                <asp:TextBox Font-Bold="true" BackColor="LightBlue" BorderColor="LightBlue" ForeColor="Brown"
                                                                                    ID="txttotal" Style="text-align: right;" ReadOnly="true" Width="75%" runat="server"></asp:TextBox>
                                                                            </span>
                                                                        </th>
                                                                    </tr>
                                                                </table>
                                                                <table cellpadding="1" cellspacing="1" runat="server" id="Table1" class="datatable"
                                                                    width="99%">
                                                                    <tr class="header">
                                                                        <th style="width: 50%; text-align: left; font-family: Verdana; font-size: 11px">
                                                                            Heads
                                                                        </th>
                                                                        <th style="width: 25%; text-align: right; font-family: Verdana; font-size: 11px">
                                                                            Amount
                                                                        </th>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="listview-container" >
                                                                <div id="demo-grid" class="vista-grid">
                                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 99%;">
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                <td style="width: 2%; text-align: left; display: none;">
                                                                    <asp:Label ID="lblshead" runat="server" Text='<%# Eval("FEE_HEAD") %>' />
                                                                </td>
                                                                <td style="width: 3%; text-align: left">
                                                                    <asp:Label ID="lblhead" runat="server" Text='<%# Eval("FEE_LONGNAME") %>' />
                                                                </td>
                                                                <td style="width: 5%; text-align: right">
                                                                    <asp:TextBox ID="txtshead" runat="server" onkeyup="return Calculate(this);" Style="text-align: right"
                                                                        MaxLength="7" Width="42%" onkeypress="return checkForSecondDecimal(this,event)" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTBE1" runat="server" TargetControlID="txtshead"
                                                                        FilterType="Custom, Numbers" ValidChars="." />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                <td style="width: 2%; text-align: left; display: none;">
                                                                    <asp:Label ID="lblshead" runat="server" Text='<%# Eval("FEE_HEAD") %>' />
                                                                </td>
                                                                <td style="width: 3%; text-align: left">
                                                                    <asp:Label ID="lblhead" runat="server" Text='<%# Eval("FEE_LONGNAME") %>' />
                                                                </td>
                                                                <td style="width: 5%; text-align: right">
                                                                    <asp:TextBox ID="txtshead" onkeyup="return Calculate(this);" Style="text-align: right"
                                                                        runat="server" Width="42%" MaxLength="7" onkeypress="return checkForSecondDecimal(this,event)" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTBE1" runat="server" TargetControlID="txtshead"
                                                                        FilterType="Custom, Numbers" ValidChars="." />
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Row5" runat="server">
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">
                                    </td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                                <tr id="row4" runat="server">
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td style="width: 7%">
                                        <asp:HiddenField ID="hdnval" runat="server" OnValueChanged="hdnval_ValueChanged" />
                                    </td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 20%">
                                        <asp:HiddenField ID="hdnTotal" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top" class="style3">
                            <table>
                                <tr >
                                    <%--<td style="width: 1%">
                                </td>--%>
                                    <td style="width: 20%">
                                        
                                    </td>
                                    <td style="width: 2%">
                                        <b>&nbsp;</b>
                                    </td>
                                    <td style="width: 50%">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 50%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" height="35px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:ListView ID="Lsv2" runat="server">
                                                <LayoutTemplate>
                                                    <div id="div4" class="vista-grid">
                                                        <div class="titlebar">
                                                            <asp:Label ID="lblMasterCode" runat="server"></asp:Label>
                                                        </div>
                                                        <table cellpadding="1" cellspacing="1" class="datatable" width="60%">
                                                            <tr class="header">
                                                                <th style="font-family: Verdana; width: 1%; text-align: left; font-size: 11px">
                                                                    Edit
                                                                </th>
                                                                <th style="font-family: Verdana; width: 8%; text-align: left; font-size: 11px">
                                                                    Mess
                                                                </th>
                                                                <th style="font-family: Verdana; width: 5%; text-align: left; font-size: 11px">
                                                                    Month
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="listview-container">
                                                        <div id="demo-grid" class="vista-grid">
                                                            <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                        <td style="width: 2%; text-align: left; font-family: Verdana">
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("TRNO")%>'
                                                                ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                        </td>
                                                        <td style="width: 10%; text-align: left; font-family: Verdana; font-size: 11px">
                                                            <asp:Label ID="lblmess" runat="server" Style="width: 100%" Text='<%# Eval("MESS_NAME") %>' />
                                                        </td>
                                                        <td style="width: 5%; text-align: left; font-family: Verdana; font-size: 11px">
                                                            <asp:Label ID="lblMonth" runat="server" Style="width: 100%" Text='<%# Eval("MONTH_YR") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                        <td style="width: 2%; text-align: left; font-family: Verdana">
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("TRNO")%>'
                                                                ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                        </td>
                                                        <td style="width: 7%; text-align: left; font-family: Verdana; font-size: 11px">
                                                            <asp:Label ID="lblmess" runat="server" Style="width: 100%" Text='<%# Eval("MESS_NAME") %>' />
                                                        </td>
                                                        <td style="width: 5%; text-align: left; font-family: Verdana; font-size: 11px">
                                                            <asp:Label ID="lblMonth" runat="server" Style="width: 100%" Text='<%# Eval("MONTH_YR") %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td style="width:15%"></td>--%>
                    </tr>
                </table>
            </table>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>
   
</asp:Content>
