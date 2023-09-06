<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StrPO.aspx.cs" Inherits="STORES_Transactions_Quotation_StrPO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<link href="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />--%>


    <%--<link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>--%>

    <%--  <style>
        #mdlPopupDel_backgroundElement {
            z-index: 100 !important;
        }

        #ctl00_ContentPlaceHolder1_pnlTaxDetail {
            z-index: 102 !important;
        }
    </style>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">PURCHASE ORDER PREPARATION</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#divPOtab" id="id1" runat="server" visible="true">Order Type</a>
                                <%-- <a class="nav-link active" data-toggle="tab" href="#divPOtab" id="id1">Order Type</a>--%>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#divTenderOrder" id="id2" runat="server" visible="false">Order For Tender</a>
                                <%--    <a class="nav-link" data-toggle="tab" href="#divTenderOrder" id="id2" style="display: none">Order For Tender</a>--%>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#divDPOrder" id="id3" runat="server" visible="false">Order For DP/PP</a>
                                <%-- <a class="nav-link" data-toggle="tab" href="#divDPOrder" id="id3" style="display: none">Order For DP/PP</a>--%>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#divQuotOrder" id="id4" runat="server" visible="false">Order For Quotation</a>
                                <%-- <a class="nav-link" data-toggle="tab" href="#divQuotOrder" id="id4" style="display: none">Order For Quotation</a>--%>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#divTermCond" id="id5" runat="server" visible="false">Term & Conditions</a>
                                <%--  <a class="nav-link" data-toggle="tab" href="#divTermCond" id="id5" style="display: none">Term & Conditions</a>--%>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#divSpecEncl" id="id6" runat="server" visible="false">Specification And Encl</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#divReports" id="id7" runat="server" visible="true">Reports</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#divPOLock" id="id8" runat="server" visible="true">PO Lock</a>
                                <%-- <a class="nav-link" data-toggle="tab" href="#divPOLock" id="id8" style="display: block">PO LOCK</a>--%>
                            </li>
                            <li class="nav-item">
                                <%--<a class="nav-link" data-toggle="tab" href="#divPOTrack" id="id9" runat="server" visible="false">PO Tracking</a>--%>
                                <a class="nav-link" data-toggle="tab" href="#divPOTrack" id="id9">PO Tracking</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="divPOtab">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="pnl" runat="server">
                                                <div class="col-12">
                                                    <%--   <div class="panel-heading">Select Type of Order</div>--%>
                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Select Type of Order</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="radSelType" runat="server">
                                                            <asp:ListItem Selected="True" Text="Quotation Purchase Order" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Open Tender Purchase Order" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Direct Purchase Order" Value="2"></asp:ListItem>
                                                            <%-- <asp:ListItem Text="Propritor Purchase Order" Value="3"></asp:ListItem>--%>
                                                            <%--<asp:ListItem Text="Limited Tender Purchase Order" Value="4"></asp:ListItem>--%>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShowPanel" runat="server" Text="Show Details" TabIndex="6" CssClass="btn btn-primary" OnClick="btnShowPanel_Click" />
                                    <%--15_02_2022--%>
                                    <%-- <input type="button" id="btnid" value="Show Details" class="btn btn-primary" onclick="ShowTabs();" />--%>
                                    <%--<asp:HiddenField ID="selected_tab" runat="server" />--%>
                                </div>

                            </div>


                            <!-- /.tab-pane -->
                            <div class="tab-pane fade" id="divTenderOrder">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="pnlorder1" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-12">
                                                            <div class="sub-heading">
                                                                <h5>Tender List</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Tender List</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddltendor" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="True"
                                                                OnSelectedIndexChanged="ddltendor_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-9 col-md-12 col-12">
                                                            <div class="row">
                                                                <div class="col-md-6">

                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Vendor Code :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblVcode" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Vendor Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblVname" runat="server"></asp:Label>
                                                                                <asp:HiddenField ID="hdnTvno" runat="server" />
                                                                            </a>
                                                                        </li>

                                                                    </ul>

                                                                </div>
                                                                <div class="col-md-6">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Vendor Contact:</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblVcontact" runat="server"></asp:Label></a>
                                                                        </li>

                                                                    </ul>


                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Vendor Address :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblVaddress" runat="server"></asp:Label></a>
                                                                        </li>
                                                                    </ul>


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-6 col-12">
                                                            <div class="sub-heading">
                                                                <h5>PO Info.</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Ref No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRefTender" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Date Of Sending</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar" id="Image2" runat="server"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtDtsendTender" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <!-- /.input group -->
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtDtsendTender"
                                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True">
                                                            </ajaxToolKit:MaskedEditExtender>

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtDtsendTender">
                                                            </ajaxToolKit:CalendarExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Supply Before Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar" id="Image1" runat="server"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtSdateTender" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <!-- /.input group -->
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtSdateTender"
                                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True">
                                                            </ajaxToolKit:MaskedEditExtender>

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtSdateTender">
                                                            </ajaxToolKit:CalendarExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubjectTender" runat="server" CssClass="form-control"></asp:TextBox>

                                                        </div>

                                                    <%--    //---------start---16-03-2023--%>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Our Reference No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOurRerenceNoTPO" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                        </div>
                                                        <%--    //---------end---16-03-2023--%>

                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>PO Items</h5>
                                                    </div>
                                                    <div class="col-md-12 table-responsive">
                                                        <asp:GridView ID="grdItemTender" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False"
                                                            DataKeyNames="TINO" HeaderStyle-CssClass="bg-light-blue" EmptyDataText="There is No Data For Vendor">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Item Name" DataField="ITEM_NAME" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>

                                                                <asp:BoundField HeaderText="Qty" DataField="QTY" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>

                                                                <asp:BoundField HeaderText="Price" DataField="PRICE" DataFormatString="{0:F}" NullDisplayText="-" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>

                                                                <asp:BoundField HeaderText="Disc Amt" DataField="DISCOUNT_AMOUNT" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>

                                                                <asp:BoundField HeaderText="Taxable Amt" DataField="TAXABLE_AMT" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>

                                                                <asp:TemplateField HeaderText="Tax Info" HeaderStyle-CssClass="bg-light-blue">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="btnAddTax" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add" OnClick="btnAddTenderTax_Click" /><%--OnClick="btnAddTax_Click" --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField HeaderText="Tax Amt" DataField="TAX_AMT" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>

                                                                <asp:BoundField HeaderText="Bill Amt" DataField="TOTAMOUNT" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>

                                                                <asp:TemplateField HeaderText="Oth Info" HeaderStyle-CssClass="bg-light-blue">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="btnAddOthInfo" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add Oth Info" OnClick="btnAddTenderOthInfo_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </div>

                                    </ContentTemplate>
                                    <%--   <Triggers>
                                        <asp:PostBackTrigger ControlID="ddltendor" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>

                            </div>
                            <div class="tab-pane fade" id="divDPOrder">

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>

                                        <div class="box-body">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-12">
                                                            <div class="sub-heading">
                                                                <h5>Order for DP/PP</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Indent Number</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlInd" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="True"
                                                                OnSelectedIndexChanged="ddlInd_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnOthEdit" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdnrowcount" runat="server" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Vendor Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="True"
                                                                OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnTaxableAmt" runat="server" Value="0" />
                                                        </div>
                                                        <div class="col-lg-3 col-md-6 col-12 mt-3">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Vendor Address :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblAdd" runat="server" Text=""></asp:Label>
                                                                        <asp:HiddenField ID="hdnListCount" runat="server" />

                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-3 col-md-6 col-12 mt-3">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Vendor Contact :</b>
                                                                    <a class="sub-label">
                                                                        <asp:HiddenField ID="hdnIndex" runat="server" />
                                                                        <asp:HiddenField ID="hdnBasicAmt" runat="server" Value="0" />
                                                                        <asp:Label ID="lblcontact" runat="server" Text=""></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>



                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-6 col-12">
                                                            <div class="sub-heading">
                                                                <h5>PO Info.</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Ref No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRefDPO" runat="server" CssClass="form-control" MaxLength="50" Enabled="false"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Date Of Sending</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="ImaCalDtSendDPO">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtDtSendDPO" runat="server" CssClass="form-control"></asp:TextBox>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtDtSendDPO"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender6"
                                                                    ControlToValidate="txtDtSendDPO" InvalidValueMessage="Date Of Sending Is Invalid (Enter In dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter Supply Before Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date Of Sending " ValidationGroup="Store"
                                                                    SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="true" />
                                                                <ajaxToolKit:CalendarExtender
                                                                    ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalDtSendDPO"
                                                                    TargetControlID="txtDtSendDPO">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDtSendDPO" Display="None"
                                                                    ErrorMessage="Enter Date Of Sending" SetFocusOnError="true" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Supply Before Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="ImaCalSDateDPO">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtSdateDPO" runat="server" CssClass="form-control"></asp:TextBox>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtSdateDPO"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender5"
                                                                    ControlToValidate="txtSdateDPO" InvalidValueMessage="Supply Before Date Is Invalid (Enter In dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter Supply Before Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Supply Before Date " ValidationGroup="Store"
                                                                    SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="true" />

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalSDateDPO" TargetControlID="txtSdateDPO">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSdateDPO" Display="None"
                                                                    ErrorMessage="Enter Supply Before Date" SetFocusOnError="true" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubjectDPO" runat="server" CssClass="form-control"></asp:TextBox>

                                                        </div>



                                                        
                                                    <%--    //---------start---16-03-2023--%>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Our Reference No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOurRerenceNoDPO" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                        </div>
                                                        <%--    //---------end---16-03-2023--%>

                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>PO Items.</h5>
                                                    </div>
                                                    <div class="panel-body">
                                                        <asp:ListView ID="lvItem" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div>
                                                                    <div class="sub-heading">
                                                                        <h5>Item List</h5>
                                                                    </div>
                                                                    <div class=" note-div">
                                                                        <h5 class="heading">Note</h5>
                                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Enter Rate And Discount Before Adding Tax </span></p>
                                                                    </div>
                                                                    <%-- <span class="box-tools pull-right" style="color: red">Note : Enter Rate And Discount Before Adding Tax</span>
                                                                    --%>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <%-- <th></th>--%>
                                                                                <th>Item Name</th>
                                                                                <th>Qty</th>
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
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="display: none">
                                                                        <asp:ImageButton ID="btnDeleteItem" runat="server" CausesValidation="false" ImageUrl="~/images/delete.png"
                                                                            CommandArgument='<%#Eval("ITEM_SRNO")%>' AlternateText="Delete Record" OnClick="btnDeleteItem_Click" />
                                                                        <asp:HiddenField ID="hdnItemSrNo" runat="server" Value='<%# Eval("ITEM_SRNO")%>' />
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnItemno" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                                        <asp:HiddenField ID="hdnTechSpec" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblItemQty" runat="server" CssClass="form-control" Text='<%# Eval("QTY")%>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnPordno" runat="server" Value='<%# Eval("PORDNO")%>' />
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="lblRate" runat="server" CssClass="form-control" Text='<%# Eval("RATE")%>' onblur="return CalOnRate(this);" MaxLength="9"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRate" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                                            TargetControlID="lblRate" ValidChars=".">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                        <asp:HiddenField ID="hdnOthItemRemark" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblDiscPer" runat="server" CssClass="form-control" MaxLength="5" Enabled="true" Text='<%# Eval("DISC_PER")%>' onblur="return CalOnDiscPer(this);"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftDiscper" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                                            TargetControlID="lblDiscPer" ValidChars=".">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                        <asp:HiddenField ID="hdnQualityQtySpec" runat="server" />
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
                                                                        <asp:ImageButton runat="server" ID="btnAddTax" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add" OnClientClick="return GetTaxableAmt(this);" OnClick="btnAddTax_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblTaxAmount" runat="server" Enabled="false" Text='<%# Eval("TAX_AMT")%>' CssClass="form-control"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnIsTax" runat="server" Value='<%# Eval("IS_TAX")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblBillAmt" runat="server" Enabled="false" Text='<%# Eval("BILL_AMT")%>' CssClass="form-control"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton runat="server" ID="btnAddOthInfo" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add Oth Info" OnClientClick="return GetOthInfoIndex(this);" OnClick="btnAddOthInfo_Click" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>

                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                                <div class="col-12" id="divItemCount" runat="server" visible="false">
                                                    <div class="row">

                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Number Of Items :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblItemCount" runat="server"></asp:Label></span> </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Total GRN Qty  :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblItemQtyCount" runat="server"></asp:Label></span></a>
                                                                </li>
                                                            </ul>
                                                        </div>

                                                    </div>

                                                </div>
                                            </asp:Panel>
                                        </div>

                                    </ContentTemplate>
                                    <%-- <Triggers>
                                        <asp:PostBackTrigger ControlID="ddlInd" />
                                        <asp:PostBackTrigger ControlID="ddlVendor" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="divQuotOrder">

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Quotation List</label>
                                                            </div>
                                                            <asp:DropDownList ID="lstQtNo" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                OnSelectedIndexChanged="lstQtNo_SelectedIndexChanged" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Vendor List</label>
                                                            </div>
                                                            <asp:DropDownList ID="lstVendor" runat="server" AutoPostBack="True" CssClass="form-control"
                                                                OnSelectedIndexChanged="lstVendor_SelectedIndexChanged" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12" id="pnlPoinfo" runat="server" visible="true">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="sub-heading">
                                                                <h5>PO Info.</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Ref No</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control" MaxLength="50" Enabled="false"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Date Of Sending</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="ImaCalDtSend">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtDtSend" runat="server" CssClass="form-control" ValidationGroup="Store"></asp:TextBox>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDtSend"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:CalendarExtender
                                                                    ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalDtSend"
                                                                    TargetControlID="txtDtSend">
                                                                </ajaxToolKit:CalendarExtender>

                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSBD" runat="server" visible="true">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Supply Before Date:</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="ImaCalSDate">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtSdate" runat="server" CssClass="form-control" ValidationGroup="Store"></asp:TextBox>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtSdate"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>

                                                                <ajaxToolKit:CalendarExtender ID="ceLasteDateofReciptTime" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalSDate" TargetControlID="txtSdate">
                                                                </ajaxToolKit:CalendarExtender>

                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDS" runat="server" visible="true">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSub" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>

                                                        </div>

                                                         <%--    //---------start---16-03-2023--%>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Our Reference No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOurReferenceNoQPO" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                        </div>
                                                        <%--    //---------end---16-03-2023--%>

                                                    </div>

                                                </div>
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>PO Items.</h5>
                                                    </div>

                                                </div>
                                                <div class="col-12 ">
                                                    <%--Quotaion po grid--%>
                                                    <asp:GridView ID="grdPOitems" runat="server" HeaderStyle-CssClass="bg-light-blue" CssClass="table table-bordered table-hover" AutoGenerateColumns="False"
                                                        DataKeyNames="PINO" EmptyDataText="There is No Data For Vendor">
                                                        <%--EnableViewState="false"--%>
                                                        <Columns>

                                                            <asp:BoundField HeaderText="Item Name" DataField="ITEM_NAME" HeaderStyle-CssClass="bg-light-blue">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>

                                                            <asp:BoundField HeaderText="Qty" DataField="QTY" HeaderStyle-CssClass="bg-light-blue"></asp:BoundField>

                                                            <asp:BoundField HeaderText="Rate" DataField="PRICE" DataFormatString="{0:F}"
                                                                NullDisplayText="-" HeaderStyle-CssClass="bg-light-blue"></asp:BoundField>

                                                            <asp:BoundField HeaderText="Disc Amt" DataField="DISCOUNT_AMOUNT" HeaderStyle-CssClass="bg-light-blue"></asp:BoundField>

                                                            <asp:BoundField HeaderText="Taxable Amt" DataField="TAXABLE_AMT" HeaderStyle-CssClass="bg-light-blue"></asp:BoundField>

                                                            <asp:TemplateField HeaderText="Tax Info" HeaderStyle-CssClass="bg-light-blue">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="btnAddTax" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add" OnClick="btnAddTax_Click" /><%--OnClick="btnAddTax_Click" --%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField HeaderText="Tax Amt" DataField="TAX_AMT" HeaderStyle-CssClass="bg-light-blue"></asp:BoundField>

                                                            <asp:BoundField HeaderText="Bill Amt" DataField="TOTAMOUNT" HeaderStyle-CssClass="bg-light-blue"></asp:BoundField>

                                                            <asp:TemplateField HeaderText="Oth Info" HeaderStyle-CssClass="bg-light-blue">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="btnAddOthInfo" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add Oth Info" OnClick="btnAddOthInfo_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>

                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lstVendor" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                            <div class="tab-pane fade" id="divTermCond">

                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel4" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="sub-heading">
                                                                <h5>Term & Conditions</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Note/Matter/Terms & Conditions</label>
                                                            </div>
                                                            <asp:TextBox ID="txtFooter" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px"
                                                                onkeyDown="checkTextAreaMaxLength(this,event,'500');" onkeyup="textCounter(this, this.form.remLen, 500);"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Remark</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRemark1" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px"
                                                                MaxLength="250"></asp:TextBox>
                                                        </div>
                                                       <%-- //--16-03-2023  ----------------------start-----------%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Nature Of Work</label>
                                                            </div>
                                                            <asp:TextBox ID="txtNatureofWork" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px"
                                                                MaxLength="400"></asp:TextBox>
                                                        </div>

                                                          <%-- //--16-03-2023  ----------------------end-----------%>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPaymentTerms" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Payment Terms</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTerm" runat="server" TextMode="MultiLine" Height="100px"
                                                                CssClass="form-control" MaxLength="500"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-12" id="divExtraCharges" runat="server" visible="false">
                                                    <div class="sub-heading">Extra Charges</div>
                                                    <div class="col-12 table-responsive">
                                                        <asp:GridView runat="server" ID="grdPartyFields" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" AutoGenerateColumns="False"
                                                            DataKeyNames="FNO">
                                                            <%--DataKeyNames="PFNO"--%>
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Field" DataField="FNAME" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Type" DataField="FTYPE" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Amount" DataField="AMT" DataFormatString="{0:F}"
                                                                    NullDisplayText="-" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Percetage(%)" DataField="PERCENTAGE"
                                                                    NullDisplayText="-" HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-12 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Signature Authority.</label>
                                                    </div>
                                                    <asp:RadioButton runat="server" ID="rdDirector" Text="Vice Chancellor" GroupName="g1" Checked="True" />
                                                    <asp:RadioButton runat="server" ID="rdforDirector" Text="For Vice Chancellor" GroupName="g1" />
                                                </div>

                                            </asp:Panel>
                                            <div class="col-12 btn-footer">
                                                <asp:Button runat="server" ID="btnSave" Text="Save P.O." CssClass="btn btn-primary" ValidationGroup="Store"
                                                    OnClick="btnSave_Click" OnClientClick="return ValidateRate();" />
                                                <asp:ValidationSummary ID="val" DisplayMode="List" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Store" />
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                    <%--   <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSave" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="divSpecEncl">

                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel5" runat="server">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-6 col-12">
                                                            <div class="sub-heading">
                                                                <h5>Specifications</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtech" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Technical Clarification</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTecClar" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50px"
                                                                MaxLength="500"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDelivery" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Delivery At</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDeliveryAt" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSchedule" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Delivery Schedule</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDeliverySchedule" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDespatch" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Mode of Despatch</label>
                                                            </div>
                                                            <asp:TextBox ID="txtModeofDespatch" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                        </div>


                                                        <div class="col-12" id="divInsured" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Insured</label>
                                                                    </div>
                                                                    <asp:RadioButtonList ID="rdlInsured" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divHandling" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Handling Charges</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtHandlingCharges" runat="server" CssClass="form-control" MaxLength="9" OnKeyUp="return ValidateNumeric(this)"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTransportation" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Transportation Charges</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTransportCharges" runat="server" CssClass="form-control" MaxLength="9" OnKeyUp="return ValidateNumeric(this)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-12" id="divBankGuranty" runat="server" visible="false">
                                                    <div class="col-md-12 well">
                                                        <div class="form-group col-md-3">
                                                            <asp:CheckBox ID="chkBankGty" runat="server" Text="Bank Guaranty" />
                                                        </div>
                                                        <div class="form-group col-md-2" style="padding-left: 50px">
                                                            <p>
                                                                <label>Amount</label>
                                                            </p>
                                                        </div>
                                                        <div class="form-group col-md-2">
                                                            <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" MaxLength="12" OnKeyUp="return ValidateNumeric(this)">0.00</asp:TextBox>
                                                            <asp:CompareValidator ID="cmpAmount" runat="server" ControlToValidate="txtAmount" Display="None"
                                                                Type="Double" Operator="DataTypeCheck" ErrorMessage="Amount Should Be Numeric" ValidationGroup="Store"></asp:CompareValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server" FilterType="Custom, Numbers"
                                                                TargetControlID="txtAmount" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                        </div>
                                                        <div class="form-group col-md-5">
                                                            <asp:TextBox runat="server" ID="txtBankRemark" CssClass="form-control" ToolTip="Any Remark" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12" id="divCIF" runat="server" visible="false">
                                                    <div class="row">
                                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label></label>
                                                            </div>
                                                            <asp:CheckBox ID="chkSD" runat="server" Text="Require SD" />
                                                        </div>
                                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>CIF Charges</label>
                                                            </div>
                                                            <asp:TextBox runat="server" ID="txtCifChrge" CssClass="form-control" MaxLength="4" OnKeyUp="return ValidateNumeric(this)">0.00</asp:TextBox>
                                                            <asp:CompareValidator
                                                                ID="cmpCIFCharge" runat="server" ControlToValidate="txtCifChrge" Display="None"
                                                                Type="Double" Operator="DataTypeCheck" ErrorMessage="CIF Charges Value Should Be Numeric"
                                                                ValidationGroup="Store"></asp:CompareValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                TargetControlID="txtCifChrge" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label></label>
                                                            </div>
                                                            <asp:TextBox runat="server" ID="txtCifSpec" CssClass="form-control" ToolTip="CIF Specification"
                                                                MaxLength="500"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label></label>
                                                            </div>
                                                            <asp:CheckBox ID="chkAgreement" runat="server" Text="Agreement" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label></label>
                                                            </div>
                                                            <asp:CheckBox ID="chkLC" runat="server" Text="L.C." /><asp:CheckBox ID="chkRelies"
                                                                runat="server" Text="Relies" /><asp:CheckBox ID="chkPending" runat="server" Text="Pending" /><asp:CheckBox
                                                                    ID="chkdelete" runat="server" Text="Delete" />
                                                        </div>
                                                    </div>

                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>ENCL</label>
                                                                <asp:TextBox ID="txtEncl" runat="server" CssClass="form-control" MaxLength="500"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Copy To</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCopyto" CssClass="form-control" runat="server" MaxLength="500"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Account Head</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAccHead" runat="server" CssClass="form-control"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Account Head</label>
                                                            </div>
                                                            <asp:CheckBox ID="chkEDexempt" runat="server" Text="E.D.Exemption" /><asp:CheckBox
                                                                ID="chkCSTExempt" runat="server" Text="CST Exemption" />
                                                        </div>

                                                    </div>

                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>



                            </div>
                            <div class="tab-pane fade" id="divReports">

                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Purchase Order <span style="color: red">*</span> </label>
                                                </div>
                                                <asp:DropDownList ID="lstPO" runat="server" AutoPostBack="True" CssClass="form-control" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="lstPO_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstPO" ValidationGroup="POReport"
                                                    ErrorMessage="Please Select Purchase Order" InitialValue="0" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <%--   <Triggers>
                                        <asp:PostBackTrigger ControlID="lstPO" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                                <div class="col-12 btn-footer">
                                    <asp:Button runat="server" ID="btnRpt" Text="View Purchase Order for Tender" CssClass="btn btn-primary" OnClick="btnRpt_Click" />
                                    <asp:Button runat="server" ID="btnPOReport" Text="Show Report" CssClass="btn btn-info" OnClick="btnPOReport_Click" ValidationGroup="POReport" />
                                    <asp:Button runat="server" ID="btnQuoRept" Text="PO Report" CssClass="btn btn-info" OnClick="btnQuoRept_Click" Visible="false" />
                                    <asp:Button runat="server" ID="btnPOApprovalRpt" Text="PO Approval Report" Visible="false" CssClass="btn btn-info" OnClick="btnPOApprovalRpt_Click1" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ValidationGroup="POReport" ShowMessageBox="true" ShowSummary="false" />

                                </div>

                            </div>

                            <div class="tab-pane fade" id="divPOLock">


                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>PO LOCK</h5>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-12 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Once You Lock PO ,You Cannot Modify It.-</span></p>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>Purchase Order<span style="color: red">*</span></label>
                                                    <asp:DropDownList ID="lstPOForLock" runat="server" AutoPostBack="True" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="lstPOForLock" ValidationGroup="POLock" Display="None"
                                                        ErrorMessage="Please Select Purchase Order" InitialValue="0"></asp:RequiredFieldValidator>

                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnLock" runat="server" Text="Lock PO" CssClass="btn btn-primary" ValidationGroup="POLock" OnClick="btnLock_Click" OnClientClick="return confirm('Are You Sure. You Want To Lock PO?');" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                <asp:ValidationSummary ID="valSum1" runat="server" DisplayMode="List" ValidationGroup="POLock" ShowMessageBox="true" ShowSummary="false" />


                                                <%-- OnClientClick="return confirm('Are you certain you want to delete this product?');"--%>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                    <%-- <Triggers>
                                        <asp:PostBackTrigger ControlID="btnLock" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>

                            </div>

                            <!-- 14022022 -->

                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">

                                        <ajaxToolKit:ModalPopupExtender ID="MdlPOPopup" runat="server" PopupControlID="pnlTaxDetail" TargetControlID="lblTax"
                                            BackgroundCssClass="modalBackground" BehaviorID="mdlPopupDel" CancelControlID="ImgTax">
                                        </ajaxToolKit:ModalPopupExtender>

                                        <asp:Label ID="lblTax" runat="server"></asp:Label>

                                        <asp:Panel ID="pnlTaxDetail" runat="server" CssClass="PopupReg" Style="display: none; height: auto; width: 40%; background: #fff; z-index: 101!important; box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;">
                                            <div class="col-12">
                                                <div class="sub-heading mt-3 mb-3">
                                                    <h5>Add Details</h5>

                                                    <div class="box-tools pull-right">
                                                        <asp:ImageButton ID="ImgTax" runat="server" ImageUrl="~/IMAGES/delete.png" ToolTip="Close" />
                                                    </div>
                                                </div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12" id="divTaxPopup" runat="server" visible="false">
                                                        <div class="col-12">
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
                                                                            <asp:TextBox ID="lblTaxAmount" runat="server" CssClass="form-control" Text='<%#Eval("TAX_AMOUNT") %>' onblur="CalTotTaxAmt(this)"></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                                TargetControlID="lblTaxAmount"
                                                                                FilterType="Custom"
                                                                                FilterMode="ValidChars"
                                                                                ValidChars="0123456789.">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
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
                                                                <asp:TextBox ID="txtTotTaxAmt" runat="server" CssClass="form-control" Enabled="false" />
                                                            </div>

                                                        </div>
                                                        <div class="form-group col-md-12" style="text-align: center">
                                                            <asp:Button ID="btnSaveTax" runat="server" CssClass="btn btn-primary" Text="Save Tax" OnClientClick="return GetTotTaxAmt();" OnClick="btnSaveTax_Click" />

                                                        </div>

                                                    </div>
                                                    <div class="form-group col-md-12" id="divOthPopup" runat="server" visible="false">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-group col-md-2">
                                                                <label>Technical Specification</label>
                                                            </div>
                                                            <div class="form-group col-md-10">
                                                                <asp:TextBox ID="txtTechSpec" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-12">
                                                            <div class="form-group col-md-2">
                                                                <label>Quality&Qty Specification</label>
                                                            </div>
                                                            <div class="form-group col-md-10">
                                                                <asp:TextBox ID="txtQualityQtySpec" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-12">
                                                            <div class="form-group col-md-2">
                                                                <label>Item Remark</label>
                                                            </div>
                                                            <div class="form-group col-md-10">
                                                                <asp:TextBox ID="txtItemRemarkOth" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-12" style="text-align: center">
                                                            <asp:Button ID="btnSaveOthInfo" runat="server" CssClass="btn btn-primary" Text="Add" OnClientClick="return SaveOthInfo();" OnClick="btnSaveOthInfo_Click" />
                                                        </div>

                                                    </div>

                                                    <div class="col-12" id="divPOPopup" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Serial No.</label>
                                                                </div>
                                                                <asp:TextBox ID="txtAppSrno" runat="server" Enabled="false" CssClass="form-control">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAppSrno"
                                                                    Display="None" ErrorMessage="Please Enter Serial Number" ValidationGroup="Submit" InitialValue="0"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                    TargetControlID="txtAppSrno"
                                                                    FilterType="Numbers"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars=" ">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Select User Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAppUser" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                    <%-- data-select2-enable="true"--%>
                                                                    <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAppUser"
                                                                    Display="None" ErrorMessage="Please Select User Name" ValidationGroup="App" InitialValue="0"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                        <div class=" col-12 btn-footer">
                                                            <asp:Button ID="btnAppSubmit" ValidationGroup="App" runat="server" Text="Add Authority" CssClass="btn btn-primary" OnClick="btnAppSubmit_Click" />
                                                            <asp:Button ID="btnAppCancel" runat="server" Text="Cancel" OnClick="btnAppCancel_Click" CssClass="btn btn-warning" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="App" />
                                                            <asp:Button ID="btnCreateApp" runat="server" Text="Submit" OnClick="btnCreateApp_Click" CssClass="btn btn-primary" />
                                                        </div>

                                                        <div class="form-group col-12">
                                                            <div>
                                                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                                                    <asp:GridView AutoGenerateColumns="false" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue"
                                                                        ID="grdAppQuot" runat="server" DataKeyNames="APP_NO">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImgAppEdit" runat="server" CommandArgument='<%# Eval("APP_NO")%>' CommandName='<%# Eval("UA_NO")%>' ImageUrl="~/IMAGES/edit.png" ToolTip="Edit" OnClick="ImgAppEdit_Click" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Serial No." DataField="SR_NO"
                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField HeaderText="User Name" DataField="UA_FULLNAME"
                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>




                            <!-- 14022022 -->


                            <%--</div>--%>


                            <div class="tab-pane fade" id="divPOTrack">

                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <%--    <div class="sub-heading">PO Tracking</div>--%>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Purchase Order <span style="color: red">*</span> </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPOTrack" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPOTrack_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                            </div>

                                            <div class="row" id="divPOT1" runat="server" visible="false">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Requisition No.:</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblReqNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>

                                                    </ul>
                                                </div>
                                                <div class="col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Quotation No. :</b>
                                                            <a class="sub-label;text-align:center">
                                                                <asp:Label ID="lblQuotNo" runat="server" Text="" Font-Bold="true"></asp:Label>

                                                            </a>
                                                        </li>

                                                    </ul>
                                                </div>

                                            </div>
                                            <div class="row" id="divPOT2" runat="server" visible="false">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Name of Supplier :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSupplier" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>

                                                    </ul>
                                                </div>

                                                <div class="col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Net Amount  :</b>
                                                            <a class="sub-label;text-align:center">
                                                                <asp:Label ID="lblNetTotal" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>

                                                    </ul>
                                                </div>

                                            </div>
                                            <div class="form-group col-12" id="divPOT4" runat="server" visible="false">
                                                <asp:Panel ID="pnlAuthhority" runat="server" Visible="false">
                                                    <asp:ListView ID="lvAuthority" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>Approval Status</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Authority</th>
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
                                                                <td><%# Eval("PANAME")%></td>
                                                                <td><%# Eval("APPSTATUS")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>

                                            </div>

                                        </div>

                                    </ContentTemplate>
                                    <%-- <Triggers>
                                        <asp:PostBackTrigger ControlID="ddlPOTrack" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>

                            </div>
                        </div>




                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script src="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>--%>

    <script type="text/javascript" language="javascript">
        // function for numeric textbox
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>

    <script type="text/javascript">
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }


        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }
    </script>

    <div id="divMsg" runat="server"></div>
    <%--<script>
        $(document).ready(function () {
            /*disable non active tabs*/
            $('.nav li').not('.active').addClass('disabled');
            /*to actually disable clicking the bootstrap tab, as noticed in comments by user3067524*/
            $('.nav li').not('.active').find('a').removeAttr("data-toggle");

            $('button').click(function () {
                /*enable next tab*/
                $('.nav li.active').next('li').removeClass('disabled');
                $('.nav li.active').next('li').find('a').attr("data-toggle", "tab")
            });
        });
    </script>--%>

    <%--<script type="text/javascript">
         var selected_tab = 1;
         $(function () {
             var tabs = $("#tabs").tabs({
                 select: function (e, i) {
                     selected_tab = i.index;
                 }
             });
             selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
             tabs.tabs('select', selected_tab);
             $("form").submit(function () {
                 $("[id$=selected_tab]").val(selected_tab);
             });
         });

</script>--%>

    <script type="text/javascript" language="javascript">

        // Tab Not Work Properly o test link thats y this is done
        function ShowTabs() {
            debugger;
            var lbl2 = document.getElementById("id2");
            var lbl3 = document.getElementById("id3");
            var lbl4 = document.getElementById("id4");
            var lbl5 = document.getElementById("id5");

            var lbl9 = document.getElementById("id9");

            var rdb = document.getElementById("ctl00_ContentPlaceHolder1_radSelType_0").value;
            var rdb1 = document.getElementById("ctl00_ContentPlaceHolder1_radSelType_1").value;
            var rdb2 = document.getElementById("ctl00_ContentPlaceHolder1_radSelType_2").value;

            var rb = document.getElementById("<%=radSelType.ClientID%>");
            var radio = rb.getElementsByTagName("input");
            var label = rb.getElementsByTagName("label");
            // alert(rb);
            // alert(label);

            if (radio[0].checked) {       ///For Quotation


                lbl2.style.display = "none";
                lbl3.style.display = "none";
                lbl4.style.display = "block";
                lbl5.style.display = "block";
                lbl9.style.display = "block";
            }
            else if (radio[1].checked)     ///For Tender
            {

                lbl2.style.display = "block";
                lbl3.style.display = "none";
                lbl4.style.display = "none";
                lbl5.style.display = "block";
                lbl9.style.display = "block";

            }

            else if (radio[2].checked) {  ///Direct


                lbl2.style.display = "none";
                lbl3.style.display = "block";
                lbl4.style.display = "none";
                lbl5.style.display = "block";
                lbl9.style.display = "block";

            }
            return false;

            // lbl2.style.display = "block";
            //lbl3.style.display = "block";
            //lbl4.style.display = "block";
            // lbl5.style.display = "block";
            //lbl6.style.display = "block";
            //lbl7.style.display = "block";
            // lbl9.style.display = "block";

        }

        function CalOnRate(crl) {
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblRate");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblItemQty').innerText;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
            var Discountamt = 0;
            if (Number(discount) == 0 || Number(discount) < 1)
                Discountamt = Number(discamt);
            else
                Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;



            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            var totamount = grossAmount;// + taxamt;

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = Discountamt.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_btnAddTax').disabled = true;
        }

        function CalOnDiscPer(crl) {


            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblDiscPer");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblItemQty').innerText;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;

            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            var totamount = grossAmount;

            if (discount == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').disabled = false;
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').disabled = true;
            }
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = totamount.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
        }

        function CalOnDiscAmount(crl) {

            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblDiscAmt");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblItemQty').innerText;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;

            var Discper = (Number(discamt).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            var totamount = grossAmount;// + taxamt;            
            var Discountamt = (Number(discamt).toFixed(2) * 1) / 1;

            if (discamt == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').disabled = false;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').disabled = true;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = totamount.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
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
                alert("Please Select Vendor Name.");
                return false;
            }
            var Rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var ITEMQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblItemQty').innerText;
            if (Number(Rate) > 0 && Number(ITEMQty) > 0) {
                document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value;
                document.getElementById('<%=hdnIndex.ClientID%>').value = index;
                document.getElementById('<%=hdnBasicAmt.ClientID%>').value = (Number(Rate) * Number(ITEMQty)).toFixed(2);
            }
            else {
                alert("Please Enter GRN Qty,Rate And Discount Before Adding Taxes.");
                return false;
            }
        }

        function GetOthInfoIndex(crl) {
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_btnAddOthInfo");
            var index = i[0];
            document.getElementById('<%=hdnIndex.ClientID%>').value = index;
            document.getElementById('<%=txtTechSpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnTechSpec').value;
            document.getElementById('<%=txtQualityQtySpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnQualityQtySpec').value;
            document.getElementById('<%=txtItemRemarkOth.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnOthItemRemark').value;
        }
        function SaveOthInfo() {

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnTechSpec').value = document.getElementById('<%=txtTechSpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnQualityQtySpec').value = document.getElementById('<%=txtQualityQtySpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnOthItemRemark').value = document.getElementById('<%=txtItemRemarkOth.ClientID%>').value;
            document.getElementById('<%=hdnOthEdit.ClientID%>').value = '1';
        }

        function GetTotTaxAmt() {
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblItemQty').disabled = false;
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblRate').disabled = false;
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + 0 + '_lblDiscPer').disabled = true;
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblDiscAmt').disabled = true;


            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
            var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
            var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);

        }
        function readListViewTextBoxes() {

            var score = 0;

            var ROWS = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblItemQty').innerText);
            }
            document.getElementById('<%= lblItemQtyCount.ClientID %>').innerHTML = score;

        }

    </script>


    <script type="text/javascript" language="javascript">
        //$.noConflict();
        function ValidateRate(crl) {

            if ($('#<%=radSelType.ClientID %> input[type=radio]:checked').val() == 0) {
                if (document.getElementById('<%=txtDtSend.ClientID%>').value != "")
                    if (ValidateDate(document.getElementById('<%=txtDtSend.ClientID%>').value, "Date Of Sending") == false) return false;
                if (document.getElementById('<%=txtSdate.ClientID%>').value != "")
                    if (ValidateDate(document.getElementById('<%=txtSdate.ClientID%>').value, "Supply Before Date") == false) return false;
            }
            else if ($('#<%=radSelType.ClientID %> input[type=radio]:checked').val() == 1) {
                if (document.getElementById('<%=txtDtsendTender.ClientID%>').value != "")
                    if (ValidateDate(document.getElementById('<%=txtDtsendTender.ClientID%>').value, "Date Of Sending") == false) return false;
                if (document.getElementById('<%=txtSdateTender.ClientID%>').value != "")
                    if (ValidateDate(document.getElementById('<%=txtSdateTender.ClientID%>').value, "Supply Before Date") == false) return false;
            }
            else if ($('#<%=radSelType.ClientID %> input[type=radio]:checked').val() == 2) {
                if (document.getElementById('<%=txtDtSendDPO.ClientID%>').value != "")
                    if (ValidateDate(document.getElementById('<%=txtDtSendDPO.ClientID%>').value, "Date Of Sending") == false) return false;
                if (document.getElementById('<%=txtSdateDPO.ClientID%>').value != "")
                    if (ValidateDate(document.getElementById('<%=txtSdateDPO.ClientID%>').value, "Supply Before Date") == false) return false;
            }


    var RowCount = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            var i = 0;
            for (i = 0; i < RowCount; i++) {
                // CalPaymentAmt += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvVendorPay_ctrl' + i + '_txtPayNowAmt').value);

                Rate = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblRate').value);
                var ItemNAme = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblItemName').innerHTML;

                if (Rate == '' || Number(Rate) == 0) {
                    alert('Rate Should Be Greater Than Zero For Item Name : ' + ItemNAme);
                    return false;
                }
            }
        }
        function ValidateDate(InputDate, InputText) {
            var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
            if (!(date_regex.test(InputDate))) {
                alert(InputText + ' Is Invalid (Enter In [dd/MM/yyyy] Format).');
                return false;
            }
        }

    </script>

    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    </script>
    <%--    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>--%>
</asp:Content>

