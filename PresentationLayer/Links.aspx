<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Links.aspx.cs" Inherits="Links" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        html {
            background-color: silver;
        }

        .SOButton {
            border: none;
            background-color: #807F83;
            color: #FFFFFF;
            font-weight: bold;
            font-family: Arial,Liberation Sans,DejaVu Sans,sans-serif;
        }
    </style>

    <div style="z-index: 1; position: absolute; top: 10%; left: 40%;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <%--<div class="box box-primary">--%>

                     <%--   <div class="box-header with-border">
                            <h3 class="box-title">Links</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>--%>

                        <%-- <div class="box-body" runat="server">
                            <asp:Repeater ID="repLinks" runat="server">
                                <HeaderTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbLink" runat="server" OnClick="lbLink_Click" Text='<%# Eval("AL_LINK")%>' CommandName='<%# Eval("AL_NO")%>' CommandArgument='<%# Eval("AL_URL")%>' CssClass="btn btn-primary" Style="margin-top: 3px"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>

                        </div>--%>

                    <%--    <asp:Repeater ID="repLinks" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbLink" runat="server" OnClick="lbLink_Click" Text='<%# Eval("AL_LINK")%>' CommandName='<%# Eval("AL_NO")%>' CommandArgument='<%# Eval("AL_URL")%>' CssClass="btn" Style="margin-top: 3px; font-family: opensans-bold, sans-serif; color: #777" ></asp:LinkButton>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                &nbsp;|&nbsp;
                            </SeparatorTemplate>
                        </asp:Repeater>--%>

                <%--    </div>--%>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>
