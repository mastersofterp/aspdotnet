<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NewAdmStudPayment.aspx.cs" Inherits="ACADEMIC_NewAdmStudPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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

    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Admission Fee Online/Offline Payment</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 btn-footer">
                                        <asp:RadioButton ID="rdoOffline" runat="server" GroupName="Report" Text="Offline Payment" OnCheckedChanged="rdoOffline_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoOnline" runat="server" GroupName="Report" Text="Online Payment" OnCheckedChanged="rdoOnline_CheckedChanged" AutoPostBack="true" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <div id="divOfflinePay" runat="server" visible="false">
                                            <asp:Button ID="btnPrintChallan" runat="server" Text="Download Bank Challan" OnClick="btnPrintChallan_Click"
                                            CssClass="btn btn-primary"/>
                                        </div>
                                        <div id="divOnlinePay" runat="server" visible="false">
                                            <asp:Button ID="btnPayOnline" runat="server" Text="Click to Pay Online" OnClick="btnPayOnline_Click"
                                            CssClass="btn btn-primary" />
                                        </div>
                                        <div runat="server" id="divCancel" visible="false">
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintChallan" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

