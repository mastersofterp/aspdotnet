<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Feespayment.aspx.cs" Inherits="ACADEMIC_Feespayment" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <asp:Panel ID="pnlTransferCredit" runat="server">

        <div class="row">
            <div class="col-md-12 col-sm-12 col-12" id="fees" runat="server">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">FEES PAYMENT</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Enroll No :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblapp" runat="server" Text="" Font-Bold="false"></asp:Label></a>

                                        </li>

                                    </ul>
                                </div>

                                <div class="form-group col-lg-4 col-md-6 col-12">

                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Name :</b><a class="sub-label">
                                                <asp:Label ID="lblname" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                                        </li>


                                        <%-- <li class="list-group-item">--%>
                                        <b hidden="hidden">Total Fees :</b><%--<a class="sub-label">--%>
                                        <asp:Label ID="lbltotalfees" runat="server" Text="0.00" Font-Bold="false" Visible="false"></asp:Label><%--</a></li>--%>
                                    </ul>
                                </div>
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Semester Name :</b><a class="sub-label">
                                                <asp:Label ID="lblSem" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                                        </li>
                                    </ul>
                                    <ul class="list-group list-group-unbordered" hidden="hidden">
                                        <li class="list-group-item">
                                            <b>Current Semester :</b><a class="sub-label">
                                                <asp:Label ID="Label2" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                                        </li>


                                        <li class="list-group-item">
                                            <b>Email Id :</b><a class="sub-label">
                                                <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                                        </li>

                                        <li class="list-group-item">
                                            <b>Mobile Number :</b><a class="sub-label">
                                                <asp:Label ID="lblmobile" runat="server" Text="" Font-Bold="false"></asp:Label></a></li>

                                        <li class="list-group-item">
                                            <b>Fee Type :</b>
                                            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlReceiptType"
                                                Display="None" ErrorMessage="Please Select Fee Type" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Show"></asp:RequiredFieldValidator>

                                            <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                        </li>


                                        <li class="list-group-item">
                                            <b>Late Fees :</b><a class="sub-label">
                                                <asp:Label ID="lblLateFee" runat="server" Text="0.00" Font-Bold="false"></asp:Label></a></li>
                                    </ul>

                                </div>
                            </div>
                        </div>



                        <div class="col-12">
                            <div class="row" hidden="hidden">

                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered mt-3">

                                        <li class="list-group-item">

                                            <b>Branch Name :</b><a class="sub-label">
                                                <asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="false"></asp:Label></a></li>
                                    </ul>
                                </div>

                                <div class="col-lg-4 col-md-6 col-12 mt-3">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Session</b>
                                            <a class="sub-label">
                                                <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>

                                    </ul>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Semester</label>
                                    </div>
                                    <asp:DropDownList ID="ddlsem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlsem_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsem"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Session</label>
                                    </div>
                                    <asp:DropDownList ID="ddlsession" runat="server" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <a class="sub-label">
                                        <asp:Label ID="lblSession" runat="server" Text="" Visible="false" Font-Bold="true"></asp:Label>
                                    </a>

                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlsession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Fee Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReceiptType"
                                        Display="None" ErrorMessage="Please Select Fee Type" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>

                                    <asp:Label ID="lblOrderID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                </div>


                            </div>
                        </div>

                        <div class="btn-footer col-12 text-center" hidden="hidden">
                            <asp:Button ID="btnPAY" runat="server" Text="Pay Now"
                                ValidationGroup="Show" CssClass="btn btn-primary" />

                            <asp:Button ID="btnReports" runat="server" Text="Print Receipt" Visible="false"
                                class="buttonStyle ui-corner-all btn btn-success" />

                            <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" TabIndex="15" CssClass="btn btn-warning" />--%>
                            <%--<asp:Button ID="btnReport" runat="server" Text="Report" CausesValidation="false"
                                        OnClick="btnReport_Click" TabIndex="16" Enabled="false" CssClass="btn btn-primary"/>--%>
                            <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="Show" />

                        </div>

                        <div class="col-12">
                            <%--<asp:Panel ID="pnl" runat="server" ScrollBars="Auto">--%>

                            <div class="sub-heading">
                                <h5>Installment Fees </h5>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <span style="color: #008d4c;">
                                    <asp:Label ID="lblRecpttype" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Visible="false"> : </asp:Label>
                                    <asp:Label ID="lblRecpttypeAmount" runat="server"></asp:Label>

                                </span>
                            </div>
                            <p class="text-center">
                                <asp:ListView ID="lvFeesPayment" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Installment </th>
                                                        <th>Amount </th>
                                                        <th>Receipt Type </th>
                                                        <th>Due Date </th>
                                                        <th>Transaction ID</th>
                                                        <th>Pay Type</th>
                                                        <th>Pay Status </th>
                                                        <th>Online Pay</th>
                                                        <%-- <th>Challan </th>--%>
                                                        <th>Receipt</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>

                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Installment")%></td>
                                            <td><%# Eval("Amount")%></td>
                                            <td>
                                                <asp:Label ID="lblReceiptCode" runat="server" Text='<%# Eval("ReceiptCode")%>'></asp:Label></td>
                                            <td><%# (Eval("DueDate").ToString() != string.Empty) ? (Eval("DueDate", "{0:dd-MMM-yyyy}")) : Eval("DueDate", "{0:dd-MMM-yyyy}")%></td>
                                            <%--<td><%# Eval("DueDate")%></td>--%>
                                            <td><%# Eval("ORDER_ID")%></td>
                                            <td><%# Eval("PAY_TYPE_Mode")%><asp:HiddenField ID="hdftransactionmode" runat="server" Value='<%# Eval("PAY_TYPE")%>' />
                                            </td>
                                            <%-- <td></td>s
                                        <td></td>--%>
                                            <td><%# Eval("RECON")%><asp:HiddenField ID="hfrecon" runat="server" Value='<%# Eval("reconval")%>' />
                                            </td>

                                            <td>
                                                <asp:Button ID="btnOnlinePay" runat="server" Enabled='<%# (Convert.ToInt32(Eval("RECONVAL") ) == 1 ?  false : true )%>' Text="Online Pay" CssClass="btn btn-primary" OnClick="btnOnlinePay_Click" CommandArgument='<%# Eval("Installment") %>' CommandName='<%# Eval("Amount") %>' />
                                            </td>
                                            <%--<td>                                            
                                            <asp:Button ID="btnChallan" runat="server" Text="Challan" class="buttonStyle ui-corner-all btn btn-success" OnClick="btnChallan_Click"  CommandArgument='<%# Eval("Installment") %>' CommandName='<%# Eval("Amount") %>'/>
                                        </td>--%>

                                            <td>
                                                <asp:Button ID="btnReceipt" runat="server" Text="Receipt" Enabled='<%# (Convert.ToInt32(Eval("RECONVAL") ) == 0 ?  false : true )%>' class="buttonStyle ui-corner-all btn btn-info" OnClick="btnReceipt_Click"
                                                    CommandArgument='<%# Eval("DCR_NO") %>' CommandName='<%# Eval("ORDER_ID") %>' />
                                            </td>
                                            <%--                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" TabIndex="10" ToolTip="Edit Record" />--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </p>
                            <%--  </asp:Panel>--%>
                        </div>


                        <div id="divMsg" runat="server">
                        </div>
                    </div>

                </div>

            </div>
        </div>


    </asp:Panel>


</asp:Content>

