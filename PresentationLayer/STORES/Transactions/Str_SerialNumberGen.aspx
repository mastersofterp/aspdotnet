<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Str_SerialNumberGen.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="STORES_Transactions_Str_SerialNumberGen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <%-- <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
    --%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">GENERATE ITEM SERIAL NUMBER </h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Select Type </label>
                                    </div>
                                    <asp:RadioButtonList ID="radlTransfer" runat="server" RepeatDirection="Horizontal" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="radlTransfer_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">New&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="1">Modify</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Category</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCategory" runat="server" data-select2-enable="true" CssClass="form-control" TabIndex="2" AutoPostBack="true" AppendDataBoundItems="true"
                                        ToolTip="Select Category" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategoriy" runat="server" ControlToValidate="ddlCategory"
                                        Display="None" ErrorMessage="Please Select Category" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Sub Category</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSubCategory" runat="server" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control"
                                        AutoPostBack="True" TabIndex="3" ToolTip="Select Sub Category" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlSubCategory"
                                        Display="None" ErrorMessage="Please Select Sub Category" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Items </label>
                                    </div>
                                    <asp:DropDownList ID="ddlItems" runat="server" data-select2-enable="true" CssClass="form-control" TabIndex="4"
                                        AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Items" OnSelectedIndexChanged="ddlItems_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlItems"
                                        Display="None" ErrorMessage="Please Select Items " InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divauto" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Check Auto Serial Number</label>
                                    </div>
                                    <asp:CheckBox ID="chkAutoSerial" runat="server" CssClass="form-control" Checked="false" TabIndex="5" AutoPostBack="true" OnCheckedChanged="chkAutoSerial_CheckedChanged" />

                                </div>


                            </div>



                        </div>
                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <p class="text-center">
                            <asp:Button ID="btnShow" runat="server" Text="Show" Font-Bold="true" Visible="false"
                                ValidationGroup="submit" CssClass="btn btn-primary" ToolTip="Click here to Show" TabIndex="10" />
                            <%-- OnClick="btnShow_Click"--%>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="true"
                                ValidationGroup="submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="10" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Clear" TabIndex="10" OnClick="btnCancel_Click" />
                            &nbsp;&nbsp;
                           <asp:ValidationSummary ID="ValidationSummury" runat="server" DisplayMode="List"
                               ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />

                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlitems" runat="server" Visible="false">
                            <asp:ListView ID="lvitems" runat="server">

                                <LayoutTemplate>
                                    <div>
                                        <div class="sub-heading">
                                            <h5>Items List</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No
                                                    </th>
                                                    <th>Items Name
                                                    </th>
                                                    <th>Serial Number
                                                    </th>
                                                    <th>Technical Specification
                                                    </th>
                                                    <th>Quality & Qty Details
                                                    </th>
                                                    <th>Remarks
                                                    </th>
                                                    <th>Depreciation Start Date
                                                    </th>
                                                    <th>GRN Number
                                                    </th>
                                                    <th>Invoice Number
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
                                            <%# Eval("SNO")%>
                                        </td>

                                        <td>
                                            <%# Eval("ITEM_NAME")%>
                                            <asp:HiddenField ID="INVDINO" runat="server" Value='<%# Eval("INVDINO") %>' />
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
                                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("ITEM_REMARK")%>'
                                                CssClass="form-control" MaxLength="150" />
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRemarks"
                                                                Display="None" ErrorMessage="Please Enter In  Remark Feild." ValidationGroup="submit"></asp:RequiredFieldValidator>--%>

                                        </td>
                                        <td>
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
                                        </td>
                                        <td>
                                            <%# Eval("GRN_NUMBER")%>                                           
                                        </td>
                                        <td>
                                            <%# Eval("INVNO")%>                                           
                                        </td>


                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>


                        </asp:Panel>











                    </div>

                </div>

            </div>

        </div>
    </div>


    <%--  <script type="text/javascript">
        function VAlidation(crl) {
            //alert('hi');
            debugger
           // alert('hii');
            //var CalPaymentAmt = 0;
            //var BillAmount = 0;
            //var payNowAmt = 0;
            //var TotalPaid = 0;
            //var BalAmt = 0;
            var RowCount = Number(document.getElementById('<%=hdnRowCount.ClientID%>').value);

            var i = 0;
            for (i = 0; i < RowCount; i++) {
                // CalPaymentAmt += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvVendorPay_ctrl' + i + '_txtPayNowAmt').value);
                //ctl00_ContentPlaceHolder1_lvitems_ctrl0_txtFromDt
             var   srno = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvitems_ctrl' + i + '_txtSerialNo').value);

             var spcf = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvVendorPay_ctrl' + i + '_txtSpecification').value);
             var Qtyspcf = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvVendorPay_ctrl' + i + '_txtQtySpec').value);
             var txtFromDt = Qtyspcf = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvVendorPay_ctrl' + i + '_txtFromDt').value);
                if (srno == "") {
                    alert('Please Fill the Serial Number Field');
                }
                else if (spcf == "") {
                    alert('Please Fill  the  Technical Spacification feild');
                }
                else if (Qtyspcf == "") {
                    alert('Please Fill  in the quality Details');
                }
                else if (txtFromDt == "") {



                    alert('Please Select The Depreciation Date.');
                   
                }
            }
            
        }

    </script>--%>
</asp:Content>


