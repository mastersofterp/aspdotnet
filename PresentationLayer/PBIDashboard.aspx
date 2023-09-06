<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PBIDashboard.aspx.cs" Inherits="PBIDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Disable Right Click --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body, html").on("contextmenu", function (e) {
                return false;
            });
        });
    </script>
    <%-- X --%>
    <style>
        #header-fixed {
            position: fixed;
            top: 0px;
            display: none;
            background-color: white;
        }

        .box-tools.pull-right {
            display: none;
        }

        .chiller-theme .sidebar-wrapper {
            display: none;
        }

        .page-wrapper.toggled .page-content {
            padding: 0;
            margin-top: 15px;
            padding-top: 0px !important;
        }

        .page-wrapper .page-content {
            height: auto;
        }

        .content {
            padding-bottom: 10px;
        }

        @media (min-width: 576px) and (max-width: 991px) {
            .content {
                padding-top: 0px;
                padding-bottom: 0px;
            }
        }
    </style>

    <style>
        .logoBar {
            display: none !important;
        }

        .sticker {
            background: white;
            position: relative;
            width: 100%;
            height: 60px;
            top: -63px;
        }
    </style>

    <%-- <style>
        .logoBarWrapper .logoBar {
            display:none !important;
        }
    </style>--%>


    <asp:UpdatePanel ID="updUpdate" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12 mt-5">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label><span style="color: red">*</span> Module</label>
                                        <asp:DropDownList ID="ddlWorkspace" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkspace_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label><span style="color: red">*</span> Dashboard</label>
                                        <asp:DropDownList ID="ddlSubWorkspace" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSubWorkspace_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="box-footer" id="divdashboard" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <iframe name="myIframe" id="myIframe" style="width: 100%" height="600" runat="server"></iframe>
                                        <div class="sticker">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

