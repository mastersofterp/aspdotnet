<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UserPaymentReconcilation.aspx.cs" Inherits="ACADEMIC_UserPaymentReconcilation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script>
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#example').DataTable({
            });
        }
    </script>--%>

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
                            <h3 class="box-title">PAYMENT RECONCILIATION</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <asp:RadioButtonList ID="rblSelection" runat="server" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="true">
                                            <asp:ListItem Value="0" Selected="True">Admission Batchwise&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">Search</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divAdmBatch" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" />
                                        <asp:RequiredFieldValidator ID="rfvddlAdmbatch" runat="server" ErrorMessage="Please Select Admission batch" ControlToValidate="ddlAdmbatch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree." ControlToValidate="ddlDegree" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                            <asp:ListItem Value="">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">DD PAYMENT</asp:ListItem>
                                            <asp:ListItem Value="1">CASH PAYMENT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divSearch" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search by Application ID</label>
                                        </div>
                                        <asp:TextBox ID="txtAppliid" runat="server" CssClass="form-control" placeholder="Search here..." />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnApplystudList_Click" CssClass="btn btn-primary" ValidationGroup="Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                            </div>

                            <div class="col-12">
                                <asp:Panel runat="server" ID="pnlStudentList" Visible="False">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Student Not found" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Students List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Application Id</th>
                                                        <th>DD No</th>
                                                        <th>DD Amount</th>
                                                        <th>Transaction Date</th>
                                                        <th>Bank Name</th>
                                                        <th>Branch</th>
                                                        <th>Remark</th>
                                                        <th>Reconcile</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                            <tr class="item">
                                                <td><%# Eval("FIRSTNAME")%><asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                </td>
                                                <td><%# Eval("USERNAME")%></td>
                                                <td><%# Eval("DD_CHEQUENO")%></td>
                                                <td><%# Eval("AMOUNT")%></td>
                                                <td><%# Eval("TRANSDATE")%></td>
                                                <td><%# Eval("BANKNAME")%></td>
                                                <td><%# Eval("BRANCHNAME")%></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtremark" Text='<%# Eval("REMARK")%>' placeholder="Please enter remark." /></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                    <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <AlternatingItemTemplate>
                                            <tr class="altitem">
                                                <td><%# Eval("FIRSTNAME")%><asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                </td>
                                                <td><%# Eval("USERNAME")%></td>
                                                <td><%# Eval("DD_CHEQUENO")%></td>
                                                <td><%# Eval("AMOUNT")%></td>
                                                <td><%# Eval("TRANSDATE")%></td>
                                                <td><%# Eval("BANKNAME")%></td>
                                                <td><%# Eval("BRANCHNAME")%></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtremark" Text='<%# Eval("REMARK")%>' placeholder="Please enter remark." /></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                    <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>

                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel runat="server" ID="pnlStudentlistCash" Visible="False">

                                    <asp:ListView ID="lvStudentCash" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Student Not found" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                            <h5>Students List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                            <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr No</th>
                                                        <th>Application Id</th>
                                                        <th>Name</th>
                                                        <th>Category
                                                            <%--<p style="padding-top: 20px; margin: 0">Category</p>--%>
                                                        </th>
                                                        <th>
                                                            <p>Bank Name</p>
                                                        </th>
                                                        <th>Branch Name</th>
                                                        <th>Transaction No</th>
                                                        <th>Transaction Date</th>
                                                        <th>Amount</th>
                                                        <th>Remark</th>
                                                        <th>Reconcile</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td><%# Eval("USERNAME")%></td>
                                                <td>
                                                    <%# Eval("FIRSTNAME")%>
                                                    <asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblCATEGORY" Text='<%# Eval("CATEGORY")%>' />
                                                </td>

                                                <%--<td><%# Eval("CATEGORY")%></td>--%>
                                                <td>
                                                    <asp:Label runat="server" ID="lblBANKNAME" Text='<%# Eval("BANKNAME")%>' />
                                                </td>
                                                <%--<td><%# Eval("BANKNAME")%></td>--%>
                                                <td><%# Eval("BRANCHNAME")%></td>

                                                <td><%# Eval("TRANSACTIONID")%></td>
                                                <td><%# Eval("TRANSDATE")%></td>
                                                <td><%# Eval("AMOUNT")%></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtremark" Text='<%# Eval("REMARK")%>' Style="width: 148px" placeholder="Please enter remark." /></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                    <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem">
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td><%# Eval("USERNAME")%></td>
                                                <td><%# Eval("FIRSTNAME")%><asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                </td>
                                                <td><%# Eval("CATEGORY")%></td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblBANKNAME" Text='<%# Eval("BANKNAME")%>' />
                                                </td>

                                                <%--<td><%# Eval("BANKNAME")%></td>--%>
                                                <td><%# Eval("BRANCHNAME")%></td>

                                                <td><%# Eval("TRANSACTIONID")%></td>
                                                <td><%# Eval("TRANSDATE")%></td>
                                                <td><%# Eval("AMOUNT")%></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtremark" Text='<%# Eval("REMARK")%>' placeholder="Please enter remark." /></td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                    <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button runat="server" ID="btnRecon" CssClass="btn btn-primary" Visible="false" TabIndex="5" Text="Reconcile" ValidationGroup="Submit" OnClick="btnRecon_Click" />&nbsp;
                                <asp:Button runat="server" ID="btnReport" CssClass="btn btn-info" Visible="false" TabIndex="6" Text="Report" ValidationGroup="report" OnClick="btnReport_Click" />&nbsp;
                                <asp:Button runat="server" ID="btnexport" Visible="false" CssClass="btn btn-info" TabIndex="6" Text="ExportToExcel" ValidationGroup="report" OnClick="btnexport_Click" />
                                <asp:ValidationSummary ID="valsub" runat="server" DisplayMode="List" CssClass="btn btn-warning" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnRecon" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnexport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

