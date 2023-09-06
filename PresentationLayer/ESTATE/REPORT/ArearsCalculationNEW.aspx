<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ArearsCalculationNEW.aspx.cs"
     Inherits="ESTATE_Report_ArearsCalculationNEW" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table  width="100%"  cellpadding ="0" cellspacing="0" >
         <tr> 
         <td width="10px"></td>
         <td width="30px"> Calculate Arears</td>
         
         <td width="20px"><asp:Button  ID ="btncalenergy" Text="cal Arrears Energy" 
                 Width="150px" runat="server" onclick="btncalenergy_Click" /></td>
         <td width="20px"><asp:Button  ID ="btncalwater" Text="cal Arrears Water" 
                 Width="150px"  runat="server" onclick="btncalwater_Click"/></td>
         <td width="20px"></td>
         </tr> 
         
  </table> 




</asp:Content>

