<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_JvStockEntry.aspx.cs" Inherits="STORES_Transactions_StockEntry_Str_JvStockEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- JQUERY MESSAGE POPUP--%>
    <%--Jquery Script File And Css--%>

    <script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />
    <%-- <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STOCK JOURNAL ENTRY</h3>
                </div>


                <div class="box-body">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnAddNew1" runat="server" Visible="false" CausesValidation="true" Text="Add New" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Add New Record" OnClick="btnAddNew2_Click" />
                    </div>

                    <asp:Panel ID="pnlJVEntry" runat="server" Visible="true">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Add Stock Journal Entry</h5>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSlipNum" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Transaction Slip No</label>
                                    </div>
                                    <asp:TextBox ID="txtTranSlipNum" TabIndex="1" ToolTip="Enter Issue Slip No" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" MaxLength="50"></asp:TextBox>
                                    <asp:DropDownList ID="ddlTranSlipNum" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                        AutoPostBack="true" TabIndex="1" ToolTip="Select Issue Slip No" Visible="false" OnSelectedIndexChanged="ddlTranSlipNum_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvtxtTranSlipNum" runat="server" ControlToValidate="ddlTranSlipNum"
                                        Display="None" ErrorMessage="Please Enter Issue Slip No." ValidationGroup="StoreReq"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Transaction Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlTranType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        TabIndex="2" ToolTip="Select Requisition" AutoPostBack="true" OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTranType" SetFocusOnError="true"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Transaction Type"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdnDsrRowCount" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnConsDsrRowCount" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnSMRRowCount" runat="server" Value="0" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTranDate" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <%--   <sup></sup>
                                                                <label></label>--%>
                                    </div>
                                    <asp:Label ID="lblTranDate" runat="server"></asp:Label></b><span id="trdate" style="color: #FF0000">*</span>
                                    <%--<label>Transaction Date :<span id="spanDept" style="color: #FF0000">*</span> </label>--%>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="ImaCalStartDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtTranDate" runat="server" Enabled="false" CssClass="form-control" TabIndex="3" ToolTip="Item Issue Date"></asp:TextBox>
                                        <%--  <div class="input-group-addon">
                                                                        <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Style="cursor: pointer" />
                                                                    </div>--%>

                                        <ajaxToolKit:CalendarExtender ID="cetxtIndentSlipDate" runat="server" Enabled="false"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtTranDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtTranDate" OnInvalidCssClass="errordate" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>

                                        <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meIssueDate"
                                            ControlToValidate="txtTranDate" Display="None" EmptyValueBlurredText="Empty" IsValidEmpty="false"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Submit" Text="*"
                                            InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"></ajaxToolKit:MaskedEditValidator>

                                    </div>



                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divStore" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Store</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStoreUser" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false" TabIndex="2">
                                        <asp:ListItem Value="1">MAIN STORE</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div class="col-12" id="divIssue" runat="server" visible="false">
                                <%-- <asp:Panel ID="pnlIssue" runat="server">--%>
                                <div class="row">
                                    <div class="sub-heading">
                                        <h5>Issue Item</h5>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divTypeOfIssue" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Type of Issue</label>
                                                </div>
                                                <asp:RadioButton ID="rdbRequisition" runat="server" TabIndex="4" AutoPostBack="True" GroupName="rdbIssue"
                                                    OnCheckedChanged="rdbRequisition_CheckedChanged" Text="Requisition Wise" Checked="true" />
                                                <asp:RadioButton ID="rdbDirectIssue" TabIndex="5" runat="server" AutoPostBack="True" GroupName="rdbIssue"
                                                    OnCheckedChanged="rdbDirectIssue_CheckedChanged" Text="Direct Item Issue" />
                                                <asp:HiddenField ID="hdnRowCount" runat="server"></asp:HiddenField>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divreq" runat="server" visible="true">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Requisition</label>
                                                </div>
                                                <asp:DropDownList ID="ddlReq" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                    TabIndex="2" ToolTip="Select Requisition" OnSelectedIndexChanged="ddlReq_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtReqNo" runat="server" CssClass="texbox" Enabled="false" MaxLength="50" Width="300px"
                                                    Visible="false"></asp:TextBox>

                                            </div>

                                        </div>

                                    </div>
                                    <div class="form-group col-md-12" id="divreqDetails" runat="server" visible="false">
                                        <div class="form-group col-md-4">
                                            <label>Requisition Date :</label>
                                            <asp:Label ID="lblReqDate" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Requisition Dept :</label>
                                            <asp:Label ID="lblReqDept" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Requisition By :</label>
                                            <asp:Label ID="lblReqUser" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-12" runat="server" id="divAddItem" visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Item Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlItemIsue" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                                    TabIndex="2" ToolTip="Select Requisition">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemIsue"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Item Name"
                                                    ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Quantity</label>
                                                </div>
                                                <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control" MaxLength="5" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtItemQty" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Enter Quantity"
                                                    ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" ValidationGroup="AddItem" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="AddItem"
                                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="col-8">
                                            <asp:Panel ID="pnlItemDetails" runat="server" Visible="false">
                                                <div class="col-12 table-responsive">
                                                    <div class="sub-heading">
                                                        <h5>Item Details</h5>
                                                    </div>
                                                    <asp:ListView ID="lvIssueItem" runat="server">

                                                        <LayoutTemplate>
                                                            <div id="demo-grid">


                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Issue
                                                                            </th>
                                                                            <th>Item Name
                                                                            </th>
                                                                            <th>Requested Qty
                                                                            </th>
                                                                            <th>Already Issued Qty
                                                                            </th>
                                                                            <th>Available Qty
                                                                            </th>
                                                                            <th>Issued Qty
                                                                            </th>
                                                                            <th>Remark
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server">
                                                                        </tr>
                                                                    </tbody>

                                                                </table>

                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIssueSelect" runat="server" ToolTip='<%# Eval("ITEM_NO")%>' AutoPostBack="true" OnCheckedChanged="chkIssueSelect_CheckedChanged" />
                                                                    <asp:HiddenField ID="hdnItemNo" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblIssueItem" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnItemName" runat="server" Value='<%# Eval("ITEM_NAME")%>' />
                                                                    <asp:HiddenField ID="hdnItemType" runat="server" Value='<%# Eval("ITEM_TYPE")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtReqQty" runat="server" Enabled="false" Style="text-align: center;"
                                                                        Text='<%# Eval("REQ_QTY")%>' CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAlredyIssue" runat="server" Enabled="false" Style="text-align: center;"
                                                                        Text='<%# Eval("ALLREADYISSUEDQTY")%>' CssClass="form-control"></asp:TextBox>
                                                                    <asp:HiddenField ID="HiddenBalance" runat="server" Value='<%# Eval("BALANCE")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAQty" runat="server" Enabled="false" Text='<%# Eval("AVLQTY")%>' Style="text-align: center;"
                                                                        CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>





                                                                    <asp:TextBox ID="txtIQty" runat="server" Enabled="true" onkeyup="IsNumeric(this);"
                                                                        Style="text-align: center;" Text='<%# Eval("JVSTOCK_QTY")%>' CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <asp:CompareValidator ID="cvIQty" runat="server" ControlToCompare="txtAQty" ControlToValidate="txtIQty"
                                                                    Display="None" ErrorMessage="Issued Quantity should be less or equal to available Quantity"
                                                                    Operator="LessThanEqual" Type="Integer" ValidationGroup="StoreReq"></asp:CompareValidator>





                                                                <td>
                                                                    <asp:TextBox ID="txtIssueItemRemark" runat="server" Enabled="true"
                                                                        Style="text-align: left;" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </div>

                                            </asp:Panel>
                                        </div>


                                        <div class="col-4">
                                            <asp:ListView ID="lvDsrIssue" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="lgv1">
                                                        <label>Items Serial Number List</label>

                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table3">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Select All
                                                                        <asp:CheckBox ID="Check" runat="server" Checked="false" onchange="CheckAll(this)"  />  <%--OnCheckedChanged="CheckAll_CheckedChanged"--%>
                                                                    </th>

                                                                    <th>Item Serial Number
                                                                    </th>
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



                                                            <asp:CheckBox ID="chkDsrselect" runat="server" ToolTip='<%# Eval("SRNO")%>' />
                                                            <asp:HiddenField ID="hdnInvidno" runat="server" Value='<%# Eval("INVDINO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDsrNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnDsrItemNo" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>

                                        </div>


                                    </div>

                                </div>

                                <%--  </asp:Panel>--%>
                            </div>

                            <div id="divAsset" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>
                                        <asp:Label ID="lblScrpOrTransfer" runat="server" Text="Scrap Item"></asp:Label></h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Category</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCategory" data-select2-enable="true" runat="server" CssClass="form-control" Enabled="false" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="false"
                                            ToolTip="Select Category" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select Sub Category </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubCategory" data-select2-enable="true" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            AutoPostBack="True" TabIndex="3" ToolTip="Select Sub Category" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divItem" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select Item </label>
                                        </div>
                                        <asp:DropDownList ID="ddlItemSMR" data-select2-enable="true" runat="server" CssClass="form-control" TabIndex="4"
                                            AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Item" OnSelectedIndexChanged="ddlItemSMR_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <%-- <asp:Panel ID="pnlDsrScrap" runat="server" Width="70%">--%>
                                            <asp:ListView ID="lvSMRDsr" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="lgv1">
                                                        <label>Item List</label>

                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%; margin-bottom: 0px;" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="width: 10%">Select</th>
                                                                    <th style="width: 30%">Item Serial Number</th>
                                                                    <th style="width: 40%">Remark</th>
                                                                    <th style="width: 20%">Fine Amount</th>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                    <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 200px;">
                                                        <div id="demo-grid" class="">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 10%">
                                                            <asp:CheckBox ID="chkSMRDsrItem" runat="server" ToolTip='<%# Eval("SRNO")%>' />
                                                            <asp:HiddenField ID="hdnSMRDsrInvidno" runat="server" Value='<%# Eval("INVDINO")%>' />
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:Label ID="lblSMRDsrNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnSMRDsrItemNo" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                        </td>
                                                        <td style="width: 40%">
                                                            <asp:TextBox ID="lblSMRDsrRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txtFineAmt" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber(event)">0</asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </div>

                                        <div class="col-12">
                                            <asp:ListView ID="lvConsItem" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="lgv1">
                                                        <label>Item List</label>

                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%; margin: 0px" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="width: 10%">Select </th>
                                                                    <th style="width: 30%">Item Name </th>
                                                                    <th style="width: 10%">Avl Qty  </th>
                                                                    <th style="width: 10%">Consume Qty</th>
                                                                    <th style="width: 40%">Remark  </th>

                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                    <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 200px">
                                                        <div id="demo-grid">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="Table1">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 10%">
                                                            <asp:CheckBox ID="chkSelConsItem" runat="server" CssClass="chkGrid" ToolTip='<%# Eval("ITEM_NO")%>' />
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:Label ID="lblConsItem" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                            <%--  <asp:HiddenField ID="hdnConsumeItemNo" runat="server" Value='<%# Eval("ITEM_NO")%>' />--%>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="lblConsAvlQty" runat="server" Text='<%# Eval("AVLQTY")%>'></asp:Label>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox ID="lblConsumeQty" runat="server" CssClass="form-control" Text="0" onkeypress="javascript:return isNumber(event)" MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 40%">
                                                            <asp:TextBox ID="lblConsItemRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                            <%--  </asp:Panel>--%>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div class="col-12" id="divFromFields" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFromCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false" ValidationGroup="Submit"
                                            TabIndex="2" ToolTip="Select Requisition">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFromDept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                            TabIndex="2" ToolTip="Select Requisition">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>From Employee</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFromEmployee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                            TabIndex="2" ToolTip="Select Requisition">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divToFields" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlToCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                            TabIndex="2" ToolTip="Select Requisition">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlToDept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                            TabIndex="2" ToolTip="Select Requisition">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>To Employee</label>
                                        </div>
                                        <asp:DropDownList ID="ddlToEmployee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                            TabIndex="2" ToolTip="Select Requisition">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Item Location </label>
                                        </div>
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false"
                                            TabIndex="2" ToolTip="Select Item Location">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="form-group col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Remark</label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" TabIndex="7" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    <asp:ValidationSummary ID="vsissueItem" runat="server" ValidationGroup="StoreReq"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                </div>
                            </div>

                            <asp:Panel ID="pnlReport" runat="server" Visible="false">
                                <div class="form-group col-12">
                                    <div class="row">
                                        <div class="sub-heading">
                                            <h5>Issue Slip Report</h5>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Issue Slip No.</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTranSlipNumRpt" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="false" TabIndex="1" ToolTip="Select Issue Slip No." EnableTheming="False" Visible="true">
                                                <asp:ListItem Selected="True" Text="Please select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvIssueSlipNoRpt" runat="server" ControlToValidate="ddlTranSlipNumRpt"
                                                Display="None" ErrorMessage="Please Select Issue Slip No." ValidationGroup="report"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnPrintRpt" runat="server" CausesValidation="true"
                                        OnClick="btnPrintRpt_Click" Text="Print Report" ValidationGroup="report"
                                        CssClass="btn btn-info" TabIndex="2" ToolTip="Click To Print Report " />

                                    <asp:Button ID="btnCancelRpt" runat="server" CausesValidation="false"
                                        OnClick="btnCancelRpt_Click1" Text="Cancel" CssClass="btn btn-warning" TabIndex="3" ToolTip="Click To Reset " />

                                    <asp:Button ID="btnCodeRpt" runat="server" CausesValidation="false"
                                        Text="View Tag Code" OnClick="btnCodeRpt_Click" CssClass="btn btn-info" TabIndex="4" ToolTip="Click To View Tag Code "
                                        Visible="false" />
                                    <asp:ValidationSummary ID="vsPrintReport" runat="server" ValidationGroup="report"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnAddNew2" runat="server" CausesValidation="true" Visible="false" Text="Add New" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Add New Record" OnClick="btnAddNew2_Click" />
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return ValidateIssueItem();"
                                    Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Submit" Visible="true" />
                                <asp:Button ID="btnReport" runat="server" CausesValidation="false" OnClick="butReport_Click"
                                    Text="Report" CssClass="btn btn-info" TabIndex="9" ToolTip="Click To Report" Visible="false" />
                                <asp:Button ID="butCancel" runat="server" CausesValidation="false" OnClick="butCancel_Click"
                                    Text="Cancel" CssClass="btn btn-warning" TabIndex="10" ToolTip="Click To Reset" />
                                <asp:Button ID="btnBack" runat="server" CausesValidation="false" OnClick="btnBack_Click"
                                    Text="Back" CssClass="btn btn-info" TabIndex="11" ToolTip="Click To Back" Visible="false" />
                                <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false" /> OnClientClick="return ValidateIssueItem();"--%>
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                        </div>

                    </asp:Panel>
                </div>



            </div>

        </div>

    </div>




    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    alert("Please type in Numeric Format.");
                    document.getElementById(textbox.id).value = "0";
                }
            }
        }

        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
                alert("Please type in Numeric Value.");
                return false;
            }

            return true;
        }
        function ValidateIssueItem() {
            //var st = crl.id.split("ctl00_ContentPlaceHolder1_lvIssueItem_ctrl");
            // var i = st[1].split("");
            //var index = i[0];

            var TranType = Number(document.getElementById('<%=ddlTranType.ClientID%>').value);
            if (TranType == 0) {
                alert('Please Select Transaction Type.');
                return false;
            }

            if (TranType == 2 || TranType == 3 || TranType == 5 || TranType == 6 || TranType == 7) {
                if (document.getElementById('<%=ddlSubCategory.ClientID%>').value == 0) {
                    alert('Please Select Sub Category.');
                    return false;
                }
                if (document.getElementById('<%=ddlItemSMR.ClientID%>').value == 0) {
                    alert('Please Select Item.');
                    return false;
                }
            }
            if (TranType == 1 || TranType == 3 || TranType == 5 || TranType == 6 || TranType == 7) {
                if (document.getElementById('<%=ddlToCollege.ClientID%>').value == 0) {
                    alert('Please Select To College.');
                    return false;
                }
                if (document.getElementById('<%=ddlToDept.ClientID%>').value == 0) {
                    alert('Please Select To Department.');
                    return false;
                }
            }


            if (TranType == 1) {

                var chkReqIssue = document.getElementById('<%=rdbRequisition.ClientID%>');

                if (chkReqIssue.checked) {
                    if (document.getElementById('<%=ddlReq.ClientID%>').value == 0) {
                        alert('Please Select Requisition');
                        return false;
                    }
                }

                var ChkCount = 0;
                var RowCount = Number(document.getElementById('<%=hdnRowCount.ClientID%>').value);
                for (var i = 0; i < RowCount; i++) {
          
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_chkIssueSelect');
                    if (chk.checked) {
                      
                        //  ctl00_ContentPlaceHolder1_lvIssueItem_ctrl0_txtIQty
                        var IssuedQty = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_txtIQty').value;
                        var ReqQty = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_txtReqQty').value;
                        var AvlQty = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_txtAQty').value;
                        var BlQty = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_HiddenBalance').value;
                        if (IssuedQty == '' || IssuedQty == 0) {
                            alert('Please Enter Issued Quantity For All The Selected Items.');
                            return false;
                        }
                        else if (Number(IssuedQty) > Number(ReqQty)) {
                            alert('Issued Quantity Should Not Be Greater Than Requested Quantity.');
                            return false;

                        }
                        else if (Number(IssuedQty) > Number(AvlQty)) {
                            alert('Issued Quantity Should Not Be Greater Than Available Quantity.');
                            return false;

                        }
                        else if (Number(BlQty) > 0) {
                            if (Number(IssuedQty) > Number(BlQty)) {
                                alert('Issued Quantity Should Not Be Greater Than Balance Quantity.');
                                return false;

                            }


                        }
                        var ItemNo = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_hdnItemNo').value;
                        var ItemName = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_hdnItemName').value;
                        var ItemType = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_hdnItemType').value;
                        var DsrRowCount = Number(document.getElementById('<%=hdnDsrRowCount.ClientID%>').value);
                        var DsrItemSelCount = 0;
                        //alert('ItemNo' + ItemNo);
                        //alert('ItemName' + ItemName);
                        //alert('ItemType' + ItemType);
                        //alert('DsrRowCount' + DsrRowCount);

                        if (ItemType == "1") // Non Consumable Item
                        {
                            //alert('yesy');
                            for (var j = 0; j < DsrRowCount; j++) {
                                var DsrItemNo = document.getElementById('ctl00_ContentPlaceHolder1_lvDsrIssue_ctrl' + j + '_hdnDsrItemNo').value;
                                var chkDsr = document.getElementById('ctl00_ContentPlaceHolder1_lvDsrIssue_ctrl' + j + '_chkDsrselect');
                                if (ItemNo == DsrItemNo && chkDsr.checked) {
                                    DsrItemSelCount++;
                                }
                            }
                            //if (DsrItemSelCount == 0) {
                            //    alert('Please Select Item Serial Numbers To Issue For : ' + ItemName);
                            //    return false;
                            //}
                            if (Number(IssuedQty) != Number(DsrItemSelCount)) {

                                alert('Number Of Selected Item Serial Number Count And Issued Quantity Should be Equal Required For Item : ' + ItemName);
                                return false;
                            }
                        }

                        ChkCount++;
                    }

                }

                if (ChkCount == 0) {
                    alert('Please Select At Least One Item.');
                    return false;
                }
            }

            else if (TranType == 3) {

                if (document.getElementById('<%=ddlFromCollege.ClientID%>').value == 0) {
                    alert('Please Select From College.');
                    return false;
                }
                if (document.getElementById('<%=ddlFromDept.ClientID%>').value == 0) {
                    alert('Please Select From Department.');
                    return false;
                }

                if (document.getElementById('<%=ddlToCollege.ClientID%>').value == 0) {
                    alert('Please Select To College.');
                    return false;
                }
                if (document.getElementById('<%=ddlToDept.ClientID%>').value == 0) {
                    alert('Please Select To Department.');
                    return false;
                }

            }
            else if (TranType == 4) {

                if (document.getElementById('<%=ddlSubCategory.ClientID%>').value == 0) {
                    alert('Please Select Sub Category.');
                    return false;
                }

                var ChkCount = 0;
                var RowCount = Number(document.getElementById('<%=hdnConsDsrRowCount.ClientID%>').value);
                for (var j = 0; j < RowCount; j++) {
                    var chkDsr = document.getElementById('ctl00_ContentPlaceHolder1_lvConsItem_ctrl' + j + '_chkSelConsItem');
                    var ConsItemName = document.getElementById('ctl00_ContentPlaceHolder1_lvConsItem_ctrl' + j + '_lblConsItem').value;
                    var ConsAvlQty = document.getElementById('ctl00_ContentPlaceHolder1_lvConsItem_ctrl' + j + '_lblConsAvlQty').innerHTML;
                    var ConsumeQty = document.getElementById('ctl00_ContentPlaceHolder1_lvConsItem_ctrl' + j + '_lblConsumeQty').value;
                    if (chkDsr.checked) {
                        //alert(Number(ConsumeQty), Number(ConsAvlQty));
                        if (ConsumeQty == 0 || ConsumeQty == '' || Number(ConsumeQty) < 1) {
                            alert('Please Enter Consume Qty For All The Selected Items.');
                            return false;
                        }
                        if (Number(ConsumeQty) > Number(ConsAvlQty)) {
                            alert('Consume Qty Should Not Be Greter Than Avl Qty.');
                            return false;
                        }
                        ChkCount++;
                    }
                }
                if (ChkCount == 0) {
                    alert('Please Select At Least One Item.');
                    return false;
                }
            }

    if (TranType != 1 && TranType != 4) {
        var ChkCount = 0;
        var RowCount = Number(document.getElementById('<%=hdnSMRRowCount.ClientID%>').value);
        for (var j = 0; j < RowCount; j++) {
            var chkDsr = document.getElementById('ctl00_ContentPlaceHolder1_lvSMRDsr_ctrl' + j + '_chkSMRDsrItem');
            if (chkDsr.checked) {
                ChkCount++;
            }
        }
        if (ChkCount == 0) {
            alert('Please Select At Least One Item.');
            return false;
        }
    }


}
function EnableDisable() {
    document.getElementById('<%=divAddItem.ClientID%>').style.display = 'block';
    return true;
}






//$(document).ready(function () {
//    //Check / Uncheck All Checkbox for Vendor Category
//    $("[id$='cbHead']").click(function () {
//        $("[id$='cbHead']").attr('checked', this.checked);
//        $("[id$='chkDsrselect']").attr('checked', this.checked);

//        $("[id$='chkIssueSelect']").attr('checked', false);
//        // $("[id$='ChkAllVendorDetails']").attr('checked', false);

//    });
//});

function totAllSubjects(headchk) {
    var frm = document.forms[0]
    for (i = 0; i < document.forms[0].elements.length; i++) {
        var e = frm.elements[i];
        if (e.type == 'checkbox') {
            if (headchk.checked == true)
                e.checked = true;
            else
                e.checked = false;
        }
    }
}



function SelectAll(mainChk) {
    // var frm = document.lvDsrIssue[0]
    var frm = document.forms[0]
    for (i = 0; i < document.forms[0].elements.length; i++) {
        var e = frm.elements[i];
        if (e.type == 'checkbox') {
            if (mainChk.checked == true)
                e.checked = true;
            else
                e.checked = false;
        }



    }
}
function CalOnGRNQty(crl) {
    debugger
    //alert('CAl');
    var st = crl.id.split("ctl00_ContentPlaceHolder1_lvIssueItem_ctrl");

    var i = st[1].split("_txtIQty");

    var index = i[0];

    // ctl00_ContentPlaceHolder1_lvIssueItem_ctrl0_txtIQty/ctl00_ContentPlaceHolder1_lvIssueItem_ctrl0_txtAQty/ctl00_ContentPlaceHolder1_lvIssueItem_ctrl0_txtReqQty
    var AvlQty = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + index + '_txtAQty').value;
    var ReqQty = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + index + '_txtReqQty').value;
    var IssueQty = document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + index + '_txtIQty').value;

    if (IssueQty > AvlQty) {
        alert('Issued Quantity Should be less or equal to Available Qty. ');
        document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + index + '_txtIQty').value = 0;
        return false;
    } else if (IssueQty == 0 || IssueQty == "") {

        alert('Please Enter in Issued Quantity.');
        return false;
    }

    if (Number(AvlQty) > 0) {
        document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + index + '_txtAQty').value = (Number(AvlQty) - Number(IssueQty)).toFixed(2);
        document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + index + '_txtReqQty').value = (Number(ReqQty) - Number(IssueQty)).toFixed(2);




        //var score = 0;
        //var ROWS = document.getElementById('<%=hdnDsrRowCount.ClientID%>').value;
        //var i = 0;

        // for (i = 0; i < ROWS; i++) {
        //    score += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvIssueItem_ctrl' + i + '_txtIQty').value);

    }

    // document.getElementById('<%= txtItemQty.ClientID %>').innerHTML = score;

}




        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
                alert("Please type in Numeric Value.");
                return false;
            }

            return true;
        }



    </script>
    <script type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

               
                if (headchk.checked == true) {
                    e.checked = true;
                    // Get the reference to the GridView control
                    var myGridView = document.getElementById("myGridViewID");

                    // Loop through all the rows in the GridView
                    for (var i = 0; i < myGridView.rows.length; i++) {
                        // Find the checkbox control in the current row
                        var myCheckBox = myGridView.rows[i].cells[0].getElementsByTagName("input")[0];

                        // Check or uncheck the checkbox based on the current state
                        myCheckBox.checked = !myCheckBox.checked;
                    }
                }
                else {
                    e.checked = false;

                }
            }
        }
        }
    </script>
    <script type="text/javascript">
        //function selectAll(invoker) {
        //    var inputElements = document.getElementsByTagName('input');
        //    for (var i = 0; i < inputElements.length; i++) {
        //        var myElement = inputElements[i];
        //        if (myElement.type === "checkbox") {
        //            myElement.checked = invoker.checked;
        //        }
        //    }
        //}
         

        function CheckAll(checkid) {
            debugger;
            var updateButtons = $('#Table3 input[type=checkbox]');
            if ($(checkid).children().is(':checked')) {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", true);
                    }
                });
            }
            else {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", false);
                    }
                });
            }
        }


        //function CheckAll(checkid) {
        //    var updateButtons = $('#Table3 input[type=checkbox]');
        //    if ($(checkid).children().is(':checked')) {
        //        updateButtons.each(function () {
        //            if ($(this).attr("id") != $(checkid).children().attr("id")) {
        //                $(this).prop("checked", true);
        //            }
        //        });
        //    }
        //    else {
        //        updateButtons.each(function () {
        //            if ($(this).attr("id") != $(checkid).children().attr("id")) {
        //                $(this).prop("checked", false);
        //            }
        //        });
        //    }
        //}

    </script>


    <script language="javascript" type="text/javascript">
        
    </script>


    <div id="divMsg" runat="server">
    </div>
</asp:Content>


