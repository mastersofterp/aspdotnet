<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ViewAcceptRejectItem.aspx.cs" Inherits="STORES_Transactions_Quotation_ViewAcceptRejectItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Requisition Items Details (Accepted/Rejected)</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="panel panel-info">
                                    <div class="panel-heading">Requisition Items Details </div>
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlDept" runat="server">

                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-3">
                                                    <label>Department:</label>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" ToolTip="Select Department">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <div class="col-md-2"></div>
                                        <div class="col-md-12">
                                            <div id="tdpending" runat="server">
                                                <div class="col-md-12 table-responsive">
                                                    <asp:Panel ID="pnllist" runat="server" ScrollBars="Auto" Visible="false">
                                                        <asp:ListView ID="lvRequisition" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">Requisition List
                                                                    </h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Edit
                                                                                </th>
                                                                                <th>Requisition No.
                                                                                </th>
                                                                                <th>Requisition Date
                                                                                </th>
                                                                                <th>Requisition By
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
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("REQTRNO") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REQ_NO")%>
                                                                        <asp:HiddenField ID="hdnREQTRNO" runat="server" Value='<%# Eval("REQTRNO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REQ_DATE","{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("NAME")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>





                                        <asp:Panel ID="pnlItemDetail" runat="server" Visible="false">
                                            <div class="col-md-6">
                                                <label>Requisition Slip No:</label>
                                                <asp:Label ID="lblReqSlipNo" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Requisition Date:</label>
                                                <asp:Label ID="lblReqDate" runat="server" Text=""></asp:Label>

                                            </div>
                                            <div class="col-md-6">
                                                <label>Department:</label>
                                                <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Requisition By:</label>
                                                <asp:Label ID="lblAuthorityName" runat="server" Text=""></asp:Label>
                                            </div>
                                            <br />
                                            <div class="col-md-12 table-responsive">
                                                <asp:ListView ID="lvitemReq" runat="server" OnItemDataBound="lvitemReq_ItemDataBound">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <div class="titlebar">
                                                                <h4>Requisition Details</h4>

                                                            </div>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <tr class="bg-light-blue">
                                                                    <th style="width: 10%;">Accept/Reject</th>
                                                                    <th style="width: 30%">Items
                                                                    </th>
                                                                    <th style="width: 10%">QTY
                                                                    </th>
                                                                    <th style="width: 10%">Approximate Cost
                                                                    </th>
                                                                    <th style="width: 25%">Item Specification
                                                                    </th>
                                                                    <th style="width: 15%">Status
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                                <asp:CheckBox ID="chkItem" runat="server" ToolTip='<%# Eval("REQ_TNO")%>' />
                                                                <asp:HiddenField ID="hdbAcceptReject" runat="server" Value='<%# Eval("ITEM_ACCEPT_REJECT")%>' />
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdReqtrNo" runat="server" Value='<%# Eval("REQTRNO")%>' />
                                                            </td>
                                                            <td style="width: 10%">
                                                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("REQ_QTY")%>'></asp:Label>
                                                                <asp:TextBox ID="txtqty" CssClass="form-control" Visible="false" runat="server" Text='<%# Eval("REQ_QTY")%>'></asp:TextBox>
                                                            </td>
                                                            <td style="width: 10%">
                                                                <asp:Label ID="lblCost" runat="server" Text='<%# Eval("Tot_Cost")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <asp:Label ID="lblItemSpeci" runat="server" Text='<%# Eval("ITEM_SPECIFICATION")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 15%">
                                                                <%# Eval("AR_STATUS")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <div class="col-md-12 text-center">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-4" style="padding-left: 60px">
                                                    <label>Approximate Total Cost:</label>
                                                </div>
                                                <div class="col-md-5" style="padding-right: 325px">
                                                    <asp:Label runat="server" Font-Bold="true" ID="lblTotAppCost"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-info"  OnClick="btnBack_Click" />
                                            </div>
                                        </asp:Panel>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div class="col-md-12">
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
</asp:Content>

