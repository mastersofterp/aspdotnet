<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_CondemnItemSale.aspx.cs" Inherits="STORES_Transactions_Str_CondemnItemSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>

    <%--<script lang="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>--%>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEM SALE</h3>

                        </div>

                        <div class="box-body">
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnAddN" runat="server" CssClass="btn btn-primary" Text="Add New" CausesValidation="true" OnClick="btnAddN_Click" />
                            </div>

                            <%--<h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>--%>
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="false">

                                <div class="sub-heading">
                                    <h5>Add/Edit  Item Sale</h5>
                                </div>

                                <div class="form-group col-12">
                                    <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                        Visible="false">

                                        <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>

                                    </asp:Panel>
                                </div>


                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divTRNumber" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Item Sale TR NO.</label>
                                            </div>
                                            <asp:TextBox ID="txtCOND_SALE_TRNO" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCategory" runat="server" data-select2-enable="true" Enabled="false" CssClass="form-control"
                                                TabIndex="2">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Item Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlItemType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" ToolTip="Select Item Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">SCRAP ITEM</asp:ListItem>
                                                <asp:ListItem Value="2">NON SCRAP ITEM</asp:ListItem>
                                                <asp:ListItem Value="3">ALL</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlItemType"
                                                Display="None" InitialValue="0" ErrorMessage="Please select Item Type" SetFocusOnError="true"
                                                ValidationGroup="Store"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sub Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" ToolTip="Select Sub Category">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubCategory"
                                                Display="None" InitialValue="0" ErrorMessage="Please select Sub Category" SetFocusOnError="true"
                                                ValidationGroup="Store"></asp:RequiredFieldValidator>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Item </label>
                                            </div>
                                            <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="2" ToolTip="Select Item">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItem"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Item" SetFocusOnError="true"
                                                ValidationGroup="Store"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 ">
                                    <div class="row">
                                        <div class="form-group col-12 text-center">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" 
                                                CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Show Button" ValidationGroup="Store" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sale Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgToDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter Up To Date"></asp:TextBox>
                                                <%-- <div class="input-group-addon">
                                                                <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                            </div>--%>
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="mevFrom" runat="server"
                                                    ControlExtender="meToDate" ControlToValidate="txtToDate" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Select Sale Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Sale Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Select  Sale Date" IsValidEmpty="false"
                                                    ValidationGroup="AddItem">
                                                </ajaxToolKit:MaskedEditValidator>

                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                    TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <%-- <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Sale Date" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Vendor Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendorName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="2" ToolTip="Select Vendor Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlVendorName"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Vendor Name" SetFocusOnError="true"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>





                                <div class="form-group col-12" id="divShowItem" runat="server" visible="false">
                                    <%-- <asp:Panel ID="pnlDsrScrap" runat="server" Width="70%">--%>
                                    <asp:ListView ID="lvShowItem" runat="server" Enabled="true">
                                        <LayoutTemplate>
                                            <div class="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Item List</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Select
                                                            </th>
                                                            <th>Item Serial Number
                                                            </th>
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
                                                    <asp:CheckBox ID="chkInvdiNo" runat="server" ToolTip='<%# Eval("DSR_ID")%>' />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDSRNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>

                                                </td>
                                                <%--<td style="width: 50%">
                                                                    <asp:TextBox ID="lblSMRDsrRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtFineAmt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>

                                <div class="col-12 btn-footer" runat="server" id="divAddItem" visible="false">

                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                                        CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Add Button" ValidationGroup="AddItem" />
                                    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="AddItem" ShowMessageBox="true" ShowSummary="false" />
                                </div>



                                <div class="col-12 mb-4">

                                    <asp:ListView ID="lvItemSale" runat="server" Visible="true">
                                        <%--<EmptyDataTemplate>
                                                        <br />
                                                        <center>
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found"  / >
                                                            </center>
                                                    </EmptyDataTemplate>--%>
                                        <LayoutTemplate>
                                            <%--  <table class="table table-bordered table-hover table-responsive" style="width:100%;"> 
                                                        <tr>
                                                            <td style="padding-left: 10px; padding-right: 10px">
                                                                <div id="demo-grid" class="vista-grid">--%>
                                            <div class="titlebar">
                                                <div class="sub-heading">
                                                    <h5>Item Sale List</h5>
                                                </div>
                                                <table id="lvItemSaleTable" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%--<th style="width:2%;">Action
                                                                            </th>--%>
                                                            <th>Item Serial No.
                                                            </th>
                                                            <th>Actual Value
                                                            </th>
                                                            <th>Current Depr. Value
                                                            </th>
                                                            <th>Value
                                                            </th>
                                                            <th>Sale Value
                                                            </th>
                                                            <th>Profit/Loss
                                                            </th>
                                                            <th>Remark
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
                                                    <asp:Label ID="lblDSR_NUMBER" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnDSR_ID" runat="server" Value='<%# Eval("DSR_ID")%>'></asp:HiddenField>
                                                </td>

                                                <td>

                                                    <asp:Label ID="lblSTOCK_VALUE" runat="server" Text='<%# Eval("STOCK_VALUE")%>'></asp:Label>

                                                </td>

                                                <td>
                                                    <asp:Label ID="lblDEPRECIATED_AMOUNT" runat="server" Text='<%# Eval("DEPRECIATED_AMOUNT")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblVALUE" runat="server" Text='<%# Eval("VALUE")%>'></asp:Label>
                                                </td>
                                                <td style="width: 15%;">
                                                    <asp:TextBox ID="lblSaleValue" runat="server" Text='<%# Eval("SALE_VALUE")%>' CssClass="form-control" onblur="return CalculateProfitLoss(this);" ToolTip="Enter Sale VAlue" MaxLength="9"></asp:TextBox>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeSalVal" runat="server"
                                                        TargetControlID="lblSaleValue"
                                                        FilterType="Numbers">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="lblSaleValue"
                                                        Display="None" ErrorMessage="Please Enter Sale Value" ValidationGroup="submit"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>




                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProfitLoss" runat="server" Text='<%# Eval("PROFLOSS")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnProfitLoss" runat="server" Value='<%# Eval("PROFLOSS")%>' />


                                                </td>
                                                <td>
                                                    <asp:TextBox ID="lblREMARK" runat="server" CssClass="form-control"></asp:TextBox>

                                                </td>
                                                <%--<td style="width: 8%;">
                                                                <%# Eval("DEPR_CAL_AMT")%>
                                                            </td>
                                                            <td style="width: 8%;">
                                                                <%# Eval("DEPR_BAL_AMT")%>
                                                            </td>--%>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>

                                <div class="col-12 btn-footer mt-3">
                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary" Text="Add New" CausesValidation="true" OnClick="btnAddNew_Click" />
                                    <asp:Button ID="btnSave" Enabled="false" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSave_Click" CausesValidation="true" TabIndex="3" ToolTip="Click To Submit Button" ValidationGroup="submit" />
                                    <%--                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="VP" CausesValidation="true" ssClass="btn btn-info" OnClick="btnSubmit_Click" />--%>
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" CausesValidation="true" OnClick="btnBack_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                </div>
                            </asp:Panel>

                            <div class="col-12">

                                <asp:ListView ID="lvCondemnedSaleEntry" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Item Sale Entry List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Item Sale Number</th>
                                                        <th>Condmned Date</th>
                                                        <th>Sale Date</th>
                                                        <th>Sale Amount</th>
                                                        <th>Profit/Loss</th>

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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%#Eval("CIS_ID")%>' AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <%--<td>
                                                        <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Edit"
                                                            CommandArgument='<%#Eval("CIS_ID")%>' OnClick="btnSelect_Click"/>
                                                    </td>--%>
                                            <td>
                                                <%# Eval("COND_SALE_TRNO")%>                                                       
                                            </td>
                                            <td>
                                                <%# Eval("COND_DATE","{0:dd-MM-yyyy}")%>                                                    
                                            </td>
                                            <td>
                                                <%# Eval("SALE_DATE","{0:dd-MM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("SALE_VALUE")%>
                                            </td>
                                            <td>
                                                <%# Eval("PROFLOSS")%>
                                            </td>


                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>


    <script type="text/javascript" language="javascript">

        function CalculateProfitLoss(crl) {
            debugger;
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItemSale_ctrl");
            var i = st[1].split("_lblSaleValue");
            var index = i[0];

            //calculate Bal Qty             
            var SaleValue = document.getElementById('ctl00_ContentPlaceHolder1_lvItemSale_ctrl' + index + '_lblSaleValue').value;
            var Value = document.getElementById('ctl00_ContentPlaceHolder1_lvItemSale_ctrl' + index + '_lblVALUE').innerHTML;
            //alert(SaleValue);
            //alert(Value);
            var ProfitLoss = 0;

            ProfitLoss = Number(SaleValue) - Number(Value);
            //alert(ProfitLoss);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItemSale_ctrl' + index + '_lblProfitLoss').innerText = (Number(ProfitLoss)).toFixed(2);

            document.getElementById('ctl00_ContentPlaceHolder1_lvItemSale_ctrl' + index + '_hdnProfitLoss').value = (Number(ProfitLoss)).toFixed(2);
        }




    </script>

</asp:Content>


