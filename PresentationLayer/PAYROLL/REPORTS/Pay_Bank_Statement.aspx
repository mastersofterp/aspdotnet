<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Bank_Statement.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_Bank_Statement" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BANK STATEMENT AND OTHERS</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Month / Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" ToolTip="Select Month/Year" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                               <%-- <sup>* </sup>--%>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="2" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12 h-100">
                                            <div class="label-dynamic">
                                                 <%--<sup>* </sup>--%>
                                                <%--<label>Scheme</label>--%>
                                                <label>Scheme/Staff</label>
                                            </div>
                                           <%-- <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Scheme" AutoPostBack="true" OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="3">--%>

                                                <%-- <asp:ListItem Value="0" >Please select</asp:ListItem>--%>
                                          <%--  </asp:DropDownList>--%>
                                             <asp:ListBox ID="lstStaffNo" SelectionMode="Multiple" ToolTip="Please Select Scheme/Staff" runat="server" CssClass="form-control" OnSelectedIndexChanged="lstStaffNo_SelectedIndexChanged" style="height: 150px!important"></asp:ListBox>
                                          
                                              <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                               ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Scheme"
                                               ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                      --%>
                                        </div>
                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12 h-100">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>Bank Name</label>
                                            </div>
                                            <%-- <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged"
                                                        AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Bank Name">
                                                    </asp:DropDownList>--%>
                                            <asp:ListBox ID="lbBank" SelectionMode="Multiple" ToolTip="Please Select Bank" runat="server" CssClass="form-control" OnSelectedIndexChanged="lbBank_SelectedIndexChanged"  style="height: 150px!important"></asp:ListBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvBank" runat="server" SetFocusOnError="true"
                                                ControlToValidate="lbBank" Display="None" ErrorMessage="Please Select Bank Name"
                                                ValidationGroup="Payroll"></asp:RequiredFieldValidator>--%>
                                        </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<label>Staff</label>--%>
                                                <label>Employee Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" ToolTip="Select Employee Type" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeType_SelectedIndexChanged" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="4">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Bank Account Number</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMappingAccNo" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="5">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlMappingAccNo" Display="None" ErrorMessage="Please Select Account Number"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Order By</label>
                                            </div>
                                            <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="6" ToolTip="Select Order by">
                                                <asp:ListItem Value="E.PFILENO" Text="Emp Code."></asp:ListItem>
                                                <asp:ListItem Value="E.Fname" Text="Name"></asp:ListItem>
                                                <asp:ListItem Value="E.IDNO" Text="IDNO"></asp:ListItem>
                                                <asp:ListItem Value="E.SEQ_NO" Text="SEQ NO."></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCertificate" runat="server">
                                            <div class="label-dynamic">
                                                <label>Salary Certificate Issued to</label>
                                            </div>
                                            <asp:TextBox ID="txtBankName" ToolTip="Enter Salary Certificate Issued to" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnShow" runat="server" Text="Bank Statement"
                                    OnClick="btnShow_Click" CssClass="btn btn-info" TabIndex="8" ToolTip="Click to Show Bank Statement"
                                    ValidationGroup="Payroll" />
                                <asp:Button ID="btnBankSuppli" runat="server" Text="Bank Statement Supplementary"
                                    OnClick="btnBankSuppli_Click" CssClass="btn btn-info" TabIndex="8" ToolTip="Click to Show Bank Statement Supplementary"
                                    ValidationGroup="Payroll" />


                                <asp:Button ID="btnBankStatment_Format2" runat="server" Text="Bank Statement Format2"
                                    CssClass="btn btn-info" TabIndex="8" ToolTip="Click to Show Bank Statement"
                                    ValidationGroup="Payroll" OnClick="btnBankStatment_Format2_Click" />


                                <asp:Button ID="btnreportexcel" runat="server" Text="Bank Excel"
                                    CssClass="btn btn-info" TabIndex="9" ToolTip="Click to Show Bank Excel"
                                    ValidationGroup="Payroll" OnClick="btnreportexcel_Click" />
                                 <asp:Button ID="btnexporttoexcelacc" runat="server" Text="Bank Excel Without Account"
                                    CssClass="btn btn-info" TabIndex="9" ToolTip="Click to Show Bank Excel"
                                    ValidationGroup="Payroll" OnClick="btnexporttoexcelacc_Click" />

                                <asp:Button ID="BtnNEFT" runat="server" Text="NEFT Statement"
                                    CssClass="btn btn-info" TabIndex="9" ToolTip="Click to Show NEFT Statement"
                                    ValidationGroup="Payroll" OnClick="btnreportNEFT_Click" />

                                <asp:Button ID="BtnNEFTExcel" runat="server" Text="NEFT Excel"
                                    CssClass="btn btn-info" TabIndex="9" ToolTip="Click to Show NEFT Excel"
                                    ValidationGroup="Payroll" OnClick="btnNEFTexcel_Click" />

                                <asp:Button ID="btnBankText" runat="server" Text="Bank Text"
                                    CssClass="btn btn-info" TabIndex="9" ToolTip="Click to Show Bank Text"
                                    ValidationGroup="Payroll" OnClick="btnBankText_Click" />

                                <asp:Button ID="btnPFStatement" runat="server" Text="PF Statement"
                                    CssClass="btn btn-info" TabIndex="10" ToolTip="Click to Show PF Statement"
                                    ValidationGroup="Payroll" OnClick="btnPFStatement_Click" />

                                <asp:Button ID="btnPFexcel" runat="server" Text="PF Excel"
                                    CssClass="btn btn-info" TabIndex="11" ToolTip="Click to Show PF Excel"
                                    ValidationGroup="Payroll" OnClick="btnPFexcel_Click" />

                                <asp:Button ID="btnITStatement" runat="server" Text="IT Statement"
                                    CssClass="btn btn-info" TabIndex="12" ToolTip="Click to Show IT Statement"
                                    ValidationGroup="Payroll" OnClick="btnITStatement_Click" />



                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                    OnClick="btnCancel_Click" TabIndex="13" ToolTip="Click to Reset" />
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                <div class="col-md-12">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnBankStatment_Format2" />
            <asp:PostBackTrigger ControlID="btnreportexcel" />
            <asp:PostBackTrigger ControlID="btnPFStatement" />
            <asp:PostBackTrigger ControlID="btnBankText" />
            <asp:PostBackTrigger ControlID="btnPFexcel" />
            <asp:PostBackTrigger ControlID="btnBankSuppli" />
            <asp:PostBackTrigger ControlID="btnITStatement" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="BtnNEFT" />
            <asp:PostBackTrigger ControlID="BtnNEFTExcel" />
              <asp:PostBackTrigger ControlID="btnexporttoexcelacc" />

        </Triggers>
    </asp:UpdatePanel>



    <div id="divMsg" runat="server"></div>
    <script type="text/javascript">
        function DisableDropDownList(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
</asp:Content>

