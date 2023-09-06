<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_AOFO_Config.aspx.cs" Inherits="ACCOUNT_Acc_AOFO_Config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPBudget"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="UPBudget" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="divMsg" runat="server">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">AO FO Configuration</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12 mb-3">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <%--<div class="panel-heading">BUDGET ENTRY </div>--%>
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>AO</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAO" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem  Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>FO</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFO" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem  Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-5 col-md-6 col-12" runat="server" id="checkbudget">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:CheckBox ID="chkTempVoucher" runat="server" Text=" Is Temp Voucher" AutoPostBack="true" />

                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit"  OnClick="btnSubmit_Click"/>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Clear" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="bhn" runat="server" ValidationGroup="bhn" ShowMessageBox="true" ShowSummary="false" />
                                </div>

                                  <div class="box-body">
                                <asp:Panel ID="Panel1" runat="server">
                                   <%-- <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Allocate Budget Amount</h5>
                                        </div>
                                    </div>--%>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlRepeator" runat="server">
                                            <table class="table table-striped table-bordered" style="width: 100%" id="">
                                                <asp:Repeater ID="lvAOFO" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="bg-light-blue">
                                                            <tr>                                                             
                                                                <th>AO Name</th>
                                                                <th>FO Name</th>
                                                                <th>Verifier Name</th>
                                                                <th>Approval Name</th>
                                                                <th>Is Temporary voucher</th>
                                                                 <th>Edit</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("AO")%>                                                         
                                                            </td>
                                                            <td>
                                                                <%# Eval("FO")%>
                                                            </td>
                                                             <td>
                                                                <%# Eval("VERIFIER")%>
                                                            </td>
                                                             <td>
                                                                <%# Eval("APPROVER")%>
                                                            </td>
                                                              <td>
                                                                <%# Eval("IsTempVoucher")%>
                                                            </td>
                                                             <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("AOID") %>'
                                                          AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                  
                                    <div class="col-md-4">
                                        <asp:Panel ID="panel2" runat="server">
                                            <asp:TreeView ID="tvHierarchy" runat="server" ImageSet="Arrows">
                                            </asp:TreeView>
                                        </asp:Panel>
                                    </div>

                                </asp:Panel>
                            </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

