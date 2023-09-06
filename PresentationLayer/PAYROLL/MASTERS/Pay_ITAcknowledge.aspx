<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_ITAcknowledge.aspx.cs" Inherits="Pay_ITAcknowledge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">IT ACKNOWLEDGE</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAck" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Edit Acknowledge</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-md-6 col-12">
                                    <div class="row">
                                        <div class="form-group col-6">
                                            <div class="label-dynamic">
                                                <label>Quarter</label>
                                            </div>
                                        </div>

                                        <div class="form-group col-6">
                                            <div class="label-dynamic">
                                                <label>Acknowledgement</label>
                                            </div>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtQuarter1" runat="server" ToolTip="Enter Quarter Number" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtAck1" runat="server" ToolTip="Enter Acknowledgement" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                        </div>


                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtQuarter2" runat="server" ToolTip="Enter Quarter Number" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtAck2" runat="server" ToolTip="Enter Acknowledgement" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtQuarter3" runat="server" ToolTip="Enter Quarter Number" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtAck3" runat="server" ToolTip="Enter Acknowledgement" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtQuarter4" runat="server" ToolTip="Enter Quarter Number" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtAck4" runat="server" ToolTip="Enter Acknowledgement" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtQuarter5" runat="server" ToolTip="Enter Quarter Number" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:TextBox ID="txtAck5" runat="server" ToolTip="Enter Acknowledgement" CssClass="form-control" TabIndex="10"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Text="Update" ValidationGroup="submit" TabIndex="11" ToolTip="Click To Save" CssClass="btn btn-primary" OnClick="btnsubmit_Click" />

                                <asp:Button ID="btncancel" runat="server" Text="Cancel" TabIndex="12" ToolTip="Click To Cancel" CssClass="btn btn-warning" OnClick="btncancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                            
                           <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                    Visible="false">
                                    <table class="table table-bordered table-hover">
                                        <tr>
                                            <td style="width: 3%; vertical-align: top">
                                                <%--<img src="../../../images/error.gif" align="absmiddle" alt="Error" />--%>
                                                <img src="../../../images/error.gif" class="text-center" alt="Error" />
                                            </td>
                                            <td style="width: 97%">
                                                <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                        </font>
                                                <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                    Visible="false">
                                    <table class="table table-bordered table-hover">
                                        <tr>
                                            <td style="width: 3%; vertical-align: top">
                                                <%-- <img src="../../../images/confirm.gif" align="absmiddle" alt="confirm" />--%>
                                                <img src="../../../images/confirm.gif" class="text-center" alt="confirm" />
                                            </td>
                                            <td style="width: 97%">
                                                <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

