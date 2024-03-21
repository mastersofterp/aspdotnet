<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Applicant_Username_Password_Dump.aspx.cs" Inherits="ACADEMIC_REPORTS_Applicant_Username_Password_Dump" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server"
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

    <asp:UpdatePanel ID="updApplication" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Applicant Username and Password Dump</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application Number</label>
                                        </div>
                                        <asp:TextBox ID="txtApplicationNumber" runat="server" TabIndex="1" MaxLength="20" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                                            ControlToValidate="txtApplicationNumber" Display="None" ErrorMessage="Please Enter Application Number"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="1" ValidationGroup="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click"/>
                                        <asp:ValidationSummary ID="vsDump" runat="server" ValidationGroup="Show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                    <asp:Panel ID="pnlApplicantUserName" runat="server" Visible="true">
                        <asp:ListView ID="lvApplicantUserName" runat="server" Visible="true">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divApplicantUserName">
                                    <thead class="bg-light-blue">
                                        <tr>                                           
                                            <th style="text-align: center; width: 20%">Application Number</th>
                                            <th style="text-align: center; width: 20%">Password</th>
                                            <th style="text-align: center; width: 20%">Student Name</th>
                                            <th style="text-align: center; width: 20%">Email ID</th>
                                            <th style="text-align: center; width: 20%">Mobile Number</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:UpdatePanel runat="server" ID="updApplicationNumber">
                                    <ContentTemplate>
                                        <tr>
                                            <td style="text-align:center">
                                                <%# Eval("USERNAME")%>
                                            </td>
                                            <td  style="text-align:center">
                                                <asp:Label ID="lblDecryptedPassword" runat="server" Text='<%# DecryptPassword_Adm(Eval("USER_PASSWORD").ToString()) %>'></asp:Label>
                                            </td>
                                            <td  style="text-align:center">
                                                <%# Eval("STUD_NAME")%>
                                            </td>
                                            <td  style="text-align:center">
                                                <%# Eval("EMAILID")%>
                                            </td>
                                           <td  style="text-align:center">
                                                <%# Eval("MOBILENO")%>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

        <div id="popup" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal" id="myModalPopUp" data-backdrop="static">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                <div class="col-12 mt-3">
                                    <h5 class="heading">Please enter password to access this page.</h5>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <%--  <label>PASSWORD</label>--%>
                                            <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                MaxLength="50" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass"
                                                Display="None" ValidationGroup="password"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                        </div>
                                        <div class="btn form-group col-lg-12 col-md-12 col-12">
                                            <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary"
                                                runat="server" Text="Submit" OnClick="btnConnect_Click" ValidationGroup="password" />
                                            <asp:Button ID="btnCancel1" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="2" CssClass="btn btn-danger"
                                                runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="password" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConnect" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


        <script type="text/javascript">
            $(window).on('load', function () {
                $('#myModalPopUp').modal('show');
            });
    </script>

</asp:Content>

