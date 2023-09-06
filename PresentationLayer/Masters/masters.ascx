<%@ Control Language="C#" AutoEventWireup="true" CodeFile="masters.ascx.cs" Inherits="Masters_masters" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolKit" %>

  

<style>
    #demo-grid {
        display:block;
    }
    #demo-grid .titlebar {
        background-color:#3c8dbc;
        padding:7px;
        color:#fff;
    }
    #demo-grid.vista-grid {
        padding:0;
    }
    #demo-grid.vista-grid .titlebar + div {
        height:auto;
        max-height:400px;
        overflow:auto;
        border:1px solid #3c8dbc;
    }
    #gvmaster > tbody > tr.gv_header > th {
        padding:7px;
    }
    #gvmaster > tbody > tr.gv_header > th:nth-child(2) {
        text-align:center
    }
    .table {
        margin-bottom:0;
    }
    .sub-heading h5 {
    text-align: left;
}
    #demo-grid .table > tbody > tr > td, #demo-grid .table > tbody > tr > th, #demo-grid .table > thead > tr > th, #demo-grid .table > thead > tr > td {
        vertical-align:middle;
    
    }
    #demo-grid table > tbody > tr > td, #demo-grid table > tbody > th {
        font-size:90%;
        padding:4px !important
    }

    #ctl00_ContentPlaceHolder1_ucmaster_tblMaster tbody > tr {
        display:inline-block;
        width:22%;
        vertical-align:top;
        margin-left:10px;
        margin-right:10px;
    }
     #ctl00_ContentPlaceHolder1_ucmaster_tblMaster tbody .form-control{
         width:100% !important;
    }
    .box-body tbody td {
        font-size: 13px;
    }
    @media (min-width:767px) and (max-width:991px) {
        #ctl00_ContentPlaceHolder1_ucmaster_tblMaster tbody > tr {
            width:45%;
        }
    }
    @media only screen and (max-width:767px) {
        #ctl00_ContentPlaceHolder1_ucmaster_tblMaster tbody > tr {
            width:94%;
        }
    }
    </style>


<script>
    $(document).ready(function () {
        $('input[type=text]').addClass("form-control");
        //$('#demo-grid table').addClass("table table-bordered table-striped")
    });
</script>


<div class="row">
    <div class="col-md-12">
         <asp:Panel ID="Panel1" runat="server">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title"><asp:Label id="lblPageTitle" runat="server" /></h3>
                </div>
                
                <form role="form">
                    <div class="box-body">
                        <div class="">
                                <asp:PlaceHolder ID="phAdd" runat="server" />  
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblmsg" runat="server" SkinID="Errorlbl" ></asp:Label>
                        </div>
                        <div class="col-12 btn-footer mb-1">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Submit" CssClass="btn btn-primary" />
                            <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False" ToolTip="Show Report" CssClass="btn btn-info"  Visible="false"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" ToolTip="Cancel" CssClass="btn btn-warning" />
                        
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </div>
                </form>

                <div class="box-footer pt-0">
                    <div class="form-group">
                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            
                            <div id="demo-grid" class="vista-grid">
                                <div class="sub-heading">
                                    <h5 id="gvTitle" runat="server"></h5>
                                </div>
                                <asp:Panel ID="pnlGrid" runat="server" Style="overflow: auto;">
                                    <asp:PlaceHolder ID="phList" runat="server" />
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="divMsg" runat="server">
                    </div>
                </div>  
            </div>    
         </asp:Panel>
    </div>
</div>
  
<table cellpadding="0" cellspacing="0" style="width:100%; margin:auto;">
  
    
    <%--ADD/UPDATE MASTERS --%>
   <asp:Panel ID="pnlAdd" runat="server">   
   <tr>
    <td colspan="2" align="center">
        <%--<asp:Label ID="lblmsg" runat="server" SkinID="Errorlbl" ></asp:Label>--%>
    </td>
   </tr>
   
   <tr>
    <td colspan="2">
       <%-- <asp:PlaceHolder ID="phAdd" runat="server" />--%>
    </td>
   </tr>
    
    <tr>
        <td class="form_left_label">&nbsp;</td>
        <td class="form_left_text">
           <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Submit" Width="80px" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" ToolTip="Cancel" Width="80px"/>&nbsp;
            <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False" ToolTip="Show Report" Width="80px"/>&nbsp;
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>--%>
        </td>
          
    </tr>
    <tr>
        <td colspan="2" class="form_button">
           <%-- <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />--%>
        </td>
    </tr>    
    </asp:Panel>

    <%--MASTERS LIST--%>
  <%--  <asp:Panel ID="pnlList" runat="server">           --%>                   
        <tr>
             <td style="padding-left:10px">
                <%--GRIDVIEW--%>
                <%--<div id="demo-grid" class="vista-grid">
                    <div id="gvTitle" runat="server" class="titlebar"></div>
                    <asp:Panel ID="pnlGrid" runat="server" style="overflow:auto;" Height="400px">
                        <asp:PlaceHolder ID="phList" runat="server" />--%>
                        <%--<asp:GridView ID="gv" runat="server"></asp:GridView>--%>
         <%--           </asp:Panel>
                </div>
            </td>
        </tr>
</asp:Panel>--%>
 
</table>


