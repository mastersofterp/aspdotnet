<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeeCollectOption.aspx.cs" Inherits="Academic_FeeCollectionOptions"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
       <div class="row">
        <div class="col-md-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">FEE COLLECTION OPTIONS</h3>

                </div>
                <form role="form">
                    <div class="box-body">
                        <div class="form-group col-md-4">
                            <label>Select Receipt Type for Fee Collection</label>
                            <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" />
                        </div>    
                        <div class="col-md-12">   
                        <asp:ListView ID="lvFeeCollectionModes" runat="server" Visible="false">
                            <LayoutTemplate>
                                <div id="divlvFeeCollectionModes">
                                  
                                       <h3> Available Modes of Fee Collection</h3>
                                    
                                    <table class="table table-bordered table-hover table-striped">
                                        <thead>
                                        <tr class="bg-light-blue" >
                                            <th>
                                                Modes
                                            </th>
                                        </tr>
                                            </thead>
                                        <tbody>
                                        <tr id="itemPlaceholder" runat="server" /></tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                    
                                        <asp:HyperLink ID="hlkFeeCollection" runat="server" Text='<%# Eval("LINK_CAPTION") %>'
                                            NavigateUrl='<%# Eval("LINK_URL") + "&RecTitle=" + ddlReceiptType.SelectedItem.Text + "&RecType=" + ddlReceiptType.SelectedValue %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView> 
        </div>                    
                    </div>
                </form>
            </div>
        </div>
        
    </div>


</ContentTemplate>
    </asp:UpdatePanel>
  
    <br />
   
</asp:Content>
