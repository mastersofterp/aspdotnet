<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Return_Items.aspx.cs" Inherits="STORES_Transactions_Quotation_Str_Return_Items" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>
    <asp:UpdatePanel runat="server" ID="UpdPnlMain">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSET RETURN</h3>
                        </div>
                        <div class="box-body">
                             <asp:Panel ID="pnl" runat="server">
                            <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Return form</h5>
                                    </div>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    <div class="form-group col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Return Slip No</label>
                                                </div>
                                                <asp:TextBox ID="txtIssueSlipNo" runat="server" MaxLength="50" Enabled="false" CssClass="form-control" TabIndex="1" ToolTip="Enter Return Slip No"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trDepartment" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Department To return</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                    CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Select Department To return ">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvDept" ControlToValidate="ddlDept"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Department"
                                                    InitialValue="0" ValidationGroup="StoreReq">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Item Return Date :</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="ImaCalStartDate">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtItemIssueDate" runat="server" CssClass="form-control" ToolTip="Enter Item Return Date" TabIndex="3"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="efvIssueItemDate" runat="server" ControlToValidate="txtItemIssueDate"
                                                        ErrorMessage="" SetFocusOnError="true" ValidationGroup="issueItem" Display="None">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtIndentSlipDate" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtItemIssueDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtItemIssueDate" runat="server" ControlToValidate="txtItemIssueDate"
                                                        Display="None" ErrorMessage="Please Select Item Issue Date in (dd/MM/yyyy Format)"
                                                        SetFocusOnError="True" ValidationGroup="issueItem">
                                                
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" TargetControlID="txtItemIssueDate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meIssueDate"
                                                        ControlToValidate="txtItemIssueDate" Display="None" EmptyValueBlurredText="Empty"
                                                        EmptyValueMessage="Please Issue Date" InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage="Issue Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                                        TooltipMessage="Please Enter Issue Date" ValidationGroup="StoreReq" />
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trGroup" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Group </label>
                                                </div>
                                                <asp:DropDownList ID="ddlItemGroup" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                    CssClass="form-control" data-select2-enable="true" TabIndex="4" ToolTip="Select Group" OnSelectedIndexChanged="ddlItemGroup_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvItemGroup" ControlToValidate="ddlItemGroup"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Item Group"
                                                    InitialValue="0" ValidationGroup="StoreReq">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trItem" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Items</label>
                                                </div>
                                                <asp:DropDownList ID="ddlItems" runat="Server" AutoPostBack="True" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" TabIndex="5" ToolTip="Select Items" OnSelectedIndexChanged="ddlItems_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="ddlItems"
                                                    InitialValue="0" Display="None" ErrorMessage="Please Select Item" SetFocusOnError="true"
                                                    ValidationGroup="StoreReq">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="tr2" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Return Remark:</label>
                                                </div>
                                                <asp:TextBox ID="txtRemark" CssClass="form-control" TabIndex="7" ToolTip="Enter Return Remark" runat="server" TextMode="MultiLine"></asp:TextBox>

                                            </div>

                                        </div>
                                         </div>
                                        <div class="col-12">
                                            <asp:Panel ID="pnlTaxMaster" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvGrandMaster" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <center>
                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Fixed Asset Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select </th>
                                                                            <th>Item Name  </th>
                                                                            <th>Employee Name </th>
                                                                            <th>Serial No.</th>
                                                                            <th>Price</th>
                                                                            <th>Issue Date</th>
                                                                            <th>Condition</th>

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
                                                                <td>
                                                                    <asp:CheckBox runat="server" ID="chkcheck" ToolTip='<%# Eval("ISSUE_TNO")%>'></asp:CheckBox>
                                                                    &nbsp;

                                                                </td>
                                                                <td><%# Eval("ITEM_NAME")%></td>
                                                                <td><%# Eval("UA_FULLNAME")%></td>
                                                                <td>
                                                                    <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("serial_no")%>'></asp:Label>
                                                                </td>
                                                                <td><%# Eval("rate")%></td>
                                                                <td><%# Eval("ISSUE_DATE")%></td>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlcondition" CssClass="form-control">
                                                                        <asp:ListItem Value="1">Working</asp:ListItem>
                                                                        <asp:ListItem Value="2">Damaged</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>

                                                </div>
                                            </asp:Panel>
                                        </div>
                                   <div class="col-12 btn-footer">
                            <asp:Button ID="btnAddItem" runat="server" OnClick="btnAddItem_Click" Text="Return"
                                ValidationGroup="StoreReq" CssClass="btn btn-primary" TabIndex="8" ToolTip="Click To Return" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="9" ToolTip="Click To Reset" OnClick="btnCancel_Click" />

                            <asp:ValidationSummary ID="vsissueItem" runat="server" ValidationGroup="StoreReq"
                                DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                        </div>
                                </div>
                  
                        </asp:Panel>
                    </div>

                </div>
                 </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">
        //        function IsNumeric(textbox) {
        //            if (textbox != null && textbox.value != "") {
        //                if (isNaN(textbox.value)) {
        //                    alert("Please Enter Only Numeric value.");
        //                    document.getElementById(textbox.id).value = "0";
        //                }

        //            }
        //        }


        //calculation for Tex box value
        function CalculateAmountOnRate() {

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_txtQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_txtRate').value;
            var Amount = Number(rate) * Number(qty);
            document.getElementById('ctl00_ContentPlaceHolder1_txtAmount').value = Amount;

        }

        //calculation for Tex box value
        function CalculateAmountOnQty() {

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_txtQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_txtRate').value;
            var Amount = Number(rate) * Number(qty);
            document.getElementById('ctl00_ContentPlaceHolder1_txtAmount').value = Amount;

        }


        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                //txt.value = '0';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
        //calculation for Tex box value from ListView
        function AddTotalAmountsOnRate(crl) {

            var st = crl.id.split("lvItemDetails_ctrl");
            var i = st[1].split("_txtRates");
            var index = i[0];
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItemDetails_ctrl' + index + '_txtRates').value;
            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItemDetails_ctrl' + index + '_txtIQty').value;
            var amount = (Number(rate).toFixed(2) * qty);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItemDetails_ctrl' + index + '_txtAmt').value = amount.toFixed(2);

        }

        //calculation for Tex box value from ListView
        function AddTotalAmountsOnQty(crl) {

            var st = crl.id.split("lvItemDetails_ctrl");
            var i = st[1].split("_txtIQty");
            var index = i[0];
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItemDetails_ctrl' + index + '_txtRates').value;
            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItemDetails_ctrl' + index + '_txtIQty').value;
            var amount = (Number(rate).toFixed(2) * qty);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItemDetails_ctrl' + index + '_txtAmt').value = amount.toFixed(2);

        }

    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
