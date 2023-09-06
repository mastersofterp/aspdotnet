<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_VendorPaymentApproval.aspx.cs" Inherits="STORES_Transactions_Str_VendorPaymentApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../jquery/bootstrap-multiselect.js"></script>--%>
    <link href="../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../plugins/multiselect/bootstrap-multiselect.js"></script>
    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            $('.multiselect').css("width", "100%");
            $(".multiselect-container").css("width", "100%");
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    allSelectedText: 'All',
                    maxHeight: 200,
                    maxWidth: '100%',
                    includeSelectAllOption: true
                });
                $('.multiselect').css("width", "100%");
                $(".multiselect-container").css("width", "100%")
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)


            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                //   InitAutoCompl();
            }

        });


        //$(document).ready(function () {
        //   // alert('a');
        //$('#ddlPaymentType').on('change', function () {
        //    alert('a');
        //        if (this.value == '1')
        //            //.....................^.......
        //        {
        //            $("#txtVPDate").show();
        //        }
        //        else {
        //            $("#txtVPDate").hide();
        //        }
        //    });
        //});



        // $(function () {


        //$("#<%= ddlPaymentType.ClientID %>").change(function () {
        //alert('a');
        // $("#<%= txtPayeeName.ClientID %>").val(_calculateAge($(this).val()));
        // });
        // });


    </script>




    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>
    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999; /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>
    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }

    </script>

    <asp:UpdatePanel ID="pnlFeeTable" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VENDOR PAYMENT APPROVAL</h3>
                        </div>

                        <div class="box-body">

                            <asp:Panel ID="PnlVPEntry" runat="server" HorizontalAlign="left" Visible="false" Enabled="false">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Vendor Payment Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divVPNumber" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>VP NO.</label>
                                            </div>
                                            <asp:TextBox ID="txtVPNumber" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>VP Date</label>
                                            </div>
                                           <div class="input-group date">
                                        <div class="input-group-addon" id="Image2">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                             <%--   <div class="input-group-addon">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                </div>--%>
                                                <asp:TextBox ID="txtVPDate" runat="server" CssClass="form-control" ToolTip="Select Date" />

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtVPDate"
                                                    Display="None" ErrorMessage="Please Select VP Date" ValidationGroup="VP"></asp:RequiredFieldValidator>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image2" PopupPosition="BottomLeft" TargetControlID="txtVPDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtVPDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Vendor Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Please Select Vendor Name" ValidationGroup="VP"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Payment type </label>
                                            </div>
                                             <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Against PO"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Against GRN"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Against Invoice"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlPaymentType"
                                                Display="None" ErrorMessage="Please Select Payment type" ValidationGroup="VP"></asp:RequiredFieldValidator>
                                    
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12"  id="divPO" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>PO Number</label>
                                            </div>
                                             <asp:ListBox ID="ddlPO" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo" disabled="true"
                                                SelectionMode="multiple"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGRN" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>GRN Number </label>
                                            </div>
                                               <asp:ListBox ID="ddlGRNNumber" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                                SelectionMode="multiple"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divInvoice" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Invoice Number</label>
                                            </div>
                                             <asp:ListBox ID="ddlInvoice" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                                SelectionMode="multiple"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                              <sup></sup>
                                            <label>List Of </label>
                                        </div>
                                               <asp:ListBox ID="lstList" runat="server" AppendDataBoundItems="true" TabIndex="3" SelectionMode="multiple"></asp:ListBox>

                                     </div>


                                    </div>
                                    </div>
                                    <div class="col-12 mb-4" id="divList" runat="server" visible="false">
                                          <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                    <th>
                                                        <asp:Label ID="thStockDate" runat="server" Text=""></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="thStockNum" runat="server" Text=""></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="thStockAmt" runat="server" Text=""></asp:Label></th>
                                                    <th>Total Paid</th>
                                                    <th>Balance Amt</th>
                                                    <th>Pay Now</th>
                                                    <th>Remark</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:ListView ID="lvVendorPay" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblStockDate" runat="server" Text='<%# Eval("STOCK_DATE","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStockNumber" runat="server" Text='<%# Eval("STOCK_NUMBER")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnStockId" runat="server" Value='<%# Eval("STOCK_ID")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Eval("BILL_AMT")%>'></asp:Label>
                                                            </td>
                                                            <td>

                                                                <asp:Label ID="lblTotalPaidAmt" runat="server" Text='<%# Eval("TOTAL_PAID_AMT")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="lblBalanceAmt" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("BALANCE_AMT")%>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnBalanceAmt" runat="server" Value='<%# Eval("BALANCE_AMT")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPayNowAmt" runat="server" CssClass="form-control" Text='<%# Eval("PAY_NOW_AMT")%>' onblur="return CalPaymentAmount(this);"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPayRemark" runat="server" CssClass="form-control" Text='<%# Eval("PAY_REMARK")%>'></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>
                                            </tbody>
                                        </table>

                                    </div>

                                    <div class="form-group col-12">
                                        <div class="row">
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                           <div class="label-dynamic">
                                              <sup></sup>
                                            <label>Payment Amount </label>
                                        </div>
                                              <asp:TextBox ID="txtPaymentAmt" runat="server" Enabled="false" Text="0.0" CssClass="form-control"></asp:TextBox>

                                        </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup></sup>
                                            <label>Mode Of Payment</label>
                                        </div>
                                             <asp:DropDownList ID="ddlModeOfPay" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Cash"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Cheque"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Online"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlModeOfPay"
                                                Display="None" ErrorMessage="Please Select Mode Of Payment" ValidationGroup="VP"></asp:RequiredFieldValidator>
                                     </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                    <sup></sup>
                                                <label>Payee Name</label>
                                            </div>
                                             <asp:TextBox ID="txtPayeeName" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                    <sup></sup>
                                                <label>Bank Account Number</label>
                                            </div>
                                              <asp:DropDownList ID="ddlAccNum" runat="server" CssClass="form-control" AppendDataBoundItems="true" onchange="return Fillbankdetails();">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="0" ControlToValidate="ddlAccNum"
                                                Display="None" ErrorMessage="Please Select Bank Account Number" ValidationGroup="VP"></asp:RequiredFieldValidator>
                                
                                       </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                    <sup></sup>
                                                <label>Bank Name</label>
                                            </div>
                                               <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" Enabled="false" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlBank"
                                                Display="None" ErrorMessage="Please Select Bank Name" ValidationGroup="VP"></asp:RequiredFieldValidator>
                                     
                                              </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>Branch Name </label>
                                        </div>
                                             <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch Name" ValidationGroup="VP"></asp:RequiredFieldValidator>
                                    
                                     </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>IFSC Code </label>
                                        </div>
                                               <asp:DropDownList ID="ddlIfscCode" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="0" ControlToValidate="ddlIfscCode"
                                                Display="None" ErrorMessage="Please Select IFSC Code" ValidationGroup="VP"></asp:RequiredFieldValidator>
                                   
                                     </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup></sup>
                                            <label>Remark</label>
                                        </div>
                                       <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" ToolTip="Enter Remark"></asp:TextBox>
                                   
                                     </div>
                                        </div>
                                       
                                    </div>

                                
                   
                            </asp:Panel>


                                    <div class="col-12">
                                        <asp:ListView ID="lvVPEntry" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                          <h5>Vendor Payment Entry List</h5>
                                                    </div>
                                                  
                                                 <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                   <thead class="bg-light-blue">
                                                     <tr>
                                                                <th>VP Number</th>
                                                                <th>VP Date</th>
                                                                <th>Vendor Name</th>
                                                                <th>Paid Amount</th>
                                                                <th>Payment Type</th>
                                                                 <th>Status</th>
                                                                <th>Action</th>
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
                                                        <%# Eval("VP_NUMBER")%>                                                       
                                                    </td>
                                                    <td>
                                                        <%# Eval("VPDATE","{0:dd-MM-yyyy}")%>                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("PNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PAYMENT_AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PAYMENT_TYPE")%>
                                                    </td>
                                                     <td>
                                                        <%# Eval("VP_STATUS")%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Select"
                                                            CommandArgument='<%#Eval("VPID")%>' OnClick="btnSelect_Click" />
                                                    </td>

                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>

                                    </div>

                                
                                <div class="col-12">
                                    <asp:Panel ID="pnlApprove" runat="server" HorizontalAlign="left" Visible="false">
                                     
                                            <div class="sub-heading"><h5>Approve/Reject Vendor Payment</h5></div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                  <asp:RadioButtonList ID="rdlApprove" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal">                                                     
                                                    
                                                      <asp:ListItem Value="A">Approve&nbsp;&nbsp;</asp:ListItem>
                                                      <asp:ListItem Value="R">Reject</asp:ListItem>
                                                  </asp:RadioButtonList>



                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="0" ControlToValidate="rdlApprove"
                                                Display="None" ErrorMessage="Please select Approve Or Reject" ValidationGroup="VP1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-12 btn-footer">                                                   
                                                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary" Text="Submit" CausesValidation="true" OnClick="btnApprove_Click"/>
                                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click"/>  
                                                     <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="VP1" />                                                 
                                                </div>

                                            </div>
                                       
                                    </asp:Panel>


                                </div>
                       </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager;
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        </script>



</asp:Content>

