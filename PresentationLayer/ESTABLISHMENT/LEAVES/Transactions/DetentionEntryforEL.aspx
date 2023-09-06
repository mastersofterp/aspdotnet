<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
 CodeFile="DetentionEntryforEL.aspx.cs" 
 Inherits="ESTABLISHMENT_LEAVES_Transactions_DetentionEntryforEL" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
   <tr>
     <td class="vista_page_title_bar" valign="top" style="height : 30px" >
      DETENTION ENTRY FOR EL
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
        <tr>
            <td style="padding-left:20px">
                Note : <span style="color:#FF0000">* Marked Is Mandatory!</span>
            </td>
        </tr>
        </table>
       <br />
       <table width="100%">
       <tr>
       <td>
                <asp:Panel ID="pnlSelect" runat="server" Style="padding-left: 10px;" Width="95%">
              
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Staff</legend>
                        <br />
                        <table cellpadding="1" cellspacing="1" style="width: 100%;">
                                                      
                               <tr>
                                <td  style="width: 5%" align="left">
                                    Department<span style="color: #FF0000">*</span> 
                                    </td>
                                    <td style="width:1%"><b>:</b></td>
                                <td  style="width: 15%" align="left">
                                <asp:DropDownList ID="ddlDept" AppendDataBoundItems="true" runat="server" 
                                        Width="300px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlDept_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                   
                              
                                </td>
                                </tr>
                                 <tr>
                                <td  style="width: 5%" align="left">
                                    StaffType<span style="color: #FF0000">*</span> 
                                    </td>
                                    <td style="width:1%"><b>:</b></td>
                                <td  style="width: 15%" align="left">
                                <asp:DropDownList ID="ddlStaffType" AppendDataBoundItems="true" runat="server" 
                                        Width="300px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlStaffType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStaffType" runat="server" ControlToValidate="ddlStaffType"
                                        Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                   
                              
                                </td>
                                </tr>
                            <tr>
                                <td  style="width: 5%" align="left">
                                    Year<span style="color: #FF0000">*</span> 
                                    </td>
                                    <td style="width:1%"><b>:</b></td>
                                <td  style="width: 15%" align="left">
                                <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" 
                                        Width="300px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlYear_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                                        Display="None" ErrorMessage="Please Select Year" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                   
                              
                                </td>
                                </tr>
                                <tr>
                                <td colspan="3" align="center">
                                      <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" Width="80px"
                                        OnClick="btnShow_Click" />                                          
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                            </tr>
                           </table>
                           </fieldset>
                </asp:Panel>
                 <asp:Panel ID="Panel1" runat="server" Style="padding-left: 10px;" Width="95%">
                          <table cellpadding="1" cellspacing="1" style="width: 100%;">
                        <tr>
                            <td >
                                <asp:ListView ID="lvInfo" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <center>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <div class="titlebar">
                                                EL-Detention Entries</div>
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr class="header">
                                                      <th width="6%">
                                                            RFID NO
                                                        </th>
                                                        <th width="20%">
                                                            Name
                                                        </th>
                                                        <th width="10%" >
                                                            Designation
                                                        </th>
                                                        <th width="15%" >
                                                            Department
                                                        </th> 
                                                         <th width="6%">
                                                         Year
                                                        </th>                                                     
                                                        <th width="6%">
                                                            Working Days
                                                        </th>                                                       
                                                        <th width="6%">
                                                            Total EL
                                                        </th>
                                                        <th width="20%">
                                                           Reason
                                                        </th>
                                                    </tr>
                                                    <thead>
                                            </table>
                                        </div>
                                        <div class="listview-container">
                                            <div id="Div1" class="vista-grid">
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
                                             <td width="6%">
                                                <%#Eval("RFIDNO")%>
                                            </td>
                                            <td width="20%">
                                                <%#Eval("NAME")%>
                                            </td>
                                            <td width="10%">
                                                <%#Eval("subdesig")%>
                                            </td>
                                            <td width="15%">
                                                <%#Eval("subdept")%>
                                            </td>                                                                                    
                                           
                                            <td width="6%">
                                            <asp:Label ID="lblYear" runat="server" Text=' <%#Eval("Year")%>'
                                                     ToolTip='<%#Eval("Year")%>' Width="25px">
                                                </asp:Label>                                             
                                            </td>
                                          
                                            <td width="6%">
                                                <asp:TextBox ID="txtDays" runat="server" MaxLength="2" Text='<%#Eval("NO_OF_DAYS")%>'
                                                    ToolTip='<%#Eval("IDNO")%>' Width="50px"  onblur="return ElCalculation(this);" TabIndex="1" />
                                            </td>
                                          
                                         
                                          
                                            <td width="6%">
                                              
                                                <asp:TextBox ID="txtTotalEL" runat="server" MaxLength="2" Text='<%#Eval("TOTAL_EL")%>'
                                                   ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                                
                                            </td>
                                             <td width="20%">
                                              
                                                <asp:TextBox ID="txtreason" runat="server"  Text='<%#Eval("REASON")%>'
                                                   ToolTip='<%#Eval("IDNO")%>' Width="100px" />
                                                
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                          <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                             <td width="6%">
                                                <%#Eval("RFIDNO")%>
                                            </td>
                                            <td width="20%">
                                                <%#Eval("NAME")%>
                                            </td>
                                            <td width="10%">
                                                <%#Eval("subdesig")%>
                                            </td>
                                            <td width="15%">
                                                <%#Eval("subdept")%>
                                            </td>                                                                                    
                                           
                                            <td width="6%">
                                            <asp:Label ID="lblYear" runat="server" Text=' <%#Eval("Year")%>'
                                                     ToolTip='<%#Eval("Year")%>' Width="25px">
                                                </asp:Label>                                             
                                            </td>
                                          
                                            <td width="6%">
                                                <asp:TextBox ID="txtDays" runat="server" MaxLength="2" Text='<%#Eval("NO_OF_DAYS")%>'
                                                    ToolTip='<%#Eval("IDNO")%>' Width="50px"  onblur="return ElCalculation(this);" TabIndex="1" />
                                            </td>
                                          
                                         
                                          
                                            <td width="6%">
                                              
                                                <asp:TextBox ID="txtTotalEL" runat="server" MaxLength="2" Text='<%#Eval("TOTAL_EL")%>'
                                                   ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                                
                                            </td>
                                             <td width="20%">
                                              
                                                <asp:TextBox ID="txtreason" runat="server"  Text='<%#Eval("REASON")%>'
                                                   ToolTip='<%#Eval("IDNO")%>' Width="100px" />
                                                
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                        <tr>
                           <td>
                           </td>
                           
                        </tr>
                        <tr>
                           <td align="center">
                               <br />
                               <asp:Button ID="btnSave" runat="server" Text="Save" Visible="False" 
                                   onclick="btnSave_Click" Width="100px"/>
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="False" 
                                   onclick="btnCancel_Click" Width="100px" />
                                     <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                           </td>
                          
                        </tr>    
                        </table>
                        </asp:Panel>
           
           </td>
            </tr>           
       </table>
        

   <div id="divMsg" runat="server">
    </div>

  <script type="text/javascript" language="javascript">
      ;debugger
      function ElCalculation(vall) {

        
          var st = vall.id.split("lvInfo_ctrl");
          var i = st[1].split("_txtDays");         
          var index = i[0];
          var workingdays = document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_ctrl' + index + '_txtDays').value;
          var totaldaysEL =  (workingdays);
          var totalEL = (totaldaysEL / 2.5);
          document.getElementById('ctl00_ContentPlaceHolder1_lvInfo_ctrl' + index + '_txtTotalEL').value = totalEL.toFixed(0);
          
          //document.getElementById('ctl00_ContentPlaceHolder1_Tabs_TabPanel2_lvIncrement_ctl' + index + '_txtTotal').value = totamount.toFixed(2);
      }

      function checkAllEmployees(chkcomplaint) {
          var frm = document.forms[0];
          for (i = 0; i < document.forms[0].elements.length; i++) {
              var e = frm.elements[i];
              if (e.type == 'checkbox') {
                  if (chkcomplaint.checked == true)
                      e.checked = true;
                  else
                      e.checked = false;
              }
          }
      }


      function roundTens(val) {
          var x = val;
          for (var i = 0; i < 9; i++) {
              if (x % 10 == 0) {
                  break;
              }
              else {
                  x = Number(x) + 1;
              }
          }
          return x;
      }
      
       
    </script>
  
</asp:Content>

