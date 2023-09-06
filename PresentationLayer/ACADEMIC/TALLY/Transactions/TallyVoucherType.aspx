<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="TallyVoucherType.aspx.cs"
    Inherits="ACADEMIC_PublicationDetail" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTallyVoucher"
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
    <asp:UpdatePanel ID="updTallyVoucher" runat="server">
        <ContentTemplate>
          <%--  <div class="col-12 mt-3">--%>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">TALLY VOUCHER TYPE</h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    Cash Voucher Name
                                                </label>
                                            </div>
                                            <asp:TextBox ID="txtCashVName" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCashName" runat="server" ControlToValidate="txtCashVName" Display="None" ErrorMessage="Please Enter Cash Voucher Name" ValidationGroup="Submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    Bank Voucher Name
                                                </label>
                                            </div>
                                            <asp:TextBox ID="txtBankVName" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="txtBankVName" Display="None" ErrorMessage="Please Enter Bank Voucher Name" ValidationGroup="Submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                                        ValidationGroup="Submit" TabIndex="3" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Cancel"
                                        TabIndex="4" />
                                    <asp:ValidationSummary ID="vldSummery" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Submit" />

                                </div>                        
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divMsg" runat="server">
                </div>
          <%--  </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
