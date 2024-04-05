<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ProvisionalCertificate.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ProvisionalCertificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<<<<<<< HEAD
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUpdate"
=======
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFacAllot"
>>>>>>> b7004b39 ( [ENHANCEMENT] [57150] Solved bugs and Added RPT)
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
<<<<<<< HEAD
    </div>
    <div runat="server" id="divDetails">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">PROVISIONAL CERTIFICATE</h3>
                    </div>
                    <div class="box-body">
                        <asp:UpdatePanel ID="updUpdate" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row" runat="server" id="divRegistrationNo" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Registration No</label>
                                            </div>
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtRegistrationNo" data-select2-enable="true"></asp:TextBox>
=======
    </div>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFacAllot"
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
    <asp:UpdatePanel runat="server" ID="updFacAllot">
        <ContentTemplate>
            <div runat="server" id="divDetails">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <%--<div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUpdate"
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
                                </div>--%>
                                <%--<asp:UpdatePanel ID="updUpdate" runat="server">
                                    <ContentTemplate>--%>
                                <div class="col-12">
                                    <div class="row" runat="server" id="divRegistrationNo" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Registration No</label>
                                            </div>
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtRegistrationNo"></asp:TextBox>
>>>>>>> b7004b39 ( [ENHANCEMENT] [57150] Solved bugs and Added RPT)
                                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                                Display="None" ErrorMessage="Please Enter Registration No." InitialValue="" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click"
<<<<<<< HEAD
                                                Text="Show Student Detail" CssClass="btn btn-primary" ValidationGroup="report" data-select2-enable="true" />
=======
                                                Text="Show Student Detail" CssClass="btn btn-primary" ValidationGroup="report" />
>>>>>>> b7004b39 ( [ENHANCEMENT] [57150] Solved bugs and Added RPT)
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="report"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" id="divStudDetails" runat="server" visible="false">
                                    <div id="divProvCert" runat="server">
                                        <div class="sub-heading">
                                            <h5>Student Detail </h5>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Mobile No :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMobileNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
<<<<<<< HEAD
                                                   
=======

>>>>>>> b7004b39 ( [ENHANCEMENT] [57150] Solved bugs and Added RPT)
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Reg. No :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblRegNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Email ID  :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEmailId" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
<<<<<<< HEAD
                                             <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>DGPA :</b>
=======
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>SGPA :</b>
>>>>>>> b7004b39 ( [ENHANCEMENT] [57150] Solved bugs and Added RPT)
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDGPA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
<<<<<<< HEAD
                                                  
=======

>>>>>>> b7004b39 ( [ENHANCEMENT] [57150] Solved bugs and Added RPT)
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:HiddenField ID="hdfSessionno" runat="server" />
                                        <asp:Button ID="btnPrint" Visible="false" runat="server" OnClick="btnPrint_Click"
                                            Text="Print Provisional Certificate" CssClass="btn btn-info" ValidationGroup="report" />
                                    </div>
                                </div>
<<<<<<< HEAD
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnPrint" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        </div>
        <div id="divMsg" runat="server">
        </div>
=======
                                <%-- </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnPrint" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
>>>>>>> b7004b39 ( [ENHANCEMENT] [57150] Solved bugs and Added RPT)
</asp:Content>

