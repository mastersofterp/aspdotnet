<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_TrackPaymentBill.aspx.cs" Inherits="ACCOUNT_Acc_TrackPaymentBill" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updTrack"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updTrack" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Track Payment Bill</h3>
                        </div>

                        <div class="box-body">
                            <div id="complaint" runat="server" visible="true">
                                <div class="col-12">
                                    <div class=" sub-heading">
                                        <h5>Track Payment Bill</h5>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lable1" runat="server" Text="" Style="font-weight: bold; font-size: large; text-align: center"></asp:Label>
                                    <table id="table2" class="table table-striped table-bordered nowrap display">
                                        <asp:Repeater ID="lvBillList" runat="server" Visible="true" OnItemDataBound="lvBillList_ItemDataBound">
                                            <HeaderTemplate>
                                                <%--<h4 class="box-title">Bill Transaction Status</h4>--%>
                                                <thead>
                                                    <tr class="bg-light-blue">

                                                        <th>Process
                                                        </th>
                                                        <th>Approval Level
                                                        </th>
                                                        <th>Status
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>

                                                    <td>

                                                        <asp:Label ID="lblindex" Style="font-weight: bold" runat="server" Text=' <%#Container.ItemIndex+1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPayeeName" Style="font-weight: bold" runat="server" Text='<%# Eval("PANAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNatureService" Style="font-weight: bold" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
          

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

