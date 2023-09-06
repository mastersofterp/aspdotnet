<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Dead_Stock_Entry.aspx.cs" Inherits="STORES_Transactions_StockEntry_Str_Dead_Stock_Entry" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

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
                    <h3 class="box-title">DEAD STOCK ENTRY</h3>
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
                                        <h5>Add/Edit Dead Stock Entry</h5>
                                    </div>
                                </div>
                               <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divGRNNumber" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>GRN Number </label>
                                    </div>
                                    <asp:TextBox ID="txtGRNNumber" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                </div>--%>
                         
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Issue Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image1">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                      <%--  <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                        <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control" ToolTip="Select Date" /><%--ValidationGroup="Store"--%>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSpDate"
                                                                    Display="None" ErrorMessage="Please Select SP Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>

                                       <%-- <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft" TargetControlID="txtIssueDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtIssueDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtIssueDate"
                                            IsValidEmpty="true" ErrorMessage="Please Enter Valid SP Date In [dd/MM/yyyy] format"
                                            InvalidValueMessage="Please Enter Valid  SP Date [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>--%>

                                         <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft" TargetControlID="txtIssueDate">
                                            </ajaxToolKit:CalendarExtender>

                                            <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtIssueDate">
                                            </ajaxToolKit:MaskedEditExtender>

                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="metxtDepDate" ControlToValidate="txtIssueDate"
                                                IsValidEmpty="false" ErrorMessage="Please Enter Valid Issue Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select Issue Date"
                                                InvalidValueMessage="Issue Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                        </div>
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
                                 </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPONum" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Selected PO's </label>
                                    </div>
                                    <asp:TextBox ID="txtPONum" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" ToolTip="Enter Remark"></asp:TextBox>

                                </div>

                            </div>

                            <div class="col-12 btn-footer" id="divAddItem" runat="server" visible="false">

                                <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-info" Text="Add Item" CausesValidation="true" OnClick="btnAddItem_Click" visible="false"/>


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
                                            <label>Qty</label>
                                        </div>
                                        <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control" MaxLength="3" ToolTip="Enter Received Qty"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemQty"
                                            Display="None" ErrorMessage="Please Enter Qty." ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                       <%-- <asp:RangeValidator ID="Range1" ControlToValidate="txtItemQty" MinimumValue="1" MaximumValue="2147483647" Type="Integer" runat="server" ValidationGroup="AddItem" ErrorMessage="GRN Quantity Must Be Greater Than Zero" Display="None" />--%>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Rate</label>
                                        </div>
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" MaxLength="6" ToolTip="Enter Received Qty"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"
                                            Display="None" ErrorMessage="Please Enter Rate." ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                      <%--  <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtItemQty" MinimumValue="1" MaximumValue="2147483647" Type="Integer" runat="server" ValidationGroup="AddItem" ErrorMessage="GRN Quantity Must Be Greater Than Zero" Display="None" />--%>

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

                                    <asp:Button ID="btnSaveItem" runat="server" CssClass="btn btn-info" Text="Save Item" ValidationGroup="AddItem" OnClick="btnSaveItem_Click" /><%--OnClick="btnSaveItem_Click"--%>
                                    <%--OnClientClick="return GetPO()"--%>
                                    <asp:Button ID="btnCancelItem" runat="server" Visible="false" CssClass="btn btn-warning" Text="Cancel" CausesValidation="true" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddItem" />

                                </div>


                            </div>
                        </asp:Panel>
                                 <div class=" col-12 btn-footer" id="divbtn" runat="server">
                            <asp:Button ID="btnAddNew2" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" Visible="false"/>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="Store" OnClick="btnSubmit_Click" OnClientClick="return Validate(this);" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />

                            <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Store" />
                        </div>

                         <div class="form-group col-lg-3 col-md-6 col-12" id="divauto" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Check Auto Serial Number</label>
                                    </div>
                                    <asp:CheckBox ID="chkAutoSerial" runat="server" CssClass="form-control" Checked="false" TabIndex="5" AutoPostBack="true" OnCheckedChanged="chkAutoSerial_CheckedChanged"/>

                                </div>
              

                        <asp:Panel ID="pnlitems" runat="server" >
                            <asp:ListView ID="lvitems" runat="server" OnItemDataBound="lvitems_DataBound">

                                <LayoutTemplate>
                                    <div>
                                        <div class="sub-heading">
                                            <h5>Items List</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.
                                                    </th>
                                                  <%--  <th> item no

                                                    </th>--%>
                                                    <th>Items Name
                                                    </th>
                                                    <%-- <th>Items Quentity
                                                    </th>--%>
                                                    <th>Items Rate
                                                    </th>
                                                    <th>Item Serial Number
                                                    </th>
                                                    <th>Technical Specification
                                                    </th>
                                                    <th>Quality & Qty Details
                                                    </th>
                                                   <%-- <th>Remarks
                                                    </th>--%>
                                                     <th>Department
                                                    </th>
                                                     <th>Location
                                                    </th>
                                                    <%--<th>Depreciation Start Date
                                                    </th>--%>

                                                    <%--<th>GRN Number
                                                    </th>
                                                    <th>Invoice Number
                                                    </th>--%>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                    <%--   <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 200px;">
                                        <div id="demo-grid" class="vista-grid">
                                            <table class="table table-bordered table-hover table-responsive">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>--%>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                           <asp:Label runat="server" ID="lblitemSrNo" Text='<%# Eval("ITEM_SRNO")%>' CssClass="form-control"></asp:Label>
                                           <asp:HiddenField runat="server" ID="hdnDSTK_ENTRY_ID" value='<%# Eval("DSTK_ENTRY_ID")%>' />
                                        </td>
                                        
                                       <%-- <td>
                                              <asp:Label runat="server" ID="lblitemNo" Text='<%# Eval("ITEM_NO")%>' CssClass="form-control"></asp:Label>
                                        </td>--%>
                                        <td>
                                            <asp:Label runat="server" ID="lblItemName" Text='<%# Eval("ITEM_NAME")%>' CssClass="form-control"></asp:Label>
                 
                                        </td>
                                         <%--<td>
                                              <asp:Label runat="server" ID="lblQty" CssClass="form-control" Text='<%# Eval("ITEM_QUENTITY")%>'></asp:Label>
                                           
                                        </td>--%>
                                         <td>
                                              <asp:Label runat="server" ID="lblItemRate" Text='<%# Eval("ITEM_RATE")%>' CssClass="form-control"></asp:Label>
                               <%--              <asp:HiddenField ID="hdQty" runat="server" Value='<%# Eval("ITEM_QUENTITY")%>' />--%>
                                            
                                        </td>
                                        <%-- <td id="td1" runat="server" Enable='<%#rdlList.SelectedValue == "1" ? true : false %>'>--%>
                                        <td>
                                            <asp:TextBox ID="txtSerialNo" runat="server" Text='<%# Eval("DSR_NUMBER")%>'
                                                CssClass="form-control" MaxLength="64" />
                                            <%--<asp:TextBox ID="txtSerialNo" runat="server" Text='<%# Eval("DSR_NUMBER")%>'
                                                CssClass="form-control" MaxLength="64"  Enable='<%#Eval("FLAG").ToString() == "0" ? true : false %>'  />--%>
                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtSerialNo" FilterType="Numbers,Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>  --%> 
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtSpecification" runat="server" Text='<%# Eval("TECH_SPEC")%>'
                                                CssClass="form-control" MaxLength="150" />
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSpecification"
                                                                Display="None" ErrorMessage="Please Enter In  Tech Spacification." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            --%>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtQtySpec" runat="server" Text='<%# Eval("QUALITY_QTY_SPEC")%>'
                                                CssClass="form-control" MaxLength="150" />
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQtySpec"
                                                                Display="None" ErrorMessage="Please Enter In  Quality Spacification." ValidationGroup="submit"></asp:RequiredFieldValidator>--%>

                                        </td>
                                        <td>
                                              <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" >
                                                   <asp:ListItem Selected="True" Text="Please select Issue Slip No." Value="0" ></asp:ListItem>
                                              </asp:DropDownList>  <%--SelectedValue='<%# Bind("DEPARTMENT") %>'--%>

                                        </td>
                                        <td>
                                              <asp:DropDownList ID="ddllocation" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"  >
                                                   <asp:ListItem Selected="True" Text="Please select Issue Slip No." Value="0"></asp:ListItem>
                                              </asp:DropDownList> <%--SelectedValue='<%# Bind("LOCATION") %>'--%>

                                        </td>

                                           </td>
                                        <%--<td>
                                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("ITEM_REMARK")%>'
                                                CssClass="form-control" MaxLength="150" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRemarks"
                                                                Display="None" ErrorMessage="Please Enter In  Remark Feild." ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </td>--%>
                                       <%-- <td>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgCalholidayDt" runat="server" ImageUrl="~/Images/Calendar.png" Style="cursor: pointer" />
                                                </div>
                                                <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="5" Text='<%# Eval("DEPR_CAL_START_DATE")%>'
                                                    ToolTip="Enter  Date" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"  EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCalholidayDt" PopupPosition="BottomLeft" TargetControlID="txtFromDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDt">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtFromDt"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Depreciation Start Date" EmptyValueMessage="Please Select Depreciation Start Date"
                                                    InvalidValueMessage="Depreciation Start Date Is Invalid (Enter In dd/MM/yyyy] Format)" Display="None" SetFocusOnError="true"
                                                    Text="*" ValidationGroup="submit"></ajaxToolKit:MaskedEditValidator>
                                        </td>--%>
                                       <%-- <td>
                                            <%# Eval("GRN_NUMBER")%>                                           
                                        </td>
                                        <td>
                                            <%# Eval("INVNO")%>                                           
                                        </td>--%>


                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>


                        </asp:Panel>
                              
                       
                          
               
                    </div>
                 <asp:Panel ID="PnlitemDetail" runat="server" Visible="false">
                            <asp:ListView ID="listitemDetail" runat="server">

                                <LayoutTemplate>
                                    <div>
                                        <div class="sub-heading">
                                            <h5>Items Detail List</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Items Name
                                                    </th>
                                                    <th>Issue Date
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
                                    <%--   <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 200px;">
                                        <div id="demo-grid" class="vista-grid">
                                            <table class="table table-bordered table-hover table-responsive">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>--%>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                          <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/IMAGES/edit.png" ToolTip="Edit" CommandArgument='<%# Eval("DSTK_ENTRY_ID")%>'
                                                                    OnClick="btnEdit_Click1" /><%--CommandArgument='<%# Eval("REJTRNO")%>'--%>
                                           
                                        </td>

                                        <td>
                                            <%# Eval("ITEM_NAME")%>
                                            
                                            
                                        </td>
                                        <td>
                                             <%-- <%# Eval("ISSUE_DATE")%>--%>
                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("ISSUE_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label>
                                        </td>
                                         <td>
                                              <%# Eval("REMARK")%>

                                            
                                        </td>

                                           </td>
                                       


                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>


                        </asp:Panel>

                 <%--   <div class="col-12">
                       



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
                <script type="text/javascript">
                    function Validate(crl) {
                        if (document.getElementById('<%= txtIssueDate.ClientID %>').value != '') {
                            var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                            if (!(date_regex.test(document.getElementById('<%= txtIssueDate.ClientID %>').value))) {
                                alert("Issue Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                                document.getElementById('<%= txtIssueDate.ClientID %>').value = "";
                                document.getElementById("<%=txtIssueDate.ClientID%>").focus();
                                return false;
                            }
                        }
                    }
                </script>
 

  
</asp:Content>