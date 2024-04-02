<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ResetFinancialYear.aspx.cs" Inherits="ResetFinancialYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.0/themes/smoothness/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.13.0/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            $("#txtDateFrom").datepicker();
            $("#txtDateTo").datepicker();
        });
    </script>

    <div>
        <asp:UpdateProgress ID="updFinancialYear" runat="server" AssociatedUpdatePanelID="updFY"
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

    <asp:UpdatePanel ID="updFY" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>

                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelCrt" runat="server">
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>Selection Criteria</h5>
                                    </div>

                                    <div class="row mt-1">
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <asp:Label ID="lblFinYearFrom" runat="server" TabIndex="1" ToolTip="Financial Year Start Date" Text="Financial Year Start Date :" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtDateFrom" TextMode="Date" runat="server" CssClass="ml-2"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <asp:Label ID="lblFinYearTo" runat="server" TabIndex="2" ToolTip="Financial Year End Date" Text="Financial Year End Date :" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtDateTo" TextMode="Date" runat="server" CssClass="ml-2"></asp:TextBox>
                                        </div>



                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="3" ToolTip="Submit Financial Year." ValidationGroup="submit" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4" ToolTip="Cancel" CssClass="btn btn-warning" />

                                </div>

                            </asp:Panel>
                        </div>
                        <div id="divMsg" runat="server"></div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

