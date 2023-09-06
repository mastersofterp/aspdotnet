<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Requisition_Track.aspx.cs" Inherits="STORES_Transactions_Quotation_Str_Requisition_Track" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                   <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">REQUISITION STATUS</h3>
                        </div>
                     
                            <div class="box-body">
                             
                                  
                                     <%--   <div class="sub-heading"><h5>Requisition Status</h5></div>--%>
                                    
                                            <div class="col-12">

                                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                                    <asp:ListView ID="lvRequisition" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>Requisition Status List</h5>
                                                                </div>
                                                               <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                            <th>Requisition No.
                                                                            </th>
                                                                            <th>Approval Status
                                                                            </th>
                                                                            <th>Indent Status
                                                                            </th>
                                                                            <th>Quotation Status
                                                                            </th>
                                                                            <th>PO Status
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%# Eval("REQ_NO")%>
                                                                    <asp:HiddenField ID="hdnREQTRNO" runat="server" Value='<%# Eval("REQTRNO")%>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("APPROVAL_STATUS")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("INDENT_STATUS")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("QUOTATION_STATUS")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PO_STATUS")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="1" ToolTip="Click to get report" />
                                            </div>
                                    
                                 
                                </div>
                            </div>
                       
                    </div>
                </div>
                
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

