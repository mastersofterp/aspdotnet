<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BusPassGeneration.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_BusPassGeneration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <div class="col-md-12 text-center">
        <asp:UpdateProgress ID="GAUpdPro" runat="server">
            <ProgressTemplate>
                <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                Loading....
            </ProgressTemplate>
        </asp:UpdateProgress>

    </div>

     <asp:UpdatePanel ID="updApprove" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANSPORT BUS PASS</h3>
                        </div>

                        <div id="divGR" runat="server">
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Transport Bus Pass</div>
                                                <div class="panel panel-body">
                                                    


                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                            </form>
                        </div>
                        <div class="box-footer" id="divList" runat="server">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlTransport" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvRequisition" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title">Student List
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Student Name 
                                                            </th>
                                                            <th>TAN / PAN 
                                                            </th>
                                                            <%--<th>Application Date 
                                                            </th>--%>
                                                            <th>Category
                                                            </th>
                                                            <th>Stop
                                                            </th>
                                                            <th>Fees Amount
                                                            </th>
                                                            <th>Approved Amount
                                                            </th>
                                                            <th>Amount Paid
                                                            </th>
                                                            <th>Balance Amount 
                                                            </th>
                                                            <th>Bus Pass 
                                                            </th>                                                            
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" style="width: 50%" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLNO")%>
                                                    <%--<asp:LinkButton ID="btnDetail" runat="server" CommandArgument='<%# Eval("STUD_IDNO")%>' CommandName='<%# Eval("VTRAID")%>' Text='<%# Eval("ENROLLNO")%>'
                                                        Font-Underline="true" Font-Bold="true"></asp:LinkButton>--%>
                                                    <asp:HiddenField ID="hdnStudIdNo" runat="server" Value='<%# Eval("STUD_IDNO")%>' />
                                                    <asp:HiddenField ID="hdnVTRAID" runat="server" Value='<%# Eval("VTRAID")%>' />

                                                    <asp:HiddenField ID="hdnSessionNo" runat="server" Value='<%# Eval("SESSIONNO")%>' />
                                                    <asp:HiddenField ID="hdnDegreeNo" runat="server" Value='<%# Eval("DEGREENO")%>' />
                                                    <asp:HiddenField ID="hdnBranchNo" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                                    <asp:HiddenField ID="hdnSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                    <asp:HiddenField ID="hdnYear" runat="server" Value='<%# Eval("YEAR")%>' />

                                                </td>
                                                <%--<td>
                                                    <%# Eval("APP_DATE")%>
                                                   
                                                </td>--%>
                                                <td>
                                                    <%# Eval("CATEGORYNAME")%>
                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("STOPNAME")%>                                                    
                                                </td>
                                                <td>
                                                    <%# Eval("ACTUAL_AMOUNT")%>                                                    
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVED_AMOUNT")%>                                                    
                                                </td>
                                                <td>
                                                    <%# Eval("PAID_AMOUNT")%>                                                    
                                                </td>
                                                <td>
                                                    <%# Eval("BALANCE_AMOUNT")%>                                                    
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CommandArgument='<%# Eval("STUD_IDNO") %>'
                                                          OnClick="btnPrint_Click"    CssClass="btn btn-primary" />
                                                </td>
                                               
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

