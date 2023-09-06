<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_CondemnedItemSaleApproval.aspx.cs" Inherits="STORES_Transactions_Str_CondemnedItemSaleApproval" %>

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
                            <h3 class="box-title">CONDEMNED SALE ITEM APPROVAL</h3>

                        </div>

                        <div class="box-body">
                         
                                <%--<h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>--%>
                                <asp:Panel ID="pnlDSRDetails" runat="server" Visible="false">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Item Sale</h5>
                                        </div>
                                        <div class="form-group col-12">
                                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                                Visible="false">onfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                           
                                              
                                            </asp:Panel>
                                        </div>


                                        <div class="form-group col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTRNumber" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Item Sale TR NO. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtCOND_SALE_TRNO" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Vendor Name  </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlVendorName" Enabled="false" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged"
                                                        TabIndex="2" ToolTip="Select Vendor Name">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlVendorName"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Vendor Name"
                                                        ValidationGroup="StoreReq"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Sale Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="imgToDate">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" Enabled="false" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter Up To Date"></asp:TextBox>
                                                        <%--  <div class="input-group-addon">
                                                                <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                            </div>--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                        </ajaxToolKit:MaskedEditExtender>

                                                        <ajaxToolKit:CalendarExtender Enabled="false" ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                            TargetControlID="txtToDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select End Date" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCatgory" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true"
                                                        TabIndex="2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Category"
                                                        ValidationGroup="StoreReq"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCat" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Sub Category </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubCategory" Enabled="false" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" ToolTip="Select Sub Category">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubCategory"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Sub Category"
                                                        ValidationGroup="StoreReq"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divItem" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Select Item</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlItem" Enabled="false" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" ToolTip="Select Item">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItem"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Item"
                                                        ValidationGroup="StoreReq"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="form-group col-12">
                                            <div class="form-group col-8" id="divShowItem" runat="server" visible="false" style="width: 60%">
                                                <%-- <asp:Panel ID="pnlDsrScrap" runat="server" Width="70%">--%>
                                                <asp:ListView ID="lvShowItem" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="lgv1">
                                                            <div class="sub-heading"><h5>Item List</h5></div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
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

                                            <div class="form-group col-3" runat="server" id="divAddItem" visible="false">
                                               
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                                                    CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Add Button" ValidationGroup="Store" />
                                            </div>
                                         
                                        </div>
                                     
                                            <asp:ListView ID="lvItemSale" Enabled="false" runat="server" Visible="true">
                                                <LayoutTemplate>
                                                        <div class="sub-heading">
                                                             <h5>Item Sale List</h5>
                                                        </div>
                                                        <table id="lvItemSaleTable" class="table table-striped table-bordered" style="width: 100%" id="">
                                                                  <%-- <table id="Table1" class="table table-striped table-bordered nowrap display" style="width: 100%" id="">--%>
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
                                                        <%--<td style="width:2%;">
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("DEP_CAL_ID") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click1" />&nbsp;
                                                        </td>--%>
                                                        <td >
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
                                                        <td>
                                                            <asp:TextBox ID="lblSaleValue" runat="server" Text='<%# Eval("SALE_VALUE")%>' CssClass="form-control" onblur="return CalculateProfitLoss(this);"></asp:TextBox>
                                                        </td>
                                                        <td >
                                                            <asp:Label ID="lblProfitLoss" runat="server" Text='<%# Eval("PROFLOSS")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnProfitLoss" runat="server" Value='<%# Eval("PROFLOSS")%>' />
                                                        </td>
                                                        <%--<td style="width: 15%;">
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("COND_STATUS")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnstatus" runat="server" Value='<%# Eval("COND_STATUS")%>' />
                                                                </td>--%>
                                                        <td>
                                                            <asp:TextBox ID="lblREMARK" runat="server" CssClass="form-control" Text='<%# Eval("REMARK")%>'></asp:TextBox>

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
                                                            <th>Transaction Date</th>
                                                            <th>Sale Date</th>
                                                            <th>Sale Amount</th>
                                                            <th>Profit/Loss</th>
                                                            <th>Status</th>
                                                            

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

                                                <%--<td>
                                                        <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Edit"
                                                            CommandArgument='<%#Eval("CIS_ID")%>' OnClick="btnSelect_Click"/>
                                                    </td>--%>

                                                <td>
                                                    <asp:Button ID="btnSelect" runat="server" CausesValidation="false" CssClass="btn btn-primary"
                                                        CommandArgument='<%#Eval("CIS_ID")%>' Text="Select" OnClick="btnSelect_Click"
                                                        Enabled='<%#Eval("COND_STATUS").ToString() == "PENDING" ? true : false %>' />
                                                </td>
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
                                                <td>
                                                    <%# Eval("COND_STATUS") %>
                                                </td>
                                                
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>
                            
                    
                        <asp:Panel ID="pnlApprove" runat="server" HorizontalAlign="left" Visible="false">
                            <div class="col-12">
                               
                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup></sup>
                                            <label>Approve/Reject Item Sale</label>
                                        </div>
                                         <asp:RadioButtonList ID="rdlApprove" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="A" Selected="True">Approve&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                        </asp:RadioButtonList>
                                     </div>
                                 </div>
                             
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary" Text="Submit" CausesValidation="true" OnClick="btnApprove_Click" enable="true" />
                                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
                                    </div>
                                    <br />
                               
                          
                        </asp:Panel>
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
            // var Status = document.getElementById('ctl00_ContentPlaceHolder1_lvItemSale_ctrl' + index + '_lblStatus).innerText;

            if (SaleValue <= 0) {
                alert("Sale Value never be Zero or Negative");
                return false;
            }
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

