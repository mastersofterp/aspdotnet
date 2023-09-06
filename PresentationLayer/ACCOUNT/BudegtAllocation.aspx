<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BudegtAllocation.aspx.cs" Inherits="ACCOUNT_BudegtAllocation" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: right;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDMainGroup"
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

    <asp:UpdatePanel ID="UPDMainGroup" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">BUDGET ALLOCATION</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size:x-large;"></div>
                            <div class="box-body">
                                <asp:Panel ID="pnl" runat="server">
                                   <%-- <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Allocate Budget Amount</h5>
                                        </div>
                                    </div>--%>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlRepeator" runat="server">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <asp:Repeater ID="lvbudgethead" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Budget Code</th>
                                                                <th>Budget Name</th>
                                                                <th>Opening Balance</th>
                                                                <th>Remaining Amount</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("BUDG_CODE")%>
                                                                <asp:HiddenField ID="hdnBudNo" runat="server" Value='<%# Eval("budg_no")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("BUDG_NAME")%>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmt" runat="server" Text='<%# Eval("BUD_AMT")%>' CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmt" runat="server" TargetControlID="txtAmt"
                                                                    ValidChars="0123456789." FilterMode="ValidChars">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRemAmt" runat="server" Text='<%# Eval("BUD_BAL_AMT") %>' CssClass="form-control"></asp:Label>
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
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    </div>


                                    <div class="col-md-4">
                                        <asp:Panel ID="panel2" runat="server">
                                            <asp:TreeView ID="tvHierarchy" runat="server" ImageSet="Arrows">
                                            </asp:TreeView>
                                        </asp:Panel>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tvHierarchy" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
