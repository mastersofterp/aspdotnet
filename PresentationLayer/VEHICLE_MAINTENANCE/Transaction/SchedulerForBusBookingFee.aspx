<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SchedulerForBusBookingFee.aspx.cs" Inherits="ACADEMIC_Automatic_Payment_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Updlog" runat="server">
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
                    <div class="col-md-12" id="pnlSelection" runat="server">
                        <div class="col-md-12">
                             <div class="row">
                       <div class="form-group col-lg-3 col-md-3 col-12">
                            <div class="label-dynamic">
                                <label></label>
                            </div>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Text="Click To Start Auto Schedular"  OnClick="btnSubmit_Click" />
                        </div>
                         <div class="form-group col-lg-1 col-md-3 col-12">
                            <div class="label-dynamic">
                                <label></label>
                            </div>
                            <asp:Button ID="btnStatusReport" runat="server" CssClass="btn btn-info" Text="Online Fees Transaction Log Report(Excel)" OnClick="btnStatusReport_Click" Visible="false" />
                        </div>
                                 </div>
                            </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-12">
                                <asp:Label ID="lblSuccessStatus" CssClass="form-control" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblFailedStatus" CssClass="form-control" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnStatusReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
