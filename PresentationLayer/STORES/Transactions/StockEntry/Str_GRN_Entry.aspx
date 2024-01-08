<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_GRN_Entry.aspx.cs" Inherits="STORES_Transactions_StockEntry_Str_GRN_Entry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../../jquery/jquery-3.2.1.min.js"></script>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('.multi-select-demo').multiselect({
        //        includeSelectAllOption: true,
        //        maxHeight: 200,
        //        enableFiltering: true,
        //        filterPlaceholder: 'Search',
        //    });
        //});
        //var parameter = Sys.WebForms.PageRequestManager.getInstance();
        //parameter.add_endRequest(function () {
        //    $(document).ready(function () {
        //        $('.multi-select-demo').multiselect({
        //            includeSelectAllOption: true,
        //            maxHeight: 200,
        //            enableFiltering: true,
        //            filterPlaceholder: 'Search',
        //        });
        //    });
        //});




        //------------05/05/2022--start--------------
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                maxWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search',
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    maxWidth: '100%',
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });


        //------------05/05/2022--start--------------

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
          //  alert(PO);
        }


    </script>
    <script type="text/javascript">
        function AddItem() {

            document.getElementById('<%=PnlItem.ClientID%>').style.display = 'block';
            document.getElementById('<%=divAddItem.ClientID%>').style.display = 'none';
            // alert('a');
        }
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



    <%--<asp:UpdatePanel ID="pnlFeeTable" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">GRN ENTRY</h3>
                </div>

                <div class="box-body">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" />
                    </div>

                    <div class="col-12" id="divGRNEtry" runat="server" visible="false">
                        <asp:Panel ID="PnlSecurityPass" runat="server" HorizontalAlign="left" Visible="true">
                            <div class="row">
                                <div class="col-lg-12 col-md-6 col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit GRN Entry</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divGRNNumber" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>GRN Number </label>
                                    </div>
                                    <asp:TextBox ID="txtGRNNumber" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>GRN Date</label>
                                    </div>

                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image2">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <%--<div class="input-group-addon">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                        <asp:TextBox ID="txtGRNDate" runat="server" CssClass="form-control"
                                            ToolTip="Select Date" Enabled="false" />
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtGRNDate"
                                                                    Display="None" ErrorMessage="Please Select GRN Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>

                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="false" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="Image2" PopupPosition="BottomLeft" TargetControlID="txtGRNDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtGRNDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtGRNDate"
                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid GRN Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select GRN Date"
                                            InvalidValueMessage="Please Enter Valid  GRN Date [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>SP Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image1">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <%--<div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                        <asp:TextBox ID="txtSpDate" runat="server" CssClass="form-control"
                                            ToolTip="Select Date" />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSpDate"
                                                                    Display="None" ErrorMessage="Please Select SP Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>

                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft" TargetControlID="txtSpDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtSpDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtSpDate"
                                            IsValidEmpty="true" ErrorMessage="Please Enter Valid SP Date In [dd/MM/yyyy] format"
                                            InvalidValueMessage="Please Enter Valid  SP Date [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>DM Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <%--    <div class="input-group-addon">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                        <asp:TextBox ID="txtDMDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDMDate"
                                                                    Display="None" ErrorMessage="Please Select DM Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>

                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="Image3" PopupPosition="BottomLeft" TargetControlID="txtDMDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDMDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="metxtDepDate" ControlToValidate="txtDMDate"
                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid DM Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select DM Date."
                                            InvalidValueMessage="Please Enter Valid  DM Date [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>DM No. </label>
                                    </div>
                                    <asp:TextBox ID="txtDMNo" runat="server" CssClass="form-control" ValidationGroup="stores" MaxLength="95" ToolTip="Enter DM No."></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDmno" runat="server" ControlToValidate="txtDMNo"
                                        Display="None" ErrorMessage="Please Enter DM No." ValidationGroup="Store"></asp:RequiredFieldValidator>
                                    <%--    <asp:HiddenField ID="hdnListCount" runat="server" />
                                    <asp:HiddenField ID="hdnTaxableAmt" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnBillAmt" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnTaxAmt" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnPO" runat="server" />--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSPNum" runat="server" visible="true">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>SP Number</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSecNumber" runat="server" data-select2-enable="true"
                                        InitialValue="0" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSecNumber_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vendor Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlVendor" data-select2-enable="true" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" ControlToValidate="ddlVendor"
                                        Display="None" ErrorMessage="Please Select Vendor Name" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                    <%--     <asp:HiddenField ID="hdnIndex" runat="server" />
                                    <asp:HiddenField ID="hdnBasicAmt" runat="server" />--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPO" runat="server" visible="true">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>PO No.</label>
                                    </div>
                                    <asp:ListBox ID="ddlPO" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                        SelectionMode="multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlPO_SelectedIndexChanged"></asp:ListBox>


                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Remark</label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" ToolTip="Enter Remark"></asp:TextBox>
                                    <%-- <asp:HiddenField ID="hdnOthEdit" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnrowcount" runat="server" />--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPONum" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Selected PO's </label>
                                    </div>
                                    <asp:TextBox ID="txtPONum" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" ToolTip="Enter Remark"></asp:TextBox>

                                </div>

                            </div>

                            <div class="col-12 btn-footer" id="divAddItem" runat="server" visible="true">

                                <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-info" Text="Add Item" CausesValidation="true" OnClick="btnAddItem_Click" />


                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PnlItem" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit Item Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Item Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlItem"
                                            Display="None" ErrorMessage="Please Select Item Name." InitialValue="0" ValidationGroup="AddItem"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>GRN Qty</label>
                                        </div>
                                        <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control" MaxLength="6" ToolTip="Enter Received Qty"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemQty"
                                            Display="None" ErrorMessage="Please Enter GRN Qty." ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="Range1" ControlToValidate="txtItemQty" MinimumValue="1" MaximumValue="2147483647" Type="Integer" runat="server" ValidationGroup="AddItem" ErrorMessage="GRN Quantity Must Be Greater Than Zero" Display="None" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divItemRemark" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtItemRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>

                                    </div>

                                </div>





                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnSaveItem" runat="server" CssClass="btn btn-info" Text="Save Item" ValidationGroup="AddItem" OnClick="btnSaveItem_Click" />
                                    <%--OnClientClick="return GetPO()"--%>
                                    <asp:Button ID="btnCancelItem" runat="server" Visible="false" CssClass="btn btn-warning" Text="Cancel" CausesValidation="true" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddItem" />

                                </div>


                            </div>
                        </asp:Panel>

                        <div class="col-12 mb-4">
                            <asp:ListView ID="lvItem" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div class="row">
                                        <div class=" col-lg-12 col-md-6 col-12">
                                            <div class="sub-heading">
                                                <h5>Item List</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="note-div">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Enter Rate And Discount Before Adding Tax </span></p>
                                            </div>
                                        </div>

                                        <div class="col-12">

                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th></th>
                                                        <th>PO Number</th>
                                                        <th>Item Name</th>
                                                        <th>PO Qty</th>
                                                        <th>Rec. Qty</th>
                                                        <th>GRN Qty</th>
                                                        <th>Bal Qty</th>
                                                        <th>Rate</th>
                                                        <th>Disc%</th>
                                                        <th>Disc Amt</th>
                                                        <th>Taxable Amt</th>
                                                        <th>Tax Info</th>
                                                        <th>Tax Amt</th>
                                                        <th>Bill Amt</th>
                                                        <th>Oth Info</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%--<asp:ImageButton ID="btnDeleteItem" runat="server" CausesValidation="false" ImageUrl="~/Images/delete.png"
                                                CommandArgument='<%#Eval("ITEM_SRNO")%>' AlternateText="Delete Record" OnClick="btnDeleteItem_Click"  OnClientClick="return confirm('Are You Sure You Want To Delete this Item?');" />
                                            <asp:HiddenField ID="hdnItemSrNo" runat="server" Value='<%# Eval("ITEM_SRNO")%>' />--%>


                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/images/delete.png"
                                                CommandArgument='<%#Eval("ITEM_SRNO")%>' AlternateText="Delete Record" OnClick="btnDeleteItem_Click" OnClientClick="return confirm('Are You Sure You Want To Delete this Item?');" />
                                            <asp:HiddenField ID="hdnItemSrNo" runat="server" Value='<%# Eval("ITEM_SRNO")%>' />

                                        </td>
                                        <td>
                                            <asp:Label ID="lblRefno" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnPordno" runat="server" Value='<%# Eval("PORDNO")%>' />
                                        </td>


                                        <td>
                                            <asp:TextBox ID="lblPOQty" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("PO_QTY")%>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnItemno" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblReceivedQty" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("RECEIVED_QTY")%>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnTechSpec" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblGRNQty" runat="server" CssClass="form-control" MaxLength="6" Text='<%# Eval("GRN_QTY")%>' onchange="return readListViewTextBoxes();" onblur="return CalOnGRNQty(this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                TargetControlID="lblGRNQty" ValidChars=".">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:HiddenField ID="hdnQualityQtySpec" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblBalQty" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("BAL_QTY")%>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnOthItemRemark" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblRate" runat="server" CssClass="form-control" MaxLength="9" Text='<%# Eval("RATE")%>' onblur="return CalOnRate(this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRate" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                TargetControlID="lblRate" ValidChars=".">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblDiscPer" runat="server" CssClass="form-control" MaxLength="3" Enabled="true" Text='<%# Eval("DISC_PER")%>' onblur="return CalOnDiscPer(this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftDiscper" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                TargetControlID="lblDiscPer" ValidChars=".">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lblDiscAmt" runat="server" CssClass="form-control" MaxLength="9" Enabled="true" Text='<%# Eval("DISC_AMT")%>' onblur="return CalOnDiscAmount(this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftdiscamt" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                TargetControlID="lblDiscAmt" ValidChars=".">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="lblTaxableAmt" runat="server" CssClass="form-control" Text='<%# Eval("TAXABLE_AMT")%>' Enabled="false"></asp:TextBox>

                                        </td>

                                        <td>
                                            <%-- <asp:Button ID="btnAddTax" runat="server" CommandArgument='<%#Eval("ITEM_NO")%>' CssClass="btn btn-primary" Text="Add" OnClientClick="return GetTaxableAmt(this);" OnClick="btnAddTax_Click" />--%>
                                            <asp:ImageButton runat="server" ID="btnAddTax" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add" OnClientClick="return GetTaxableAmt(this);" OnClick="btnAddTax_Click" />
                                         <asp:HiddenField ID="hdnIsTaxInclusive" runat="server" Value='<%#Eval("IsTaxInclusive") %>' />   <%--30/12/2023--%>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="lblTaxAmount" runat="server" Enabled="false" Text='<%# Eval("TAX_AMT")%>' CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnIsTax" runat="server" Value='<%# Eval("IS_TAX")%>' />
                                        </td>

                                        <td>
                                            <asp:TextBox ID="lblBillAmt" runat="server" Enabled="false" Text='<%# Eval("BILL_AMT")%>' CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hdnItemPOQty" runat="server" Value='<%# Eval("PO_QTY")%>' />
                                            <asp:HiddenField ID="hdnItemRecQty" runat="server" Value='<%# Eval("RECEIVED_QTY")%>' />
                                            <asp:HiddenField ID="hdnItemBalQty" runat="server" Value='<%# Eval("BAL_QTY")%>' />
                                            <asp:HiddenField ID="hdnItemDiscPer" runat="server" Value='<%# Eval("DISC_PER")%>' />
                                            <asp:HiddenField ID="hdnItemDiscAmt" runat="server" Value='<%# Eval("DISC_AMT")%>' />
                                            <asp:HiddenField ID="hdnItemTaxableAmt" runat="server" Value='<%# Eval("TAXABLE_AMT")%>' />
                                            <asp:HiddenField ID="hdnItemTaxAmt" runat="server" Value='<%# Eval("TAX_AMT")%>' />
                                            <asp:HiddenField ID="hdnItemBillAmt" runat="server" Value='<%# Eval("BILL_AMT")%>' />
                                        </td>
                                        <%-- <td>
                                                        <asp:TextBox ID="lblItemRemark" runat="server" CssClass="form-control" Text='<%# Eval("ITEM_REMARK")%>'></asp:TextBox>
                                                    </td>--%>
                                        <td>
                                            <asp:ImageButton runat="server" ID="btnAddOthInfo" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add Oth Info" OnClientClick="return GetOthInfoIndex(this);" OnClick="btnAddOthInfo_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:ListView>
                        </div>


                        <div class="col-12" id="divItemCount" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-2 col-md-3 col-6">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Number Of Items :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblItemCount" runat="server"></asp:Label>
                                            </a>
                                        </li>

                                    </ul>
                                </div>

                                <div class="col-lg-2 col-md-3 col-6">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Total GRN Qty :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblItemQtyCount" runat="server"></asp:Label></a>
                                        </li>

                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class=" col-12 btn-footer">
                            <asp:Button ID="btnAddNew2" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="Store" OnClick="btnSubmit_Click" OnClientClick="return Validate(this);" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />

                            <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Store" />
                        </div>
                    </div>
                    <div class="col-12">

                        <ajaxToolKit:ModalPopupExtender ID="MdlTax" runat="server" PopupControlID="pnlTaxDetail" TargetControlID="lblTax"
                            BackgroundCssClass="modalBackground" BehaviorID="mdlPopupDel" CancelControlID="ImgTax">
                        </ajaxToolKit:ModalPopupExtender>

                        <asp:Label ID="lblTax" runat="server"></asp:Label>

                        <asp:Panel ID="pnlTaxDetail" runat="server" CssClass="PopupReg" Style="height: auto; width: 50%; background: #fff; z-index: 333; box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;">
                            <div class="col-12">
                                <div class="sub-heading mt-3 mb-3">
                                    <h5>Add Details</h5>
                                    <div class="box-tools pull-right">
                                        <asp:ImageButton ID="ImgTax" runat="server" ImageUrl="~/IMAGES/delete.png" ToolTip="Close" />
                                    </div>
                                </div>
                                <div class=" col-12" id="divTaxPopup" runat="server" visible="false">

                                    <asp:ListView ID="lvTax" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Tax Name                                                                              
                                                            </th>
                                                            <th>Tax Amount
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
                                                    <asp:Label ID="lblTaxName" runat="server" Text='<%#Eval("TAX_NAME") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnTaxId" runat="server" Value='<%#Eval("TAXID") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="lblTaxAmount" runat="server" CssClass="form-control" MaxLength="9" Text='<%#Eval("TAX_AMOUNT") %>' onblur="CalTotTaxAmt(this)"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" ValidChars="." TargetControlID="lblTaxAmount" />
                                                </td>


                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Total Tax Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtTotTaxAmt" runat="server" CssClass="form-control" Enabled="false" />
                                            </div>

                                           <%-- //=================================================================30/12/2023--%>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label> </label>
                                                </div>
                                                <asp:CheckBox ID="chkTaxInclusive" runat="server" checked="false"/>
                                                        <label>Is Tax Inclusive</label>
                                            </div>
                                            <%-- //================================================================30/12/2023--%>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="btnTaxSubmit" runat="server" CssClass="btn btn-primary" Text="Save Tax" OnClientClick="return GetTotTaxAmt();" OnClick="btnTaxSubmit_Click" />

                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-12" id="divOthPopup" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Technical Specification</label>
                                            </div>
                                            <asp:TextBox ID="txtTechSpec" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">

                                                <label>Quality&Qty Specification</label>
                                            </div>
                                            <asp:TextBox ID="txtQualityQtySpec" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Item Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtItemRemarkOth" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSaveOthInfo" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnSaveOthInfo_Click" OnClientClick="return SaveOthInfo();" />
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>

                    </div>

                    <div class="col-12">
                        <asp:ListView ID="lvGRNEntry" runat="server" Visible="false">
                            <LayoutTemplate>
                                <div>
                                    <div class="sub-heading">
                                        <h5>GRN Entry List</h5>
                                    </div>

                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action</th>
                                                <th>GRN Number</th>
                                                <th>SP Date</th>
                                                <th>GRN Date</th>
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
                                            CommandArgument='<%#Eval("GRNID")%>' AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("GRN_NUMBER")%>                                                       
                                    </td>
                                    <td>
                                        <%# Eval("SPDATE","{0:dd-MM-yyyy}")%>                                                    
                                    </td>
                                    <td>
                                        <%# Eval("GRNDATE","{0:dd-MM-yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("PNAME")%>
                                    </td>

                                </tr>
                            </ItemTemplate>

                        </asp:ListView>

                    </div>
                    <asp:HiddenField ID="hdnServiceRows" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnServiceAmt" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnIndex" runat="server" />
                    <asp:HiddenField ID="hdnBasicAmt" runat="server" />
                    <asp:HiddenField ID="hdnOthEdit" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnrowcount" runat="server" />
                    <asp:HiddenField ID="hdnServiceIndex" runat="server" />
                    <asp:HiddenField ID="hdnServiceTotAmt" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnListCount" runat="server" />
                    <asp:HiddenField ID="hdnTaxableAmt" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnDiscAmt" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnPO" runat="server" />
                    <asp:HiddenField ID="hdnSerTaxLvCount" runat="server" />
                    <asp:HiddenField ID="hdnBillAmt" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnTaxAmt" runat="server" Value="0" />



                </div>
            </div>
        </div>
    </div>

    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPO" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnSaveItem" />
            <asp:PostBackTrigger ControlID="btnAddItem" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function CalOnGRNQty(crl) {
            debugger;
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblGRNQty");
            var index = i[0];

            //calculate Bal Qty             
            var POQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblPOQty').value;
            var RecQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblReceivedQty').value;
            var GRNQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblGRNQty').value;
            var BAlQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBalQty').value;
            var PoNumber = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRefno').innerHTML;

            //if (PoNumber != '') {
            //    if (GRNQty > POQty) {
            //        alert("GRN Qty Should Not Be Greater Than PO Qty.");
            //        return;
            //    }
            //    if (GRNQty > BAlQty) {
            //        alert("GRN Qty Should Not Be Greater Than Balance Qty.");
            //        return;
            //    }
            //}
            //if (GRNQty == 0 || GRNQty == "") {

            //    //BalAmt = Number(Number(BillAmount) - Number(TotalPaid)).toFixed(2);
            //    //alert(BalAmt);

            //    //alert('Please Enter in GRN Quantity.');
            //    return false;
            //}

            if (Number(POQty) > 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnItemBalQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBalQty').value = (Number(POQty) - (Number(RecQty) + Number(GRNQty))).toFixed(2);
            }
            //
            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblGRNQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
          //  var taxamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;
            var Discountamt = 0;
            if (Number(discount) == 0 || Number(discount) < 1){
                Discountamt = Number(discamt);
            }
            else {
                Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            }
            
         //   var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;   31/10/2023
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            var totamount = grossAmount;//+ taxamt;

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = discamt.toFixed(2);  31/10/2023
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = Discountamt.toFixed(2); //31/10/2023
            //if (taxamt > 0) {
            //    alert('Please Calculate Tax Again.');
            //}
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value = 0;//31/10/2023
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_btnAddTax').disabled = true;

            var score = 0;
            var ROWS = document.getElementById('<%=hdnrowcount.ClientID%>').value;
            var i = 0;

            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblGRNQty').value);
            }
            document.getElementById('<%= lblItemQtyCount.ClientID %>').innerHTML = score;
            
        }

        function CalOnRate(crl) {
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblRate");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblGRNQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
            var taxamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;

            var Discountamt = 0;
            if (Number(discount) == 0 || Number(discount) < 1)
                Discountamt = Number(discamt);
            else
                Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;

            //var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            var totamount = Number(grossAmount) + Number(taxamt);


            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = grossAmount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = Discountamt.toFixed(2);

            //if (taxamt > 0) {
            //    a1lert('Please Calculate Tax Again.');
            //}
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value = 0;//31/10/2023
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_btnAddTax').disabled = true;
        }

        function CalOnDiscPer(crl) {


            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblDiscPer");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblGRNQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;
            var taxamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;

            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            //var totamount = grossAmount;
            var totamount = Number(grossAmount) + Number(taxamt);

            if (discount == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').disabled = false;
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').disabled = true;
            }
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = grossAmount.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
            //if (taxamt > 0) {
            //    a1lert('Please Calculate Tax Again.');
            //}
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value = 0;//31/10/2023
             
        }

        function CalOnDiscAmount(crl) {

            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblDiscAmt");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblGRNQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
            var taxamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;

            var Discper = (Number(discamt).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            //var totamount = grossAmount;// + taxamt;            
            var Discountamt = (Number(discamt).toFixed(2) * 1) / 1;
            var totamount = Number(grossAmount) + Number(taxamt);

            if (discamt == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').disabled = false;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').disabled = true;
               // document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value = 0;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = grossAmount.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
            if (taxamt > 0) {
                a1lert('Please Calculate Tax Again.');
            }
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value = 0;//31/10/2023
        }

        function CalTotTaxAmt(crl) {
            var TotAmount = 0;
            var ROWS = document.getElementById('<%=hdnListCount.ClientID%>').value;
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                TotAmount += Number(document.getElementById("ctl00_ContentPlaceHolder1_lvTax_ctrl" + i + "_lblTaxAmount").value);
            }
            document.getElementById('<%= txtTotTaxAmt.ClientID %>').value = TotAmount;
        }

        function GetTaxableAmt(crl) {

            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_btnAddTax");
            var index = i[0];
            if (document.getElementById('<%=ddlVendor.ClientID%>').value == 0) {
                alert("Please Select Vendor.");
                return false;
            }

            var Rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var GRNQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblGRNQty').value;

            if (Number(Rate) > 0 && Number(GRNQty) > 0) {
                var hdnTaxableAmt=  document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value;
                var hdnBillAmt =document.getElementById('<%=hdnBillAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value;
                 document.getElementById('<%=hdnTaxAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;
                document.getElementById('<%=hdnIndex.ClientID%>').value = index;
                var  hdnBasicAmt=document.getElementById('<%=hdnBasicAmt.ClientID%>').value = (Number(Rate) * Number(GRNQty)).toFixed(2);
                var hdnDiscAmt = document.getElementById('<%=hdnDiscAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
               //alert('hdnTaxableAmt=' + hdnTaxableAmt);
               //alert('hdnBillAmt=' + hdnTaxableAmt);
               //alert('hdnBasicAmt=' + hdnTaxableAmt);
               //alert('hdnDiscAmt=' + hdnDiscAmt);
            }
            else {
                alert("Please Enter GRN Qty,Rate And Discount Before Adding Taxes.");
                return false;
            }
        }

        function GetOthInfoIndex(crl) {
            debugger;
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_btnAddOthInfo");
            var index = i[0];
            document.getElementById('<%=hdnIndex.ClientID%>').value = index;

            document.getElementById('<%=hdnDiscAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
            document.getElementById('<%=txtTechSpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTechSpec').value;
            document.getElementById('<%=txtQualityQtySpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnQualityQtySpec').value;
            document.getElementById('<%=txtItemRemarkOth.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnOthItemRemark').value;

            document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value;
            document.getElementById('<%=hdnBillAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value;
            document.getElementById('<%=hdnTaxAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;



        }
        function SaveOthInfo() {

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnTechSpec').value = document.getElementById('<%=txtTechSpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnQualityQtySpec').value = document.getElementById('<%=txtQualityQtySpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnOthItemRemark').value = document.getElementById('<%=txtItemRemarkOth.ClientID%>').value;
            document.getElementById('<%=hdnOthEdit.ClientID%>').value = '1';
        }

        function GetTotTaxAmt() {
            debugger;
          //  document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
          //
          //  document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
          //  var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
          //  var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
            //  document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);



            //===========================30/12/2023==========================//
            if (document.getElementById('<%=chkTaxInclusive.ClientID%>').checked == false) {

                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
                var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
                var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTaxInclusive').value = 0;   //30/12/2023



                //alert(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTaxInclusive').value);
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
                var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
                var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                var deductAmt = Number(TaxableAmt) - Number(TotTaxAmt);
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxableAmt').value = Number(deductAmt);
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(deductAmt) + Number(TotTaxAmt); //30/12/2023
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTaxInclusive').value = 1;   //30/12/2023




                //alert(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxableAmt').value);

            }


        }
        function readListViewTextBoxes() {

            var score = 0;

            var ROWS = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblGRNQty').value);
            }
            document.getElementById('<%= lblItemQtyCount.ClientID %>').innerHTML = score;

        }

        function Validate(crl) {
            //debugger;
            if (document.getElementById('<%= txtDMDate.ClientID %>').value == '') {
                alert("Please Enter DM Date.");
                return false;
            }

            //var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/;
            var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
            if (!(date_regex.test(document.getElementById('<%= txtDMDate.ClientID %>').value))) {
                alert("DM Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                return false;
            }

            if (document.getElementById('<%= txtDMNo.ClientID %>').value == '') {
                alert("Please Enter DM No.");
                return false;
            }
            if (document.getElementById('<%= ddlVendor.ClientID %>').value == 0) {
                alert("Please Select Vendor Name.");
                return false;
            }

            var ItemQtyCount = 0;
            var ROWS = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                ItemQtyCount += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblGRNQty').value);

                var Rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblRate').value;
                var GRNQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblGRNQty').value;
                var POQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblPOQty').value;
                var BAlQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblBalQty').value;
                var RECQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblReceivedQty').value;
                var ItemName = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblItemName').innerHTML;

                if (GRNQty == '0' || GRNQty == '') {
                    alert("Please Enter GRN Qty For The Item Name : " + ItemName);
                    return false;
                }
                else if (Rate == '0' || Rate == '') {
                    alert("Please Enter Rate For The Item Name : " + ItemName);
                    return false;
                }

                var PoNumber = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblRefno').innerHTML;

                var RemainQty = Number(POQty) -Number(RECQty);

                if (PoNumber != '') {
                    if (Number(GRNQty) > Number(POQty)) {
                        alert("GRN Qty Should Not Be Greater Than PO Qty For Item Name " + ItemName);
                        return false;
                    }
                    if (Number(GRNQty) > Number(RemainQty)) {
                        alert("GRN Qty Should Not Be Greater Than PO Qty For Item Name " + ItemName);
                        return false;
                    }
                }
                //debugger;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemPOQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblPOQty').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemRecQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblReceivedQty').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemBalQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblBalQty').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemDiscAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblDiscAmt').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemDiscPer').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblDiscPer').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemTaxableAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblTaxableAmt').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemTaxAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblTaxAmount').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemBillAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblBillAmt').value;

            }

            document.getElementById('<%= lblItemQtyCount.ClientID %>').value = ItemQtyCount;


        }




    </script>

    <%-- <script type="text/javascript">
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

    </script>--%>
</asp:Content>

