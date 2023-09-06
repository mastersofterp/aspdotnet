<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlineAdmissionReportPortal.aspx.cs" Inherits="ACADEMIC_OnlineAdmissionReportPortal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUser"
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

    <asp:UpdatePanel ID="updpnlUser" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Online Admission Portal Reports</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAdmbatch" runat="server" ErrorMessage="Please Select Admission batch" ControlToValidate="ddlAdmbatch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server" ErrorMessage="Please Select Admission batch" ControlToValidate="ddlAdmbatch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="StatasticsReport"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ErrorMessage="Please Select Branch" ControlToValidate="ddlBranch" SetFocusOnError="true" Display="None" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnApplystudList" runat="server" Text="Apply Applicant List" CssClass="btn btn-primary" OnClick="btnApplystudList_Click" ValidationGroup="Report" />
                                <asp:Button ID="btnRecStudList" runat="server" Text="Received Student List" CssClass="btn btn-primary" OnClick="btnRecStudList_Click" ValidationGroup="Report" Visible="false" />   <%--visible false by Amit karamkar on 2019 feb 07 --%>
                                <asp:Button ID="btnPendingStudList" runat="server" Text="Pending Student List" CssClass="btn btn-primary" OnClick="btnPendingStudList_Click" ValidationGroup="Report" Visible="false" />          <%--visible false by Amit karamkar on 2019 feb 07 --%>
                                <asp:Button ID="btnExport" runat="server" Text="Export to ExcelSheet" CssClass="btn btn-primary" ValidationGroup="Report" OnClick="btnExport_Click" />
                                <asp:Button ID="btnRegCountinExcel" runat="server" Text="Applicant Count in Excel" CssClass="btn btn-primary" OnClick="btnRegCountinExcel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" CssClass="btn btn-primary" ShowSummary="False" ValidationGroup="Report" />
                            
                                <asp:Button ID="btnApplirecvCount" runat="server" Text="Application Received Statastics" CssClass="btn btn-primary" ValidationGroup="Report" OnClick="btnApplirecvCount_Click" />
                                <asp:Button ID="btnDeptStudList" runat="server" Text="Departmentwise Student List" ValidationGroup="Report" CssClass="btn btn-primary" OnClick="btnDeptStudList_Click" />
                                <asp:Button ID="btnRegCount" runat="server" Text="Applicant Count" CssClass="btn btn-primary" OnClick="btnRegCount_Click" />
                                <asp:Button ID="btnFeeCount" runat="server" Text="Applicant Fees Count" CssClass="btn btn-primary" OnClick="btnFeeCount_Click" />
                                <asp:Button ID="btnRegCountDatewise" runat="server" Text="Applicant Count Datewise" CssClass="btn btn-primary" OnClick="btnRegCountDatewise_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="StatasticsReport" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApplystudList" />
            <asp:PostBackTrigger ControlID="btnRecStudList" />
            <asp:PostBackTrigger ControlID="btnPendingStudList" />
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnApplirecvCount" />
            <asp:PostBackTrigger ControlID="btnDeptStudList" />
            <asp:PostBackTrigger ControlID="btnRegCount" />
            <asp:PostBackTrigger ControlID="btnRegCountDatewise" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnRegCountinExcel" />
            <asp:PostBackTrigger ControlID="btnFeeCount" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server"></div>
</asp:Content>

