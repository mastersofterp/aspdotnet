<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ElectricityBillPayment.aspx.cs" Inherits="ESTATE_Transaction_ElectricityBillPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlge" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ELECTRICITY BILL PAYMENT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Electricity Bill Payment
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info">
                                                            <div class="panel panel-heading">Select Month</div>
                                                            <div class="panel panel-body">
                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Select Bill Month<span style="color: red;">*</span> :</label>
                                                                        <div class="input-group date">
                                                                            <asp:TextBox ID="txtDate" runat="server" TabIndex="1" ValidationGroup="submit" MaxLength="7" CssClass="form-control" AutoPostBack="true"
                                                                                OnTextChanged="txtDate_TextChanged" />
                                                                            <ajaxToolKit:CalendarExtender ID="calext" runat="server"
                                                                                Enabled="true" EnableViewState="true" Format="MM/yyyy"
                                                                                PopupButtonID="Image5" TargetControlID="txtDate">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="rfvdate" runat="server"
                                                                                ControlToValidate="txtDate" ErrorMessage="Please Enter Month & Year in (MM/YYYY Format)" Display="None"
                                                                                ValidationGroup="Submit">
                                                                            </asp:RequiredFieldValidator>
                                                                            <div class="input-group-addon">
                                                                               <%-- <asp:Image ID="Image5" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                                 <asp:ImageButton runat="Server" ID="Image5" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4" id="trBlock" runat="server" visible="false">
                                                                        <label>Block :</label>
                                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                                            CssClass="form-control" TabIndex="2" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-md-4" id="trEmp" runat="server" visible="false">
                                                                        <label>Select Employee<span style="color: red;">*</span> :</label>
                                                                        <asp:DropDownList ID="ddlEmployee" runat="server" TabIndex="3" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvApplicant" runat="server" ControlToValidate="ddlEmployee" InitialValue="0"
                                                                            ErrorMessage="Please Select Employee." ValidationGroup="Submit" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="form-group row" id="fPanel" runat="server" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info">
                                                            <div class="panel panel-heading">Energy Bill Details</div>
                                                            <div class="panel panel-body">
                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Unit Sold :</label>
                                                                        <asp:Label ID="lblUnitSold" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Present Demand :</label>
                                                                        <asp:Label ID="lblPreDemand" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Meter Rent :</label>
                                                                        <asp:Label ID="lblMRent" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Fixed Charge :</label>
                                                                        <asp:Label ID="lblFCharge" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Arrear :</label>
                                                                        <asp:Label ID="lblArrear" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Arrear Interest  :</label>
                                                                        <asp:Label ID="lblAInterest" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Within Due Date Amt :</label>
                                                                        <asp:Label ID="lblW" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>After Due Date Amt :</label>
                                                                        <asp:Label ID="lblAfter" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Due Date :</label>
                                                                        <asp:Label ID="lblDueDate" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Last Date :</label>
                                                                        <asp:Label ID="lblLastDate" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-4" id="trAmt" runat="server" visible="false">
                                                                        <label>Total Amount :</label>
                                                                        <asp:Label ID="lblTAmount" runat="server" Text="" CssClass="form-control"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row" id="fBillPanel" runat="server">
                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info">
                                                            <div class="panel panel-heading">Bill Payment Details</div>
                                                            <div class="panel panel-body">
                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Receipt No<span style="color: red;">*</span> :</label>
                                                                        <asp:TextBox ID="txtReceiptNo" runat="server" MaxLength="100" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvMembers" runat="server" ControlToValidate="txtReceiptNo"
                                                                            ErrorMessage="Please Enter Receipt Number" ValidationGroup="Submit"
                                                                            Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMembers" runat="server"
                                                                            TargetControlID="txtReceiptNo" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                                                            ValidChars="/\- ()">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Receipt Date<span style="color: red;">*</span> :</label>
                                                                        <div class="input-group date">
                                                                            <asp:TextBox ID="txtReceiptDt" runat="server" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                                                Format="dd/MM/yyyy" PopupButtonID="imgRDt" TargetControlID="txtReceiptDt" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                                                AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                                ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReceiptDt" />
                                                                            <asp:RequiredFieldValidator ID="rfvReceiptDt" runat="server" ControlToValidate="txtReceiptDt"
                                                                                ErrorMessage="Please Select Receipt Date." Display="None"
                                                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                            <div class="input-group-addon">
                                                                                <%--<asp:Image ID="imgRDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                                  <asp:ImageButton runat="Server" ID="imgRDt" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Paid Amount<span style="color: red;">*</span> :</label>
                                                                        <asp:TextBox ID="txtPAmount" runat="server" MaxLength="20" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvpAmt" runat="server" ControlToValidate="txtPAmount"
                                                                            ErrorMessage="Please Enter Paid Amount" ValidationGroup="Submit" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbePAmt" runat="server" TargetControlID="txtPAmount" FilterType="Custom,Numbers"
                                                                            ValidChars=".">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-12">
                                                                        <div class="text-center">
                                                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" TabIndex="7" Text="Submit"
                                                                                CssClass="btn btn-primary" ValidationGroup="Submit" />
                                                                            <asp:Button ID="btnreset" runat="server" OnClick="btnReset_Click" TabIndex="8" Text="Reset"
                                                                                CssClass="btn btn-warning" />
                                                                            <asp:ValidationSummary ID="vsBillPayment" runat="server" ValidationGroup="Submit"
                                                                                ShowMessageBox="true" ShowSummary="false" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <div class="col-md-12">
                                                            <asp:ListView ID="lvBillPay" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid" class="vista-grid">
                                                                        <h4 class="box-title">Electricity Bill Payment List</h4>
                                                                        <table class="table table-bordered table-hover">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>EDIT
                                                                                    </th>
                                                                                    <th>EMPLOYEE NAME
                                                                                    </th>
                                                                                    <th>RECEIPT NO.
                                                                                    </th>
                                                                                    <th>RECEIPT DATE
                                                                                    </th>
                                                                                    <th>TOTAL AMOUNT
                                                                                    </th>
                                                                                    <th>PAID AMOUNT
                                                                                    </th>
                                                                                    <th>BALANCE AMOUNT
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                                CommandArgument='<%# Eval("PID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                OnClick="btnEdit_Click" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("EMP_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("RECEIPT_NO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("RECEIPT_DATE","{0:dd-MMM-yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("TOTAL_AMOUNT")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PAID_AMOUNT")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BALANCE_AMOUNT")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="text-center">
                                                        <div class="col-md-12">
                                                            <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvBillPay" PageSize="10">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                                    <asp:NumericPagerField ButtonCount="10" ButtonType="Link" CurrentPageLabelCssClass="Current" />
                                                                    <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript">

        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>
</asp:Content>

