<%@ Page Language="C#"  AutoEventWireup="true"
 CodeFile="PopUp.aspx.cs" Inherits="PopUp" Title="Help Page" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link rel="shortcut icon" href="images/favicon.ico"  type="image/x-icon"/>
    <link href="Css/master.css" rel="stylesheet" type="text/css" />
    <link href="Css/Theme1.css" rel="stylesheet" type="text/css" />
    <link href="Css/linkedin/tabs.css" rel="stylesheet" type="text/css" />
    <link href="Css/linkedin/blue/linkedin-blue.css" rel="stylesheet" type="text/css" />
    <link href="includes/modalbox.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">

 function UpdateParentWindow(fName,lName)
 {
 
  var arrayValues= new Array(fName,lName);
  window.opener.updateValues(arrayValues);       
  window.close(); 
  }


</script>
<body  style="background-color:LightBlue">
 <form id="frmMaster" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="90%">
            <tr>
                
            </tr>
            <tr>
                <td style="padding: 10px">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <br />
            <table style="width: 100%">
                <tr>
                    <td class="form_left_label" style="width: 111px">
                        <span style="color: #FF3300">*</span> Search Criteria..</td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtSearch" runat="server" Width="60%" 
                            ontextchanged="txtSearch_TextChanged" AutoPostBack="True"></asp:TextBox>
                       </td>
                </tr>
                <tr>
                    <td class="form_left_label" style="height: 194px; width: 111px;">
                        &nbsp;
                    </td>
                    <td class="form_left_text" style="height: 194px">
                        <asp:Panel ID="pnl" runat="server" Style="height: 300px; width: 60%" 
                            BorderColor="#0066FF">
                            <asp:GridView ID="lvGrp" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" style="width:100%" 
                                 onrowdatabound="lvGrp_RowDataBound"  
                                 >
                                <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>                                    
                                            <asp:LinkButton id="lnkHead" runat="server" text= '<%# Eval("PARTY_NAME") %>'></asp:LinkButton>
                                            <asp:HiddenField id="hdnPcd" runat="server" value= '<%# Eval("PARTY_NO") %>' />
                                              </ItemTemplate>                          
                                
                                </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                          </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</form>
</body>
