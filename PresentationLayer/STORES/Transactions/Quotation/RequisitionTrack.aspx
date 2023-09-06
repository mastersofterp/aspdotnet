<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RequisitionTrack.aspx.cs" Inherits="STORES_Transactions_Quotation_RequisitionTrack" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">REQUISITION TRACK</h3>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span> </h5>

                            <asp:Panel ID="PnlReport" runat="server">
                                <div class="panel panel-info">
                                    <div class="panel-heading">Select Requisition Number</div>
                                    <div class="panel-body">
                                        <%--<asp:Label ID="lblmsg" runat="server" SkinID="Msglbl"></asp:Label>--%>
                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-8">
                                                <div class="form-group col-md-9">
                                                    <label>Requisition Slip No. :<span id="span8" style="color: #FF0000">*</span></label>
                                                    <asp:DropDownList ID="ddlRequisition" TabIndex="1" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="true" ValidationGroup="report">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvReportIndent" runat="server" ControlToValidate="ddlRequisition"
                                                        ErrorMessage="Please Select Requisition Slip No." Display="None" ValidationGroup="report"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <br />

                                                    <asp:Button ID="btnShow" Text="Show" runat="server" TabIndex="2" ToolTip="Click To Show " CssClass="btn btn-info" ValidationGroup="report"
                                                        OnClick="btnShow_Click" />
                                                    <asp:Button ID="btnBack" Text="Back" runat="server" TabIndex="2" ToolTip="Click To Go Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                                    <asp:ValidationSummary ID="vsReport" runat="server" ValidationGroup="report" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4">
                                            </div>
                                        </div>



                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-10" id="divReqDate" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <label>Requisition Date :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="lblReqDate" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                <asp:Panel ID="pnlItem" runat="server" Visible="false">
                                                    <asp:ListView ID="lvItemDetails" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <h4>Item Details</h4>
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Item Name</th>
                                                                            <th>Quantity</th>
                                                                            <th>Approx. Amount</th>
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
                                                                <td><%# Eval("ITEM_NAME")%></td>
                                                                <td><%# Eval("REQ_QTY")%></td>
                                                                <td><%# Eval("AMOUNT")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <%-- <div class="form-group row">--%>
                                                <%--<div class="col-md-12">--%>
                                                <asp:Panel ID="pnlAuthhority" runat="server" Visible="false">
                                                    <asp:ListView ID="lvAuthority" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <h4>Approval Status</h4>
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Authority</th>
                                                                            <th>Status</th>
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
                                                                <td><%# Eval("PANAME")%></td>
                                                                <td><%# Eval("APPSTATUS")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                                <%--</div>--%>
                                                <%--</div>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="Panel1" runat="server" Visible="true">
                                                            <div class="form-group col-md-10" id="divIndentP" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>Indent Preparation :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblIndent" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-10" id="divQuot" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>Quotation  :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblQuot" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-10" id="divPOPre" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>PO Preparation :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblPO" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-10" id="divInv" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>Invoice :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblInvoice" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-10" id="divPOApproval" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>PO Approval :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblPOApprroval" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-10" id="divItemAvail" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>Item Availability :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblItemReceived" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-10" id="divItemIssue" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>Item Issued :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblItemIssue" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-10" id="divItemAccept" runat="server" visible="false">
                                                                <div class="col-md-6">
                                                                    <label>Item Acceptance :</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblItemAccept" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                </div>
                                                            </div>

                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>

