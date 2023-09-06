<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_InvoiceReport.aspx.cs" Inherits="STORES_Reports_Str_InvoiceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%-- <script src="../../jquery/jquery-3.2.1.min.js"></script>--%>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>
    <%--<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="jquery/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
     
    </script>--%>
    <asp:UpdatePanel ID="UpApproval" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Invoice Wise Report</h3>

                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlStrConfig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="sub-heading">
                                            <h5>Invoice Entry List</h5>
                                        </div>
                                        <div class="col-12">
                                            <asp:ListView ID="lvInvdetails" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Invoice Number</th>
                                                                    <th>Invoice Date</th>
                                                                    <th>PO Reference No.</th>
                                                                    <th>Action</th>
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
                                                        <td><%# Eval("INVNO")%>                                                                    
                                                        </td>
                                                        <%-- <asp:HiddenField ID="hdnInvtrno" runat="server" Value='<%#Eval("INVTRNO")%>'/>--%>
                                                        <td><%#Eval("INVDT","{0:dd/MM/yyyy}") %></td>
                                                        <td><%#Eval("POREFNO") %></td>
                                                        <td>
                                                            <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Text="Report"
                                                                AlternateText="Edit Record" CommandArgument='<%#Eval("INVTRNO")%>' OnClick="btnReport_Click" />
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>


                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
