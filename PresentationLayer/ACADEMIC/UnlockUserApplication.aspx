<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UnlockUserApplication.aspx.cs"
    Inherits="ACADEMIC_UnlockUserApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAppliid"
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

    <asp:UpdatePanel ID="updAppliid" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Unlock User Application</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application ID</label>
                                        </div>
                                        <asp:TextBox ID="txtUserName" runat="server" TabIndex="1"
                                            MaxLength="20" CssClass="form-control" Wrap="False" ValidationGroup="appli" />
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                                            ControlToValidate="txtUserName" Display="None" ErrorMessage="Please enter Application id."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="2" ValidationGroup="Show"
                                            OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Details</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="divStudentInfo" style="display: block;">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStudentname" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Date of Birth :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDateOfBirth" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Degree :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="true" /></a>
                                                    </li>

                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Mobile No :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMobile" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Email ID :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEmail" runat="server" Font-Bold="true" /></a>
                                                    </li>


                                                    <li class="list-group-item"><b>Pin Code :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPinCode" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Application Status :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Address :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAddress" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnUnlock" runat="server" Text="Unlock Application"
                                            ValidationGroup="appli" CssClass="btn btn-primary" ToolTip="Unlock Application" OnClick="btnUnlock_Click" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" ToolTip="Cancel"
                                            CausesValidation="False" OnClick="btnCancel_Click" />
                                    </div>

                                    <div id="divMsg" runat="server">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

