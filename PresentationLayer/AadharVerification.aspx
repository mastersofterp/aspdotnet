<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AadharVerification.aspx.cs" Inherits="AadharVerification" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTimeTable"
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

    <asp:UpdatePanel ID="updTimeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><%--Aadhaar Authentication--%><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                           <%-- <label>Enter Enrollment No.</label>--%>
                                             <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                         <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />
                                        <span class="">
                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" Visible="false" TabIndex="4" />
                                            </a>
                                        </span>
                                        <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                            Display="None" ErrorMessage="Please Enter Enrollment Number." SetFocusOnError="true"
                                            ValidationGroup="studSearch" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShowInfo" runat="server" Text="Show Information" OnClick="btnShowInfo_Click"
                                            TabIndex="3" ValidationGroup="studSearch" CssClass="btn btn-info" />
                                         <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"
                                            TabIndex="3" CssClass="btn btn-danger" />
                                        <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="studSearch" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divStudentInfo" runat="server" style="display:none">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label"><asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label> </a>
                                            </li>
                                            <li class="list-group-item"><b>Registration No. :</b>
                                                <a class="sub-label"><asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>      
                                        </ul>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label> </a>
                                            </li>
                                            <li class="list-group-item"><b>Aadhaar Number :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblAadhar" runat="server" Font-Bold="True"></asp:Label>
                                                    <img runat="server" src="~/Images/NoVerified.png" id="img1" style=" height: 1%; width: 30px;" alt="Not verified" title="Aadhaar no. is not verified."/>
                                                    <img runat="server" src="~/Images/AadharVerified.jpg" id="img2" style=" height: 1%; width: 100px;" alt="Verified" title="Aadhaar no. is verified."/>
                                                </a>
                                            </li>      
                                        </ul>
                                    </div>

                                </div>
                            </div>        

                            <div class="col-12" id="divOption" runat="server">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Have you linked your mobile number with Aadhaar?</label>
                                    </div>
                                    <asp:RadioButtonList runat="server" ID="rdbSelect" OnSelectedIndexChanged="rdbSelect_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblError" runat="server" Text="Aadhar is not verified" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="col-12 btn-footer" id="divFooter" runat="server">
                                <img runat="server" src="~/Images/AadharVerified.jpg" style="margin-left: -5%;margin-top: -15px; height: 1%; width: 100px;" alt="Verified" title="Aadhaar no. is verified."/>
                                <h3 class="box-title"><b>Aadhaar User Details</b></h3>
                                
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Name :</b>
                                                <a class="sub-label"><asp:Label runat="server" ID="lblName" Font-Bold="true"></asp:Label> </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Date of birth :</b>
                                                <a class="sub-label"><asp:Label runat="server" ID="lblDOB" Font-Bold="true"></asp:Label> </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Gender :</b>
                                                <a class="sub-label"><asp:Label runat="server" ID="lblGender" Font-Bold="true"></asp:Label> </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ajaxCall(lnk) {
            debugger;
            window.open(lnk, '_blank');
        }
    </script>
</asp:Content>
