<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Vendor_Tender_Entry.aspx.cs" Inherits="STORES_Transactions_Quotation_Vendor_Tender_Entry"
    Title="" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <%-- <script src="../../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>
    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div5" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VENDOR ENTRY FOR TENDER</h3>

                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <%--  <ul class="nav nav-tabs">
                                <li class="active"><a href="#Div1" data-toggle="tab" aria-expanded="true">Tender Selection</a></li>
                                <li class=""><a href="#Div2" data-toggle="tab" aria-expanded="false">Tender Items</a></li>
                                 <li class=""><a href="#Div3" data-toggle="tab" aria-expanded="false">Extra Charges</a></li>
                                <li class=""><a href="#Div4" data-toggle="tab" aria-expanded="false">Comparative Statement</a></li>
                            </ul>--%>

                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#Div1" aria-expanded="true">Tender Selection</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link " data-toggle="tab" href="#Div2" aria-expanded="false">Tender Items</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link " data-toggle="tab" href="#Div4" aria-expanded="false">Comparative Statement</a>
                                    </li>
                                </ul>
                                <div class="tab-content active" id="my-tab-content">
                                    <div class="tab-pane active" id="Div1">
                                        <asp:UpdatePanel ID="updatePanel2" runat="server">
                                            <ContentTemplate>
                                                <%-- <form role="form">--%>
                                                <div class="box-body">

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Tender List</label>
                                                                </div>
                                                                <asp:Panel ID="pnl" runat="server">
                                                                    <asp:DropDownList ID="lstTender" CssClass="form-control" runat="server" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="lstTender_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvtender" runat="server" ControlToValidate="lstTender" ValidationGroup="Submit"
                                                                        ErrorMessage="Please Select Tender From List" Display="None" InitialValue="0"></asp:RequiredFieldValidator>

                                                                </asp:Panel>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Vendor List</label>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">

                                                                    <asp:DropDownList ID="lstVendor" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                        OnSelectedIndexChanged="lstVendor_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                         <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstVendor" ValidationGroup="Submit"
                                                                        ErrorMessage="Please Select Vendor From List" Display="None" InitialValue="0"></asp:RequiredFieldValidator>

                                                                    <asp:HiddenField ID="hdnIndex" runat="server" />
                                                                    <asp:HiddenField ID="hdnBasicAmt" runat="server" />
                                                                    <asp:HiddenField ID="hdnListCount" runat="server" />
                                                                    <asp:HiddenField ID="hdnRowCount" runat="server" />
                                                                    <asp:HiddenField ID="hdnTaxableAmt" runat="server" Value="0" />
                                                                    <asp:HiddenField ID="hdnOthEdit" runat="server" Value="0" />
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <div class="col-md-12">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Tendor Details</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Tender Number  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblTREFNO" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>EMD:</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblEMD" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-4 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Tender Amount  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblTAMT" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Tender Opening Date :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblTOpen" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-4 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Sales Tax  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblSalesTax" runat="server"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Total Amount  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </asp:Panel>

                                                </div>


                                                <div class="box-body">
                                                    <asp:Panel ID="Panel4" runat="server">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Vendor Information</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Vendor Code  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblVCode" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Vendor Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblvnd" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-3 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Contact Number :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblContact" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Vendor Address  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblvndadd" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-3 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">

                                                                        <li class="list-group-item"><b>CST :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblcst" runat="server"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Vendor Remark  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblremark" runat="server" TextMode="MultiLine"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-3 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">

                                                                        <li class="list-group-item"><b>BST  :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblbst" runat="server"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Email Address :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                </div>

                                                <div class="pull-right d-none">
                                                    <asp:ImageButton ID="imgFieldNext" runat="server" ImageUrl="~/Images/next.jpeg" OnClick="imgFieldNext_Click"
                                                        ToolTip="Next" />
                                                </div>

                                                <%-- </form>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane fade" id="Div2">
                                        <asp:UpdatePanel ID="updatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Item List</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel5" runat="server">
                                                            <div class="col-12 table-responsive">
                                                                <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdItemList" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="ITEM_NO">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Item Name" DataField="ITEM_NAME" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ControlStyle Width="120px" />
                                                                        </asp:BoundField>

                                                                        <%-- <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="bg-light-blue">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" Text='<%#Eval("ITEM_UNIT") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hdnTINO" runat="server" Value='<%#Eval("TINO") %>' />
                                                                                <asp:Label ID="lblQty" Text='<%#Eval("QTY") %>' runat="server"></asp:Label>
                                                                                <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("QTY") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" TabIndex="1" Text='<%#Eval("PRICE") %>' MaxLength="5" onblur="return AddTotalAmountsOnRate(this)" onKeyUp="return validateNumeric(this)">
                                                                                </asp:TextBox>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Disc%" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDisc" runat="server" onblur="return AddTotalAmountsOnDiscount(this);" Enabled="true" Text='<%#Eval("DISCOUNT_PERCENT") %>' MaxLength="3" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtDiscFilter" runat="server" FilterType="Numbers,Custom"
                                                                                    FilterMode="ValidChars" TargetControlID="txtDisc" ValidChars=".">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <asp:HiddenField ID="hfDiscountAmount" runat="server" Value="0" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Disc Amt" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDiscAmt" runat="server" onblur="return AddTotalAmountsOnDiscountAmount(this)" Enabled="true" Text='<%#Eval("DISCOUNT_AMOUNT") %>'
                                                                                    MaxLength="9" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftpDiscamt" runat="server" FilterType="Numbers,Custom"
                                                                                    FilterMode="ValidChars" TargetControlID="txtDiscAmt" ValidChars=".">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Taxable Amt" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtTaxableAmt" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text='<%#Eval("TAXABLE_AMT") %>'>
                                                                                </asp:TextBox>
                                                                                <asp:HiddenField ID="hdnOthItemRemark" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tax Info" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="btnAddTax" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add" OnClientClick="return GetTaxableAmt(this);" OnClick="btnAddTax_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tax Amt" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtTaxAmt" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text='<%#Eval("TAX_AMT") %>'>
                                                                                </asp:TextBox>
                                                                                <asp:HiddenField ID="hdnTechSpec" runat="server" />
                                                                                <asp:HiddenField ID="hdnQualityQtySpec" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bill Amt" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text='<%#Eval("TOTAMOUNT") %>'>
                                                                                </asp:TextBox>
                                                                                <asp:HiddenField ID="hfTotalAmount" runat="server" Value='<%#Eval("TOTAMOUNT") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Oth Info" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="btnAddOthInfo" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add Oth Info" OnClientClick="return GetOthInfoIndex(this);" OnClick="btnAddOthInfo_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <AlternatingRowStyle CssClass="altitem" />
                                                                    <EmptyDataRowStyle CssClass="altitem" />
                                                                    <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                                                    <RowStyle CssClass="item" />
                                                                </asp:GridView>
                                                            </div>

                                                            <div class="col-md-12 text-center">
                                                                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnsave_Click" CssClass="btn btn-primary" TabIndex="6" ToolTip="Click To Submit" ValidationGroup="Submit" />
                                                                <asp:Button ID="btnModify" runat="server" Visible="false" Text="Modify" OnClick="btnModify_Click" CssClass="btn btn-warning" TabIndex="7" ToolTip="Click To Modify" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="7" ToolTip="Click To Reset" />
                                                                <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            </div>

                                                        </asp:Panel>

                                                    </div>
                                                </div>
                                                <div class="pull-left d-none">

                                                    <asp:ImageButton ID="imgVendorPrevious" runat="server" ImageUrl="~/images/prev.jpeg"
                                                        ToolTip="Previous" OnClick="imgVendorPrevious_Click1" />


                                                </div>
                                                <div class="pull-right d-none">
                                                    <asp:ImageButton ID="imgVendorNext" runat="server" ImageUrl="~/images/next.jpeg"
                                                        ToolTip="Next" OnClick="imgVendorNext_Click1" />
                                                </div>



                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="Div4">
                                        <asp:UpdatePanel ID="updatePanel4" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <asp:Panel ID="pnlCmpst" runat="server">

                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Tender List</label>
                                                                    </div>
                                                                    <asp:Panel ID="Panel10" runat="server">
                                                                        <asp:DropDownList ID="lstTender1" runat="server"  TabIndex="1" AppendDataBoundItems="true" CssClass="form-control multi-select-demo">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                          </asp:DropDownList><%--AutoPostBack="True"  OnSelectedIndexChanged="lstTender1_SelectedIndexChanged"--%>
                                                                        <asp:RequiredFieldValidator ID="rfvTender1" runat="server" ControlToValidate="lstTender1" ValidationGroup="Comparative" Display="None"
                                                                            ErrorMessage="Please Select Tender From List" InitialValue="0"></asp:RequiredFieldValidator>

                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Item List</label>
                                                                    </div>
                                                                    <asp:Panel ID="Panel11" runat="server">
                                                                        <asp:ListBox ID="lstItem" runat="server" TabIndex="2" CssClass="form-control multi-select-demo"></asp:ListBox>

                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btncmpall" runat="server" OnClick="btncmpall_Click"
                                                            Text="Comparative Statement Excel" CssClass="btn btn-primary" ValidationGroup="Comparative"/>

                                                        <asp:Button ID="btnCmpAlliPdf" runat="server" 
                                                            Text="Comparative Stmt. For All Item" Visible="false" CssClass="btn btn-primary" />

                                                        <asp:Button ID="btncmpitem" runat="server" OnClick="btncmpitem_Click"
                                                            Text="Comparative Stmt. For Single Item" Visible="false" CssClass="btn btn-primary" />

                                                        <asp:Button ID="btnSendForApproval" runat="server" Visible="false" OnClick="btnSendForApproval_Click"
                                                            Text="Send For Approval" CssClass="btn btn-primary" />
                                                        <asp:ValidationSummary id="valSumComp" runat="server" ValidationGroup="Comparative" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:UpdatePanel ID="updpopup" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-12">

                                                <ajaxToolKit:ModalPopupExtender ID="MdlTax" runat="server" PopupControlID="pnlTaxDetail" TargetControlID="lblTax"
                                                    BackgroundCssClass="modalBackground" BehaviorID="mdlPopupDel" CancelControlID="ImgTax">
                                                </ajaxToolKit:ModalPopupExtender>

                                                <asp:Label ID="lblTax" runat="server"></asp:Label>

                                                <asp:Panel ID="pnlTaxDetail" runat="server" CssClass="PopupReg" Style="display: none; height: auto; width: 50%; background: #fff; z-index: 333; box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;">
                                                    <div class="col-12">
                                                        <div class=" sub-heading mt-3 ">
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
                                                                                            <th style="width: 15%">Tax Name                                                                              
                                                                                            </th>
                                                                                            <th style="width: 15%">Tax Amount
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
                                                                                <td style="width: 15%">
                                                                                    <asp:Label ID="lblTaxName" runat="server" Text='<%#Eval("TAX_NAME") %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnTaxId" runat="server" Value='<%#Eval("TAXID") %>' />
                                                                                </td>
                                                                                <td style="width: 15%">
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
                                                            <div class="form-group col-md-12" id="divOthPopup" runat="server" visible="false">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <div class="label-dynamic">
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

                                                                                <label>Item Remark</label>
                                                                            </div>
                                                                            <asp:TextBox ID="txtItemRemarkOth" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class=" col-12 btn-footer">
                                                                    <asp:Button ID="btnSaveOthInfo" runat="server" CssClass="btn btn-primary" Text="Add" OnClientClick="return SaveOthInfo();" OnClick="btnSaveOthInfo_Click" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btncmpall"/>
        </Triggers>
    </asp:UpdatePanel>
    <table cellpadding="0" cellspacing="0" width="100%">
        <%--<tr>
            <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;">VENDOR TENDER ENTRY
        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false"
            AlternateText="Page help" ToolTip="Page Help" />

            </td>


        </tr>--%>
        <%-- Flash the text/border red and fade in the "close" button --%>        <%--  Shrink the info panel out of view --%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>

            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td style="padding-left: 10px; padding-right: 10px;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <br />
                            <ajaxToolKit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="1" Width="990px"
                                CssClass="ajax__tab_yuitabview-theme">
                                <ajaxToolKit:TabPanel runat="server" ID="TabPanel1"
                                    TabIndex="0">
                                    <%--<HeaderTemplate>
                                        Tender Selection
                                    </HeaderTemplate>--%>
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <br />
                                                    <div id="div1" style="display: block;">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td style="padding-left: 10px">
                                                                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td style="padding-left: 10px; padding-right: 10px" align="center">
                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td colspan="2" align="center">
                                                                                                <%--<fieldset class="fieldset ">
                                                                                                    <legend class="legend">Tender List</legend>--%>
                                                                                                <br />
                                                                                                <%--<asp:ListBox ID="lstTender" runat="server" AutoPostBack="True" Width="300px"
                                                                                                        Height="200px" OnSelectedIndexChanged="lstTender_SelectedIndexChanged"></asp:ListBox>--%>
                                                                                                <br />
                                                                                                <br />
                                                                                                </fieldset>
                                                                                            </td>
                                                                                            <td colspan="2" style="padding-left: 10px" align="center">
                                                                                                <%--<fieldset class="fieldset ">
                                                                                                    <legend class="legend">Vendor List</legend>--%>
                                                                                                <br />
                                                                                                <%-- <asp:ListBox ID="lstVendor" runat="server" AutoPostBack="True" Width="300px" Height="200px"
                                                                                                        OnSelectedIndexChanged="lstVendor_SelectedIndexChanged"></asp:ListBox>--%>
                                                                                                <br />
                                                                                                <br />
                                                                                                </fieldset>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="padding-left: 10px; padding-right: 10px" align="center">
                                                                                    <br />
                                                                                    <%-- <fieldset class="fieldset">
                                                                                        <legend class="legend">Tendor Details</legend>--%>
                                                                                    <br />
                                                                                    <table cellpadding="0" cellspacing="0" width="90%">
                                                                                        <tr>
                                                                                            <td align="left" style="padding-left: 10px; width: 50%">
                                                                                                <%--<b>Tender Number : </b>
                                                                                                    <asp:Label ID="lblTREFNO" runat="server"></asp:Label>--%>
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 10px;">
                                                                                                <%--<b>Tender Opening Date : </b>
                                                                                                <asp:Label ID="lblTOpen" runat="server"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" style="padding-left: 10px; height: 16px;">
                                                                                                <%--<b>EMD : </b>
                                                                                                <asp:Label ID="lblEMD" runat="server"></asp:Label>--%>
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 10px; height: 16px;">
                                                                                                <%-- <b>Sales Tax : </b>
                                                                                                <asp:Label ID="lblSalesTax" runat="server"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" style="padding-left: 10px;">
                                                                                                <%--<b>Tender Amount : </b>
                                                                                                <asp:Label ID="lblTAMT" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 10px;">
                                                                                                <b>Total Amount : </b>
                                                                                                <asp:Label ID="lblTotal" runat="server"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    </fieldset>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="padding-left: 10px; padding-right: 10px;" align="center">
                                                                                    <br />
                                                                                    <%--<fieldset class="fieldset">
                                                                                        <legend class="legend">Vendor Information</legend>--%>
                                                                                    <br />
                                                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                                                        <tr style="padding-left: 15px;" align="left">
                                                                                            <td style="padding-left: 10px;"><%--Vendor Code:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblVCode" runat="server" Width="200px"></asp:Label>--%>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px;"><%--CST:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblcst" runat="server" Width="200px"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="padding-left: 15px;" align="left">
                                                                                            <td style="padding-left: 10px;"><%--Vendor Name:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblvnd" runat="server" Width="200px"></asp:Label>--%>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px;"><%--BST:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblbst" runat="server" Width="200px"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="padding-left: 15px;" align="left">
                                                                                            <td style="padding-left: 10px;"><%--Contact Number:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblContact" runat="server" Width="200px"></asp:Label>--%>
                                                                                            </td>
                                                                                            <td style="padding-left: 10px;"><%--Email Address:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblEmail" runat="server" Width="200px"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                                                        <tr style="padding-left: 10px;" align="left">
                                                                                            <td style="padding-left: 10px; width: 18%"><%--Vendor Address:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblvndadd" runat="server" Height="40px" Width="580px"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="padding-left: 10px;" align="left">
                                                                                            <td style="padding-left: 10px;"><%--Vendor Remark:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblremark" runat="server" TextMode="MultiLine" Height="40px" Width="580px"></asp:Label>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    </fieldset>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table>
                                            <tr>
                                                <td align="right" style="padding-left: 740px">
                                                    <%--<asp:ImageButton ID="imgFieldNext" runat="server" ImageUrl="~/images/next.jpeg" OnClick="imgFieldNext_Click"
                                                        ToolTip="Next" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolKit:TabPanel>
                                <ajaxToolKit:TabPanel runat="server" ID="TabPanel2" Width="90%">

                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <br />
                                                    <div id="divItems" style="display: block;">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td style="padding-left: 10px; padding-right: 10px">
                                                                    <asp:Panel ID="pnlitems" runat="server" Width="100%">
                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td colspan="2">

                                                                                    <br />
                                                                                    <br />
                                                                                    </fieldset>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="990">
                                                                        <caption>
                                                                            <br />
                                                                        <tr>
                                                                            <%--<td align="left" style="padding-left: 10px">
                                                                                    <asp:ImageButton ID="imgVendorPrevious" runat="server" ImageUrl="~/images/prev.jpeg"
                                                                                        ToolTip="Previous" OnClick="imgVendorPrevious_Click1" />
                                                                                </td>
                                                                                <td align="right" style="padding-left: 690px">
                                                                                    <asp:ImageButton ID="imgVendorNext" runat="server" ImageUrl="~/images/next.jpeg"
                                                                                        ToolTip="Next" OnClick="imgVendorNext_Click1" />--%>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            </caption>
                                                        </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ContentTemplate>
                                </ajaxToolKit:TabPanel>
                                <ajaxToolKit:TabPanel runat="server" ID="TabPanel3">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="padding-right: 10px" align="center">
                                                    <br />
                                                    <div id="div2" style="display: block;">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td style="padding-left: 10px">
                                                                    <asp:Panel ID="pnlEcharge" runat="server" Width="100%">
                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td colspan="2" style="padding-left: 10px; padding-right: 10px">
                                                                                    <%--<fieldset class="fieldset">
                                                                                        <legend class="legend">Fields List</legend>--%>
                                                                                    <br />
                                                                                    <%--<asp:GridView CssClass="vista-grid" runat="server" ID="grdAllFields" AutoGenerateColumns="False"
                                                                                        AllowPaging="True" AllowSorting="True" DataKeyNames="FNO" EmptyDataText="NO Fields Found"
                                                                                        OnPageIndexChanging="grdAllFields_PageIndexChanging" OnSorting="grdAllFields_Sorting"
                                                                                        OnSelectedIndexChanging="grdAllFields_SelectedIndexChanging">
                                                                                        <Columns>
                                                                                            <asp:BoundField HeaderText="Fields" DataField="FNAME" SortExpression="FIELDS" />
                                                                                            <asp:BoundField HeaderText="FTYPE" DataField="FTYPE" SortExpression="FTYPE" />
                                                                                            <asp:BoundField HeaderText="IND/FORG" DataField="IND_FOR" />
                                                                                            <asp:CommandField HeaderText="Select Field" ShowSelectButton="True" ButtonType="Image"
                                                                                                SelectImageUrl="~/images/action_down.gif" SelectText="Add This Field" />
                                                                                        </Columns>
                                                                                        <AlternatingRowStyle CssClass="altitem" />
                                                                                        <EmptyDataRowStyle CssClass="altitem" />
                                                                                        <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                                                                        <PagerSettings Mode="NextPrevious" NextPageText="Next" PreviousPageText="Prev" />
                                                                                        <RowStyle CssClass="item" />
                                                                                    </asp:GridView>--%>
                                                                                    <br />
                                                                                    </fieldset>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="padding-left: 10px; padding-right: 10px">
                                                                                    <%-- <fieldset class="fieldset">
                                                                                        <legend class="legend">Calculative</legend>--%>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%--<asp:GridView ID="grdCalField" runat="server" CssClass="vista-grid " AutoGenerateColumns="False"
                                                                                                    HorizontalAlign="Left" DataKeyNames="FNO" Width="700px">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField HeaderText="Fields" DataField="FNAME">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField HeaderText="SrNo" DataField="FSRNO">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField HeaderText="Type" DataField="FTYPE">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Amount">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox runat="server" ID="txtCalAmt">0</asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Calc. Depend On">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox runat="server" ID="txtCalDpon">0</asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <AlternatingRowStyle CssClass="altitem" />
                                                                                                    <EmptyDataRowStyle CssClass="altitem" />
                                                                                                    <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                                                                                    <RowStyle CssClass="item" />
                                                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                                </asp:GridView>--%>
                                                                                            </td>
                                                                                            <caption>
                                                                                                <br />
                                                                                            </caption>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    </fieldset>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="padding-left: 10px; padding-right: 10px">
                                                                                    <%--<fieldset class="fieldset">
                                                                                        <legend class="legend">Percentage</legend>--%>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%--<asp:GridView ID="grdperfield" runat="server" CssClass="vista-grid " AutoGenerateColumns="False"
                                                                                                    HorizontalAlign="Left" DataKeyNames="FNO" Width="700px" ItemStyle-HorizontalAlign="Left">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField HeaderText="Fields" DataField="FNAME">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField HeaderText="SrNo" DataField="FSRNO">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField HeaderText="Type" DataField="FTYPE">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Percetage(%)">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox runat="server" ID="txtPer">0</asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Calc. Depend On">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox runat="server" ID="txtCalDpon">0</asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <AlternatingRowStyle CssClass="altitem" />
                                                                                                    <EmptyDataRowStyle CssClass="altitem" />
                                                                                                    <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                                                                                    <RowStyle CssClass="item" />
                                                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                                </asp:GridView>--%>
                                                                                            </td>
                                                                                            <caption>
                                                                                                <br />
                                                                                            </caption>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    </fieldset>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="padding-left: 10px; padding-right: 10px;">
                                                                                    <%--<fieldset class="fieldset">
                                                                                        <legend class="legend">Informative</legend>--%>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <%--<asp:GridView ID="grdInfoField" runat="server" CssClass="vista-grid " AutoGenerateColumns="False"
                                                                                                    HorizontalAlign="Left" DataKeyNames="FNO" Width="700px" ItemStyle-HorizontalAlign="Left">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField HeaderText="Fields" DataField="FNAME">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField HeaderText="SrNo" DataField="FSRNO">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField HeaderText="Type" DataField="FTYPE">
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Information">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox runat="server" ID="txtInfo" MaxLength="400" Width="200px" Font-Size="Smaller"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <AlternatingRowStyle CssClass="altitem" />
                                                                                                    <EmptyDataRowStyle CssClass="altitem" />
                                                                                                    <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                                                                                    <RowStyle CssClass="item" />
                                                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                                </asp:GridView>--%>
                                                                                            </td>
                                                                                            <caption>
                                                                                                <br />
                                                                                            </caption>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    </fieldset>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-left: 10px; padding-right: 10px" align="center">
                                                                                    <fieldset class="fieldset1 ">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <%--<asp:Button ID="Button1" runat="server" ValidationGroup="Store" Text="Submit" OnClick="btnsave_Click" />
                                                                                                    <asp:Button ID="btnModify" runat="server" Text="Modify" OnClick="btnModify_Click" />
                                                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
                                                                                                </td>
                                                                                                <caption>
                                                                                                    <br />
                                                                                                </caption>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <br />
                                                                                    </fieldset>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ContentTemplate>
                                </ajaxToolKit:TabPanel>
                                <ajaxToolKit:TabPanel runat="server" ID="TabPanel4">

                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="cmpStatement" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <div id="div3" style="display: block;">
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td style="padding-left: 10px" align="center">
                                                                            <%--<asp:Panel ID="pnlCmpst" runat="server" Width="100%">--%>
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td style="padding-left: 10px">
                                                                                        <%-- <fieldset class="fieldset">
                                                                                            <legend class="legend">Tender List</legend>--%>
                                                                                        <br />
                                                                                        <%--<asp:ListBox ID="lstTender1" runat="server" AutoPostBack="True" Height="200px"
                                                                                                OnSelectedIndexChanged="lstTender1_SelectedIndexChanged" Width="300px"></asp:ListBox>--%>
                                                                                        <br />
                                                                                        <br />
                                                                                        </fieldset>
                                                                                    </td>
                                                                                    <td style="padding-left: 10px">
                                                                                        <%--<fieldset class="fieldset">
                                                                                            <legend class="legend">Item List</legend>
                                                                                            <br />
                                                                                            <asp:ListBox ID="lstItem" runat="server" Height="200px" Width="300px"></asp:ListBox>
                                                                                            <br />--%>
                                                                                        <br />
                                                                                        </fieldset>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-left: 10px"></td>
                                                                                    <td style="padding-left: 10px"></td>
                                                                                </tr>
                                                                            </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2" style="padding-left: 10px">
                                                    <%--<asp:Button ID="btncmpall" runat="server" OnClick="btncmpall_Click"
                                                        Text="Comparative Stmt. For All Item in Excel" />
                                                </td>
                                                <td colspan="2" style="padding-left: 10px">
                                                    <asp:Button ID="btnCmpAlliPdf" runat="server" OnClick="btnCmpAlliPdf_Click"
                                                        Text="Comparative Stmt. For All Item" />
                                                </td>
                                                <td colspan="2" style="padding-left: 10px">
                                                    <asp:Button ID="btncmpitem" runat="server" OnClick="btncmpitem_Click"
                                                        Text="Comparative Stmt. For Single Item" Visible="false" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GvCmpAllItem" runat="server">
                                                    </asp:GridView>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolKit:TabPanel>
                            </ajaxToolKit:TabContainer>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server"></div>
    <script language="javascript">
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

    </script>

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

        function AddTotalAmountsOnTax(crl) {

            var st = crl.id.split("grdItemList_ctl");
            var i = st[1].split("_txtField");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').value;
            var tax = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtField').value;

            var Discper = (Number(discount).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;
            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            var taxamt = grossAmount * Number(tax).toFixed(2) / 100;
            var totamount = grossAmount + taxamt;


            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txttaxamt').value = taxamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfTotalAmount').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfTaxAmount').value = taxamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfDiscountAmount').value = Discountamt.toFixed(2);

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

            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;

            var taxableAmt = grossAmount;// + taxamt;


            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfTotalAmount').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxableAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfDiscountAmount').value = discamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').value = discamt.toFixed(2);

        }

        function AddTotalAmountsOnDiscount(crl) {


            var st = crl.id.split("grdItemList_ctl");
            var i = st[1].split("_txtDisc");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').value;

            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            //var Discper = (Number(discamt).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;              
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            var taxableAmt = grossAmount;

            if (discount == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').disabled = false;
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').disabled = true;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfTotalAmount').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfDiscountAmount').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxableAmt').value = taxableAmt.toFixed(2);
        }

        function AddTotalAmountsOnDiscountAmount(crl) {
            debugger;

            var st = crl.id.split("grdItemList_ctl");
            var i = st[1].split("_txtDiscAmt");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hdQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtRate').value;
            //var discount = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDiscAmt').value;


            var Discper = (Number(discamt).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;
            //var Discountamt = (Number(rate).toFixed(2) * Number(Discper).toFixed(2) * qty) / 100;             
            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            var taxableAmt = grossAmount;// + taxamt;            
            var Discountamt = (Number(discamt).toFixed(2) * 1) / 1;

            if (discamt == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').disabled = false;
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtDisc').disabled = true;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtAmt').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfTotalAmount').value = taxableAmt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_hfDiscountAmount').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxableAmt').value = taxableAmt.toFixed(2);

        }

        function GetTaxableAmt(crl) {
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
                document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + index + '_txtTaxableAmt').value;
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


            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_txtTaxAmt').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;

            var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_txtTaxableAmt').value;
            var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_txtAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);

        }
        function SaveOthInfo() {

            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnTechSpec').value = document.getElementById('<%=txtTechSpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnQualityQtySpec').value = document.getElementById('<%=txtQualityQtySpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_grdItemList_ctl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnOthItemRemark').value = document.getElementById('<%=txtItemRemarkOth.ClientID%>').value;
            document.getElementById('<%=hdnOthEdit.ClientID%>').value = '1';
        }

        function CalTotTaxAmt(crl) {
            debugger;
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
