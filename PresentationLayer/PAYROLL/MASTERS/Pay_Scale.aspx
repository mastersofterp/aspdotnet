<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Scale.aspx.cs" Inherits="PayRoll_Pay_Scale" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--===== Data Table Script added by gaurav =====--%>
         <script>
             $(document).ready(function () {
                 var table = $('#mytable').DataTable({
                     responsive: true,
                     lengthChange: true,
                     scrollY: 320,
                     scrollX: true,
                     scrollCollapse: true,
                     paging: false,

                     dom: 'lBfrtip',
                     buttons: [
                         {
                             extend: 'colvis',
                             text: 'Column Visibility',
                             columns: function (idx, data, node) {
                                 var arr = [0];
                                 if (arr.indexOf(idx) !== -1) {
                                     return false;
                                 } else {
                                     return $('#mytable').DataTable().column(idx).visible();
                                 }
                             }
                         },
                         {
                             extend: 'collection',
                             text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                             buttons: [
                                     {
                                         extend: 'copyHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [0];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#mytable').DataTable().column(idx).visible();
                                                 }
                                             }
                                         }
                                     },
                                     {
                                         extend: 'excelHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [0];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#mytable').DataTable().column(idx).visible();
                                                 }
                                             }
                                         }
                                     },
                                     {
                                         extend: 'pdfHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [0];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#mytable').DataTable().column(idx).visible();
                                                 }
                                             }
                                         }
                                     },
                             ]
                         }
                     ],
                     "bDestroy": true,
                 });
             });
             var parameter = Sys.WebForms.PageRequestManager.getInstance();
             parameter.add_endRequest(function () {
                 $(document).ready(function () {
                     var table = $('#mytable').DataTable({
                         responsive: true,
                         lengthChange: true,
                         scrollY: 320,
                         scrollX: true,
                         scrollCollapse: true,
                         paging: false,

                         dom: 'lBfrtip',
                         buttons: [
                             {
                                 extend: 'colvis',
                                 text: 'Column Visibility',
                                 columns: function (idx, data, node) {
                                     var arr = [0];
                                     if (arr.indexOf(idx) !== -1) {
                                         return false;
                                     } else {
                                         return $('#mytable').DataTable().column(idx).visible();
                                     }
                                 }
                             },
                             {
                                 extend: 'collection',
                                 text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                                 buttons: [
                                         {
                                             extend: 'copyHtml5',
                                             exportOptions: {
                                                 columns: function (idx, data, node) {
                                                     var arr = [0];
                                                     if (arr.indexOf(idx) !== -1) {
                                                         return false;
                                                     } else {
                                                         return $('#mytable').DataTable().column(idx).visible();
                                                     }
                                                 }
                                             }
                                         },
                                         {
                                             extend: 'excelHtml5',
                                             exportOptions: {
                                                 columns: function (idx, data, node) {
                                                     var arr = [0];
                                                     if (arr.indexOf(idx) !== -1) {
                                                         return false;
                                                     } else {
                                                         return $('#mytable').DataTable().column(idx).visible();
                                                     }
                                                 }
                                             }
                                         },
                                         {
                                             extend: 'pdfHtml5',
                                             exportOptions: {
                                                 columns: function (idx, data, node) {
                                                     var arr = [0];
                                                     if (arr.indexOf(idx) !== -1) {
                                                         return false;
                                                     } else {
                                                         return $('#mytable').DataTable().column(idx).visible();
                                                     }
                                                 }
                                             }
                                         },
                                 ]
                             }
                         ],
                         "bDestroy": true,
                     });
                 });
             });

    </script>


    <asp:UpdatePanel ID="updmain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAY SCALE</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Pay Scale</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Basic 1</label>
                                            </div>
                                            <input type="hidden" id="hidCheck" runat="server" />
                                            <asp:TextBox ID="txtBasic1" runat="server" MaxLength="6" CssClass="form-control" TabIndex="1" ToolTip="Enter Basic 1" OnKeyup="GetScale();"
                                                OnBlur="return validation(this);"  onkeypress="return isNumber(event)" />
                                        <%--    <asp:RangeValidator ID="rngBasic1" runat="server" ControlToValidate="txtBasic1" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 1 to 99999" ValidationGroup="payroll"
                                                MinimumValue="1" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                            <asp:RequiredFieldValidator ID="rvfBasci1" runat="server" ControlToValidate="txtBasic1"
                                                Display="None" ErrorMessage="Please Enter Basic 1" ValidationGroup="payroll"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Increment 1</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement1" runat="server" MaxLength="6" CssClass="form-control" TabIndex="2" ToolTip="Enter Increment 1" OnKeyup="GetScale();"  onkeypress="return isNumber(event)" />
                                         <%--   <asp:RangeValidator ID="rngIncrement1" runat="server" ControlToValidate="txtIncrement1"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999"></asp:RangeValidator>--%>
                                            <asp:RequiredFieldValidator ID="rfvIncrement1" runat="server" ControlToValidate="txtIncrement1"
                                                Display="None" ErrorMessage="Please Enter Increment 1" ValidationGroup="payroll"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Basic 2</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic2" runat="server" MaxLength="6" CssClass="form-control" TabIndex="3" ToolTip="Enter Basic 2" OnKeyup="GetScale();"
                                                OnBlur="return validation();"   onkeypress="return isNumber(event)"/>
                                            <%--<asp:TextBox ID="TextBox1" runat="server" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();" />--%>
                                           <%-- <asp:RangeValidator ID="rngBasic2" runat="server" ControlToValidate="txtBasic2" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 1 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999"></asp:RangeValidator>--%>
                                            <asp:RequiredFieldValidator ID="rfvbasic2" runat="server" ControlToValidate="txtBasic2"
                                                Display="None" ErrorMessage="Please Enter Basic 2 " ValidationGroup="payroll"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 2</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement2" runat="server" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();" TabIndex="4" ToolTip="Enter Increment 2"   onkeypress="return isNumber(event)"/>
                                           <%-- <asp:RangeValidator ID="rngIncrement2" runat="server" ControlToValidate="txtIncrement2"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                            <asp:CustomValidator runat="server" SetFocusOnError="true" ID="cvtxtIncrement2" ControlToValidate="txtIncrement2"
                                                Display="None" ValidationGroup="payroll" EnableClientScript="true" ClientValidationFunction="validation"></asp:CustomValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 3</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic3" runat="server" MaxLength="6" CssClass="form-control" TabIndex="5" ToolTip="Enter Basic 3"  OnKeyup="GetScale();"  OnBlur="return validation();"   onkeypress="return isNumber(event)"/>
                                           <%-- <asp:RangeValidator ID="rngBasic3" runat="server" ControlToValidate="txtBasic3" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                            <asp:CustomValidator ID="cvtxtBasic3" runat="server" ControlToValidate="txtBasic3"
                                                SetFocusOnError="True" Display="None" ValidationGroup="payroll" Text="Please enter Basic3"
                                                ClientValidationFunction="validation"> </asp:CustomValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 3</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement3" runat="server" MaxLength="6" CssClass="form-control" TabIndex="6" ToolTip="Enter Increment 3" OnKeyup="GetScale();"
                                               onkeypress="return isNumber(event)" />
                                           <%-- <asp:RangeValidator ID="rngIncrement3" runat="server" ControlToValidate="txtIncrement3"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 4</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic4" runat="server" MaxLength="6" CssClass="form-control" ToolTip="Enter Basic 4" TabIndex="7" OnKeyup="GetScale();"   OnBlur="return validation(); "  onkeypress="return isNumber(event)"/>
                                          <%--  <asp:RangeValidator ID="rngtxtBasic4" runat="server" ControlToValidate="txtBasic4"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 4</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement4" runat="server" MaxLength="6" CssClass="form-control" ToolTip="Enter Increment 4" TabIndex="8" OnKeyup="GetScale();"   onkeypress="return isNumber(event)"/>
                                          <%--  <asp:RangeValidator ID="rngIncrement4" runat="server" ControlToValidate="txtIncrement4"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 5</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic5" runat="server" TabIndex="9" ToolTip="Enter Basic 5" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"  OnBlur="return validation();"  onkeypress="return isNumber(event)"/>
                                            <%--<asp:RangeValidator ID="rngBasic5" runat="server" ControlToValidate="txtBasic5" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 5</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement5" runat="server" MaxLength="6" TabIndex="10" ToolTip="Enter Increment 5" CssClass="form-control" OnKeyup="GetScale();"   onkeypress="return isNumber(event)"/>
                                            <%--<asp:RangeValidator ID="rngIncrement5" runat="server" ControlToValidate="txtIncrement5"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 6</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic6" runat="server" TabIndex="11" ToolTip="Enter Basic 6" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"  OnBlur="return validation();"  onkeypress="return isNumber(event)"/>
                                           <%-- <asp:RangeValidator ID="rngBasic6" runat="server" ControlToValidate="txtBasic6" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 6</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement6" runat="server" MaxLength="6" TabIndex="12" ToolTip="Enter Increment 6" CssClass="form-control" OnKeyup="GetScale();"   onkeypress="return isNumber(event)"/>
                                           <%-- <asp:RangeValidator ID="rngIncrement6" runat="server" ControlToValidate="txtIncrement6"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 7</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic7" runat="server" TabIndex="13" ToolTip="Enter Basic 7" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"  OnBlur="return validation();"  onkeypress="return isNumber(event)"/>
                                        <%--    <asp:RangeValidator ID="rngBasic7" runat="server" ControlToValidate="txtBasic7" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 7</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement7" runat="server" MaxLength="6" TabIndex="14" ToolTip="Enter Increment 7" CssClass="form-control" OnKeyup="GetScale();"  onkeypress="return isNumber(event)" />
                                           <%-- <asp:RangeValidator ID="rngIncrement7" runat="server" ControlToValidate="txtIncrement7"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999"
                                                ValidationGroup="payroll" MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 8</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic8" runat="server" TabIndex="15" ToolTip="Enter Basic 8" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();" OnBlur="return validation();"  onkeypress="return isNumber(event)" />
                                           <%-- <asp:RangeValidator ID="rngBasic8" runat="server" ControlToValidate="txtBasic8" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 8</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement8" runat="server" TabIndex="16" ToolTip="Enter Increment 8" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"   onkeypress="return isNumber(event)"/>
                                           <%-- <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtIncrement8" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 9</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic9" runat="server" TabIndex="17" ToolTip="Enter Basic 9" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"  OnBlur="return validation();"  onkeypress="return isNumber(event)"/>
                                           <%-- <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtBasic9" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 9</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement9" runat="server" TabIndex="18" ToolTip="Enter Increment 9" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"   onkeypress="return isNumber(event)"/>
                                          <%--  <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtIncrement9" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 10</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic10" runat="server" TabIndex="19" ToolTip="Enter Basic 10" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"  OnBlur="return validation();"  onkeypress="return isNumber(event)"/>
                                           <%-- <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtBasic10" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Increment 10</label>
                                            </div>
                                            <asp:TextBox ID="txtIncrement10" runat="server" TabIndex="20" ToolTip="Enter Increment 10" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"  onkeypress="return isNumber(event)" />
                                         <%--   <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtIncrement10" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Basic 11</label>
                                            </div>
                                            <asp:TextBox ID="txtBasic11" runat="server" TabIndex="21" ToolTip="Enter Basic 11" MaxLength="6" CssClass="form-control" OnKeyup="GetScale();"  OnBlur="return validation();"  onkeypress="return isNumber(event)"/>
                                          <%--  <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtBasic11" SetFocusOnError="True"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 99999" ValidationGroup="payroll"
                                                MinimumValue="0" MaximumValue="99999" Type="Integer"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>CCA</label>
                                            </div>
                                            <asp:TextBox ID="txtscalerange" runat="server" MaxLength="6" TabIndex="22" ToolTip="Enter Scale No" CssClass="form-control" BackColor="#FFFF99"  onkeypress="return isNumber(event)" />
                                 <%--           <asp:RequiredFieldValidator ID="rfvscalerange" runat="server" ControlToValidate="txtscalerange"
                                                Display="None" ErrorMessage="Please Enter Scale No " ValidationGroup="payroll"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:RequiredFieldValidator ID="rfvscalerange" runat="server" ControlToValidate="txtscalerange" 
                                                ErrorMessage="Please Enter Scale No" ValidationGroup="payroll" SetFocusOnError="true" ForeColor="Red" ></asp:RequiredFieldValidator>
                                            <%--<asp:RangeValidator ID="rngtxtscalerange" runat="server" ControlToValidate="txtscalerange"
                                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Numbers between 1 to 99999.99"
                                                ValidationGroup="payroll" MinimumValue="1" MaximumValue="99999.99" Type="Double"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                 <sup>* </sup>
                                                <label>Rule :</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRule" AppendDataBoundItems="true" BackColor="#FFFF99" runat="server"
                                                CssClass="form-control" TabIndex="23" ToolTip="Select Rule" data-select2-enable="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRule" runat="server" ControlToValidate="ddlRule"
                                                Display="None" ErrorMessage="Select Rule" ValidationGroup="payroll" SetFocusOnError="True" ForeColor="Red"
                                               ></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Scale</label>
                                            </div>
                                            <asp:TextBox ID="txtScale" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="24" ToolTip="Enter Scale" Font-Bold="true" ForeColor="Green"
                                                BackColor="#FFFF99" disabled="true"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Grade Pay</label>
                                            </div>
                                            <input type="hidden" id="hidgradepay" runat="server" />
                                             <input type="hidden" id="hidepradepay1" runat="server" />
                                            <input type="hidden" id="checkhiden" runat="server" />
                                            <asp:TextBox ID="txtGradePay" runat="server" MaxLength="6" TabIndex="25" ToolTip="Enter Grade Pay" CssClass="form-control" BackColor="#FFFF99"
                                                OnKeyup="return GradePay(this.value);"   onkeypress="return isNumber(event)"/>
                                         <%--   <asp:RangeValidator ID="reGradePay" runat="server" ControlToValidate="txtGradePay"
                                                Display="None" ErrorMessage="Please Enter Numbers between 0 to 9999999.99" ValidationGroup="payroll"
                                                MaximumValue="9999999.99" MinimumValue="0" Type="Double"></asp:RangeValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Short Scale Name</label>
                                            </div>
                                            <asp:TextBox ID="txtshortScalename" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="26" ToolTip="Enter Short Scale Name" Font-Bold="true"  onkeypress="return isNumber(event)"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtshortScalename"
                                                ErrorMessage="Please Enter Scale Short Name " ValidationGroup="payroll"
                                                SetFocusOnError="True" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                </fieldset>
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </asp:Panel>

                            <div id="pnlList" runat="server">
                                <div class="col-12">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                         <sup>* </sup>
                                            <label>SELECT RULE</label>
                                        </div>
                                        <asp:DropDownList ID="ddlpayruleselect" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" TabIndex="27" ToolTip="Select Rule" data-select2-enable="true" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div id="pnlbtn" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnAdd" CssClass="btn btn-primary" TabIndex="28" ToolTip="Click add new to enter scale" Text="Add New" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click"></asp:LinkButton>
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" OnClick="btnSave_Click"
                                        CssClass="btn btn-primary" TabIndex="29" ToolTip="Click Save To Submit" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnback_Click"
                                        TabIndex="30" ToolTip="Click to go to Previous" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" TabIndex="31" ToolTip="Click to Show the Report"
                                        OnClick="btnShowReport_Click" Visible="false" />
                                     <asp:Button ID="btnshowScaleReport" runat="server" Text="Show Scale Report" CssClass="btn btn-info" TabIndex="31" ToolTip="Click to Show the Report" OnClick="btnshowScaleReport_Click"
                                       />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" TabIndex="32" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </div>

                            <div id="List" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvScale" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click add new to enter scale" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Scale Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Scale
                                                        </th>
                                                        <th>Grade Pay
                                                        </th>
                                                        <th>Rule
                                                        </th>
                                                        <%--<th>Scale No
                                                        </th>--%>
                                                        <th>CCA
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SCALENO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("SCALE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("GRADEPAY")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAYRULE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SCALERANGE")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div class="col-12 text-center d-none">
                                    <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvScale" PageSize="100"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
      <%--  <triggers>
            <asp:postbacktrigger controlid="btnshowreport" />
        </triggers>--%>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
    <script type="text/javascript" language="javascript">

        function GetScale() {
            debugger;
            var string = '';

            for (i = 1; i <= 11; i++) {
                if ((document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i) != null && document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i) != "" && document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i) != "0")) {
                    if ((document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i).value.trim() != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i).value.trim() != "") && (document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i).value.trim() != "0")) {
                        if (string.length > 0) {
                            string += "-";
                        }
                        string += document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i).value;
                    }
                }

                if ((document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i) != null && document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i) != "" && document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i) != "0")) {
                    if ((document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i).value.trim() != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i).value.trim() != "") && (document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i).value.trim() != "0")) {
                        if (string.length > 0) {
                            string += "-";
                        }
                        string += document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i).value;
                    }
                }
                document.getElementById('ctl00_ContentPlaceHolder1_txtScale').value = string;
                document.getElementById('ctl00_ContentPlaceHolder1_txtshortScalename').value = string;
                document.getElementById('ctl00_ContentPlaceHolder1_hidgradepay').value = document.getElementById('ctl00_ContentPlaceHolder1_txtScale').value;
                document.getElementById('ctl00_ContentPlaceHolder1_hidepradepay1').value = document.getElementById('ctl00_ContentPlaceHolder1_txtshortScalename').value;                             
            }
            /////////////// added for grade pay
            var gradePay11 = document.getElementById('ctl00_ContentPlaceHolder1_txtGradePay').value;
            var hidsale = document.getElementById('ctl00_ContentPlaceHolder1_hidgradepay').value;
            var hidsale1 = document.getElementById('ctl00_ContentPlaceHolder1_hidepradepay1').value;
            if (!(gradePay11 == null || gradePay11 == "")) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtScale').value = hidsale + "(" + gradePay11 + ")";
                document.getElementById('ctl00_ContentPlaceHolder1_txtshortScalename').value = hidsale1 + "(" + gradePay11 + ")";

            }
            else {
                gradePay11 = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_txtScale').value = hidsale + "(" + gradePay11 + ")";
                document.getElementById('ctl00_ContentPlaceHolder1_txtshortScalename').value = hidsale1 + "(" + gradePay11 + ")";
            }
        }
        function GradePay(gradePay) {
            var hidsale = document.getElementById('ctl00_ContentPlaceHolder1_hidgradepay').value;

            if (!(gradePay == null || gradePay == "")) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtScale').value = hidsale + "(" + gradePay + ")";
                document.getElementById('ctl00_ContentPlaceHolder1_txtshortScalename').value = hidsale + "(" + gradePay + ")";
            }
            else {
                gradePay = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_txtScale').value = hidsale;
                document.getElementById('ctl00_ContentPlaceHolder1_txtshortScalename').value = hidsale + "(" + gradePay + ")";
            }
        }


        function validation() {
            var txtBasic1 = document.getElementById('ctl00_ContentPlaceHolder1_txtBasic1').value;
            var txtIncrement1 = document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement1').value;
            var txtBasic5 = document.getElementById('ctl00_ContentPlaceHolder1_txtBasic5').value;
            var txtIncrement5 = document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement5').value;

            for (i = 1; i <= 11; i++) {
                var count = Number(i) + 1;
                var txtBasic = document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + count).value;
                var txtIncrement = document.getElementById('ctl00_ContentPlaceHolder1_txtIncrement' + i).value;

                if (!(txtBasic == null || txtBasic == "" || txtBasic == 0)) {
                    if (Number(txtBasic) < Number(document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + i).value)) {
                        alert("Please Enter Basic" + count + " Greater Than Basic" + i);
                        document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + count).value = "";
                        document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + count).focus();
                    }
                }


                //if (!(txtIncrement == null || txtIncrement == "" || txtIncrement == 0) && (txtBasic == null || txtBasic == "" || txtBasic == 0)) {
                //    alert("Please Entere Basic" + count);
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + count).value = "";
                //    document.getElementById('ctl00_ContentPlaceHolder1_txtBasic' + count).focus();
                //}


            }

            if (!(txtIncrement1 == null || txtIncrement1 == "" || txtIncrement1 == 0) && (txtBasic1 == null || txtBasic1 == "" || txtBasic1 == 0)) {
                alert("Please Entere Basic1");
                document.getElementById('ctl00_ContentPlaceHolder1_txtBasic1').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtBasic1').focus();
            }

            if (!(txtIncrement5 == null || txtIncrement5 == "" || txtIncrement5 == 0) && (txtBasic5 == null || txtBasic5 == "" || txtBasic5 == 0)) {
                alert("Please Entere Basic5");
                document.getElementById('ctl00_ContentPlaceHolder1_txtBasic5').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtBasic5').focus();
            }

        }
    </script>
    <script type="text/javascript" language="javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
