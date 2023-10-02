<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Convo_Stud_Apply.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Convo_Stud_Apply" %>

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

    <div id="divMsg" runat="server">
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Student Record Upload</h3>--%>
                            <h3 class="box-title">
                                Student Convocation Apply</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="col-12 mt-md-5 mt-3 std-det">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Student Details</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Name of Student :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblregno" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Name of Student :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <%--<li class="list-group-item"><b>Degree Award Ceremony Date :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblConvoDate" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>--%>
                                                    <li class="list-group-item"><b>Program Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDegree" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Branch Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                     <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblsem" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Email ID :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Mobile No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMobile" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Address :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAddress" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Class Obtained :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblClassObtained" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Convocation Fees :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblFees" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnPay" runat="server" CssClass="btn btn-outline-primary" Text="Make Payment" OnClick="btnPay_Click" />

                                        <asp:Button ID="btnReciept" runat="server" Visible="true" Text="Receipt" TabIndex="5"
                                            CssClass="btn btn-outline-primary" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="academic" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="sheet" />
                                    </div>

                                    <div class="col-12">
                                        <asp:Label ID="lblTotalMsg" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnsheet" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

