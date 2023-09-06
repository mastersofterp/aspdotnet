<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RequisitionSecondApproval.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_RequisitionSecondApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

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
                            <h3 class="box-title">TRANSPORT REQUISITION SECOND LEVEL APPROVAL</h3>
                        </div>

                        <div id="divGR" runat="server">
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Transport Requisition Second Level Approval</div>
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
                                                <h4 class="box-title">Fees Paid Student List
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Select
                                                            </th>
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
                                                            <th>Fees Paid
                                                            </th>                                                           
                                                            <th>Accept/Reject
                                                            </th>
                                                            <th>Remark
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
                                                    <asp:CheckBox ID="chkStatus" runat="server" ToolTip='<%# Eval("STUD_IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                <%--   OnClick="btnDetail_Click"--%>
                                                </td>
                                                <td>
                                                    <%--<asp:LinkButton ID="btnDetail" runat="server" CommandArgument='<%# Eval("STUD_IDNO")%>' CommandName='<%# Eval("VTRAID")%>' Text='<%# Eval("ENROLLNO")%>'
                                                        Font-Underline="true" Font-Bold="true" ></asp:LinkButton>--%>
                                                     <%# Eval("ENROLLNO")%>
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
                                                    <%# Eval("DEMAND_AMT")%>                                                    
                                                </td>
                                                <td>
                                                    <%# Eval("DSR_AMT")%>                                                    
                                                </td>                                               
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" CssClass="form-control">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Accept</asp:ListItem>
                                                        <asp:ListItem Value="2">Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("SECOND_APPROVE_REMARK") %>' CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                </p>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

