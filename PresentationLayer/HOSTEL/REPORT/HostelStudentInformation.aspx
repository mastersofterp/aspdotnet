<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelStudentInformation.aspx.cs" Inherits="HOSTEL_REPORT_HostelStudentInformation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudInfo"
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

    <asp:UpdatePanel ID="updStudInfo" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">HOSTEL STUDENT INFORMATION REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Hostel Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlHostelSessionNo" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                            ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Hostel Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlHostelNo" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ErrorMessage="Please Select Hostel Name"
                                            ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnPrintReport" runat="server" Text="Report" CssClass="btn btn-info"
                                    ToolTip="Show Report Under Selected Criteria."
                                    TabIndex="3" ValidationGroup="Submit" OnClick="btnPrintReport_Click" />
                                 <asp:Button ID="btnExcelReport" runat="server" Text="Excel Report" CssClass="btn btn-info"
                                    ToolTip="Get Excel Report Under Selected Criteria."
                                    TabIndex="3" ValidationGroup="Submit" OnClick="btnExcelReport_Click"/>
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                    TabIndex="4" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="vsStudent" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                            </div>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

