<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Vendor_Quotation_Entry.aspx.cs" Inherits="Stores_Transactions_Quotation_Str_Vendor_Quotation_Entry"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<link href="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <link href="../../../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multi-select/jquery.multiselect.js"></script>--%>
    <%-- <script src="../../../plugins/multiselect/jquery.multiselect.js"></script>
    <link href="../../../plugins/multiselect/jquery.multiselect.css" rel="stylesheet" />--%>

    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>


    <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div4" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VENDOR QUOTATION ENTRY FORM</h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12" id="divQuotlist" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Quotation List<span style="color: red">*</span></label>
                                        </div>
                                        <%--  <asp:Panel ID="pnlQuotlist" runat="server">--%>
                                        <asp:DropDownList ID="lstQtNo" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="1" ToolTip="Quotation List"
                                            OnSelectedIndexChanged="lstQtNo_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- </asp:Panel>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Vendor List<span style="color: red">*</span></label>
                                        </div>
                                        <%-- <asp:Panel ID="Panel5" runat="server">--%>
                                        <asp:DropDownList ID="lstVendor" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="2" ToolTip="Vendor List"
                                            OnSelectedIndexChanged="lstVendor_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  </asp:Panel>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divLastDate" runat="server">
                                        <div class="label-dynamic">
                                            <label>Last Submission Date</label>
                                        </div>
                                        <asp:TextBox ID="txtLastDate" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlitems" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Item List</h5>
                                    </div>
                                    <div class="col-12 table-responsive">
                                        <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdItemList"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="ITEM_NO" EnableModelValidation="True">
                                            <Columns>

                                                <asp:BoundField HeaderText="Item Name" DataField="ITEM_NAME" HeaderStyle-CssClass="bg-light-blue"></asp:BoundField>

                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnPINO" runat="server" Value='<%#Eval("PINO") %>' />
                                                        <asp:Label ID="lblQty" Text='<%#Eval("QTY") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("QTY") %>' />
                                                        <asp:HiddenField ID="hdnItemName" runat="server" Value='<%#Eval("ITEM_NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRate" runat="server" Text='<%#Eval("PRICE") %>' CssClass="form-control" TabIndex="1" MaxLength="9" onblur="return AddTotalAmountsOnRate(this)">                                                                    
                                                        </asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                            TargetControlID="txtRate" ValidChars=".">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfvtxtRate" runat="server" ErrorMessage="Please Enter Proper Rate"
                                                            Display="None" ValidationGroup="Store" EnableClientScript="true" ControlToValidate="txtRate"></asp:RequiredFieldValidator>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disc%" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDisc" runat="server" onblur="return AddTotalAmountsOnDiscount(this);" Enabled="true" Text='<%#Eval("DISCOUNT_PERCENT") %>' MaxLength="3" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="txtDiscFilter" runat="server" FilterType="Numbers,Custom"
                                                            FilterMode="ValidChars" TargetControlID="txtDisc" ValidChars=".">
                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                        <asp:HiddenField ID="hdnItemDiscPer" runat="server" Value='<%#Eval("DISCOUNT_PERCENT") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disc Amt" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDiscAmt" runat="server" onblur="return AddTotalAmountsOnDiscountAmount(this)" Enabled="true" Text='<%#Eval("DISCOUNT_AMOUNT") %>'
                                                            MaxLength="9" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftpDiscamt" runat="server" FilterType="Numbers,Custom"
                                                            FilterMode="ValidChars" TargetControlID="txtDiscAmt" ValidChars=".">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:HiddenField ID="hdnItemDiscAmt" runat="server" Value='<%#Eval("DISCOUNT_AMOUNT") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Taxable Amt" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTaxableAmt" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text='<%#Eval("TAXABLE_AMT") %>'>
                                                        </asp:TextBox>
                                                        <asp:HiddenField ID="hdnOthItemRemark" runat="server" />
                                                        <asp:HiddenField ID="hdnItemTaxableAmt" runat="server" Value='<%#Eval("TAXABLE_AMT") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax Info" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btnAddTax" ImageUrl="~/Images/Addblue.png" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add" OnClientClick="return GetTaxableAmt(this);" OnClick="btnAddTax_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax Amt" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTaxAmt" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text='<%#Eval("TAX_AMT") %>'>
                                                        </asp:TextBox>
                                                        <asp:HiddenField ID="hdnTechSpec" runat="server" />
                                                        <asp:HiddenField ID="hdnQualityQtySpec" runat="server" />
                                                        <asp:HiddenField ID="hdnItemTaxAmt" runat="server" Value='<%#Eval("TAX_AMT") %>' />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill Amt" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text='<%#Eval("TOTAMOUNT") %>'>
                                                        </asp:TextBox>
                                                        <asp:HiddenField ID="hdnItemTotalAmt" runat="server" Value='<%#Eval("TOTAMOUNT") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Oth Info" HeaderStyle-CssClass="bg-light-blue">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="btnAddOthInfo" ImageUrl="~/Images/Addblue.png" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add Oth Info" OnClientClick="return GetOthInfoIndex(this);" OnClick="btnAddOthInfo_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="altitem" />
                                            <EmptyDataRowStyle CssClass="altitem" />
                                            <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                            <RowStyle CssClass="item" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%-- <asp:UpdatePanel ID="updPopup" runat="server">
                        <ContentTemplate>--%>
                            <div class="col-12" id="divItemEntryList" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Item Entry List</h5>
                                </div>
                                <asp:Panel ID="PnlItemEntry" runat="server">
                                    <div class="col-12 table-responsive">
                                        <asp:ListView ID="lvItemEntry" runat="server">
                                            <EmptyDataTemplate>
                                                <center>
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                </center>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Item Name</th>
                                                            <th>Unit </th>
                                                            <th>Qty</th>
                                                            <th>Rate</th>
                                                            <th>Disc%</th>
                                                            <th>Discount Amount</th>
                                                            <th>Bill Amount</th>
                                                            <%-- <th></th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="javascript:return confirm('Are You Sure You Want To Delete This Record?')" CommandArgument='<%# Eval("ITEM_NO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("ITEM_NAME")%>                                                                   
                                                    </td>

                                                    <td>
                                                        <%# Eval("UNIT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("QTY")%>                                                                   
                                                    </td>

                                                    <td>
                                                        <%# Eval("PRICE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DISCOUNT_PERCENT")%>                                                                   
                                                    </td>

                                                    <td>
                                                        <%# Eval("DISCOUNT_AMOUNT")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("TOTAMOUNT")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>
                            </div>
                            <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>

                            <asp:Panel ID="pnlCmpst" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Quotation List <span style="color: red">*</span></label>
                                            </div>
                                            <asp:Panel ID="Panel4" runat="server">
                                                <asp:DropDownList ID="lstQuot1" CssClass="form-control" runat="server" AutoPostBack="True" TabIndex="1" ToolTip="Quotation List"
                                                    OnSelectedIndexChanged="lstQuot1_SelectedIndexChanged" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divItemList">
                                            <div class="label-dynamic">
                                                <label>Item List </label>
                                            </div>
                                            <asp:Panel ID="Panel10" runat="server">
                                                <asp:DropDownList ID="lstItem" CssClass="form-control" runat="server" TabIndex="2" ToolTip="Item List" AppendDataBoundItems="true">
                                                     <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12">
                                <ajaxToolKit:ModalPopupExtender ID="MdlTax" runat="server" PopupControlID="pnlTaxDetail" TargetControlID="lblTax"
                                    BackgroundCssClass="modalBackground" BehaviorID="mdlPopupDel" CancelControlID="ImgTax">
                                </ajaxToolKit:ModalPopupExtender>

                                <asp:Label ID="lblTax" runat="server"></asp:Label>

                                <asp:Panel ID="pnlTaxDetail" runat="server" CssClass="PopupReg" Style="display: none; height: auto; width: 50%; background: #fff; z-index: 333; box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;">
                                    <div class="col-12">
                                        <div class=" sub-heading mt-3">
                                            <h5>Add Details</h5>
                                            <div class="box-tools pull-right">
                                                <asp:ImageButton ID="ImgTax" runat="server" ImageUrl="~/IMAGES/delete.png" ToolTip="Close" />
                                            </div>
                                        </div>
                                        <div class="panel panel-body">
                                            <div class="form-group col-md-12" id="divTaxPopup" runat="server" visible="false">
                                                <div class="form-group col-md-12">
                                                    <asp:ListView ID="lvTax" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <%--<th style="width: 5%">Select                                                                           
                                                                                </th>--%>
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
                                                                    <asp:TextBox ID="lblTaxAmount" runat="server" CssClass="form-control" Text='<%#Eval("TAX_AMOUNT") %>' onblur="CalTotTaxAmt(this)" MaxLength="9" onkeypress="return numeric(event);"></asp:TextBox>
                                                                </td>


                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <label>Total Tax Amount</label>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <asp:TextBox ID="txtTotTaxAmt" runat="server" MaxLength="9" CssClass="form-control" Enabled="false" />
                                                    </div>

                                                </div>
                                                <div class="form-group col-md-12" style="text-align: center">
                                                    <asp:Button ID="btnSaveTax" runat="server" CssClass="btn btn-primary" Text="Save Tax" OnClientClick="return GetTotTaxAmt();" OnClick="btnSaveTax_Click" />

                                                </div>
                                            </div>
                                            <div class="form-group col-md-12" id="divOthPopup" runat="server" visible="true">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Technical Specification</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTechSpec" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Quality& Qty Specification</label>
                                                        </div>
                                                        <asp:TextBox ID="txtQualityQtySpec" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Item Remark</label>
                                                        </div>
                                                        <asp:TextBox ID="txtItemRemarkOth" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSaveOthInfo" runat="server" CssClass="btn btn-primary" Text="Add" OnClientClick="return SaveOthInfo();" OnClick="btnSaveOthInfo_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <%--//=================================file upload ==================================================//--%>

                             <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attach File</label>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group date">
                                                    <asp:FileUpload ID="Uploadinvoice" runat="server" ValidationGroup="complaint" ToolTip="Select file to upload" TabIndex="9" />
                                                    <div class="input-group-addon">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                                                            CssClass="btn btn-primary"
                                                            CausesValidation="False" TabIndex="10" ToolTip="Add Attach File" />
                                                        <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnAdd" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                            <ProgressTemplate>
                                                <div class="overlay">
                                                    <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                                        <img alt="" src="loader.gif" />
                                                    </div>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>

                             <div class="form-group col-12">
                                        <div id="divAttch" runat="server" >
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlAttachmentList" runat="server" ScrollBars="Auto" Visible="false">
                                                        <asp:ListView ID="lvCompAttach" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Delete</th>
                                                                            <th >Attachments  
                                                                            </th>
                                                                            <th >Download
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
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete.png"
                                                                            CommandArgument=' <%#Eval("FILENAME") %>' ToolTip="Delete Record"
                                                                            OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="ImageButton1_Click" />
                                                                    </td>
                                   
                                                                    <td >
                                                                        <%# Eval("DisplayFileName")%></a>
                                                                    </td>

                                                                    <td style="text-align: center" >
                                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                                            <ContentTemplate>
                                                                                     <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>

                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                            <%--//====================================================================================//--%>



                            <div class="form-group col-md-12 text-center">
                                <asp:Button ID="btncmpitem" runat="server" TabIndex="5" ToolTip="Click To Comparative Stmt. For Single Item" CssClass="btn btn-primary" OnClick="btncmpitem_Click" Visible="false" Text="Comparative Stmt. For Single Item" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ValidationGroup="Store" />
                                <asp:Button ID="btnApproval" runat="server" TabIndex="6" ToolTip="Click To Send For Approval" CssClass="btn btn-primary" OnClick="btnApproval_Click" Text="Send Comparative Stmt. For Approval" Visible="false" />

                                <asp:Button ID="btnBack" runat="server" TabIndex="3" Visible="false" CssClass="btn btn-primary" OnClick="btnBack_Click" Text="Back" />
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="3" Visible="true" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return ValidateSubmit();" />
                                <asp:Button ID="btnShowComp" runat="server" ToolTip="Click To Comparative Statement Excel" TabIndex="3" Visible="true" CssClass="btn btn-info" OnClick="btnShowComp_Click" Text="Comparative Statement" />
                                <asp:Button ID="btncmpall" runat="server" ToolTip="Click To Comparative Statement Excel" Visible="false" TabIndex="3" CssClass="btn btn-info" OnClick="btncmpall_Click" Text="Comparative Statement Excel" />

                                <asp:HiddenField ID="hdnListCount" runat="server" />
                                <asp:HiddenField ID="hdnRowCount" runat="server" />
                            </div>

                            <div class="col-12 table-responsive">
                                <asp:GridView ID="GvCmpAllItem" runat="server" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue">
                                    <HeaderStyle Font-Bold="True" />
                                </asp:GridView>
                            </div>

                            <div>
                                <asp:HiddenField ID="hdnIndex" runat="server" />
                                <asp:HiddenField ID="hdnBasicAmt" runat="server" />
                                <asp:HiddenField ID="hdnTaxableAmt" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnOthEdit" runat="server" Value="0" />
                            </div>
                        </div>

                      <%--  //======================================================================//--%>
                         <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                    </div>



                      <%--  //===========================================================================//--%>


                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btncmpall" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <%--<asp:PostBackTrigger ControlID="lstQtNo" />--%>
           <%-- <asp:PostBackTrigger ControlID="lstVendor" />--%>
            <asp:PostBackTrigger ControlID="grdItemList" />
            <asp:PostBackTrigger ControlID="btnShowComp" />
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="btnSaveTax" />
            <asp:PostBackTrigger ControlID="btnSaveOthInfo" />
            <%-- <asp:PostBackTrigger ControlID="lstQuot1" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <%--<asp:UpdatePanel ID="updVendorentry" runat="server">
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>
    <%--  Reset the sample so it can be played again --%>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '0';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }




        function CheckMaxLength(crl) {
            var myArr = new Array();
            myString = "" + crl.id + "";
            myArr = myString.split("_");
            var index = myArr[3];
            var Ftype = document.getElementById("ctl00_ContentPlaceHolder1_lvFieldList_" + index + "_hdnFtype").value;
            if (Ftype == "INFORMATIVE") {
                document.getElementById("ctl00_ContentPlaceHolder1_lvFieldList_" + index + "_txtFieldValue").maxLength = 50;
                //var Value1 = document.getElementById("ctl00_ContentPlaceHolder1_lvFieldList_" + index + "_txtFieldValue").value;
            }
            else {
                var Value2 = document.getElementById("ctl00_ContentPlaceHolder1_lvFieldList_" + index + "_txtFieldValue").maxLength = 7;
                if (isNaN(crl.value)) {
                    crl.value = crl.value.substring(0, (crl.value.length) - 1);
                    crl.value = '';
                    crl.focus = true;
                    alert("Only Numeric Characters allowed !");
                }
            }
        }


        function AddTotalAmountsOnRate(crl) {

            debugger;
            var st = crl.id.split("grdItemList_ctl");
            var i = st[1].split("_txtRate");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').value;
            var TaxAmt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxAmt').value;

            var Discountamt = 0;
            if (Number(discount) == 0 || Number(discount) < 1)
                Discountamt = Number(discamt);
            else
                Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;

            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;

            var taxableAmt = grossAmount;// + taxamt;
            var BillAmt = Number(taxableAmt) + Number(TaxAmt);


            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTotalAmt').value = BillAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemTotalAmt').value = BillAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxableAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemTaxableAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemDiscPer').value = Number(discount).toFixed(2);

        }

        function AddTotalAmountsOnDiscount(crl) {

            debugger;
            var st = crl.id.split("grdItemList_ctl");
            var i = st[1].split("_txtDisc");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').value;
            var TaxAmt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxAmt').value;

            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            //var Discper = (Number(discamt).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;              
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            var taxableAmt = grossAmount;
            var BillAmt = Number(taxableAmt) + Number(TaxAmt);

            if (discount == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').disabled = false;
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').disabled = true;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTotalAmt').value = BillAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemTotalAmt').value = BillAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemDiscPer').value = Number(discount).toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxableAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemTaxableAmt').value = taxableAmt.toFixed(2);
        }

        function AddTotalAmountsOnDiscountAmount(crl) {
            debugger;

            var st = crl.id.split("grdItemList_ctl");
            var i = st[1].split("_txtDiscAmt");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtRate').value;
            //var discount = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + 'hdnItemDiscPer').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').value;
            var TaxAmt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxAmt').value;


            var Discper = (Number(discamt).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;
            //var Discountamt = (Number(rate).toFixed(2) * Number(Discper).toFixed(2) * qty) / 100;             
            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            var taxableAmt = grossAmount;// + taxamt;            
            var Discountamt = (Number(discamt).toFixed(2) * 1) / 1;
            var BillAmt = Number(taxableAmt) + Number(TaxAmt);

            if (discamt == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').disabled = false;
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').disabled = true;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTotalAmt').value = BillAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemTotalAmt').value = BillAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxableAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemTaxableAmt').value = taxableAmt.toFixed(2);

        }

        function GetTaxableAmt(crl) {
            debugger;
            var st = crl.id.split("ctl00_ContentPlaceHolder1_grdItemList_ctl");
            var i = st[1].split("_btnAddTax");
            var index = i[0];
            if (document.getElementById('<%=lstVendor.ClientID%>').value == 0) {
                alert("Please Select Vendor.");
                return false;
            }
            var Rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtRate').value;
            var Qty = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdQty').value;
            if (Number(Rate) > 0) {

                document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdnItemTaxableAmt').value;
                document.getElementById('<%=hdnIndex.ClientID%>').value = index;
                document.getElementById('<%=hdnBasicAmt.ClientID%>').value = (Number(Rate) * Number(Qty)).toFixed(2);                
            }
            else {
                alert("Please Enter Rate And Discount Before Adding Taxes.");
                return false;
            }
        }

        function GetOthInfoIndex(crl) {
            var st = crl.id.split("ctl00_ContentPlaceHolder1_grdItemList_ctl");
            var i = st[1].split("_btnAddOthInfo");
            var index = i[0];
            document.getElementById('<%=hdnIndex.ClientID%>').value = index;
            document.getElementById('<%=txtTechSpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnTechSpec').value;
            document.getElementById('<%=txtQualityQtySpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnQualityQtySpec').value;
            document.getElementById('<%=txtItemRemarkOth.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnOthItemRemark').value;
        }

        function GetTotTaxAmt() {
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxAmt').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
            var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxableAmt').value;
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxableAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxableAmt').value;
            var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTotalAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);

        }
        function SaveOthInfo() {

            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnTechSpec').value = document.getElementById('<%=txtTechSpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnQualityQtySpec').value = document.getElementById('<%=txtQualityQtySpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnOthItemRemark').value = document.getElementById('<%=txtItemRemarkOth.ClientID%>').value;
            document.getElementById('<%=hdnOthEdit.ClientID%>').value = '1';
        }

        function CalTotTaxAmt(crl) {
            var TotAmount = 0;
            var ROWS = document.getElementById('<%=hdnListCount.ClientID%>').value;
            // alert(ROWS);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                TotAmount += Number(document.getElementById("ctl00_ContentPlaceHolder1_lvTax_ctrl" + i + "_lblTaxAmount").value);
            }

            document.getElementById('<%= txtTotTaxAmt.ClientID %>').value = TotAmount.toFixed(2);
        }

        function ValidateRate() {

            var RowCount = document.getElementById('<%=hdnRowCount.ClientID%>').value;

            for (var i = 0; i < RowCount; i++) {

                var Rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + i + '_txtRate').value;

                if (Rate == '' || Rate == 0) {
                    alert('Please Enter Valid Rate');
                    return false;
                }
            }

        }


        function numeric(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57)) {
                return true;
            }
            else {
                alert("Only Numeric Value Allowed");
                return false;
            }
        }
        function ValidateSubmit() {
            //debugger;
            
            if (document.getElementById('<%=lstQtNo.ClientID%>').value == '') {
                alert('Please Select Quotation From List.');
                return false;
            }
            if (document.getElementById('<%=lstVendor.ClientID%>').value == '') {
                alert('Please Select Vendor From List.');
                return false;
            }
           // debugger;
            var RowCount = Number($("#<%=grdItemList.ClientID %> tr").length);
            for (var i = 2; i < Number(RowCount) + 1; i++) {
                var ItemName = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemName').value;
                var Rate = Number(document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_txtRate').value);
                if (Rate == 0 || Rate == '') {
                    alert('Please Enter Valid Rate For Item Name : ' + ItemName);
                }
               // alert(document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemDiscPer').value);
                //alert(document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemDiscAmt').value);
                //document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemDiscPer').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + 'hdnItemDiscPer').value;
                //document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemDiscAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemDiscAmt').value;
                //document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemTaxableAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemTaxableAmt').value;
                //document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemTaxAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemTaxAmt').value;
                //document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemTotalAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl0' + i + '_hdnItemTotalAmt').value;
            }
        }
    </script>

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
