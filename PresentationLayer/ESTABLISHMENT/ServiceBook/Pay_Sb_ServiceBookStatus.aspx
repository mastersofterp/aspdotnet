<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pay_Sb_ServiceBookStatus.aspx.cs" MasterPageFile="~/ServiceBookMaster.master" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_ServiceBookStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="sbctp" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Service Book Status</h3>
                </div>
                <div class="box-body">
                    <%--<asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Select User Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                                        ToolTip="Select User Type">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUserType"
                                        Display="None" ErrorMessage="Please Select User Type" ValidationGroup="config"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>--%>

                    <asp:Panel ID="pnlStatus" runat="server">
                        <div class="col-12">
                            <%--<div class="sub-heading">
                                <h5>Service Book Links</h5>
                            </div>--%>
                            <asp:ListView ID="lvStatus" runat="server" OnItemDataBound="lvStatus_ItemDataBound">
                                <EmptyDataTemplate>
                                    <p class="text-center">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Service Book Title
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                    <th>Total No.of Records
                                                    </th>
                                                    <%--<th>Percentage
                                                    </th>    --%>             
                                                </tr>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </thead>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>

                                            <asp:Label ID="lblMenuid" runat="server" ToolTip='<%# Eval("MenuId")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbltitle" runat="server" ToolTip='<%# Eval("Title")%>' Text='<%# Eval("Title")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNoofRec" runat="server" ToolTip='<%# Eval("TOTAL_COUNT")%>' Text='<%# Eval("TOTAL_COUNT")%>'></asp:Label>
                                        </td>
                                        <%-- <td>
                                            <asp:Label ID="lblPer" runat="server" ToolTip='<%# Eval("PER")%>' Text='<%# Eval("PER")%>' Visible="false"></asp:Label>
                                        </td>--%>
                                        <%--<td>
                                            <asp:CheckBox runat="server" ID="chkActive" />
                                        </td>--%>
                                        <%--<td>
                                            <asp:CheckBox runat="server" ID="chkPer" />
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                    <div id="Div1" class="col-12" runat="server" style="text-align:center">
                        <asp:Label ID="lblPer" runat="server" Font-Size="Medium"></asp:Label>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" ValidationGroup="config" Text="FINAL SUBMIT" Visible="false"
                            CssClass="btn btn-primary" ToolTip="Click here to Final Submit"  />
                        <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="" />--%>
                        <asp:ValidationSummary ID="valConfig" ValidationGroup="config" runat="server" ShowMessageBox="true"
                            ShowSummary="false" />
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
