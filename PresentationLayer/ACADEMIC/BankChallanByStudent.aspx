<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BankChallanByStudent.aspx.cs" Inherits="ACADEMIC_ChallanReconciliationByStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divEnrollment">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Enrollment No.</label>--%>
                                    <asp:Label ID="lblDYtxtEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:TextBox ID="txtEnrollmentNo" runat="server" placeholder="Enter Enrollment No." TabIndex="1" ValidationGroup="submit" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvtxtEnrollmentNo" runat="server" ControlToValidate="txtEnrollmentNo"
                                    Display="None" ErrorMessage="Please Enter Enrollment No." InitialValue="" SetFocusOnError="true"
                                    ValidationGroup="Payment" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Receipt Type</label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" />
                                <asp:RequiredFieldValidator ID="rfvddlReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                    Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Payment" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Semester</label>--%>
                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="true" runat="server"
                                    CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" ErrorMessage="Please Select Semester" SetFocusOnError="true"
                                    Display="None" ControlToValidate="ddlSemester" InitialValue="0" ValidationGroup="Payment"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div id="divFeeDetails" class="col-12" runat="server" visible="false">
                        <div id="divFeeItems" runat="server">
                            <asp:Panel ID="Panel3" runat="server">
                                <asp:ListView ID="lvFeeItems" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Fee Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFeeItems">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr. No.
                                                    </th>
                                                    <th>Fee Items
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                                <tr class="item">
                                                    <td>&nbsp;
                                                    </td>
                                                    <td class="data_label">TOTAL AMOUNT:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalAmount" onkeydown="javascript:return false;" Style="text-align: right"
                                                            runat="server" CssClass="data_label" TabIndex="14" Enabled="false" />

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("FEE_LONGNAME")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFeeItemAmount" onblur="UpdateTotalAmount();" Enabled="false"
                                                    Text='<%# Eval("AMOUNT")%>' Style="text-align: right" runat="server" CssClass="data_label"
                                                    TabIndex="14" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>

                    <div class="col-12 btn-footer" runat="server" id="divMSG" visible="false">
                        <div>
                            <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                        </div>
                        <asp:Button ID="btnReceipt" runat="server" Text="Print Challan" OnClick="btnReceipt_Click" Visible="false"
                            ValidationGroup="Payment" CssClass="btn btn-info" />
                        <%--<asp:Button ID="btnPrint" runat="server" Text="Print Receipt" OnClick="btnPrint_Click" Visible="false" Enabled="false" 
                        ValidationGroup="search" CssClass="btn btn-primary" />--%>
                        <asp:ValidationSummary ID="vsbtn" runat="server" ShowSummary="false" DisplayMode="List" ShowMessageBox="true" ValidationGroup="Payment" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">

        function UpdateTotalAmount() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < (dataRows.length - 1) ; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(2);
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_txtTotalAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_txtTotalAmount').value = totalFeeAmt;
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>
    <div id="divMsg1" runat="server">
    </div>
</asp:Content>
