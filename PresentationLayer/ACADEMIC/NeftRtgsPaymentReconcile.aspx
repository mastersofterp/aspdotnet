<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NeftRtgsPaymentReconcile.aspx.cs" Inherits="ACADEMIC_NeftRtgsPaymentReconcile" MasterPageFile="~/SiteMasterPage.master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReconcile"
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

    <asp:UpdatePanel ID="updReconcile" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">NEFT/RTGS PAYMENT RECONCILE</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceipt" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlReceipt_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReceipt" ErrorMessage="Please Select Receipt Type" Display="None" ValidationGroup="Show" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceipt" ErrorMessage="Please Select Receipt Type" Display="None" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester" ErrorMessage="Please Select Semester" Display="None" ValidationGroup="Show" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSemester" ErrorMessage="Please Select Semester" Display="None" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" TabIndex="3" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" />
                                        <asp:Button ID="btnSubmit" runat="server" TabIndex="4" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Submit" Enabled="false" />
                                        <asp:Button ID="btnExcel" runat="server" TabIndex="5" Text="Report(Excel)" CssClass="btn btn-info" OnClick="btnExcel_Click" ValidationGroup="Show" />
                                        <asp:Button ID="btnCancel" runat="server" TabIndex="5" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="vsSubmit" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                    </div>

                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlReconcile" runat="server" Visible="true">
                                    <asp:ListView ID="lvReconcile" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Students NEFT/RTGS Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tbllist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Select
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this);" />
                                                        </th>
                                                        <th>Registration No.
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Transaction ID
                                                        </th>
                                                        <th>Transaction Date
                                                        </th>
                                                        <th>Bank Name
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                        <th>Download Receipt
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkSelect" runat="server" TabIndex="6" Enabled='<%# (Convert.ToInt32(Eval("RECON") )== 1 ? false:true)%>' Checked='<%# (Convert.ToInt32(Eval("RECON") )== 1 ? true:false)%>' />
                                                    <asp:HiddenField ID="hdnIdno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    <asp:HiddenField ID="hdnSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' TabIndex="7"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' TabIndex="8"></asp:Label>
                                                </td>
                                                <th>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' TabIndex="9"></asp:Label>
                                                </th>
                                                <td>
                                                    <asp:Label ID="lblTransactionID" runat="server" Text='<%# Eval("TRANSACTIONID") %>' TabIndex="10"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("TRANSACTION_DATE") %>' TabIndex="11"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BANK_NAME") %>' TabIndex="12"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>' TabIndex="13"></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:ImageButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" Visible='<%# Convert.ToString(Eval("FILE_UPLOAD"))== string.Empty ? false:true %>' ImageUrl="~/Images/down-arrow.png" CommandArgument='<%# Eval("FILE_UPLOAD") %>' TabIndex="14" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" ForeColor='<%# (Convert.ToInt32(Eval("RECON") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                        Text='<%# (Convert.ToInt32(Eval("RECON") )== 1 ?  "Reconciled" : "Pending" )%>' runat="server" TabIndex="15"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:HiddenField ID="hdncount1" runat="server" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvReconcile" />
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function SelectAll(chk) {
            //debugger;
            var hdnVal = document.getElementById('<%= hdncount1.ClientID %>');
            //alert(hdnVal.value);


            for (i = 0; i < hdnVal.value; i++) {
                // alert('aa');
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvReconcile_ctrl' + i + '_chkSelect');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
