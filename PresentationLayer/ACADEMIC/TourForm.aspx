<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TourForm.aspx.cs" Inherits="ACADEMIC_TourForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dvMain" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Session</label>--%>
                                                <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Session."
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="submit">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Receipt Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRecType" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" Enabled="false"
                                                AppendDataBoundItems="true"
                                                ValidationGroup="submit" ToolTip="Please Select Receipt Type">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRecType" runat="server" ErrorMessage="Please Select Receipt Type"
                                                Display="None" ControlToValidate="ddlRecType" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Genrate Tour-Form" OnClick="btnSubmit_Click" ValidationGroup="submit"
                                        class="buttonStyle ui-corner-all" CssClass="btn btn-primary" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" class="buttonStyle ui-corner-all"
                                        CssClass="btn btn-warning" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btncancel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server" />
</asp:Content>
