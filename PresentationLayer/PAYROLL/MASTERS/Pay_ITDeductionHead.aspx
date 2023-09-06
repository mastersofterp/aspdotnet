<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_ITDeductionHead.aspx.cs" Inherits="Pay_ITRebate" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">IT DEDUCTION HEADS</h3>
                            <%--<div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>--%>
                        </div>

                        <div class="box-body">           
                            <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                Visible="false">
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
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
                                            <img src="../../../images/confirm.gif" class="text-center" alt="confirm" />
                                        </td>
                                        <td style="width: 97%">
                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div class="col-12">
                                <div class="sub-heading">
	                                <h5>Deduction Heads</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
                                             <sup>* </sup>
									        <label>Head</label>
								        </div>
                                        <asp:TextBox ID="txtHead" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" TabIndex="1" ToolTip="Enter Deduction Head" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtHead" runat="server" ErrorMessage="Enter Deduction Head Name" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
          
                                         </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Field</label>
								        </div>
                                            <asp:TextBox ID="txtField" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" TabIndex="2" ToolTip="Enter Maximum Limit" />
							        </div>                                
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="5" ToolTip="Click To Save" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print" TabIndex="6" ToolTip="Click To Print" OnClick="btnPrint_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" TabIndex="7" ToolTip="Click To Cancel" OnClick="btncancel_Click" CssClass="btn btn-warning" />                                
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                            <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvITDedHead" runat="server">
                                    <LayoutTemplate>
                                            <div class="sub-heading">
	                                            <h5>IT DEDUCTION HEADS</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>HEAD
                                                        </th>
                                                        <th>FIELD
                                                        </th>
                                                        <th>FIELD TYPE
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPayHead" runat="server" Text='<%# Eval("PAYHEAD") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDedHead" runat="server" Text='<%# Eval("DEDHEAD") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFieldType" runat="server" Text='<%# Eval("ST") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>                            
                            </asp:Panel>
                            </div>
                            <div id="divMsg" runat="server"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

