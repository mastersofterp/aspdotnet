<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_SecurityPassEntry.aspx.cs" Inherits="STORES_Transactions_StockEntry_Str_SecurityPassEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>


    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }


    </script>

    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            $('.multiselect').css("width", "100%");
            $(".multiselect-container").css("width", "110%");
            $(".multiselect-container dropdown-menu").css("width", "110% ! important");
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    allSelectedText: 'All',
                    maxHeight: 200,
                    maxWidth: '100%',
                    includeSelectAllOption: true
                });
                $('.multiselect').css("width", "100%");
                $(".multiselect-container").css("width", "110%")
                $(".multiselect.ul").css("width", "110%");
                $(".multiselect-container dropdown-menu").css("width", "110%");
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

        function GetPO() {

            var POarray = []
            var PO;

            var checkboxes = document.querySelectorAll("#Pordno input[type=checkbox]:checked")

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (checkboxes[i].value != 'multiselect-all') {
                    if (PO == undefined) {
                        PO = checkboxes[i].value + '$';
                    }
                    else {
                        PO += checkboxes[i].value + '$';
                    }
                }
            }

            $('#<%= hdnPO.ClientID %>').val(degreeNo);
            alert(PO);
        }



    </script>
    <script type="text/javascript">
       // function AddItem() {

           /// document.getElementById('<%=PnlItem.ClientID%>').style.display = 'block';
           // document.getElementById('<%=divAddItem.ClientID%>').style.display = 'none';
            // alert('a');
        //}
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


    <script type="text/javascript">


        function readListViewTextBoxes() {
            var score = 0;
            var ROWS = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById("ctl00_ContentPlaceHolder1_lvItem_ctrl" + i + "_lblItemQty").value);
            }
            document.getElementById('<%= lblItemQtyCount.ClientID %>').innerHTML = score;
        }

        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
                alert("Please Enter Numeric Value");
                return false;
            }
            else {

                return true;
            }
        }

    </script>


    <script type="text/javascript">
        function CalPaymentAmount(crl) {



            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblItemQty");
            var index = i[0];
            var PoQty = 0;
            var RecQty = 0;
            var PoQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblPOQty').innerHTML;
            // alert(PoQty);
            //var PoQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblItemQty').value;
            var RecQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblItemQty').value;
            // alert(PoQty);
            //  alert(RecQty);

            if (Number(RecQty) > Number(PoQty)) {
                alert("Received Quantity Should Not Be Greater Than PO Quantity");
                // alert("b");
                return;


            }


        }
    </script>




    <%-- <asp:UpdatePanel ID="pnlFeeTable" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Security Pass Inward Entry</h3>
                </div>

                <div class="box-body">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnAdNew" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" />
                    </div>
                    <div class="col-12" id="divSPEntry" runat="server" visible="false">
                        <asp:Panel ID="PnlSecurityPass" runat="server" HorizontalAlign="left" Visible="true">


                            <div class="row">
                                <div class="col-lg-12 col-md-6 col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Security Pass Entry</h5>
                                    </div>

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSPNumber" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>SP Number</label>
                                    </div>
                                    <asp:TextBox ID="txtSecNumber" runat="server" Enabled="false" CssClass="form-control" ToolTip="Enter SP Number"></asp:TextBox>
                                    <asp:DropDownList ID="ddlSecNumber" runat="server" Visible="false" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSecNumber_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>SP Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image2">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <%--<div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar.png" Style="cursor: pointer" />
                                                            </div>--%>
                                        <asp:TextBox ID="txtSpDate" runat="server" CssClass="form-control"
                                            ToolTip="Select Date" Enabled="false" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtSpDate"
                                            Display="None" ErrorMessage="Please Select SP Date" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="false" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="Image2" PopupPosition="BottomLeft" TargetControlID="txtSpDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtSpDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtSpDate"
                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            InvalidValueMessage="SP Date Is Invalid (Enter In [dd/MM/yyyy] Format)" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Time</label>
                                    </div>
                                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTime"
                                        Mask="99:99" MaskType="Time" AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                        DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender1"
                                        ControlToValidate="txtTime" Display="None" EmptyValueBlurredText="Empty"
                                        InvalidValueMessage="Time Is Invalid [Enter In HH:MM AM/PM Format]"
                                        SetFocusOnError="true" TooltipMessage="Please Enter Arrival Time"
                                        IsValidEmpty="false" EmptyValueMessage="Please Enter Time." ValidationGroup="Store" />

                                    <%--                                    <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="Time Is Invalid [Enter In HH:MM AM/PM Format]" ControlToValidate="txtTime" ValidationExpression="^(1[0-2]|0[1-9]):[0-5][0-9]\040(AM|am|PM|pm)$" ValidationGroup="Store" Display="None">
                                    </asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle No. </label>
                                    </div>
                                    <asp:TextBox ID="txtVehicleno" runat="server" CssClass="form-control" ToolTip="Enter Vehicle No" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtVehicleno"
                                        Display="None" ErrorMessage="Please Enter Vehicle No." ValidationGroup="Store"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>DM Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image1">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtDMDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDMDate"
                                            Display="None" ErrorMessage="Please Select DM Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>

                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft" TargetControlID="txtDMDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDMDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="metxtDepDate" ControlToValidate="txtDMDate"
                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select DM Date"
                                            InvalidValueMessage="DM Date Is Invalid (Enter In [dd/MM/yyyy] Format)" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>

                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>DM No.</label>
                                    </div>
                                    <asp:TextBox ID="txtDMNo" runat="server" CssClass="form-control" MaxLength="95" ValidationGroup="stores" ToolTip="Enter DM No."></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDmno" runat="server" ControlToValidate="txtDMNo"
                                        Display="None" ErrorMessage="Please Enter DM No." ValidationGroup="Store"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vendor Name  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlVendor" data-select2-enable="true" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" ControlToValidate="ddlVendor"
                                        Display="None" ErrorMessage="Please Select Vendor Name" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Gate Keeper</label>
                                    </div>
                                    <asp:TextBox ID="txtGateKeeper" runat="server" CssClass="form-control" ToolTip="Enter Gate Keeper" MaxLength="64"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtGateKeeper"
                                        Display="None" ErrorMessage="Please Enter Gate Keeper" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>In Charge</label>
                                    </div>
                                    <asp:TextBox ID="txtInCharge" runat="server" CssClass="form-control" ToolTip="Enter In Charge" MaxLength="64"></asp:TextBox>

                                </div>
                                <%-- </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPONum" runat="server" visible="true">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>PO Number</label>
                                    </div>
                                    <asp:ListBox ID="ddlPO" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                        SelectionMode="multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlPO_SelectedIndexChanged"></asp:ListBox>

                                    <asp:HiddenField ID="hdnPO" runat="server" />

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server" visible="true">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Remark </label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>
                                    <asp:HiddenField ID="hdnrowcount" runat="server" />
                                </div>

                            </div>
                            <div class="col-12 btn-footer" id="divAddItem" runat="server">
                                <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-info" Text="Add Item" CausesValidation="true" OnClick="btnAddItem_Click" />

                            </div>

                        </asp:Panel>

                        <asp:Panel ID="PnlItem" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-12 col-md-6 col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Item Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Item Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlItem" data-select2-enable="true" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlItem"
                                            Display="None" ErrorMessage="Please Select Item Name." InitialValue="0" ValidationGroup="AddItem"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Received Qty</label>
                                        </div>
                                        <asp:TextBox ID="txtItemQty" data-select2-enable="true" runat="server" CssClass="form-control" ToolTip="Enter Received Qty" onkeypress="javascript:return isNumber(event)" MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemQty"
                                            Display="None" ErrorMessage="Please Enter Received Qty." ValidationGroup="AddItem"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtItemRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>

                                    </div>

                                    <div class="col-12 btn-footer">

                                        <asp:Button ID="btnSaveItem" runat="server" CssClass="btn btn-info" Text="Save Item" OnClick="btnSaveItem_Click" CausesValidation="true" ValidationGroup="AddItem" />

                                        <asp:Button ID="btnCancelItem" runat="server" Visible="false" CssClass="btn btn-warning" Text="Cancel" CausesValidation="true" OnClick="btnCancelItem_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddItem" />
                                    </div>
                                </div>

                            </div>

                        </asp:Panel>

                        <div class="form-group col-md-12" id="divlvItem" runat="server" visible="false">
                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr class="bg-light-blue">
                                        <th style="width: 5%">Action</th>
                                        <th style="width: 20%">PO Number</th>
                                        <th style="width: 10%">Item Code</th>
                                        <th style="width: 20%">Item Name</th>
                                        <th id="thPOQty" runat="server" visible="false" style="width: 10%">PO Qty</th>
                                        <th style="width: 10%">Received Qty</th>
                                        <th style="width: 35%">Remarks</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvItem" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Item List</h4>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 5%">
                                                    <asp:ImageButton ID="btnDeleteItem" runat="server" CausesValidation="false" ImageUrl="~/images/delete.png"
                                                        CommandArgument='<%#Eval("SRNO")%>' AlternateText="Delete Record" OnClick="btnDeleteItem_Click" />
                                                    <asp:HiddenField ID="hdnSrNo" runat="server" Value='<%# Eval("SRNO")%>' />
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblRefno" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ITEM_CODE")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnItemno" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnPordno" runat="server" Value='<%# Eval("PORDNO")%>' />
                                                    <%-- <asp:HiddenField ID="lblPOQty" runat="server" Value='<%# Eval("ITEM_QTY")%>' />--%>
                                                </td>
                                                <td id="tdPOQty" runat="server" visible='<%# Eval("PORDNO").ToString()=="0"?false:true%>'>
                                                    <asp:Label ID="lblPOQty" runat="server" Text='<%# Eval("ITEM_QTY")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="lblItemQty" runat="server" CssClass="form-control" value="0" Text='<%# Eval("ITEM_QTY")%>'
                                                        onblur="return CalPaymentAmount(this);" MaxLength="15"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                        TargetControlID="lblItemQty" ValidChars=".">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td style="width: 35%">

                                                    <asp:TextBox ID="lblItemRemark" runat="server" CssClass="form-control" Text='<%# Eval("ITEM_REMARK")%>'></asp:TextBox>
                                                </td>


                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>

                                </tbody>
                            </table>


                        </div>
                        <div class="col-12" id="divItemCount" runat="server" visible="false">
                            <div class="row">

                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Number Of Items :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblItemCount" runat="server"></asp:Label></a>
                                        </li>
                                    </ul>

                                </div>
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Total Quantity  :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblItemQtyCount" runat="server"></asp:Label></a>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <div class="col-12 btn-footer">

                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit"  OnClick="btnSubmit_Click" ValidationGroup="Store"/>
      
                            <asp:Button ID="btnAddNew2" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-info" Text="Back" OnClick="btnBack_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />

                            <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Store" />
                        </div>
                    </div>


                    <div class="col-12">
                        <asp:ListView ID="lvSecPass" runat="server" Visible="false">
                            <LayoutTemplate>
                                <div>
                                    <div class="sub-heading">
                                        <h5>Security Pass Entry List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <th>Action</th>
                                                <th>SP Number</th>
                                                <th>SP Date</th>
                                                <th>DM Date</th>
                                                <th>Vendor Name</th>
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
                                            CommandArgument='<%#Eval("SPID")%>' AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("SP_NUMBER")%>                                                       
                                    </td>
                                    <td>
                                        <%# Eval("SPDATE","{0:dd-MM-yyyy}")%>                                                    
                                    </td>
                                    <td>
                                        <%# Eval("DMDATE","{0:dd-MM-yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("PNAME")%>
                                    </td>

                                </tr>
                            </ItemTemplate>

                        </asp:ListView>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPO" />
            <asp:PostBackTrigger ControlID="btnSaveItem" />
        </Triggers>
    </asp:UpdatePanel>--%>
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

        function Validate() {
           // alert('a');
            if (!event.detail || event.detail == 1) {//activate on first click only to avoid hiding again on multiple clicks
                // code here. // It will execute only once on multiple clicks
              //  alert('yes');
                document.getElementById('<%=btnSubmit.ClientID%>').disabled = true;
                return true;
            }
        }
       
    </script>

</asp:Content>

