<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="commercialQuarter_allotment.aspx.cs" 
    Inherits="ESTATE_Transaction_commercialQuarter_allotment" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table cellpadding="2" cellspacing="2" border="0" style="width: 100%">
        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px" colspan="2">
               Commercial Quarter Allotment
   
               <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" 
                    AlternateText="Page Help" ToolTip="Page Help" Height="16px" 
                    style="width: 16px" />
            </td>
        </tr>
          <tr>
             <td>
                  Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
             </td>

         </tr>
       
         <tr>
             <td>
             </td>
        <td valign ="top" style="height: 117px; width: 1128px;"> 
        <fieldset class="fieldset">
        
        
        <table cellpadding="2" cellspacing="0"  width="100%">
        
      
          <tr>
            <td style="width:10%; height: 25px;">
            </td>
            <td style="width: 13%; text-align:left; height: 25px;">Name
            </td>
            <td style="width:1%; height: 25px;"> <center style="height: 16px; width: 14px"><b>:</b></center>
            </td>
            <td style="width:33%; height: 25px;">
           <asp:DropDownList ID="ddlComName" runat="server" AppendDataBoundItems="true" Width="400px">
           </asp:DropDownList>
            </td> 
            <td>
            </td>  
            <td style="height: 25px;">
            <asp:Button ID="btnNew" runat="server" Text="New"  Width="90px"/>
            </td>    
            </tr>
            
           
             
           </table>
            </fieldset> 
           
           
           
           <fieldset>
           
           
           
           <table>
           
           <tr>
               <td style="width: 107px">
               </td>
           <td style="width: 150px">
           Quarter Type 
                      
           </td>
               <td style="width: 4px">
               :
               </td>
               <td style="width: 323px">
           <asp:DropDownList ID="ddlComQuartertype" runat="server" AppendDataBoundItems="true" Width="150px">
           </asp:DropDownList>
           
               </td>
               <td style="width: 150px">
               Quarter No.
               </td>
               <td>
               :
               </td>
               <td style="width: 146px">
               <asp:DropDownList ID="ddlComQuarterno" runat="server" AppendDataBoundItems="true" Width="150px">
           </asp:DropDownList>
               </td>
           </tr>
           <tr>
           <td style="width: 107px">
           </td>
           
               <td>
                   &nbsp;</td>
               <td style="width: 4px">
                   &nbsp;</td>
               <td style="width: 323px">
                   &nbsp;</td>
               <td>
                   Water Meter No.</td>
               <td>
                   :</td>
               <td style="width: 146px">
                   <asp:DropDownList ID="ddlComWatermeterno" runat="server" 
                       AppendDataBoundItems="true" Width="150px">
                   </asp:DropDownList>
               </td>
           
           </tr>
           <tr>
           <td>
           
           </td>
               <td>
                   Energy Meter Type</td>
               <td style="width: 4px">
                   :</td>
               <td style="width: 323px">
                   <asp:DropDownList ID="ddlComEmetertype" runat="server" AppendDataBoundItems="true" 
                       Width="150px">
                   </asp:DropDownList>
               </td>
               <td>
                   Meter No.</td>
                                        <td>
                                            :</td>
               <td>
                   <asp:DropDownList ID="ddlComMeterno" runat="server" 
                       AppendDataBoundItems="true" Width="150px">
                   </asp:DropDownList>
               </td>
           </tr>
          
           
           <tr>
           <td>
           
               &nbsp;</td>
               <td>
           <asp:Button ID="btnComAddmetr" runat="server" Text="Add Meter" />
           
               </td>
               <td style="width: 4px">
                   &nbsp;</td>
               <td style="width: 323px">
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
               <td>
                   &nbsp;</td>
           </tr>
          
           
           <tr>
           
           </tr>
          
           
             </table>
              </fieldset>
             <fieldset style="width: 1127px; margin-right: 0px">
             <table>
             <tr>
             <td style="width: 107px">
             
             </td>
                 <td style="width: 150px">
                     Allot Order No.</td>
                                        <td>
                                            :</td>
                 <td style="width: 150px">
                 <asp:TextBox ID="txtComAllotorderno" runat="server" Width="145px" ></asp:TextBox>
                 </td>
                 <td style="width: 171px">
                 </td>
                 <td style="width: 150px">
                     Office Order Date</td>
                 <td style="width: 3px">
                     :</td>
                 <td style="width: 204px">
                   <asp:DropDownList ID="ddlComOrderDate" runat="server" 
                       AppendDataBoundItems="true" Width="150px">
                   </asp:DropDownList>
                 </td>
             </tr>
             <tr>
             <td style="width: 107px">
             
                 &nbsp;</td>
                 <td style="width: 150px">
                     Allotment Date</td>
                                        <td>
                                            :</td>
                 <td style="width: 150px">
                   <asp:DropDownList ID="ddlComAllotmentDate" runat="server" 
                       AppendDataBoundItems="true" Width="150px">
                   </asp:DropDownList>
                 </td>
                 <td style="width: 171px">
                     &nbsp;</td>
                 <td style="width: 150px">
                     Quarter Rent</td>
                 <td style="width: 3px">
                     :</td>
                 <td style="width: 204px">
                 <asp:TextBox ID="txtComQuarterRent" runat="server" Width="145px" ></asp:TextBox>
                 &nbsp;/Month</td>
             </tr>
             <tr>
             <td style="width: 107px">
             
                 &nbsp;</td>
                 <td style="width: 150px">
                     Water Meter</td>
                                        <td>
                                            :</td>
                 <td style="width: 150px">
                 <asp:CheckBox ID="chkbComWaterMeter" runat="server" Text="No" />
                     &nbsp;</td>
                 <td style="width: 171px">
                     &nbsp;</td>
                 <td style="width: 150px">
                     Water Meter Rent</td>
                 <td style="width: 3px">
                     :</td>
                 <td style="width: 204px">
                 <asp:CheckBox ID="chkbComWaterMeterRent" runat="server" Text="Yes" />
                     </td>
             </tr>
             <tr>
             <td style="width: 107px">
             
                 &nbsp;</td>
                 <td style="width: 150px">
                     Gas Master</td>
                                        <td>
                                            :</td>
                 <td style="width: 150px">
                 <asp:RadioButton ID="rdbtnComGasMeter" runat="server" Text="Yes"/>
                     &nbsp;</td>
                 <td style="width: 171px">
                     &nbsp;</td>
                 <td style="width: 150px">
                 <asp:RadioButton ID="rdbtnComGasMeterNo" runat="server" Text="No"/>
                     </td>
                 <td style="width: 3px">
                     :</td>
                 <td style="width: 204px">
                   <asp:DropDownList ID="ddlComGasmeter" runat="server" 
                       AppendDataBoundItems="true" Width="150px">
                   </asp:DropDownList>
                 </td>
             </tr>
             <tr>
             <td style="width: 107px">
             
                 &nbsp;</td>
                 <td style="width: 150px">
                     Customer No.</td>
                                        <td>
                                            :</td>
                 <td style="width: 150px">
                   <asp:DropDownList ID="ddlComCustomerNo" runat="server" 
                       AppendDataBoundItems="true" Width="150px">
                   </asp:DropDownList>
                 </td>
                 <td style="width: 171px">
                     &nbsp;</td>
                 <td style="width: 150px">
                     &nbsp;</td>
                 <td style="width: 3px">
                     &nbsp;</td>
                 <td style="width: 204px">
                     &nbsp;</td>
             </tr>
             </table>
             </fieldset>
             <fieldset style="width: 1125px">
             <table>
             <tr>
             <td style="width: 100px">
             
             </td>
                 <td>
                 </td>
                 <td style="width: 100px">
                 <asp:Button ID="btnComSave" runat="server" Text="Save" Width="90px" />
                 
                 </td>
                 <td style="width: 10px">
                 </td>
                 <td style="width: 100px">
                 <asp:Button ID="btnComModify" runat="server" Text="Modify" Width="90px" />
                 
                 </td>
                 <td>
                 </td>
                 <td style="width: 242px">
                 </td>
                 <td style="width: 100px">
                 <asp:Button ID="btnComReport" runat="server" Text="Report" Width="90px" />
                 
                 </td>
                 <td>
                 </td>
                 <td style="width: 100px">
                 <asp:Button ID="btnComCancel" runat="server" Text="Reset" Width="90px" />
                 
                 </td>
                 <td>
                 </td>
                 <td style="width: 100px">
                 <asp:Button ID="btnComExit" runat="server" Text="Exit" Width="90px" />
                 
                 </td>
                 <td>
                 </td>
             </tr>
             
             
             
             </fieldset>
             </table>


</asp:Content>

