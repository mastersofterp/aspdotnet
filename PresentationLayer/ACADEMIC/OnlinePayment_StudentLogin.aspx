<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="OnlinePayment_StudentLogin.aspx.cs" Inherits="ACADEMIC_OnlinePayment_StudentLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="background-color: Aqua; padding-left: 5px">
                    <img src="../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait.....
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>PAYMENT</b></h3>
                      <div class="pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div>  
                </div>
                <asp:UpdatePanel ID="updBulkReg" runat="server">
                    <ContentTemplate>

                        <div class="box-body">
                            <div class="col-md-4">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Enrollment No. :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblRegNo" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Degree :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblDegree" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Student Name :</b>
                                        <a>
                                            <asp:Label ID="lblStudName" CssClass="data_label" runat="server" />
                                            <asp:Label ID="lblStudLastName" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Branch :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblBranchs" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Gender :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblSex" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-md-4">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Year :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblYear" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Batch :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblBatch" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Semester :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblSemester" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Mobile No. :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblMobileNo" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Email ID :</b>
                                        <%--<a class="pull-right">--%>
                                        <a>
                                            <asp:Label ID="lblEmailID" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                </ul>
                            </div>


                            <div class="col-md-4">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Payment Type :</b>
                                        <a class="pull-right">
                                            <asp:Label ID="lblPaymentType" CssClass="data_label" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Institute :</b>
                                        <%--<a class="pull-right">--%>
                                        <a>
                                            <asp:Label ID="lblCollege" CssClass="data_label" runat="server" />
                                            <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                                        </a>
                                    </li>
                                </ul>

                                <div class="form-group col-md-6">
                                    <label><span class="validstar" style="color: red">*</span>Receipt Type</label>

                                    <asp:DropDownList ID="ddlReceiptType" CssClass="form-control" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group col-md-6">
                                    <label><span class="validstar" style="color: red">*</span>Semester</label>

                                    <asp:DropDownList ID="ddlSemester" CssClass="form-control" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                            </div>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                &nbsp;<asp:UpdatePanel ID="up" runat="server">
                                    <ContentTemplate>

                                        <asp:Panel ID="pnlStudentsFees" runat="server" Visible="false">
                                            <asp:ListView ID="lvStudentFees" runat="server" OnItemDataBound="lvStudentFees_ItemDataBound" OnPreRender="lvStudentFees_PreRender">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div>
                                                            <b>Fees Details</b>
                                                        </div>
                                                        <table id="tblHead" class="table table-hover table-bordered">
                                                            <thead class="bg-light-blue">
                                                                <tr id="trRow">
                                                                    <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                    <th>SrNo.
                                                                    </th>
                                                                    <th>Fees Head
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>

                                                                </tr>

                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                            <%--<tfoot><asp:Label ID="lbltotal" runat="server"  Text="0"></asp:Label></tfoot>--%>
                                                            <thead class="bg-light-blue">
                                                                <tr id="Tr1" runat="server">
                                                                    <th></th>
                                                                    <th><span class="pull-right">Total</span></th>

                                                                    <th id="Td1" runat="server">
                                                                        <asp:Label ID="lbltotal" CssClass="data_label" runat="server" Text="0"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                        </table>

                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <%--<td style="width: 10%"></td>--%>
                                                        <td><%# Eval("SRNO") %>
                                                        </td>
                                                        <td><%# Eval("FEE_LONGNAME") %></td>
                                                        <td>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </asp:Panel>

                                        <div class="col-md-12" id="TRNote" runat="server" visible="false">

                                           <blockquote><span class="text-danger">**Note: If Installment or Fees Not Matched,Contact To Student Section!</span></blockquote>

                                        </div>
                                        <div class="col-md-12" runat="server" id="TRAmount">
                                            <li class="list-group-item">
                                                <b>Total Amount :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblOrderID" runat="server" CssClass="data_label">0</asp:Label>
                                                </a>
                                            </li>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Label ID="lblStatus" runat="server" CssClass="data_label"></asp:Label>
                                        </div>


                                        <div class="col-md-12">
                                       
                                            <hr />
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div id="TRSPayOption" runat="server" visible="false">
                                   <span class="text-danger">*</span><b>Select Payment Option: </b>
                                    <div class="text-center">                                   
                                         <asp:RadioButtonList ID="rdbPayOption" runat="server" RepeatDirection="Horizontal" Height="26px" Width="312px" AutoPostBack="true" OnSelectedIndexChanged="rdbPayOption_SelectedIndexChanged">
                                            <%--<asp:ListItem Value="1">Online Payment</asp:ListItem>--%>
                                            <asp:ListItem Value="2" Selected="True">By Challan</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvPayOption" runat="server" ControlToValidate="rdbPayOption"
                                            Display="None" ErrorMessage="Please Select Payment Option." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                    </div>
                             <div id="TRSGatewayType" runat="server" visible="false">
                                <span class="text-danger">*</span><b>Select Payment Service:</b>
                                 <div class="text-center">
                                 <asp:RadioButtonList ID="rdbPayServiceType" runat="server" RepeatDirection="Horizontal" Height="26px" Width="312px">
                                                <asp:ListItem Value="1" Selected="True">ATOM </asp:ListItem>
                                                <asp:ListItem Value="2">PAYTM </asp:ListItem>                                             
                                  </asp:RadioButtonList>
                                     </div>
                                  <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbPayServiceType"
                                                              Display="None" ErrorMessage="Please Select Pay Service Type."  ValidationGroup="submit">
                                  </asp:RequiredFieldValidator>--%>
                             </div>
                                <p>
                                </p>
                                <p class="text-center">                                   
                                      
                                     <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Font-Bold="True" OnClick="btnSubmit_Click" OnClientClick="return fnConfirmDelete();" Text="Pay" ValidationGroup="submit" Visible="false"/>
                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Enabled="false" Font-Bold="True" OnClick="btnReport_Click" Text="Print Challan" ValidationGroup="submit" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-warning" Font-Bold="True" OnClick="btnCancel_Click" Text="Cancel" Visible="false" />
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                            </p>

                        </div>

                        </fieldset>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSubmit" />
                        <asp:PostBackTrigger ControlID="btnReport" />
                        <asp:PostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="box-footer">
                    <p class="text-center">
                        &nbsp;<asp:Repeater ID="lvPaidReceipts" runat="server" OnItemDataBound="lvPaidReceipts_ItemDataBound">
                            <HeaderTemplate>
                                <div>
                                    <div class="titlebar">
                                        <b>Previous Receipts Information</b>
                                    </div>
                                </div>
                                <table class="table table-hover table-bordered">
                                    <thead>
                                        <tr class="bg-light-blue">

                                            <th>Receipt Type
                                            </th>
                                            <th>Receipt No
                                            </th>
                                            <th>Date
                                            </th>
                                            <th>Semester
                                            </th>
                                            <th>Pay Type
                                            </th>
                                            <th>Amount
                                            </th>
                                            <th>Payment Status
                                            </th>
                                            <th>Print
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item">

                                    <td>
                                        <%# Eval("RECIEPT_TITLE") %>
                                    </td>
                                    <td>
                                        <%# Eval("REC_NO") %>
                                    </td>
                                    <td>
                                        <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                    </td>
                                    <td>
                                        <%# Eval("SEMESTERNAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("PAY_TYPE") %>
                                    </td>
                                    <td>
                                        <%# Eval("TOTAL_AMT") %>
                                    </td>
                                    <td>
                                        <%# Eval("PAY_STATUS") %>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                            CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </p>

                </div>
                <div class="col-md-12">
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script lang="javascript" type="text/javascript">
        function fnConfirmDelete() {
            return Confirm("Are You Sure,You Want To confirm This Payment?");
        }
    </script>
</asp:Content>

