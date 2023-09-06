<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeCertificateReports.aspx.cs" Inherits="PAYROLL_REPORTS_EmployeeCertificateReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE CERTIFICATE</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Employee Certificate </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" TabIndex="2" ToolTip="Select College" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                   >
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="Payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaffNo" runat="server" ToolTip="Select Staff" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                                    TabIndex="3" AutoPostBack="true"  OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff"
                                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Employee</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" TabIndex="7" data-select2-enable="true"
                                                    CssClass="form-control" ToolTip="Select Employee" OnSelectedIndexChanged="ddlEmployeeNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlEmployeeNo" Display="None" ErrorMessage="Please Select Employee"
                                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Purpose :</label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtforpurpose" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                              <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Remark :</label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtremark" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                    <asp:Button ID="btnCharacterCertificate" runat="server" Text="Character Certificate" ToolTip="Click to  show Employee Character Certificate" OnClick="btnCharacterCertificate_Click" CssClass="btn btn-primary"
                                        TabIndex="8" ValidationGroup="Payroll" />
                                    <asp:Button ID="btnAddressCertificate" runat="server" ToolTip="Click to show Employee Address Certificate Report" Text="Address Certificate" OnClick="btnAddressCertificate_Click"
                                        TabIndex="9" ValidationGroup="Payroll" CssClass="btn btn-primary" />
                                     <asp:Button ID="btnExperienceCertificate" runat="server" ToolTip="Click to Show the Employee Experience Certificate Report" Text="Expereince Certificate" OnClick="btnExperienceCertificate_Click"
                                        TabIndex="9" ValidationGroup="Payroll" CssClass="btn btn-primary" />
                                     <asp:Button ID="btnNoObjectionCertificate" runat="server" ToolTip="Click to Show the Employee No Objection Certificate Report" Text="No Objection Certificate" OnClick="btnNoObjectionCertificate_Click"
                                        TabIndex="9" ValidationGroup="Payroll" CssClass="btn btn-primary" />
                                   
                                        <asp:Button ID="btnEmployeeCertificate" runat="server" ToolTip="Click to Show the Employee Certificate Report" Text="Employee Certificate" OnClick="btnEmployeeCertificate_Click"
                                        TabIndex="9" ValidationGroup="Payroll" CssClass="btn btn-primary" />
                                 
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click To Reset"  OnClick="btnCancel_Click"
                                        TabIndex="10" />
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>


            <div id="divMsg" runat="server">
            </div>

            <script type="text/javascript">
                function DisableDropDownList(disable) {
                    document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
                    document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCharacterCertificate" />
            <asp:PostBackTrigger ControlID="btnAddressCertificate" />
            <asp:PostBackTrigger ControlID="btnExperienceCertificate" />
            <asp:PostBackTrigger ControlID="btnNoObjectionCertificate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

